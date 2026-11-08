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
            try
            {
                return new LoginAttemptResult { Success = DangNhap(tenDangNhap, matKhau) };
            }
            catch (Exception ex)
            {
                UserSession.Clear();
                return new LoginAttemptResult { Error = ex };
            }
        }

        public bool DangNhap(string tenDangNhap, string matKhau)
        {
            UserSession.Clear();
            if (string.IsNullOrWhiteSpace(tenDangNhap) ||
                string.IsNullOrEmpty(matKhau))
            {
                return false;
            }

            using (var db = new QuanLyKaraokeEntities())
            {
                // LocalDB is on this machine: do not spend another 10 seconds
                // retrying a failed login connection. Keep remote configurations
                // and their retry policies unchanged.
                var connection = db.Database.Connection;
                var settings = new SqlConnectionStringBuilder(connection.ConnectionString);
                if (settings.DataSource.StartsWith("(localdb)\\", StringComparison.OrdinalIgnoreCase))
                {
                    settings.ConnectRetryCount = 0;
                    connection.ConnectionString = settings.ConnectionString;
                }

                var taiKhoan = db.TAIKHOANs
                    .FirstOrDefault(t =>
                        t.TenDangNhap == tenDangNhap);

                // Khong tiet lo tai khoan co ton tai hay khong
                if (taiKhoan == null)
                    return false;

                // Chi cho phep tai khoan dang hoat dong
                if (!taiKhoan.TrangThai)
                    return false;

                if (string.IsNullOrEmpty(taiKhoan.MatKhauHash))
                    return false;

                bool matKhauDung;

                try
                {
                    matKhauDung = BCryptNet.Verify(
                        matKhau,
                        taiKhoan.MatKhauHash
                    );
                }
                catch (BCrypt.Net.SaltParseException)
                {
                    // Hash khong dung dinh dang BCrypt
                    return false;
                }

                if (!matKhauDung)
                    return false;

                // Dang nhap thanh cong
                UserSession.MaTK = taiKhoan.MaTK;
                UserSession.MaNV = taiKhoan.MaNV;
                UserSession.TenDangNhap =
                    taiKhoan.TenDangNhap;
                UserSession.VaiTro = taiKhoan.VaiTro;

                return true;
            }
        }

        public static TAIKHOAN ValidateCurrentUser(QuanLyKaraokeEntities db, params string[] allowedRoles)
        {
            if (!UserSession.IsLoggedIn)
            {
                throw new UnauthorizedAccessException("Vui lòng đăng nhập để thực hiện thao tác.");
            }

            var taiKhoan = db.TAIKHOANs
                .Include("NHANVIEN")
                .FirstOrDefault(t => t.MaTK == UserSession.MaTK);

            if (taiKhoan == null || !taiKhoan.TrangThai)
            {
                throw new UnauthorizedAccessException("Tài khoản không tồn tại hoặc đã bị vô hiệu hóa.");
            }

            if (taiKhoan.NHANVIEN == null || taiKhoan.NHANVIEN.TrangThai != "Đang làm việc")
            {
                throw new UnauthorizedAccessException("Nhân viên liên kết với tài khoản này không còn làm việc.");
            }

            if (allowedRoles != null && allowedRoles.Length > 0 && !allowedRoles.Contains(taiKhoan.VaiTro))
            {
                throw new UnauthorizedAccessException(
                    string.Format("Tài khoản không có quyền thực hiện. Vai trò yêu cầu: {0}.", string.Join(", ", allowedRoles))
                );
            }

            return taiKhoan;
        }
    }
}
