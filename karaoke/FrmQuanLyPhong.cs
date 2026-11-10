using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using karaoke.Services;

namespace karaoke
{
    public partial class FrmQuanLyPhong : Form
    {
        private readonly RoomService service = new RoomService();
        private string selectedMaPhong;
        private List<RoomRow> allRooms = new List<RoomRow>();
        private readonly Dictionary<string, Button> roomButtons = new Dictionary<string, Button>();
        private readonly Timer searchTimer = new Timer { Interval = 250 };

        public FrmQuanLyPhong()
        {
            InitializeComponent();

            dataGridViewPhong.ReadOnly = true;
            dataGridViewPhong.AllowUserToAddRows = false;
            dataGridViewPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPhong.MultiSelect = false;
            dataGridViewPhong.CellClick += dataGridViewPhong_CellClick;
            searchTimer.Tick += (sender, e) => { searchTimer.Stop(); ApplyRoomFilter(); };
            Disposed += (sender, e) => searchTimer.Dispose();

            Load += FrmQuanLyPhong_Load;
        }

        private void Run(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmQuanLyPhong_Load(object sender, EventArgs e)
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            Run(() => {
                bool admin = AuthService.IsAdmin(AuthService.RequireCurrentUser());
                btnThem.Enabled = btnSua.Enabled = admin;
                btnXoa.Enabled = admin;
            });
            cbTrangThai.Items.Clear();
            cbTrangThai.Items.AddRange(new object[] { "Trống", "Đang sử dụng", "Bảo trì" });
            cbTrangThai.SelectedIndex = 0;
            cboLocTrangThai.SelectedIndex = 0;

            LoadComboBoxLoaiPhong();
            cbLoaiPhong.SelectedIndexChanged += cbLoaiPhong_SelectedIndexChanged;

            numSucChua.Minimum = 1;
            numSucChua.Maximum = 1000;

            Run(LoadDanhSachPhong);
        }

        private void LoadComboBoxLoaiPhong()
        {
            cbLoaiPhong.Items.Clear();
            cbLoaiPhong.Items.Add(new LoaiPhongItem { TenLoai = "Phòng thường", DonGia = 80000 });
            cbLoaiPhong.Items.Add(new LoaiPhongItem { TenLoai = "Phòng VIP", DonGia = 150000 });


            if (cbLoaiPhong.Items.Count > 0)
                cbLoaiPhong.SelectedIndex = 0;
        }

        private void cbLoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLoaiPhong.SelectedItem is LoaiPhongItem selectedType)
            {
                txtDonGia.Text = selectedType.DonGia.ToString("0.##");
            }
            else if (cbLoaiPhong.SelectedItem is string typeName)
            {
                if (typeName == "Phòng VIP")
                    txtDonGia.Text = "150000";
                else
                    txtDonGia.Text = "80000";
            }
        }

        private void LoadDanhSachPhong()
        {
            allRooms = service.List();
            ApplyRoomFilter();
        }

        private void BoLocPhong_Changed(object sender, EventArgs e)
        {
            searchTimer.Stop();
            if (sender == txtTimPhong)
                searchTimer.Start();
            else
                ApplyRoomFilter();
        }

        private void ApplyRoomFilter()
        {
            string keyword = txtTimPhong.Text.Trim();
            string status = cboLocTrangThai.SelectedItem as string;
            var comparison = CultureInfo.GetCultureInfo("vi-VN").CompareInfo;
            var rooms = allRooms.Where(room =>
                (keyword.Length == 0 ||
                 comparison.IndexOf(room.MaPhong ?? "", keyword, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) >= 0 ||
                 comparison.IndexOf(room.TenPhong ?? "", keyword, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) >= 0) &&
                (string.IsNullOrEmpty(status) || status == "Tất cả" || room.TrangThai == status)).ToList();

            dataGridViewPhong.DataSource = rooms;
            dataGridViewPhong.ClearSelection();
            dataGridViewPhong.CurrentCell = null;
            SetGridHeaders();

            flowLayoutPanelPhong.SuspendLayout();
            var oldButtons = flowLayoutPanelPhong.Controls.Cast<Control>().ToArray();
            flowLayoutPanelPhong.Controls.Clear();
            foreach (var oldButton in oldButtons) oldButton.Dispose();
            roomButtons.Clear();

            foreach (var room in rooms)
            {
                var btn = new Button
                {
                    Width = 140,
                    Height = 100,
                    Text = room.TenPhong + "\n" + room.LoaiPhong + "\n" + room.TrangThai,
                    BackColor = GetRoomColor(room.TrangThai),
                    UseVisualStyleBackColor = false
                };

                btn.Click += (s, e) => SelectRoom(room);
                flowLayoutPanelPhong.Controls.Add(btn);
                roomButtons[room.MaPhong] = btn;
            }

            flowLayoutPanelPhong.ResumeLayout();
            UpdateRoomCount(rooms.Count);
            if (selectedMaPhong != null)
            {
                HighlightGridRow(selectedMaPhong);
            }
        }

        private void SetGridHeaders()
        {
            var headers = new Dictionary<string, string>
            {
                { "MaPhong", "Mã phòng" }, { "TenPhong", "Tên phòng" },
                { "LoaiPhong", "Loại phòng" }, { "DonGia", "Đơn giá/giờ" },
                { "SucChua", "Sức chứa" }, { "TrangThai", "Trạng thái" }
            };
            foreach (DataGridViewColumn column in dataGridViewPhong.Columns)
            {
                string title;
                if (headers.TryGetValue(column.DataPropertyName, out title)) column.HeaderText = title;
                if (column.DataPropertyName == "DonGia") column.DefaultCellStyle.Format = "N0";
            }
        }

        private void UpdateRoomCount(int visibleCount)
        {
            lblSoPhong.Text = string.Format("Hiển thị {0} / {1} phòng", visibleCount, allRooms.Count);
            if (selectedMaPhong != null && !roomButtons.ContainsKey(selectedMaPhong))
                lblSoPhong.Text += " | Đang chọn: " + selectedMaPhong + " (ngoài bộ lọc)";
        }

        private Color GetRoomColor(string trangThai)
        {
            switch (trangThai)
            {
                case "Trống":
                    return Color.LightGreen;
                case "Đang sử dụng":
                    return Color.LightCoral;
                case "Bảo trì":
                    return Color.LightYellow;
                default:
                    return Color.LightGray;
            }
        }

        private void SelectRoom(RoomRow r)
        {
            selectedMaPhong = r.MaPhong;
            txtMaPhong.Text = r.MaPhong;
            txtMaPhong.ReadOnly = true;
            txtTenPhong.Text = r.TenPhong;

            cbLoaiPhong.Text = r.LoaiPhong;

            txtDonGia.Text = r.DonGia.ToString("0.##");

            int val = r.SucChua;
            if (val < numSucChua.Minimum) val = (int)numSucChua.Minimum;
            if (val > numSucChua.Maximum) val = (int)numSucChua.Maximum;
            numSucChua.Value = val;

            cbTrangThai.SelectedItem = r.TrangThai;

            HighlightGridRow(r.MaPhong);
            UpdateRoomCount(dataGridViewPhong.Rows.Count);

            Button roomButton;
            if (roomButtons.TryGetValue(r.MaPhong, out roomButton) && flowLayoutPanelPhong.Visible)
                flowLayoutPanelPhong.ScrollControlIntoView(roomButton);
        }

        private void HighlightGridRow(string roomId)
        {
            foreach (DataGridViewRow row in dataGridViewPhong.Rows)
            {
                if (row.DataBoundItem is RoomRow item && item.MaPhong == roomId)
                {
                    row.Selected = true;
                    if (dataGridViewPhong.Columns.Count > 0)
                    {
                        dataGridViewPhong.CurrentCell = row.Cells[0];
                        if (!row.Displayed && dataGridViewPhong.Visible)
                            dataGridViewPhong.FirstDisplayedScrollingRowIndex = row.Index;
                    }
                    break;
                }
            }
        }

        private void dataGridViewPhong_CellClick(object s, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridViewPhong.Rows[e.RowIndex].DataBoundItem is RoomRow r)
            {
                SelectRoom(r);
            }
        }

        private void Save(bool create)
        {
            decimal price;
            if (!decimal.TryParse(txtDonGia.Text, out price) || price < 0)
                throw new InvalidOperationException("Đơn giá không hợp lệ.");

            if (!create && string.IsNullOrEmpty(selectedMaPhong))
                throw new InvalidOperationException("Vui lòng chọn phòng cần sửa.");

            string maPhong = create ? txtMaPhong.Text.Trim() : selectedMaPhong;
            if (string.IsNullOrEmpty(maPhong))
                throw new InvalidOperationException("Mã phòng không được để trống.");

            string loaiPhongStr = cbLoaiPhong.Text.Trim();

            service.Save(
                maPhong,
                txtTenPhong.Text.Trim(),
                loaiPhongStr,
                price,
                (int)numSucChua.Value,
                Convert.ToString(cbTrangThai.SelectedItem),
                create
            );

            LoadDanhSachPhong();
            ClearForm();
        }

        private void btnThem_Click(object s, EventArgs e) => Run(() => Save(true));
        private void btnSua_Click(object s, EventArgs e) => Run(() => Save(false));

        private void btnXoa_Click(object s, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaPhong))
            {
                MessageBox.Show("Vui lòng chọn phòng cần xóa từ danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa phòng đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Run(() =>
                {
                    service.Delete(selectedMaPhong);
                    LoadDanhSachPhong();
                    ClearForm();
                });
            }
        }

        private void btnLamMoi_Click(object s, EventArgs e) => Run(() =>
        {
            ClearForm();
            LoadDanhSachPhong();
        });

        private void ClearForm()
        {
            selectedMaPhong = null;
            txtMaPhong.ReadOnly = false;
            txtMaPhong.Clear();
            txtTenPhong.Clear();
            if (cbLoaiPhong.Items.Count > 0) cbLoaiPhong.SelectedIndex = 0;
            txtDonGia.Clear();
            numSucChua.Value = 10;
            cbTrangThai.SelectedIndex = 0;
            dataGridViewPhong.ClearSelection();
            UpdateRoomCount(dataGridViewPhong.Rows.Count);
        }
    }

    public class LoaiPhongItem
    {
        public string TenLoai { get; set; }
        public decimal DonGia { get; set; }
        public override string ToString() => TenLoai;
    }
}
