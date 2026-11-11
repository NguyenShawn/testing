namespace karaoke
{
    partial class FrmDanhSachDatPhong
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
            this.components = new System.ComponentModel.Container();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.chkLocNgay = new System.Windows.Forms.CheckBox();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.labelDenNgay = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.labelTuNgay = new System.Windows.Forms.Label();
            this.cbTrangThai = new System.Windows.Forms.ComboBox();
            this.labelTrangThai = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.labelTimKiem = new System.Windows.Forms.Label();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnNhanPhong = new System.Windows.Forms.Button();
            this.btnHuyDat = new System.Windows.Forms.Button();
            this.btnSuaDat = new System.Windows.Forms.Button();
            this.btnThemDat = new System.Windows.Forms.Button();
            this.lblTongSo = new System.Windows.Forms.Label();
            this.dgvDanhSach = new System.Windows.Forms.DataGridView();
            this.panelHeader.SuspendLayout();
            this.panelFilter.SuspendLayout();
            this.panelActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSach)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1000, 48);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(16, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(236, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ ĐẶT PHÒNG HÁT";
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.White;
            this.panelFilter.Controls.Add(this.chkLocNgay);
            this.panelFilter.Controls.Add(this.dtpDenNgay);
            this.panelFilter.Controls.Add(this.labelDenNgay);
            this.panelFilter.Controls.Add(this.dtpTuNgay);
            this.panelFilter.Controls.Add(this.labelTuNgay);
            this.panelFilter.Controls.Add(this.cbTrangThai);
            this.panelFilter.Controls.Add(this.labelTrangThai);
            this.panelFilter.Controls.Add(this.txtTimKiem);
            this.panelFilter.Controls.Add(this.labelTimKiem);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 48);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1000, 68);
            this.panelFilter.TabIndex = 1;
            // 
            // chkLocNgay
            // 
            this.chkLocNgay.AutoSize = true;
            this.chkLocNgay.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLocNgay.Location = new System.Drawing.Point(365, 8);
            this.chkLocNgay.Name = "chkLocNgay";
            this.chkLocNgay.Size = new System.Drawing.Size(95, 17);
            this.chkLocNgay.TabIndex = 4;
            this.chkLocNgay.Text = "Lọc theo ngày";
            this.chkLocNgay.UseVisualStyleBackColor = true;
            this.chkLocNgay.CheckedChanged += new System.EventHandler(this.chkLocNgay_CheckedChanged);
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgay.Enabled = false;
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(525, 27);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(130, 23);
            this.dtpDenNgay.TabIndex = 8;
            // 
            // labelDenNgay
            // 
            this.labelDenNgay.AutoSize = true;
            this.labelDenNgay.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDenNgay.Location = new System.Drawing.Point(522, 10);
            this.labelDenNgay.Name = "labelDenNgay";
            this.labelDenNgay.Size = new System.Drawing.Size(59, 13);
            this.labelDenNgay.TabIndex = 7;
            this.labelDenNgay.Text = "Đến ngày:";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgay.Enabled = false;
            this.dtpTuNgay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new System.Drawing.Point(365, 27);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(140, 23);
            this.dtpTuNgay.TabIndex = 6;
            // 
            // labelTuNgay
            // 
            this.labelTuNgay.AutoSize = true;
            this.labelTuNgay.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTuNgay.Location = new System.Drawing.Point(462, 10);
            this.labelTuNgay.Name = "labelTuNgay";
            this.labelTuNgay.Size = new System.Drawing.Size(52, 13);
            this.labelTuNgay.TabIndex = 5;
            this.labelTuNgay.Text = "Từ ngày:";
            // 
            // cbTrangThai
            // 
            this.cbTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTrangThai.FormattingEnabled = true;
            this.cbTrangThai.Location = new System.Drawing.Point(215, 27);
            this.cbTrangThai.Name = "cbTrangThai";
            this.cbTrangThai.Size = new System.Drawing.Size(130, 23);
            this.cbTrangThai.TabIndex = 3;
            // 
            // labelTrangThai
            // 
            this.labelTrangThai.AutoSize = true;
            this.labelTrangThai.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTrangThai.Location = new System.Drawing.Point(212, 10);
            this.labelTrangThai.Name = "labelTrangThai";
            this.labelTrangThai.Size = new System.Drawing.Size(61, 13);
            this.labelTrangThai.TabIndex = 2;
            this.labelTrangThai.Text = "Trạng thái:";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimKiem.Location = new System.Drawing.Point(18, 27);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(180, 23);
            this.txtTimKiem.TabIndex = 1;
            // 
            // labelTimKiem
            // 
            this.labelTimKiem.AutoSize = true;
            this.labelTimKiem.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimKiem.Location = new System.Drawing.Point(15, 10);
            this.labelTimKiem.Name = "labelTimKiem";
            this.labelTimKiem.Size = new System.Drawing.Size(89, 13);
            this.labelTimKiem.TabIndex = 0;
            this.labelTimKiem.Text = "Tên khách / SĐT:";
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panelActions.Controls.Add(this.btnNhanPhong);
            this.panelActions.Controls.Add(this.btnHuyDat);
            this.panelActions.Controls.Add(this.btnSuaDat);
            this.panelActions.Controls.Add(this.btnThemDat);
            this.panelActions.Controls.Add(this.lblTongSo);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelActions.Location = new System.Drawing.Point(0, 116);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(1000, 48);
            this.panelActions.TabIndex = 3;
            // 
            // btnNhanPhong
            // 
            this.btnNhanPhong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNhanPhong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnNhanPhong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNhanPhong.FlatAppearance.BorderSize = 0;
            this.btnNhanPhong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhanPhong.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNhanPhong.ForeColor = System.Drawing.Color.White;
            this.btnNhanPhong.Location = new System.Drawing.Point(825, 8);
            this.btnNhanPhong.Name = "btnNhanPhong";
            this.btnNhanPhong.Size = new System.Drawing.Size(160, 32);
            this.btnNhanPhong.TabIndex = 4;
            this.btnNhanPhong.Text = "Nhận phòng (Check-in)";
            this.btnNhanPhong.UseVisualStyleBackColor = false;
            this.btnNhanPhong.Click += new System.EventHandler(this.btnNhanPhong_Click);
            // 
            // btnHuyDat
            // 
            this.btnHuyDat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuyDat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnHuyDat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuyDat.FlatAppearance.BorderSize = 0;
            this.btnHuyDat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuyDat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuyDat.ForeColor = System.Drawing.Color.White;
            this.btnHuyDat.Location = new System.Drawing.Point(710, 8);
            this.btnHuyDat.Name = "btnHuyDat";
            this.btnHuyDat.Size = new System.Drawing.Size(105, 32);
            this.btnHuyDat.TabIndex = 3;
            this.btnHuyDat.Text = "Hủy đặt phòng";
            this.btnHuyDat.UseVisualStyleBackColor = false;
            this.btnHuyDat.Click += new System.EventHandler(this.btnHuyDat_Click);
            // 
            // btnSuaDat
            // 
            this.btnSuaDat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSuaDat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnSuaDat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSuaDat.FlatAppearance.BorderSize = 0;
            this.btnSuaDat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuaDat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuaDat.ForeColor = System.Drawing.Color.White;
            this.btnSuaDat.Location = new System.Drawing.Point(600, 8);
            this.btnSuaDat.Name = "btnSuaDat";
            this.btnSuaDat.Size = new System.Drawing.Size(100, 32);
            this.btnSuaDat.TabIndex = 2;
            this.btnSuaDat.Text = "Chỉnh sửa";
            this.btnSuaDat.UseVisualStyleBackColor = false;
            this.btnSuaDat.Click += new System.EventHandler(this.btnSuaDat_Click);
            // 
            // btnThemDat
            // 
            this.btnThemDat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemDat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.btnThemDat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemDat.FlatAppearance.BorderSize = 0;
            this.btnThemDat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemDat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemDat.ForeColor = System.Drawing.Color.White;
            this.btnThemDat.Location = new System.Drawing.Point(460, 8);
            this.btnThemDat.Name = "btnThemDat";
            this.btnThemDat.Size = new System.Drawing.Size(130, 32);
            this.btnThemDat.TabIndex = 1;
            this.btnThemDat.Text = "+ Đặt phòng mới";
            this.btnThemDat.UseVisualStyleBackColor = false;
            this.btnThemDat.Click += new System.EventHandler(this.btnThemDat_Click);
            // 
            // lblTongSo
            // 
            this.lblTongSo.AutoSize = true;
            this.lblTongSo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongSo.Location = new System.Drawing.Point(15, 17);
            this.lblTongSo.Name = "lblTongSo";
            this.lblTongSo.Size = new System.Drawing.Size(124, 15);
            this.lblTongSo.TabIndex = 0;
            this.lblTongSo.Text = "Tổng số phiếu đặt: 0";
            // 
            // dgvDanhSach
            // 
            this.dgvDanhSach.AllowUserToAddRows = false;
            this.dgvDanhSach.AllowUserToDeleteRows = false;
            this.dgvDanhSach.BackgroundColor = System.Drawing.Color.White;
            this.dgvDanhSach.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDanhSach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDanhSach.Location = new System.Drawing.Point(0, 164);
            this.dgvDanhSach.MultiSelect = false;
            this.dgvDanhSach.Name = "dgvDanhSach";
            this.dgvDanhSach.ReadOnly = true;
            this.dgvDanhSach.RowHeadersVisible = false;
            this.dgvDanhSach.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhSach.Size = new System.Drawing.Size(1000, 436);
            this.dgvDanhSach.TabIndex = 2;
            this.dgvDanhSach.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDanhSach_CellFormatting);
            this.dgvDanhSach.DoubleClick += new System.EventHandler(this.dgvDanhSach_DoubleClick);
            // 
            // FrmDanhSachDatPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.dgvDanhSach);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmDanhSachDatPhong";
            this.Text = "Danh Sách Đặt Phòng";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.panelActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSach)).EndInit();
            this.designColumn_dgvDanhSach_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvDanhSach_1.DataPropertyName = "MaDatPhong";
            this.designColumn_dgvDanhSach_1.HeaderText = "Mã đặt";
            this.designColumn_dgvDanhSach_1.Width = 70;
            this.designColumn_dgvDanhSach_1.Name = "designColumn_dgvDanhSach_1";
            this.dgvDanhSach.Columns.Add(this.designColumn_dgvDanhSach_1);
            this.designColumn_dgvDanhSach_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvDanhSach_2.DataPropertyName = "TenPhong";
            this.designColumn_dgvDanhSach_2.HeaderText = "Phòng";
            this.designColumn_dgvDanhSach_2.Width = 100;
            this.designColumn_dgvDanhSach_2.Name = "designColumn_dgvDanhSach_2";
            this.dgvDanhSach.Columns.Add(this.designColumn_dgvDanhSach_2);
            this.designColumn_dgvDanhSach_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvDanhSach_3.DataPropertyName = "LoaiPhong";
            this.designColumn_dgvDanhSach_3.HeaderText = "Loại phòng";
            this.designColumn_dgvDanhSach_3.Width = 100;
            this.designColumn_dgvDanhSach_3.Name = "designColumn_dgvDanhSach_3";
            this.dgvDanhSach.Columns.Add(this.designColumn_dgvDanhSach_3);
            this.designColumn_dgvDanhSach_4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvDanhSach_4.DataPropertyName = "TenKhach";
            this.designColumn_dgvDanhSach_4.HeaderText = "Khách hàng";
            this.designColumn_dgvDanhSach_4.Width = 140;
            this.designColumn_dgvDanhSach_4.Name = "designColumn_dgvDanhSach_4";
            this.dgvDanhSach.Columns.Add(this.designColumn_dgvDanhSach_4);
            this.designColumn_dgvDanhSach_5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvDanhSach_5.DataPropertyName = "SDT";
            this.designColumn_dgvDanhSach_5.HeaderText = "SĐT";
            this.designColumn_dgvDanhSach_5.Width = 100;
            this.designColumn_dgvDanhSach_5.Name = "designColumn_dgvDanhSach_5";
            this.dgvDanhSach.Columns.Add(this.designColumn_dgvDanhSach_5);
            this.designColumn_dgvDanhSach_6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvDanhSach_6.DataPropertyName = "SoKhach";
            this.designColumn_dgvDanhSach_6.HeaderText = "Số khách";
            this.designColumn_dgvDanhSach_6.Width = 80;
            this.designColumn_dgvDanhSach_6.Name = "designColumn_dgvDanhSach_6";
            this.dgvDanhSach.Columns.Add(this.designColumn_dgvDanhSach_6);
            this.designColumn_dgvDanhSach_7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvDanhSach_7.DataPropertyName = "GioBatDauDuKien";
            this.designColumn_dgvDanhSach_7.HeaderText = "Bắt đầu dự kiến";
            this.designColumn_dgvDanhSach_7.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            this.designColumn_dgvDanhSach_7.Width = 135;
            this.designColumn_dgvDanhSach_7.Name = "designColumn_dgvDanhSach_7";
            this.dgvDanhSach.Columns.Add(this.designColumn_dgvDanhSach_7);
            this.designColumn_dgvDanhSach_8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvDanhSach_8.DataPropertyName = "GioKetThucDuKien";
            this.designColumn_dgvDanhSach_8.HeaderText = "Kết thúc dự kiến";
            this.designColumn_dgvDanhSach_8.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            this.designColumn_dgvDanhSach_8.Width = 135;
            this.designColumn_dgvDanhSach_8.Name = "designColumn_dgvDanhSach_8";
            this.dgvDanhSach.Columns.Add(this.designColumn_dgvDanhSach_8);
            this.designColumn_dgvDanhSach_9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvDanhSach_9.DataPropertyName = "DonGiaGioDaChot";
            this.designColumn_dgvDanhSach_9.HeaderText = "Đơn giá/h";
            this.designColumn_dgvDanhSach_9.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvDanhSach_9.Width = 95;
            this.designColumn_dgvDanhSach_9.Name = "designColumn_dgvDanhSach_9";
            this.dgvDanhSach.Columns.Add(this.designColumn_dgvDanhSach_9);
            this.designColumn_dgvDanhSach_10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvDanhSach_10.DataPropertyName = "TrangThai";
            this.designColumn_dgvDanhSach_10.HeaderText = "Trạng thái";
            this.designColumn_dgvDanhSach_10.Width = 110;
            this.designColumn_dgvDanhSach_10.Name = "designColumn_dgvDanhSach_10";
            this.dgvDanhSach.Columns.Add(this.designColumn_dgvDanhSach_10);
            this.designColumn_dgvDanhSach_11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvDanhSach_11.DataPropertyName = "TenNV";
            this.designColumn_dgvDanhSach_11.HeaderText = "NV tiếp nhận";
            this.designColumn_dgvDanhSach_11.Width = 110;
            this.designColumn_dgvDanhSach_11.Name = "designColumn_dgvDanhSach_11";
            this.dgvDanhSach.Columns.Add(this.designColumn_dgvDanhSach_11);
            this.designColumn_dgvDanhSach_12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvDanhSach_12.DataPropertyName = "GhiChu";
            this.designColumn_dgvDanhSach_12.HeaderText = "Ghi chú";
            this.designColumn_dgvDanhSach_12.Width = 100;
            this.designColumn_dgvDanhSach_12.Name = "designColumn_dgvDanhSach_12";
            this.dgvDanhSach.Columns.Add(this.designColumn_dgvDanhSach_12);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Label labelTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label labelTrangThai;
        private System.Windows.Forms.ComboBox cbTrangThai;
        private System.Windows.Forms.CheckBox chkLocNgay;
        private System.Windows.Forms.Label labelTuNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label labelDenNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.DataGridView dgvDanhSach;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Label lblTongSo;
        private System.Windows.Forms.Button btnThemDat;
        private System.Windows.Forms.Button btnSuaDat;
        private System.Windows.Forms.Button btnHuyDat;
        private System.Windows.Forms.Button btnNhanPhong;

        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvDanhSach_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvDanhSach_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvDanhSach_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvDanhSach_4;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvDanhSach_5;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvDanhSach_6;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvDanhSach_7;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvDanhSach_8;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvDanhSach_9;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvDanhSach_10;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvDanhSach_11;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvDanhSach_12;
    }
}
