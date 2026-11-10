using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using karaoke.Models;
using karaoke.Services;
using karaoke.UI;

namespace karaoke
{
    public sealed partial class FrmQuanLyNhanVien : Form
    {
        private readonly StaffService service = new StaffService();
        private bool editing;
        public FrmQuanLyNhanVien()
        {
            InitializeComponent();
            ButtonActive.RoundButton(btnNew);
            ButtonActive.RoundButton(btnSave);
            ButtonActive.RoundButton(btnReload);
            this.Load += Form_Load;
        }
        private void Form_Load(object sender, EventArgs e) { if (LicenseManager.UsageMode != LicenseUsageMode.Designtime) Reload(); }
        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e) { if (e.RowIndex >= 0) SelectRow(); }
        private void btnNew_Click(object sender, EventArgs e) { New(); }
        private void btnSave_Click(object sender, EventArgs e) { Save(); }
        private void btnReload_Click(object sender, EventArgs e) { Reload(); }
        private void Run(Action action) { try { action(); } catch (Exception ex) { MessageBox.Show(ex.GetBaseException().Message, "Nhân viên", MessageBoxButtons.OK, MessageBoxIcon.Warning); } }
        private void New() { editing = false; id.ReadOnly = false; id.Clear(); name.Clear(); phone.Clear(); position.Clear(); working.Checked = true; grid.ClearSelection(); grid.CurrentCell = null; }
        private void Reload()
        {
            Run(() => {
                grid.DataSource = service.List().Select(x => new { x.MaNV, x.HoTen, x.SDT, x.ChucVu, x.TrangThai }).ToList();
                var labels = new[] { "Mã nhân viên", "Họ tên", "Số điện thoại", "Chức vụ", "Trạng thái" };
                for (int i = 0; i < labels.Length && i < grid.Columns.Count; i++) grid.Columns[i].HeaderText = labels[i];
                New();
            });
        }
        private void SelectRow()
        {
            if (grid.CurrentRow == null) return;
            id.Text = Convert.ToString(grid.CurrentRow.Cells["MaNV"].Value);
            name.Text = Convert.ToString(grid.CurrentRow.Cells["HoTen"].Value);
            phone.Text = Convert.ToString(grid.CurrentRow.Cells["SDT"].Value);
            position.Text = Convert.ToString(grid.CurrentRow.Cells["ChucVu"].Value);
            working.Checked = Convert.ToString(grid.CurrentRow.Cells["TrangThai"].Value) == "Đang làm việc";
            id.ReadOnly = true; editing = true;
        }
        private void Save()
        {
            Run(() => { service.Save(id.Text, name.Text, phone.Text, position.Text, working.Checked, !editing); Reload(); MessageBox.Show("Đã lưu thông tin nhân viên."); });
        }
    }
}
