using System;
using System.Linq;
using karaoke.Helpers;
using karaoke.Models;
using karaoke.Services;

class PasswordChangeTests
{
    static void Check(bool condition, string message) { if (!condition) throw new Exception(message); Console.WriteLine("PASS: " + message); }
    static int Main()
    {
        try
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                if (!db.Database.Connection.Database.StartsWith("KaraokePasswordTest_")) throw new Exception("Test requires an isolated test database.");
                db.NHANVIENs.Add(new NHANVIEN { MaNV = "TESTSTAFF", HoTen = "Test", ChucVu = "Nhân viên", TrangThai = "Đang làm việc" });
                db.NHANVIENs.Add(new NHANVIEN { MaNV = "TESTADMIN", HoTen = "Admin", ChucVu = "Admin", TrangThai = "Đang làm việc" });
                db.NHANVIENs.Add(new NHANVIEN { MaNV = "NEWSTAFF", HoTen = "Nhân viên mới", ChucVu = "Nhân viên", TrangThai = "Đang làm việc" });
                db.TAIKHOANs.Add(new TAIKHOAN { MaNV = "TESTSTAFF", TenDangNhap = "pwdtest", MatKhauHash = BCrypt.Net.BCrypt.HashPassword("OldPassword123"), VaiTro = "NhanVien", TrangThai = true });
                db.TAIKHOANs.Add(new TAIKHOAN { MaNV = "TESTADMIN", TenDangNhap = "admin_test", MatKhauHash = BCrypt.Net.BCrypt.HashPassword("AdminPassword123"), VaiTro = "Admin", TrangThai = true });
                db.SaveChanges();
            }
            var auth = new AuthService(); var accounts = new AccountService();
            Check(auth.DangNhap("admin", "AdminKaraoke2026!"), "fresh setup administrator password works");
            Check(auth.DangNhap("pwdtest", "OldPassword123"), "initial password works");
            int accountId = UserSession.MaTK, previousVersion = UserSession.PhienBanBaoMat;
            bool denied = false;
            try { accounts.ChangeOwnPassword("WrongPassword", "NewPassword456"); } catch (UnauthorizedAccessException) { denied = true; }
            Check(denied, "wrong current password rejected");
            denied = false;
            try { accounts.ChangeOwnPassword("OldPassword123", "short"); } catch (ArgumentException) { denied = true; }
            Check(denied, "short new password rejected");
            accounts.ChangeOwnPassword("OldPassword123", "NewPassword456");
            Check(UserSession.PhienBanBaoMat == previousVersion + 1, "current session remains valid with new version");
            AuthService.RequireCurrentUser();
            UserSession.PhienBanBaoMat = previousVersion;
            denied = false;
            try { AuthService.RequireCurrentUser(); } catch (UnauthorizedAccessException) { denied = true; }
            Check(denied, "previous sessions revoked");
            Check(!auth.DangNhap("pwdtest", "OldPassword123"), "old password rejected");
            Check(auth.DangNhap("pwdtest", "NewPassword456") && UserSession.MaTK == accountId, "new password accepted");
            using (var db = new QuanLyKaraokeEntities()) Check(db.TAIKHOANs.Single(x => x.MaTK == accountId).MatKhauHash != "NewPassword456", "password stored as hash");
            denied = false;
            try { accounts.List(); } catch (UnauthorizedAccessException) { denied = true; }
            Check(denied, "staff cannot administer accounts");
            denied = false;
            try { new StaffService().List(); } catch (UnauthorizedAccessException) { denied = true; }
            Check(denied, "staff cannot administer employees");
            denied = false;
            try { new RoomTypeService().Save(83, "Test", 80000, true); } catch (UnauthorizedAccessException) { denied = true; }
            Check(denied, "staff cannot edit room types");
            Check(auth.DangNhap("admin_test", "AdminPassword123"), "admin login");
            var roles = new RoleService();
            int groupId = roles.Save(null, "Lễ tân thử", new[] { "PHONG_XEM", "DATPHONG" });
            Check(roles.GetPermissions(groupId).OrderBy(x => x).SequenceEqual(new[] { "DATPHONG", "PHONG_XEM" }), "group permissions saved");
            roles.Save(groupId, "Lễ tân thử", new[] { "PHONG_XEM", "GOIDICHVU" });
            Check(roles.GetPermissions(groupId).Count == 2, "overlapping permissions remain valid when edited");
            roles.Save(groupId, "Lễ tân cập nhật", new[] { "GOIDICHVU" });
            Check(roles.GetPermissions(groupId).SequenceEqual(new[] { "GOIDICHVU" }), "group permissions replaced transactionally");
            roles.Delete(groupId);
            Check(!roles.List().Any(r => r.MaNhom == groupId), "unused custom group deleted");
            denied = false;
            try { roles.Delete(roles.List().First(r => r.LaHeThong).MaNhom); } catch (InvalidOperationException) { denied = true; }
            Check(denied, "system group cannot be deleted");
            var types = new RoomTypeService();
            types.Save(83, "Loại thử", 80000, true);
            using (var db = new QuanLyKaraokeEntities()) { db.PHONGs.Add(new PHONG { MaPhong = "TESTROOM", TenPhong = "Test", MaLoaiPhong = 83, SucChua = 6, TrangThai = "Trống" }); db.SaveChanges(); }
            int bookingId = new BookingService().CreateBooking("TESTROOM", "Khách thử", "0901234567", 2, DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(1), null);
            types.Save(83, "Loại thử", 120000, false);
            using (var db = new QuanLyKaraokeEntities()) Check(db.DATPHONGs.Single(x => x.MaDatPhong == bookingId).DonGiaGioDaChot == 80000, "price change preserves booking snapshot");
            var editBooking = new BookingService().GetBooking(bookingId);
            var editableRooms = new BookingService().FindAvailableRooms(editBooking.GioBatDauDuKien, editBooking.GioKetThucDuKien, 2, 83, bookingId);
            Check(editableRooms.Any(r => r.MaPhong == "TESTROOM" && r.DonGiaGio == 80000), "editing search keeps original room and frozen price");
            Check(!new BookingService().FindAvailableRooms(editBooking.GioBatDauDuKien, editBooking.GioKetThucDuKien, 2, 84, bookingId).Any(r => r.MaPhong == "TESTROOM"), "editing search honors room type filter");
            Check(!new BookingService().FindAvailableRooms(editBooking.GioBatDauDuKien, editBooking.GioKetThucDuKien, 2).Any(r => r.MaPhong == "TESTROOM"), "normal search still excludes booked room");
            denied = false;
            try { types.Delete(83); } catch (InvalidOperationException) { denied = true; }
            Check(denied, "cannot delete room type in use");
            types.Save(84, "Loại tạm", 10000, true); types.Delete(84);
            Check(!types.List().Any(x => x.MaLoaiPhong == 84), "unused room type can be removed");
            using (var db = new QuanLyKaraokeEntities())
            {
                db.PHONGs.Add(new PHONG { MaPhong = "FLOWROOM", TenPhong = "Phòng liên thông", MaLoaiPhong = 83, SucChua = 6, TrangThai = "Trống" });
                db.DICHVUs.Add(new DICHVU { MaDV = "FLOWDRINK", TenDV = "Nước thử", DonViTinh = "Chai", DonGia = 15000, TrangThai = true });
                db.DICHVUs.Add(new DICHVU { MaDV = "FLOWSNACK", TenDV = "Món thử", DonViTinh = "Đĩa", DonGia = 25000, TrangThai = true });
                db.SaveChanges();
            }
            var bookingService = new BookingService();
            int flowBooking = bookingService.CreateBooking("FLOWROOM", "Khách kiểm thử", "0901234567", 2, DateTime.Now.AddMinutes(1), DateTime.Now.AddHours(1), null);
            int flowSession = bookingService.CheckInBooking(flowBooking);
            var orderService = new ServiceOrderService();
            orderService.OrderServices(flowSession, new System.Collections.Generic.List<CartItemInput> {
                new CartItemInput { MaDV = "FLOWDRINK", SoLuong = 2, DonGia = 1 },
                new CartItemInput { MaDV = "FLOWSNACK", SoLuong = 1, DonGia = 1 }
            });
            Check(orderService.GetSessionTotal(flowSession).TienDichVu == 55000, "order uses catalog price for both items");
            var payments = new PaymentService();
            int invoiceId = payments.CompletePayment(flowSession, 1000, "Tiền mặt");
            var saved = payments.GetSavedInvoiceDetail(invoiceId);
            Check(saved.DanhSachDichVu.Count == 2 && saved.TienDichVu == 55000 && saved.GiamGia == 1000 && saved.ThanhTien == saved.TienPhong + saved.TienDichVu - saved.GiamGia, "saved invoice has both items and correct totals");
            Check(saved.TongGioHat >= 0.25 && saved.TienPhong == Math.Round((decimal)saved.TongGioHat * saved.DonGiaGio, 0), "saved invoice shows billed hours consistent with charge");
            using (var db = new QuanLyKaraokeEntities()) Check(db.PHONGs.Single(x => x.MaPhong == "FLOWROOM").TrangThai == "Trống" && db.PHIENHATs.Single(x => x.MaPhien == flowSession).TrangThai == "DaKetThuc", "checkout ends session and frees room");
            Check(new StatisticService().GetInvoiceReportList(DateTime.Today, DateTime.Today).Any(h => h.MaHD == invoiceId && h.ThanhTien == saved.ThanhTien), "revenue report includes saved invoice");
            denied = false;
            try { orderService.OrderServices(flowSession, new System.Collections.Generic.List<CartItemInput> { new CartItemInput { MaDV = "FLOWDRINK", SoLuong = 1 } }); } catch (InvalidOperationException) { denied = true; }
            Check(denied, "cannot order after checkout");
            denied = false;
            try { payments.CompletePayment(flowSession, 0, "Tiền mặt"); } catch (InvalidOperationException) { denied = true; }
            Check(denied, "cannot charge same session twice");
            var staffService = new StaffService();
            staffService.Save("ADDED", "Nhân viên thử", "0901234567", "Phục vụ", true, true);
            Check(staffService.List().Any(n => n.MaNV == "ADDED" && n.TrangThai == "Đang làm việc"), "admin creates employee");
            staffService.Save("ADDED", "Tên cập nhật", "0901234567", "Phục vụ", false, false);
            Check(staffService.List().Any(n => n.MaNV == "ADDED" && n.TrangThai == "Đã nghỉ việc"), "employee can be deactivated without deletion");
            denied = false;
            try { staffService.Save("TESTADMIN", "Admin", "", "Admin", false, false); } catch (InvalidOperationException) { denied = true; }
            Check(denied, "admin cannot deactivate own employee profile");
            Check(accounts.AvailableStaff().Any(n => n.MaNV == "NEWSTAFF"), "available employee listed");
            int newAccount = accounts.CreateStaffAccount("NEWSTAFF", "newstaff", "StaffPassword123");
            Check(accounts.List().Any(a => a.MaTK == newAccount && a.VaiTro == "NhanVien"), "admin creates employee account");
            int assignedGroup = roles.Save(null, "Tiếp tân thử", new[] { "DATPHONG" });
            Check(roles.GetAccountRole(newAccount).HasValue, "new employee gets default group");
            roles.AssignStaffRole(newAccount, assignedGroup);
            Check(roles.GetAccountRole(newAccount) == assignedGroup, "admin assigns custom group");
            denied = false;
            try { roles.Delete(assignedGroup); } catch (InvalidOperationException) { denied = true; }
            Check(denied, "assigned group cannot be deleted");
            denied = false;
            try { roles.AssignStaffRole(newAccount, roles.List().Single(r => r.TenNhom == "Admin").MaNhom); } catch (InvalidOperationException) { denied = true; }
            Check(denied, "admin system group cannot be assigned to employee");
            denied = false;
            try { accounts.ResetStaffPassword(newAccount, "wrong", "ChangedPassword123"); } catch (UnauthorizedAccessException) { denied = true; }
            Check(denied, "password reset requires admin password");
            accounts.ResetStaffPassword(newAccount, "AdminPassword123", "ChangedPassword123");
            accounts.SetStaffStatus(newAccount, false);
            Check(!auth.DangNhap("newstaff", "ChangedPassword123"), "disabled staff cannot log in");
            Check(auth.DangNhap("admin_test", "AdminPassword123"), "admin can re-enable staff");
            accounts.SetStaffStatus(newAccount, true);
            Check(auth.DangNhap("newstaff", "ChangedPassword123"), "reset password works after re-enabling");
            Check(AuthService.HasPermission("DATPHONG") && !AuthService.HasPermission("GOIDICHVU"), "custom group permissions are effective");
            new BookingService().ListBookings();
            denied = false;
            try { new ServiceOrderService().GetActiveSessions(); } catch (UnauthorizedAccessException) { denied = true; }
            Check(denied, "group without service order permission is denied by service");
            denied = false;
            try { new PaymentService().GetActiveSessions(); } catch (UnauthorizedAccessException) { denied = true; }
            Check(denied, "group without payment permission is denied by service");
            Check(auth.DangNhap("admin_test", "AdminPassword123"), "admin prepares delegated rights test");
            roles.Save(assignedGroup, "Tiếp tân thử", new[] { "TAIKHOAN", "NHOMQUYEN", "NHANVIEN" });
            int privileged = roles.Save(null, "Quản lý thử", new[] { "THONGKE" });
            roles.AssignStaffRole(accountId, privileged);
            Check(auth.DangNhap("newstaff", "ChangedPassword123"), "delegated manager logs in");
            denied = false;
            try { roles.AssignStaffRole(newAccount, privileged); } catch (UnauthorizedAccessException) { denied = true; }
            Check(denied, "delegated account manager cannot grant own account stronger permissions");
            denied = false;
            try { accounts.ResetStaffPassword(accountId, "ChangedPassword123", "AnotherPassword123"); } catch (UnauthorizedAccessException) { denied = true; }
            Check(denied, "delegated account manager cannot reset a more privileged account");
            denied = false;
            try { roles.Save(null, "Quyền cao không hợp lệ", new[] { "THONGKE" }); } catch (UnauthorizedAccessException) { denied = true; }
            Check(denied, "delegated group manager cannot create stronger group");
            for (int i = 0; i < 5; i++) Check(!auth.DangNhap("newstaff", "IncorrectPassword123"), "wrong password rejected " + (i + 1));
            Check(!auth.DangNhap("newstaff", "ChangedPassword123"), "account temporarily locked after five failures");
            Check(auth.DangNhap("admin_test", "AdminPassword123"), "admin can recover locked account");
            accounts.ResetStaffPassword(newAccount, "AdminPassword123", "RecoveredPassword123");
            Check(auth.DangNhap("newstaff", "RecoveredPassword123"), "admin reset clears lockout");
            return 0;
        }
        catch (Exception ex) { Console.Error.WriteLine(ex); return 1; }
        finally { UserSession.Clear(); }
    }
}
