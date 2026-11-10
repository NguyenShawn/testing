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
    public sealed partial class FrmQuanLyTaiKhoan : Form
    {
        private readonly AccountService service = new AccountService();
        public FrmQuanLyTaiKhoan()
        {
            InitializeComponent();
            ButtonActive.RoundButton(btnCreate); ButtonActive.RoundButton(btnToggle); ButtonActive.RoundButton(btnReset); ButtonActive.RoundButton(btnRefresh); ButtonActive.RoundButton(btnAssign);
            Load += Form_Load;
        }
        private void Form_Load(object sender, EventArgs e) { if (LicenseManager.UsageMode != LicenseUsageMode.Designtime) RefreshData(); }
        private void Grid_CurrentCellChanged(object sender, EventArgs e) { SelectCurrentRole(); }
        private void btnCreate_Click(object sender, EventArgs e) { Create(); }
        private void btnToggle_Click(object sender, EventArgs e) { Toggle(); }
        private void btnReset_Click(object sender, EventArgs e) { ResetPassword(); }
        private void btnRefresh_Click(object sender, EventArgs e) { RefreshData(); }
        private void btnAssign_Click(object sender, EventArgs e) { AssignRole(); }
        private void Run(Action action)
        {
            try { action(); }
            catch (Exception ex) { MessageBox.Show(ex.GetBaseException().Message, "Quản lý tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        private AccountRow Selected()
        {
            var row = grid.CurrentRow == null ? null : grid.CurrentRow.DataBoundItem as AccountRow;
            if (row == null) throw new InvalidOperationException("Vui lòng chọn tài khoản nhân viên.");
            return row;
        }
        private void RefreshData()
        {
            Run(() => {
                grid.DataSource = service.List(); employees.DataSource = service.AvailableStaff();
                role.DataSource = new RoleService().ListForAssignment();
                grid.Columns["MaTK"].HeaderText = "Mã TK"; grid.Columns["MaNV"].HeaderText = "Mã NV";
                grid.Columns["HoTen"].HeaderText = "Họ tên"; grid.Columns["TenDangNhap"].HeaderText = "Tên đăng nhập";
                grid.Columns["VaiTro"].HeaderText = "Loại tài khoản"; grid.Columns["TrangThai"].HeaderText = "Đang hoạt động";
                grid.Columns["NhomQuyen"].HeaderText = "Nhóm quyền"; grid.Columns["MaNhom"].Visible = false;
                grid.ClearSelection(); grid.CurrentCell = null;
            });
        }
        private void Create()
        {
            Run(() => {
                var staff = employees.SelectedItem as NHANVIEN;
                if (staff == null) throw new InvalidOperationException("Vui lòng chọn nhân viên chưa có tài khoản.");
                service.CreateStaffAccount(staff.MaNV, username.Text, newPassword.Text);
                username.Clear(); newPassword.Clear(); RefreshData(); MessageBox.Show("Đã tạo tài khoản nhân viên.");
            });
        }
        private void Toggle()
        {
            Run(() => {
                var row = Selected();
                var action = row.TrangThai ? "khóa" : "mở khóa";
                if (MessageBox.Show("Bạn muốn " + action + " tài khoản " + row.TenDangNhap + "?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                service.SetStaffStatus(row.MaTK, !row.TrangThai); RefreshData();
            });
        }
        private void ResetPassword()
        {
            Run(() => {
                var row = Selected();
                if (MessageBox.Show("Đặt lại mật khẩu cho " + row.TenDangNhap + "?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                service.ResetStaffPassword(row.MaTK, adminPassword.Text, newPassword.Text);
                adminPassword.Clear(); newPassword.Clear(); MessageBox.Show("Đã đặt lại mật khẩu.");
            });
        }
        private void AssignRole()
        {
            Run(() => {
                var account = Selected(); var selectedRole = role.SelectedItem as RoleRecord;
                if (selectedRole == null) throw new InvalidOperationException("Vui lòng chọn nhóm quyền.");
                if (MessageBox.Show("Gán nhóm " + selectedRole.TenNhom + " cho " + account.TenDangNhap + "?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                new RoleService().AssignStaffRole(account.MaTK, selectedRole.MaNhom);
                RefreshData();
                MessageBox.Show("Đã gán nhóm. Nhân viên cần đăng nhập lại.");
            });
        }
        private void SelectCurrentRole()
        {
            var account = grid.CurrentRow == null ? null : grid.CurrentRow.DataBoundItem as AccountRow;
            if (account == null || !account.MaNhom.HasValue || account.VaiTro == "Admin") { role.SelectedIndex = -1; return; }
            role.SelectedValue = account.MaNhom.Value;
        }
    }
}
