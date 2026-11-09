using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using karaoke.Services;
using karaoke.UI;

namespace karaoke
{
    public partial class FrmThanhToan : Form
    {
        private readonly PaymentService paymentService = new PaymentService();
        private int currentMaPhien = 0;
        private InvoiceBillingDetail currentBilling = null;

        public FrmThanhToan()
        {
            InitializeComponent();

            foreach (var btn in new[] { btnLamMoiPhong, btnThanhToan, btnInHoaDon, btnTimLichSu, btnInLaiHD })
            {
                ButtonActive.RoundButton(btn);
            }

            this.Load += FrmThanhToan_Load;
        }

        private void FrmThanhToan_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            InitGrids();
            cboPhuongThuc.SelectedIndex = 0;

            // Bộ lọc ngày mặc định: 30 ngày qua
            dtpTuNgay.Value = DateTime.Today.AddDays(-30);
            dtpDenNgay.Value = DateTime.Today;

            LoadActiveSessions();
            LoadInvoiceHistory();
        }

        private void InitGrids()
        {
            // Grid phòng đang hát
            dgvPhongDangHat.AutoGenerateColumns = false;
            dgvPhongDangHat.Columns.Clear();
            dgvPhongDangHat.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenPhong",
                HeaderText = "Phòng",
                Width = 110
            });
            dgvPhongDangHat.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "LoaiPhong",
                HeaderText = "Loại",
                Width = 90
            });
            dgvPhongDangHat.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TienPhongTamTinh",
                HeaderText = "Tạm tính",
                DefaultCellStyle = { Format = "N0" },
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            FormatGridStyle(dgvPhongDangHat);

            // Grid dịch vụ
            dgvDichVu.AutoGenerateColumns = false;
            dgvDichVu.Columns.Clear();
            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenDV",
                HeaderText = "Tên dịch vụ",
                Width = 180
            });
            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DonViTinh",
                HeaderText = "ĐVT",
                Width = 60
            });
            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoLuong",
                HeaderText = "SL",
                Width = 50
            });
            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DonGia",
                HeaderText = "Đơn giá",
                Width = 110,
                DefaultCellStyle = { Format = "N0" }
            });
            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ThanhTien",
                HeaderText = "Thành tiền",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = { Format = "N0" }
            });
            FormatGridStyle(dgvDichVu);

            // Grid lịch sử hóa đơn
            dgvLichSu.AutoGenerateColumns = false;
            dgvLichSu.Columns.Clear();
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaHD",
                HeaderText = "Mã HD",
                Width = 70
            });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NgayThanhToan",
                HeaderText = "Ngày TT",
                Width = 130,
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
            });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenPhong",
                HeaderText = "Phòng",
                Width = 90
            });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TienPhong",
                HeaderText = "Tiền phòng",
                Width = 100,
                DefaultCellStyle = { Format = "N0" }
            });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TienDichVu",
                HeaderText = "Tiền dịch vụ",
                Width = 100,
                DefaultCellStyle = { Format = "N0" }
            });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GiamGia",
                HeaderText = "Giảm giá",
                Width = 90,
                DefaultCellStyle = { Format = "N0" }
            });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ThanhTien",
                HeaderText = "Tổng thanh toán",
                Width = 130,
                DefaultCellStyle = { Format = "N0", Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) }
            });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PhuongThucTT",
                HeaderText = "Hình thức",
                Width = 100
            });
            dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenNV",
                HeaderText = "Thu ngân",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            FormatGridStyle(dgvLichSu);
        }

        private void FormatGridStyle(DataGridView grid)
        {
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 101, 52);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            grid.ColumnHeadersHeight = 32;
            grid.RowTemplate.Height = 28;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(187, 247, 208);
            grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(22, 101, 52);
        }

        /// <summary>
        /// Nạp danh sách các phòng đang hát chờ thanh toán
        /// </summary>
        private void LoadActiveSessions()
        {
            try
            {
                var sessions = paymentService.GetActiveSessions();
                dgvPhongDangHat.DataSource = sessions;

                if (sessions.Count == 0)
                {
                    ResetBillingUI();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPhongDangHat_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPhongDangHat.SelectedRows.Count == 0) return;

            var row = dgvPhongDangHat.SelectedRows[0].DataBoundItem as PaymentSessionRow;
            if (row == null) return;

            currentMaPhien = row.MaPhien;
            LoadBillingForSession(currentMaPhien);
        }

        private void LoadBillingForSession(int maPhien)
        {
            try
            {
                decimal giamGia = numGiamGia.Value;
                currentBilling = paymentService.CalculateBillingDetail(maPhien, DateTime.Now, giamGia);

                // Cập nhật thông tin phòng
                lblTenPhong.Text = string.Format("Phòng: {0} ({1})", currentBilling.TenPhong, currentBilling.LoaiPhong);
                lblGioVao.Text = string.Format("Giờ bắt đầu: {0:dd/MM/yyyy HH:mm}", currentBilling.GioBatDau);
                lblGioRa.Text = string.Format("Giờ kết thúc: {0:dd/MM/yyyy HH:mm}", currentBilling.GioKetThuc);
                lblThoiGianHat.Text = string.Format("Thời gian hát: {0} giờ {1} phút ({2:F2}h)",
                    (int)currentBilling.ThoiGianHat.TotalHours,
                    currentBilling.ThoiGianHat.Minutes,
                    currentBilling.TongGioHat);
                lblDonGia.Text = string.Format("Đơn giá giờ: {0:N0} đ/h", currentBilling.DonGiaGio);
                lblTienPhongUocTinh.Text = string.Format("Tiền phòng: {0:N0} đ", currentBilling.TienPhong);

                // Cập nhật danh sách dịch vụ
                dgvDichVu.DataSource = currentBilling.DanhSachDichVu;
                lblTongTienDVGrid.Text = string.Format("Tổng tiền dịch vụ: {0:N0} đ", currentBilling.TienDichVu);

                // Cập nhật thanh toán
                lblSumTienPhongVal.Text = currentBilling.TienPhong.ToString("N0") + " đ";
                lblSumTienDVVal.Text = currentBilling.TienDichVu.ToString("N0") + " đ";
                lblThanhTienVal.Text = currentBilling.ThanhTien.ToString("N0") + " đ";

                TinhTienThua();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tính tiền phiên hát: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void numGiamGia_ValueChanged(object sender, EventArgs e)
        {
            if (currentMaPhien > 0)
            {
                LoadBillingForSession(currentMaPhien);
            }
        }

        private void txtTienKhachDua_TextChanged(object sender, EventArgs e)
        {
            TinhTienThua();
        }

        private void TinhTienThua()
        {
            if (currentBilling == null)
            {
                lblTienThuaVal.Text = "0 đ";
                return;
            }

            string text = txtTienKhachDua.Text.Replace(",", "").Replace(".", "").Trim();
            if (decimal.TryParse(text, out decimal tienDua))
            {
                decimal tienThua = tienDua - currentBilling.ThanhTien;
                if (tienThua >= 0)
                {
                    lblTienThuaVal.ForeColor = Color.FromArgb(22, 101, 52);
                    lblTienThuaVal.Text = tienThua.ToString("N0") + " đ";
                }
                else
                {
                    lblTienThuaVal.ForeColor = Color.FromArgb(220, 38, 38);
                    lblTienThuaVal.Text = string.Format("Còn thiếu {0:N0} đ", Math.Abs(tienThua));
                }
            }
            else
            {
                lblTienThuaVal.Text = "0 đ";
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (currentMaPhien <= 0 || currentBilling == null)
            {
                MessageBox.Show("Vui lòng chọn phòng cần thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string phuongThuc = cboPhuongThuc.SelectedItem?.ToString() ?? "Tiền mặt";
            decimal giamGia = numGiamGia.Value;

            var confirm = MessageBox.Show(
                string.Format("Xác nhận thanh toán cho phòng '{0}'?\n\n- Tiền phòng: {1:N0} đ\n- Tiền dịch vụ: {2:N0} đ\n- Giảm giá: {3:N0} đ\n- TỔNG THANH TOÁN: {4:N0} đ\n- Hình thức: {5}",
                currentBilling.TenPhong,
                currentBilling.TienPhong,
                currentBilling.TienDichVu,
                giamGia,
                currentBilling.ThanhTien,
                phuongThuc),
                "Xác nhận thanh toán",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes) return;

            try
            {
                int maHD = paymentService.CompletePayment(currentMaPhien, giamGia, phuongThuc, currentBilling.GioKetThuc);

                MessageBox.Show(
                    string.Format("Thanh toán thành công! Mã hóa đơn: HD{0:D6}", maHD),
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Mở cửa sổ in hóa đơn cho khách
                using (var frmIn = new FrmInHoaDon(currentBilling, maHD))
                {
                    frmIn.ShowDialog(this);
                }

                // Reset và tải lại
                ResetBillingUI();
                LoadActiveSessions();
                LoadInvoiceHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực hiện thanh toán: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (currentBilling == null)
            {
                MessageBox.Show("Vui lòng chọn phòng cần xem hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var frmIn = new FrmInHoaDon(currentBilling, null))
            {
                frmIn.ShowDialog(this);
            }
        }

        private void btnLamMoiPhong_Click(object sender, EventArgs e)
        {
            LoadActiveSessions();
        }

        private void ResetBillingUI()
        {
            currentMaPhien = 0;
            currentBilling = null;
            lblTenPhong.Text = "Phòng: (Chưa chọn phòng)";
            lblGioVao.Text = "Giờ bắt đầu: -";
            lblGioRa.Text = "Giờ kết thúc: -";
            lblThoiGianHat.Text = "Thời gian hát: -";
            lblDonGia.Text = "Đơn giá giờ: -";
            lblTienPhongUocTinh.Text = "Tiền phòng: -";
            dgvDichVu.DataSource = null;
            lblTongTienDVGrid.Text = "Tổng tiền dịch vụ: 0 đ";
            lblSumTienPhongVal.Text = "0 đ";
            lblSumTienDVVal.Text = "0 đ";
            lblThanhTienVal.Text = "0 đ";
            numGiamGia.Value = 0;
            txtTienKhachDua.Clear();
            lblTienThuaVal.Text = "0 đ";
        }

        // ==========================
        // TAB LỊCH SỬ HÓA ĐƠN
        // ==========================

        private void LoadInvoiceHistory()
        {
            try
            {
                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date;
                string tuKhoa = txtTimKiemHD.Text.Trim();

                var list = paymentService.GetInvoiceHistory(tuNgay, denNgay, null, tuKhoa);
                dgvLichSu.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimLichSu_Click(object sender, EventArgs e)
        {
            LoadInvoiceHistory();
        }

        private void btnInLaiHD_Click(object sender, EventArgs e)
        {
            if (dgvLichSu.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn trong danh sách để xem/in.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var item = dgvLichSu.SelectedRows[0].DataBoundItem as InvoiceHistoryRow;
            if (item == null) return;

            try
            {
                var detail = paymentService.GetSavedInvoiceDetail(item.MaHD);
                using (var frmIn = new FrmInHoaDon(detail, item.MaHD))
                {
                    frmIn.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở chi tiết hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
