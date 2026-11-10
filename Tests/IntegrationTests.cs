using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using karaoke.Helpers;
using karaoke.Models;
using karaoke.Services;

class IntegrationTests
{
    private static int adminMaTK;
    private static int staffMaTK;
    private static string adminMaNV;
    private static string staffMaNV;
    private static string testRoomId;
    private static string maintenanceRoomId;
    private static string testServiceId;
    private static string inactiveServiceId;

    static void Main()
    {
        Console.WriteLine("=================================================");
        Console.WriteLine("STARTING KARAOKE INTEGRATION TEST SUITE");
        Console.WriteLine("=================================================");

        SetupTestFixtures();

        try
        {
            RunTest("1. Role & Database Account Permission Checks", TestPermissions);
            RunTest("2. Schedule Overlap & Adjacency Boundary Cases", TestScheduleOverlapAndAdjacency);
            RunTest("3. Concurrent Overlapping Booking Race Condition", TestConcurrentBooking);
            RunTest("4. Capacity & Maintenance Validation", TestCapacityAndMaintenance);
            RunTest("5. State Transitions (Edit/Cancel on DaNhan/DaHuy)", TestStateTransitions);
            RunTest("6. Check-in Atomicity, Usable Window & Duplicate Prevention", TestCheckInAtomicityAndDuplicates);
            RunTest("7. Service Order Price Snapshot & Immutability", TestOrderPriceSnapshot);
            RunTest("8. Multi-line Cart Rollback on Second Line Error", TestMultiLineCartRollback);
            RunTest("9. Order Rejection on Ended or Paid Sessions", TestEndedAndPaidSessionRejected);
            RunTest("10. Session Totals Calculation for Checkout", TestSessionTotalsCalculation);
            RunTest("11. Concurrent service order versus checkout", TestConcurrentOrderAndCheckout);

            Console.WriteLine("=================================================");
            Console.WriteLine("ALL 11 INTEGRATION TESTS PASSED SUCCESSFULLY!");
            Console.WriteLine("=================================================");
        }
        finally
        {
            CleanupTestFixtures();
        }
    }

    private static void RunTest(string testName, Action testMethod)
    {
        Console.Write("[RUNNING] " + testName + "... ");
        try
        {
            testMethod();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("PASS");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("FAIL: " + ex.Message);
            Console.ResetColor();
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    private static void SetupTestFixtures()
    {
        using (var db = new QuanLyKaraokeEntities())
        {
            if (!db.Database.Connection.Database.StartsWith("KaraokePasswordTest_")) throw new InvalidOperationException("Integration tests require an isolated test database.");
            if (!db.LOAIPHONGs.Any(x => x.MaLoaiPhong == 1)) db.LOAIPHONGs.Add(new LOAIPHONG { MaLoaiPhong = 1, TenLoaiPhong = "Phòng thử", DonGiaGio = 80000 });
            adminMaNV = "NV_ADM_" + Guid.NewGuid().ToString("N").Substring(0, 8);
            staffMaNV = "NV_STF_" + Guid.NewGuid().ToString("N").Substring(0, 8);

            // Add test employees
            db.NHANVIENs.Add(new NHANVIEN
            {
                MaNV = adminMaNV,
                HoTen = "Admin Kiểm Thử",
                ChucVu = "Quản lý",
                TrangThai = "Đang làm việc"
            });

            db.NHANVIENs.Add(new NHANVIEN
            {
                MaNV = staffMaNV,
                HoTen = "Nhân Viên Kiểm Thử",
                ChucVu = "Thu ngân",
                TrangThai = "Đang làm việc"
            });
            db.SaveChanges();

            // Create test admin account
            string adminLogin = "adm_" + Guid.NewGuid().ToString("N").Substring(0, 8);
            var adminAcc = new TAIKHOAN
            {
                MaNV = adminMaNV,
                TenDangNhap = adminLogin,
                MatKhauHash = "$2a$11$DummyHashForTestingOnlyNotRealBCryptPasswd123",
                VaiTro = "Admin",
                TrangThai = true
            };
            db.TAIKHOANs.Add(adminAcc);

            // Create test staff account
            string staffLogin = "stf_" + Guid.NewGuid().ToString("N").Substring(0, 8);
            var staffAcc = new TAIKHOAN
            {
                MaNV = staffMaNV,
                TenDangNhap = staffLogin,
                MatKhauHash = "$2a$11$DummyHashForTestingOnlyNotRealBCryptPasswd123",
                VaiTro = "NhanVien",
                TrangThai = true
            };
            db.TAIKHOANs.Add(staffAcc);
            db.SaveChanges();

            adminMaTK = adminAcc.MaTK;
            staffMaTK = staffAcc.MaTK;

            // Create test room (Capacity 10, Type 1)
            testRoomId = "TR_" + Guid.NewGuid().ToString("N").Substring(0, 10);
            var room = new PHONG
            {
                MaPhong = testRoomId,
                TenPhong = "Phòng Test Tự Động",
                MaLoaiPhong = 1,
                SucChua = 10,
                TrangThai = "Trống"
            };
            db.PHONGs.Add(room);

            // Create maintenance room
            maintenanceRoomId = "MR_" + Guid.NewGuid().ToString("N").Substring(0, 10);
            var mRoom = new PHONG
            {
                MaPhong = maintenanceRoomId,
                TenPhong = "Phòng Đang Bảo Trì",
                MaLoaiPhong = 1,
                SucChua = 10,
                TrangThai = "Bảo trì"
            };
            db.PHONGs.Add(mRoom);

            // Create test services
            testServiceId = "TDV_" + Guid.NewGuid().ToString("N").Substring(0, 8);
            db.DICHVUs.Add(new DICHVU
            {
                MaDV = testServiceId,
                TenDV = "Bia Test Heineken",
                DonViTinh = "Lon",
                DonGia = 25000,
                TrangThai = true
            });

            inactiveServiceId = "TIN_" + Guid.NewGuid().ToString("N").Substring(0, 8);
            db.DICHVUs.Add(new DICHVU
            {
                MaDV = inactiveServiceId,
                TenDV = "Món Ngừng Phục Vụ",
                DonViTinh = "Đĩa",
                DonGia = 50000,
                TrangThai = false
            });

            db.SaveChanges();
        }
        ActAs(adminMaTK, "Admin");
        var roles = new RoleService();
        roles.AssignStaffRole(staffMaTK, roles.List().Single(r => r.TenNhom == "Nhân viên").MaNhom);
        UserSession.Clear();
    }

    private static void CleanupTestFixtures()
    {
        UserSession.Clear();
        using (var db = new QuanLyKaraokeEntities())
        {
            // Remove invoices, order details, sessions, bookings associated with test rooms
            var sessions = db.PHIENHATs.Where(ph => ph.MaPhong == testRoomId || ph.MaPhong == maintenanceRoomId).ToList();
            foreach (var s in sessions)
            {
                var hds = db.HOADONs.Where(h => h.MaPhien == s.MaPhien).ToList();
                db.HOADONs.RemoveRange(hds);
                var ctdvs = db.CHITIETDICHVUs.Where(c => c.MaPhien == s.MaPhien).ToList();
                db.CHITIETDICHVUs.RemoveRange(ctdvs);
            }
            db.SaveChanges();

            db.PHIENHATs.RemoveRange(sessions);
            db.SaveChanges();

            var bookings = db.DATPHONGs.Where(b => b.MaPhong == testRoomId || b.MaPhong == maintenanceRoomId).ToList();
            db.DATPHONGs.RemoveRange(bookings);

            var rooms = db.PHONGs.Where(p => p.MaPhong == testRoomId || p.MaPhong == maintenanceRoomId).ToList();
            db.PHONGs.RemoveRange(rooms);

            var services = db.DICHVUs.Where(d => d.MaDV == testServiceId || d.MaDV == inactiveServiceId).ToList();
            db.DICHVUs.RemoveRange(services);

            db.Database.ExecuteSqlCommand("DELETE FROM dbo.TAIKHOAN_NHOMQUYEN WHERE MaTK = {0} OR MaTK = {1}", adminMaTK, staffMaTK);
            var accounts = db.TAIKHOANs.Where(t => t.MaTK == adminMaTK || t.MaTK == staffMaTK).ToList();
            db.TAIKHOANs.RemoveRange(accounts);
            db.SaveChanges();

            var employees = db.NHANVIENs.Where(n => n.MaNV == adminMaNV || n.MaNV == staffMaNV).ToList();
            db.NHANVIENs.RemoveRange(employees);

            db.SaveChanges();
        }
    }

    private static void ActAs(int maTK, string role)
    {
        UserSession.MaTK = maTK;
        UserSession.VaiTro = role;
        using (var db = new QuanLyKaraokeEntities()) UserSession.PhienBanBaoMat = db.TAIKHOANs.Where(t => t.MaTK == maTK).Select(t => t.PhienBanBaoMat).Single();
    }

    // 1. Role & Database Account Permission Checks
    private static void TestPermissions()
    {
        var svc = new BookingService();
        DateTime start = DateTime.Now.AddDays(1).Date.AddHours(14);
        DateTime end = start.AddHours(2);

        // Anonymous user must be rejected
        UserSession.Clear();
        bool rejectedAnon = false;
        try
        {
            svc.CreateBooking(testRoomId, "Khách test", "0901234567", 4, start, end, "Test");
        }
        catch (UnauthorizedAccessException)
        {
            rejectedAnon = true;
        }
        if (!rejectedAnon) throw new Exception("Anonymous user was NOT rejected!");

        // Tampered / Fake MaTK must be rejected by database check
        UserSession.MaTK = 9999999;
        UserSession.VaiTro = "Admin";
        bool rejectedFake = false;
        try
        {
            svc.CreateBooking(testRoomId, "Khách test", "0901234567", 4, start, end, "Test");
        }
        catch (UnauthorizedAccessException)
        {
            rejectedFake = true;
        }
        if (!rejectedFake) throw new Exception("Fake MaTK was NOT rejected by DB check!");

        // Valid Admin succeeds
        ActAs(adminMaTK, "Admin");
        int b1 = svc.CreateBooking(testRoomId, "Khách Admin", "0901234567", 4, start, end, "Test Admin");
        if (b1 <= 0) throw new Exception("Admin booking failed!");

        // Valid Staff succeeds
        ActAs(staffMaTK, "NhanVien");
        DateTime start2 = start.AddDays(1);
        DateTime end2 = start2.AddHours(2);
        int b2 = svc.CreateBooking(testRoomId, "Khách Nhân Viên", "0907654321", 4, start2, end2, "Test Staff");
        if (b2 <= 0) throw new Exception("Staff booking failed!");
    }

    // 2. Schedule Overlap & Adjacency Boundary Cases
    private static void TestScheduleOverlapAndAdjacency()
    {
        ActAs(adminMaTK, "Admin");
        var svc = new BookingService();

        // Base booking: Day 5, 14:00 - 16:00
        DateTime baseStart = DateTime.Today.AddDays(5).AddHours(14);
        DateTime baseEnd = baseStart.AddHours(2);
        int baseId = svc.CreateBooking(testRoomId, "Khách Gốc", "0911222333", 4, baseStart, baseEnd, "Base slot");

        // ADJACENCY CASE 1: Exactly adjacent before: 12:00 - 14:00
        // end == other.start => start < other.end (12<16 True) but end > other.start (14>14 False) => NO overlap!
        DateTime adjBeforeStart = baseStart.AddHours(-2);
        DateTime adjBeforeEnd = baseStart;
        int adjBeforeId = svc.CreateBooking(testRoomId, "Khách Liền Trước", "0911222334", 4, adjBeforeStart, adjBeforeEnd, "Adjacent before");
        if (adjBeforeId <= 0) throw new Exception("Adjacent-before booking should have succeeded!");

        // ADJACENCY CASE 2: Exactly adjacent after: 16:00 - 18:00
        // start == other.end => start < other.end (16<16 False) => NO overlap!
        DateTime adjAfterStart = baseEnd;
        DateTime adjAfterEnd = baseEnd.AddHours(2);
        int adjAfterId = svc.CreateBooking(testRoomId, "Khách Liền Sau", "0911222335", 4, adjAfterStart, adjAfterEnd, "Adjacent after");
        if (adjAfterId <= 0) throw new Exception("Adjacent-after booking should have succeeded!");

        // OVERLAP CASE 1: Partial overlap front: 13:30 - 15:30
        AssertThrows<InvalidOperationException>(() =>
            svc.CreateBooking(testRoomId, "Trùng Đầu", "0911222336", 4, baseStart.AddMinutes(-30), baseStart.AddMinutes(90), "Overlap start")
        );

        // OVERLAP CASE 2: Partial overlap back: 15:30 - 17:30
        AssertThrows<InvalidOperationException>(() =>
            svc.CreateBooking(testRoomId, "Trùng Đuôi", "0911222337", 4, baseEnd.AddMinutes(-30), baseEnd.AddMinutes(90), "Overlap end")
        );

        // OVERLAP CASE 3: Completely enclosed: 14:30 - 15:30
        AssertThrows<InvalidOperationException>(() =>
            svc.CreateBooking(testRoomId, "Trùng Giữa", "0911222338", 4, baseStart.AddMinutes(30), baseEnd.AddMinutes(-30), "Enclosed")
        );

        // OVERLAP CASE 4: Completely enclosing: 13:00 - 17:00
        AssertThrows<InvalidOperationException>(() =>
            svc.CreateBooking(testRoomId, "Bao Trùm", "0911222339", 4, baseStart.AddHours(-1), baseEnd.AddHours(1), "Enclosing")
        );
    }

    // 3. Concurrent Overlapping Booking Race Condition
    private static void TestConcurrentBooking()
    {
        ActAs(adminMaTK, "Admin");
        DateTime raceStart = DateTime.Today.AddDays(10).AddHours(19);
        DateTime raceEnd = raceStart.AddHours(3);

        int attempts = 4;
        int successCount = 0;
        int failureCount = 0;

        Parallel.For(0, attempts, i =>
        {
            var svc = new BookingService();
            try
            {
                // Each concurrent thread tries to book the exact same slot
                svc.CreateBooking(testRoomId, "Khách Đua " + i, "090888800" + i, 4, raceStart, raceEnd, "Race " + i);
                System.Threading.Interlocked.Increment(ref successCount);
            }
            catch (InvalidOperationException)
            {
                System.Threading.Interlocked.Increment(ref failureCount);
            }
            catch (Exception)
            {
                // In some concurrent database serializable transactions, a deadlock victim / conflict exception may be raised
                System.Threading.Interlocked.Increment(ref failureCount);
            }
        });

        if (successCount != 1)
        {
            throw new Exception(string.Format("Concurrent race condition failure: expected exactly 1 winner, but had {0} winners and {1} losers!", successCount, failureCount));
        }
    }

    // 4. Capacity & Maintenance Validation
    private static void TestCapacityAndMaintenance()
    {
        ActAs(adminMaTK, "Admin");
        var svc = new BookingService();
        DateTime start = DateTime.Today.AddDays(12).AddHours(14);
        DateTime end = start.AddHours(2);

        // testRoomId has capacity 10 -> booking 15 guests must fail
        AssertThrows<InvalidOperationException>(() =>
            svc.CreateBooking(testRoomId, "Quá Tải", "0912345678", 15, start, end, "Over capacity")
        );

        // maintenanceRoomId is "Bảo trì" -> booking must fail
        AssertThrows<InvalidOperationException>(() =>
            svc.CreateBooking(maintenanceRoomId, "Khách", "0912345678", 4, start, end, "Under maintenance")
        );

        // End <= Start must fail
        AssertThrows<InvalidOperationException>(() =>
            svc.CreateBooking(testRoomId, "Khách", "0912345678", 4, end, start, "Invalid time range")
        );

        // Past start time must fail
        AssertThrows<InvalidOperationException>(() =>
            svc.CreateBooking(testRoomId, "Khách", "0912345678", 4, DateTime.Now.AddHours(-2), DateTime.Now.AddHours(-1), "Past start")
        );
    }

    // 5. State Transitions (Edit/Cancel on DaNhan/DaHuy)
    private static void TestStateTransitions()
    {
        ActAs(adminMaTK, "Admin");
        var svc = new BookingService();
        DateTime start = DateTime.Today.AddDays(15).AddHours(14);
        DateTime end = start.AddHours(2);

        int bId = svc.CreateBooking(testRoomId, "Khách State", "0918765432", 4, start, end, "State test");

        // Cancel the booking
        svc.CancelBooking(bId, "Khách hủy hẹn");

        // Attempting to edit a cancelled booking must be rejected
        AssertThrows<InvalidOperationException>(() =>
            svc.EditBooking(bId, testRoomId, "Đổi Tên", "0918765432", 4, start, end, "Edit cancelled")
        );

        // Attempting to cancel an already cancelled booking must be rejected
        AssertThrows<InvalidOperationException>(() =>
            svc.CancelBooking(bId, "Hủy lần hai")
        );

        // Attempting to check in a cancelled booking must be rejected
        AssertThrows<InvalidOperationException>(() =>
            svc.CheckInBooking(bId)
        );
    }

    // 6. Check-in Atomicity, Usable Window & Duplicate Prevention
    private static void TestCheckInAtomicityAndDuplicates()
    {
        ActAs(adminMaTK, "Admin");
        var svc = new BookingService();

        // 1. Check early arrival window rejection:
        // Booking is 5 hours in future => early check-in (> 30 min before) must be rejected
        DateTime futureStart = DateTime.Now.AddHours(5);
        DateTime futureEnd = futureStart.AddHours(2);
        int futureId = svc.CreateBooking(testRoomId, "Khách Đến Quá Sớm", "0913334445", 4, futureStart, futureEnd, "Too early");

        AssertThrows<InvalidOperationException>(() =>
            svc.CheckInBooking(futureId)
        );

        // 2. Check-in within valid window (e.g. starting in 15 minutes, ending in 2 hours):
        DateTime validStart = DateTime.Now.AddMinutes(15);
        DateTime validEnd = validStart.AddHours(2);
        int validId = svc.CreateBooking(testRoomId, "Khách Nhận Phòng", "0913334446", 4, validStart, validEnd, "Check-in test");

        int maPhien = svc.CheckInBooking(validId);
        if (maPhien <= 0) throw new Exception("Check-in failed to return valid MaPhien!");

        using (var db = new QuanLyKaraokeEntities())
        {
            var booking = db.DATPHONGs.Find(validId);
            if (booking.TrangThai != "DaNhan") throw new Exception("Booking was not marked DaNhan!");

            var room = db.PHONGs.Find(testRoomId);
            if (room.TrangThai != "Đang sử dụng") throw new Exception("Room status was not set to Đang sử dụng!");

            var phien = db.PHIENHATs.Find(maPhien);
            if (phien == null || phien.TrangThai != "DangHat") throw new Exception("Active session not found!");
            if (phien.DonGiaGio != booking.DonGiaGioDaChot) throw new Exception("Session rate did not match frozen booking rate!");
            if (phien.MaDatPhong != validId) throw new Exception("Session not linked to booking!");
        }

        // 3. Duplicate check-in on the same booking must be rejected
        AssertThrows<InvalidOperationException>(() =>
            svc.CheckInBooking(validId)
        );

        // 4. Another booking on the room while it's in use cannot be checked in
        DateTime overlapStart = DateTime.Now.AddMinutes(20);
        DateTime overlapEnd = overlapStart.AddHours(1);
        AssertThrows<InvalidOperationException>(() => svc.CreateBooking(testRoomId, "Khách trùng lịch", "0919998887", 4, overlapStart, overlapEnd, null));
        if (svc.FindAvailableRooms(overlapStart, overlapEnd, 4).Any(r => r.MaPhong == testRoomId))
            throw new Exception("Checked-in room was offered for an overlapping booking.");
        using (var db = new QuanLyKaraokeEntities())
        {
            // Direct insert for test to bypass create validation
            var bConflict = new DATPHONG
            {
                MaPhong = testRoomId,
                TenKhach = "Khách Kẹt",
                SDT = "0919998887",
                SoKhach = 4,
                GioBatDauDuKien = overlapStart,
                GioKetThucDuKien = overlapEnd,
                DonGiaGioDaChot = 80000,
                TrangThai = "ChoNhan",
                MaNV = adminMaNV
            };
            db.DATPHONGs.Add(bConflict);
            db.SaveChanges();

            AssertThrows<InvalidOperationException>(() =>
                svc.CheckInBooking(bConflict.MaDatPhong)
            );

            // Clean up conflict booking
            db.DATPHONGs.Remove(bConflict);
            db.SaveChanges();
        }

        // Clean up session and reset room to 'Trống' for subsequent tests
        using (var db = new QuanLyKaraokeEntities())
        {
            var p = db.PHIENHATs.Find(maPhien);
            if (p != null) db.PHIENHATs.Remove(p);
            var r = db.PHONGs.Find(testRoomId);
            if (r != null) r.TrangThai = "Trống";
            db.SaveChanges();
        }
    }

    // 7. Service Order Price Snapshot & Immutability
    private static void TestOrderPriceSnapshot()
    {
        ActAs(adminMaTK, "Admin");
        var orderService = new ServiceOrderService();

        int maPhien = CreateTestSession();

        // 1. Initial order at current catalog price (25,000)
        var cart = new List<CartItemInput>
        {
            new CartItemInput { MaDV = testServiceId, SoLuong = 2 }
        };
        orderService.OrderServices(maPhien, cart);

        // 2. Change catalog price to 40,000
        using (var db = new QuanLyKaraokeEntities())
        {
            var dv = db.DICHVUs.Find(testServiceId);
            dv.DonGia = 40000;
            db.SaveChanges();
        }

        // 3. Verify that the previous order preserved the frozen price of 25,000
        var orders = orderService.GetSessionOrders(maPhien);
        var firstOrder = orders.FirstOrDefault(o => o.MaDV == testServiceId);
        if (firstOrder == null) throw new Exception("Order not found!");
        if (firstOrder.DonGia != 25000)
        {
            throw new Exception(string.Format("Price snapshot failed! Expected frozen price 25,000, but got {0}", firstOrder.DonGia));
        }

        // Reset service catalog price
        using (var db = new QuanLyKaraokeEntities())
        {
            var dv = db.DICHVUs.Find(testServiceId);
            dv.DonGia = 25000;
            db.SaveChanges();
        }

        CloseTestSession(maPhien);
    }

    // 8. Multi-line Cart Rollback on Second Line Error
    private static void TestMultiLineCartRollback()
    {
        ActAs(adminMaTK, "Admin");
        var orderService = new ServiceOrderService();

        int maPhien = CreateTestSession();
        int initialOrderCount = orderService.GetSessionOrders(maPhien).Count;

        // Line 1 is valid (Bia Heineken, qty 2). Line 2 has invalid quantity (qty <= 0)
        var badCart = new List<CartItemInput>
        {
            new CartItemInput { MaDV = testServiceId, SoLuong = 2 },
            new CartItemInput { MaDV = testServiceId, SoLuong = 0 } // Duplicate and invalid qty
        };

        AssertThrows<InvalidOperationException>(() =>
            orderService.OrderServices(maPhien, badCart)
        );

        // Verify that NO line was committed (atomic rollback)
        int orderCountAfter = orderService.GetSessionOrders(maPhien).Count;
        if (orderCountAfter != initialOrderCount)
        {
            throw new Exception("Transaction was NOT rolled back on second line error! Items were inserted.");
        }

        // Cart with inactive product must also roll back
        var inactiveCart = new List<CartItemInput>
        {
            new CartItemInput { MaDV = testServiceId, SoLuong = 3 },
            new CartItemInput { MaDV = inactiveServiceId, SoLuong = 1 }
        };

        AssertThrows<InvalidOperationException>(() =>
            orderService.OrderServices(maPhien, inactiveCart)
        );

        int orderCountAfterInactive = orderService.GetSessionOrders(maPhien).Count;
        if (orderCountAfterInactive != initialOrderCount)
        {
            throw new Exception("Transaction was NOT rolled back on inactive item error!");
        }

        CloseTestSession(maPhien);
    }

    // 9. Order Rejection on Ended or Paid Sessions
    private static void TestEndedAndPaidSessionRejected()
    {
        ActAs(adminMaTK, "Admin");
        var orderService = new ServiceOrderService();

        int maPhien = CreateTestSession();

        // End the session
        using (var db = new QuanLyKaraokeEntities())
        {
            var ph = db.PHIENHATs.Find(maPhien);
            ph.TrangThai = "DaKetThuc";
            ph.GioKetThuc = DateTime.Now;
            db.SaveChanges();
        }

        var cart = new List<CartItemInput>
        {
            new CartItemInput { MaDV = testServiceId, SoLuong = 1 }
        };

        // Ended session order must fail
        AssertThrows<InvalidOperationException>(() =>
            orderService.OrderServices(maPhien, cart)
        );

        // Simulate session having an invoice (paid)
        using (var db = new QuanLyKaraokeEntities())
        {
            var ph = db.PHIENHATs.Find(maPhien);
            ph.TrangThai = "DangHat"; // revert state to test invoice rule
            var hd = new HOADON
            {
                MaPhien = maPhien,
                MaNV = adminMaNV,
                NgayThanhToan = DateTime.Now,
                TienPhong = 80000,
                TienDichVu = 25000,
                GiamGia = 0,
                ThanhTien = 105000,
                PhuongThucTT = "Tiền mặt"
            };
            db.HOADONs.Add(hd);
            db.SaveChanges();
        }

        // Paid session order must fail
        AssertThrows<InvalidOperationException>(() =>
            orderService.OrderServices(maPhien, cart)
        );

        CloseTestSession(maPhien);
    }

    // 10. Session Totals Calculation for Checkout
    private static void TestSessionTotalsCalculation()
    {
        ActAs(adminMaTK, "Admin");
        var orderService = new ServiceOrderService();

        int maPhien = CreateTestSession();

        var cart = new List<CartItemInput>
        {
            new CartItemInput { MaDV = testServiceId, SoLuong = 3 } // 3 * 25,000 = 75,000
        };
        orderService.OrderServices(maPhien, cart);

        var summary = orderService.GetSessionTotal(maPhien);
        if (summary.TienDichVu != 75000)
        {
            throw new Exception(string.Format("TienDichVu incorrect: expected 75,000, got {0}", summary.TienDichVu));
        }
        if (summary.SoLuongMon != 3)
        {
            throw new Exception(string.Format("SoLuongMon incorrect: expected 3, got {0}", summary.SoLuongMon));
        }
        if (summary.TongCongTamTinh < 75000)
        {
            throw new Exception("TongCongTamTinh should be >= TienDichVu");
        }

        CloseTestSession(maPhien);
    }

    private static void TestConcurrentOrderAndCheckout()
    {
        ActAs(adminMaTK, "Admin");
        for (int iteration = 0; iteration < 5; iteration++)
        {
            int session = CreateTestSession();
            int invoice = 0; bool orderSucceeded = false; Exception orderError = null, paymentError = null;
            try
            {
                Parallel.Invoke(
                    () => { try { new ServiceOrderService().OrderServices(session, new List<CartItemInput> { new CartItemInput { MaDV = testServiceId, SoLuong = 2 } }); orderSucceeded = true; } catch (Exception ex) { orderError = ex; } },
                    () => { try { invoice = new PaymentService().CompletePayment(session, 0m, "Tiền mặt"); } catch (Exception ex) { paymentError = ex; } });
                if (orderError != null && !(orderError is InvalidOperationException) && !IsDeadlock(orderError)) throw new Exception("Unexpected order error", orderError);
                if (paymentError != null && !IsDeadlock(paymentError)) throw new Exception("Unexpected checkout error", paymentError);
                if (invoice == 0) invoice = new PaymentService().CompletePayment(session, 0m, "Tiền mặt");
                using (var db = new QuanLyKaraokeEntities())
                {
                    var bill = db.HOADONs.Single(h => h.MaHD == invoice);
                    var lines = db.CHITIETDICHVUs.Where(c => c.MaPhien == session).ToList();
                    decimal sum = lines.Sum(c => c.SoLuong * c.DonGia);
                    if (bill.TienDichVu != sum || (orderSucceeded ? lines.Count != 1 : lines.Count != 0))
                        throw new Exception("Concurrent checkout produced inconsistent service charges.");
                }
                AssertThrows<InvalidOperationException>(() => new ServiceOrderService().OrderServices(session, new List<CartItemInput> { new CartItemInput { MaDV = testServiceId, SoLuong = 1 } }));
            }
            finally { CloseTestSession(session); }
        }
    }

    private static bool IsDeadlock(Exception error)
    {
        var sql = error.GetBaseException() as System.Data.SqlClient.SqlException;
        return sql != null && sql.Number == 1205;
    }

    private static int CreateTestSession()
    {
        using (var db = new QuanLyKaraokeEntities())
        {
            var ph = new PHIENHAT
            {
                MaPhong = testRoomId,
                MaNV = adminMaNV,
                GioBatDau = DateTime.Now.AddHours(-1),
                DonGiaGio = 80000,
                TrangThai = "DangHat"
            };
            db.PHIENHATs.Add(ph);
            db.SaveChanges();
            return ph.MaPhien;
        }
    }

    private static void CloseTestSession(int maPhien)
    {
        using (var db = new QuanLyKaraokeEntities())
        {
            var hds = db.HOADONs.Where(h => h.MaPhien == maPhien).ToList();
            db.HOADONs.RemoveRange(hds);

            var ctdvs = db.CHITIETDICHVUs.Where(c => c.MaPhien == maPhien).ToList();
            db.CHITIETDICHVUs.RemoveRange(ctdvs);

            var ph = db.PHIENHATs.Find(maPhien);
            if (ph != null) db.PHIENHATs.Remove(ph);

            var room = db.PHONGs.Find(testRoomId);
            if (room != null) room.TrangThai = "Trống";

            db.SaveChanges();
        }
    }

    private static void AssertThrows<TException>(Action action) where TException : Exception
    {
        bool threw = false;
        try
        {
            action();
        }
        catch (TException)
        {
            threw = true;
        }
        catch (Exception ex)
        {
            if (ex.GetBaseException() is TException)
            {
                threw = true;
            }
            else
            {
                throw new Exception(string.Format("Expected {0}, but got {1}: {2}", typeof(TException).Name, ex.GetType().Name, ex.Message));
            }
        }

        if (!threw)
        {
            throw new Exception(string.Format("Expected {0} was NOT thrown!", typeof(TException).Name));
        }
    }
}
