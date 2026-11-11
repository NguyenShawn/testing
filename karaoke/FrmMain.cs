
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
            using (var iconStream = typeof(FrmMain).Assembly.GetManifestResourceStream("karaoke.Images.Karaoke404.ico"))
            {
                if (iconStream != null)
                {
                    using (var icon = new Icon(iconStream)) Icon = (Icon)icon.Clone();
                }
            }
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            KeyPreview = true;
            KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Escape && FormBorderStyle == FormBorderStyle.None)
                {
                    FormBorderStyle = FormBorderStyle.Sizable;
                    WindowState = FormWindowState.Normal;
                    e.Handled = true;
                }
            };
            this.Load += FrmMain_Load;
            this.btnThanhToan.Click += btnThanhToan_Click;
            this.btnThongKe.Click += btnThongKe_Click;
        }

        private void changePassword_Click(object sender, EventArgs e)
        { if (OpenChildForm(new FrmDoiMatKhau())) SetActiveSidebar(changePassword); }
        private void accountManagement_Click(object sender, EventArgs e)
        { OpenAdminPage("TAIKHOAN", "Tài khoản", () => new FrmQuanLyTaiKhoan(), accountManagement); }
        private void staffManagement_Click(object sender, EventArgs e)
        { OpenAdminPage("NHANVIEN", "Nhân viên", () => new FrmQuanLyNhanVien(), staffManagement); }
        private void roomTypes_Click(object sender, EventArgs e)
        { OpenAdminPage("LOAIPHONG", "Loại phòng", () => new FrmQuanLyLoaiPhong(), roomTypes); }
        private void roleManagement_Click(object sender, EventArgs e)
        { OpenAdminPage("NHOMQUYEN", "Nhóm quyền", () => new FrmQuanLyNhomQuyen(), roleManagement); }
        private void OpenAdminPage(string permission, string title, Func<Form> create, Guna.UI2.WinForms.Guna2Button button)
        {
            try
            {
                using (var db = new QuanLyKaraokeEntities()) AuthService.RequirePermission(db, permission);
                if (OpenChildForm(create())) SetActiveSidebar(button);
            }
            catch (Exception ex) { MessageBox.Show(ex.GetBaseException().Message, title); }
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
            accountManagement.Visible = AuthService.HasPermission("TAIKHOAN");
            staffManagement.Visible = AuthService.HasPermission("NHANVIEN");
            roomTypes.Visible = AuthService.HasPermission("LOAIPHONG");
            roleManagement.Visible = AuthService.HasPermission("NHOMQUYEN");
            btnPhong.Enabled = AuthService.HasPermission("PHONG_XEM");
            btnDichVu.Enabled = AuthService.HasPermission("DICHVU_XEM");
            btnDatPhong.Enabled = AuthService.HasPermission("DATPHONG");
            btnGoiDichVu.Enabled = AuthService.HasPermission("GOIDICHVU");
            btnThanhToan.Enabled = AuthService.HasPermission("THANHTOAN");
            btnThongKe.Enabled = AuthService.HasPermission("THONGKE");
            chart1.Visible = lbl4.Visible = btnThongKe.Enabled;
            if (!btnThongKe.Enabled) { lblDoanhThu.Text = ""; chart1.Series.Clear(); }
        }
        private bool OpenInternal(Func<Form> factory, bool adminOnly = false)
        {
            try {
                using (var db = new QuanLyKaraokeEntities())
                { if (adminOnly) AuthService.RequireAdmin(db); else AuthService.RequireCurrentUser(db); }
                return OpenChildForm(factory());
            }
            catch (Exception ex) { MessageBox.Show(ex.GetBaseException().Message, "Tài khoản nội bộ"); return false; }
        }
        private void LoadDashboard()
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                var account = AuthService.RequireCurrentUser(db);
                lblTongPhong.Text = lblDangSuDung.Text = lblPhongTrong.Text = "—";
                lblDoanhThu.Text = "";
                if (AuthService.HasPermission("PHONG_XEM"))
                {
                    lblTongPhong.Text = db.PHONGs.Count().ToString();
                    lblDangSuDung.Text = db.PHONGs.Count(x => x.TrangThai == "Đang sử dụng").ToString();
                    lblPhongTrong.Text = db.PHONGs.Count(x => x.TrangThai == "Trống").ToString();
                }
                if (AuthService.HasPermission("THONGKE"))
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
            if (!AuthService.HasPermission("THONGKE")) return;
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

        private bool OpenChildForm(Form childForm)
        {
            var orderForm = currentChildForm as FrmGoiDichVu;
            if (orderForm != null && !orderForm.ConfirmLeave()) { childForm.Dispose(); return false; }
            if (currentChildForm != null)
            {
                var previousForm = currentChildForm;
                currentChildForm = null;
                previousForm.Close();
                previousForm.Dispose();
            }

            panelDashboard.Visible = false;
            panelTrangChuHeader.Visible = false;

            currentChildForm = childForm;
            childForm.FormClosed += (sender, e) =>
            {
                if (currentChildForm == childForm)
                {
                    currentChildForm = null;
                    btnTrangChu_Click(this, EventArgs.Empty);
                }
            };

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panelContent.Controls.Add(childForm);

            childForm.BringToFront();
            childForm.Show();
            return true;
        }

        // ============================================
        // 5. BUTTON QUAN LY PHONG
        // ============================================

        private void btnPhong_Click(object sender, EventArgs e)
        {
            if (OpenInternal(() => new FrmQuanLyPhong())) SetActiveSidebar(btnPhong);
        }

        // ============================================
        // 6. BUTTON TRANG CHU
        // ============================================

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            var orderForm = currentChildForm as FrmGoiDichVu;
            if (orderForm != null && !orderForm.ConfirmLeave()) return;
            SetActiveSidebar(btnTrangChu);
            if (currentChildForm != null)
            {
                var previousForm = currentChildForm;
                currentChildForm = null;
                previousForm.Close();
                previousForm.Dispose();
                currentChildForm = null;
            }

            panelTrangChuHeader.Visible = true;
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
            if (OpenInternal(() => new FrmQuanLyDichVu())) SetActiveSidebar(btnDichVu);
        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            if (OpenInternal(() => new FrmDanhSachDatPhong())) SetActiveSidebar(btnDatPhong);
        }

        private void btnGoiDichVu_Click(object sender, EventArgs e)
        {
            if (OpenInternal(() => new FrmGoiDichVu())) SetActiveSidebar(btnGoiDichVu);
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (OpenChildForm(new FrmThanhToan())) SetActiveSidebar(btnThanhToan);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (OpenChildForm(new FrmThongKe())) SetActiveSidebar(btnThongKe);
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
                btnThongKe,
                changePassword,
                accountManagement,
                staffManagement,
                roomTypes,
                roleManagement
            );
        }
    }
}
