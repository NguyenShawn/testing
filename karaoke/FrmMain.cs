
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using karaoke.Models;
using karaoke.Services;
using karaoke.UI;
namespace karaoke
{
    public partial class FrmMain : Form
    {
        private Form currentChildForm = null;

        public FrmMain()
        {
            InitializeComponent();
            this.Load += FrmMain_Load;
        }

        // ============================================
        // 1. LOAD FORM MAIN
        // ============================================

        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                ApplyAccountMode();
                LoadDashboard();
                LoadChartDoanhThu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải Dashboard: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ============================================
        // 2. LOAD THONG KE DASHBOARD BANG EF + LINQ
        // ============================================

        private void ApplyAccountMode()
        {
            bool admin = AuthService.IsAdmin(AuthService.RequireCurrentUser());
            btnPhong.Enabled = btnDichVu.Enabled = btnDatPhong.Enabled = btnGoiDichVu.Enabled = true;
            btnThanhToan.Enabled = true;
            btnThongKe.Enabled = admin;
            chart1.Visible = lbl4.Visible = admin;
            if (!admin) { lblDoanhThu.Text = ""; chart1.Series.Clear(); }
        }
        private void OpenInternal(Func<Form> factory, bool adminOnly = false)
        {
            try {
                using (var db = new QuanLyKaraokeEntities())
                { if (adminOnly) AuthService.RequireAdmin(db); else AuthService.RequireCurrentUser(db); }
                OpenChildForm(factory());
            }
            catch (Exception ex) { MessageBox.Show(ex.GetBaseException().Message, "Tài khoản nội bộ"); }
        }
        private void LoadDashboard()
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                var account = AuthService.RequireCurrentUser(db);
                lblTongPhong.Text = lblDangSuDung.Text = lblPhongTrong.Text = "—";
                lblDoanhThu.Text = "";
                if (AuthService.IsInternalAccount(account))
                {
                    lblTongPhong.Text = db.PHONGs.Count().ToString();
                    lblDangSuDung.Text = db.PHONGs.Count(x => x.TrangThai == "Đang sử dụng").ToString();
                    lblPhongTrong.Text = db.PHONGs.Count(x => x.TrangThai == "Trống").ToString();
                }
                if (AuthService.IsAdmin(account))
                {
                    DateTime start = DateTime.Today, end = start.AddDays(1);
                    decimal revenue = db.HOADONs.Where(x => x.NgayThanhToan >= start && x.NgayThanhToan < end).Select(x => (decimal?)x.ThanhTien).Sum() ?? 0m;
                    lblDoanhThu.Text = revenue.ToString("N0") + " đ";
                }
            }
        }

        // ============================================
        // 3. LOAD CHART DOANH THU BANG EF + LINQ
        // ============================================

        private void LoadChartDoanhThu()
        {
            chart1.Series.Clear();
            if (!AuthService.IsAdmin(AuthService.RequireCurrentUser())) return;
            int nam = DateTime.Today.Year;

            DateTime tuNgay = new DateTime(nam, 1, 1);
            DateTime denNgay = tuNgay.AddYears(1);

            decimal[] doanhThuThang = new decimal[13];

            using (var db = new QuanLyKaraokeEntities())
            {
                var duLieu = db.HOADONs
                    .Where(h =>
                        h.NgayThanhToan >= tuNgay &&
                        h.NgayThanhToan < denNgay
                    )
                    .GroupBy(h => h.NgayThanhToan.Month)
                    .Select(g => new
                    {
                        Thang = g.Key,
                        DoanhThu = g.Sum(h => h.ThanhTien)
                    })
                    .ToList();

                foreach (var item in duLieu)
                {
                    doanhThuThang[item.Thang] = item.DoanhThu;
                }
            }

            // Xoa du lieu Chart cu
            chart1.Series.Clear();

            Series series = new Series("DoanhThu");

            series.ChartType = SeriesChartType.Column;
            series.IsVisibleInLegend = false;

            for (int thang = 1; thang <= 12; thang++)
            {
                // Chuyen VND sang trieu dong
                double giaTri =
                    (double)(doanhThuThang[thang] / 1000000m);

                series.Points.AddXY(thang, giaTri);
            }

            chart1.Series.Add(series);
            chart1.Series["DoanhThu"].Color = Color.FromArgb(16, 185, 129);
            ChartArea area = chart1.ChartAreas[0];

            // Cau hinh truc X
            area.AxisX.Minimum = 1;
            area.AxisX.Maximum = 12;
            area.AxisX.Interval = 1;
            area.AxisX.LabelStyle.Format = "0";
            area.AxisX.Title = "Tháng";
            area.AxisX.MajorGrid.Enabled = false;

            // Cau hinh truc Y tu dong
            area.AxisY.Minimum = 0;
            area.AxisY.Maximum = double.NaN;
            area.AxisY.Interval = 0;
            area.AxisY.LabelStyle.Format = "0.##";
            area.AxisY.Title = "Doanh thu (triệu đồng)";
            area.AxisY.MajorGrid.LineColor = Color.Gainsboro;

            chart1.Invalidate();
        }

        // ============================================
        // 4. MO FORM CON TRONG PANEL CONTENT
        // ============================================

        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
                currentChildForm.Dispose();
            }

            panelDashboard.Visible = false;

            currentChildForm = childForm;

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panelContent.Controls.Add(childForm);

            childForm.BringToFront();
            childForm.Show();
        }

        // ============================================
        // 5. BUTTON QUAN LY PHONG
        // ============================================

        private void btnPhong_Click(object sender, EventArgs e)
        {
            OpenInternal(() => new FrmQuanLyPhong());
            SetActiveSidebar(btnPhong);
        }

        // ============================================
        // 6. BUTTON TRANG CHU
        // ============================================

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            SetActiveSidebar(btnTrangChu);
            if (currentChildForm != null)
            {
                currentChildForm.Close();
                currentChildForm.Dispose();
                currentChildForm = null;
            }

            panelDashboard.Visible = true;
            panelDashboard.BringToFront();
            
            try
            {
                ApplyAccountMode();
                LoadDashboard();
                LoadChartDoanhThu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi cập nhật Dashboard: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {
            OpenInternal(() => new FrmQuanLyDichVu());
            SetActiveSidebar(btnDichVu);
        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            OpenInternal(() => new FrmDanhSachDatPhong());
            SetActiveSidebar(btnDatPhong);
        }

        private void btnGoiDichVu_Click(object sender, EventArgs e)
        {
            OpenInternal(() => new FrmGoiDichVu());
            SetActiveSidebar(btnGoiDichVu);
        }

        private void SetActiveSidebar(Guna.UI2.WinForms.Guna2Button button)
        {
            ButtonActive.SetActiveButton(
                button,
                btnTrangChu,
                btnPhong,
                btnDichVu,
                btnDatPhong,
                btnGoiDichVu,
                btnThanhToan,
                btnThongKe
            );
        }

        
    }
}
