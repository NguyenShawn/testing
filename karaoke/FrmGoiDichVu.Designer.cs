namespace karaoke
{
    partial class FrmGoiDichVu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelTopBar = new System.Windows.Forms.Panel();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.lblSessionDetail = new System.Windows.Forms.Label();
            this.cbPhienHat = new System.Windows.Forms.ComboBox();
            this.labelChonPhong = new System.Windows.Forms.Label();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.panelCatalog = new System.Windows.Forms.Panel();
            this.btnThemVaoGio = new System.Windows.Forms.Button();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.labelSoLuong = new System.Windows.Forms.Label();
            this.dgvMenu = new System.Windows.Forms.DataGridView();
            this.txtTimDichVu = new System.Windows.Forms.TextBox();
            this.labelTimDV = new System.Windows.Forms.Label();
            this.lblCatalogHeader = new System.Windows.Forms.Label();
            this.splitContainerRight = new System.Windows.Forms.SplitContainer();
            this.panelCart = new System.Windows.Forms.Panel();
            this.btnXoaHetGio = new System.Windows.Forms.Button();
            this.btnXoaMonGio = new System.Windows.Forms.Button();
            this.btnXacNhanGoi = new System.Windows.Forms.Button();
            this.lblTongTienGio = new System.Windows.Forms.Label();
            this.dgvGioHang = new System.Windows.Forms.DataGridView();
            this.lblCartHeader = new System.Windows.Forms.Label();
            this.panelOrders = new System.Windows.Forms.Panel();
            this.lblTongCongTamTinh = new System.Windows.Forms.Label();
            this.lblTienPhongTamTinh = new System.Windows.Forms.Label();
            this.lblTongTienDichVu = new System.Windows.Forms.Label();
            this.dgvDaGoi = new System.Windows.Forms.DataGridView();
            this.lblOrdersHeader = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelTopBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.panelCatalog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRight)).BeginInit();
            this.splitContainerRight.Panel1.SuspendLayout();
            this.splitContainerRight.Panel2.SuspendLayout();
            this.splitContainerRight.SuspendLayout();
            this.panelCart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).BeginInit();
            this.panelOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDaGoi)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1040, 48);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(16, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(258, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "GỌI DỊCH VỤ / NƯỚC UỐNG";
            // 
            // panelTopBar
            // 
            this.panelTopBar.BackColor = System.Drawing.Color.White;
            this.panelTopBar.Controls.Add(this.btnLamMoi);
            this.panelTopBar.Controls.Add(this.lblSessionDetail);
            this.panelTopBar.Controls.Add(this.cbPhienHat);
            this.panelTopBar.Controls.Add(this.labelChonPhong);
            this.panelTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopBar.Location = new System.Drawing.Point(0, 48);
            this.panelTopBar.Name = "panelTopBar";
            this.panelTopBar.Size = new System.Drawing.Size(1040, 60);
            this.panelTopBar.TabIndex = 1;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoi.BackColor = System.Drawing.Color.Gainsboro;
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatAppearance.BorderSize = 0;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnLamMoi.Location = new System.Drawing.Point(945, 16);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(80, 28);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // lblSessionDetail
            // 
            this.lblSessionDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right));
            this.lblSessionDetail.AutoEllipsis = true;
            this.lblSessionDetail.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSessionDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblSessionDetail.Location = new System.Drawing.Point(340, 8);
            this.lblSessionDetail.Name = "lblSessionDetail";
            this.lblSessionDetail.Size = new System.Drawing.Size(585, 44);
            this.lblSessionDetail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSessionDetail.TabIndex = 2;
            this.lblSessionDetail.Text = "Chọn phòng đang sử dụng để gọi";
            // 
            // cbPhienHat
            // 
            this.cbPhienHat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPhienHat.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPhienHat.FormattingEnabled = true;
            this.cbPhienHat.Location = new System.Drawing.Point(135, 18);
            this.cbPhienHat.Name = "cbPhienHat";
            this.cbPhienHat.Size = new System.Drawing.Size(190, 25);
            this.cbPhienHat.TabIndex = 1;
            this.cbPhienHat.SelectionChangeCommitted += new System.EventHandler(this.cbPhienHat_SelectionChangeCommitted);
            // 
            // labelChonPhong
            // 
            this.labelChonPhong.AutoSize = true;
            this.labelChonPhong.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelChonPhong.Location = new System.Drawing.Point(15, 21);
            this.labelChonPhong.Name = "labelChonPhong";
            this.labelChonPhong.Size = new System.Drawing.Size(114, 17);
            this.labelChonPhong.TabIndex = 0;
            this.labelChonPhong.Text = "Chọn phòng hát:";
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 108);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.panelCatalog);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainerRight);
            this.splitContainerMain.Size = new System.Drawing.Size(1040, 532);
            this.splitContainerMain.SplitterDistance = 450;
            this.splitContainerMain.TabIndex = 2;
            // 
            // panelCatalog
            // 
            this.panelCatalog.BackColor = System.Drawing.Color.White;
            this.panelCatalog.Controls.Add(this.btnThemVaoGio);
            this.panelCatalog.Controls.Add(this.numSoLuong);
            this.panelCatalog.Controls.Add(this.labelSoLuong);
            this.panelCatalog.Controls.Add(this.dgvMenu);
            this.panelCatalog.Controls.Add(this.txtTimDichVu);
            this.panelCatalog.Controls.Add(this.labelTimDV);
            this.panelCatalog.Controls.Add(this.lblCatalogHeader);
            this.panelCatalog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCatalog.Location = new System.Drawing.Point(0, 0);
            this.panelCatalog.Name = "panelCatalog";
            this.panelCatalog.Size = new System.Drawing.Size(450, 532);
            this.panelCatalog.TabIndex = 0;
            // 
            // btnThemVaoGio
            // 
            this.btnThemVaoGio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemVaoGio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnThemVaoGio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemVaoGio.FlatAppearance.BorderSize = 0;
            this.btnThemVaoGio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemVaoGio.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemVaoGio.ForeColor = System.Drawing.Color.White;
            this.btnThemVaoGio.Location = new System.Drawing.Point(315, 486);
            this.btnThemVaoGio.Name = "btnThemVaoGio";
            this.btnThemVaoGio.Size = new System.Drawing.Size(120, 34);
            this.btnThemVaoGio.TabIndex = 6;
            this.btnThemVaoGio.Text = "+ Thêm vào giỏ";
            this.btnThemVaoGio.UseVisualStyleBackColor = false;
            this.btnThemVaoGio.Click += new System.EventHandler(this.btnThemVaoGio_Click);
            // 
            // numSoLuong
            // 
            this.numSoLuong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numSoLuong.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSoLuong.Location = new System.Drawing.Point(235, 492);
            this.numSoLuong.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.numSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new System.Drawing.Size(70, 25);
            this.numSoLuong.TabIndex = 5;
            this.numSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // labelSoLuong
            // 
            this.labelSoLuong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSoLuong.AutoSize = true;
            this.labelSoLuong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSoLuong.Location = new System.Drawing.Point(172, 496);
            this.labelSoLuong.Name = "labelSoLuong";
            this.labelSoLuong.Size = new System.Drawing.Size(57, 15);
            this.labelSoLuong.TabIndex = 4;
            this.labelSoLuong.Text = "Số lượng:";
            // 
            // dgvMenu
            // 
            this.dgvMenu.AllowUserToAddRows = false;
            this.dgvMenu.AllowUserToDeleteRows = false;
            this.dgvMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMenu.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvMenu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMenu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMenu.Location = new System.Drawing.Point(15, 75);
            this.dgvMenu.MultiSelect = false;
            this.dgvMenu.Name = "dgvMenu";
            this.dgvMenu.ReadOnly = true;
            this.dgvMenu.RowHeadersVisible = false;
            this.dgvMenu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMenu.Size = new System.Drawing.Size(420, 395);
            this.dgvMenu.TabIndex = 3;
            this.dgvMenu.DoubleClick += new System.EventHandler(this.dgvMenu_DoubleClick);
            // 
            // txtTimDichVu
            // 
            this.txtTimDichVu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimDichVu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimDichVu.Location = new System.Drawing.Point(100, 42);
            this.txtTimDichVu.Name = "txtTimDichVu";
            this.txtTimDichVu.Size = new System.Drawing.Size(335, 23);
            this.txtTimDichVu.TabIndex = 2;
            this.txtTimDichVu.TextChanged += new System.EventHandler(this.txtTimDichVu_TextChanged);
            // 
            // labelTimDV
            // 
            this.labelTimDV.AutoSize = true;
            this.labelTimDV.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimDV.Location = new System.Drawing.Point(15, 45);
            this.labelTimDV.Name = "labelTimDV";
            this.labelTimDV.Size = new System.Drawing.Size(59, 15);
            this.labelTimDV.TabIndex = 1;
            this.labelTimDV.Text = "Tìm kiếm:";
            // 
            // lblCatalogHeader
            // 
            this.lblCatalogHeader.AutoSize = true;
            this.lblCatalogHeader.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCatalogHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblCatalogHeader.Location = new System.Drawing.Point(15, 12);
            this.lblCatalogHeader.Name = "lblCatalogHeader";
            this.lblCatalogHeader.Size = new System.Drawing.Size(183, 19);
            this.lblCatalogHeader.TabIndex = 0;
            this.lblCatalogHeader.Text = "1. Menu dịch vụ đang bán";
            // 
            // splitContainerRight
            // 
            this.splitContainerRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRight.Location = new System.Drawing.Point(0, 0);
            this.splitContainerRight.Name = "splitContainerRight";
            this.splitContainerRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerRight.Panel1
            // 
            this.splitContainerRight.Panel1.Controls.Add(this.panelCart);
            // 
            // splitContainerRight.Panel2
            // 
            this.splitContainerRight.Panel2.Controls.Add(this.panelOrders);
            this.splitContainerRight.Size = new System.Drawing.Size(586, 532);
            this.splitContainerRight.SplitterDistance = 250;
            this.splitContainerRight.TabIndex = 0;
            // 
            // panelCart
            // 
            this.panelCart.BackColor = System.Drawing.Color.White;
            this.panelCart.Controls.Add(this.btnXoaHetGio);
            this.panelCart.Controls.Add(this.btnXoaMonGio);
            this.panelCart.Controls.Add(this.btnXacNhanGoi);
            this.panelCart.Controls.Add(this.lblTongTienGio);
            this.panelCart.Controls.Add(this.dgvGioHang);
            this.panelCart.Controls.Add(this.lblCartHeader);
            this.panelCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCart.Location = new System.Drawing.Point(0, 0);
            this.panelCart.Name = "panelCart";
            this.panelCart.Size = new System.Drawing.Size(586, 250);
            this.panelCart.TabIndex = 0;
            // 
            // btnXoaHetGio
            // 
            this.btnXoaHetGio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnXoaHetGio.BackColor = System.Drawing.Color.Gainsboro;
            this.btnXoaHetGio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaHetGio.FlatAppearance.BorderSize = 0;
            this.btnXoaHetGio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaHetGio.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoaHetGio.Location = new System.Drawing.Point(95, 212);
            this.btnXoaHetGio.Name = "btnXoaHetGio";
            this.btnXoaHetGio.Size = new System.Drawing.Size(75, 28);
            this.btnXoaHetGio.TabIndex = 5;
            this.btnXoaHetGio.Text = "Xóa hết";
            this.btnXoaHetGio.UseVisualStyleBackColor = false;
            this.btnXoaHetGio.Click += new System.EventHandler(this.btnXoaHetGio_Click);
            // 
            // btnXoaMonGio
            // 
            this.btnXoaMonGio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnXoaMonGio.BackColor = System.Drawing.Color.Gainsboro;
            this.btnXoaMonGio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaMonGio.FlatAppearance.BorderSize = 0;
            this.btnXoaMonGio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaMonGio.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoaMonGio.Location = new System.Drawing.Point(14, 212);
            this.btnXoaMonGio.Name = "btnXoaMonGio";
            this.btnXoaMonGio.Size = new System.Drawing.Size(75, 28);
            this.btnXoaMonGio.TabIndex = 4;
            this.btnXoaMonGio.Text = "Xóa món";
            this.btnXoaMonGio.UseVisualStyleBackColor = false;
            this.btnXoaMonGio.Click += new System.EventHandler(this.btnXoaMonGio_Click);
            // 
            // btnXacNhanGoi
            // 
            this.btnXacNhanGoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXacNhanGoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnXacNhanGoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXacNhanGoi.FlatAppearance.BorderSize = 0;
            this.btnXacNhanGoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXacNhanGoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXacNhanGoi.ForeColor = System.Drawing.Color.White;
            this.btnXacNhanGoi.Location = new System.Drawing.Point(425, 208);
            this.btnXacNhanGoi.Name = "btnXacNhanGoi";
            this.btnXacNhanGoi.Size = new System.Drawing.Size(145, 34);
            this.btnXacNhanGoi.TabIndex = 3;
            this.btnXacNhanGoi.Text = "Xác nhận gọi";
            this.btnXacNhanGoi.UseVisualStyleBackColor = false;
            this.btnXacNhanGoi.Click += new System.EventHandler(this.btnXacNhanGoi_Click);
            // 
            // lblTongTienGio
            // 
            this.lblTongTienGio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongTienGio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTienGio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblTongTienGio.Location = new System.Drawing.Point(14, 178);
            this.lblTongTienGio.Name = "lblTongTienGio";
            this.lblTongTienGio.Size = new System.Drawing.Size(556, 22);
            this.lblTongTienGio.TabIndex = 2;
            this.lblTongTienGio.Text = "Tổng giỏ: 0 đ";
            this.lblTongTienGio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvGioHang
            // 
            this.dgvGioHang.AllowUserToAddRows = false;
            this.dgvGioHang.AllowUserToDeleteRows = false;
            this.dgvGioHang.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvGioHang.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvGioHang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvGioHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGioHang.Location = new System.Drawing.Point(14, 38);
            this.dgvGioHang.MultiSelect = false;
            this.dgvGioHang.Name = "dgvGioHang";
            this.dgvGioHang.ReadOnly = true;
            this.dgvGioHang.RowHeadersVisible = false;
            this.dgvGioHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGioHang.Size = new System.Drawing.Size(556, 130);
            this.dgvGioHang.TabIndex = 1;
            // 
            // lblCartHeader
            // 
            this.lblCartHeader.AutoSize = true;
            this.lblCartHeader.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCartHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblCartHeader.Location = new System.Drawing.Point(10, 10);
            this.lblCartHeader.Name = "lblCartHeader";
            this.lblCartHeader.Size = new System.Drawing.Size(248, 19);
            this.lblCartHeader.TabIndex = 0;
            this.lblCartHeader.Text = "2. Giỏ hàng dịch vụ chờ xác nhận";
            // 
            // panelOrders
            // 
            this.panelOrders.BackColor = System.Drawing.Color.White;
            this.panelOrders.Controls.Add(this.lblTongCongTamTinh);
            this.panelOrders.Controls.Add(this.lblTienPhongTamTinh);
            this.panelOrders.Controls.Add(this.lblTongTienDichVu);
            this.panelOrders.Controls.Add(this.dgvDaGoi);
            this.panelOrders.Controls.Add(this.lblOrdersHeader);
            this.panelOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOrders.Location = new System.Drawing.Point(0, 0);
            this.panelOrders.Name = "panelOrders";
            this.panelOrders.Size = new System.Drawing.Size(586, 278);
            this.panelOrders.TabIndex = 0;
            // 
            // lblTongCongTamTinh
            // 
            this.lblTongCongTamTinh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongCongTamTinh.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongCongTamTinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.lblTongCongTamTinh.Location = new System.Drawing.Point(260, 245);
            this.lblTongCongTamTinh.Name = "lblTongCongTamTinh";
            this.lblTongCongTamTinh.Size = new System.Drawing.Size(310, 24);
            this.lblTongCongTamTinh.TabIndex = 4;
            this.lblTongCongTamTinh.Text = "TỔNG CỘNG TẠM TÍNH: 0 đ";
            this.lblTongCongTamTinh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTienPhongTamTinh
            // 
            this.lblTienPhongTamTinh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTienPhongTamTinh.AutoSize = true;
            this.lblTienPhongTamTinh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTienPhongTamTinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTienPhongTamTinh.Location = new System.Drawing.Point(14, 252);
            this.lblTienPhongTamTinh.Name = "lblTienPhongTamTinh";
            this.lblTienPhongTamTinh.Size = new System.Drawing.Size(147, 15);
            this.lblTienPhongTamTinh.TabIndex = 3;
            this.lblTienPhongTamTinh.Text = "Tiền giờ hát tạm tính: 0 đ";
            // 
            // lblTongTienDichVu
            // 
            this.lblTongTienDichVu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTongTienDichVu.AutoSize = true;
            this.lblTongTienDichVu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTienDichVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTongTienDichVu.Location = new System.Drawing.Point(14, 230);
            this.lblTongTienDichVu.Name = "lblTongTienDichVu";
            this.lblTongTienDichVu.Size = new System.Drawing.Size(126, 15);
            this.lblTongTienDichVu.TabIndex = 2;
            this.lblTongTienDichVu.Text = "Tiền dịch vụ đã gọi: 0 đ";
            // 
            // dgvDaGoi
            // 
            this.dgvDaGoi.AllowUserToAddRows = false;
            this.dgvDaGoi.AllowUserToDeleteRows = false;
            this.dgvDaGoi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDaGoi.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvDaGoi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDaGoi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDaGoi.Location = new System.Drawing.Point(14, 35);
            this.dgvDaGoi.MultiSelect = false;
            this.dgvDaGoi.Name = "dgvDaGoi";
            this.dgvDaGoi.ReadOnly = true;
            this.dgvDaGoi.RowHeadersVisible = false;
            this.dgvDaGoi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDaGoi.Size = new System.Drawing.Size(556, 185);
            this.dgvDaGoi.TabIndex = 1;
            // 
            // lblOrdersHeader
            // 
            this.lblOrdersHeader.AutoSize = true;
            this.lblOrdersHeader.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrdersHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblOrdersHeader.Location = new System.Drawing.Point(10, 8);
            this.lblOrdersHeader.Name = "lblOrdersHeader";
            this.lblOrdersHeader.Size = new System.Drawing.Size(325, 19);
            this.lblOrdersHeader.TabIndex = 0;
            this.lblOrdersHeader.Text = "3. Các món đã phục vụ (Đã xác nhận chốt giá)";
            // 
            // FrmGoiDichVu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1040, 640);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.panelTopBar);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmGoiDichVu";
            this.Text = "Gọi Dịch Vụ";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelTopBar.ResumeLayout(false);
            this.panelTopBar.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.panelCatalog.ResumeLayout(false);
            this.panelCatalog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMenu)).EndInit();
            this.splitContainerRight.Panel1.ResumeLayout(false);
            this.splitContainerRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRight)).EndInit();
            this.splitContainerRight.ResumeLayout(false);
            this.panelCart.ResumeLayout(false);
            this.panelCart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).EndInit();
            this.panelOrders.ResumeLayout(false);
            this.panelOrders.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDaGoi)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelTopBar;
        private System.Windows.Forms.Label labelChonPhong;
        private System.Windows.Forms.ComboBox cbPhienHat;
        private System.Windows.Forms.Label lblSessionDetail;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Panel panelCatalog;
        private System.Windows.Forms.Label lblCatalogHeader;
        private System.Windows.Forms.Label labelTimDV;
        private System.Windows.Forms.TextBox txtTimDichVu;
        private System.Windows.Forms.DataGridView dgvMenu;
        private System.Windows.Forms.Label labelSoLuong;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.Button btnThemVaoGio;
        private System.Windows.Forms.SplitContainer splitContainerRight;
        private System.Windows.Forms.Panel panelCart;
        private System.Windows.Forms.Label lblCartHeader;
        private System.Windows.Forms.DataGridView dgvGioHang;
        private System.Windows.Forms.Label lblTongTienGio;
        private System.Windows.Forms.Button btnXacNhanGoi;
        private System.Windows.Forms.Button btnXoaMonGio;
        private System.Windows.Forms.Button btnXoaHetGio;
        private System.Windows.Forms.Panel panelOrders;
        private System.Windows.Forms.Label lblOrdersHeader;
        private System.Windows.Forms.DataGridView dgvDaGoi;
        private System.Windows.Forms.Label lblTongTienDichVu;
        private System.Windows.Forms.Label lblTienPhongTamTinh;
        private System.Windows.Forms.Label lblTongCongTamTinh;
    }
}
