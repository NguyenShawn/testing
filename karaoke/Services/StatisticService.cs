using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using karaoke.Models;

namespace karaoke.Services
{
    /// <summary>
    /// DTO tổng hợp chỉ số KPI doanh thu
    /// </summary>
    public class StatisticKPIDto
    {
        public decimal TongDoanhThu { get; set; }
        public decimal TongTienPhong { get; set; }
        public decimal TongTienDichVu { get; set; }
        public decimal TongGiamGia { get; set; }
        public int TongSoHoaDon { get; set; }
        public decimal DoanhThuTrungBinh { get; set; }
    }

    /// <summary>
    /// DTO dữ liệu biểu đồ doanh thu theo thời gian
    /// </summary>
    public class RevenueTimePointDto
    {
        public string ThoiGianNhan { get; set; }
        public DateTime Ngay { get; set; }
        public decimal TienPhong { get; set; }
        public decimal TienDichVu { get; set; }
        public decimal TongTien { get; set; }
        public int SoHoaDon { get; set; }
    }

    /// <summary>
    /// DTO thống kê dịch vụ tiêu thụ / bán chạy
    /// </summary>
    public class TopServiceStatisticDto
    {
        public string MaDV { get; set; }
        public string TenDV { get; set; }
        public string DonViTinh { get; set; }
        public int TongSoLuong { get; set; }
        public decimal TongDoanhThu { get; set; }
        public double TiLeDoanhThu { get; set; }
    }

    /// <summary>
    /// DTO thống kê doanh thu theo phòng hát
    /// </summary>
    public class RoomRevenueStatisticDto
    {
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public string LoaiPhong { get; set; }
        public int SoLuotSuDung { get; set; }
        public decimal TongTienPhong { get; set; }
        public decimal TongTienDichVu { get; set; }
        public decimal TongDoanhThu { get; set; }
    }

    /// <summary>
    /// DTO thống kê doanh thu theo nhân viên
    /// </summary>
    public class StaffRevenueStatisticDto
    {
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public string ChucVu { get; set; }
        public int SoHoaDon { get; set; }
        public decimal TongDoanhThu { get; set; }
    }

    /// <summary>
    /// DTO chi tiết hóa đơn trong kỳ báo cáo
    /// </summary>
    public class InvoiceReportItemDto
    {
        public int MaHD { get; set; }
        public DateTime NgayThanhToan { get; set; }
        public string TenPhong { get; set; }
        public DateTime GioBatDau { get; set; }
        public DateTime GioKetThuc { get; set; }
        public double SoGioHat { get; set; }
        public decimal TienPhong { get; set; }
        public decimal TienDichVu { get; set; }
        public decimal GiamGia { get; set; }
        public decimal ThanhTien { get; set; }
        public string PhuongThucTT { get; set; }
        public string NhanVienLap { get; set; }
    }

    /// <summary>
    /// Service phục vụ Thống kê & Báo cáo sử dụng hoàn toàn LINQ to Entities (Entity Framework)
    /// </summary>
    public class StatisticService
    {
        /// <summary>
        /// Lấy các chỉ số KPI tổng hợp trong khoảng thời gian
        /// Sử dụng LINQ Where, Sum, Count, Average
        /// </summary>
        public StatisticKPIDto GetKPIOverview(DateTime tuNgay, DateTime denNgay)
        {
            DateTime start = tuNgay.Date;
            DateTime end = denNgay.Date.AddDays(1);

            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequirePermission(db, "THONGKE");
                // LINQ Where lọc hóa đơn theo khoảng thời gian
                var query = db.HOADONs.Where(h => h.NgayThanhToan >= start && h.NgayThanhToan < end);

                int count = query.Count();
                if (count == 0)
                {
                    return new StatisticKPIDto();
                }

                // Sử dụng các hàm tính tổng LINQ Sum
                decimal tongDoanhThu = query.Select(h => (decimal?)h.ThanhTien).Sum() ?? 0m;
                decimal tongTienPhong = query.Select(h => (decimal?)h.TienPhong).Sum() ?? 0m;
                decimal tongTienDichVu = query.Select(h => (decimal?)h.TienDichVu).Sum() ?? 0m;
                decimal tongGiamGia = query.Select(h => (decimal?)h.GiamGia).Sum() ?? 0m;
                decimal tb = count > 0 ? (tongDoanhThu / count) : 0m;

                return new StatisticKPIDto
                {
                    TongDoanhThu = tongDoanhThu,
                    TongTienPhong = tongTienPhong,
                    TongTienDichVu = tongTienDichVu,
                    TongGiamGia = tongGiamGia,
                    TongSoHoaDon = count,
                    DoanhThuTrungBinh = Math.Round(tb, 0)
                };
            }
        }

        /// <summary>
        /// Thống kê doanh thu theo từng ngày hoặc tháng để vẽ biểu đồ
        /// Sử dụng LINQ Where, GroupBy, Sum
        /// </summary>
        public List<RevenueTimePointDto> GetRevenueChartData(DateTime tuNgay, DateTime denNgay)
        {
            DateTime start = tuNgay.Date;
            DateTime end = denNgay.Date.AddDays(1);
            int totalDays = (int)(denNgay.Date - tuNgay.Date).TotalDays;

            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequirePermission(db, "THONGKE");
                var hoadons = db.HOADONs
                                .Where(h => h.NgayThanhToan >= start && h.NgayThanhToan < end)
                                .Select(h => new
                                {
                                    h.NgayThanhToan,
                                    h.TienPhong,
                                    h.TienDichVu,
                                    h.ThanhTien
                                })
                                .ToList();

                // Nếu khoảng thời gian <= 31 ngày: Thống kê gom theo từng ngày
                if (totalDays <= 31)
                {
                    var groupedByDate = hoadons
                        .GroupBy(h => h.NgayThanhToan.Date)
                        .ToDictionary(g => g.Key, g => new
                        {
                            TienPhong = g.Sum(x => x.TienPhong),
                            TienDichVu = g.Sum(x => x.TienDichVu),
                            TongTien = g.Sum(x => x.ThanhTien),
                            SoHD = g.Count()
                        });

                    var result = new List<RevenueTimePointDto>();
                    for (DateTime cur = start; cur <= denNgay.Date; cur = cur.AddDays(1))
                    {
                        if (groupedByDate.TryGetValue(cur, out var val))
                        {
                            result.Add(new RevenueTimePointDto
                            {
                                ThoiGianNhan = cur.ToString("dd/MM"),
                                Ngay = cur,
                                TienPhong = val.TienPhong,
                                TienDichVu = val.TienDichVu,
                                TongTien = val.TongTien,
                                SoHoaDon = val.SoHD
                            });
                        }
                        else
                        {
                            result.Add(new RevenueTimePointDto
                            {
                                ThoiGianNhan = cur.ToString("dd/MM"),
                                Ngay = cur,
                                TienPhong = 0,
                                TienDichVu = 0,
                                TongTien = 0,
                                SoHoaDon = 0
                            });
                        }
                    }
                    return result;
                }
                else
                {
                    // Khoảng thời gian dài > 31 ngày: Gom theo Tháng/Năm
                    var groupedByMonth = hoadons
                        .GroupBy(h => new { h.NgayThanhToan.Year, h.NgayThanhToan.Month })
                        .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                        .Select(g => new RevenueTimePointDto
                        {
                            ThoiGianNhan = string.Format("T{0:00}/{1}", g.Key.Month, g.Key.Year),
                            Ngay = new DateTime(g.Key.Year, g.Key.Month, 1),
                            TienPhong = g.Sum(x => x.TienPhong),
                            TienDichVu = g.Sum(x => x.TienDichVu),
                            TongTien = g.Sum(x => x.ThanhTien),
                            SoHoaDon = g.Count()
                        })
                        .ToList();

                    return groupedByMonth;
                }
            }
        }

        /// <summary>
        /// Thống kê dịch vụ bán chạy nhất trong khoảng thời gian
        /// Sử dụng LINQ Where, Join giữa HOADON, PHIENHAT, CHITIETDICHVU, DICHVU, GroupBy và Sum
        /// </summary>
        public List<TopServiceStatisticDto> GetTopServices(DateTime tuNgay, DateTime denNgay, int top = 10)
        {
            DateTime start = tuNgay.Date;
            DateTime end = denNgay.Date.AddDays(1);

            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequirePermission(db, "THONGKE");
                // LINQ Join 4 bảng: HOADON -> PHIENHAT -> CHITIETDICHVU -> DICHVU
                var query = from hd in db.HOADONs
                            join ph in db.PHIENHATs on hd.MaPhien equals ph.MaPhien
                            join ct in db.CHITIETDICHVUs on ph.MaPhien equals ct.MaPhien
                            join dv in db.DICHVUs on ct.MaDV equals dv.MaDV
                            where hd.NgayThanhToan >= start && hd.NgayThanhToan < end
                            group new { ct, dv } by new { dv.MaDV, dv.TenDV, dv.DonViTinh } into g
                            select new
                            {
                                MaDV = g.Key.MaDV,
                                TenDV = g.Key.TenDV,
                                DonViTinh = g.Key.DonViTinh,
                                TongSoLuong = g.Sum(x => x.ct.SoLuong),
                                TongDoanhThu = g.Sum(x => x.ct.SoLuong * x.ct.DonGia)
                            };

                var list = query.OrderByDescending(x => x.TongDoanhThu).Take(top).ToList();

                decimal grandTotal = list.Sum(x => x.TongDoanhThu);

                return list.Select(x => new TopServiceStatisticDto
                {
                    MaDV = x.MaDV,
                    TenDV = x.TenDV,
                    DonViTinh = x.DonViTinh,
                    TongSoLuong = x.TongSoLuong,
                    TongDoanhThu = x.TongDoanhThu,
                    TiLeDoanhThu = grandTotal > 0 ? Math.Round((double)(x.TongDoanhThu / grandTotal) * 100, 1) : 0
                }).ToList();
            }
        }

        /// <summary>
        /// Thống kê doanh thu theo phòng hát
        /// Sử dụng LINQ Where, Join giữa HOADON, PHIENHAT, PHONG, LOAIPHONG, GroupBy, Sum
        /// </summary>
        public List<RoomRevenueStatisticDto> GetRevenueByRoom(DateTime tuNgay, DateTime denNgay)
        {
            DateTime start = tuNgay.Date;
            DateTime end = denNgay.Date.AddDays(1);

            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequirePermission(db, "THONGKE");
                // LINQ Join kết nối HOADON, PHIENHAT, PHONG, LOAIPHONG
                var query = from hd in db.HOADONs
                            join ph in db.PHIENHATs on hd.MaPhien equals ph.MaPhien
                            join p in db.PHONGs on ph.MaPhong equals p.MaPhong
                            join lp in db.LOAIPHONGs on p.MaLoaiPhong equals lp.MaLoaiPhong
                            where hd.NgayThanhToan >= start && hd.NgayThanhToan < end
                            group new { hd, ph, p, lp } by new { p.MaPhong, p.TenPhong, lp.TenLoaiPhong } into g
                            select new RoomRevenueStatisticDto
                            {
                                MaPhong = g.Key.MaPhong,
                                TenPhong = g.Key.TenPhong,
                                LoaiPhong = g.Key.TenLoaiPhong,
                                SoLuotSuDung = g.Count(),
                                TongTienPhong = g.Sum(x => x.hd.TienPhong),
                                TongTienDichVu = g.Sum(x => x.hd.TienDichVu),
                                TongDoanhThu = g.Sum(x => x.hd.ThanhTien)
                            };

                return query.OrderByDescending(x => x.TongDoanhThu).ToList();
            }
        }

        /// <summary>
        /// Thống kê doanh thu theo nhân viên thanh toán
        /// Sử dụng LINQ Where, Join, GroupBy, Sum
        /// </summary>
        public List<StaffRevenueStatisticDto> GetRevenueByStaff(DateTime tuNgay, DateTime denNgay)
        {
            DateTime start = tuNgay.Date;
            DateTime end = denNgay.Date.AddDays(1);

            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequirePermission(db, "THONGKE");
                var query = from hd in db.HOADONs
                            join nv in db.NHANVIENs on hd.MaNV equals nv.MaNV
                            where hd.NgayThanhToan >= start && hd.NgayThanhToan < end
                            group new { hd, nv } by new { nv.MaNV, nv.HoTen, nv.ChucVu } into g
                            select new StaffRevenueStatisticDto
                            {
                                MaNV = g.Key.MaNV,
                                TenNV = g.Key.HoTen,
                                ChucVu = g.Key.ChucVu,
                                SoHoaDon = g.Count(),
                                TongDoanhThu = g.Sum(x => x.hd.ThanhTien)
                            };

                return query.OrderByDescending(x => x.TongDoanhThu).ToList();
            }
        }

        /// <summary>
        /// Lấy bảng danh sách chi tiết các hóa đơn trong kỳ báo cáo
        /// Sử dụng LINQ Where, Join, OrderByDescending
        /// </summary>
        public List<InvoiceReportItemDto> GetInvoiceReportList(DateTime tuNgay, DateTime denNgay)
        {
            DateTime start = tuNgay.Date;
            DateTime end = denNgay.Date.AddDays(1);

            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequirePermission(db, "THONGKE");
                var query = from hd in db.HOADONs
                            join ph in db.PHIENHATs on hd.MaPhien equals ph.MaPhien
                            join p in db.PHONGs on ph.MaPhong equals p.MaPhong
                            join nv in db.NHANVIENs on hd.MaNV equals nv.MaNV into nvGroup
                            from nv in nvGroup.DefaultIfEmpty()
                            where hd.NgayThanhToan >= start && hd.NgayThanhToan < end
                            orderby hd.NgayThanhToan descending
                            select new
                            {
                                hd.MaHD,
                                hd.NgayThanhToan,
                                p.TenPhong,
                                ph.GioBatDau,
                                GioKetThuc = ph.GioKetThuc ?? hd.NgayThanhToan,
                                hd.TienPhong,
                                hd.TienDichVu,
                                hd.GiamGia,
                                hd.ThanhTien,
                                hd.PhuongThucTT,
                                TenNV = nv != null ? nv.HoTen : hd.MaNV
                            };

                var list = query.ToList();

                return list.Select(x =>
                {
                    TimeSpan duration = x.GioKetThuc - x.GioBatDau;
                    return new InvoiceReportItemDto
                    {
                        MaHD = x.MaHD,
                        NgayThanhToan = x.NgayThanhToan,
                        TenPhong = x.TenPhong,
                        GioBatDau = x.GioBatDau,
                        GioKetThuc = x.GioKetThuc,
                        SoGioHat = Math.Round(duration.TotalHours, 2),
                        TienPhong = x.TienPhong,
                        TienDichVu = x.TienDichVu,
                        GiamGia = x.GiamGia,
                        ThanhTien = x.ThanhTien,
                        PhuongThucTT = x.PhuongThucTT,
                        NhanVienLap = x.TenNV
                    };
                }).ToList();
            }
        }
    }
}
