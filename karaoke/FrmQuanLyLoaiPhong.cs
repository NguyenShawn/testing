using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using karaoke.Services;
using karaoke.UI;

namespace karaoke
{
    public sealed partial class FrmQuanLyLoaiPhong : Form
    {
        private readonly RoomTypeService service = new RoomTypeService();
        private bool editing;
        public FrmQuanLyLoaiPhong()
        {
            InitializeComponent();
            ButtonActive.RoundButton(btnNew);
            ButtonActive.RoundButton(btnSave);
            ButtonActive.RoundButton(btnDelete);
            ButtonActive.RoundButton(btnReload);
            this.Load += Form_Load;
        }
        private void Form_Load(object sender, EventArgs e) { if (LicenseManager.UsageMode != LicenseUsageMode.Designtime) Reload(); }
        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e) { if (e.RowIndex >= 0) SelectRow(); }
        private void btnNew_Click(object sender, EventArgs e) { New(); }
        private void btnSave_Click(object sender, EventArgs e) { Save(); }
        private void btnDelete_Click(object sender, EventArgs e) { Delete(); }
        private void btnReload_Click(object sender, EventArgs e) { Reload(); }
        private void Run(Action action) { try { action(); } catch (Exception ex) { MessageBox.Show(ex.GetBaseException().Message, "Loại phòng", MessageBoxButtons.OK, MessageBoxIcon.Warning); } }
        private void New() { editing = false; id.ReadOnly = false; id.Clear(); name.Clear(); price.Clear(); grid.ClearSelection(); grid.CurrentCell = null; }
        private void Reload()
        {
            Run(() => {
                grid.DataSource = service.List().Select(x => new { x.MaLoaiPhong, x.TenLoaiPhong, x.DonGiaGio }).ToList();
                if (grid.Columns.Count == 3) { grid.Columns[0].HeaderText = "Mã loại"; grid.Columns[1].HeaderText = "Loại phòng"; grid.Columns[2].HeaderText = "Giá giờ"; grid.Columns[2].DefaultCellStyle.Format = "N0"; }
                New();
            });
        }
        private void SelectRow()
        {
            if (grid.CurrentRow == null) return;
            id.Text = Convert.ToString(grid.CurrentRow.Cells["MaLoaiPhong"].Value);
            name.Text = Convert.ToString(grid.CurrentRow.Cells["TenLoaiPhong"].Value);
            price.Text = Convert.ToString(grid.CurrentRow.Cells["DonGiaGio"].Value);
            id.ReadOnly = true; editing = true;
        }
        private void Save()
        {
            Run(() => {
                int code; decimal value;
                if (!int.TryParse(id.Text, out code) || !decimal.TryParse(price.Text, out value)) throw new ArgumentException("Mã loại và giá giờ phải là số hợp lệ.");
                service.Save(code, name.Text, value, !editing); Reload(); MessageBox.Show("Đã lưu loại phòng. Giá đã chốt trong lịch đặt và phiên hát không thay đổi.");
            });
        }
        private void Delete()
        {
            Run(() => {
                int code;
                if (!editing || !int.TryParse(id.Text, out code)) throw new InvalidOperationException("Vui lòng chọn loại phòng cần xóa.");
                if (MessageBox.Show("Xóa loại phòng " + name.Text + "?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                service.Delete(code); Reload();
            });
        }
    }
}
