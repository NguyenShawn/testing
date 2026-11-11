using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using karaoke;
using karaoke.Helpers;
using karaoke.Models;
using karaoke.Services;

class AccountProfileTests
{
    static void Check(bool ok, string label) { if (!ok) throw new Exception(label); Console.WriteLine("PASS: " + label); }
    static void Denied(Action action, string label) { try { action(); } catch (UnauthorizedAccessException) { Check(true, label); return; } throw new Exception("Not denied: " + label); }
    static T Field<T>(object o, string name) where T : class { return (T)o.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(o); }
    [STAThread] static int Main()
    {
        try
        {
            using (var db = new QuanLyKaraokeEntities())
                if (!db.Database.Connection.Database.StartsWith("KaraokeProfileTest_")) throw new Exception("Requires an isolated test database.");
            Application.EnableVisualStyles(); Application.SetCompatibleTextRenderingDefault(false);
            var auth = new AuthService(); var roles = new RoleService(); var accounts = new AccountService(); var staff = new StaffService();
            Check(auth.DangNhap("admin", "AdminKaraoke2026!"), "admin login");
            roles.EnsureManagerRole(); roles.EnsureManagerRole();
            var manager = roles.List().Single(r => r.TenNhom == "Manager");
            Check(manager.LaHeThong && roles.GetPermissions(manager.MaNhom).Contains("TAIKHOAN") && roles.GetPermissions(manager.MaNhom).Contains("NHANVIEN"), "manager exists with staff management rights");
            staff.Save("P_MGR1", "Manager Một", "", "Quản lý", true, true);
            staff.Save("P_MGR2", "Manager Hai", "", "Quản lý", true, true);
            staff.Save("P_STAFF", "Staff Một", "", "Nhân viên", true, true);
            staff.Save("P_VIP", "Staff báo cáo", "", "Nhân viên", true, true);
            int m1 = accounts.CreateStaffAccount("P_MGR1", "profile_m1", "Password123");
            int m2 = accounts.CreateStaffAccount("P_MGR2", "profile_m2", "Password123");
            int s1 = accounts.CreateStaffAccount("P_STAFF", "profile_staff", "Password123");
            int vip = accounts.CreateStaffAccount("P_VIP", "profile_vip", "Password123");
            int reportingRole = roles.Save(null, "Staff báo cáo", new[] { "THONGKE" });
            roles.AssignStaffRole(vip, reportingRole);
            roles.AssignStaffRole(m1, manager.MaNhom); roles.AssignStaffRole(m2, manager.MaNhom);
            roles.Save(manager.MaNhom, "Manager", new[] { "PHONG_XEM", "DICHVU_XEM", "DATPHONG", "GOIDICHVU", "THANHTOAN", "NHANVIEN", "TAIKHOAN", "NHOMQUYEN" });
            staff.Save("P_MGR2", "Manager Hai đã cập nhật", "", "Quản lý", true, false);
            Check(staff.List().Any(x => x.MaNV == "P_MGR2" && x.HoTen == "Manager Hai đã cập nhật"), "admin can edit manager record");
            var adminProfile = accounts.GetOwnProfile();
            Check(adminProfile.NhomQuyen == "Admin" && adminProfile.Quyen.Contains("Quản lý tài khoản"), "admin profile shows effective permissions");
            using (var roleForm = new FrmQuanLyNhomQuyen())
            {
                roleForm.Show(); Application.DoEvents();
                var groupList = Field<ListBox>(roleForm, "groups");
                groupList.SelectedItem = groupList.Items.Cast<RoleRecord>().Single(r => r.TenNhom == "Manager"); Application.DoEvents();
                Check(Field<TextBox>(roleForm, "name").ReadOnly && Field<CheckedListBox>(roleForm, "permissions").Enabled, "admin can configure manager permissions without renaming the role");
                roleForm.Close();
            }
            Check(auth.DangNhap("profile_m1", "Password123"), "manager login");
            var managerProfile = accounts.GetOwnProfile();
            Check(managerProfile.NhomQuyen == "Manager" && managerProfile.Quyen.Contains("Quản lý nhân viên") && managerProfile.Quyen.Contains("Quản lý tài khoản"), "manager profile shows assigned rights");
            using (var managerMain = new FrmMain())
            {
                managerMain.Show(); Application.DoEvents();
                Check(Field<Guna.UI2.WinForms.Guna2Button>(managerMain, "accountManagement").Text == "Quản lý tài khoản" && Field<Guna.UI2.WinForms.Guna2Button>(managerMain, "accountManagement").Visible, "manager sees the renamed account management entry");
                Field<Guna.UI2.WinForms.Guna2Button>(managerMain, "personalAccount").PerformClick(); Application.DoEvents();
                var managerPage = Field<Panel>(managerMain, "panelContent").Controls.OfType<FrmTaiKhoanCaNhan>().Single();
                Check(Field<Label>(managerPage, "lblRoleValue").Text == "Manager", "manager page shows the Manager title");
                managerMain.Close(); Application.DoEvents();
            }
            accounts.UpdateOwnName("Tên Manager mới");
            Check(accounts.GetOwnProfile().HoTen == "Tên Manager mới", "manager edits own name");
            bool invalid = false; try { accounts.UpdateOwnName("  "); } catch (ArgumentException) { invalid = true; }
            Check(invalid && accounts.GetOwnProfile().HoTen == "Tên Manager mới", "empty name rejected without changing data");
            accounts.SetStaffStatus(s1, false); accounts.SetStaffStatus(s1, true);
            accounts.SetStaffStatus(vip, false); accounts.SetStaffStatus(vip, true);
            Check(true, "manager can manage staff even when a staff permission differs");
            Denied(() => roles.AssignStaffRole(s1, reportingRole), "manager cannot grant permissions they do not hold");
            Check(accounts.List().Any(x => x.MaTK == s1) && !accounts.List().Any(x => x.MaTK == m2) && !staff.List().Any(x => x.MaNV == "P_MGR2"), "manager sees staff but not manager records");
            Check(!roles.ListForAssignment().Any(r => r.TenNhom == "Manager"), "manager cannot select manager role for assignment");
            staff.Save("P_STAFF", "Staff đã sửa", "", "Nhân viên", true, false);
            Check(staff.List().Single(x => x.MaNV == "P_STAFF").HoTen == "Staff đã sửa", "manager can manage staff");
            Denied(() => staff.Save(adminProfile.MaNV, "Đổi trái phép", "", "Admin", true, false), "manager cannot edit admin record");
            Denied(() => roles.Save(manager.MaNhom, "Manager", new[] { "NHANVIEN", "TAIKHOAN" }), "manager cannot edit own role even when granted role management");
            Denied(() => accounts.ResetStaffPassword(m2, "Password123", "AnotherPassword123"), "manager cannot reset another manager password");
            Denied(() => accounts.SetStaffStatus(m2, false), "manager cannot lock another manager");
            Denied(() => accounts.DeleteStaffAccount(m2), "manager cannot delete another manager");
            Denied(() => staff.Save("P_MGR2", "Đổi trái phép", "", "Quản lý", true, false), "manager cannot edit another manager's staff record");
            Denied(() => roles.AssignStaffRole(s1, manager.MaNhom), "manager cannot promote staff to manager");
            Denied(() => roles.AssignStaffRole(m2, roles.ListForAssignment().Single(r => r.TenNhom == "Nhân viên").MaNhom), "manager cannot demote another manager");
            Check(auth.DangNhap("profile_staff", "Password123"), "staff login");
            var staffProfile = accounts.GetOwnProfile();
            Check(staffProfile.NhomQuyen == "Nhân viên" && !staffProfile.Quyen.Contains("Quản lý tài khoản"), "staff profile shows staff role and rights");
            accounts.UpdateOwnName("Staff tự sửa");
            Check(accounts.GetOwnProfile().HoTen == "Staff tự sửa", "staff can edit own name");
            Denied(() => accounts.SetStaffStatus(m1, false), "staff cannot manage manager");
            Denied(() => staff.Save("P_MGR1", "Không hợp lệ", "", "Quản lý", true, false), "staff cannot edit manager");
            var main = new FrmMain();
            main.Show(); Application.DoEvents();
            var personal = Field<Guna.UI2.WinForms.Guna2Button>(main, "personalAccount");
            Check(personal.Visible, "staff sees personal account page");
            Check(!Field<Guna.UI2.WinForms.Guna2Button>(main, "btnThongKe").Visible, "staff statistics button is hidden");
            Check(Field<Panel>(main, "lbl4").Visible && Field<Label>(main, "label11").Text.Contains(DateTime.Today.ToString("dd/MM/yyyy")), "staff sees today's revenue dashboard");
            Check(!Field<Panel>(main, "panelSidebar").Controls.Cast<Control>().Any(c => c.Text == "Đổi mật khẩu"), "sidebar no longer has separate password page");
            Check(!Field<Guna.UI2.WinForms.Guna2Button>(main, "accountManagement").Visible, "staff does not see account management");
            personal.PerformClick(); Application.DoEvents();
            var panel = Field<Panel>(main, "panelContent");
            var page = panel.Controls.OfType<FrmTaiKhoanCaNhan>().Single();
            Check(Field<Label>(page, "lblRoleValue").Text == "Staff (Nhân viên)" && Field<ListBox>(page, "permissions").Items.Count > 0, "profile page displays role and permissions");
            Field<Button>(page, "btnEdit").PerformClick(); Field<TextBox>(page, "txtName").Text = "Staff qua giao diện"; Field<Button>(page, "btnSave").PerformClick(); Application.DoEvents();
            Check(accounts.GetOwnProfile().HoTen == "Staff qua giao diện", "profile save button updates name");
            var currentPassword = Field<TextBox>(page, "txtCurrentPassword");
            var newPassword = Field<TextBox>(page, "txtNewPassword");
            var confirmPassword = Field<TextBox>(page, "txtConfirmPassword");
            var changePassword = Field<Button>(page, "btnChangePassword");
            var passwordStatus = Field<Label>(page, "lblPasswordStatus");
            Check(currentPassword.UseSystemPasswordChar && newPassword.UseSystemPasswordChar && confirmPassword.UseSystemPasswordChar, "password fields are masked");
            int version = UserSession.PhienBanBaoMat;
            currentPassword.Text = "Password123"; newPassword.Text = "NewPassword456"; confirmPassword.Text = "DifferentPassword456";
            changePassword.PerformClick(); Application.DoEvents();
            Check(passwordStatus.Text.Contains("không khớp") && UserSession.PhienBanBaoMat == version, "mismatched confirmation is rejected");
            currentPassword.Text = "WrongPassword123"; confirmPassword.Text = "NewPassword456";
            changePassword.PerformClick(); Application.DoEvents();
            Check(passwordStatus.Text.Contains("không đúng") && UserSession.PhienBanBaoMat == version, "wrong current password is rejected");
            currentPassword.Text = "Password123";
            changePassword.PerformClick(); Application.DoEvents();
            Check(passwordStatus.Text.Contains("thành công") && UserSession.PhienBanBaoMat == version + 1 && currentPassword.Text == "" && newPassword.Text == "" && confirmPassword.Text == "", "profile changes password, clears fields and keeps session valid");
            Check(accounts.GetOwnProfile().HoTen == "Staff qua giao diện", "session remains valid after password change");
            currentPassword.Text = "x"; Check(passwordStatus.Text == "", "changing password fields clears old feedback"); currentPassword.Clear();
            var screenshot = Environment.GetEnvironmentVariable("KARAOKE_PROFILE_SCREENSHOT");
            if (!string.IsNullOrEmpty(screenshot)) using (var image = new Bitmap(main.Width, main.Height)) { main.DrawToBitmap(image, new Rectangle(0, 0, image.Width, image.Height)); image.Save(screenshot); }
            Field<Button>(page, "btnLogout").PerformClick(); Application.DoEvents();
            Check(main.DialogResult == DialogResult.Retry && main.IsDisposed, "logout closes main and returns to login flow");
            Check(!auth.DangNhap("profile_staff", "Password123") && auth.DangNhap("profile_staff", "NewPassword456"), "new password works and old password is rejected");
            Check(auth.DangNhap("admin", "AdminKaraoke2026!"), "admin login after manager and staff");
            accounts.SetStaffStatus(m2, false); accounts.SetStaffStatus(m2, true);
            roles.Save(manager.MaNhom, "Manager", new[] { "PHONG_XEM", "NHANVIEN", "TAIKHOAN" });
            roles.EnsureManagerRole();
            Check(roles.GetPermissions(manager.MaNhom).Count == 3, "admin changes persist across manager setup");
            int unusualStaffRole = roles.Save(null, "Staff quyền thử", new[] { "NHANVIEN", "TAIKHOAN" });
            roles.AssignStaffRole(s1, unusualStaffRole);
            Check(auth.DangNhap("profile_staff", "NewPassword456"), "staff login with delegated rights");
            Check(!accounts.GetOwnProfile().Quyen.Contains("Quản lý tài khoản"), "staff profile excludes manager-only actions");
            Denied(() => accounts.List(), "staff cannot use personnel management even if granted its permission");
            using (var db = new QuanLyKaraokeEntities()) { db.TAIKHOANs.Single(a => a.MaTK == s1).PhienBanBaoMat++; db.SaveChanges(); }
            Denied(() => accounts.UpdateOwnName("Không hợp lệ"), "revoked session cannot edit profile");
            UserSession.Clear(); Denied(() => accounts.GetOwnProfile(), "anonymous profile access denied");
            Console.WriteLine("PASS: all account profile and hierarchy checks"); return 0;
        }
        catch (Exception ex) { Console.Error.WriteLine(ex); return 1; }
        finally { UserSession.Clear(); }
    }
}
