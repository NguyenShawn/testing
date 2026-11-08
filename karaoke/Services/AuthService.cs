
using System;
using System.Linq;
using karaoke.Models;
using karaoke.Helpers;
using BCryptNet = BCrypt.Net.BCrypt;

namespace karaoke.Services
{
    public class AuthService
    {
        public bool DangNhap(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) ||
                string.IsNullOrEmpty(matKhau))
            {
                return false;
            }

            using (var db = new QuanLyKaraokeEntities())
            {
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
    }
}
