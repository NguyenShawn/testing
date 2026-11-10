namespace karaoke
{
    partial class FrmQuanLyLoaiPhong
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
            this.price = new System.Windows.Forms.TextBox();
            this.lbl_id = new System.Windows.Forms.Label();
            this.lbl_name = new System.Windows.Forms.Label();
            this.lbl_price = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.col_MaLoaiPhong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TenLoaiPhong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_DonGiaGio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SuspendLayout();
            this.fields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.header.Name = "header"; this.header.Dock = System.Windows.Forms.DockStyle.Top; this.header.Height = 46; this.header.Text = "QUẢN LÝ LOẠI PHÒNG";
            this.header.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.header.ForeColor = System.Drawing.Color.White; this.header.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold); this.header.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0); this.header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fields.Name = "fields"; this.fields.Dock = System.Windows.Forms.DockStyle.Top; this.fields.Height = 160; this.fields.AutoScroll = true; this.fields.AutoScrollMinSize = new System.Drawing.Size(750, 150);
            this.lbl_id.Name = "lbl_id"; this.lbl_id.Text = "Mã loại (số)"; this.lbl_id.AutoSize = true; this.lbl_id.Location = new System.Drawing.Point(18, 12);
            this.id.Name = "id"; this.id.Location = new System.Drawing.Point(18, 34); this.id.Size = new System.Drawing.Size(125, 23);
            this.fields.Controls.Add(this.lbl_id); this.fields.Controls.Add(this.id);
            this.lbl_name.Name = "lbl_name"; this.lbl_name.Text = "Tên loại phòng"; this.lbl_name.AutoSize = true; this.lbl_name.Location = new System.Drawing.Point(160, 12);
            this.name.Name = "name"; this.name.Location = new System.Drawing.Point(160, 34); this.name.Size = new System.Drawing.Size(220, 23);
            this.fields.Controls.Add(this.lbl_name); this.fields.Controls.Add(this.name);
            this.lbl_price.Name = "lbl_price"; this.lbl_price.Text = "Giá giờ (đ)"; this.lbl_price.AutoSize = true; this.lbl_price.Location = new System.Drawing.Point(400, 12);
            this.price.Name = "price"; this.price.Location = new System.Drawing.Point(400, 34); this.price.Size = new System.Drawing.Size(160, 23);
            this.fields.Controls.Add(this.lbl_price); this.fields.Controls.Add(this.price);
            this.btnNew.Name = "btnNew"; this.btnNew.Text = "Thêm mới"; this.btnNew.Location = new System.Drawing.Point(18, 88); this.btnNew.Size = new System.Drawing.Size(118, 34);
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnNew.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.btnNew.ForeColor = System.Drawing.Color.White; this.btnNew.Click += new System.EventHandler(this.btnNew_Click); this.fields.Controls.Add(this.btnNew);
            this.btnSave.Name = "btnSave"; this.btnSave.Text = "Lưu"; this.btnSave.Location = new System.Drawing.Point(148, 88); this.btnSave.Size = new System.Drawing.Size(118, 34);
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnSave.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.btnSave.ForeColor = System.Drawing.Color.White; this.btnSave.Click += new System.EventHandler(this.btnSave_Click); this.fields.Controls.Add(this.btnSave);
            this.btnDelete.Name = "btnDelete"; this.btnDelete.Text = "Xóa"; this.btnDelete.Location = new System.Drawing.Point(278, 88); this.btnDelete.Size = new System.Drawing.Size(118, 34);
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnDelete.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.btnDelete.ForeColor = System.Drawing.Color.White; this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click); this.fields.Controls.Add(this.btnDelete);
            this.btnReload.Name = "btnReload"; this.btnReload.Text = "Làm mới"; this.btnReload.Location = new System.Drawing.Point(408, 88); this.btnReload.Size = new System.Drawing.Size(118, 34);
            this.btnReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnReload.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.btnReload.ForeColor = System.Drawing.Color.White; this.btnReload.Click += new System.EventHandler(this.btnReload_Click); this.fields.Controls.Add(this.btnReload);
            this.grid.Name = "grid"; this.grid.Dock = System.Windows.Forms.DockStyle.Fill; this.grid.ReadOnly = true; this.grid.AllowUserToAddRows = false; this.grid.MultiSelect = false;
            this.grid.AutoGenerateColumns = false; this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect; this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellClick);
            this.col_MaLoaiPhong.Name = "MaLoaiPhong"; this.col_MaLoaiPhong.DataPropertyName = "MaLoaiPhong"; this.col_MaLoaiPhong.HeaderText = "Mã loại"; this.col_MaLoaiPhong.ReadOnly = true; this.grid.Columns.Add(this.col_MaLoaiPhong);
            this.col_TenLoaiPhong.Name = "TenLoaiPhong"; this.col_TenLoaiPhong.DataPropertyName = "TenLoaiPhong"; this.col_TenLoaiPhong.HeaderText = "Loại phòng"; this.col_TenLoaiPhong.ReadOnly = true; this.grid.Columns.Add(this.col_TenLoaiPhong);
            this.col_DonGiaGio.Name = "DonGiaGio"; this.col_DonGiaGio.DataPropertyName = "DonGiaGio"; this.col_DonGiaGio.HeaderText = "Giá giờ"; this.col_DonGiaGio.ReadOnly = true; this.grid.Columns.Add(this.col_DonGiaGio);
            this.Controls.Add(this.grid); this.Controls.Add(this.fields); this.Controls.Add(this.header);
            this.Name = "FrmQuanLyLoaiPhong"; this.Text = "Quản lý loại phòng"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ClientSize = new System.Drawing.Size(800, 500); this.MinimumSize = new System.Drawing.Size(670, 420); this.BackColor = System.Drawing.Color.White;
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit(); this.fields.ResumeLayout(false); this.fields.PerformLayout(); this.ResumeLayout(false);
        }
        private System.Windows.Forms.Label header;
        private System.Windows.Forms.Panel fields;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.TextBox id;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox price;
        private System.Windows.Forms.Label lbl_id;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.Label lbl_price;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MaLoaiPhong;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TenLoaiPhong;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_DonGiaGio;
    }
}
