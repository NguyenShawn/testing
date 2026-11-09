using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using karaoke.Models;
using karaoke.Helpers;

namespace karaoke.Services
{
    public class RoomRow
    {
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public string LoaiPhong { get; set; }
        public decimal DonGia { get; set; }
        public int SucChua { get; set; }
        public string TrangThai { get; set; }
    }

    public class RoomService
    {
        public List<RoomRow> List()
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequireCurrentUser(db);
                return db.PHONGs
                    .AsNoTracking()
                    .Select(p => new RoomRow
                    {
                        MaPhong = p.MaPhong,
                        TenPhong = p.TenPhong,
                        LoaiPhong = p.LOAIPHONG.TenLoaiPhong,
                        DonGia = p.LOAIPHONG.DonGiaGio,
                        SucChua = p.SucChua,
                        TrangThai = p.TrangThai
                    })
                    .ToList();
            }
        }

        public void Save(string id, string name, string type, decimal price, int capacity, string status, bool create)
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequireAdmin(db);
                var category = db.LOAIPHONGs.FirstOrDefault(x => x.TenLoaiPhong == type && x.DonGiaGio == price);
                if (category == null)
                    throw new InvalidOperationException("Loại phòng và đơn giá phải khớp danh mục loại phòng đã có.");

                Save(id, name, category.MaLoaiPhong, capacity, status, create);
            }
        }

        public void Save(string id, string name, int maLoaiPhong, int capacity, string status, bool create)
        {

            if (string.IsNullOrWhiteSpace(id) || id.Length > 20 ||
                string.IsNullOrWhiteSpace(name) || name.Length > 50 || capacity < 1)
            {
                throw new InvalidOperationException("Kiểm tra mã, tên và sức chứa phòng.");
            }

            using (var db = new QuanLyKaraokeEntities())
            using (var tx = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                AuthService.RequireAdmin(db);
                var category = db.LOAIPHONGs.Find(maLoaiPhong);
                if (category == null)
                    throw new InvalidOperationException("Loại phòng không tồn tại trong hệ thống.");

                var room = db.PHONGs.Find(id);
                if (create && room != null)
                    throw new InvalidOperationException("Mã phòng đã tồn tại.");
                if (!create && room == null)
                    throw new InvalidOperationException("Phòng không còn tồn tại.");

                bool active = db.PHIENHATs.Any(x => x.MaPhong == id && x.TrangThai == "DangHat");
                if ((active && status != "Đang sử dụng") || (!active && status == "Đang sử dụng"))
                    throw new InvalidOperationException("Trạng thái đang sử dụng được quản lý qua phiên hát.");

                if (status != "Trống" && status != "Bảo trì" && status != "Đang sử dụng")
                    throw new InvalidOperationException("Trạng thái không hợp lệ.");

                if (create)
                {
                    room = new PHONG { MaPhong = id };
                    db.PHONGs.Add(room);
                }

                if (status == "Bảo trì" && db.DATPHONGs.Any(x => x.MaPhong == id && x.TrangThai == "ChoNhan"))
                    throw new InvalidOperationException("Phòng còn lịch đặt chưa nhận; hãy xử lý lịch đặt trước khi bảo trì.");
                room.TenPhong = name;
                room.MaLoaiPhong = category.MaLoaiPhong;
                room.SucChua = capacity;
                room.TrangThai = status;

                db.SaveChanges();
                tx.Commit();
            }
        }

        public void Delete(string id)
        {
            using (var db = new QuanLyKaraokeEntities())
            {
                AuthService.RequireAdmin(db);
                var room = db.PHONGs.Find(id);
                if (room == null) return;

                if (db.PHIENHATs.Any(x => x.MaPhong == id) || db.DATPHONGs.Any(x => x.MaPhong == id))
                    throw new InvalidOperationException("Phòng đã có lịch sử phiên hát, không thể xóa.");

                db.PHONGs.Remove(room);
                db.SaveChanges();
            }
        }
    }
}
