using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using karaoke;
using karaoke.Helpers;
using karaoke.Models;
using karaoke.Services;
using Microsoft.Reporting.WinForms;

class AdminUiTests
{
    static T Field<T>(object target, string name) where T : class
    { return target.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(target) as T; }
    static void Check(bool value, string label) { if (!value) throw new Exception(label); Console.WriteLine("PASS: " + label); }
    static System.Collections.Generic.IEnumerable<Control> Descendants(Control root)
    { foreach (Control child in root.Controls) { yield return child; foreach (var nested in Descendants(child)) yield return nested; } }
    static void ShowAndCapture(Form form, string name, Action check)
    {
        using (form)
        {
            form.Show(); Application.DoEvents(); check();
            using (var bitmap = new Bitmap(form.Width, form.Height))
            {
                form.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                bitmap.Save(Path.Combine(Path.GetTempPath(), "karaoke-test-" + name + ".png"));
            }
            form.Close(); Application.DoEvents();
        }
    }
    [STAThread] static int Main()
    {
        try
        {
            using (var db = new QuanLyKaraokeEntities())
                if (!db.Database.Connection.Database.StartsWith("KaraokePasswordTest_")) throw new Exception("UI tests require isolated DB.");
            Application.EnableVisualStyles(); Application.SetCompatibleTextRenderingDefault(false);
            var loginForm = new FrmLogin();
            ShowAndCapture(loginForm, "login", () => {
                Check(Descendants(loginForm).Any(c => c.Text == "Đăng nhập"), "login form opens with sign-in control");
            });
            Check(new AuthService().DangNhap("admin", "AdminKaraoke2026!"), "admin login for UI tests");
            var main = new FrmMain();
            ShowAndCapture(main, "main", () => {
                Check(main.WindowState == FormWindowState.Maximized && main.FormBorderStyle == FormBorderStyle.None, "main form starts fullscreen");
                Check(Descendants(main).Any(c => c.Text == "Nhóm quyền" && c.Visible), "admin sees group management entry");
                foreach (var pair in new[] { new[] { "label3", "lblTongPhong" }, new[] { "label5", "lblDangSuDung" }, new[] { "label7", "lblPhongTrong" }, new[] { "label9", "lblDoanhThu" } })
                    Check(!Field<Label>(main, pair[0]).Bounds.IntersectsWith(Field<Label>(main, pair[1]).Bounds), "dashboard caption does not overlap value: " + pair[0]);
            });
            var bookings = new FrmDanhSachDatPhong();
            ShowAndCapture(bookings, "booking-search", () => {
                var grid = Field<DataGridView>(bookings, "dgvDanhSach");
                var actions = Field<Panel>(bookings, "panelActions");
                Check(grid.DataSource == null, "bookings are not loaded before search");
                Check(actions.Bottom <= grid.Top && Field<Button>(bookings, "btnThemDat").Visible, "booking actions are above the list");
                Field<ComboBox>(bookings, "cbTrangThai").SelectedIndex = 1; Application.DoEvents();
                Check(grid.DataSource == null, "changing filter does not search automatically");
                Field<Button>(bookings, "btnTimKiem").PerformClick(); Application.DoEvents();
                Check(grid.DataSource != null, "search displays bookings");
                Field<Button>(bookings, "btnLamMoi").PerformClick(); Application.DoEvents();
                Check(grid.DataSource == null, "reset clears search results");
            });
            var orders = new FrmGoiDichVu();
            ShowAndCapture(orders, "room-picker", () => {
                Check(Field<Panel>(orders, "panelChonPhong").Visible, "service order opens the room picker");
                Check(Field<FlowLayoutPanel>(orders, "danhSachPhong").Controls.Count > 0, "room picker displays database rooms");
            });
            var accounts = new FrmQuanLyTaiKhoan();
            ShowAndCapture(accounts, "accounts", () => {
                var grid = Field<DataGridView>(accounts, "grid");
                Check(grid.Rows.Count >= 1 && grid.Columns["NhomQuyen"].Visible, "account form displays group column");
                var staffRow = grid.Rows.Cast<DataGridViewRow>().First(r => ((AccountRow)r.DataBoundItem).TenDangNhap == "newstaff");
                var model = (AccountRow)staffRow.DataBoundItem;
                grid.CurrentCell = staffRow.Cells["TenDangNhap"]; Application.DoEvents();
                var selectedRole = Field<ComboBox>(accounts, "role").SelectedItem as RoleRecord;
                Check(model.NhomQuyen == "Tiếp tân thử" && selectedRole != null && selectedRole.MaNhom == model.MaNhom, "selection displays actual assigned group");
            });
            var staff = new FrmQuanLyNhanVien();
            ShowAndCapture(staff, "staff", () => Check(Field<DataGridView>(staff, "grid").Rows.Count >= 1, "employee form displays rows"));
            var types = new FrmQuanLyLoaiPhong();
            ShowAndCapture(types, "room-types", () => Check(Field<DataGridView>(types, "grid").Rows.Count >= 2, "room type form displays rows"));
            var roles = new FrmQuanLyNhomQuyen();
            ShowAndCapture(roles, "roles", () => {
                Check(Field<ListBox>(roles, "groups").Items.Count >= 2 && Field<CheckedListBox>(roles, "permissions").Items.Count >= 10, "role form loads groups and permissions");
                foreach (var size in new[] { new Size(820, 540), new Size(700, 480) })
                {
                    roles.ClientSize = size; roles.PerformLayout(); Application.DoEvents();
                    var buttons = Descendants(roles).OfType<Button>().ToList();
                    Check(buttons.Count == 3 && buttons.All(b => b.Parent.ClientRectangle.Contains(b.Bounds)), "role actions fit at " + size);
                }
                roles.ClientSize = new Size(820, 540); roles.PerformLayout();
            });
            var payments = new PaymentService();
            var flowInvoice = payments.GetInvoiceHistory(DateTime.Today, DateTime.Today).First(x => x.MaPhong == "FLOWROOM");
            var receipt = new FrmInHoaDon(payments.GetSavedInvoiceDetail(flowInvoice.MaHD), flowInvoice.MaHD);
            ShowAndCapture(receipt, "invoice", () => {
                var receiptText = Field<RichTextBox>(receipt, "rtbHoaDon");
                string content = receiptText.Text;
                Check(content.Contains("Nước thử") && content.Contains("Món thử") && content.Contains(string.Format("{0:N0}", flowInvoice.TienDichVu)), "invoice preview shows saved services and their total");
                using (var template = typeof(FrmBaoCaoRdlc).Assembly.GetManifestResourceStream("karaoke.Reports.ReportLines.rdlc"))
                using (var localReport = new LocalReport())
                {
                    localReport.LoadReportDefinition(template);
                    localReport.DataSources.Add(new ReportDataSource("Lines", receiptText.Lines.Select(line => new ReportLine { Text = line }).ToList()));
                    File.WriteAllBytes(Path.Combine(Path.GetTempPath(), "karaoke-real-invoice-test.pdf"), localReport.Render("PDF"));
                }
                Check(File.Exists(Path.Combine(Path.GetTempPath(), "karaoke-real-invoice-test.pdf")), "saved invoice renders with RDLC");
            });
            var report = new FrmBaoCaoRdlc("Báo cáo thử", new[] { "BÁO CÁO THỬ", "Dòng đầu", "Dòng cuối" });
            var viewer = Field<ReportViewer>(report, "viewer");
            bool rendered = false; Exception reportError = null;
            viewer.RenderingComplete += (s, e) => { rendered = true; reportError = e.Exception; };
            ShowAndCapture(report, "report", () => {
                var timer = System.Diagnostics.Stopwatch.StartNew();
                while (!rendered && timer.ElapsedMilliseconds < 15000) { Application.DoEvents(); System.Threading.Thread.Sleep(30); }
                if (reportError != null) throw new Exception("ReportViewer failed", reportError);
                Check(rendered, "RDLC viewer completed rendering");
            });
            return 0;
        }
        catch (Exception ex) { Console.Error.WriteLine(ex); return 1; }
        finally { UserSession.Clear(); }
    }
}
