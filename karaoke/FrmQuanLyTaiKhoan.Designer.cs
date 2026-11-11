namespace karaoke
{
    partial class FrmQuanLyTaiKhoan
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.header = new System.Windows.Forms.Label();
            this.fields = new System.Windows.Forms.Panel();
            this.grid = new System.Windows.Forms.DataGridView();
            this.employees = new System.Windows.Forms.ComboBox();
            this.username = new System.Windows.Forms.TextBox();
            this.newPassword = new System.Windows.Forms.TextBox();
            this.adminPassword = new System.Windows.Forms.TextBox();
            this.role = new System.Windows.Forms.ComboBox();
            this.lblEmployees = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblAdminPassword = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnToggle = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnAssign = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.col_MaTK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MaNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_HoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TenDangNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_VaiTro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TrangThai = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.col_NhomQuyen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MaNhom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SuspendLayout(); this.fields.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.header.Name = "header"; this.header.Dock = System.Windows.Forms.DockStyle.Top; this.header.Height = 46; this.header.Text = "QUẢN LÝ TÀI KHOẢN"; this.header.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.header.ForeColor = System.Drawing.Color.White; this.header.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold); this.header.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0); this.header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fields.Name = "fields"; this.fields.Dock = System.Windows.Forms.DockStyle.Top; this.fields.Height = 205; this.fields.AutoScroll = true; this.fields.AutoScrollMinSize = new System.Drawing.Size(900, 185);
            this.lblEmployees.Name = "lblEmployees"; this.lblEmployees.Text = "Nhân viên chưa có tài khoản"; this.lblEmployees.AutoSize = true; this.lblEmployees.Location = new System.Drawing.Point(16, 10);
            this.employees.Name = "employees"; this.employees.Location = new System.Drawing.Point(16, 32); this.employees.Size = new System.Drawing.Size(260, 23); this.fields.Controls.Add(this.lblEmployees); this.fields.Controls.Add(this.employees);
            this.lblUsername.Name = "lblUsername"; this.lblUsername.Text = "Tên đăng nhập"; this.lblUsername.AutoSize = true; this.lblUsername.Location = new System.Drawing.Point(290, 10);
            this.username.Name = "username"; this.username.Location = new System.Drawing.Point(290, 32); this.username.Size = new System.Drawing.Size(180, 23); this.fields.Controls.Add(this.lblUsername); this.fields.Controls.Add(this.username);
            this.lblPassword.Name = "lblPassword"; this.lblPassword.Text = "Mật khẩu mới"; this.lblPassword.AutoSize = true; this.lblPassword.Location = new System.Drawing.Point(485, 10);
            this.newPassword.Name = "newPassword"; this.newPassword.Location = new System.Drawing.Point(485, 32); this.newPassword.Size = new System.Drawing.Size(180, 23); this.fields.Controls.Add(this.lblPassword); this.fields.Controls.Add(this.newPassword);
            this.lblAdminPassword.Name = "lblAdminPassword"; this.lblAdminPassword.Text = "Mật khẩu của bạn (để đặt lại)"; this.lblAdminPassword.AutoSize = true; this.lblAdminPassword.Location = new System.Drawing.Point(680, 10);
            this.adminPassword.Name = "adminPassword"; this.adminPassword.Location = new System.Drawing.Point(680, 32); this.adminPassword.Size = new System.Drawing.Size(205, 23); this.fields.Controls.Add(this.lblAdminPassword); this.fields.Controls.Add(this.adminPassword);
            this.employees.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.employees.DisplayMember = "HoTen"; this.employees.ValueMember = "MaNV";
            this.newPassword.UseSystemPasswordChar = true; this.adminPassword.UseSystemPasswordChar = true;
            this.lblRole.Name = "lblRole"; this.lblRole.Text = "Nhóm quyền"; this.lblRole.AutoSize = true; this.lblRole.Location = new System.Drawing.Point(16, 136); this.fields.Controls.Add(this.lblRole);
            this.role.Name = "role"; this.role.Location = new System.Drawing.Point(16, 158); this.role.Size = new System.Drawing.Size(170, 23); this.role.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.role.DisplayMember = "TenNhom"; this.role.ValueMember = "MaNhom"; this.fields.Controls.Add(this.role);
            this.btnCreate.Name = "btnCreate"; this.btnCreate.Text = "Tạo tài khoản"; this.btnCreate.Location = new System.Drawing.Point(16, 91); this.btnCreate.Size = new System.Drawing.Size(125, 34); this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnCreate.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.btnCreate.ForeColor = System.Drawing.Color.White; this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click); this.fields.Controls.Add(this.btnCreate);
            this.btnToggle.Name = "btnToggle"; this.btnToggle.Text = "Khóa / Mở khóa"; this.btnToggle.Location = new System.Drawing.Point(153, 91); this.btnToggle.Size = new System.Drawing.Size(145, 34); this.btnToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnToggle.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.btnToggle.ForeColor = System.Drawing.Color.White; this.btnToggle.Click += new System.EventHandler(this.btnToggle_Click); this.fields.Controls.Add(this.btnToggle);
            this.btnReset.Name = "btnReset"; this.btnReset.Text = "Đặt lại mật khẩu"; this.btnReset.Location = new System.Drawing.Point(310, 91); this.btnReset.Size = new System.Drawing.Size(145, 34); this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnReset.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.btnReset.ForeColor = System.Drawing.Color.White; this.btnReset.Click += new System.EventHandler(this.btnReset_Click); this.fields.Controls.Add(this.btnReset);
            this.btnRefresh.Name = "btnRefresh"; this.btnRefresh.Text = "Làm mới"; this.btnRefresh.Location = new System.Drawing.Point(467, 91); this.btnRefresh.Size = new System.Drawing.Size(95, 34); this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.btnRefresh.ForeColor = System.Drawing.Color.White; this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click); this.fields.Controls.Add(this.btnRefresh);
            this.btnAssign.Name = "btnAssign"; this.btnAssign.Text = "Gán nhóm"; this.btnAssign.Location = new System.Drawing.Point(198, 151); this.btnAssign.Size = new System.Drawing.Size(110, 34); this.btnAssign.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnAssign.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.btnAssign.ForeColor = System.Drawing.Color.White; this.btnAssign.Click += new System.EventHandler(this.btnAssign_Click); this.fields.Controls.Add(this.btnAssign);
            this.btnDelete.Name = "btnDelete"; this.btnDelete.Text = "Xóa tài khoản"; this.btnDelete.Location = new System.Drawing.Point(580, 91); this.btnDelete.Size = new System.Drawing.Size(130, 34); this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnDelete.BackColor = System.Drawing.Color.FromArgb(160, 45, 45); this.btnDelete.ForeColor = System.Drawing.Color.White; this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click); this.fields.Controls.Add(this.btnDelete);
            this.grid.Name = "grid"; this.grid.Dock = System.Windows.Forms.DockStyle.Fill; this.grid.ReadOnly = true; this.grid.AllowUserToAddRows = false; this.grid.MultiSelect = false; this.grid.AutoGenerateColumns = false;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect; this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill; this.grid.CurrentCellChanged += new System.EventHandler(this.Grid_CurrentCellChanged);
            this.col_MaTK.Name = "MaTK"; this.col_MaTK.DataPropertyName = "MaTK"; this.col_MaTK.HeaderText = "Mã TK"; this.col_MaTK.ReadOnly = true; this.grid.Columns.Add(this.col_MaTK);
            this.col_MaNV.Name = "MaNV"; this.col_MaNV.DataPropertyName = "MaNV"; this.col_MaNV.HeaderText = "Mã NV"; this.col_MaNV.ReadOnly = true; this.grid.Columns.Add(this.col_MaNV);
            this.col_HoTen.Name = "HoTen"; this.col_HoTen.DataPropertyName = "HoTen"; this.col_HoTen.HeaderText = "Họ tên"; this.col_HoTen.ReadOnly = true; this.grid.Columns.Add(this.col_HoTen);
            this.col_TenDangNhap.Name = "TenDangNhap"; this.col_TenDangNhap.DataPropertyName = "TenDangNhap"; this.col_TenDangNhap.HeaderText = "Tên đăng nhập"; this.col_TenDangNhap.ReadOnly = true; this.grid.Columns.Add(this.col_TenDangNhap);
            this.col_VaiTro.Name = "VaiTro"; this.col_VaiTro.DataPropertyName = "VaiTro"; this.col_VaiTro.HeaderText = "Loại tài khoản"; this.col_VaiTro.ReadOnly = true; this.grid.Columns.Add(this.col_VaiTro);
            this.col_TrangThai.Name = "TrangThai"; this.col_TrangThai.DataPropertyName = "TrangThai"; this.col_TrangThai.HeaderText = "Đang hoạt động"; this.col_TrangThai.ReadOnly = true; this.grid.Columns.Add(this.col_TrangThai);
            this.col_NhomQuyen.Name = "NhomQuyen"; this.col_NhomQuyen.DataPropertyName = "NhomQuyen"; this.col_NhomQuyen.HeaderText = "Nhóm quyền"; this.col_NhomQuyen.ReadOnly = true; this.grid.Columns.Add(this.col_NhomQuyen);
            this.col_MaNhom.Name = "MaNhom"; this.col_MaNhom.DataPropertyName = "MaNhom"; this.col_MaNhom.HeaderText = "Mã nhóm"; this.col_MaNhom.ReadOnly = true; this.grid.Columns.Add(this.col_MaNhom);
            this.col_MaNhom.Visible = false;
            this.Controls.Add(this.grid); this.Controls.Add(this.fields); this.Controls.Add(this.header); this.Name = "FrmQuanLyTaiKhoan"; this.Text = "Quản lý tài khoản"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent; this.ClientSize = new System.Drawing.Size(920, 570); this.MinimumSize = new System.Drawing.Size(760, 520); this.BackColor = System.Drawing.Color.White;
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit(); this.fields.ResumeLayout(false); this.fields.PerformLayout(); this.ResumeLayout(false);
        }
        private System.Windows.Forms.Label header;
        private System.Windows.Forms.Panel fields;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.ComboBox employees;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.TextBox newPassword;
        private System.Windows.Forms.TextBox adminPassword;
        private System.Windows.Forms.ComboBox role;
        private System.Windows.Forms.Label lblEmployees;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblAdminPassword;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnToggle;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnAssign;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MaTK;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MaNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_HoTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TenDangNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_VaiTro;
        private System.Windows.Forms.DataGridViewCheckBoxColumn col_TrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_NhomQuyen;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MaNhom;
    }
}
