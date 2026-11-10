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
            this.panelChonPhong = new System.Windows.Forms.Panel();
            this.panelTimPhong = new System.Windows.Forms.Panel();
            this.lblChonPhong = new System.Windows.Forms.Label();
            this.lblTimPhong = new System.Windows.Forms.Label();
            this.txtTimPhong = new System.Windows.Forms.TextBox();
            this.lblSoPhong = new System.Windows.Forms.Label();
            this.btnLamMoiPhong = new System.Windows.Forms.Button();
            this.danhSachPhong = new System.Windows.Forms.FlowLayoutPanel();
            this.btnChonPhongKhac = new System.Windows.Forms.Button();
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
            this.panelTopBar.Controls.Add(this.btnChonPhongKhac);
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
            this.lblSessionDetail.Location = new System.Drawing.Point(155, 8);
            this.lblSessionDetail.Name = "lblSessionDetail";
            this.lblSessionDetail.Size = new System.Drawing.Size(770, 44);
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

            // 
            // labelChonPhong
            // 
            this.labelChonPhong.Visible = false;
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
            // Static room picker controls (room cards are created from live data).
            this.panelChonPhong.Name = "panelChonPhong";
            this.panelChonPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChonPhong.BackColor = System.Drawing.Color.White;
            this.panelChonPhong.Controls.Add(this.danhSachPhong);
            this.panelChonPhong.Controls.Add(this.panelTimPhong);
            this.panelTimPhong.Name = "panelTimPhong";
            this.panelTimPhong.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTimPhong.Size = new System.Drawing.Size(1040, 70);
            this.panelTimPhong.BackColor = System.Drawing.Color.White;
            this.panelTimPhong.Controls.Add(this.lblChonPhong);
            this.panelTimPhong.Controls.Add(this.lblTimPhong);
            this.panelTimPhong.Controls.Add(this.txtTimPhong);
            this.panelTimPhong.Controls.Add(this.lblSoPhong);
            this.panelTimPhong.Controls.Add(this.btnLamMoiPhong);
            this.lblChonPhong.Name = "lblChonPhong";
            this.lblChonPhong.Text = "CHỌN PHÒNG ĐỂ GỌI DỊCH VỤ";
            this.lblChonPhong.AutoSize = true;
            this.lblChonPhong.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblChonPhong.ForeColor = System.Drawing.Color.FromArgb(22, 101, 52);
            this.lblChonPhong.Location = new System.Drawing.Point(16, 9);
            this.lblTimPhong.Name = "lblTimPhong";
            this.lblTimPhong.Text = "Tìm phòng:";
            this.lblTimPhong.AutoSize = true;
            this.lblTimPhong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTimPhong.Location = new System.Drawing.Point(16, 43);
            this.txtTimPhong.Name = "txtTimPhong";
            this.txtTimPhong.Location = new System.Drawing.Point(94, 39);
            this.txtTimPhong.Size = new System.Drawing.Size(200, 23);
            this.txtTimPhong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTimPhong.TextChanged += new System.EventHandler(this.txtTimPhong_TextChanged);
            this.lblSoPhong.Name = "lblSoPhong";
            this.lblSoPhong.AutoSize = true;
            this.lblSoPhong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSoPhong.ForeColor = System.Drawing.Color.DimGray;
            this.lblSoPhong.Location = new System.Drawing.Point(315, 43);
            this.btnLamMoiPhong.Name = "btnLamMoiPhong";
            this.btnLamMoiPhong.Text = "Làm mới";
            this.btnLamMoiPhong.Size = new System.Drawing.Size(85, 28);
            this.btnLamMoiPhong.Location = new System.Drawing.Point(940, 36);
            this.btnLamMoiPhong.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnLamMoiPhong.BackColor = System.Drawing.Color.Gainsboro;
            this.btnLamMoiPhong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoiPhong.FlatAppearance.BorderSize = 0;
            this.btnLamMoiPhong.Click += new System.EventHandler(this.btnLamMoiPhong_Click);
            this.danhSachPhong.Name = "danhSachPhong";
            this.danhSachPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.danhSachPhong.AutoScroll = true;
            this.danhSachPhong.Padding = new System.Windows.Forms.Padding(12);
            this.danhSachPhong.BackColor = System.Drawing.Color.FromArgb(243, 248, 245);
            this.btnChonPhongKhac.Name = "btnChonPhongKhac";
            this.btnChonPhongKhac.Text = "← Chọn phòng";
            this.btnChonPhongKhac.Size = new System.Drawing.Size(125, 32);
            this.btnChonPhongKhac.Location = new System.Drawing.Point(15, 14);
            this.btnChonPhongKhac.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChonPhongKhac.FlatAppearance.BorderSize = 0;
            this.btnChonPhongKhac.BackColor = System.Drawing.Color.FromArgb(22, 101, 52);
            this.btnChonPhongKhac.ForeColor = System.Drawing.Color.White;
            this.btnChonPhongKhac.Click += new System.EventHandler(this.btnChonPhongKhac_Click);
            this.cbPhienHat.Visible = false;
            // FrmGoiDichVu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1040, 640);
            this.Controls.Add(this.panelChonPhong);
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
            this.designColumn_dgvMenu_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvMenu_1.DataPropertyName = "MaDV";
            this.designColumn_dgvMenu_1.HeaderText = "Mã";
            this.designColumn_dgvMenu_1.Width = 65;
            this.designColumn_dgvMenu_1.Name = "designColumn_dgvMenu_1";
            this.dgvMenu.Columns.Add(this.designColumn_dgvMenu_1);
            this.designColumn_dgvMenu_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvMenu_2.DataPropertyName = "TenDV";
            this.designColumn_dgvMenu_2.HeaderText = "Tên dịch vụ";
            this.designColumn_dgvMenu_2.Width = 160;
            this.designColumn_dgvMenu_2.Name = "designColumn_dgvMenu_2";
            this.dgvMenu.Columns.Add(this.designColumn_dgvMenu_2);
            this.designColumn_dgvMenu_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvMenu_3.DataPropertyName = "DonViTinh";
            this.designColumn_dgvMenu_3.HeaderText = "ĐVT";
            this.designColumn_dgvMenu_3.Width = 65;
            this.designColumn_dgvMenu_3.Name = "designColumn_dgvMenu_3";
            this.dgvMenu.Columns.Add(this.designColumn_dgvMenu_3);
            this.designColumn_dgvMenu_4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvMenu_4.DataPropertyName = "DonGia";
            this.designColumn_dgvMenu_4.HeaderText = "Đơn giá";
            this.designColumn_dgvMenu_4.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvMenu_4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.designColumn_dgvMenu_4.Name = "designColumn_dgvMenu_4";
            this.dgvMenu.Columns.Add(this.designColumn_dgvMenu_4);
            this.designColumn_dgvGioHang_5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvGioHang_5.DataPropertyName = "TenDV";
            this.designColumn_dgvGioHang_5.HeaderText = "Mặt hàng";
            this.designColumn_dgvGioHang_5.Width = 160;
            this.designColumn_dgvGioHang_5.Name = "designColumn_dgvGioHang_5";
            this.dgvGioHang.Columns.Add(this.designColumn_dgvGioHang_5);
            this.designColumn_dgvGioHang_6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvGioHang_6.DataPropertyName = "DonViTinh";
            this.designColumn_dgvGioHang_6.HeaderText = "ĐVT";
            this.designColumn_dgvGioHang_6.Width = 60;
            this.designColumn_dgvGioHang_6.Name = "designColumn_dgvGioHang_6";
            this.dgvGioHang.Columns.Add(this.designColumn_dgvGioHang_6);
            this.designColumn_dgvGioHang_7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvGioHang_7.DataPropertyName = "DonGia";
            this.designColumn_dgvGioHang_7.HeaderText = "Đơn giá";
            this.designColumn_dgvGioHang_7.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvGioHang_7.Width = 90;
            this.designColumn_dgvGioHang_7.Name = "designColumn_dgvGioHang_7";
            this.dgvGioHang.Columns.Add(this.designColumn_dgvGioHang_7);
            this.designColumn_dgvGioHang_8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvGioHang_8.DataPropertyName = "SoLuong";
            this.designColumn_dgvGioHang_8.HeaderText = "SL";
            this.designColumn_dgvGioHang_8.Width = 55;
            this.designColumn_dgvGioHang_8.Name = "designColumn_dgvGioHang_8";
            this.dgvGioHang.Columns.Add(this.designColumn_dgvGioHang_8);
            this.designColumn_dgvGioHang_9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvGioHang_9.DataPropertyName = "ThanhTien";
            this.designColumn_dgvGioHang_9.HeaderText = "Thành tiền";
            this.designColumn_dgvGioHang_9.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvGioHang_9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.designColumn_dgvGioHang_9.Name = "designColumn_dgvGioHang_9";
            this.dgvGioHang.Columns.Add(this.designColumn_dgvGioHang_9);
            this.designColumn_dgvDaGoi_10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvDaGoi_10.DataPropertyName = "TenDV";
            this.designColumn_dgvDaGoi_10.HeaderText = "Tên món";
            this.designColumn_dgvDaGoi_10.Width = 150;
            this.designColumn_dgvDaGoi_10.Name = "designColumn_dgvDaGoi_10";
            this.dgvDaGoi.Columns.Add(this.designColumn_dgvDaGoi_10);
            this.designColumn_dgvDaGoi_11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvDaGoi_11.DataPropertyName = "DonViTinh";
            this.designColumn_dgvDaGoi_11.HeaderText = "ĐVT";
            this.designColumn_dgvDaGoi_11.Width = 60;
            this.designColumn_dgvDaGoi_11.Name = "designColumn_dgvDaGoi_11";
            this.dgvDaGoi.Columns.Add(this.designColumn_dgvDaGoi_11);
            this.designColumn_dgvDaGoi_12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvDaGoi_12.DataPropertyName = "DonGia";
            this.designColumn_dgvDaGoi_12.HeaderText = "Đơn giá";
            this.designColumn_dgvDaGoi_12.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvDaGoi_12.Width = 85;
            this.designColumn_dgvDaGoi_12.Name = "designColumn_dgvDaGoi_12";
            this.dgvDaGoi.Columns.Add(this.designColumn_dgvDaGoi_12);
            this.designColumn_dgvDaGoi_13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvDaGoi_13.DataPropertyName = "SoLuong";
            this.designColumn_dgvDaGoi_13.HeaderText = "SL";
            this.designColumn_dgvDaGoi_13.Width = 50;
            this.designColumn_dgvDaGoi_13.Name = "designColumn_dgvDaGoi_13";
            this.dgvDaGoi.Columns.Add(this.designColumn_dgvDaGoi_13);
            this.designColumn_dgvDaGoi_14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvDaGoi_14.DataPropertyName = "ThanhTien";
            this.designColumn_dgvDaGoi_14.HeaderText = "Thành tiền";
            this.designColumn_dgvDaGoi_14.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvDaGoi_14.Width = 95;
            this.designColumn_dgvDaGoi_14.Name = "designColumn_dgvDaGoi_14";
            this.dgvDaGoi.Columns.Add(this.designColumn_dgvDaGoi_14);
            this.designColumn_dgvDaGoi_15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvDaGoi_15.DataPropertyName = "ThoiGianGoi";
            this.designColumn_dgvDaGoi_15.HeaderText = "Thời gian gọi";
            this.designColumn_dgvDaGoi_15.DefaultCellStyle.Format = "HH:mm";
            this.designColumn_dgvDaGoi_15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.designColumn_dgvDaGoi_15.Name = "designColumn_dgvDaGoi_15";
            this.dgvDaGoi.Columns.Add(this.designColumn_dgvDaGoi_15);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelChonPhong;
        private System.Windows.Forms.Panel panelTimPhong;
        private System.Windows.Forms.Label lblChonPhong;
        private System.Windows.Forms.Label lblTimPhong;
        private System.Windows.Forms.TextBox txtTimPhong;
        private System.Windows.Forms.Label lblSoPhong;
        private System.Windows.Forms.Button btnLamMoiPhong;
        private System.Windows.Forms.FlowLayoutPanel danhSachPhong;
        private System.Windows.Forms.Button btnChonPhongKhac;
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

        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvMenu_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvMenu_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvMenu_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvMenu_4;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvGioHang_5;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvGioHang_6;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvGioHang_7;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvGioHang_8;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvGioHang_9;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvDaGoi_10;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvDaGoi_11;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvDaGoi_12;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvDaGoi_13;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvDaGoi_14;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvDaGoi_15;
    }
}
