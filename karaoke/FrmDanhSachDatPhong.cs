using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using karaoke.Services;
using karaoke.UI;

namespace karaoke
{
    public partial class FrmDanhSachDatPhong : Form
    {
        private readonly BookingService bookingService = new BookingService();

        private class TrangThaiItem
        {
            public string Value { get; set; }
            public string Text { get; set; }
            public override string ToString() { return Text; }
        }

        public FrmDanhSachDatPhong()
        {
            InitializeComponent();
            foreach (var button in new[] { btnTimKiem, btnLamMoi, btnThemDat, btnSuaDat, btnHuyDat, btnNhanPhong })
            {
                button.Resize += (sender, args) => ButtonActive.RoundButton((Button)sender);
                ButtonActive.RoundButton(button);
            }
            this.Load += FrmDanhSachDatPhong_Load;
        }

        private void FrmDanhSachDatPhong_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            try {
                AuthService.RequireCurrentUser();
                btnThemDat.Enabled = true;
                btnSuaDat.Enabled = true;
                btnHuyDat.Enabled = true;
                btnNhanPhong.Visible = true;
                btnNhanPhong.Enabled = true;
            } catch (Exception ex) { MessageBox.Show(ex.Message); Close(); return; }
            InitGrid();
            InitFilterControls();
            LoadBookings();
        }

        private void InitGrid()
        {
            dgvDanhSach.AutoGenerateColumns = false;
            dgvDanhSach.Columns.Clear();

            dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaDatPhong",
                HeaderText = "Mã đặt",
                Width = 70
            });
            dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenPhong",
                HeaderText = "Phòng",
                Width = 100
            });
            dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "LoaiPhong",
                HeaderText = "Loại phòng",
                Width = 100
            });
            dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenKhach",
                HeaderText = "Khách hàng",
                Width = 140
            });
            dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SDT",
                HeaderText = "SĐT",
                Width = 100
            });
            dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoKhach",
                HeaderText = "Số khách",
                Width = 80
            });
            dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GioBatDauDuKien",
                HeaderText = "Bắt đầu dự kiến",
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" },
                Width = 135
            });
            dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GioKetThucDuKien",
                HeaderText = "Kết thúc dự kiến",
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" },
                Width = 135
            });
            dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DonGiaGioDaChot",
                HeaderText = "Đơn giá/h",
                DefaultCellStyle = { Format = "N0" },
                Width = 95
            });
            dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TrangThai",
                HeaderText = "Trạng thái",
                Width = 110
            });
            dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenNV",
                HeaderText = "NV tiếp nhận",
                Width = 110
            });
            dgvDanhSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GhiChu",
                HeaderText = "Ghi chú",
                Width = 100
            });

            // Chia đều không gian theo tỉ lệ, để cột cuối vẫn hiện ở cửa sổ mặc định.
            foreach (DataGridViewColumn column in dgvDanhSach.Columns)
            {
                column.FillWeight = column.Width;
                column.MinimumWidth = 40;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgvDanhSach.Columns[6].MinimumWidth = 105;
            dgvDanhSach.Columns[7].MinimumWidth = 105;
            dgvDanhSach.Columns[11].MinimumWidth = 65;
            dgvDanhSach.EnableHeadersVisualStyles = false;
            dgvDanhSach.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 101, 52);
            dgvDanhSach.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDanhSach.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvDanhSach.DefaultCellStyle.SelectionBackColor = Color.FromArgb(16, 185, 129);
            dgvDanhSach.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void InitFilterControls()
        {
            cbTrangThai.Items.Clear();
            cbTrangThai.Items.Add(new TrangThaiItem { Value = "TatCa", Text = "Tất cả trạng thái" });
            cbTrangThai.Items.Add(new TrangThaiItem { Value = "ChoNhan", Text = "Chờ nhận (Đang đặt)" });
            cbTrangThai.Items.Add(new TrangThaiItem { Value = "DaNhan", Text = "Đã nhận phòng" });
            cbTrangThai.Items.Add(new TrangThaiItem { Value = "DaHuy", Text = "Đã hủy" });
            cbTrangThai.SelectedIndex = 0;

            dtpTuNgay.Value = DateTime.Today;
            dtpDenNgay.Value = DateTime.Today.AddDays(7);
        }

        private void chkLocNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpTuNgay.Enabled = chkLocNgay.Checked;
            dtpDenNgay.Enabled = chkLocNgay.Checked;
        }

        private void cbTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBookings();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadBookings();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            cbTrangThai.SelectedIndex = 0;
            chkLocNgay.Checked = false;
            dtpTuNgay.Value = DateTime.Today;
            dtpDenNgay.Value = DateTime.Today.AddDays(7);
            LoadBookings();
        }

        private void LoadBookings()
        {
            try
            {
                var filter = new BookingFilter
                {
                    SearchText = txtTimKiem.Text.Trim()
                };

                var item = cbTrangThai.SelectedItem as TrangThaiItem;
                if (item != null)
                {
                    filter.TrangThai = item.Value;
                }

                if (chkLocNgay.Checked)
                {
                    filter.TuNgay = dtpTuNgay.Value.Date;
                    filter.DenNgay = dtpDenNgay.Value.Date;
                }

                var list = bookingService.ListBookings(filter);
                dgvDanhSach.DataSource = list;
                lblTongSo.Text = string.Format("Tổng số phiếu đặt: {0}", list.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách đặt phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private BookingRow GetSelectedBooking()
        {
            if (dgvDanhSach.CurrentRow != null && dgvDanhSach.CurrentRow.DataBoundItem is BookingRow row)
            {
                return row;
            }
            return null;
        }

        private void btnThemDat_Click(object sender, EventArgs e)
        {
            using (var form = new FrmDatPhong())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    LoadBookings();
                }
            }
        }

        private void btnSuaDat_Click(object sender, EventArgs e)
        {
            var booking = GetSelectedBooking();
            if (booking == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu đặt phòng cần chỉnh sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (booking.TrangThai != "ChoNhan")
            {
                MessageBox.Show(string.Format("Phiếu đặt phòng đang ở trạng thái '{0}', không thể chỉnh sửa.", booking.TrangThai), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var form = new FrmDatPhong(booking.MaDatPhong))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    LoadBookings();
                }
            }
        }

        private void btnHuyDat_Click(object sender, EventArgs e)
        {
            var booking = GetSelectedBooking();
            if (booking == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu đặt phòng cần hủy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (booking.TrangThai != "ChoNhan")
            {
                MessageBox.Show(string.Format("Phiếu đặt phòng đang ở trạng thái '{0}', không thể hủy.", booking.TrangThai), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                string.Format("Bạn có chắc chắn muốn hủy phiếu đặt phòng #{0} của khách {1} ({2})?", booking.MaDatPhong, booking.TenKhach, booking.TenPhong),
                "Xác nhận hủy đặt phòng",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes) return;

            try
            {
                bookingService.CancelBooking(booking.MaDatPhong, "Hủy bởi người dùng");
                MessageBox.Show("Hủy phiếu đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBookings();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể hủy đặt phòng: " + ex.GetBaseException().Message, "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNhanPhong_Click(object sender, EventArgs e)
        {
            var booking = GetSelectedBooking();
            if (booking == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu đặt phòng cần nhận phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (booking.TrangThai != "ChoNhan")
            {
                MessageBox.Show(string.Format("Phiếu đặt phòng đang ở trạng thái '{0}', không thể nhận phòng.", booking.TrangThai), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                string.Format("Xác nhận nhận phòng và bắt đầu tính giờ hát cho khách '{0}' tại '{1}'?\n\nĐơn giá chốt: {2:N0} đ/giờ",
                    booking.TenKhach, booking.TenPhong, booking.DonGiaGioDaChot),
                "Xác nhận nhận phòng",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes) return;

            try
            {
                int maPhien = bookingService.CheckInBooking(booking.MaDatPhong);
                MessageBox.Show(
                    string.Format("Nhận phòng thành công!\nPhiên hát mới #{0} đã được mở cho phòng {1}.\nTrạng thái phòng đã chuyển sang 'Đang sử dụng'.", maPhien, booking.TenPhong),
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                LoadBookings();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể nhận phòng: " + ex.GetBaseException().Message, "Thông báo kiểm tra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvDanhSach_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDanhSach.Columns[e.ColumnIndex].DataPropertyName == "TrangThai" && e.Value != null)
            {
                string status = e.Value.ToString();
                switch (status)
                {
                    case "ChoNhan":
                        e.Value = "Chờ nhận";
                        e.CellStyle.ForeColor = Color.FromArgb(22, 101, 52);
                        e.CellStyle.Font = new Font(dgvDanhSach.Font, FontStyle.Bold);
                        break;
                    case "DaNhan":
                        e.Value = "Đã nhận";
                        e.CellStyle.ForeColor = Color.FromArgb(37, 99, 235);
                        break;
                    case "DaHuy":
                        e.Value = "Đã hủy";
                        e.CellStyle.ForeColor = Color.FromArgb(156, 163, 175);
                        break;
                }
            }
        }

        private void dgvDanhSach_DoubleClick(object sender, EventArgs e)
        {
            var booking = GetSelectedBooking();
            if (booking != null && booking.TrangThai == "ChoNhan")
            {
                btnSuaDat_Click(sender, e);
            }
        }
    }
}
