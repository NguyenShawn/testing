using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using karaoke.Models;
using karaoke.Helpers;

namespace karaoke.Services
{
    public class BookingFilter
    {
        public string SearchText { get; set; }
        public string TrangThai { get; set; }
        public string MaPhong { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
    }

    public class BookingRow
    {
        public int MaDatPhong { get; set; }
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public string LoaiPhong { get; set; }
        public string TenKhach { get; set; }
        public string SDT { get; set; }
        public int SoKhach { get; set; }
        public DateTime GioBatDauDuKien { get; set; }
        public DateTime GioKetThucDuKien { get; set; }
        public decimal DonGiaGioDaChot { get; set; }
        public string TrangThai { get; set; }
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public string GhiChu { get; set; }
    }

    public class AvailableRoomRow
    {
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public int MaLoaiPhong { get; set; }
        public string TenLoaiPhong { get; set; }
        public decimal DonGiaGio { get; set; }
        public int SucChua { get; set; }
        public string TrangThai { get; set; }
    }

    public class BookingService
    {
        public List<BookingRow> ListBookings(BookingFilter filter = null)
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                var query = from b in db.DATPHONGs
                            join p in db.PHONGs on b.MaPhong equals p.MaPhong
                            join lp in db.LOAIPHONGs on p.MaLoaiPhong equals lp.MaLoaiPhong
                            join nv in db.NHANVIENs on b.MaNV equals nv.MaNV into nvGroup
                            from nv in nvGroup.DefaultIfEmpty()
                            select new
                            {
                                Booking = b,
                                TenPhong = p.TenPhong,
                                LoaiPhong = lp.TenLoaiPhong,
                                TenNV = nv != null ? nv.HoTen : b.MaNV
                            };

                if (filter != null)
                {
                    if (!string.IsNullOrWhiteSpace(filter.SearchText))
                    {
                        string search = filter.SearchText.Trim();
                        query = query.Where(x => x.Booking.TenKhach.Contains(search) || x.Booking.SDT.Contains(search));
                    }

                    if (!string.IsNullOrWhiteSpace(filter.TrangThai) && filter.TrangThai != "TatCa")
                    {
                        query = query.Where(x => x.Booking.TrangThai == filter.TrangThai);
                    }

                    if (!string.IsNullOrWhiteSpace(filter.MaPhong))
                    {
                        query = query.Where(x => x.Booking.MaPhong == filter.MaPhong);
                    }

                    if (filter.TuNgay.HasValue)
                    {
                        DateTime tuNgay = filter.TuNgay.Value.Date;
                        query = query.Where(x => x.Booking.GioBatDauDuKien >= tuNgay);
                    }

                    if (filter.DenNgay.HasValue)
                    {
                        DateTime denNgay = filter.DenNgay.Value.Date.AddDays(1);
                        query = query.Where(x => x.Booking.GioBatDauDuKien < denNgay);
                    }
                }

                var list = query.OrderByDescending(x => x.Booking.GioBatDauDuKien)
                                .ToList();

                return list.Select(x => new BookingRow
                {
                    MaDatPhong = x.Booking.MaDatPhong,
                    MaPhong = x.Booking.MaPhong,
                    TenPhong = x.TenPhong,
                    LoaiPhong = x.LoaiPhong,
                    TenKhach = x.Booking.TenKhach,
                    SDT = x.Booking.SDT,
                    SoKhach = x.Booking.SoKhach,
                    GioBatDauDuKien = x.Booking.GioBatDauDuKien,
                    GioKetThucDuKien = x.Booking.GioKetThucDuKien,
                    DonGiaGioDaChot = x.Booking.DonGiaGioDaChot,
                    TrangThai = x.Booking.TrangThai,
                    MaNV = x.Booking.MaNV,
                    TenNV = x.TenNV,
                    GhiChu = x.Booking.GhiChu
                }).ToList();
            }
        }

        public BookingRow GetBooking(int maDatPhong)
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                var row = (from b in db.DATPHONGs
                           join p in db.PHONGs on b.MaPhong equals p.MaPhong
                           join lp in db.LOAIPHONGs on p.MaLoaiPhong equals lp.MaLoaiPhong
                           join nv in db.NHANVIENs on b.MaNV equals nv.MaNV into nvGroup
                           from nv in nvGroup.DefaultIfEmpty()
                           where b.MaDatPhong == maDatPhong
                           select new BookingRow
                           {
                               MaDatPhong = b.MaDatPhong,
                               MaPhong = b.MaPhong,
                               TenPhong = p.TenPhong,
                               LoaiPhong = lp.TenLoaiPhong,
                               TenKhach = b.TenKhach,
                               SDT = b.SDT,
                               SoKhach = b.SoKhach,
                               GioBatDauDuKien = b.GioBatDauDuKien,
                               GioKetThucDuKien = b.GioKetThucDuKien,
                               DonGiaGioDaChot = b.DonGiaGioDaChot,
                               TrangThai = b.TrangThai,
                               MaNV = b.MaNV,
                               TenNV = nv != null ? nv.HoTen : b.MaNV,
                               GhiChu = b.GhiChu
                           }).FirstOrDefault();

                return row;
            }
        }

        public List<AvailableRoomRow> FindAvailableRooms(DateTime start, DateTime end, int guestCount = 1, int? maLoaiPhong = null)
        {
            if (end <= start)
            {
                throw new ArgumentException("Giờ kết thúc dự kiến phải sau giờ bắt đầu dự kiến.");
            }
            if (guestCount <= 0)
            {
                throw new ArgumentException("Số lượng khách phải lớn hơn 0.");
            }

            DateTime now = DateTime.Now;

            using (var db = new QuanLyKaraokeEntities())
            {
                var query = db.PHONGs.Include("LOAIPHONG")
                                     .Where(p => p.TrangThai != "Bảo trì" && p.SucChua >= guestCount);

                if (maLoaiPhong.HasValue && maLoaiPhong.Value > 0)
                {
                    query = query.Where(p => p.MaLoaiPhong == maLoaiPhong.Value);
                }

                var rooms = query.ToList();

                var busyRoomIds = db.DATPHONGs
                    .Where(b => b.TrangThai == "ChoNhan" && start < b.GioKetThucDuKien && end > b.GioBatDauDuKien)
                    .Select(b => b.MaPhong)
                    .Distinct()
                    .ToList();

                // If schedule covers right now, exclude rooms currently in an active session
                if (start <= now && end >= now)
                {
                    var activeSessionRoomIds = db.PHIENHATs
                        .Where(ph => ph.TrangThai == "DangHat")
                        .Select(ph => ph.MaPhong)
                        .Distinct()
                        .ToList();

                    busyRoomIds = busyRoomIds.Union(activeSessionRoomIds).Distinct().ToList();
                }

                return rooms
                    .Where(p => !busyRoomIds.Contains(p.MaPhong))
                    .OrderBy(p => p.MaPhong)
                    .Select(p => new AvailableRoomRow
                    {
                        MaPhong = p.MaPhong,
                        TenPhong = p.TenPhong,
                        MaLoaiPhong = p.MaLoaiPhong,
                        TenLoaiPhong = p.LOAIPHONG.TenLoaiPhong,
                        DonGiaGio = p.LOAIPHONG.DonGiaGio,
                        SucChua = p.SucChua,
                        TrangThai = p.TrangThai
                    })
                    .ToList();
            }
        }

        public int CreateBooking(string maPhong, string tenKhach, string sdt, int soKhach, DateTime start, DateTime end, string ghiChu)
        {
            ValidateInput(maPhong, tenKhach, sdt, soKhach, start, end, ghiChu);

            using (var db = new QuanLyKaraokeEntities())
            using (var tx = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                var staff = AuthService.ValidateCurrentUser(db, "Admin", "NhanVien");

                var room = db.PHONGs.Include("LOAIPHONG").FirstOrDefault(p => p.MaPhong == maPhong);
                if (room == null)
                {
                    throw new InvalidOperationException("Phòng không tồn tại.");
                }
                if (room.TrangThai == "Bảo trì")
                {
                    throw new InvalidOperationException("Phòng đang trong trạng thái bảo trì, không thể đặt.");
                }
                if (soKhach > room.SucChua)
                {
                    throw new InvalidOperationException(string.Format("Số khách ({0}) vượt quá sức chứa tối đa ({1} người) của phòng {2}.", soKhach, room.SucChua, room.TenPhong));
                }

                bool overlap = db.DATPHONGs.Any(b =>
                    b.MaPhong == maPhong &&
                    b.TrangThai == "ChoNhan" &&
                    start < b.GioKetThucDuKien &&
                    end > b.GioBatDauDuKien
                );

                if (overlap)
                {
                    throw new InvalidOperationException("Phòng đã có lịch đặt khác trong khoảng thời gian này.");
                }

                decimal donGiaGioDaChot = room.LOAIPHONG.DonGiaGio;

                var booking = new DATPHONG
                {
                    MaPhong = maPhong,
                    TenKhach = tenKhach.Trim(),
                    SDT = sdt.Trim(),
                    SoKhach = soKhach,
                    GioBatDauDuKien = start,
                    GioKetThucDuKien = end,
                    DonGiaGioDaChot = donGiaGioDaChot,
                    TrangThai = "ChoNhan",
                    MaNV = staff.MaNV,
                    GhiChu = string.IsNullOrWhiteSpace(ghiChu) ? null : ghiChu.Trim()
                };

                db.DATPHONGs.Add(booking);
                db.SaveChanges();
                tx.Commit();

                return booking.MaDatPhong;
            }
        }

        public void EditBooking(int maDatPhong, string maPhong, string tenKhach, string sdt, int soKhach, DateTime start, DateTime end, string ghiChu)
        {
            ValidateInput(maPhong, tenKhach, sdt, soKhach, start, end, ghiChu);

            using (var db = new QuanLyKaraokeEntities())
            using (var tx = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                var staff = AuthService.ValidateCurrentUser(db, "Admin", "NhanVien");

                var booking = db.DATPHONGs.FirstOrDefault(b => b.MaDatPhong == maDatPhong);
                if (booking == null)
                {
                    throw new InvalidOperationException("Phiếu đặt phòng không tồn tại.");
                }

                if (booking.TrangThai != "ChoNhan")
                {
                    throw new InvalidOperationException(string.Format("Không thể chỉnh sửa phiếu đặt phòng đã ở trạng thái '{0}'. Chỉ cho phép sửa phiếu đang 'ChoNhan'.", booking.TrangThai));
                }

                var room = db.PHONGs.Include("LOAIPHONG").FirstOrDefault(p => p.MaPhong == maPhong);
                if (room == null)
                {
                    throw new InvalidOperationException("Phòng không tồn tại.");
                }
                if (room.TrangThai == "Bảo trì")
                {
                    throw new InvalidOperationException("Phòng đang trong trạng thái bảo trì, không thể đặt.");
                }
                if (soKhach > room.SucChua)
                {
                    throw new InvalidOperationException(string.Format("Số khách ({0}) vượt quá sức chứa tối đa ({1} người) của phòng {2}.", soKhach, room.SucChua, room.TenPhong));
                }

                bool overlap = db.DATPHONGs.Any(b =>
                    b.MaDatPhong != maDatPhong &&
                    b.MaPhong == maPhong &&
                    b.TrangThai == "ChoNhan" &&
                    start < b.GioKetThucDuKien &&
                    end > b.GioBatDauDuKien
                );

                if (overlap)
                {
                    throw new InvalidOperationException("Phòng đã có lịch đặt khác trong khoảng thời gian này.");
                }

                // If room changed, freeze the rate of the new room
                if (booking.MaPhong != maPhong)
                {
                    booking.DonGiaGioDaChot = room.LOAIPHONG.DonGiaGio;
                }

                booking.MaPhong = maPhong;
                booking.TenKhach = tenKhach.Trim();
                booking.SDT = sdt.Trim();
                booking.SoKhach = soKhach;
                booking.GioBatDauDuKien = start;
                booking.GioKetThucDuKien = end;
                booking.MaNV = staff.MaNV;
                booking.GhiChu = string.IsNullOrWhiteSpace(ghiChu) ? null : ghiChu.Trim();

                db.SaveChanges();
                tx.Commit();
            }
        }

        public void CancelBooking(int maDatPhong, string ghiChuHuy = null)
        {
            using (var db = new QuanLyKaraokeEntities())
            using (var tx = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                AuthService.ValidateCurrentUser(db, "Admin", "NhanVien");

                var booking = db.DATPHONGs.FirstOrDefault(b => b.MaDatPhong == maDatPhong);
                if (booking == null)
                {
                    throw new InvalidOperationException("Phiếu đặt phòng không tồn tại.");
                }

                if (booking.TrangThai != "ChoNhan")
                {
                    throw new InvalidOperationException(string.Format("Không thể hủy phiếu đặt phòng đã ở trạng thái '{0}'. Chỉ cho phép hủy phiếu đang 'ChoNhan'.", booking.TrangThai));
                }

                booking.TrangThai = "DaHuy";
                if (!string.IsNullOrWhiteSpace(ghiChuHuy))
                {
                    string extra = "[Hủy lúc " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "]: " + ghiChuHuy.Trim();
                    booking.GhiChu = string.IsNullOrWhiteSpace(booking.GhiChu) ? extra : (booking.GhiChu + "\n" + extra);
                }

                db.SaveChanges();
                tx.Commit();
            }
        }

        public int CheckInBooking(int maDatPhong)
        {
            using (var db = new QuanLyKaraokeEntities())
            using (var tx = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                var staff = AuthService.ValidateCurrentUser(db, "Admin", "NhanVien");

                var booking = db.DATPHONGs.FirstOrDefault(b => b.MaDatPhong == maDatPhong);
                if (booking == null)
                {
                    throw new InvalidOperationException("Phiếu đặt phòng không tồn tại.");
                }

                if (booking.TrangThai != "ChoNhan")
                {
                    throw new InvalidOperationException(string.Format("Không thể nhận phòng. Phiếu đặt hiện ở trạng thái '{0}' (yêu cầu trạng thái 'ChoNhan').", booking.TrangThai));
                }

                DateTime now = DateTime.Now;

                // Explicit arrival window policy:
                // Early check-in allowed up to 30 minutes before GioBatDauDuKien.
                // Cannot check in after GioKetThucDuKien has passed.
                if (now < booking.GioBatDauDuKien.AddMinutes(-30))
                {
                    throw new InvalidOperationException(
                        string.Format("Chưa đến thời gian nhận phòng. Chỉ có thể nhận phòng trước tối đa 30 phút so với giờ bắt đầu dự kiến ({0:HH:mm dd/MM/yyyy}).", booking.GioBatDauDuKien)
                    );
                }

                if (now >= booking.GioKetThucDuKien)
                {
                    throw new InvalidOperationException(
                        string.Format("Lịch đặt phòng đã quá giờ kết thúc dự kiến ({0:HH:mm dd/MM/yyyy}). Không thể nhận phòng.", booking.GioKetThucDuKien)
                    );
                }

                var room = db.PHONGs.FirstOrDefault(p => p.MaPhong == booking.MaPhong);
                if (room == null)
                {
                    throw new InvalidOperationException("Phòng không tồn tại.");
                }

                if (room.TrangThai == "Bảo trì")
                {
                    throw new InvalidOperationException("Phòng đang trong trạng thái bảo trì. Không thể nhận phòng.");
                }

                if (room.TrangThai == "Đang sử dụng")
                {
                    throw new InvalidOperationException("Phòng hiện đang được sử dụng. Vui lòng thanh toán hoặc kết thúc phiên trước.");
                }

                bool hasActiveSession = db.PHIENHATs.Any(ph => ph.MaPhong == booking.MaPhong && ph.TrangThai == "DangHat");
                if (hasActiveSession)
                {
                    throw new InvalidOperationException("Phòng đang có phiên hát chưa kết thúc. Không thể mở phiên mới.");
                }

                // Check if this booking was already assigned to any session
                bool alreadyCheckedIn = db.PHIENHATs.Any(ph => ph.MaDatPhong == booking.MaDatPhong);
                if (alreadyCheckedIn)
                {
                    throw new InvalidOperationException("Phiếu đặt phòng này đã được tạo phiên hát trước đó.");
                }

                // Atomically create PHIENHAT with frozen rate and current staff
                var phienHat = new PHIENHAT
                {
                    MaPhong = booking.MaPhong,
                    MaNV = staff.MaNV,
                    GioBatDau = now,
                    GioKetThuc = null,
                    DonGiaGio = booking.DonGiaGioDaChot,
                    TrangThai = "DangHat",
                    MaDatPhong = booking.MaDatPhong
                };

                db.PHIENHATs.Add(phienHat);
                booking.TrangThai = "DaNhan";
                room.TrangThai = "Đang sử dụng";

                db.SaveChanges();
                tx.Commit();

                return phienHat.MaPhien;
            }
        }

        private static void ValidateInput(string maPhong, string tenKhach, string sdt, int soKhach, DateTime start, DateTime end, string ghiChu)
        {
            if (string.IsNullOrWhiteSpace(maPhong) || maPhong.Length > 20)
            {
                throw new InvalidOperationException("Mã phòng không hợp lệ.");
            }
            if (string.IsNullOrWhiteSpace(tenKhach) || tenKhach.Trim().Length > 100)
            {
                throw new InvalidOperationException("Tên khách hàng không được để trống và tối đa 100 ký tự.");
            }
            if (string.IsNullOrWhiteSpace(sdt) || sdt.Trim().Length < 8 || sdt.Trim().Length > 15)
            {
                throw new InvalidOperationException("Số điện thoại không hợp lệ (từ 8 đến 15 ký tự).");
            }
            if (soKhach <= 0)
            {
                throw new InvalidOperationException("Số khách phải lớn hơn 0.");
            }
            if (end <= start)
            {
                throw new InvalidOperationException("Giờ kết thúc dự kiến phải sau giờ bắt đầu dự kiến.");
            }
            // Allow 2 minutes grace for clock drift / request lag
            if (start < DateTime.Now.AddMinutes(-2))
            {
                throw new InvalidOperationException("Giờ bắt đầu dự kiến phải ở thời điểm tương lai.");
            }
            if (ghiChu != null && ghiChu.Length > 500)
            {
                throw new InvalidOperationException("Ghi chú tối đa 500 ký tự.");
            }
        }
    }
}
