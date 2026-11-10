using System;
using System.Drawing;
using System.Windows.Forms;
using karaoke.Services;

namespace karaoke
{
    public partial class FrmQuanLyPhong : Form
    {
        private readonly RoomService service = new RoomService();
        private string selectedMaPhong;

        public FrmQuanLyPhong()
        {
            InitializeComponent();

            dataGridViewPhong.ReadOnly = true;
            dataGridViewPhong.AllowUserToAddRows = false;
            dataGridViewPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPhong.MultiSelect = false;

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
            var rooms = service.List();
            dataGridViewPhong.DataSource = rooms;

            flowLayoutPanelPhong.SuspendLayout();
            flowLayoutPanelPhong.Controls.Clear();

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
            }

            flowLayoutPanelPhong.ResumeLayout();
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

            foreach (DataGridViewRow row in dataGridViewPhong.Rows)
            {
                if (row.DataBoundItem is RoomRow item && item.MaPhong == r.MaPhong)
                {
                    row.Selected = true;
                    if (dataGridViewPhong.Columns.Count > 0)
                    {
                        dataGridViewPhong.CurrentCell = row.Cells[0];
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
        }
    }

    public class LoaiPhongItem
    {
        public string TenLoai { get; set; }
        public decimal DonGia { get; set; }
        public override string ToString() => TenLoai;
    }
}