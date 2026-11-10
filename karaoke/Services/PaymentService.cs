using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using karaoke.Models;
using karaoke.Helpers;

namespace karaoke.Services
{
    /// <summary>
    /// DTO thông tin phiên hát đang hoạt động chờ thanh toán
    /// </summary>
    public class PaymentSessionRow
    {
        public int MaPhien { get; set; }
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public string LoaiPhong { get; set; }
        public decimal DonGiaGio { get; set; }
        public DateTime GioBatDau { get; set; }
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public TimeSpan ThoiGianSuDung { get; set; }
        public decimal TienPhongTamTinh { get; set; }
    }

    /// <summary>
    /// DTO dòng dịch vụ trong hóa đơn
    /// </summary>
    public class InvoiceServiceDetailRow
    {
        public int MaCTDV { get; set; }
        public string MaDV { get; set; }
        public string TenDV { get; set; }
        public string DonViTinh { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        public DateTime ThoiGianGoi { get; set; }
    }

    /// <summary>
    /// DTO tổng hợp chi tiết hóa đơn thanh toán
    /// </summary>
    public class InvoiceBillingDetail
    {
        public int MaPhien { get; set; }
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public string LoaiPhong { get; set; }
        public decimal DonGiaGio { get; set; }
        public DateTime GioBatDau { get; set; }
        public DateTime GioKetThuc { get; set; }
        public TimeSpan ThoiGianHat { get; set; }
        public double TongGioHat { get; set; }
        public decimal TienPhong { get; set; }
        public decimal TienDichVu { get; set; }
        public decimal GiamGia { get; set; }
        public decimal ThanhTien { get; set; }
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public List<InvoiceServiceDetailRow> DanhSachDichVu { get; set; } = new List<InvoiceServiceDetailRow>();
    }

    /// <summary>
    /// DTO tra cứu hóa đơn đã thanh toán
    /// </summary>
    public class InvoiceHistoryRow
    {
        public int MaHD { get; set; }
        public int MaPhien { get; set; }
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public DateTime NgayThanhToan { get; set; }
        public DateTime GioBatDau { get; set; }
        public DateTime GioKetThuc { get; set; }
        public decimal TienPhong { get; set; }
        public decimal TienDichVu { get; set; }
        public decimal GiamGia { get; set; }
        public decimal ThanhTien { get; set; }
        public string PhuongThucTT { get; set; }
        public string MaNV { get; set; }
        public string TenNV { get; set; }
    }

    /// <summary>
    /// Service xử lý nghiệp vụ Thanh toán & Hóa đơn sử dụng Entity Framework & LINQ
    /// </summary>
    public class PaymentService
    {
        /// <summary>
        /// Lấy danh sách các phiên hát đang hoạt động (chưa kết thúc, chưa xuất hóa đơn)
        /// Sử dụng LINQ Join giữa PHIENHAT, PHONG, LOAIPHONG, NHANVIEN
        /// </summary>
        public List<PaymentSessionRow> GetActiveSessions()
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequirePermission(db, "THANHTOAN");
                var now = DateTime.Now;

                // LINQ Join kết nối các bảng dữ liệu
                var query = from ph in db.PHIENHATs
                            join p in db.PHONGs on ph.MaPhong equals p.MaPhong
                            join lp in db.LOAIPHONGs on p.MaLoaiPhong equals lp.MaLoaiPhong
                            join nv in db.NHANVIENs on ph.MaNV equals nv.MaNV into nvGroup
                            from nv in nvGroup.DefaultIfEmpty()
                            where ph.TrangThai == "DangHat" && !db.HOADONs.Any(h => h.MaPhien == ph.MaPhien)
                            orderby ph.GioBatDau ascending
                            select new
                            {
                                ph.MaPhien,
                                ph.MaPhong,
                                p.TenPhong,
                                lp.TenLoaiPhong,
                                ph.DonGiaGio,
                                ph.GioBatDau,
                                ph.MaNV,
                                TenNV = nv != null ? nv.HoTen : ph.MaNV
                            };

                var rawList = query.ToList();

                return rawList.Select(x =>
                {
                    TimeSpan usedTime = now > x.GioBatDau ? (now - x.GioBatDau) : TimeSpan.Zero;
                    // Làm tròn theo giờ hát: tối thiểu 15 phút (0.25h)
                    decimal hours = (decimal)Math.Max(0.25, Math.Round(usedTime.TotalMinutes / 60.0, 2));
                    decimal estimatedRoomCharge = Math.Round(hours * x.DonGiaGio, 0);

                    return new PaymentSessionRow
                    {
                        MaPhien = x.MaPhien,
                        MaPhong = x.MaPhong,
                        TenPhong = x.TenPhong,
                        LoaiPhong = x.TenLoaiPhong,
                        DonGiaGio = x.DonGiaGio,
                        GioBatDau = x.GioBatDau,
                        MaNV = x.MaNV,
                        TenNV = x.TenNV,
                        ThoiGianSuDung = usedTime,
                        TienPhongTamTinh = estimatedRoomCharge
                    };
                }).ToList();
            }
        }

        /// <summary>
        /// Tính toán chi tiết hóa đơn theo phiên hát
        /// Sử dụng LINQ Where, Join, Sum
        /// </summary>
        public InvoiceBillingDetail CalculateBillingDetail(int maPhien, DateTime? thoiDiemThanhToan = null, decimal giamGia = 0m)
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequirePermission(db, "THANHTOAN");
                // LINQ Join lấy thông tin phiên hát và phòng
                var sessionInfo = (from ph in db.PHIENHATs
                                   join p in db.PHONGs on ph.MaPhong equals p.MaPhong
                                   join lp in db.LOAIPHONGs on p.MaLoaiPhong equals lp.MaLoaiPhong
                                   join nv in db.NHANVIENs on ph.MaNV equals nv.MaNV into nvGroup
                                   from nv in nvGroup.DefaultIfEmpty()
                                   where ph.MaPhien == maPhien
                                   select new
                                   {
                                       ph.MaPhien,
                                       ph.MaPhong,
                                       p.TenPhong,
                                       lp.TenLoaiPhong,
                                       ph.DonGiaGio,
                                       ph.GioBatDau,
                                       ph.GioKetThuc,
                                       ph.TrangThai,
                                       ph.MaNV,
                                       TenNV = nv != null ? nv.HoTen : ph.MaNV
                                   }).FirstOrDefault();

                if (sessionInfo == null)
                {
                    throw new InvalidOperationException("Không tìm thấy thông tin phiên hát mã " + maPhien);
                }

                DateTime gioKetThuc = thoiDiemThanhToan ?? sessionInfo.GioKetThuc ?? DateTime.Now;
                if (gioKetThuc < sessionInfo.GioBatDau)
                {
                    gioKetThuc = sessionInfo.GioBatDau;
                }

                TimeSpan duration = gioKetThuc - sessionInfo.GioBatDau;
                // Tính số giờ hát thực tế (làm tròn 2 chữ số thập phân, tối thiểu 15 phút)
                double totalHours = Math.Max(0.25, Math.Round(duration.TotalMinutes / 60.0, 2));
                decimal tienPhong = Math.Round((decimal)totalHours * sessionInfo.DonGiaGio, 0);

                // LINQ Join lấy chi tiết dịch vụ đã gọi của phiên hát
                var serviceItems = (from ct in db.CHITIETDICHVUs
                                    join dv in db.DICHVUs on ct.MaDV equals dv.MaDV
                                    where ct.MaPhien == maPhien
                                    orderby ct.ThoiGianGoi ascending
                                    select new InvoiceServiceDetailRow
                                    {
                                        MaCTDV = ct.MaCTDV,
                                        MaDV = ct.MaDV,
                                        TenDV = dv.TenDV,
                                        DonViTinh = dv.DonViTinh,
                                        SoLuong = ct.SoLuong,
                                        DonGia = ct.DonGia,
                                        ThanhTien = ct.SoLuong * ct.DonGia,
                                        ThoiGianGoi = ct.ThoiGianGoi
                                    }).ToList();

                // Dùng LINQ Sum để tính tổng tiền dịch vụ
                decimal tienDichVu = serviceItems.Sum(x => x.ThanhTien);

                if (giamGia < 0) giamGia = 0;
                decimal subtotal = tienPhong + tienDichVu;
                if (giamGia > subtotal) giamGia = subtotal; // Không giảm quá tổng tiền
                decimal thanhTien = subtotal - giamGia;

                return new InvoiceBillingDetail
                {
                    MaPhien = sessionInfo.MaPhien,
                    MaPhong = sessionInfo.MaPhong,
                    TenPhong = sessionInfo.TenPhong,
                    LoaiPhong = sessionInfo.TenLoaiPhong,
                    DonGiaGio = sessionInfo.DonGiaGio,
                    GioBatDau = sessionInfo.GioBatDau,
                    GioKetThuc = gioKetThuc,
                    ThoiGianHat = duration,
                    TongGioHat = totalHours,
                    TienPhong = tienPhong,
                    TienDichVu = tienDichVu,
                    GiamGia = giamGia,
                    ThanhTien = thanhTien,
                    MaNV = sessionInfo.MaNV,
                    TenNV = sessionInfo.TenNV,
                    DanhSachDichVu = serviceItems
                };
            }
        }

        /// <summary>
        /// Thực hiện thanh toán và lập hóa đơn:
        /// 1. Cập nhật PHIENHAT: GioKetThuc, TrangThai = 'DaKetThuc'
        /// 2. Cập nhật PHONG: TrangThai = 'Trống'
        /// 3. Thêm mới HOADON
        /// Chạy an toàn trong Transaction
        /// </summary>
        public int CompletePayment(int maPhien, decimal giamGia, string phuongThucTT, DateTime? thoiDiemThanhToan = null)
        {
            if (string.IsNullOrWhiteSpace(phuongThucTT))
            {
                phuongThucTT = "Tiền mặt";
            }

            using (var db = new QuanLyKaraokeEntities())
            using (var tx = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                var account = AuthService.RequirePermission(db, "THANHTOAN");
                // Kiểm tra phiên hát
                var phienHat = db.PHIENHATs.FirstOrDefault(p => p.MaPhien == maPhien);
                if (phienHat == null)
                {
                    throw new InvalidOperationException("Không tìm thấy phiên hát cần thanh toán.");
                }

                if (phienHat.TrangThai == "DaKetThuc" || db.HOADONs.Any(h => h.MaPhien == maPhien))
                {
                    throw new InvalidOperationException("Phiên hát này đã được thanh toán trước đó.");
                }

                DateTime ngayTT = thoiDiemThanhToan ?? DateTime.Now;
                if (ngayTT < phienHat.GioBatDau)
                {
                    ngayTT = phienHat.GioBatDau;
                }

                // Tính tiền phòng
                TimeSpan duration = ngayTT - phienHat.GioBatDau;
                double totalHours = Math.Max(0.25, Math.Round(duration.TotalMinutes / 60.0, 2));
                decimal tienPhong = Math.Round((decimal)totalHours * phienHat.DonGiaGio, 0);

                // LINQ Sum tính tiền dịch vụ
                decimal tienDichVu = db.CHITIETDICHVUs
                                       .Where(c => c.MaPhien == maPhien)
                                       .Select(c => (decimal?)(c.SoLuong * c.DonGia))
                                       .Sum() ?? 0m;

                if (giamGia < 0) giamGia = 0m;
                decimal subtotal = tienPhong + tienDichVu;
                if (giamGia > subtotal) giamGia = subtotal;
                decimal thanhTien = subtotal - giamGia;

                // Cập nhật phiên hát
                phienHat.GioKetThuc = ngayTT;
                phienHat.TrangThai = "DaKetThuc";

                // Cập nhật trạng thái phòng về 'Trống'
                var phong = db.PHONGs.FirstOrDefault(p => p.MaPhong == phienHat.MaPhong);
                if (phong != null)
                {
                    phong.TrangThai = "Trống";
                }

                // Lấy mã nhân viên thanh toán (ưu tiên UserSession, nếu không thì lấy MaNV phiên hát hoặc NV001)
                string maNVThanhToan = account.MaNV;

                // Tạo hóa đơn mới
                var hoaDon = new HOADON
                {
                    MaPhien = maPhien,
                    MaNV = maNVThanhToan,
                    NgayThanhToan = ngayTT,
                    TienPhong = tienPhong,
                    TienDichVu = tienDichVu,
                    GiamGia = giamGia,
                    ThanhTien = thanhTien,
                    PhuongThucTT = phuongThucTT
                };

                db.HOADONs.Add(hoaDon);
                db.SaveChanges();
                tx.Commit();

                return hoaDon.MaHD;
            }
        }

        /// <summary>
        /// Lấy lịch sử danh sách hóa đơn theo bộ lọc
        /// Sử dụng LINQ Where, Join, OrderByDescending
        /// </summary>
        public List<InvoiceHistoryRow> GetInvoiceHistory(DateTime? tuNgay = null, DateTime? denNgay = null, string maPhong = null, string tuKhoa = null)
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequirePermission(db, "THANHTOAN");
                var query = from hd in db.HOADONs
                            join ph in db.PHIENHATs on hd.MaPhien equals ph.MaPhien
                            join p in db.PHONGs on ph.MaPhong equals p.MaPhong
                            join nv in db.NHANVIENs on hd.MaNV equals nv.MaNV into nvGroup
                            from nv in nvGroup.DefaultIfEmpty()
                            select new
                            {
                                hd.MaHD,
                                hd.MaPhien,
                                ph.MaPhong,
                                p.TenPhong,
                                hd.NgayThanhToan,
                                ph.GioBatDau,
                                GioKetThuc = ph.GioKetThuc ?? hd.NgayThanhToan,
                                hd.TienPhong,
                                hd.TienDichVu,
                                hd.GiamGia,
                                hd.ThanhTien,
                                hd.PhuongThucTT,
                                hd.MaNV,
                                TenNV = nv != null ? nv.HoTen : hd.MaNV
                            };

                // Lọc theo ngày
                if (tuNgay.HasValue)
                {
                    DateTime start = tuNgay.Value.Date;
                    query = query.Where(x => x.NgayThanhToan >= start);
                }

                if (denNgay.HasValue)
                {
                    DateTime end = denNgay.Value.Date.AddDays(1);
                    query = query.Where(x => x.NgayThanhToan < end);
                }

                // Lọc theo phòng
                if (!string.IsNullOrWhiteSpace(maPhong) && maPhong != "TatCa")
                {
                    query = query.Where(x => x.MaPhong == maPhong);
                }

                // Lọc theo từ khóa (Mã HD, tên phòng, nhân viên)
                if (!string.IsNullOrWhiteSpace(tuKhoa))
                {
                    string kw = tuKhoa.Trim();
                    query = query.Where(x => x.MaHD.ToString().Contains(kw) ||
                                             x.TenPhong.Contains(kw) ||
                                             x.TenNV.Contains(kw));
                }

                var list = query.OrderByDescending(x => x.NgayThanhToan).ToList();

                return list.Select(x => new InvoiceHistoryRow
                {
                    MaHD = x.MaHD,
                    MaPhien = x.MaPhien,
                    MaPhong = x.MaPhong,
                    TenPhong = x.TenPhong,
                    NgayThanhToan = x.NgayThanhToan,
                    GioBatDau = x.GioBatDau,
                    GioKetThuc = x.GioKetThuc,
                    TienPhong = x.TienPhong,
                    TienDichVu = x.TienDichVu,
                    GiamGia = x.GiamGia,
                    ThanhTien = x.ThanhTien,
                    PhuongThucTT = x.PhuongThucTT,
                    MaNV = x.MaNV,
                    TenNV = x.TenNV
                }).ToList();
            }
        }

        /// <summary>
        /// Lấy chi tiết hóa đơn đã lưu để xem hoặc in
        /// </summary>
        public InvoiceBillingDetail GetSavedInvoiceDetail(int maHD)
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequirePermission(db, "THANHTOAN");
                var query = from hd in db.HOADONs
                            join ph in db.PHIENHATs on hd.MaPhien equals ph.MaPhien
                            join p in db.PHONGs on ph.MaPhong equals p.MaPhong
                            join lp in db.LOAIPHONGs on p.MaLoaiPhong equals lp.MaLoaiPhong
                            join nv in db.NHANVIENs on hd.MaNV equals nv.MaNV into nvGroup
                            from nv in nvGroup.DefaultIfEmpty()
                            where hd.MaHD == maHD
                            select new
                            {
                                hd,
                                ph,
                                p.TenPhong,
                                lp.TenLoaiPhong,
                                TenNV = nv != null ? nv.HoTen : hd.MaNV
                            };

                var item = query.FirstOrDefault();
                if (item == null)
                {
                    throw new InvalidOperationException("Không tìm thấy hóa đơn mã " + maHD);
                }

                DateTime end = item.ph.GioKetThuc ?? item.hd.NgayThanhToan;
                TimeSpan duration = end - item.ph.GioBatDau;

                // Chi tiết dịch vụ
                var services = (from ct in db.CHITIETDICHVUs
                                join dv in db.DICHVUs on ct.MaDV equals dv.MaDV
                                where ct.MaPhien == item.ph.MaPhien
                                select new InvoiceServiceDetailRow
                                {
                                    MaCTDV = ct.MaCTDV,
                                    MaDV = ct.MaDV,
                                    TenDV = dv.TenDV,
                                    DonViTinh = dv.DonViTinh,
                                    SoLuong = ct.SoLuong,
                                    DonGia = ct.DonGia,
                                    ThanhTien = ct.SoLuong * ct.DonGia,
                                    ThoiGianGoi = ct.ThoiGianGoi
                                }).ToList();

                return new InvoiceBillingDetail
                {
                    MaPhien = item.ph.MaPhien,
                    MaPhong = item.ph.MaPhong,
                    TenPhong = item.TenPhong,
                    LoaiPhong = item.TenLoaiPhong,
                    DonGiaGio = item.ph.DonGiaGio,
                    GioBatDau = item.ph.GioBatDau,
                    GioKetThuc = end,
                    ThoiGianHat = duration,
                    TongGioHat = Math.Max(0.25, Math.Round(duration.TotalHours, 2)),
                    TienPhong = item.hd.TienPhong,
                    TienDichVu = item.hd.TienDichVu,
                    GiamGia = item.hd.GiamGia,
                    ThanhTien = item.hd.ThanhTien,
                    MaNV = item.hd.MaNV,
                    TenNV = item.TenNV,
                    DanhSachDichVu = services
                };
            }
        }
    }
}
