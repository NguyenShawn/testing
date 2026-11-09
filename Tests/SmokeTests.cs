using System;
using System.Linq;
using karaoke.Services;
using karaoke.Helpers;
class SmokeTests {
 static void Main() {
  var svc=new RoomService();
  UserSession.Clear(); bool denied=false;try {svc.Delete("NO_SUCH_ROOM");}catch(UnauthorizedAccessException){denied=true;}if(!denied)throw new Exception("Permission failure");
  var username=Environment.GetEnvironmentVariable("KARAOKE_DEMO_USER");
  var password=Environment.GetEnvironmentVariable("KARAOKE_DEMO_PASS");
  if(!new AuthService().DangNhap(username,password))throw new Exception("Set KARAOKE_DEMO_USER and KARAOKE_DEMO_PASS to an active account with room read/write/delete permissions.");
  var rooms=svc.List(); if(rooms.Count==0)throw new Exception("No room fixtures");
  var r=rooms.First();string id="T"+Guid.NewGuid().ToString("N").Substring(0,18);
  try {svc.Save(id,"Test room",r.LoaiPhong,r.DonGia,12,"Trống",true);if(!svc.List().Any(x=>x.MaPhong==id&&x.SucChua==12))throw new Exception("Create failed");svc.Save(id,"Updated",r.LoaiPhong,r.DonGia,15,"Bảo trì",false);if(!svc.List().Any(x=>x.MaPhong==id&&x.SucChua==15&&x.TrangThai=="Bảo trì"))throw new Exception("Update failed");}
  finally {svc.Delete(id);UserSession.Clear();}
  if(svc.List().Any(x=>x.MaPhong==id))throw new Exception("Delete failed");
  Console.WriteLine("PASS: room EF read/create/update/delete; anonymous write denied; fixtures cleaned.");
 }
}
