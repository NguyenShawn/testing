using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using karaoke.Models;

namespace karaoke.Services
{
    public sealed class StaffService
    {
        public List<string> ListPositions()
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                // Staff managers may read position names without managing group permissions.
                AuthService.RequirePersonnelPermission(db, "NHANVIEN");
                using (var roles = RoleService.Open(db))
                    return roles.Roles.AsNoTracking().OrderBy(r => r.TenNhom).Select(r => r.TenNhom).ToList();
            }
        }

        public List<NHANVIEN> List()
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                var actor = AuthService.RequirePersonnelPermission(db, "NHANVIEN");
                var staff = db.NHANVIENs.AsNoTracking().OrderBy(x => x.MaNV).ToList();
                if (AuthService.IsAdmin(actor)) return staff;
                var groups = RoleService.GetAccountGroups(db);
                var protectedStaff = db.TAIKHOANs.AsNoTracking().ToList()
                    .Where(a => a.VaiTro == "Admin" || (groups.ContainsKey(a.MaTK) &&
                        string.Equals(groups[a.MaTK].TenNhom, "Manager", StringComparison.OrdinalIgnoreCase)))
                    .Select(a => a.MaNV).ToHashSet();
                return staff.Where(s => !protectedStaff.Contains(s.MaNV)).ToList();
            }
        }

        public void Save(string id, string name, string phone, string position, bool working, bool create)
        {
            id = (id ?? "").Trim(); name = (name ?? "").Trim();
            phone = (phone ?? "").Trim(); position = (position ?? "").Trim();
            if (!Regex.IsMatch(id, @"\A[a-zA-Z0-9_-]{1,20}\z")) throw new ArgumentException("Mã nhân viên chỉ gồm chữ, số, gạch ngang hoặc gạch dưới; tối đa 20 ký tự.");
            if (name.Length == 0 || name.Length > 100) throw new ArgumentException("Họ tên phải có từ 1 đến 100 ký tự.");
            if (position.Length == 0) throw new ArgumentException("Vui lòng chọn chức vụ trong danh sách nhóm quyền.");
            if (position.Length > 50) throw new ArgumentException("Chức vụ không được vượt quá 50 ký tự.");
            if (phone.Length != 0 && !Regex.IsMatch(phone, @"\A[0-9+]{8,15}\z")) throw new ArgumentException("Số điện thoại phải có 8–15 chữ số hoặc dấu +.");
            using (var db = new QuanLyKaraokeEntities())
            using (var tx = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                var admin = AuthService.RequirePersonnelPermission(db, "NHANVIEN");
                if (!working && admin.MaNV == id) throw new InvalidOperationException("Không thể cho chính mình nghỉ việc khi đang đăng nhập.");
                var staff = db.NHANVIENs.SingleOrDefault(x => x.MaNV == id);
                if (create && staff != null) throw new InvalidOperationException("Mã nhân viên đã tồn tại.");
                if (!create && staff == null) throw new InvalidOperationException("Nhân viên không còn tồn tại.");
                if (!create && !AuthService.IsAdmin(admin))
                {
                    var existingAccount = db.TAIKHOANs.SingleOrDefault(t => t.MaNV == id);
                    if (existingAccount != null && existingAccount.VaiTro == "Admin") throw new UnauthorizedAccessException("Không thể thay đổi hồ sơ Admin.");
                    if (existingAccount != null) RoleService.EnsureCanManageAccount(db, admin, existingAccount.MaTK);
                }
                using (var roles = RoleService.Open(db))
                {
                    var positions = roles.Roles.AsNoTracking().Select(r => r.TenNhom).ToList();
                    position = positions.FirstOrDefault(p => string.Equals(p, position, StringComparison.OrdinalIgnoreCase));
                    if (position == null) throw new ArgumentException("Chức vụ không còn hợp lệ. Vui lòng làm mới và chọn chức vụ trong danh sách nhóm quyền.");
                }
                // A position is a profile label; account permissions are assigned separately.
                if (create) { staff = new NHANVIEN { MaNV = id }; db.NHANVIENs.Add(staff); }
                string newStatus = working ? "Đang làm việc" : "Đã nghỉ việc";
                if (!create && staff.TrangThai != newStatus)
                {
                    foreach (var account in db.TAIKHOANs.Where(t => t.MaNV == id)) account.PhienBanBaoMat++;
                }
                staff.HoTen = name; staff.SDT = phone.Length == 0 ? null : phone; staff.ChucVu = position;
                staff.TrangThai = newStatus;
                db.SaveChanges(); tx.Commit();
            }
        }
    }
}
