using System;
using System.Drawing;
using System.Windows.Forms;
using karaoke.Services;
namespace karaoke {
 public partial class FrmQuanLyPhong : Form {
  private readonly RoomService service=new RoomService();
  private string selectedMaPhong;
  private NumericUpDown capacity=new NumericUpDown {Minimum=1,Maximum=1000,Value=10,Location=new Point(330,105),Width=70};
  public FrmQuanLyPhong() { InitializeComponent(); txtLoaiPhong.Parent.Controls.Add(capacity); txtLoaiPhong.Parent.Controls.Add(new Label {Text="Sức chứa",Location=new Point(330,80)}); Load+=FrmQuanLyPhong_Load; dataGridViewPhong.ReadOnly=true;dataGridViewPhong.AllowUserToAddRows=false; }
  private void Run(Action action) {try {action();}catch(Exception ex){MessageBox.Show(ex.GetBaseException().Message,"Thông báo");}}
  private void FrmQuanLyPhong_Load(object sender,EventArgs e) {if(System.ComponentModel.LicenseManager.UsageMode==System.ComponentModel.LicenseUsageMode.Designtime)return; cbTrangThai.Items.Clear();cbTrangThai.Items.AddRange(new object[]{"Trống","Đang sử dụng","Bảo trì"});cbTrangThai.SelectedIndex=0;Run(LoadDanhSachPhong);}
  private void LoadDanhSachPhong() {var rooms=service.List();dataGridViewPhong.DataSource=rooms;while(flowLayoutPanelPhong.Controls.Count>0)flowLayoutPanelPhong.Controls[0].Dispose();foreach(var room in rooms){var b=new Button {Width=140,Height=100,Text=room.TenPhong+"\n"+room.LoaiPhong+"\n"+room.TrangThai,BackColor=room.TrangThai=="Trống"?Color.LightGreen:Color.LightGray};b.Click+=(s,e)=>SelectRoom(room);flowLayoutPanelPhong.Controls.Add(b);}}
  private void SelectRoom(RoomRow r){selectedMaPhong=r.MaPhong;txtMaPhong.Text=r.MaPhong;txtMaPhong.ReadOnly=true;txtTenPhong.Text=r.TenPhong;txtLoaiPhong.Text=r.LoaiPhong;txtDonGia.Text=r.DonGia.ToString("0.##");capacity.Value=Math.Min(capacity.Maximum,r.SucChua);cbTrangThai.SelectedItem=r.TrangThai;}
  private void dataGridViewPhong_CellClick(object s,DataGridViewCellEventArgs e){if(e.RowIndex>=0 && dataGridViewPhong.Rows[e.RowIndex].DataBoundItem is RoomRow r)SelectRoom(r);}
  private void Save(bool create){decimal price;if(!decimal.TryParse(txtDonGia.Text,out price)||price<0)throw new InvalidOperationException("Đơn giá không hợp lệ.");if(!create && selectedMaPhong==null)throw new InvalidOperationException("Chọn phòng cần sửa.");service.Save(create?txtMaPhong.Text.Trim():selectedMaPhong,txtTenPhong.Text.Trim(),txtLoaiPhong.Text.Trim(),price,(int)capacity.Value,Convert.ToString(cbTrangThai.SelectedItem),create);LoadDanhSachPhong();ClearForm();}
  private void btnThem_Click(object s,EventArgs e){Run(()=>Save(true));}
  private void btnSua_Click(object s,EventArgs e){Run(()=>Save(false));}
  private void btnXoa_Click(object s,EventArgs e){if(selectedMaPhong!=null && MessageBox.Show("Xóa phòng đã chọn?","Xác nhận",MessageBoxButtons.YesNo)==DialogResult.Yes)Run(()=>{service.Delete(selectedMaPhong);LoadDanhSachPhong();ClearForm();});}
  private void btnLamMoi_Click(object s,EventArgs e){Run(()=>{ClearForm();LoadDanhSachPhong();});}
  private void ClearForm(){selectedMaPhong=null;txtMaPhong.ReadOnly=false;txtMaPhong.Clear();txtTenPhong.Clear();txtLoaiPhong.Clear();txtDonGia.Clear();capacity.Value=10;cbTrangThai.SelectedIndex=0;}
 }
}
