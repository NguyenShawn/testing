using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using karaoke.Models;

namespace karaoke.Services
{
    [Table("NHOMQUYEN")]
    public class RoleRecord
    {
        [Key] public int MaNhom { get; set; }
        [Required, MaxLength(100)] public string TenNhom { get; set; }
        public bool LaHeThong { get; set; }
    }
    [Table("QUYEN")]
    public class PermissionRecord
    {
        [Key, MaxLength(50), Column(TypeName = "varchar")] public string MaQuyen { get; set; }
        [Required, MaxLength(100)] public string TenQuyen { get; set; }
    }
    [Table("NHOMQUYEN_CHITIET")]
    public class RolePermissionRecord
    {
        [Key, Column(Order = 0)] public int MaNhom { get; set; }
        [Key, Column(Order = 1, TypeName = "varchar"), MaxLength(50)] public string MaQuyen { get; set; }
    }
    [Table("TAIKHOAN_NHOMQUYEN")]
    public class AccountRoleRecord
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)] public int MaTK { get; set; }
        public int MaNhom { get; set; }
    }
    internal sealed class RoleContext : DbContext
    {
        static RoleContext() { Database.SetInitializer<RoleContext>(null); }
        public RoleContext(DbConnection connection) : base(connection, false) { }
        public RoleContext(DbConnection connection, bool contextOwnsConnection) : base(connection, contextOwnsConnection) { }
        public DbSet<RoleRecord> Roles { get; set; }
        public DbSet<PermissionRecord> Permissions { get; set; }
        public DbSet<RolePermissionRecord> RolePermissions { get; set; }
        public DbSet<AccountRoleRecord> AccountRoles { get; set; }
    }
    public sealed class RoleService
    {
        internal static void PopulateOwnRole(QuanLyKaraokeEntities db, TAIKHOAN account, OwnAccountProfile profile)
        {
            using (var roles = Open(db))
            {
                if (AuthService.IsAdmin(account))
                {
                    profile.NhomQuyen = "Admin";
                    profile.Quyen = roles.Permissions.OrderBy(p => p.TenQuyen).Select(p => p.TenQuyen).ToList();
                }
                else
                {
                    profile.NhomQuyen = (from ar in roles.AccountRoles
                                         join role in roles.Roles on ar.MaNhom equals role.MaNhom
                                         where ar.MaTK == account.MaTK select role.TenNhom).SingleOrDefault() ?? "Chưa gán nhóm";
                    bool canManageStaff = string.Equals(profile.NhomQuyen, "Manager", StringComparison.OrdinalIgnoreCase);
                    profile.Quyen = (from ar in roles.AccountRoles
                                     join rp in roles.RolePermissions on ar.MaNhom equals rp.MaNhom
                                     join p in roles.Permissions on rp.MaQuyen equals p.MaQuyen
                                     where ar.MaTK == account.MaTK &&
                                         (canManageStaff || (p.MaQuyen != "NHANVIEN" && p.MaQuyen != "TAIKHOAN"))
                                     orderby p.TenQuyen select p.TenQuyen).ToList();
                }
            }
        }

        // Create the default role once. Existing Manager permissions are never reset.
        public void EnsureManagerRole()
        {
            using (var db = new QuanLyKaraokeEntities())
            using (var tx = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                AuthService.RequireAdmin(db);
                using (var roles = Open(db))
                {
                    if (!roles.Roles.Any(r => r.TenNhom.ToUpper() == "MANAGER"))
                    {
                        var role = new RoleRecord { TenNhom = "Manager", LaHeThong = true };
                        roles.Roles.Add(role);
                        roles.SaveChanges();
                        var defaults = new[] { "PHONG_XEM", "DICHVU_XEM", "DATPHONG", "GOIDICHVU", "THANHTOAN", "NHANVIEN", "TAIKHOAN" };
                        foreach (var permission in roles.Permissions.Where(p => defaults.Contains(p.MaQuyen)).Select(p => p.MaQuyen).ToList())
                            roles.RolePermissions.Add(new RolePermissionRecord { MaNhom = role.MaNhom, MaQuyen = permission });
                        roles.SaveChanges();
                    }
                }
                tx.Commit();
            }
        }

        internal static RoleContext Open(QuanLyKaraokeEntities db)
        {
            var roles = new RoleContext(db.Database.Connection, false);
            if (db.Database.CurrentTransaction != null) roles.Database.UseTransaction(db.Database.CurrentTransaction.UnderlyingTransaction);
            return roles;
        }
        public bool IsManager(QuanLyKaraokeEntities db, TAIKHOAN account)
        {
            if (account == null) return false;
            using (var roles = Open(db))
                return (from ar in roles.AccountRoles join r in roles.Roles on ar.MaNhom equals r.MaNhom
                        where ar.MaTK == account.MaTK && r.TenNhom.ToUpper() == "MANAGER" select ar.MaTK).Any();
        }
        private static void EnsureGrantable(RoleContext roles, TAIKHOAN actor, IEnumerable<string> requested)
        {
            if (AuthService.IsAdmin(actor)) return;
            var owned = (from ar in roles.AccountRoles join rp in roles.RolePermissions on ar.MaNhom equals rp.MaNhom
                         where ar.MaTK == actor.MaTK select rp.MaQuyen).ToList();
            if (requested.Except(owned).Any()) throw new UnauthorizedAccessException("Không thể cấp hoặc sửa quyền vượt quá quyền của chính bạn.");
        }
        internal static void EnsureCanManageAccount(QuanLyKaraokeEntities db, TAIKHOAN actor, int accountId)
        {
            if (AuthService.IsAdmin(actor)) return;
            using (var roles = Open(db))
            {
                bool isManager = (from ar in roles.AccountRoles join r in roles.Roles on ar.MaNhom equals r.MaNhom
                                  where ar.MaTK == accountId && r.TenNhom.ToUpper() == "MANAGER" select ar.MaTK).Any();
                if (isManager) throw new UnauthorizedAccessException("Chỉ Admin được quản lý tài khoản Manager.");
            }
        }
        internal static Dictionary<int, RoleRecord> GetAccountGroups(QuanLyKaraokeEntities db)
        {
            using (var roles = Open(db))
                return (from a in roles.AccountRoles join r in roles.Roles on a.MaNhom equals r.MaNhom
                        select new { a.MaTK, Role = r }).ToList().ToDictionary(x => x.MaTK, x => x.Role);
        }
        internal static void AssignDefaultStaffRole(QuanLyKaraokeEntities db, int accountId)
        {
            using (var roles = Open(db))
            {
                var defaultRole = roles.Roles.SingleOrDefault(r => r.LaHeThong && r.TenNhom == "Nhân viên");
                if (defaultRole == null) throw new InvalidOperationException("Chưa có nhóm nhân viên. Hãy chạy script nâng cấp phân quyền.");
                var actor = AuthService.RequireCurrentUser(db);
                EnsureGrantable(roles, actor, roles.RolePermissions.Where(p => p.MaNhom == defaultRole.MaNhom).Select(p => p.MaQuyen).ToList());
                roles.AccountRoles.Add(new AccountRoleRecord { MaTK = accountId, MaNhom = defaultRole.MaNhom });
                roles.SaveChanges();
            }
        }
        public int? GetAccountRole(int accountId)
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequirePersonnelPermission(db, "TAIKHOAN");
                using (var roles = Open(db)) return roles.AccountRoles.Where(a => a.MaTK == accountId).Select(a => (int?)a.MaNhom).SingleOrDefault();
            }
        }
        public void AssignStaffRole(int accountId, int roleId)
        {
            using (var db = new QuanLyKaraokeEntities())
            using (var tx = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                var actor = AuthService.RequirePersonnelPermission(db, "TAIKHOAN");
                var account = db.TAIKHOANs.SingleOrDefault(a => a.MaTK == accountId);
                if (account == null || account.VaiTro != "NhanVien") throw new InvalidOperationException("Chỉ gán nhóm cho tài khoản nhân viên.");
                EnsureCanManageAccount(db, actor, accountId);
                using (var roles = Open(db))
                {
                    var role = roles.Roles.SingleOrDefault(r => r.MaNhom == roleId);
                    if (role == null || (role.LaHeThong && role.TenNhom == "Admin")) throw new InvalidOperationException("Nhóm không hợp lệ.");
                    if (!AuthService.IsAdmin(actor) && string.Equals(role.TenNhom, "Manager", StringComparison.OrdinalIgnoreCase))
                        throw new UnauthorizedAccessException("Chỉ Admin được gán vai trò Manager.");
                    EnsureGrantable(roles, actor, roles.RolePermissions.Where(p => p.MaNhom == roleId).Select(p => p.MaQuyen).ToList());
                    var assignment = roles.AccountRoles.SingleOrDefault(a => a.MaTK == accountId);
                    if (assignment == null) roles.AccountRoles.Add(new AccountRoleRecord { MaTK = accountId, MaNhom = roleId });
                    else assignment.MaNhom = roleId;
                    roles.SaveChanges();
                    account.PhienBanBaoMat++; db.SaveChanges(); tx.Commit();
                }
            }
        }
        public List<RoleRecord> List()
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequirePermission(db, "NHOMQUYEN");
                using (var roles = Open(db)) return roles.Roles.AsNoTracking().OrderBy(r => r.TenNhom).ToList();
            }
        }
        public List<RoleRecord> ListForAssignment()
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                var actor = AuthService.RequirePersonnelPermission(db, "TAIKHOAN");
                bool admin = AuthService.IsAdmin(actor);
                using (var roles = Open(db))
                    return roles.Roles.AsNoTracking().Where(r => (!r.LaHeThong || r.TenNhom != "Admin") && (admin || r.TenNhom.ToUpper() != "MANAGER"))
                        .OrderBy(r => r.TenNhom).ToList();
            }
        }
        public List<PermissionRecord> ListPermissions()
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequirePermission(db, "NHOMQUYEN");
                using (var roles = Open(db)) return roles.Permissions.AsNoTracking().OrderBy(p => p.TenQuyen).ToList();
            }
        }
        public List<string> GetPermissions(int roleId)
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequirePermission(db, "NHOMQUYEN");
                using (var roles = Open(db)) return roles.RolePermissions.Where(p => p.MaNhom == roleId).Select(p => p.MaQuyen).ToList();
            }
        }
        public bool HasPermission(QuanLyKaraokeEntities db, TAIKHOAN account, string permission)
        {
            if (account == null) return false;
            using (var roles = Open(db))
                return (from ar in roles.AccountRoles join rp in roles.RolePermissions on ar.MaNhom equals rp.MaNhom
                        where ar.MaTK == account.MaTK && rp.MaQuyen == permission select ar.MaTK).Any();
        }
        public int Save(int? roleId, string name, IEnumerable<string> permissions)
        {
            name = (name ?? "").Trim();
            if (name.Length == 0 || name.Length > 100) throw new ArgumentException("Tên nhóm quyền không hợp lệ.");
            var selected = (permissions ?? Enumerable.Empty<string>()).Distinct().ToList();
            if (selected.Contains("PHONG_SUA") && !selected.Contains("PHONG_XEM"))
                throw new ArgumentException("Quyền quản lý phòng yêu cầu thêm quyền xem phòng.");
            if (selected.Contains("DICHVU_SUA") && !selected.Contains("DICHVU_XEM"))
                throw new ArgumentException("Quyền quản lý dịch vụ yêu cầu thêm quyền xem dịch vụ.");
            using (var db = new QuanLyKaraokeEntities())
            using (var tx = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                var actor = AuthService.RequirePermission(db, "NHOMQUYEN");
                using (var roles = Open(db))
                {
                    var role = roleId.HasValue ? roles.Roles.SingleOrDefault(r => r.MaNhom == roleId.Value) : null;
                    if (roleId.HasValue && role == null) throw new InvalidOperationException("Nhóm quyền không tồn tại.");
                    bool isManager = role != null && string.Equals(role.TenNhom, "Manager", StringComparison.OrdinalIgnoreCase);
                    if (isManager && (!AuthService.IsAdmin(actor) || !string.Equals(name, role.TenNhom, StringComparison.Ordinal)))
                        throw new UnauthorizedAccessException("Chỉ Admin được sửa quyền Manager; tên Manager không thể đổi.");
                    if (string.Equals(name, "Manager", StringComparison.OrdinalIgnoreCase) && !AuthService.IsAdmin(actor))
                        throw new UnauthorizedAccessException("Chỉ Admin được tạo nhóm Manager.");
                    if (role != null && role.LaHeThong && !isManager) throw new InvalidOperationException("Không thể sửa nhóm hệ thống.");
                    if (role != null) EnsureGrantable(roles, actor, roles.RolePermissions.Where(p => p.MaNhom == role.MaNhom).Select(p => p.MaQuyen).ToList());
                    EnsureGrantable(roles, actor, selected);
                    if (roles.Roles.Any(r => r.TenNhom == name && (!roleId.HasValue || r.MaNhom != roleId.Value))) throw new InvalidOperationException("Tên nhóm quyền đã tồn tại.");
                    int validCount = roles.Permissions.Count(p => selected.Contains(p.MaQuyen));
                    if (validCount != selected.Count) throw new InvalidOperationException("Có quyền không hợp lệ.");
                    if (role == null) { role = new RoleRecord { TenNhom = name }; roles.Roles.Add(role); roles.SaveChanges(); }
                    role.TenNhom = name;
                    roles.RolePermissions.RemoveRange(roles.RolePermissions.Where(p => p.MaNhom == role.MaNhom));
                    foreach (string permission in selected) roles.RolePermissions.Add(new RolePermissionRecord { MaNhom = role.MaNhom, MaQuyen = permission });
                    roles.SaveChanges(); tx.Commit(); return role.MaNhom;
                }
            }
        }
        public void Delete(int roleId)
        {
            using (var db = new QuanLyKaraokeEntities())
            using (var tx = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                var actor = AuthService.RequirePermission(db, "NHOMQUYEN");
                using (var roles = Open(db))
                {
                    var role = roles.Roles.SingleOrDefault(r => r.MaNhom == roleId);
                    if (role == null || role.LaHeThong || string.Equals(role.TenNhom, "Manager", StringComparison.OrdinalIgnoreCase)) throw new InvalidOperationException("Không thể xóa nhóm hệ thống hoặc nhóm không tồn tại.");
                    EnsureGrantable(roles, actor, roles.RolePermissions.Where(p => p.MaNhom == roleId).Select(p => p.MaQuyen).ToList());
                    if (roles.AccountRoles.Any(a => a.MaNhom == roleId)) throw new InvalidOperationException("Nhóm đang có tài khoản; không thể xóa.");
                    roles.RolePermissions.RemoveRange(roles.RolePermissions.Where(p => p.MaNhom == roleId));
                    roles.Roles.Remove(role); roles.SaveChanges(); tx.Commit();
                }
            }
        }
    }
}
