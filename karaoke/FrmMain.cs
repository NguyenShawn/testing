
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using karaoke.Models;
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

        private void LoadDashboard()
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                // Tong so phong
                int tongPhong = db.PHONGs.Count();

                // So phong dang su dung
                int dangSuDung = db.PHONGs.Count(
                    p => p.TrangThai == "Đang sử dụng"
                );

                // So phong trong
                int phongTrong = db.PHONGs.Count(
                    p => p.TrangThai == "Trống"
                );

                lblTongPhong.Text = tongPhong.ToString();
                lblDangSuDung.Text = dangSuDung.ToString();
                lblPhongTrong.Text = phongTrong.ToString();

                // Doanh thu trong ngay hien tai
                DateTime tuNgay = DateTime.Today;
                DateTime denNgay = tuNgay.AddDays(1);

                decimal doanhThu = db.HOADONs
                    .Where(h =>
                        h.NgayThanhToan >= tuNgay &&
                        h.NgayThanhToan < denNgay
                    )
                    .Select(h => (decimal?)h.ThanhTien)
                    .Sum() ?? 0m;

                lblDoanhThu.Text =
                    doanhThu.ToString("N0") + "đ";
            }
        }

        // ============================================
        // 3. LOAD CHART DOANH THU BANG EF + LINQ
        // ============================================

        private void LoadChartDoanhThu()
        {
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
            OpenChildForm(new FrmQuanLyPhong());
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

        private void SetActiveSidebar(Guna.UI2.WinForms.Guna2Button button)
        {
            ButtonActive.SetActiveButton(
                button,
                btnTrangChu,
                btnPhong,
                btnDichVu,
                btnDatPhong,
                btnThanhToan,
                btnThongKe
            );
        }

        
    }
}
