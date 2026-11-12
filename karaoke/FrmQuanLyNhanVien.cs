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
        private void New()
        {
            editing = false; id.ReadOnly = false; id.Clear(); name.Clear(); phone.Clear();
            position.SelectedIndex = position.FindStringExact("Nhân viên");
            Position_SelectedIndexChanged(position, EventArgs.Empty);
            working.Checked = true; grid.ClearSelection(); grid.CurrentCell = null;
        }
        private void Reload()
        {
            Run(() => {
                var positions = service.ListPositions();
                grid.DataSource = service.List().Select(x => new { x.MaNV, x.HoTen, x.SDT, x.ChucVu, x.TrangThai }).ToList();
                position.DataSource = positions;
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
            string previousPosition = Convert.ToString(grid.CurrentRow.Cells["ChucVu"].Value).Trim();
            var positions = position.Items.Cast<string>().ToList();
            position.SelectedIndex = positions.FindIndex(p => string.Equals(p, previousPosition, StringComparison.OrdinalIgnoreCase));
            // Recognize the old staff spelling without changing the stored profile on selection.
            if (position.SelectedIndex < 0 && string.Equals(previousPosition, "NhanVien", StringComparison.OrdinalIgnoreCase))
                position.SelectedIndex = positions.FindIndex(p => string.Equals(p, "Nhân viên", StringComparison.OrdinalIgnoreCase));
            Position_SelectedIndexChanged(position, EventArgs.Empty);
            if (position.SelectedIndex < 0 && previousPosition.Length > 0)
                lblPositionHint.Text = "Chức vụ cũ: " + previousPosition + ". Vui lòng chọn lại từ danh sách.";
            working.Checked = Convert.ToString(grid.CurrentRow.Cells["TrangThai"].Value) == "Đang làm việc";
            id.ReadOnly = true; editing = true;
        }
        private void Save()
        {
            Run(() => { service.Save(id.Text, name.Text, phone.Text, Convert.ToString(position.SelectedItem), working.Checked, !editing); Reload(); MessageBox.Show("Đã lưu thông tin nhân viên."); });
        }
        private void Position_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = Convert.ToString(position.SelectedItem);
            btnSave.Enabled = selected.Length > 0 && selected.Length <= 50;
            lblPositionHint.Text = selected.Length > 50
                ? "Chức vụ tối đa 50 ký tự. Vui lòng chọn nhóm quyền có tên ngắn hơn."
                : selected.Length == 0 ? "Vui lòng chọn chức vụ trong danh sách nhóm quyền." : "";
        }
    }
}
