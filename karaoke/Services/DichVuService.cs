using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using karaoke.Models;
using karaoke.Helpers;

namespace karaoke.Services
{
    public class DichVuRow
    {
        public string MaDV { get; set; }
        public string TenDV { get; set; }
        public string DonViTinh { get; set; }
        public decimal DonGia { get; set; }
        public bool TrangThai { get; set; }
    }

    public class DichVuService
    {
        public List<DichVuRow> List()
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequirePermission(db, "DICHVU_XEM");
                return db.DICHVUs
                    .AsNoTracking()
                    .Select(d => new DichVuRow
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

        public void Save(string maDV, string tenDV, string donViTinh, decimal donGia, bool trangThai, bool create)
        {

            if (string.IsNullOrWhiteSpace(maDV) || maDV.Length > 20 ||
                string.IsNullOrWhiteSpace(tenDV) || tenDV.Length > 100 ||
                string.IsNullOrWhiteSpace(donViTinh) || donGia < 0)
            {
                throw new InvalidOperationException("Vui lòng kiểm tra lại thông tin dịch vụ (Mã, Tên, Đơn vị tính, Đơn giá).");
            }

            using (var db = new QuanLyKaraokeEntities())
            using (var tx = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                AuthService.RequirePermission(db, "DICHVU_SUA");
                var dv = db.DICHVUs.Find(maDV);

                if (create && dv != null)
                    throw new InvalidOperationException("Mã dịch vụ này đã tồn tại.");
                if (!create && dv == null)
                    throw new InvalidOperationException("Dịch vụ không tồn tại trong hệ thống.");

                if (create)
                {
                    dv = new DICHVU { MaDV = maDV };
                    db.DICHVUs.Add(dv);
                }

                dv.TenDV = tenDV;
                dv.DonViTinh = donViTinh;
                dv.DonGia = donGia;
                dv.TrangThai = trangThai;

                db.SaveChanges();
                tx.Commit();
            }
        }

        public void Delete(string maDV)
        {

            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequirePermission(db, "DICHVU_SUA");
                var dv = db.DICHVUs.Find(maDV);
                if (dv == null) return;

                if (db.CHITIETDICHVUs.Any(x => x.MaDV == maDV))
                {
                    throw new InvalidOperationException("Dịch vụ này đã phát sinh lịch sử gọi món trong phiên hát, không thể xóa. Bạn có thể khóa (vô hiệu hóa) trạng thái.");
                }

                db.DICHVUs.Remove(dv);
                db.SaveChanges();
            }
        }
    }
}