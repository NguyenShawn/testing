using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using karaoke.Helpers;
using karaoke.Models;
using BCryptNet = BCrypt.Net.BCrypt;

namespace karaoke.Services
{
    public sealed class OwnAccountProfile
    {
        public string HoTen { get; set; }
        public string MaNV { get; set; }
        public string TenDangNhap { get; set; }
        public string NhomQuyen { get; set; }
        public List<string> Quyen { get; set; }
    }

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
        public OwnAccountProfile GetOwnProfile()
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                var account = AuthService.RequireCurrentUser(db);
                var staff = db.NHANVIENs.AsNoTracking().Single(x => x.MaNV == account.MaNV);
                var profile = new OwnAccountProfile
                {
                    HoTen = staff.HoTen,
                    MaNV = staff.MaNV,
                    TenDangNhap = account.TenDangNhap
                };
                RoleService.PopulateOwnRole(db, account, profile);
                return profile;
            }
        }

        public void UpdateOwnName(string name)
        {
            name = (name ?? "").Trim();
            if (name.Length == 0 || name.Length > 100 || name.Any(char.IsControl))
                throw new ArgumentException("Họ tên phải có từ 1 đến 100 ký tự và không chứa ký tự điều khiển.");
            using (var db = new QuanLyKaraokeEntities())
            using (var tx = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                var account = AuthService.RequireCurrentUser(db);
                var staff = db.NHANVIENs.Single(x => x.MaNV == account.MaNV);
                staff.HoTen = name;
                db.SaveChanges();
                tx.Commit();
            }
        }

        private static void ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8 || System.Text.Encoding.UTF8.GetByteCount(password) > 72)
                throw new ArgumentException("Mật khẩu phải có ít nhất 8 ký tự và tối đa 72 byte UTF-8.");
        }

        public List<AccountRow> List()
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                var actor = AuthService.RequirePersonnelPermission(db, "TAIKHOAN");
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
                return AuthService.IsAdmin(actor) ? accounts : accounts.Where(a => a.VaiTro != "Admin" &&
                    !string.Equals(a.NhomQuyen, "Manager", StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }

        public List<NHANVIEN> AvailableStaff()
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequirePersonnelPermission(db, "TAIKHOAN");
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
                AuthService.RequirePersonnelPermission(db, "TAIKHOAN");
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
                var actor = AuthService.RequirePersonnelPermission(db, "TAIKHOAN");
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
                try
                {
                    var actor = AuthService.RequirePersonnelPermission(db, "TAIKHOAN");
                    var account = db.TAIKHOANs.SingleOrDefault(t => t.MaTK == accountId);
                    if (account == null || account.VaiTro != "NhanVien") throw new InvalidOperationException("Chỉ xóa được tài khoản nhân viên.");
                    if (actor.MaTK == accountId) throw new InvalidOperationException("Không thể xóa tài khoản đang đăng nhập.");
                    RoleService.EnsureCanManageAccount(db, actor, accountId);

                    using (RoleContext roles = RoleService.Open(db))
                    {
                        var accountRoles = roles.AccountRoles.Where(ar => ar.MaTK == accountId).ToList();
                        roles.AccountRoles.RemoveRange(accountRoles);
                        roles.SaveChanges();
                    }

                    db.TAIKHOANs.Remove(account);
                    db.SaveChanges();
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        public void ResetStaffPassword(int accountId, string administratorPassword, string newPassword)
        {
            ValidatePassword(newPassword);
            using (var db = new QuanLyKaraokeEntities())
            using (var tx = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                var admin = AuthService.RequirePersonnelPermission(db, "TAIKHOAN");
                bool verified;
                try { verified = !string.IsNullOrEmpty(administratorPassword) && BCryptNet.Verify(administratorPassword, admin.MatKhauHash); }
                catch (BCrypt.Net.SaltParseException) { verified = false; }
                if (!verified) throw new UnauthorizedAccessException("Mật khẩu tài khoản hiện tại không đúng.");
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
