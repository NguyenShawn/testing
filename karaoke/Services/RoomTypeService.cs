using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using karaoke.Models;

namespace karaoke.Services
{
    public sealed class RoomTypeService
    {
        public List<LOAIPHONG> List()
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequirePermission(db, "LOAIPHONG");
                return db.LOAIPHONGs.AsNoTracking().OrderBy(x => x.MaLoaiPhong).ToList();
            }
        }
        public void Save(int id, string name, decimal price, bool create)
        {
            name = (name ?? "").Trim();
            if (id <= 0 || name.Length == 0 || name.Length > 50 || price < 0 || price > 9999999999999999.99m || decimal.Round(price, 2) != price)
                throw new ArgumentException("Kiểm tra mã, tên và đơn giá loại phòng.");
            using (var db = new QuanLyKaraokeEntities())
            using (var tx = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                AuthService.RequirePermission(db, "LOAIPHONG");
                var type = db.LOAIPHONGs.SingleOrDefault(x => x.MaLoaiPhong == id);
                if (create && type != null) throw new InvalidOperationException("Mã loại phòng đã tồn tại.");
                if (!create && type == null) throw new InvalidOperationException("Loại phòng không tồn tại.");
                if (db.LOAIPHONGs.Any(x => x.MaLoaiPhong != id && x.TenLoaiPhong == name)) throw new InvalidOperationException("Tên loại phòng đã tồn tại.");
                if (create) { type = new LOAIPHONG { MaLoaiPhong = id }; db.LOAIPHONGs.Add(type); }
                type.TenLoaiPhong = name; type.DonGiaGio = price;
                db.SaveChanges(); tx.Commit();
            }
        }
        public void Delete(int id)
        {
            using (var db = new QuanLyKaraokeEntities())
            using (var tx = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                AuthService.RequirePermission(db, "LOAIPHONG");
                var type = db.LOAIPHONGs.SingleOrDefault(x => x.MaLoaiPhong == id);
                if (type == null) throw new InvalidOperationException("Loại phòng không tồn tại.");
                if (db.PHONGs.Any(x => x.MaLoaiPhong == id)) throw new InvalidOperationException("Loại phòng đang được sử dụng; không thể xóa.");
                db.LOAIPHONGs.Remove(type); db.SaveChanges(); tx.Commit();
            }
        }
    }
}
