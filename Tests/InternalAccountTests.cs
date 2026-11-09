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
    static void Sql(string query, params object[] args) { using (var db = new QuanLyKaraokeEntities()) db.Database.ExecuteSqlCommand(query, args); }
    static int Main(string[] args)
    {
        try { Offline(); if (args.Contains("--database")) Integration(); Console.WriteLine("PASS: " + count + " checks."); return 0; }
        catch (Exception ex) { Console.Error.WriteLine(ex); return 1; }
        finally { UserSession.Clear(); }
    }
    static void Offline()
    {
        Check(AuthService.IsInternalAccount(new TAIKHOAN { MaNV = "N1", VaiTro = "Admin", TrangThai = true }), "Admin account supported");
        Check(AuthService.IsInternalAccount(new TAIKHOAN { MaNV = "N1", VaiTro = "NhanVien", TrangThai = true }), "employee account supported");
        foreach (var role in new[] { "QuanLy", "LeTan", "ThuNgan", "PhucVu", "KhachHang", "unknown" })
            Check(!AuthService.IsInternalAccount(new TAIKHOAN { MaNV = "N1", VaiTro = role, TrangThai = true }), "retired role rejected: " + role);
        Check(!AuthService.IsInternalAccount(new TAIKHOAN { VaiTro = "NhanVien", TrangThai = true }), "account without employee cannot log in");
        Check(!AuthService.IsAdmin(new TAIKHOAN { MaNV = "N1", VaiTro = "NhanVien", TrangThai = true }), "employee cannot claim Admin");
        Check(!AuthService.IsInternalAccount(new TAIKHOAN { MaNV = "N1", VaiTro = "Admin", TrangThai = false }), "disabled Admin denied");
        UserSession.Clear();
        Denied(() => new BookingService().ListBookings(), "anonymous booking read denied");
        Denied(() => new ServiceOrderService().GetSessionOrders(1), "anonymous service history denied");
        Denied(() => new RoomService().Delete("unused"), "anonymous room deletion denied");
        Denied(() => new AccountService().List(), "anonymous employee list denied");
        Denied(() => new AccountService().CreateStaff("user", "Password123", "Name", "0901234567"), "no public employee registration");
        Denied(() => new AccountService().ResetPassword(1, "Password123", "Password456"), "anonymous password reset denied");
        Denied(() => new AccountService().ChangeOwnPassword("Password123", "Password456"), "anonymous password change denied");
        Invalid(() => AuthenticationPolicy.Password("short"), "short password rejected");
        Invalid(() => AuthenticationPolicy.Password(new string('\u754c', 25)), "BCrypt byte limit checked for Unicode");
        Invalid(() => AuthenticationPolicy.StaffProfile("Employee", "abc"), "employee phone validated");
        Check(!new AuthService().DangNhap("bad username", "Password123"), "invalid login input rejected without SQL");
        UserSession.PhienBanBaoMat = 9; UserSession.Clear(); Check(!UserSession.IsLoggedIn && UserSession.PhienBanBaoMat == 0, "logout clears session");
        using (var db = new QuanLyKaraokeEntities())
        {
            var metadata = ((IObjectContextAdapter)db).ObjectContext.MetadataWorkspace;
            metadata.GetItemCollection(DataSpace.CSSpace); metadata.LoadFromAssembly(typeof(TAIKHOAN).Assembly);
            Check(!db.TAIKHOANs.ToString().Contains("MaKH"), "account mapping has no customer dependency");
            Check(!db.DATPHONGs.ToString().Contains("MaKH"), "booking mapping has no customer account dependency");
            Check(db.TAIKHOANs.Where(x => x.PhienBanBaoMat == 0).ToString().Contains("PhienBanBaoMat"), "security-version mapping remains valid");
        }
        var assembly = typeof(AuthService).Assembly;
        Check(assembly.GetType("karaoke.FrmRegister") == null && assembly.GetType("karaoke.FrmForgotPassword") == null, "public registration and email recovery removed");
        Check(assembly.GetType("karaoke.Services.AuthorizationService") == null && assembly.GetType("karaoke.FrmPermissions") == null, "configurable permission subsystem removed");
        Check(assembly.GetType("karaoke.Services.SmtpVerificationEmailSender") == null, "SMTP no longer required");
    }
    static void Integration()
    {
        int adminId; string staffName = "employee";
        using (var db = new QuanLyKaraokeEntities())
        {
            if (!db.Database.Connection.Database.StartsWith("KaraokeInternalTest_")) throw new Exception("Use an isolated test database.");
            AuthService.EnsureInternalSchema(db);
            db.NHANVIENs.Add(new NHANVIEN { MaNV = "TESTADMIN", HoTen = "Admin", ChucVu = "Admin", TrangThai = "Đang làm việc" });
            var admin = new TAIKHOAN { MaNV = "TESTADMIN", TenDangNhap = "testadmin", MatKhauHash = BCrypt.Net.BCrypt.HashPassword("AdminPass123"), VaiTro = "Admin", TrangThai = true };
            db.TAIKHOANs.Add(admin);
            db.LOAIPHONGs.Add(new LOAIPHONG { MaLoaiPhong = 1, TenLoaiPhong = "Test", DonGiaGio = 100000 });
            db.PHONGs.Add(new PHONG { MaPhong = "TEST", TenPhong = "Room", MaLoaiPhong = 1, SucChua = 10, TrangThai = "Trống" });
            db.DICHVUs.Add(new DICHVU { MaDV = "WATER", TenDV = "Water", DonViTinh = "Bottle", DonGia = 10000, TrangThai = true });
            db.SaveChanges(); adminId = admin.MaTK;
        }
        var auth = new AuthService(); var accounts = new AccountService(); var bookings = new BookingService();
        Check(auth.DangNhap("testadmin", "AdminPass123"), "Admin login without email or permission tables");
        int staff = accounts.CreateStaff(staffName, "StaffPass123", "Employee", "0901234567");
        Check(accounts.List().Any(x => x.MaTK == staff), "Admin creates employee account");
        Check(auth.DangNhap(staffName, "StaffPass123"), "employee login");
        Denied(() => accounts.CreateStaff("illegal", "StaffPass123", "Employee", "0901234567"), "employee cannot create account");
        Denied(() => accounts.List(), "employee cannot read account administration");
        Denied(() => new RoomService().Delete("TEST"), "employee cannot delete room catalog");
        int booking = bookings.CreateBooking("TEST", "Walk-in guest", "0901234567", 2, DateTime.Now.AddMinutes(5), DateTime.Now.AddHours(1), null);
        string creator;
        using (var db = new QuanLyKaraokeEntities()) creator = db.DATPHONGs.Single(x => x.MaDatPhong == booking).MaNV;
        Check(!string.IsNullOrEmpty(creator), "booking records staff without customer login");
        Check(auth.DangNhap("testadmin", "AdminPass123"), "Admin switches to edit booking");
        bookings.EditBooking(booking, "TEST", "Guest updated", "0901234567", 2, DateTime.Now.AddMinutes(5), DateTime.Now.AddHours(1), "updated");
        using (var db = new QuanLyKaraokeEntities()) Check(db.DATPHONGs.Single(x => x.MaDatPhong == booking).MaNV == creator, "editing keeps original booking creator");
        Check(auth.DangNhap(staffName, "StaffPass123"), "employee resumes operations");
        int session = bookings.CheckInBooking(booking);
        new ServiceOrderService().OrderServices(session, new System.Collections.Generic.List<CartItemInput> { new CartItemInput { MaDV = "WATER", SoLuong = 2, DonGia = 1 } });
        var invoice = new PaymentService().Checkout(session, 0, "Tiền mặt");
        Check(invoice.TienDichVu == 20000 && invoice.ThanhTien == invoice.TienPhong + invoice.TienDichVu, "employee checks in, orders and checks out with server prices");
        using (var db = new QuanLyKaraokeEntities()) Check(db.PHONGs.Single(x => x.MaPhong == "TEST").TrangThai == "Trống", "checkout releases room");
        Check(auth.DangNhap("testadmin", "AdminPass123"), "Admin login to reset employee password");
        Denied(() => accounts.ResetPassword(staff, "wrong", "Changed123"), "reset requires Admin password confirmation");
        Denied(() => accounts.ResetPassword(adminId, "AdminPass123", "Changed123"), "employee administration cannot reset Admin");
        accounts.ResetPassword(staff, "AdminPass123", "Changed123");
        UserSession.MaTK = staff; UserSession.PhienBanBaoMat = 0;
        Denied(() => AuthService.RequireCurrentUser(), "reset revokes previous employee session");
        Check(!auth.DangNhap(staffName, "StaffPass123") && auth.DangNhap(staffName, "Changed123"), "only replacement password logs in");
        accounts.ChangeOwnPassword("Changed123", "OwnPassword123");
        Check(AuthService.RequireCurrentUser().MaTK == staff, "own password change keeps current session valid");
        Check(!auth.DangNhap(staffName, "Changed123") && auth.DangNhap(staffName, "OwnPassword123"), "own password change stored as BCrypt");
        Check(auth.DangNhap("testadmin", "AdminPass123"), "Admin locks employee"); accounts.SetActive(staff, false);
        Check(!auth.DangNhap(staffName, "OwnPassword123"), "disabled employee cannot sign in");
        Check(auth.DangNhap("testadmin", "AdminPass123"), "Admin unlocks employee"); accounts.SetActive(staff, true);
        Check(auth.DangNhap(staffName, "OwnPassword123"), "unlocked employee signs in");
        for (int i = 0; i < 5; i++) Check(!auth.DangNhap(staffName, "incorrect"), "invalid password rejected");
        Check(!auth.DangNhap(staffName, "OwnPassword123"), "temporary lock persists after five failures");
        Check(auth.DangNhap("testadmin", "AdminPass123"), "Admin resets temporarily locked account"); accounts.ResetPassword(staff, "AdminPass123", "Recovered123");
        Check(auth.DangNhap(staffName, "Recovered123"), "Admin reset clears temporary login lock");
        Sql("UPDATE dbo.NHANVIEN SET TrangThai=N'Nghỉ việc' WHERE MaNV={0}", creator);
        Check(!auth.DangNhap(staffName, "Recovered123"), "inactive employee rejected");
        using (var db = new QuanLyKaraokeEntities())
        {
            Check(db.Database.SqlQuery<int>("SELECT COUNT(*) FROM sys.tables WHERE schema_id=SCHEMA_ID('dbo') AND name IN ('VAITRO','QUYEN','VAITRO_QUYEN','MAXACMINH','KHACHHANG')").Single() == 0, "no active permission or customer authentication tables");
            Check(db.Database.SqlQuery<int>("SELECT CASE WHEN OBJECT_ID('dbo.NHATKYTAIKHOAN','U') IS NULL THEN 1 ELSE 0 END").Single() == 1, "account audit table no longer required");
            Check(db.Database.SqlQuery<int>("SELECT COUNT(*) FROM sys.tables WHERE schema_id=SCHEMA_ID('legacy')").Single() == 0, "obsolete archive tables removed");
            Check(db.Database.SqlQuery<int>("SELECT COUNT(*) FROM sys.columns WHERE (object_id=OBJECT_ID('dbo.TAIKHOAN') AND name='MaKH') OR (object_id=OBJECT_ID('dbo.DATPHONG') AND name IN ('MaKH','MaNguoiTaoTK','MaNguoiSuaTK'))").Single() == 0, "unused customer account columns removed");
        }
    }
}
