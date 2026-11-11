using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using karaoke.Helpers;
using karaoke.Models;
using BCryptNet = BCrypt.Net.BCrypt;

namespace karaoke.Services
{
    public sealed class AccountRow
    {
        public int MaTK { get; set; }
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public string TenDangNhap { get; set; }
        public string VaiTro { get; set; }
        public bool TrangThai { get; set; }
        public string NhomQuyen { get; set; }
        public int? MaNhom { get; set; }
    }

    public sealed class AccountService
    {
        private static void ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8 || System.Text.Encoding.UTF8.GetByteCount(password) > 72)
                throw new ArgumentException("Mật khẩu phải có ít nhất 8 ký tự và tối đa 72 byte UTF-8.");
        }

        public List<AccountRow> List()
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequirePermission(db, "TAIKHOAN");
                var accounts = (from t in db.TAIKHOANs join n in db.NHANVIENs on t.MaNV equals n.MaNV
                        orderby t.TenDangNhap
                        select new AccountRow { MaTK = t.MaTK, MaNV = t.MaNV, HoTen = n.HoTen, TenDangNhap = t.TenDangNhap, VaiTro = t.VaiTro, TrangThai = t.TrangThai }).ToList();
                var assigned = RoleService.GetAccountGroups(db);
                foreach (var account in accounts)
                {
                    RoleRecord group;
                    if (assigned.TryGetValue(account.MaTK, out group)) { account.MaNhom = group.MaNhom; account.NhomQuyen = group.TenNhom; }
                    else account.NhomQuyen = account.VaiTro == "Admin" ? "Admin (hệ thống)" : "Chưa gán";
                }
                return accounts;
            }
        }

        public List<NHANVIEN> AvailableStaff()
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequirePermission(db, "TAIKHOAN");
                return db.NHANVIENs.AsNoTracking().Where(n => n.TrangThai == "Đang làm việc" && !db.TAIKHOANs.Any(t => t.MaNV == n.MaNV)).OrderBy(n => n.HoTen).ToList();
            }
        }

        public int CreateStaffAccount(string employeeId, string username, string password)
        {
            username = (username ?? "").Trim();
            if (!Regex.IsMatch(username, @"\A[a-zA-Z0-9_.]{3,50}\z")) throw new ArgumentException("Tên đăng nhập phải gồm 3–50 chữ, số, dấu chấm hoặc gạch dưới.");
            ValidatePassword(password);
            using (var db = new QuanLyKaraokeEntities())
            using (var tx = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                AuthService.RequirePermission(db, "TAIKHOAN");
                if (!db.NHANVIENs.Any(n => n.MaNV == employeeId && n.TrangThai == "Đang làm việc")) throw new InvalidOperationException("Nhân viên không tồn tại hoặc đã nghỉ việc.");
                if (db.TAIKHOANs.Any(t => t.MaNV == employeeId || t.TenDangNhap == username)) throw new InvalidOperationException("Nhân viên hoặc tên đăng nhập đã có tài khoản.");
                var account = new TAIKHOAN { MaNV = employeeId, TenDangNhap = username, MatKhauHash = BCryptNet.HashPassword(password), VaiTro = "NhanVien", TrangThai = true };
                db.TAIKHOANs.Add(account); db.SaveChanges();
                RoleService.AssignDefaultStaffRole(db, account.MaTK);
                tx.Commit(); return account.MaTK;
            }
        }

        public void SetStaffStatus(int accountId, bool enabled)
        {
            using (var db = new QuanLyKaraokeEntities())
            using (var tx = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                var actor = AuthService.RequirePermission(db, "TAIKHOAN");
                var account = db.TAIKHOANs.SingleOrDefault(t => t.MaTK == accountId);
                if (account == null || account.VaiTro != "NhanVien") throw new InvalidOperationException("Chỉ quản lý trạng thái tài khoản nhân viên.");
                RoleService.EnsureCanManageAccount(db, actor, accountId);
                if (account.TrangThai != enabled) { account.TrangThai = enabled; account.PhienBanBaoMat++; }
                db.SaveChanges(); tx.Commit();
            }
        }

        public void DeleteStaffAccount(int accountId)
        {
            using (var db = new QuanLyKaraokeEntities())
            using (var tx = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                var actor = AuthService.RequirePermission(db, "TAIKHOAN");
                var account = db.TAIKHOANs.SingleOrDefault(t => t.MaTK == accountId);
                if (account == null || account.VaiTro != "NhanVien") throw new InvalidOperationException("Chỉ xóa được tài khoản nhân viên.");
                if (actor.MaTK == accountId) throw new InvalidOperationException("Không thể xóa tài khoản đang đăng nhập.");
                RoleService.EnsureCanManageAccount(db, actor, accountId);
                db.Database.ExecuteSqlCommand("DELETE FROM dbo.TAIKHOAN_NHOMQUYEN WHERE MaTK = @p0", accountId);
                db.TAIKHOANs.Remove(account);
                db.SaveChanges();
                tx.Commit();
            }
        }

        public void ResetStaffPassword(int accountId, string administratorPassword, string newPassword)
        {
            ValidatePassword(newPassword);
            using (var db = new QuanLyKaraokeEntities())
            using (var tx = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                var admin = AuthService.RequirePermission(db, "TAIKHOAN");
                bool verified;
                try { verified = !string.IsNullOrEmpty(administratorPassword) && BCryptNet.Verify(administratorPassword, admin.MatKhauHash); }
                catch (BCrypt.Net.SaltParseException) { verified = false; }
                if (!verified) throw new UnauthorizedAccessException("Mật khẩu Admin không đúng.");
                var account = db.TAIKHOANs.SingleOrDefault(t => t.MaTK == accountId);
                if (account == null || account.VaiTro != "NhanVien") throw new InvalidOperationException("Chỉ đặt lại mật khẩu tài khoản nhân viên.");
                RoleService.EnsureCanManageAccount(db, admin, accountId);
                account.MatKhauHash = BCryptNet.HashPassword(newPassword); account.PhienBanBaoMat++;
                account.SoLanDangNhapSai = 0; account.KhoaDen = null;
                db.SaveChanges(); tx.Commit();
            }
        }

        public void ChangeOwnPassword(string currentPassword, string newPassword)
        {
            ValidatePassword(newPassword);
            using (var db = new QuanLyKaraokeEntities())
            using (var tx = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                var profile = AuthService.RequireCurrentUser(db);
                var account = db.TAIKHOANs.Single(x => x.MaTK == profile.MaTK);
                bool valid;
                try { valid = !string.IsNullOrEmpty(currentPassword) && BCryptNet.Verify(currentPassword, account.MatKhauHash); }
                catch (BCrypt.Net.SaltParseException) { valid = false; }
                if (!valid) throw new UnauthorizedAccessException("Mật khẩu hiện tại không đúng.");
                account.MatKhauHash = BCryptNet.HashPassword(newPassword);
                account.PhienBanBaoMat++;
                db.SaveChanges();
                tx.Commit();
                UserSession.PhienBanBaoMat = account.PhienBanBaoMat;
            }
        }
    }
}
