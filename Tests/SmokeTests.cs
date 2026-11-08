using System;
using System.Linq;
using karaoke.Services;
using karaoke.Helpers;
class SmokeTests {
 static void Main() {
  var svc=new RoomService(); var rooms=svc.List(); if(rooms.Count==0)throw new Exception("No room fixtures");
  UserSession.Clear(); bool denied=false;try {svc.Delete("NO_SUCH_ROOM");}catch(InvalidOperationException){denied=true;}if(!denied)throw new Exception("Permission failure");
  var r=rooms.First();string id="T"+Guid.NewGuid().ToString("N").Substring(0,18);
  UserSession.MaTK=1;UserSession.VaiTro="Admin";
  try {svc.Save(id,"Test room",r.LoaiPhong,r.DonGia,12,"Trống",true);if(!svc.List().Any(x=>x.MaPhong==id&&x.SucChua==12))throw new Exception("Create failed");svc.Save(id,"Updated",r.LoaiPhong,r.DonGia,15,"Bảo trì",false);if(!svc.List().Any(x=>x.MaPhong==id&&x.SucChua==15&&x.TrangThai=="Bảo trì"))throw new Exception("Update failed");}
  finally {svc.Delete(id);UserSession.Clear();}
  if(svc.List().Any(x=>x.MaPhong==id))throw new Exception("Delete failed");
  Console.WriteLine("PASS: room EF read/create/update/delete; anonymous write denied; fixtures cleaned.");
 }
}
