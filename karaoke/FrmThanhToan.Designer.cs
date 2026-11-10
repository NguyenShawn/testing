namespace karaoke
{
    partial class FrmThanhToan
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabThanhToan = new System.Windows.Forms.TabPage();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.panelLeftHeader = new System.Windows.Forms.Panel();
            this.btnLamMoiPhong = new System.Windows.Forms.Button();
            this.lblListPhongTitle = new System.Windows.Forms.Label();
            this.dgvPhongDangHat = new System.Windows.Forms.DataGridView();
            this.panelRightBilling = new System.Windows.Forms.Panel();
            this.grpKetToan = new System.Windows.Forms.GroupBox();
            this.lblTienThuaVal = new System.Windows.Forms.Label();
            this.lblTienThua = new System.Windows.Forms.Label();
            this.txtTienKhachDua = new System.Windows.Forms.TextBox();
            this.lblTienKhachDua = new System.Windows.Forms.Label();
            this.btnInHoaDon = new System.Windows.Forms.Button();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.cboPhuongThuc = new System.Windows.Forms.ComboBox();
            this.lblPhuongThuc = new System.Windows.Forms.Label();
            this.lblThanhTienVal = new System.Windows.Forms.Label();
            this.lblThanhTien = new System.Windows.Forms.Label();
            this.numGiamGia = new System.Windows.Forms.NumericUpDown();
            this.lblGiamGia = new System.Windows.Forms.Label();
            this.lblSumTienDVVal = new System.Windows.Forms.Label();
            this.lblSumTienDV = new System.Windows.Forms.Label();
            this.lblSumTienPhongVal = new System.Windows.Forms.Label();
            this.lblSumTienPhong = new System.Windows.Forms.Label();
            this.grpDichVu = new System.Windows.Forms.GroupBox();
            this.lblTongTienDVGrid = new System.Windows.Forms.Label();
            this.dgvDichVu = new System.Windows.Forms.DataGridView();
            this.grpThongTinPhong = new System.Windows.Forms.GroupBox();
            this.lblTienPhongUocTinh = new System.Windows.Forms.Label();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.lblThoiGianHat = new System.Windows.Forms.Label();
            this.lblGioRa = new System.Windows.Forms.Label();
            this.lblGioVao = new System.Windows.Forms.Label();
            this.lblTenPhong = new System.Windows.Forms.Label();
            this.tabLichSu = new System.Windows.Forms.TabPage();
            this.dgvLichSu = new System.Windows.Forms.DataGridView();
            this.panelLichSuFilter = new System.Windows.Forms.Panel();
            this.btnInLaiHD = new System.Windows.Forms.Button();
            this.btnTimLichSu = new System.Windows.Forms.Button();
            this.txtTimKiemHD = new System.Windows.Forms.TextBox();
            this.lblTimKiemHD = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabThanhToan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.panelLeftHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhongDangHat)).BeginInit();
            this.panelRightBilling.SuspendLayout();
            this.grpKetToan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiamGia)).BeginInit();
            this.grpDichVu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).BeginInit();
            this.grpThongTinPhong.SuspendLayout();
            this.tabLichSu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).BeginInit();
            this.panelLichSuFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1024, 46);
            this.panelHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(14, 10);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(325, 25);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "QUẢN LÝ THANH TOÁN & HÓA ĐƠN";
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabThanhToan);
            this.tabMain.Controls.Add(this.tabLichSu);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.tabMain.Location = new System.Drawing.Point(0, 46);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1024, 624);
            this.tabMain.TabIndex = 1;
            // 
            // tabThanhToan
            // 
            this.tabThanhToan.Controls.Add(this.splitContainerMain);
            this.tabThanhToan.Location = new System.Drawing.Point(4, 26);
            this.tabThanhToan.Name = "tabThanhToan";
            this.tabThanhToan.Padding = new System.Windows.Forms.Padding(3);
            this.tabThanhToan.Size = new System.Drawing.Size(1016, 594);
            this.tabThanhToan.TabIndex = 0;
            this.tabThanhToan.Text = "  Thanh Toán Phòng  ";
            this.tabThanhToan.UseVisualStyleBackColor = true;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.Location = new System.Drawing.Point(3, 3);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.dgvPhongDangHat);
            this.splitContainerMain.Panel1.Controls.Add(this.panelLeftHeader);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.panelRightBilling);
            this.splitContainerMain.Size = new System.Drawing.Size(1010, 588);
            this.splitContainerMain.SplitterDistance = 330;
            this.splitContainerMain.TabIndex = 0;
            // 
            // panelLeftHeader
            // 
            this.panelLeftHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.panelLeftHeader.Controls.Add(this.btnLamMoiPhong);
            this.panelLeftHeader.Controls.Add(this.lblListPhongTitle);
            this.panelLeftHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLeftHeader.Location = new System.Drawing.Point(0, 0);
            this.panelLeftHeader.Name = "panelLeftHeader";
            this.panelLeftHeader.Size = new System.Drawing.Size(330, 42);
            this.panelLeftHeader.TabIndex = 0;
            // 
            // btnLamMoiPhong
            // 
            this.btnLamMoiPhong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoiPhong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.btnLamMoiPhong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoiPhong.FlatAppearance.BorderSize = 0;
            this.btnLamMoiPhong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoiPhong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLamMoiPhong.ForeColor = System.Drawing.Color.White;
            this.btnLamMoiPhong.Location = new System.Drawing.Point(235, 7);
            this.btnLamMoiPhong.Name = "btnLamMoiPhong";
            this.btnLamMoiPhong.Size = new System.Drawing.Size(88, 28);
            this.btnLamMoiPhong.TabIndex = 1;
            this.btnLamMoiPhong.Text = "Làm mới";
            this.btnLamMoiPhong.UseVisualStyleBackColor = false;
            this.btnLamMoiPhong.Click += new System.EventHandler(this.btnLamMoiPhong_Click);
            // 
            // lblListPhongTitle
            // 
            this.lblListPhongTitle.AutoSize = true;
            this.lblListPhongTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblListPhongTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblListPhongTitle.Location = new System.Drawing.Point(8, 11);
            this.lblListPhongTitle.Name = "lblListPhongTitle";
            this.lblListPhongTitle.Size = new System.Drawing.Size(147, 19);
            this.lblListPhongTitle.TabIndex = 0;
            this.lblListPhongTitle.Text = "PHÒNG ĐANG HÁT";
            // 
            // dgvPhongDangHat
            // 
            this.dgvPhongDangHat.AllowUserToAddRows = false;
            this.dgvPhongDangHat.AllowUserToDeleteRows = false;
            this.dgvPhongDangHat.BackgroundColor = System.Drawing.Color.White;
            this.dgvPhongDangHat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPhongDangHat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhongDangHat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPhongDangHat.Location = new System.Drawing.Point(0, 42);
            this.dgvPhongDangHat.MultiSelect = false;
            this.dgvPhongDangHat.Name = "dgvPhongDangHat";
            this.dgvPhongDangHat.ReadOnly = true;
            this.dgvPhongDangHat.RowHeadersVisible = false;
            this.dgvPhongDangHat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhongDangHat.Size = new System.Drawing.Size(330, 546);
            this.dgvPhongDangHat.TabIndex = 1;
            this.dgvPhongDangHat.SelectionChanged += new System.EventHandler(this.dgvPhongDangHat_SelectionChanged);
            // 
            // panelRightBilling
            // 
            this.panelRightBilling.AutoScroll = true;
            this.panelRightBilling.Controls.Add(this.grpKetToan);
            this.panelRightBilling.Controls.Add(this.grpDichVu);
            this.panelRightBilling.Controls.Add(this.grpThongTinPhong);
            this.panelRightBilling.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRightBilling.Location = new System.Drawing.Point(0, 0);
            this.panelRightBilling.Name = "panelRightBilling";
            this.panelRightBilling.Padding = new System.Windows.Forms.Padding(10);
            this.panelRightBilling.Size = new System.Drawing.Size(676, 588);
            this.panelRightBilling.TabIndex = 0;
            // 
            // grpKetToan
            // 
            this.grpKetToan.Controls.Add(this.lblTienThuaVal);
            this.grpKetToan.Controls.Add(this.lblTienThua);
            this.grpKetToan.Controls.Add(this.txtTienKhachDua);
            this.grpKetToan.Controls.Add(this.lblTienKhachDua);
            this.grpKetToan.Controls.Add(this.btnInHoaDon);
            this.grpKetToan.Controls.Add(this.btnThanhToan);
            this.grpKetToan.Controls.Add(this.cboPhuongThuc);
            this.grpKetToan.Controls.Add(this.lblPhuongThuc);
            this.grpKetToan.Controls.Add(this.lblThanhTienVal);
            this.grpKetToan.Controls.Add(this.lblThanhTien);
            this.grpKetToan.Controls.Add(this.numGiamGia);
            this.grpKetToan.Controls.Add(this.lblGiamGia);
            this.grpKetToan.Controls.Add(this.lblSumTienDVVal);
            this.grpKetToan.Controls.Add(this.lblSumTienDV);
            this.grpKetToan.Controls.Add(this.lblSumTienPhongVal);
            this.grpKetToan.Controls.Add(this.lblSumTienPhong);
            this.grpKetToan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpKetToan.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.grpKetToan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.grpKetToan.Location = new System.Drawing.Point(10, 360);
            this.grpKetToan.Name = "grpKetToan";
            this.grpKetToan.Size = new System.Drawing.Size(656, 218);
            this.grpKetToan.TabIndex = 2;
            this.grpKetToan.TabStop = false;
            this.grpKetToan.Text = "KẾT TOÁN THANH TOÁN";
            // 
            // lblTienThuaVal
            // 
            this.lblTienThuaVal.AutoSize = true;
            this.lblTienThuaVal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTienThuaVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.lblTienThuaVal.Location = new System.Drawing.Point(445, 115);
            this.lblTienThuaVal.Name = "lblTienThuaVal";
            this.lblTienThuaVal.Size = new System.Drawing.Size(35, 20);
            this.lblTienThuaVal.TabIndex = 15;
            this.lblTienThuaVal.Text = "0 đ";
            // 
            // lblTienThua
            // 
            this.lblTienThua.AutoSize = true;
            this.lblTienThua.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTienThua.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTienThua.Location = new System.Drawing.Point(325, 117);
            this.lblTienThua.Name = "lblTienThua";
            this.lblTienThua.Size = new System.Drawing.Size(101, 17);
            this.lblTienThua.TabIndex = 14;
            this.lblTienThua.Text = "Tiền thừa trả lại:";
            // 
            // txtTienKhachDua
            // 
            this.txtTienKhachDua.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTienKhachDua.Location = new System.Drawing.Point(445, 75);
            this.txtTienKhachDua.Name = "txtTienKhachDua";
            this.txtTienKhachDua.Size = new System.Drawing.Size(190, 25);
            this.txtTienKhachDua.TabIndex = 13;
            this.txtTienKhachDua.TextChanged += new System.EventHandler(this.txtTienKhachDua_TextChanged);
            // 
            // lblTienKhachDua
            // 
            this.lblTienKhachDua.AutoSize = true;
            this.lblTienKhachDua.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTienKhachDua.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTienKhachDua.Location = new System.Drawing.Point(325, 78);
            this.lblTienKhachDua.Name = "lblTienKhachDua";
            this.lblTienKhachDua.Size = new System.Drawing.Size(100, 17);
            this.lblTienKhachDua.TabIndex = 12;
            this.lblTienKhachDua.Text = "Tiền khách đưa:";
            // 
            // btnInHoaDon
            // 
            this.btnInHoaDon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInHoaDon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnInHoaDon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInHoaDon.FlatAppearance.BorderSize = 0;
            this.btnInHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInHoaDon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnInHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnInHoaDon.Location = new System.Drawing.Point(328, 160);
            this.btnInHoaDon.Name = "btnInHoaDon";
            this.btnInHoaDon.Size = new System.Drawing.Size(130, 42);
            this.btnInHoaDon.TabIndex = 11;
            this.btnInHoaDon.Text = "Xem Mẫu In";
            this.btnInHoaDon.UseVisualStyleBackColor = false;
            this.btnInHoaDon.Click += new System.EventHandler(this.btnInHoaDon_Click);
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThanhToan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.btnThanhToan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThanhToan.FlatAppearance.BorderSize = 0;
            this.btnThanhToan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThanhToan.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnThanhToan.ForeColor = System.Drawing.Color.White;
            this.btnThanhToan.Location = new System.Drawing.Point(468, 160);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(175, 42);
            this.btnThanhToan.TabIndex = 10;
            this.btnThanhToan.Text = "THANH TOÁN";
            this.btnThanhToan.UseVisualStyleBackColor = false;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // cboPhuongThuc
            // 
            this.cboPhuongThuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhuongThuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboPhuongThuc.FormattingEnabled = true;
            this.cboPhuongThuc.Items.AddRange(new object[] {
            "Tiền mặt",
            "Chuyển khoản",
            "Thẻ ngân hàng"});
            this.cboPhuongThuc.Location = new System.Drawing.Point(445, 34);
            this.cboPhuongThuc.Name = "cboPhuongThuc";
            this.cboPhuongThuc.Size = new System.Drawing.Size(190, 25);
            this.cboPhuongThuc.TabIndex = 9;
            // 
            // lblPhuongThuc
            // 
            this.lblPhuongThuc.AutoSize = true;
            this.lblPhuongThuc.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPhuongThuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblPhuongThuc.Location = new System.Drawing.Point(325, 37);
            this.lblPhuongThuc.Name = "lblPhuongThuc";
            this.lblPhuongThuc.Size = new System.Drawing.Size(107, 17);
            this.lblPhuongThuc.TabIndex = 8;
            this.lblPhuongThuc.Text = "Phương thức TT:";
            // 
            // lblThanhTienVal
            // 
            this.lblThanhTienVal.AutoSize = true;
            this.lblThanhTienVal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblThanhTienVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblThanhTienVal.Location = new System.Drawing.Point(135, 145);
            this.lblThanhTienVal.Name = "lblThanhTienVal";
            this.lblThanhTienVal.Size = new System.Drawing.Size(46, 25);
            this.lblThanhTienVal.TabIndex = 7;
            this.lblThanhTienVal.Text = "0 đ";
            // 
            // lblThanhTien
            // 
            this.lblThanhTien.AutoSize = true;
            this.lblThanhTien.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblThanhTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblThanhTien.Location = new System.Drawing.Point(15, 148);
            this.lblThanhTien.Name = "lblThanhTien";
            this.lblThanhTien.Size = new System.Drawing.Size(107, 20);
            this.lblThanhTien.TabIndex = 6;
            this.lblThanhTien.Text = "TỔNG CỘNG:";
            // 
            // numGiamGia
            // 
            this.numGiamGia.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.numGiamGia.Increment = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numGiamGia.Location = new System.Drawing.Point(138, 105);
            this.numGiamGia.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numGiamGia.Name = "numGiamGia";
            this.numGiamGia.Size = new System.Drawing.Size(140, 24);
            this.numGiamGia.TabIndex = 5;
            this.numGiamGia.ThousandsSeparator = true;
            this.numGiamGia.ValueChanged += new System.EventHandler(this.numGiamGia_ValueChanged);
            // 
            // lblGiamGia
            // 
            this.lblGiamGia.AutoSize = true;
            this.lblGiamGia.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblGiamGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblGiamGia.Location = new System.Drawing.Point(15, 107);
            this.lblGiamGia.Name = "lblGiamGia";
            this.lblGiamGia.Size = new System.Drawing.Size(95, 17);
            this.lblGiamGia.TabIndex = 4;
            this.lblGiamGia.Text = "Giảm giá (VNĐ):";
            // 
            // lblSumTienDVVal
            // 
            this.lblSumTienDVVal.AutoSize = true;
            this.lblSumTienDVVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSumTienDVVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSumTienDVVal.Location = new System.Drawing.Point(135, 72);
            this.lblSumTienDVVal.Name = "lblSumTienDVVal";
            this.lblSumTienDVVal.Size = new System.Drawing.Size(32, 19);
            this.lblSumTienDVVal.TabIndex = 3;
            this.lblSumTienDVVal.Text = "0 đ";
            // 
            // lblSumTienDV
            // 
            this.lblSumTienDV.AutoSize = true;
            this.lblSumTienDV.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSumTienDV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSumTienDV.Location = new System.Drawing.Point(15, 72);
            this.lblSumTienDV.Name = "lblSumTienDV";
            this.lblSumTienDV.Size = new System.Drawing.Size(81, 17);
            this.lblSumTienDV.TabIndex = 2;
            this.lblSumTienDV.Text = "Tiền dịch vụ:";
            // 
            // lblSumTienPhongVal
            // 
            this.lblSumTienPhongVal.AutoSize = true;
            this.lblSumTienPhongVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSumTienPhongVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSumTienPhongVal.Location = new System.Drawing.Point(135, 37);
            this.lblSumTienPhongVal.Name = "lblSumTienPhongVal";
            this.lblSumTienPhongVal.Size = new System.Drawing.Size(32, 19);
            this.lblSumTienPhongVal.TabIndex = 1;
            this.lblSumTienPhongVal.Text = "0 đ";
            // 
            // lblSumTienPhong
            // 
            this.lblSumTienPhong.AutoSize = true;
            this.lblSumTienPhong.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSumTienPhong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSumTienPhong.Location = new System.Drawing.Point(15, 37);
            this.lblSumTienPhong.Name = "lblSumTienPhong";
            this.lblSumTienPhong.Size = new System.Drawing.Size(77, 17);
            this.lblSumTienPhong.TabIndex = 0;
            this.lblSumTienPhong.Text = "Tiền phòng:";
            // 
            // grpDichVu
            // 
            this.grpDichVu.Controls.Add(this.lblTongTienDVGrid);
            this.grpDichVu.Controls.Add(this.dgvDichVu);
            this.grpDichVu.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpDichVu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.grpDichVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.grpDichVu.Location = new System.Drawing.Point(10, 135);
            this.grpDichVu.Name = "grpDichVu";
            this.grpDichVu.Size = new System.Drawing.Size(656, 225);
            this.grpDichVu.TabIndex = 1;
            this.grpDichVu.TabStop = false;
            this.grpDichVu.Text = "DỊCH VỤ ĐÃ SỬ DỤNG";
            // 
            // lblTongTienDVGrid
            // 
            this.lblTongTienDVGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongTienDVGrid.AutoSize = true;
            this.lblTongTienDVGrid.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTongTienDVGrid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTongTienDVGrid.Location = new System.Drawing.Point(450, 203);
            this.lblTongTienDVGrid.Name = "lblTongTienDVGrid";
            this.lblTongTienDVGrid.Size = new System.Drawing.Size(142, 17);
            this.lblTongTienDVGrid.TabIndex = 1;
            this.lblTongTienDVGrid.Text = "Tổng tiền dịch vụ: 0 đ";
            // 
            // dgvDichVu
            // 
            this.dgvDichVu.AllowUserToAddRows = false;
            this.dgvDichVu.AllowUserToDeleteRows = false;
            this.dgvDichVu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDichVu.BackgroundColor = System.Drawing.Color.White;
            this.dgvDichVu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDichVu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDichVu.Location = new System.Drawing.Point(8, 22);
            this.dgvDichVu.Name = "dgvDichVu";
            this.dgvDichVu.ReadOnly = true;
            this.dgvDichVu.RowHeadersVisible = false;
            this.dgvDichVu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDichVu.Size = new System.Drawing.Size(640, 175);
            this.dgvDichVu.TabIndex = 0;
            // 
            // grpThongTinPhong
            // 
            this.grpThongTinPhong.Controls.Add(this.lblTienPhongUocTinh);
            this.grpThongTinPhong.Controls.Add(this.lblDonGia);
            this.grpThongTinPhong.Controls.Add(this.lblThoiGianHat);
            this.grpThongTinPhong.Controls.Add(this.lblGioRa);
            this.grpThongTinPhong.Controls.Add(this.lblGioVao);
            this.grpThongTinPhong.Controls.Add(this.lblTenPhong);
            this.grpThongTinPhong.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpThongTinPhong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.grpThongTinPhong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.grpThongTinPhong.Location = new System.Drawing.Point(10, 10);
            this.grpThongTinPhong.Name = "grpThongTinPhong";
            this.grpThongTinPhong.Size = new System.Drawing.Size(656, 125);
            this.grpThongTinPhong.TabIndex = 0;
            this.grpThongTinPhong.TabStop = false;
            this.grpThongTinPhong.Text = "THÔNG TIN PHÒNG HÁT";
            // 
            // lblTienPhongUocTinh
            // 
            this.lblTienPhongUocTinh.AutoSize = true;
            this.lblTienPhongUocTinh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTienPhongUocTinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblTienPhongUocTinh.Location = new System.Drawing.Point(325, 90);
            this.lblTienPhongUocTinh.Name = "lblTienPhongUocTinh";
            this.lblTienPhongUocTinh.Size = new System.Drawing.Size(125, 19);
            this.lblTienPhongUocTinh.TabIndex = 5;
            this.lblTienPhongUocTinh.Text = "Tiền phòng tạm: -";
            // 
            // lblDonGia
            // 
            this.lblDonGia.AutoSize = true;
            this.lblDonGia.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDonGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblDonGia.Location = new System.Drawing.Point(325, 30);
            this.lblDonGia.Name = "lblDonGia";
            this.lblDonGia.Size = new System.Drawing.Size(89, 17);
            this.lblDonGia.TabIndex = 4;
            this.lblDonGia.Text = "Đơn giá giờ: -";
            // 
            // lblThoiGianHat
            // 
            this.lblThoiGianHat.AutoSize = true;
            this.lblThoiGianHat.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblThoiGianHat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblThoiGianHat.Location = new System.Drawing.Point(325, 60);
            this.lblThoiGianHat.Name = "lblThoiGianHat";
            this.lblThoiGianHat.Size = new System.Drawing.Size(95, 17);
            this.lblThoiGianHat.TabIndex = 3;
            this.lblThoiGianHat.Text = "Thời gian hát: -";
            // 
            // lblGioRa
            // 
            this.lblGioRa.AutoSize = true;
            this.lblGioRa.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblGioRa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblGioRa.Location = new System.Drawing.Point(15, 90);
            this.lblGioRa.Name = "lblGioRa";
            this.lblGioRa.Size = new System.Drawing.Size(107, 17);
            this.lblGioRa.TabIndex = 2;
            this.lblGioRa.Text = "Giờ kết thúc: -";
            // 
            // lblGioVao
            // 
            this.lblGioVao.AutoSize = true;
            this.lblGioVao.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblGioVao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblGioVao.Location = new System.Drawing.Point(15, 60);
            this.lblGioVao.Name = "lblGioVao";
            this.lblGioVao.Size = new System.Drawing.Size(103, 17);
            this.lblGioVao.TabIndex = 1;
            this.lblGioVao.Text = "Giờ bắt đầu: -";
            // 
            // lblTenPhong
            // 
            this.lblTenPhong.AutoSize = true;
            this.lblTenPhong.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTenPhong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTenPhong.Location = new System.Drawing.Point(15, 28);
            this.lblTenPhong.Name = "lblTenPhong";
            this.lblTenPhong.Size = new System.Drawing.Size(189, 20);
            this.lblTenPhong.TabIndex = 0;
            this.lblTenPhong.Text = "Phòng: (Chưa chọn phòng)";
            // 
            // tabLichSu
            // 
            this.tabLichSu.Controls.Add(this.dgvLichSu);
            this.tabLichSu.Controls.Add(this.panelLichSuFilter);
            this.tabLichSu.Location = new System.Drawing.Point(4, 26);
            this.tabLichSu.Name = "tabLichSu";
            this.tabLichSu.Padding = new System.Windows.Forms.Padding(3);
            this.tabLichSu.Size = new System.Drawing.Size(1016, 594);
            this.tabLichSu.TabIndex = 1;
            this.tabLichSu.Text = "  Lịch Sử Hóa Đơn  ";
            this.tabLichSu.UseVisualStyleBackColor = true;
            // 
            // dgvLichSu
            // 
            this.dgvLichSu.AllowUserToAddRows = false;
            this.dgvLichSu.AllowUserToDeleteRows = false;
            this.dgvLichSu.BackgroundColor = System.Drawing.Color.White;
            this.dgvLichSu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLichSu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLichSu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLichSu.Location = new System.Drawing.Point(3, 53);
            this.dgvLichSu.MultiSelect = false;
            this.dgvLichSu.Name = "dgvLichSu";
            this.dgvLichSu.ReadOnly = true;
            this.dgvLichSu.RowHeadersVisible = false;
            this.dgvLichSu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLichSu.Size = new System.Drawing.Size(1010, 538);
            this.dgvLichSu.TabIndex = 1;
            // 
            // panelLichSuFilter
            // 
            this.panelLichSuFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelLichSuFilter.Controls.Add(this.btnInLaiHD);
            this.panelLichSuFilter.Controls.Add(this.btnTimLichSu);
            this.panelLichSuFilter.Controls.Add(this.txtTimKiemHD);
            this.panelLichSuFilter.Controls.Add(this.lblTimKiemHD);
            this.panelLichSuFilter.Controls.Add(this.dtpDenNgay);
            this.panelLichSuFilter.Controls.Add(this.lblDenNgay);
            this.panelLichSuFilter.Controls.Add(this.dtpTuNgay);
            this.panelLichSuFilter.Controls.Add(this.lblTuNgay);
            this.panelLichSuFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLichSuFilter.Location = new System.Drawing.Point(3, 3);
            this.panelLichSuFilter.Name = "panelLichSuFilter";
            this.panelLichSuFilter.Size = new System.Drawing.Size(1010, 50);
            this.panelLichSuFilter.TabIndex = 0;
            // 
            // btnInLaiHD
            // 
            this.btnInLaiHD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInLaiHD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnInLaiHD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInLaiHD.FlatAppearance.BorderSize = 0;
            this.btnInLaiHD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInLaiHD.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnInLaiHD.ForeColor = System.Drawing.Color.White;
            this.btnInLaiHD.Location = new System.Drawing.Point(885, 10);
            this.btnInLaiHD.Name = "btnInLaiHD";
            this.btnInLaiHD.Size = new System.Drawing.Size(115, 30);
            this.btnInLaiHD.TabIndex = 7;
            this.btnInLaiHD.Text = "Xem & In Hóa Đơn";
            this.btnInLaiHD.UseVisualStyleBackColor = false;
            this.btnInLaiHD.Click += new System.EventHandler(this.btnInLaiHD_Click);
            // 
            // btnTimLichSu
            // 
            this.btnTimLichSu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.btnTimLichSu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimLichSu.FlatAppearance.BorderSize = 0;
            this.btnTimLichSu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimLichSu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTimLichSu.ForeColor = System.Drawing.Color.White;
            this.btnTimLichSu.Location = new System.Drawing.Point(695, 10);
            this.btnTimLichSu.Name = "btnTimLichSu";
            this.btnTimLichSu.Size = new System.Drawing.Size(85, 30);
            this.btnTimLichSu.TabIndex = 6;
            this.btnTimLichSu.Text = "Tìm kiếm";
            this.btnTimLichSu.UseVisualStyleBackColor = false;
            this.btnTimLichSu.Click += new System.EventHandler(this.btnTimLichSu_Click);
            // 
            // txtTimKiemHD
            // 
            this.txtTimKiemHD.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTimKiemHD.Location = new System.Drawing.Point(495, 13);
            this.txtTimKiemHD.Name = "txtTimKiemHD";
            this.txtTimKiemHD.Size = new System.Drawing.Size(185, 24);
            this.txtTimKiemHD.TabIndex = 5;
            // 
            // lblTimKiemHD
            // 
            this.lblTimKiemHD.AutoSize = true;
            this.lblTimKiemHD.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTimKiemHD.Location = new System.Drawing.Point(430, 16);
            this.lblTimKiemHD.Name = "lblTimKiemHD";
            this.lblTimKiemHD.Size = new System.Drawing.Size(62, 17);
            this.lblTimKiemHD.TabIndex = 4;
            this.lblTimKiemHD.Text = "Từ khóa:";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(285, 13);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(125, 24);
            this.dtpDenNgay.TabIndex = 3;
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDenNgay.Location = new System.Drawing.Point(215, 16);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(66, 17);
            this.lblDenNgay.TabIndex = 2;
            this.lblDenNgay.Text = "Đến ngày:";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgay.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new System.Drawing.Point(75, 13);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(125, 24);
            this.dtpTuNgay.TabIndex = 1;
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTuNgay.Location = new System.Drawing.Point(12, 16);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(58, 17);
            this.lblTuNgay.TabIndex = 0;
            this.lblTuNgay.Text = "Từ ngày:";
            // 
            // FrmThanhToan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 670);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmThanhToan";
            this.Text = "Thanh Toán & Hóa Đơn";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabThanhToan.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.panelLeftHeader.ResumeLayout(false);
            this.panelLeftHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhongDangHat)).EndInit();
            this.panelRightBilling.ResumeLayout(false);
            this.grpKetToan.ResumeLayout(false);
            this.grpKetToan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiamGia)).EndInit();
            this.grpDichVu.ResumeLayout(false);
            this.grpDichVu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).EndInit();
            this.grpThongTinPhong.ResumeLayout(false);
            this.grpThongTinPhong.PerformLayout();
            this.tabLichSu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).EndInit();
            this.panelLichSuFilter.ResumeLayout(false);
            this.panelLichSuFilter.PerformLayout();
            this.designColumn_dgvPhongDangHat_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvPhongDangHat_1.DataPropertyName = "TenPhong";
            this.designColumn_dgvPhongDangHat_1.HeaderText = "Phòng";
            this.designColumn_dgvPhongDangHat_1.Width = 110;
            this.designColumn_dgvPhongDangHat_1.Name = "designColumn_dgvPhongDangHat_1";
            this.dgvPhongDangHat.Columns.Add(this.designColumn_dgvPhongDangHat_1);
            this.designColumn_dgvPhongDangHat_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvPhongDangHat_2.DataPropertyName = "LoaiPhong";
            this.designColumn_dgvPhongDangHat_2.HeaderText = "Loại";
            this.designColumn_dgvPhongDangHat_2.Width = 90;
            this.designColumn_dgvPhongDangHat_2.Name = "designColumn_dgvPhongDangHat_2";
            this.dgvPhongDangHat.Columns.Add(this.designColumn_dgvPhongDangHat_2);
            this.designColumn_dgvPhongDangHat_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvPhongDangHat_3.DataPropertyName = "TienPhongTamTinh";
            this.designColumn_dgvPhongDangHat_3.HeaderText = "Tạm tính";
            this.designColumn_dgvPhongDangHat_3.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvPhongDangHat_3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.designColumn_dgvPhongDangHat_3.Name = "designColumn_dgvPhongDangHat_3";
            this.dgvPhongDangHat.Columns.Add(this.designColumn_dgvPhongDangHat_3);
            this.designColumn_dgvDichVu_4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvDichVu_4.DataPropertyName = "TenDV";
            this.designColumn_dgvDichVu_4.HeaderText = "Tên dịch vụ";
            this.designColumn_dgvDichVu_4.Width = 180;
            this.designColumn_dgvDichVu_4.Name = "designColumn_dgvDichVu_4";
            this.dgvDichVu.Columns.Add(this.designColumn_dgvDichVu_4);
            this.designColumn_dgvDichVu_5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvDichVu_5.DataPropertyName = "DonViTinh";
            this.designColumn_dgvDichVu_5.HeaderText = "ĐVT";
            this.designColumn_dgvDichVu_5.Width = 60;
            this.designColumn_dgvDichVu_5.Name = "designColumn_dgvDichVu_5";
            this.dgvDichVu.Columns.Add(this.designColumn_dgvDichVu_5);
            this.designColumn_dgvDichVu_6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvDichVu_6.DataPropertyName = "SoLuong";
            this.designColumn_dgvDichVu_6.HeaderText = "SL";
            this.designColumn_dgvDichVu_6.Width = 50;
            this.designColumn_dgvDichVu_6.Name = "designColumn_dgvDichVu_6";
            this.dgvDichVu.Columns.Add(this.designColumn_dgvDichVu_6);
            this.designColumn_dgvDichVu_7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvDichVu_7.DataPropertyName = "DonGia";
            this.designColumn_dgvDichVu_7.HeaderText = "Đơn giá";
            this.designColumn_dgvDichVu_7.Width = 110;
            this.designColumn_dgvDichVu_7.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvDichVu_7.Name = "designColumn_dgvDichVu_7";
            this.dgvDichVu.Columns.Add(this.designColumn_dgvDichVu_7);
            this.designColumn_dgvDichVu_8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvDichVu_8.DataPropertyName = "ThanhTien";
            this.designColumn_dgvDichVu_8.HeaderText = "Thành tiền";
            this.designColumn_dgvDichVu_8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.designColumn_dgvDichVu_8.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvDichVu_8.Name = "designColumn_dgvDichVu_8";
            this.dgvDichVu.Columns.Add(this.designColumn_dgvDichVu_8);
            this.designColumn_dgvLichSu_9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvLichSu_9.DataPropertyName = "MaHD";
            this.designColumn_dgvLichSu_9.HeaderText = "Mã HD";
            this.designColumn_dgvLichSu_9.Width = 70;
            this.designColumn_dgvLichSu_9.Name = "designColumn_dgvLichSu_9";
            this.dgvLichSu.Columns.Add(this.designColumn_dgvLichSu_9);
            this.designColumn_dgvLichSu_10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvLichSu_10.DataPropertyName = "NgayThanhToan";
            this.designColumn_dgvLichSu_10.HeaderText = "Ngày TT";
            this.designColumn_dgvLichSu_10.Width = 130;
            this.designColumn_dgvLichSu_10.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            this.designColumn_dgvLichSu_10.Name = "designColumn_dgvLichSu_10";
            this.dgvLichSu.Columns.Add(this.designColumn_dgvLichSu_10);
            this.designColumn_dgvLichSu_11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvLichSu_11.DataPropertyName = "TenPhong";
            this.designColumn_dgvLichSu_11.HeaderText = "Phòng";
            this.designColumn_dgvLichSu_11.Width = 90;
            this.designColumn_dgvLichSu_11.Name = "designColumn_dgvLichSu_11";
            this.dgvLichSu.Columns.Add(this.designColumn_dgvLichSu_11);
            this.designColumn_dgvLichSu_12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvLichSu_12.DataPropertyName = "TienPhong";
            this.designColumn_dgvLichSu_12.HeaderText = "Tiền phòng";
            this.designColumn_dgvLichSu_12.Width = 100;
            this.designColumn_dgvLichSu_12.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvLichSu_12.Name = "designColumn_dgvLichSu_12";
            this.dgvLichSu.Columns.Add(this.designColumn_dgvLichSu_12);
            this.designColumn_dgvLichSu_13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvLichSu_13.DataPropertyName = "TienDichVu";
            this.designColumn_dgvLichSu_13.HeaderText = "Tiền dịch vụ";
            this.designColumn_dgvLichSu_13.Width = 100;
            this.designColumn_dgvLichSu_13.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvLichSu_13.Name = "designColumn_dgvLichSu_13";
            this.dgvLichSu.Columns.Add(this.designColumn_dgvLichSu_13);
            this.designColumn_dgvLichSu_14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvLichSu_14.DataPropertyName = "GiamGia";
            this.designColumn_dgvLichSu_14.HeaderText = "Giảm giá";
            this.designColumn_dgvLichSu_14.Width = 90;
            this.designColumn_dgvLichSu_14.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvLichSu_14.Name = "designColumn_dgvLichSu_14";
            this.dgvLichSu.Columns.Add(this.designColumn_dgvLichSu_14);
            this.designColumn_dgvLichSu_15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvLichSu_15.DataPropertyName = "ThanhTien";
            this.designColumn_dgvLichSu_15.HeaderText = "Tổng thanh toán";
            this.designColumn_dgvLichSu_15.Width = 130;
            this.designColumn_dgvLichSu_15.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvLichSu_15.Name = "designColumn_dgvLichSu_15";
            this.dgvLichSu.Columns.Add(this.designColumn_dgvLichSu_15);
            this.designColumn_dgvLichSu_16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvLichSu_16.DataPropertyName = "PhuongThucTT";
            this.designColumn_dgvLichSu_16.HeaderText = "Hình thức";
            this.designColumn_dgvLichSu_16.Width = 100;
            this.designColumn_dgvLichSu_16.Name = "designColumn_dgvLichSu_16";
            this.dgvLichSu.Columns.Add(this.designColumn_dgvLichSu_16);
            this.designColumn_dgvLichSu_17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvLichSu_17.DataPropertyName = "TenNV";
            this.designColumn_dgvLichSu_17.HeaderText = "Thu ngân";
            this.designColumn_dgvLichSu_17.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.designColumn_dgvLichSu_17.Name = "designColumn_dgvLichSu_17";
            this.dgvLichSu.Columns.Add(this.designColumn_dgvLichSu_17);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabThanhToan;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Panel panelLeftHeader;
        private System.Windows.Forms.Label lblListPhongTitle;
        private System.Windows.Forms.Button btnLamMoiPhong;
        private System.Windows.Forms.DataGridView dgvPhongDangHat;
        private System.Windows.Forms.Panel panelRightBilling;
        private System.Windows.Forms.GroupBox grpThongTinPhong;
        private System.Windows.Forms.Label lblTenPhong;
        private System.Windows.Forms.Label lblGioVao;
        private System.Windows.Forms.Label lblGioRa;
        private System.Windows.Forms.Label lblThoiGianHat;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.Label lblTienPhongUocTinh;
        private System.Windows.Forms.GroupBox grpDichVu;
        private System.Windows.Forms.DataGridView dgvDichVu;
        private System.Windows.Forms.Label lblTongTienDVGrid;
        private System.Windows.Forms.GroupBox grpKetToan;
        private System.Windows.Forms.Label lblSumTienPhong;
        private System.Windows.Forms.Label lblSumTienPhongVal;
        private System.Windows.Forms.Label lblSumTienDV;
        private System.Windows.Forms.Label lblSumTienDVVal;
        private System.Windows.Forms.Label lblGiamGia;
        private System.Windows.Forms.NumericUpDown numGiamGia;
        private System.Windows.Forms.Label lblThanhTien;
        private System.Windows.Forms.Label lblThanhTienVal;
        private System.Windows.Forms.Label lblPhuongThuc;
        private System.Windows.Forms.ComboBox cboPhuongThuc;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.Button btnInHoaDon;
        private System.Windows.Forms.Label lblTienKhachDua;
        private System.Windows.Forms.TextBox txtTienKhachDua;
        private System.Windows.Forms.Label lblTienThua;
        private System.Windows.Forms.Label lblTienThuaVal;
        private System.Windows.Forms.TabPage tabLichSu;
        private System.Windows.Forms.Panel panelLichSuFilter;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label lblTimKiemHD;
        private System.Windows.Forms.TextBox txtTimKiemHD;
        private System.Windows.Forms.Button btnTimLichSu;
        private System.Windows.Forms.Button btnInLaiHD;
        private System.Windows.Forms.DataGridView dgvLichSu;

        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvPhongDangHat_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvPhongDangHat_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvPhongDangHat_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvDichVu_4;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvDichVu_5;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvDichVu_6;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvDichVu_7;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvDichVu_8;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvLichSu_9;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvLichSu_10;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvLichSu_11;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvLichSu_12;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvLichSu_13;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvLichSu_14;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvLichSu_15;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvLichSu_16;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvLichSu_17;
    }
}
