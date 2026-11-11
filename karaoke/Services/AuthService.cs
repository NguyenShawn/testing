using System;
using System.Data.SqlClient;
using System.Linq;
using karaoke.Models;
using karaoke.Helpers;
using BCryptNet = BCrypt.Net.BCrypt;

namespace karaoke.Services
{
    public sealed class LoginAttemptResult
    {
        public bool Success { get; internal set; }
        public Exception Error { get; internal set; }
    }

    public class AuthService
    {
        // Catch on the calling worker thread, before Task.Run completes, so the
        // UI can distinguish a connection failure from an invalid password.
        public LoginAttemptResult TryDangNhap(string tenDangNhap, string matKhau)
        {
            for (int attempt = 0; ; attempt++)
            {
                try { return new LoginAttemptResult { Success = DangNhap(tenDangNhap, matKhau) }; }
                catch (Exception ex)
                {
                    var sql = ex.GetBaseException() as SqlException;
                    if (sql != null && sql.Number == 1205 && attempt < 2) continue;
                    UserSession.Clear();
                    return new LoginAttemptResult { Error = ex };
                }
            }
        }

        private static readonly string DummyHash = BCryptNet.HashPassword("invalid-account-password");
        public bool DangNhap(string tenDangNhap, string matKhau)
        {
            UserSession.Clear();
            tenDangNhap = (tenDangNhap ?? "").Trim();
            if (!System.Text.RegularExpressions.Regex.IsMatch(tenDangNhap, @"\A[a-zA-Z0-9_.]{3,50}\z"))
                return false;
            if (string.IsNullOrEmpty(matKhau) || System.Text.Encoding.UTF8.GetByteCount(matKhau) > 72) return false;
            using (var db = new QuanLyKaraokeEntities())
            {
                var connection = db.Database.Connection;
                var settings = new SqlConnectionStringBuilder(connection.ConnectionString);
                if (settings.DataSource.StartsWith("(localdb)\\", StringComparison.OrdinalIgnoreCase))
                { settings.ConnectRetryCount = 0; connection.ConnectionString = settings.ConnectionString; }
                EnsureInternalSchema(db);
                using (var tx = db.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    var account = db.TAIKHOANs.SingleOrDefault(t => t.TenDangNhap == tenDangNhap);
                    bool correct;
                    try { correct = BCryptNet.Verify(matKhau, account == null ? DummyHash : account.MatKhauHash); }
                    catch (BCrypt.Net.SaltParseException) { correct = false; }
                    DateTime now = db.Database.SqlQuery<DateTime>("SELECT SYSUTCDATETIME()").Single();
                    if (account == null || !account.TrangThai || (account.KhoaDen.HasValue && account.KhoaDen > now))
                    { tx.Commit(); return false; }
                    if (!correct)
                    {
                        // Five consecutive failures lock login for 15 minutes.
                        if (account.KhoaDen.HasValue) { account.SoLanDangNhapSai = 0; account.KhoaDen = null; }
                        account.SoLanDangNhapSai++;
                        if (account.SoLanDangNhapSai >= 5) account.KhoaDen = now.AddMinutes(15);
                        db.SaveChanges(); tx.Commit(); return false;
                    }
                    try { ValidateProfile(db, account); }
                    catch (UnauthorizedAccessException) { tx.Commit(); return false; }
                    account.SoLanDangNhapSai = 0; account.KhoaDen = null;
                    db.SaveChanges(); tx.Commit();
                    UserSession.MaTK = account.MaTK;
                    UserSession.MaNV = account.MaNV; UserSession.TenDangNhap = account.TenDangNhap;
                    UserSession.VaiTro = account.VaiTro; UserSession.PhienBanBaoMat = account.PhienBanBaoMat;
                    return true;
                }
            }
        }

        public static bool IsInternalAccount(TAIKHOAN account)
        {
            return account != null && account.TrangThai && !string.IsNullOrWhiteSpace(account.MaNV)
                && (account.VaiTro == "Admin" || account.VaiTro == "NhanVien");
        }
        public static bool IsAdmin(TAIKHOAN account)
        {
            return IsInternalAccount(account) && account.VaiTro == "Admin";
        }
        public static void ValidateProfile(QuanLyKaraokeEntities db, TAIKHOAN account)
        {
            if (!IsInternalAccount(account)) throw new UnauthorizedAccessException("Chỉ Admin và Nhân viên được đăng nhập.");
            var staff = db.NHANVIENs.AsNoTracking().SingleOrDefault(x => x.MaNV == account.MaNV);
            if (staff == null || staff.TrangThai != "Đang làm việc")
                throw new UnauthorizedAccessException("Nhân viên không còn làm việc.");
        }
        public static TAIKHOAN RequireCurrentUser(QuanLyKaraokeEntities db)
        {
            if (!UserSession.IsLoggedIn) throw new UnauthorizedAccessException("Vui lòng đăng nhập.");
            var account = db.TAIKHOANs.AsNoTracking().SingleOrDefault(x => x.MaTK == UserSession.MaTK);
            ValidateProfile(db, account);
            if (account.PhienBanBaoMat != UserSession.PhienBanBaoMat)
            { UserSession.Clear(); throw new UnauthorizedAccessException("Mật khẩu đã thay đổi. Vui lòng đăng nhập lại."); }
            return account;
        }
        public static TAIKHOAN RequireCurrentUser()
        { using (var db = new QuanLyKaraokeEntities()) return RequireCurrentUser(db); }
        public static TAIKHOAN RequirePermission(QuanLyKaraokeEntities db, string permission)
        {
            var account = RequireCurrentUser(db);
            if (!IsAdmin(account) && !new RoleService().HasPermission(db, account, permission))
                throw new UnauthorizedAccessException("Tài khoản không có quyền thực hiện chức năng này.");
            return account;
        }
        public static bool CanManageStaff(QuanLyKaraokeEntities db, TAIKHOAN account)
        {
            return IsAdmin(account) || new RoleService().IsManager(db, account);
        }
        public static TAIKHOAN RequirePersonnelPermission(QuanLyKaraokeEntities db, string permission)
        {
            var account = RequirePermission(db, permission);
            if (!CanManageStaff(db, account))
                throw new UnauthorizedAccessException("Chỉ Admin hoặc Manager được quản lý nhân viên và tài khoản.");
            return account;
        }
        public static bool HasPermission(string permission)
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                var account = RequireCurrentUser(db);
                return IsAdmin(account) || new RoleService().HasPermission(db, account, permission);
            }
        }
        public static TAIKHOAN RequireAdmin(QuanLyKaraokeEntities db)
        {
            var account = RequireCurrentUser(db);
            if (!IsAdmin(account)) throw new UnauthorizedAccessException("Chỉ Admin được thực hiện thao tác này.");
            return account;
        }
        public static void EnsureInternalSchema(QuanLyKaraokeEntities db)
        {
            int ready = db.Database.SqlQuery<int>(@"SELECT CASE WHEN COL_LENGTH('dbo.TAIKHOAN','PhienBanBaoMat') IS NOT NULL
 AND COL_LENGTH('dbo.TAIKHOAN','SoLanDangNhapSai') IS NOT NULL AND COL_LENGTH('dbo.TAIKHOAN','KhoaDen') IS NOT NULL
 AND COL_LENGTH('dbo.DICHVU','TonKho') IS NOT NULL
 AND OBJECT_ID('dbo.NHOMQUYEN','U') IS NOT NULL AND OBJECT_ID('dbo.QUYEN','U') IS NOT NULL
 AND OBJECT_ID('dbo.NHOMQUYEN_CHITIET','U') IS NOT NULL AND OBJECT_ID('dbo.TAIKHOAN_NHOMQUYEN','U') IS NOT NULL THEN 1 ELSE 0 END").Single();
            if (ready != 1) throw new InvalidOperationException("Cơ sở dữ liệu chưa được nâng cấp. Chạy Karaoke404_Setup.sql, sau đó thử lại.");
        }
    }
}
