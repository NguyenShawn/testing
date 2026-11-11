using System;
using System.ComponentModel;
using System.Windows.Forms;
using karaoke.Services;
using karaoke.UI;

namespace karaoke
{
    public sealed partial class FrmTaiKhoanCaNhan : Form
    {
        private readonly AccountService service = new AccountService();
        private OwnAccountProfile profile;
        public event EventHandler LogoutRequested;
        public event EventHandler ProfileRefreshed;

        public FrmTaiKhoanCaNhan()
        {
            InitializeComponent();
            ButtonActive.RoundButton(btnEdit);
            ButtonActive.RoundButton(btnSave);
            ButtonActive.RoundButton(btnCancel);
            ButtonActive.RoundButton(btnReload);
            ButtonActive.RoundButton(btnLogout);
            ButtonActive.RoundButton(btnChangePassword);
            txtCurrentPassword.TextChanged += PasswordTextChanged;
            txtNewPassword.TextChanged += PasswordTextChanged;
            txtConfirmPassword.TextChanged += PasswordTextChanged;
            Load += Form_Load;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime) LoadProfile();
        }

        private void LoadProfile()
        {
            try
            {
                profile = service.GetOwnProfile();
                txtName.Text = profile.HoTen;
                lblUsernameValue.Text = profile.TenDangNhap;
                lblEmployeeValue.Text = profile.MaNV;
                bool privilegedRole = string.Equals(profile.NhomQuyen, "Admin", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(profile.NhomQuyen, "Manager", StringComparison.OrdinalIgnoreCase);
                lblRoleValue.Text = privilegedRole ? profile.NhomQuyen : "Staff (" + profile.NhomQuyen + ")";
                permissions.Items.Clear();
                foreach (var permission in profile.Quyen) permissions.Items.Add(permission);
                lblEmpty.Visible = permissions.Items.Count == 0;
                SetEditing(false);
                var refreshed = ProfileRefreshed;
                if (refreshed != null) refreshed(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message, "Tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
        }

        private void SetEditing(bool editing)
        {
            txtName.ReadOnly = !editing;
            btnEdit.Visible = !editing;
            btnSave.Visible = editing;
            btnCancel.Visible = editing;
            btnReload.Enabled = !editing;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SetEditing(true);
            txtName.Focus();
            txtName.SelectAll();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtName.Text = profile == null ? "" : profile.HoTen;
            SetEditing(false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                service.UpdateOwnName(txtName.Text);
                LoadProfile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message, "Không thể lưu họ tên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnReload_Click(object sender, EventArgs e) { LoadProfile(); }
        private void PasswordTextChanged(object sender, EventArgs e) { lblPasswordStatus.Text = ""; }
        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            lblPasswordStatus.ForeColor = System.Drawing.Color.FromArgb(160, 45, 45);
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                lblPasswordStatus.Text = "Mật khẩu nhập lại không khớp.";
                txtConfirmPassword.Focus();
                return;
            }
            try
            {
                service.ChangeOwnPassword(txtCurrentPassword.Text, txtNewPassword.Text);
                txtCurrentPassword.Clear();
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                lblPasswordStatus.ForeColor = System.Drawing.Color.FromArgb(22, 101, 52);
                lblPasswordStatus.Text = "Đã đổi mật khẩu thành công.";
            }
            catch (Exception ex)
            {
                lblPasswordStatus.Text = ex.GetBaseException().Message;
            }
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            var handler = LogoutRequested;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
