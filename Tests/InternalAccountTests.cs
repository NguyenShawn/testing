using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using karaoke.Helpers;
using karaoke.Models;
using karaoke.Services;

class InternalAccountTests
{
    static int count;
    static void Check(bool ok, string label) { if (!ok) throw new Exception(label); count++; Console.WriteLine("PASS: " + label); }
    static void Denied(Action action, string label)
    { try { action(); } catch (UnauthorizedAccessException) { Check(true, label); return; } throw new Exception("Access was not denied: " + label); }
    static void Invalid(Action action, string label)
    { try { action(); } catch (ArgumentException) { Check(true, label); return; } throw new Exception("Input was not rejected: " + label); }
    static int Main()
    {
        try
        {
            Check(AuthService.IsInternalAccount(new TAIKHOAN { MaNV = "N1", VaiTro = "Admin", TrangThai = true }), "Admin account supported");
            Check(AuthService.IsInternalAccount(new TAIKHOAN { MaNV = "N1", VaiTro = "NhanVien", TrangThai = true }), "employee account supported");
            foreach (var role in new[] { "QuanLy", "LeTan", "ThuNgan", "PhucVu", "KhachHang", "unknown" })
                Check(!AuthService.IsInternalAccount(new TAIKHOAN { MaNV = "N1", VaiTro = role, TrangThai = true }), "invalid legacy role rejected: " + role);
            Check(!AuthService.IsInternalAccount(new TAIKHOAN { VaiTro = "NhanVien", TrangThai = true }), "employee link required");
            Check(!AuthService.IsInternalAccount(new TAIKHOAN { MaNV = "N1", VaiTro = "Admin", TrangThai = false }), "disabled Admin rejected");
            UserSession.Clear();
            Denied(() => new BookingService().ListBookings(), "anonymous booking read denied");
            Denied(() => new ServiceOrderService().GetSessionOrders(1), "anonymous service history denied");
            Denied(() => new RoomService().Delete("unused"), "anonymous room deletion denied");
            Denied(() => new AccountService().List(), "anonymous account listing denied");
            Denied(() => new AccountService().CreateStaffAccount("N1", "user", "Password123"), "anonymous account creation denied");
            Denied(() => new AccountService().ResetStaffPassword(1, "Password123", "Password456"), "anonymous password reset denied");
            Denied(() => new AccountService().ChangeOwnPassword("Password123", "Password456"), "anonymous password change denied");
            Invalid(() => new AccountService().ChangeOwnPassword("Password123", "short"), "short password rejected");
            Invalid(() => new AccountService().ChangeOwnPassword("Password123", new string('\u754c', 25)), "BCrypt byte limit enforced");
            Invalid(() => new StaffService().Save("N1", "Employee", "abc", "Staff", true, true), "invalid employee phone rejected");
            Check(!new AuthService().DangNhap("bad username", "Password123"), "invalid username rejected offline");
            UserSession.PhienBanBaoMat = 9; UserSession.Clear(); Check(!UserSession.IsLoggedIn && UserSession.PhienBanBaoMat == 0, "logout clears session");
            using (var db = new QuanLyKaraokeEntities())
            {
                var metadata = ((IObjectContextAdapter)db).ObjectContext.MetadataWorkspace;
                metadata.GetItemCollection(DataSpace.CSSpace); metadata.LoadFromAssembly(typeof(TAIKHOAN).Assembly);
                Check(!db.TAIKHOANs.ToString().Contains("MaKH"), "account mapping has no customer dependency");
                Check(db.TAIKHOANs.Where(x => x.PhienBanBaoMat == 0).ToString().Contains("PhienBanBaoMat"), "security version mapped");
            }
            Console.WriteLine("PASS: " + count + " offline checks."); return 0;
        }
        catch (Exception ex) { Console.Error.WriteLine(ex); return 1; }
        finally { UserSession.Clear(); }
    }
}
