namespace karaoke
{
    partial class FrmDatPhong
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
            this.panelSearch = new System.Windows.Forms.Panel();
            this.btnTimPhong = new System.Windows.Forms.Button();
            this.cbLoaiPhong = new System.Windows.Forms.ComboBox();
            this.labelLoaiPhong = new System.Windows.Forms.Label();
            this.numSoKhachTim = new System.Windows.Forms.NumericUpDown();
            this.labelSoKhachTim = new System.Windows.Forms.Label();
            this.dtpKetThuc = new System.Windows.Forms.DateTimePicker();
            this.labelKetThuc = new System.Windows.Forms.Label();
            this.dtpBatDau = new System.Windows.Forms.DateTimePicker();
            this.labelBatDau = new System.Windows.Forms.Label();
            this.lblSearchHeader = new System.Windows.Forms.Label();
            this.panelRooms = new System.Windows.Forms.Panel();
            this.dgvPhongTrong = new System.Windows.Forms.DataGridView();
            this.lblRoomsHeader = new System.Windows.Forms.Label();
            this.panelForm = new System.Windows.Forms.Panel();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.labelGhiChu = new System.Windows.Forms.Label();
            this.lblDonGiaChot = new System.Windows.Forms.Label();
            this.labelDonGia = new System.Windows.Forms.Label();
            this.numSoKhach = new System.Windows.Forms.NumericUpDown();
            this.labelSoKhach = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.labelSDT = new System.Windows.Forms.Label();
            this.txtTenKhach = new System.Windows.Forms.TextBox();
            this.labelTenKhach = new System.Windows.Forms.Label();
            this.txtTenPhong = new System.Windows.Forms.TextBox();
            this.labelTenPhong = new System.Windows.Forms.Label();
            this.lblFormHeader = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoKhachTim)).BeginInit();
            this.panelRooms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhongTrong)).BeginInit();
            this.panelForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoKhach)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(940, 50);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(16, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(199, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ĐẶT PHÒNG HÁT";
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.White;
            this.panelSearch.Controls.Add(this.btnTimPhong);
            this.panelSearch.Controls.Add(this.cbLoaiPhong);
            this.panelSearch.Controls.Add(this.labelLoaiPhong);
            this.panelSearch.Controls.Add(this.numSoKhachTim);
            this.panelSearch.Controls.Add(this.labelSoKhachTim);
            this.panelSearch.Controls.Add(this.dtpKetThuc);
            this.panelSearch.Controls.Add(this.labelKetThuc);
            this.panelSearch.Controls.Add(this.dtpBatDau);
            this.panelSearch.Controls.Add(this.labelBatDau);
            this.panelSearch.Controls.Add(this.lblSearchHeader);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 50);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(940, 90);
            this.panelSearch.TabIndex = 1;
            // 
            // btnTimPhong
            // 
            this.btnTimPhong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTimPhong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnTimPhong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimPhong.FlatAppearance.BorderSize = 0;
            this.btnTimPhong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimPhong.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimPhong.ForeColor = System.Drawing.Color.White;
            this.btnTimPhong.Location = new System.Drawing.Point(800, 40);
            this.btnTimPhong.Name = "btnTimPhong";
            this.btnTimPhong.Size = new System.Drawing.Size(125, 32);
            this.btnTimPhong.TabIndex = 9;
            this.btnTimPhong.Text = "Tìm phòng";
            this.btnTimPhong.UseVisualStyleBackColor = false;
            this.btnTimPhong.Click += new System.EventHandler(this.btnTimPhong_Click);
            // 
            // cbLoaiPhong
            // 
            this.cbLoaiPhong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLoaiPhong.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLoaiPhong.FormattingEnabled = true;
            this.cbLoaiPhong.Location = new System.Drawing.Point(620, 44);
            this.cbLoaiPhong.Name = "cbLoaiPhong";
            this.cbLoaiPhong.Size = new System.Drawing.Size(160, 25);
            this.cbLoaiPhong.TabIndex = 8;
            // 
            // labelLoaiPhong
            // 
            this.labelLoaiPhong.AutoSize = true;
            this.labelLoaiPhong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLoaiPhong.Location = new System.Drawing.Point(617, 26);
            this.labelLoaiPhong.Name = "labelLoaiPhong";
            this.labelLoaiPhong.Size = new System.Drawing.Size(69, 15);
            this.labelLoaiPhong.TabIndex = 7;
            this.labelLoaiPhong.Text = "Loại phòng:";
            // 
            // numSoKhachTim
            // 
            this.numSoKhachTim.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSoKhachTim.Location = new System.Drawing.Point(500, 44);
            this.numSoKhachTim.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.numSoKhachTim.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numSoKhachTim.Name = "numSoKhachTim";
            this.numSoKhachTim.Size = new System.Drawing.Size(100, 25);
            this.numSoKhachTim.TabIndex = 6;
            this.numSoKhachTim.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // labelSoKhachTim
            // 
            this.labelSoKhachTim.AutoSize = true;
            this.labelSoKhachTim.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSoKhachTim.Location = new System.Drawing.Point(497, 26);
            this.labelSoKhachTim.Name = "labelSoKhachTim";
            this.labelSoKhachTim.Size = new System.Drawing.Size(58, 15);
            this.labelSoKhachTim.TabIndex = 5;
            this.labelSoKhachTim.Text = "Số khách:";
            // 
            // dtpKetThuc
            // 
            this.dtpKetThuc.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpKetThuc.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpKetThuc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpKetThuc.Location = new System.Drawing.Point(260, 44);
            this.dtpKetThuc.Name = "dtpKetThuc";
            this.dtpKetThuc.Size = new System.Drawing.Size(220, 25);
            this.dtpKetThuc.TabIndex = 4;
            // 
            // labelKetThuc
            // 
            this.labelKetThuc.AutoSize = true;
            this.labelKetThuc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelKetThuc.Location = new System.Drawing.Point(257, 26);
            this.labelKetThuc.Name = "labelKetThuc";
            this.labelKetThuc.Size = new System.Drawing.Size(103, 15);
            this.labelKetThuc.TabIndex = 3;
            this.labelKetThuc.Text = "Kết thúc dự kiến:";
            // 
            // dtpBatDau
            // 
            this.dtpBatDau.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpBatDau.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBatDau.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBatDau.Location = new System.Drawing.Point(20, 44);
            this.dtpBatDau.Name = "dtpBatDau";
            this.dtpBatDau.Size = new System.Drawing.Size(220, 25);
            this.dtpBatDau.TabIndex = 2;
            // 
            // labelBatDau
            // 
            this.labelBatDau.AutoSize = true;
            this.labelBatDau.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBatDau.Location = new System.Drawing.Point(17, 26);
            this.labelBatDau.Name = "labelBatDau";
            this.labelBatDau.Size = new System.Drawing.Size(100, 15);
            this.labelBatDau.TabIndex = 1;
            this.labelBatDau.Text = "Bắt đầu dự kiến:";
            // 
            // lblSearchHeader
            // 
            this.lblSearchHeader.AutoSize = true;
            this.lblSearchHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblSearchHeader.Location = new System.Drawing.Point(17, 5);
            this.lblSearchHeader.Name = "lblSearchHeader";
            this.lblSearchHeader.Size = new System.Drawing.Size(193, 19);
            this.lblSearchHeader.TabIndex = 0;
            this.lblSearchHeader.Text = "1. Tìm kiếm phòng khả dụng";
            // 
            // panelRooms
            // 
            this.panelRooms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRooms.BackColor = System.Drawing.Color.White;
            this.panelRooms.Controls.Add(this.dgvPhongTrong);
            this.panelRooms.Controls.Add(this.lblRoomsHeader);
            this.panelRooms.Location = new System.Drawing.Point(12, 148);
            this.panelRooms.Name = "panelRooms";
            this.panelRooms.Size = new System.Drawing.Size(520, 460);
            this.panelRooms.TabIndex = 2;
            // 
            // dgvPhongTrong
            // 
            this.dgvPhongTrong.AllowUserToAddRows = false;
            this.dgvPhongTrong.AllowUserToDeleteRows = false;
            this.dgvPhongTrong.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPhongTrong.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvPhongTrong.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPhongTrong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhongTrong.Location = new System.Drawing.Point(10, 35);
            this.dgvPhongTrong.MultiSelect = false;
            this.dgvPhongTrong.Name = "dgvPhongTrong";
            this.dgvPhongTrong.ReadOnly = true;
            this.dgvPhongTrong.RowHeadersVisible = false;
            this.dgvPhongTrong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhongTrong.Size = new System.Drawing.Size(500, 415);
            this.dgvPhongTrong.TabIndex = 1;
            this.dgvPhongTrong.SelectionChanged += new System.EventHandler(this.dgvPhongTrong_SelectionChanged);
            // 
            // lblRoomsHeader
            // 
            this.lblRoomsHeader.AutoSize = true;
            this.lblRoomsHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomsHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblRoomsHeader.Location = new System.Drawing.Point(10, 10);
            this.lblRoomsHeader.Name = "lblRoomsHeader";
            this.lblRoomsHeader.Size = new System.Drawing.Size(217, 19);
            this.lblRoomsHeader.TabIndex = 0;
            this.lblRoomsHeader.Text = "2. Chọn phòng trống thỏa điều kiện";
            // 
            // panelForm
            // 
            this.panelForm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelForm.BackColor = System.Drawing.Color.White;
            this.panelForm.Controls.Add(this.btnHuy);
            this.panelForm.Controls.Add(this.btnLuu);
            this.panelForm.Controls.Add(this.txtGhiChu);
            this.panelForm.Controls.Add(this.labelGhiChu);
            this.panelForm.Controls.Add(this.lblDonGiaChot);
            this.panelForm.Controls.Add(this.labelDonGia);
            this.panelForm.Controls.Add(this.numSoKhach);
            this.panelForm.Controls.Add(this.labelSoKhach);
            this.panelForm.Controls.Add(this.txtSDT);
            this.panelForm.Controls.Add(this.labelSDT);
            this.panelForm.Controls.Add(this.txtTenKhach);
            this.panelForm.Controls.Add(this.labelTenKhach);
            this.panelForm.Controls.Add(this.txtTenPhong);
            this.panelForm.Controls.Add(this.labelTenPhong);
            this.panelForm.Controls.Add(this.lblFormHeader);
            this.panelForm.Location = new System.Drawing.Point(542, 148);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(386, 460);
            this.panelForm.TabIndex = 3;
            // 
            // btnHuy
            // 
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuy.BackColor = System.Drawing.Color.Gainsboro;
            this.btnHuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnHuy.Location = new System.Drawing.Point(140, 412);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(100, 36);
            this.btnHuy.TabIndex = 14;
            this.btnHuy.Text = "Hủy bỏ";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(250, 412);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(120, 36);
            this.btnLuu.TabIndex = 13;
            this.btnLuu.Text = "Xác nhận";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGhiChu.Location = new System.Drawing.Point(20, 276);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGhiChu.Size = new System.Drawing.Size(350, 120);
            this.txtGhiChu.TabIndex = 12;
            // 
            // labelGhiChu
            // 
            this.labelGhiChu.AutoSize = true;
            this.labelGhiChu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGhiChu.Location = new System.Drawing.Point(17, 258);
            this.labelGhiChu.Name = "labelGhiChu";
            this.labelGhiChu.Size = new System.Drawing.Size(51, 15);
            this.labelGhiChu.TabIndex = 11;
            this.labelGhiChu.Text = "Ghi chú:";
            // 
            // lblDonGiaChot
            // 
            this.lblDonGiaChot.AutoSize = true;
            this.lblDonGiaChot.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDonGiaChot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblDonGiaChot.Location = new System.Drawing.Point(170, 226);
            this.lblDonGiaChot.Name = "lblDonGiaChot";
            this.lblDonGiaChot.Size = new System.Drawing.Size(57, 21);
            this.lblDonGiaChot.TabIndex = 10;
            this.lblDonGiaChot.Text = "0 đ/giờ";
            // 
            // labelDonGia
            // 
            this.labelDonGia.AutoSize = true;
            this.labelDonGia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDonGia.Location = new System.Drawing.Point(170, 204);
            this.labelDonGia.Name = "labelDonGia";
            this.labelDonGia.Size = new System.Drawing.Size(102, 15);
            this.labelDonGia.TabIndex = 9;
            this.labelDonGia.Text = "Đơn giá giờ chốt:";
            // 
            // numSoKhach
            // 
            this.numSoKhach.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSoKhach.Location = new System.Drawing.Point(20, 224);
            this.numSoKhach.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.numSoKhach.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numSoKhach.Name = "numSoKhach";
            this.numSoKhach.Size = new System.Drawing.Size(120, 25);
            this.numSoKhach.TabIndex = 8;
            this.numSoKhach.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // labelSoKhach
            // 
            this.labelSoKhach.AutoSize = true;
            this.labelSoKhach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSoKhach.Location = new System.Drawing.Point(17, 204);
            this.labelSoKhach.Name = "labelSoKhach";
            this.labelSoKhach.Size = new System.Drawing.Size(58, 15);
            this.labelSoKhach.TabIndex = 7;
            this.labelSoKhach.Text = "Số khách:";
            // 
            // txtSDT
            // 
            this.txtSDT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSDT.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSDT.Location = new System.Drawing.Point(20, 168);
            this.txtSDT.MaxLength = 15;
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(350, 25);
            this.txtSDT.TabIndex = 6;
            // 
            // labelSDT
            // 
            this.labelSDT.AutoSize = true;
            this.labelSDT.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSDT.Location = new System.Drawing.Point(17, 150);
            this.labelSDT.Name = "labelSDT";
            this.labelSDT.Size = new System.Drawing.Size(81, 15);
            this.labelSDT.TabIndex = 5;
            this.labelSDT.Text = "Số điện thoại:";
            // 
            // txtTenKhach
            // 
            this.txtTenKhach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTenKhach.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenKhach.Location = new System.Drawing.Point(20, 114);
            this.txtTenKhach.MaxLength = 100;
            this.txtTenKhach.Name = "txtTenKhach";
            this.txtTenKhach.Size = new System.Drawing.Size(350, 25);
            this.txtTenKhach.TabIndex = 4;
            // 
            // labelTenKhach
            // 
            this.labelTenKhach.AutoSize = true;
            this.labelTenKhach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTenKhach.Location = new System.Drawing.Point(17, 96);
            this.labelTenKhach.Name = "labelTenKhach";
            this.labelTenKhach.Size = new System.Drawing.Size(95, 15);
            this.labelTenKhach.TabIndex = 3;
            this.labelTenKhach.Text = "Tên khách hàng:";
            // 
            // txtTenPhong
            // 
            this.txtTenPhong.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTenPhong.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTenPhong.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenPhong.Location = new System.Drawing.Point(20, 60);
            this.txtTenPhong.Name = "txtTenPhong";
            this.txtTenPhong.ReadOnly = true;
            this.txtTenPhong.Size = new System.Drawing.Size(350, 25);
            this.txtTenPhong.TabIndex = 2;
            // 
            // labelTenPhong
            // 
            this.labelTenPhong.AutoSize = true;
            this.labelTenPhong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTenPhong.Location = new System.Drawing.Point(17, 42);
            this.labelTenPhong.Name = "labelTenPhong";
            this.labelTenPhong.Size = new System.Drawing.Size(90, 15);
            this.labelTenPhong.TabIndex = 1;
            this.labelTenPhong.Text = "Phòng đã chọn:";
            // 
            // lblFormHeader
            // 
            this.lblFormHeader.AutoSize = true;
            this.lblFormHeader.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblFormHeader.Location = new System.Drawing.Point(17, 10);
            this.lblFormHeader.Name = "lblFormHeader";
            this.lblFormHeader.Size = new System.Drawing.Size(185, 19);
            this.lblFormHeader.TabIndex = 0;
            this.lblFormHeader.Text = "3. Thông tin người đặt phòng";
            // 
            // FrmDatPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(940, 620);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.panelRooms);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "FrmDatPhong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Đặt Phòng Karaoke";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoKhachTim)).EndInit();
            this.panelRooms.ResumeLayout(false);
            this.panelRooms.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhongTrong)).EndInit();
            this.panelForm.ResumeLayout(false);
            this.panelForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoKhach)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Label lblSearchHeader;
        private System.Windows.Forms.Label labelBatDau;
        private System.Windows.Forms.DateTimePicker dtpBatDau;
        private System.Windows.Forms.Label labelKetThuc;
        private System.Windows.Forms.DateTimePicker dtpKetThuc;
        private System.Windows.Forms.Label labelSoKhachTim;
        private System.Windows.Forms.NumericUpDown numSoKhachTim;
        private System.Windows.Forms.Label labelLoaiPhong;
        private System.Windows.Forms.ComboBox cbLoaiPhong;
        private System.Windows.Forms.Button btnTimPhong;
        private System.Windows.Forms.Panel panelRooms;
        private System.Windows.Forms.Label lblRoomsHeader;
        private System.Windows.Forms.DataGridView dgvPhongTrong;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Label lblFormHeader;
        private System.Windows.Forms.Label labelTenPhong;
        private System.Windows.Forms.TextBox txtTenPhong;
        private System.Windows.Forms.Label labelTenKhach;
        private System.Windows.Forms.TextBox txtTenKhach;
        private System.Windows.Forms.Label labelSDT;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label labelSoKhach;
        private System.Windows.Forms.NumericUpDown numSoKhach;
        private System.Windows.Forms.Label labelDonGia;
        private System.Windows.Forms.Label lblDonGiaChot;
        private System.Windows.Forms.Label labelGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
    }
}
