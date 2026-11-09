using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using karaoke.Services;
using karaoke.UI;

namespace karaoke
{
    public partial class FrmThongKe : Form
    {
        private readonly StatisticService statisticService = new StatisticService();

        public FrmThongKe()
        {
            InitializeComponent();

            foreach (var btn in new[] { btnHomNay, btn7Ngay, btnThangNay, btnNamNay, btnXemThongKe, btnXuatBaoCao })
            {
                ButtonActive.RoundButton(btn);
            }

            this.Load += FrmThongKe_Load;
        }

        private void FrmThongKe_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            InitGrids();

            // Mặc định: Đầu tháng hiện tại đến hôm nay
            DateTime now = DateTime.Today;
            dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
            dtpDenNgay.Value = now;

            TaiDuLieuThongKe();
        }

        private void InitGrids()
        {
            // Grid Chi tiết hóa đơn
            dgvHoaDonChiTiet.AutoGenerateColumns = false;
            dgvHoaDonChiTiet.Columns.Clear();
            dgvHoaDonChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaHD",
                HeaderText = "Mã HD",
                Width = 70
            });
            dgvHoaDonChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NgayThanhToan",
                HeaderText = "Ngày TT",
                Width = 130,
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
            });
            dgvHoaDonChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenPhong",
                HeaderText = "Phòng",
                Width = 90
            });
            dgvHoaDonChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoGioHat",
                HeaderText = "Giờ hát",
                Width = 75,
                DefaultCellStyle = { Format = "0.##" }
            });
            dgvHoaDonChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TienPhong",
                HeaderText = "Tiền phòng",
                Width = 100,
                DefaultCellStyle = { Format = "N0" }
            });
            dgvHoaDonChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TienDichVu",
                HeaderText = "Tiền dịch vụ",
                Width = 100,
                DefaultCellStyle = { Format = "N0" }
            });
            dgvHoaDonChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GiamGia",
                HeaderText = "Giảm giá",
                Width = 90,
                DefaultCellStyle = { Format = "N0" }
            });
            dgvHoaDonChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ThanhTien",
                HeaderText = "Thành tiền",
                Width = 120,
                DefaultCellStyle = { Format = "N0", Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) }
            });
            dgvHoaDonChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PhuongThucTT",
                HeaderText = "Hình thức",
                Width = 100
            });
            dgvHoaDonChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NhanVienLap",
                HeaderText = "Thu ngân",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            FormatGridStyle(dgvHoaDonChiTiet);

            // Grid Top Dịch Vụ
            dgvTopDichVu.AutoGenerateColumns = false;
            dgvTopDichVu.Columns.Clear();
            dgvTopDichVu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaDV",
                HeaderText = "Mã DV",
                Width = 80
            });
            dgvTopDichVu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenDV",
                HeaderText = "Tên dịch vụ / món",
                Width = 200
            });
            dgvTopDichVu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DonViTinh",
                HeaderText = "ĐVT",
                Width = 70
            });
            dgvTopDichVu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TongSoLuong",
                HeaderText = "Tổng SL bán",
                Width = 110,
                DefaultCellStyle = { Format = "N0" }
            });
            dgvTopDichVu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TongDoanhThu",
                HeaderText = "Tổng doanh thu",
                Width = 150,
                DefaultCellStyle = { Format = "N0", Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) }
            });
            dgvTopDichVu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TiLeDoanhThu",
                HeaderText = "Tỷ lệ (%)",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = { Format = "0.# '%'" }
            });
            FormatGridStyle(dgvTopDichVu);

            // Grid Doanh Thu Theo Phòng
            dgvTheoPhong.AutoGenerateColumns = false;
            dgvTheoPhong.Columns.Clear();
            dgvTheoPhong.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaPhong",
                HeaderText = "Mã phòng",
                Width = 100
            });
            dgvTheoPhong.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenPhong",
                HeaderText = "Tên phòng",
                Width = 130
            });
            dgvTheoPhong.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "LoaiPhong",
                HeaderText = "Loại phòng",
                Width = 120
            });
            dgvTheoPhong.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoLuotSuDung",
                HeaderText = "Lượt sử dụng",
                Width = 110,
                DefaultCellStyle = { Format = "N0" }
            });
            dgvTheoPhong.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TongTienPhong",
                HeaderText = "Tiền phòng",
                Width = 130,
                DefaultCellStyle = { Format = "N0" }
            });
            dgvTheoPhong.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TongTienDichVu",
                HeaderText = "Tiền dịch vụ",
                Width = 130,
                DefaultCellStyle = { Format = "N0" }
            });
            dgvTheoPhong.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TongDoanhThu",
                HeaderText = "Tổng doanh thu",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = { Format = "N0", Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) }
            });
            FormatGridStyle(dgvTheoPhong);

            // Grid Doanh Thu Theo Nhân Viên
            dgvNhanVien.AutoGenerateColumns = false;
            dgvNhanVien.Columns.Clear();
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaNV",
                HeaderText = "Mã NV",
                Width = 100
            });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenNV",
                HeaderText = "Họ và tên",
                Width = 200
            });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ChucVu",
                HeaderText = "Chức vụ",
                Width = 140
            });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoHoaDon",
                HeaderText = "Số HĐ đã lập",
                Width = 120,
                DefaultCellStyle = { Format = "N0" }
            });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TongDoanhThu",
                HeaderText = "Tổng doanh thu",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = { Format = "N0", Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) }
            });
            FormatGridStyle(dgvNhanVien);
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

        private void TaiDuLieuThongKe()
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;

            if (tuNgay > denNgay)
            {
                MessageBox.Show("Khoảng ngày không hợp lệ. 'Từ ngày' phải nhỏ hơn hoặc bằng 'Đến ngày'.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. Nạp KPI Overview
                var kpi = statisticService.GetKPIOverview(tuNgay, denNgay);
                lblTongDoanhThuVal.Text = kpi.TongDoanhThu.ToString("N0") + " đ";
                lblTongTienPhongVal.Text = kpi.TongTienPhong.ToString("N0") + " đ";
                lblTongTienDVVal.Text = kpi.TongTienDichVu.ToString("N0") + " đ";
                lblTongSoHDVal.Text = kpi.TongSoHoaDon.ToString("N0");

                // 2. Nạp Biểu đồ doanh thu
                TaiBieuDoDoanhThu(tuNgay, denNgay);

                // 3. Nạp Danh sách hóa đơn chi tiết
                dgvHoaDonChiTiet.DataSource = statisticService.GetInvoiceReportList(tuNgay, denNgay);

                // 4. Nạp Top Dịch Vụ
                dgvTopDichVu.DataSource = statisticService.GetTopServices(tuNgay, denNgay, 20);

                // 5. Nạp Doanh thu theo phòng
                dgvTheoPhong.DataSource = statisticService.GetRevenueByRoom(tuNgay, denNgay);

                // 6. Nạp Doanh thu theo nhân viên
                dgvNhanVien.DataSource = statisticService.GetRevenueByStaff(tuNgay, denNgay);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu thống kê: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TaiBieuDoDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            var chartData = statisticService.GetRevenueChartData(tuNgay, denNgay);

            chartDoanhThu.Series.Clear();
            chartDoanhThu.Legends.Clear();

            // Series Doanh Thu Tổng
            var seriesTong = new Series("Tổng Doanh Thu")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(16, 185, 129),
                IsValueShownAsLabel = false
            };

            // Series Tiền Phòng
            var seriesPhong = new Series("Tiền Giờ Hát")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(59, 130, 246),
                IsValueShownAsLabel = false
            };

            // Series Dịch Vụ
            var seriesDV = new Series("Tiền Dịch Vụ")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(245, 158, 11),
                IsValueShownAsLabel = false
            };

            foreach (var pt in chartData)
            {
                // Chuyển sang triệu đồng để đồ thị dễ nhìn
                double tongTrieu = (double)(pt.TongTien / 1000000m);
                double phongTrieu = (double)(pt.TienPhong / 1000000m);
                double dvTrieu = (double)(pt.TienDichVu / 1000000m);

                seriesTong.Points.AddXY(pt.ThoiGianNhan, tongTrieu);
                seriesPhong.Points.AddXY(pt.ThoiGianNhan, phongTrieu);
                seriesDV.Points.AddXY(pt.ThoiGianNhan, dvTrieu);
            }

            var legend = new Legend("MainLegend")
            {
                Docking = Docking.Top,
                Alignment = StringAlignment.Center,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular)
            };
            chartDoanhThu.Legends.Add(legend);

            chartDoanhThu.Series.Add(seriesTong);
            chartDoanhThu.Series.Add(seriesPhong);
            chartDoanhThu.Series.Add(seriesDV);

            if (chartDoanhThu.ChartAreas.Count > 0)
            {
                var area = chartDoanhThu.ChartAreas[0];
                area.AxisX.Title = "Thời gian";
                area.AxisX.TitleFont = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                area.AxisX.MajorGrid.LineColor = Color.FromArgb(241, 245, 249);
                area.AxisX.Interval = 1;

                area.AxisY.Title = "Doanh thu (triệu đồng)";
                area.AxisY.TitleFont = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                area.AxisY.MajorGrid.LineColor = Color.FromArgb(226, 232, 240);
                area.AxisY.LabelStyle.Format = "0.##";
            }

            chartDoanhThu.Invalidate();
        }

        private void btnXemThongKe_Click(object sender, EventArgs e)
        {
            TaiDuLieuThongKe();
        }

        private void btnHomNay_Click(object sender, EventArgs e)
        {
            dtpTuNgay.Value = DateTime.Today;
            dtpDenNgay.Value = DateTime.Today;
            TaiDuLieuThongKe();
        }

        private void btn7Ngay_Click(object sender, EventArgs e)
        {
            dtpTuNgay.Value = DateTime.Today.AddDays(-6);
            dtpDenNgay.Value = DateTime.Today;
            TaiDuLieuThongKe();
        }

        private void btnThangNay_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Today;
            dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
            dtpDenNgay.Value = now;
            TaiDuLieuThongKe();
        }

        private void btnNamNay_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Today;
            dtpTuNgay.Value = new DateTime(now.Year, 1, 1);
            dtpDenNgay.Value = now;
            TaiDuLieuThongKe();
        }

        private void btnXuatBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "CSV UTF-8 (*.csv)|*.csv|All files (*.*)|*.*";
                    sfd.FileName = string.Format("BaoCaoDoanhThu_{0:yyyyMMdd}_{1:yyyyMMdd}.csv", dtpTuNgay.Value, dtpDenNgay.Value);

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        var sb = new StringBuilder();
                        // Ghi BOM UTF-8 để Excel hiển thị đúng tiếng Việt có dấu
                        sb.Append('\uFEFF');
                        sb.AppendLine(string.Format("BÁO CÁO DOANH THU KARAOKE TỪ {0:dd/MM/yyyy} ĐẾN {1:dd/MM/yyyy}", dtpTuNgay.Value, dtpDenNgay.Value));
                        sb.AppendLine(string.Format("Thời gian xuất: {0:dd/MM/yyyy HH:mm:ss}", DateTime.Now));
                        sb.AppendLine();
                        sb.AppendLine("Mã HD,Ngày thanh toán,Phòng,Số giờ hát,Tiền phòng,Tiền dịch vụ,Giảm giá,Thành tiền,Hình thức TT,Thu ngân");

                        var list = dgvHoaDonChiTiet.DataSource as List<InvoiceReportItemDto>;
                        if (list != null)
                        {
                            foreach (var item in list)
                            {
                                sb.AppendLine(string.Format("\"{0}\",\"{1:dd/MM/yyyy HH:mm}\",\"{2}\",\"{3:F2}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\"",
                                    item.MaHD,
                                    item.NgayThanhToan,
                                    item.TenPhong,
                                    item.SoGioHat,
                                    item.TienPhong,
                                    item.TienDichVu,
                                    item.GiamGia,
                                    item.ThanhTien,
                                    item.PhuongThucTT,
                                    item.NhanVienLap));
                            }
                        }

                        File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                        MessageBox.Show("Xuất file báo cáo thành công!\n" + sfd.FileName, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
