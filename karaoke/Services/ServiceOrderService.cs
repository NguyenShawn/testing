using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using karaoke.Models;
using karaoke.Helpers;

namespace karaoke.Services
{
    public class ActiveSessionRow
    {
        public int MaPhien { get; set; }
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public string LoaiPhong { get; set; }
        public decimal DonGiaGio { get; set; }
        public DateTime GioBatDau { get; set; }
        public string MaNV { get; set; }
        public string TenNV { get; set; }
    }

    public class ServiceItemRow
    {
        public string MaDV { get; set; }
        public string TenDV { get; set; }
        public string DonViTinh { get; set; }
        public decimal DonGia { get; set; }
        public bool TrangThai { get; set; }
    }

    public class ServiceOrderRow
    {
        public int MaCTDV { get; set; }
        public int MaPhien { get; set; }
        public string MaDV { get; set; }
        public string TenDV { get; set; }
        public string DonViTinh { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        public DateTime ThoiGianGoi { get; set; }
    }

    public class CartItemInput
    {
        public string MaDV { get; set; }
        public string TenDV { get; set; }
        public string DonViTinh { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien { get { return DonGia * SoLuong; } }
    }

    public class SessionTotalSummary
    {
        public int MaPhien { get; set; }
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public DateTime GioBatDau { get; set; }
        public decimal DonGiaGio { get; set; }
        public decimal TienDichVu { get; set; }
        public int SoLuongMon { get; set; }
        public TimeSpan ThoiGianHat { get; set; }
        public decimal TienPhongTamTinh { get; set; }
        public decimal TongCongTamTinh { get; set; }
    }

    public class ServiceOrderService
    {
        public List<ActiveSessionRow> GetActiveSessions()
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequireCurrentUser(db);
                var query = from ph in db.PHIENHATs
                            join p in db.PHONGs on ph.MaPhong equals p.MaPhong
                            join lp in db.LOAIPHONGs on p.MaLoaiPhong equals lp.MaLoaiPhong
                            join nv in db.NHANVIENs on ph.MaNV equals nv.MaNV into nvGroup
                            from nv in nvGroup.DefaultIfEmpty()
                            where ph.TrangThai == "DangHat" && !db.HOADONs.Any(h => h.MaPhien == ph.MaPhien)
                            orderby ph.GioBatDau descending
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

                var list = query.ToList();
                return list.Select(x => new ActiveSessionRow
                {
                    MaPhien = x.MaPhien,
                    MaPhong = x.MaPhong,
                    TenPhong = x.TenPhong,
                    LoaiPhong = x.TenLoaiPhong,
                    DonGiaGio = x.DonGiaGio,
                    GioBatDau = x.GioBatDau,
                    MaNV = x.MaNV,
                    TenNV = x.TenNV
                }).ToList();
            }
        }

        public List<ServiceItemRow> GetActiveServices(string search = null)
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequireCurrentUser(db);
                var query = db.DICHVUs.Where(d => d.TrangThai == true);

                if (!string.IsNullOrWhiteSpace(search))
                {
                    string keyword = search.Trim();
                    query = query.Where(d => d.TenDV.Contains(keyword) || d.MaDV.Contains(keyword));
                }

                return query.OrderBy(d => d.TenDV)
                            .Select(d => new ServiceItemRow
                            {
                                MaDV = d.MaDV,
                                TenDV = d.TenDV,
                                DonViTinh = d.DonViTinh,
                                DonGia = d.DonGia,
                                TrangThai = d.TrangThai
                            })
                            .ToList();
            }
        }

        public List<ServiceOrderRow> GetSessionOrders(int maPhien)
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequireCurrentUser(db);
                var query = from ct in db.CHITIETDICHVUs
                            join dv in db.DICHVUs on ct.MaDV equals dv.MaDV
                            where ct.MaPhien == maPhien
                            orderby ct.ThoiGianGoi
                            select new
                            {
                                ct.MaCTDV,
                                ct.MaPhien,
                                ct.MaDV,
                                dv.TenDV,
                                dv.DonViTinh,
                                ct.SoLuong,
                                ct.DonGia,
                                ct.ThoiGianGoi
                            };

                var list = query.ToList();
                return list.Select(x => new ServiceOrderRow
                {
                    MaCTDV = x.MaCTDV,
                    MaPhien = x.MaPhien,
                    MaDV = x.MaDV,
                    TenDV = x.TenDV,
                    DonViTinh = x.DonViTinh,
                    SoLuong = x.SoLuong,
                    DonGia = x.DonGia,
                    ThanhTien = x.SoLuong * x.DonGia,
                    ThoiGianGoi = x.ThoiGianGoi
                }).ToList();
            }
        }

        public void OrderServices(int maPhien, List<CartItemInput> cart)
        {
            if (cart == null || cart.Count == 0)
            {
                throw new InvalidOperationException("Giỏ hàng dịch vụ đang trống. Vui lòng chọn ít nhất một dịch vụ.");
            }

            // Reject duplicate MaDV within the submission
            if (cart.GroupBy(c => c.MaDV).Any(g => g.Count() > 1))
            {
                throw new InvalidOperationException("Danh sách dịch vụ gọi món có mặt hàng bị trùng lặp. Vui lòng cộng dồn số lượng.");
            }

            foreach (var item in cart)
            {
                if (string.IsNullOrWhiteSpace(item.MaDV))
                {
                    throw new InvalidOperationException("Mã dịch vụ không hợp lệ.");
                }
                if (item.SoLuong <= 0)
                {
                    throw new InvalidOperationException("Số lượng dịch vụ phải lớn hơn 0.");
                }
            }

            using (var db = new QuanLyKaraokeEntities())
            using (var tx = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                AuthService.RequireCurrentUser(db);

                var phienHat = db.PHIENHATs.FirstOrDefault(ph => ph.MaPhien == maPhien);
                if (phienHat == null || phienHat.TrangThai != "DangHat")
                {
                    throw new InvalidOperationException("Phiên hát không tồn tại hoặc đã kết thúc.");
                }

                bool alreadyBilled = db.HOADONs.Any(h => h.MaPhien == maPhien);
                if (alreadyBilled)
                {
                    throw new InvalidOperationException("Phiên hát này đã được thanh toán hóa đơn. Không thể gọi thêm dịch vụ.");
                }

                DateTime orderTime = DateTime.Now;

                foreach (var item in cart)
                {
                    var service = db.DICHVUs.FirstOrDefault(d => d.MaDV == item.MaDV);
                    if (service == null)
                    {
                        throw new InvalidOperationException(string.Format("Dịch vụ mã '{0}' không tồn tại.", item.MaDV));
                    }

                    if (!service.TrangThai)
                    {
                        throw new InvalidOperationException(string.Format("Dịch vụ '{0}' hiện đã ngưng phục vụ.", service.TenDV));
                    }

                    // Freeze price snapshot from catalog at order time
                    decimal frozenPrice = service.DonGia;

                    var ctdv = new CHITIETDICHVU
                    {
                        MaPhien = maPhien,
                        MaDV = service.MaDV,
                        SoLuong = item.SoLuong,
                        DonGia = frozenPrice,
                        ThoiGianGoi = orderTime
                    };

                    db.CHITIETDICHVUs.Add(ctdv);
                }

                db.SaveChanges();
                tx.Commit();
            }
        }

        public SessionTotalSummary GetSessionTotal(int maPhien)
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequireCurrentUser(db);
                var phienHat = (from ph in db.PHIENHATs
                                join p in db.PHONGs on ph.MaPhong equals p.MaPhong
                                where ph.MaPhien == maPhien
                                select new
                                {
                                    ph.MaPhien,
                                    ph.MaPhong,
                                    p.TenPhong,
                                    ph.GioBatDau,
                                    ph.GioKetThuc,
                                    ph.DonGiaGio,
                                    ph.TrangThai
                                }).FirstOrDefault();

                if (phienHat == null)
                {
                    throw new InvalidOperationException("Không tìm thấy phiên hát.");
                }

                var orders = db.CHITIETDICHVUs
                               .Where(c => c.MaPhien == maPhien)
                               .ToList();

                decimal tienDichVu = orders.Sum(c => c.SoLuong * c.DonGia);
                int soLuongMon = orders.Sum(c => c.SoLuong);

                DateTime now = DateTime.Now;
                DateTime end = phienHat.GioKetThuc ?? now;
                TimeSpan duration = end > phienHat.GioBatDau ? (end - phienHat.GioBatDau) : TimeSpan.Zero;

                // Estimated room charge based on hours used (at least 15 mins block)
                decimal hours = (decimal)Math.Max(0.25, duration.TotalMinutes / 60.0);
                decimal tienPhongTamTinh = Math.Round(hours * phienHat.DonGiaGio, 0);

                return new SessionTotalSummary
                {
                    MaPhien = phienHat.MaPhien,
                    MaPhong = phienHat.MaPhong,
                    TenPhong = phienHat.TenPhong,
                    GioBatDau = phienHat.GioBatDau,
                    DonGiaGio = phienHat.DonGiaGio,
                    TienDichVu = tienDichVu,
                    SoLuongMon = soLuongMon,
                    ThoiGianHat = duration,
                    TienPhongTamTinh = tienPhongTamTinh,
                    TongCongTamTinh = tienPhongTamTinh + tienDichVu
                };
            }
        }
    }
}
