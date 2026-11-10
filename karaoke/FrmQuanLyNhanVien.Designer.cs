namespace karaoke
{
    partial class FrmQuanLyNhanVien
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.header = new System.Windows.Forms.Label();
            this.fields = new System.Windows.Forms.Panel();
            this.grid = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.TextBox();
            this.name = new System.Windows.Forms.TextBox();
            this.phone = new System.Windows.Forms.TextBox();
            this.position = new System.Windows.Forms.TextBox();
            this.lbl_id = new System.Windows.Forms.Label();
            this.lbl_name = new System.Windows.Forms.Label();
            this.lbl_phone = new System.Windows.Forms.Label();
            this.lbl_position = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.working = new System.Windows.Forms.CheckBox();
            this.col_MaNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_HoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_SDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ChucVu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SuspendLayout();
            this.fields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.header.Name = "header"; this.header.Dock = System.Windows.Forms.DockStyle.Top; this.header.Height = 46; this.header.Text = "QUẢN LÝ NHÂN VIÊN";
            this.header.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.header.ForeColor = System.Drawing.Color.White; this.header.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold); this.header.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0); this.header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fields.Name = "fields"; this.fields.Dock = System.Windows.Forms.DockStyle.Top; this.fields.Height = 170; this.fields.AutoScroll = true; this.fields.AutoScrollMinSize = new System.Drawing.Size(860, 150);
            this.lbl_id.Name = "lbl_id"; this.lbl_id.Text = "Mã nhân viên"; this.lbl_id.AutoSize = true; this.lbl_id.Location = new System.Drawing.Point(18, 12);
            this.id.Name = "id"; this.id.Location = new System.Drawing.Point(18, 34); this.id.Size = new System.Drawing.Size(140, 23);
            this.fields.Controls.Add(this.lbl_id); this.fields.Controls.Add(this.id);
            this.lbl_name.Name = "lbl_name"; this.lbl_name.Text = "Họ tên"; this.lbl_name.AutoSize = true; this.lbl_name.Location = new System.Drawing.Point(170, 12);
            this.name.Name = "name"; this.name.Location = new System.Drawing.Point(170, 34); this.name.Size = new System.Drawing.Size(200, 23);
            this.fields.Controls.Add(this.lbl_name); this.fields.Controls.Add(this.name);
            this.lbl_phone.Name = "lbl_phone"; this.lbl_phone.Text = "Số điện thoại"; this.lbl_phone.AutoSize = true; this.lbl_phone.Location = new System.Drawing.Point(382, 12);
            this.phone.Name = "phone"; this.phone.Location = new System.Drawing.Point(382, 34); this.phone.Size = new System.Drawing.Size(150, 23);
            this.fields.Controls.Add(this.lbl_phone); this.fields.Controls.Add(this.phone);
            this.lbl_position.Name = "lbl_position"; this.lbl_position.Text = "Chức vụ"; this.lbl_position.AutoSize = true; this.lbl_position.Location = new System.Drawing.Point(544, 12);
            this.position.Name = "position"; this.position.Location = new System.Drawing.Point(544, 34); this.position.Size = new System.Drawing.Size(170, 23);
            this.fields.Controls.Add(this.lbl_position); this.fields.Controls.Add(this.position);
            this.working.Name = "working"; this.working.Text = "Đang làm việc"; this.working.Checked = true; this.working.AutoSize = true; this.working.Location = new System.Drawing.Point(728, 36); this.fields.Controls.Add(this.working);
            this.btnNew.Name = "btnNew"; this.btnNew.Text = "Thêm mới"; this.btnNew.Location = new System.Drawing.Point(18, 88); this.btnNew.Size = new System.Drawing.Size(118, 34);
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnNew.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.btnNew.ForeColor = System.Drawing.Color.White; this.btnNew.Click += new System.EventHandler(this.btnNew_Click); this.fields.Controls.Add(this.btnNew);
            this.btnSave.Name = "btnSave"; this.btnSave.Text = "Lưu"; this.btnSave.Location = new System.Drawing.Point(148, 88); this.btnSave.Size = new System.Drawing.Size(118, 34);
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnSave.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.btnSave.ForeColor = System.Drawing.Color.White; this.btnSave.Click += new System.EventHandler(this.btnSave_Click); this.fields.Controls.Add(this.btnSave);
            this.btnReload.Name = "btnReload"; this.btnReload.Text = "Làm mới"; this.btnReload.Location = new System.Drawing.Point(278, 88); this.btnReload.Size = new System.Drawing.Size(118, 34);
            this.btnReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnReload.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.btnReload.ForeColor = System.Drawing.Color.White; this.btnReload.Click += new System.EventHandler(this.btnReload_Click); this.fields.Controls.Add(this.btnReload);
            this.grid.Name = "grid"; this.grid.Dock = System.Windows.Forms.DockStyle.Fill; this.grid.ReadOnly = true; this.grid.AllowUserToAddRows = false; this.grid.MultiSelect = false;
            this.grid.AutoGenerateColumns = false; this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect; this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellClick);
            this.col_MaNV.Name = "MaNV"; this.col_MaNV.DataPropertyName = "MaNV"; this.col_MaNV.HeaderText = "Mã nhân viên"; this.col_MaNV.ReadOnly = true; this.grid.Columns.Add(this.col_MaNV);
            this.col_HoTen.Name = "HoTen"; this.col_HoTen.DataPropertyName = "HoTen"; this.col_HoTen.HeaderText = "Họ tên"; this.col_HoTen.ReadOnly = true; this.grid.Columns.Add(this.col_HoTen);
            this.col_SDT.Name = "SDT"; this.col_SDT.DataPropertyName = "SDT"; this.col_SDT.HeaderText = "Số điện thoại"; this.col_SDT.ReadOnly = true; this.grid.Columns.Add(this.col_SDT);
            this.col_ChucVu.Name = "ChucVu"; this.col_ChucVu.DataPropertyName = "ChucVu"; this.col_ChucVu.HeaderText = "Chức vụ"; this.col_ChucVu.ReadOnly = true; this.grid.Columns.Add(this.col_ChucVu);
            this.col_TrangThai.Name = "TrangThai"; this.col_TrangThai.DataPropertyName = "TrangThai"; this.col_TrangThai.HeaderText = "Trạng thái"; this.col_TrangThai.ReadOnly = true; this.grid.Columns.Add(this.col_TrangThai);
            this.Controls.Add(this.grid); this.Controls.Add(this.fields); this.Controls.Add(this.header);
            this.Name = "FrmQuanLyNhanVien"; this.Text = "Quản lý Nhân viên"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ClientSize = new System.Drawing.Size(900, 560); this.MinimumSize = new System.Drawing.Size(760, 480); this.BackColor = System.Drawing.Color.White;
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit(); this.fields.ResumeLayout(false); this.fields.PerformLayout(); this.ResumeLayout(false);
        }
        private System.Windows.Forms.Label header;
        private System.Windows.Forms.Panel fields;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.TextBox id;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox phone;
        private System.Windows.Forms.TextBox position;
        private System.Windows.Forms.Label lbl_id;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.Label lbl_phone;
        private System.Windows.Forms.Label lbl_position;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.CheckBox working;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MaNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_HoTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_SDT;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_ChucVu;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TrangThai;
    }
}
