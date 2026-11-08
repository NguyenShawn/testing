using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using karaoke.Models;
using karaoke.Helpers;
namespace karaoke.Services {
 public class RoomRow { public string MaPhong {get;set;} public string TenPhong {get;set;} public string LoaiPhong {get;set;} public decimal DonGia {get;set;} public int SucChua {get;set;} public string TrangThai {get;set;} }
 public class RoomService {
  public List<RoomRow> List() { using(var db=new QuanLyKaraokeEntities()) return db.PHONGs.Select(p=>new RoomRow { MaPhong=p.MaPhong,TenPhong=p.TenPhong,LoaiPhong=p.LOAIPHONG.TenLoaiPhong,DonGia=p.LOAIPHONG.DonGiaGio,SucChua=p.SucChua,TrangThai=p.TrangThai }).ToList(); }
  public static void RequireAdmin() { if(!UserSession.IsLoggedIn || UserSession.VaiTro!="Admin") throw new InvalidOperationException("Chỉ Admin được thay đổi danh mục phòng."); }
  public void Save(string id,string name,string type,decimal price,int capacity,string status,bool create) {
   RequireAdmin();
   if(string.IsNullOrWhiteSpace(id)||id.Length>20||string.IsNullOrWhiteSpace(name)||name.Length>50||capacity<1) throw new InvalidOperationException("Kiểm tra mã, tên và sức chứa phòng.");
   using(var db=new QuanLyKaraokeEntities()) using(var tx=db.Database.BeginTransaction(IsolationLevel.Serializable)) {
    var category=db.LOAIPHONGs.FirstOrDefault(x=>x.TenLoaiPhong==type && x.DonGiaGio==price);
    if(category==null) throw new InvalidOperationException("Loại phòng và đơn giá phải khớp danh mục loại phòng đã có.");
    var room=db.PHONGs.Find(id);
    if(create && room!=null) throw new InvalidOperationException("Mã phòng đã tồn tại.");
    if(!create && room==null) throw new InvalidOperationException("Phòng không còn tồn tại.");
    bool active=db.PHIENHATs.Any(x=>x.MaPhong==id && x.TrangThai=="DangHat");
    if((active && status!="Đang sử dụng") || (!active && status=="Đang sử dụng")) throw new InvalidOperationException("Trạng thái đang sử dụng được quản lý qua phiên hát.");
    if(status!="Trống" && status!="Bảo trì" && status!="Đang sử dụng") throw new InvalidOperationException("Trạng thái không hợp lệ.");
    if(create) { room=new PHONG {MaPhong=id}; db.PHONGs.Add(room); }
    room.TenPhong=name;room.MaLoaiPhong=category.MaLoaiPhong;room.SucChua=capacity;room.TrangThai=status;
    db.SaveChanges();tx.Commit();
   }
  }
  public void Delete(string id) { RequireAdmin(); using(var db=new QuanLyKaraokeEntities()) {var room=db.PHONGs.Find(id);if(room==null)return; if(db.PHIENHATs.Any(x=>x.MaPhong==id))throw new InvalidOperationException("Phòng đã có lịch sử phiên hát, không thể xóa.");db.PHONGs.Remove(room);db.SaveChanges();} }
 }
}
