namespace karaoke
{
    partial class FrmTaiKhoanCaNhan
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.header = new System.Windows.Forms.Label();
            this.content = new System.Windows.Forms.Panel();
            this.profileCard = new System.Windows.Forms.Panel();
            this.permissionCard = new System.Windows.Forms.Panel();
            this.passwordCard = new System.Windows.Forms.Panel();
            this.passwordFields = new System.Windows.Forms.TableLayoutPanel();
            this.passwordTitle = new System.Windows.Forms.Label();
            this.currentPasswordLabel = new System.Windows.Forms.Label();
            this.newPasswordLabel = new System.Windows.Forms.Label();
            this.confirmPasswordLabel = new System.Windows.Forms.Label();
            this.txtCurrentPassword = new System.Windows.Forms.TextBox();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.btnOpenChangePassword = new System.Windows.Forms.Button();
            this.lblPasswordStatus = new System.Windows.Forms.Label();
            this.profileTitle = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.lblUsernameValue = new System.Windows.Forms.Label();
            this.employeeLabel = new System.Windows.Forms.Label();
            this.lblEmployeeValue = new System.Windows.Forms.Label();
            this.roleLabel = new System.Windows.Forms.Label();
            this.lblRoleValue = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.permissionTitle = new System.Windows.Forms.Label();
            this.permissionHint = new System.Windows.Forms.Label();
            this.permissions = new System.Windows.Forms.ListBox();
            this.lblEmpty = new System.Windows.Forms.Label();
            this.btnReload = new System.Windows.Forms.Button();
            this.SuspendLayout(); this.content.SuspendLayout(); this.profileCard.SuspendLayout(); this.permissionCard.SuspendLayout(); this.passwordCard.SuspendLayout(); this.passwordFields.SuspendLayout();
            this.header.Name = "header"; this.header.Dock = System.Windows.Forms.DockStyle.Top; this.header.Height = 52;
            this.header.Text = "TÀI KHOẢN"; this.header.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.header.ForeColor = System.Drawing.Color.White;
            this.header.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold); this.header.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0); this.header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.content.Name = "content"; this.content.Dock = System.Windows.Forms.DockStyle.Fill; this.content.AutoScroll = true;
            this.content.AutoScrollMinSize = new System.Drawing.Size(900, 650); this.content.BackColor = System.Drawing.Color.FromArgb(244, 247, 245);
            this.profileCard.Name = "profileCard"; this.profileCard.Location = new System.Drawing.Point(24, 24); this.profileCard.Size = new System.Drawing.Size(378, 382); this.profileCard.BackColor = System.Drawing.Color.White;
            this.permissionCard.Name = "permissionCard"; this.permissionCard.Location = new System.Drawing.Point(420, 24); this.permissionCard.Size = new System.Drawing.Size(455, 382); this.permissionCard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right; this.permissionCard.BackColor = System.Drawing.Color.White;
            this.profileTitle.Text = "Thông tin cá nhân"; this.profileTitle.Location = new System.Drawing.Point(24, 20); this.profileTitle.AutoSize = true; this.profileTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold); this.profileTitle.ForeColor = System.Drawing.Color.FromArgb(20, 55, 42);
            this.nameLabel.Text = "Họ và tên"; this.nameLabel.Location = new System.Drawing.Point(25, 55); this.nameLabel.AutoSize = true; this.nameLabel.ForeColor = System.Drawing.Color.FromArgb(90, 105, 97);
            this.txtName.Name = "txtName"; this.txtName.Location = new System.Drawing.Point(25, 76); this.txtName.Size = new System.Drawing.Size(325, 25); this.txtName.MaxLength = 100; this.txtName.ReadOnly = true;
            this.btnEdit.Name = "btnEdit"; this.btnEdit.Text = "Chỉnh sửa"; this.btnEdit.Location = new System.Drawing.Point(25, 107); this.btnEdit.Size = new System.Drawing.Size(112, 34); this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnEdit.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.btnEdit.ForeColor = System.Drawing.Color.White; this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            this.btnSave.Name = "btnSave"; this.btnSave.Text = "Lưu"; this.btnSave.Location = new System.Drawing.Point(25, 107); this.btnSave.Size = new System.Drawing.Size(90, 34); this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnSave.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.btnSave.ForeColor = System.Drawing.Color.White; this.btnSave.Visible = false; this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnCancel.Name = "btnCancel"; this.btnCancel.Text = "Hủy"; this.btnCancel.Location = new System.Drawing.Point(126, 107); this.btnCancel.Size = new System.Drawing.Size(90, 34); this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnCancel.BackColor = System.Drawing.Color.White; this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(22, 101, 52); this.btnCancel.Visible = false; this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.usernameLabel.Text = "Tên đăng nhập"; this.usernameLabel.Location = new System.Drawing.Point(25, 153); this.usernameLabel.AutoSize = true; this.usernameLabel.ForeColor = System.Drawing.Color.FromArgb(90, 105, 97);
            this.lblUsernameValue.Name = "lblUsernameValue"; this.lblUsernameValue.Location = new System.Drawing.Point(25, 175); this.lblUsernameValue.Size = new System.Drawing.Size(325, 24); this.lblUsernameValue.AutoEllipsis = true; this.lblUsernameValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.employeeLabel.Text = "Mã nhân viên"; this.employeeLabel.Location = new System.Drawing.Point(25, 211); this.employeeLabel.AutoSize = true; this.employeeLabel.ForeColor = System.Drawing.Color.FromArgb(90, 105, 97);
            this.lblEmployeeValue.Name = "lblEmployeeValue"; this.lblEmployeeValue.Location = new System.Drawing.Point(25, 233); this.lblEmployeeValue.Size = new System.Drawing.Size(325, 24); this.lblEmployeeValue.AutoEllipsis = true; this.lblEmployeeValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.roleLabel.Text = "Vai trò"; this.roleLabel.Location = new System.Drawing.Point(25, 267); this.roleLabel.AutoSize = true; this.roleLabel.ForeColor = System.Drawing.Color.FromArgb(90, 105, 97);
            this.lblRoleValue.Name = "lblRoleValue"; this.lblRoleValue.Location = new System.Drawing.Point(25, 289); this.lblRoleValue.Size = new System.Drawing.Size(325, 24); this.lblRoleValue.AutoEllipsis = true; this.lblRoleValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold); this.lblRoleValue.ForeColor = System.Drawing.Color.FromArgb(22, 101, 52);
            this.btnLogout.Name = "btnLogout"; this.btnLogout.Text = "Đăng xuất"; this.btnLogout.Location = new System.Drawing.Point(25, 325); this.btnLogout.Size = new System.Drawing.Size(125, 38); this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnLogout.BackColor = System.Drawing.Color.FromArgb(160, 45, 45); this.btnLogout.ForeColor = System.Drawing.Color.White; this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            this.permissionTitle.Text = "Quyền của bạn"; this.permissionTitle.Location = new System.Drawing.Point(24, 20); this.permissionTitle.AutoSize = true; this.permissionTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold); this.permissionTitle.ForeColor = System.Drawing.Color.FromArgb(20, 55, 42);
            this.permissionHint.Text = "Các chức năng được cấp cho vai trò hiện tại"; this.permissionHint.Location = new System.Drawing.Point(25, 57); this.permissionHint.AutoSize = true; this.permissionHint.ForeColor = System.Drawing.Color.FromArgb(90, 105, 97);
            this.permissions.Name = "permissions"; this.permissions.Location = new System.Drawing.Point(25, 91); this.permissions.Size = new System.Drawing.Size(404, 210); this.permissions.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.permissions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; this.permissions.SelectionMode = System.Windows.Forms.SelectionMode.None; this.permissions.IntegralHeight = false; this.permissions.Font = new System.Drawing.Font("Segoe UI", 10F); this.permissions.ItemHeight = 22;
            this.lblEmpty.Name = "lblEmpty"; this.lblEmpty.Text = "Chưa có quyền chức năng nào được gán."; this.lblEmpty.Location = new System.Drawing.Point(35, 102); this.lblEmpty.AutoSize = true; this.lblEmpty.ForeColor = System.Drawing.Color.FromArgb(90, 105, 97); this.lblEmpty.BackColor = System.Drawing.Color.White;
            this.btnReload.Name = "btnReload"; this.btnReload.Text = "Làm mới quyền"; this.btnReload.Location = new System.Drawing.Point(25, 325); this.btnReload.Size = new System.Drawing.Size(140, 38); this.btnReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnReload.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.btnReload.ForeColor = System.Drawing.Color.White; this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            this.passwordCard.Name = "passwordCard"; this.passwordCard.Location = new System.Drawing.Point(24, 422); this.passwordCard.Size = new System.Drawing.Size(851, 195); this.passwordCard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right; this.passwordCard.BackColor = System.Drawing.Color.White;
            this.passwordTitle.Text = "Đổi mật khẩu"; this.passwordTitle.Location = new System.Drawing.Point(24, 16); this.passwordTitle.AutoSize = true; this.passwordTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold); this.passwordTitle.ForeColor = System.Drawing.Color.FromArgb(20, 55, 42);
            this.passwordFields.Name = "passwordFields"; this.passwordFields.Location = new System.Drawing.Point(22, 52); this.passwordFields.Size = new System.Drawing.Size(805, 64); this.passwordFields.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.passwordFields.ColumnCount = 3; this.passwordFields.RowCount = 2;
            this.passwordFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F)); this.passwordFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F)); this.passwordFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.334F));
            this.passwordFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F)); this.passwordFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.currentPasswordLabel.Text = "Mật khẩu hiện tại"; this.currentPasswordLabel.AutoSize = true; this.currentPasswordLabel.ForeColor = System.Drawing.Color.FromArgb(90, 105, 97);
            this.newPasswordLabel.Text = "Mật khẩu mới (ít nhất 8 ký tự)"; this.newPasswordLabel.AutoSize = true; this.newPasswordLabel.ForeColor = System.Drawing.Color.FromArgb(90, 105, 97);
            this.confirmPasswordLabel.Text = "Nhập lại mật khẩu mới"; this.confirmPasswordLabel.AutoSize = true; this.confirmPasswordLabel.ForeColor = System.Drawing.Color.FromArgb(90, 105, 97);
            this.txtCurrentPassword.Name = "txtCurrentPassword"; this.txtCurrentPassword.Dock = System.Windows.Forms.DockStyle.Top; this.txtCurrentPassword.Margin = new System.Windows.Forms.Padding(3, 0, 16, 0); this.txtCurrentPassword.UseSystemPasswordChar = true; this.txtCurrentPassword.MaxLength = 72;
            this.txtNewPassword.Name = "txtNewPassword"; this.txtNewPassword.Dock = System.Windows.Forms.DockStyle.Top; this.txtNewPassword.Margin = new System.Windows.Forms.Padding(3, 0, 16, 0); this.txtNewPassword.UseSystemPasswordChar = true; this.txtNewPassword.MaxLength = 72;
            this.txtConfirmPassword.Name = "txtConfirmPassword"; this.txtConfirmPassword.Dock = System.Windows.Forms.DockStyle.Top; this.txtConfirmPassword.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0); this.txtConfirmPassword.UseSystemPasswordChar = true; this.txtConfirmPassword.MaxLength = 72;
            this.passwordFields.Controls.Add(this.currentPasswordLabel, 0, 0); this.passwordFields.Controls.Add(this.newPasswordLabel, 1, 0); this.passwordFields.Controls.Add(this.confirmPasswordLabel, 2, 0);
            this.passwordFields.Controls.Add(this.txtCurrentPassword, 0, 1); this.passwordFields.Controls.Add(this.txtNewPassword, 1, 1); this.passwordFields.Controls.Add(this.txtConfirmPassword, 2, 1);
            this.btnChangePassword.Name = "btnChangePassword"; this.btnChangePassword.Text = "Lưu mật khẩu"; this.btnChangePassword.Location = new System.Drawing.Point(25, 134); this.btnChangePassword.Size = new System.Drawing.Size(140, 38); this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnChangePassword.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.btnChangePassword.ForeColor = System.Drawing.Color.White; this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            this.btnOpenChangePassword.Name = "btnOpenChangePassword"; this.btnOpenChangePassword.Text = "Mở cửa sổ đổi mật khẩu"; this.btnOpenChangePassword.Location = new System.Drawing.Point(180, 134); this.btnOpenChangePassword.Size = new System.Drawing.Size(200, 38); this.btnOpenChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnOpenChangePassword.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.btnOpenChangePassword.ForeColor = System.Drawing.Color.White; this.btnOpenChangePassword.Click += new System.EventHandler(this.btnOpenChangePassword_Click);
            this.lblPasswordStatus.Name = "lblPasswordStatus"; this.lblPasswordStatus.Location = new System.Drawing.Point(395, 134); this.lblPasswordStatus.Size = new System.Drawing.Size(425, 38); this.lblPasswordStatus.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right; this.lblPasswordStatus.AutoEllipsis = true; this.lblPasswordStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.passwordCard.Controls.AddRange(new System.Windows.Forms.Control[] { this.passwordTitle, this.passwordFields, this.btnChangePassword, this.btnOpenChangePassword, this.lblPasswordStatus });
            this.profileCard.Controls.AddRange(new System.Windows.Forms.Control[] { this.profileTitle, this.nameLabel, this.txtName, this.btnEdit, this.btnSave, this.btnCancel, this.usernameLabel, this.lblUsernameValue, this.employeeLabel, this.lblEmployeeValue, this.roleLabel, this.lblRoleValue, this.btnLogout });
            this.permissionCard.Controls.AddRange(new System.Windows.Forms.Control[] { this.permissionTitle, this.permissionHint, this.lblEmpty, this.permissions, this.btnReload });
            this.content.Controls.Add(this.profileCard); this.content.Controls.Add(this.permissionCard); this.content.Controls.Add(this.passwordCard);
            this.Controls.Add(this.content); this.Controls.Add(this.header); this.Name = "FrmTaiKhoanCaNhan"; this.Text = "Tài khoản"; this.Font = new System.Drawing.Font("Segoe UI", 9F); this.ClientSize = new System.Drawing.Size(1000, 640); this.BackColor = System.Drawing.Color.White;
            this.passwordFields.ResumeLayout(false); this.passwordFields.PerformLayout(); this.passwordCard.ResumeLayout(false); this.passwordCard.PerformLayout(); this.permissionCard.ResumeLayout(false); this.permissionCard.PerformLayout(); this.profileCard.ResumeLayout(false); this.profileCard.PerformLayout(); this.content.ResumeLayout(false); this.ResumeLayout(false);
        }
        private System.Windows.Forms.Label header, profileTitle, nameLabel, usernameLabel, lblUsernameValue, employeeLabel, lblEmployeeValue, roleLabel, lblRoleValue, permissionTitle, permissionHint, lblEmpty;
        private System.Windows.Forms.Panel content, profileCard, permissionCard, passwordCard;
        private System.Windows.Forms.TableLayoutPanel passwordFields;
        private System.Windows.Forms.Label passwordTitle, currentPasswordLabel, newPasswordLabel, confirmPasswordLabel, lblPasswordStatus;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtCurrentPassword, txtNewPassword, txtConfirmPassword;
        private System.Windows.Forms.Button btnEdit, btnSave, btnCancel, btnLogout, btnReload, btnChangePassword, btnOpenChangePassword;
        private System.Windows.Forms.ListBox permissions;
    }
}
