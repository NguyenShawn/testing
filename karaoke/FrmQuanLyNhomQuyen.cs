using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using karaoke.Services;
using karaoke.UI;

namespace karaoke
{
    public sealed partial class FrmQuanLyNhomQuyen : Form
    {
        private readonly RoleService service = new RoleService();
        private int? selected;
        public FrmQuanLyNhomQuyen()
        {
            InitializeComponent();
            ButtonActive.RoundButton(btnAdd); ButtonActive.RoundButton(btnSave); ButtonActive.RoundButton(btnDelete);
            Load += Form_Load;
        }
        private void Form_Load(object sender, EventArgs e) { if (LicenseManager.UsageMode != LicenseUsageMode.Designtime) Reload(); }
        private void Groups_Changed(object sender, EventArgs e) { SelectGroup(); }
        private void btnAdd_Click(object sender, EventArgs e) { NewGroup(); }
        private void btnSave_Click(object sender, EventArgs e) { SaveGroup(); }
        private void btnDelete_Click(object sender, EventArgs e) { DeleteGroup(); }
        private void Run(Action action) { try { action(); } catch (Exception ex) { MessageBox.Show(ex.GetBaseException().Message, "Nhóm quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning); } }
        private void Reload()
        {
            Run(() => { groups.DataSource = service.List(); permissions.Items.Clear(); foreach (var p in service.ListPermissions()) permissions.Items.Add(p); NewGroup(); });
        }
        private void NewGroup()
        {
            groups.ClearSelected(); selected = null; name.Clear(); name.ReadOnly = false; permissions.Enabled = true;
            for (int i = 0; i < permissions.Items.Count; i++) permissions.SetItemChecked(i, false);
        }
        private void SelectGroup()
        {
            var role = groups.SelectedItem as RoleRecord;
            if (role == null) return;
            Run(() => {
                var assigned = service.GetPermissions(role.MaNhom); selected = role.MaNhom; name.Text = role.TenNhom;
                bool manager = string.Equals(role.TenNhom, "Manager", StringComparison.OrdinalIgnoreCase);
                name.ReadOnly = role.LaHeThong || manager;
                permissions.Enabled = manager ? AuthService.IsAdmin(AuthService.RequireCurrentUser()) : !role.LaHeThong;
                for (int i = 0; i < permissions.Items.Count; i++) permissions.SetItemChecked(i, assigned.Contains(((PermissionRecord)permissions.Items[i]).MaQuyen));
            });
        }
        private void SaveGroup()
        {
            Run(() => { service.Save(selected, name.Text, permissions.CheckedItems.Cast<PermissionRecord>().Select(p => p.MaQuyen)); Reload(); MessageBox.Show("Đã lưu nhóm quyền."); });
        }
        private void DeleteGroup()
        {
            Run(() => {
                if (!selected.HasValue) throw new InvalidOperationException("Vui lòng chọn nhóm cần xóa.");
                if (MessageBox.Show("Xóa nhóm " + name.Text + "?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                service.Delete(selected.Value); Reload();
            });
        }
    }
}
