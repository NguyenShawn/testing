namespace karaoke
{
    partial class FrmThongKe
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.btnXuatBaoCao = new System.Windows.Forms.Button();
            this.btnXemThongKe = new System.Windows.Forms.Button();
            this.btnNamNay = new System.Windows.Forms.Button();
            this.btnThangNay = new System.Windows.Forms.Button();
            this.btn7Ngay = new System.Windows.Forms.Button();
            this.btnHomNay = new System.Windows.Forms.Button();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.panelKpis = new System.Windows.Forms.Panel();
            this.panelKpiSoHD = new System.Windows.Forms.Panel();
            this.lblTongSoHDVal = new System.Windows.Forms.Label();
            this.lblKpiSoHD = new System.Windows.Forms.Label();
            this.panelKpiTienDV = new System.Windows.Forms.Panel();
            this.lblTongTienDVVal = new System.Windows.Forms.Label();
            this.lblKpiTienDV = new System.Windows.Forms.Label();
            this.panelKpiTienPhong = new System.Windows.Forms.Panel();
            this.lblTongTienPhongVal = new System.Windows.Forms.Label();
            this.lblKpiTienPhong = new System.Windows.Forms.Label();
            this.panelKpiDoanhThu = new System.Windows.Forms.Panel();
            this.lblTongDoanhThuVal = new System.Windows.Forms.Label();
            this.lblKpiDoanhThu = new System.Windows.Forms.Label();
            this.tabThongKe = new System.Windows.Forms.TabControl();
            this.tabBieuDo = new System.Windows.Forms.TabPage();
            this.chartDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabChiTietHD = new System.Windows.Forms.TabPage();
            this.dgvHoaDonChiTiet = new System.Windows.Forms.DataGridView();
            this.tabTopDichVu = new System.Windows.Forms.TabPage();
            this.dgvTopDichVu = new System.Windows.Forms.DataGridView();
            this.tabTheoPhong = new System.Windows.Forms.TabPage();
            this.dgvTheoPhong = new System.Windows.Forms.DataGridView();
            this.tabNhanVien = new System.Windows.Forms.TabPage();
            this.dgvNhanVien = new System.Windows.Forms.DataGridView();
            this.panelHeader.SuspendLayout();
            this.panelFilter.SuspendLayout();
            this.panelKpis.SuspendLayout();
            this.panelKpiSoHD.SuspendLayout();
            this.panelKpiTienDV.SuspendLayout();
            this.panelKpiTienPhong.SuspendLayout();
            this.panelKpiDoanhThu.SuspendLayout();
            this.tabThongKe.SuspendLayout();
            this.tabBieuDo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).BeginInit();
            this.tabChiTietHD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDonChiTiet)).BeginInit();
            this.tabTopDichVu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopDichVu)).BeginInit();
            this.tabTheoPhong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTheoPhong)).BeginInit();
            this.tabNhanVien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).BeginInit();
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
            this.lblHeader.Size = new System.Drawing.Size(326, 25);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "THỐNG KÊ & BÁO CÁO DOANH THU";
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelFilter.Controls.Add(this.btnXuatBaoCao);
            this.panelFilter.Controls.Add(this.btnXemThongKe);
            this.panelFilter.Controls.Add(this.btnNamNay);
            this.panelFilter.Controls.Add(this.btnThangNay);
            this.panelFilter.Controls.Add(this.btn7Ngay);
            this.panelFilter.Controls.Add(this.btnHomNay);
            this.panelFilter.Controls.Add(this.dtpDenNgay);
            this.panelFilter.Controls.Add(this.lblDenNgay);
            this.panelFilter.Controls.Add(this.dtpTuNgay);
            this.panelFilter.Controls.Add(this.lblTuNgay);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 46);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1024, 52);
            this.panelFilter.TabIndex = 1;
            // 
            // btnXuatBaoCao
            // 
            this.btnXuatBaoCao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXuatBaoCao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnXuatBaoCao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXuatBaoCao.FlatAppearance.BorderSize = 0;
            this.btnXuatBaoCao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatBaoCao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXuatBaoCao.ForeColor = System.Drawing.Color.White;
            this.btnXuatBaoCao.Location = new System.Drawing.Point(905, 11);
            this.btnXuatBaoCao.Name = "btnXuatBaoCao";
            this.btnXuatBaoCao.Size = new System.Drawing.Size(105, 30);
            this.btnXuatBaoCao.TabIndex = 9;
            this.btnXuatBaoCao.Text = "Xuất CSV";
            this.btnXuatBaoCao.UseVisualStyleBackColor = false;
            this.btnXuatBaoCao.Click += new System.EventHandler(this.btnXuatBaoCao_Click);
            // 
            // btnXemThongKe
            // 
            this.btnXemThongKe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.btnXemThongKe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXemThongKe.FlatAppearance.BorderSize = 0;
            this.btnXemThongKe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemThongKe.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXemThongKe.ForeColor = System.Drawing.Color.White;
            this.btnXemThongKe.Location = new System.Drawing.Point(755, 11);
            this.btnXemThongKe.Name = "btnXemThongKe";
            this.btnXemThongKe.Size = new System.Drawing.Size(105, 30);
            this.btnXemThongKe.TabIndex = 8;
            this.btnXemThongKe.Text = "Xem Thống Kê";
            this.btnXemThongKe.UseVisualStyleBackColor = false;
            this.btnXemThongKe.Click += new System.EventHandler(this.btnXemThongKe_Click);
            // 
            // btnNamNay
            // 
            this.btnNamNay.BackColor = System.Drawing.Color.White;
            this.btnNamNay.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnNamNay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNamNay.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnNamNay.Location = new System.Drawing.Point(670, 12);
            this.btnNamNay.Name = "btnNamNay";
            this.btnNamNay.Size = new System.Drawing.Size(75, 28);
            this.btnNamNay.TabIndex = 7;
            this.btnNamNay.Text = "Năm nay";
            this.btnNamNay.UseVisualStyleBackColor = false;
            this.btnNamNay.Click += new System.EventHandler(this.btnNamNay_Click);
            // 
            // btnThangNay
            // 
            this.btnThangNay.BackColor = System.Drawing.Color.White;
            this.btnThangNay.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnThangNay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThangNay.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnThangNay.Location = new System.Drawing.Point(585, 12);
            this.btnThangNay.Name = "btnThangNay";
            this.btnThangNay.Size = new System.Drawing.Size(80, 28);
            this.btnThangNay.TabIndex = 6;
            this.btnThangNay.Text = "Tháng này";
            this.btnThangNay.UseVisualStyleBackColor = false;
            this.btnThangNay.Click += new System.EventHandler(this.btnThangNay_Click);
            // 
            // btn7Ngay
            // 
            this.btn7Ngay.BackColor = System.Drawing.Color.White;
            this.btn7Ngay.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btn7Ngay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn7Ngay.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btn7Ngay.Location = new System.Drawing.Point(495, 12);
            this.btn7Ngay.Name = "btn7Ngay";
            this.btn7Ngay.Size = new System.Drawing.Size(85, 28);
            this.btn7Ngay.TabIndex = 5;
            this.btn7Ngay.Text = "7 ngày qua";
            this.btn7Ngay.UseVisualStyleBackColor = false;
            this.btn7Ngay.Click += new System.EventHandler(this.btn7Ngay_Click);
            // 
            // btnHomNay
            // 
            this.btnHomNay.BackColor = System.Drawing.Color.White;
            this.btnHomNay.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnHomNay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHomNay.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnHomNay.Location = new System.Drawing.Point(420, 12);
            this.btnHomNay.Name = "btnHomNay";
            this.btnHomNay.Size = new System.Drawing.Size(70, 28);
            this.btnHomNay.TabIndex = 4;
            this.btnHomNay.Text = "Hôm nay";
            this.btnHomNay.UseVisualStyleBackColor = false;
            this.btnHomNay.Click += new System.EventHandler(this.btnHomNay_Click);
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(280, 14);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(125, 24);
            this.dtpDenNgay.TabIndex = 3;
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDenNgay.Location = new System.Drawing.Point(210, 17);
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
            this.dtpTuNgay.Location = new System.Drawing.Point(75, 14);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(125, 24);
            this.dtpTuNgay.TabIndex = 1;
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTuNgay.Location = new System.Drawing.Point(12, 17);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(58, 17);
            this.lblTuNgay.TabIndex = 0;
            this.lblTuNgay.Text = "Từ ngày:";
            // 
            // panelKpis
            // 
            this.panelKpis.BackColor = System.Drawing.Color.White;
            this.panelKpis.Controls.Add(this.panelKpiSoHD);
            this.panelKpis.Controls.Add(this.panelKpiTienDV);
            this.panelKpis.Controls.Add(this.panelKpiTienPhong);
            this.panelKpis.Controls.Add(this.panelKpiDoanhThu);
            this.panelKpis.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelKpis.Location = new System.Drawing.Point(0, 98);
            this.panelKpis.Name = "panelKpis";
            this.panelKpis.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.panelKpis.Size = new System.Drawing.Size(1024, 85);
            this.panelKpis.TabIndex = 2;
            // 
            // panelKpiSoHD
            // 
            this.panelKpiSoHD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.panelKpiSoHD.Controls.Add(this.lblTongSoHDVal);
            this.panelKpiSoHD.Controls.Add(this.lblKpiSoHD);
            this.panelKpiSoHD.Location = new System.Drawing.Point(760, 10);
            this.panelKpiSoHD.Name = "panelKpiSoHD";
            this.panelKpiSoHD.Size = new System.Drawing.Size(240, 65);
            this.panelKpiSoHD.TabIndex = 3;
            // 
            // lblTongSoHDVal
            // 
            this.lblTongSoHDVal.AutoSize = true;
            this.lblTongSoHDVal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTongSoHDVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(34)))), ((int)(((byte)(206)))));
            this.lblTongSoHDVal.Location = new System.Drawing.Point(12, 30);
            this.lblTongSoHDVal.Name = "lblTongSoHDVal";
            this.lblTongSoHDVal.Size = new System.Drawing.Size(23, 25);
            this.lblTongSoHDVal.TabIndex = 1;
            this.lblTongSoHDVal.Text = "0";
            // 
            // lblKpiSoHD
            // 
            this.lblKpiSoHD.AutoSize = true;
            this.lblKpiSoHD.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblKpiSoHD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(33)))), ((int)(((byte)(168)))));
            this.lblKpiSoHD.Location = new System.Drawing.Point(12, 10);
            this.lblKpiSoHD.Name = "lblKpiSoHD";
            this.lblKpiSoHD.Size = new System.Drawing.Size(117, 15);
            this.lblKpiSoHD.TabIndex = 0;
            this.lblKpiSoHD.Text = "TỔNG SỐ HÓA ĐƠN";
            // 
            // panelKpiTienDV
            // 
            this.panelKpiTienDV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(243)))), ((int)(((byte)(199)))));
            this.panelKpiTienDV.Controls.Add(this.lblTongTienDVVal);
            this.panelKpiTienDV.Controls.Add(this.lblKpiTienDV);
            this.panelKpiTienDV.Location = new System.Drawing.Point(510, 10);
            this.panelKpiTienDV.Name = "panelKpiTienDV";
            this.panelKpiTienDV.Size = new System.Drawing.Size(240, 65);
            this.panelKpiTienDV.TabIndex = 2;
            // 
            // lblTongTienDVVal
            // 
            this.lblTongTienDVVal.AutoSize = true;
            this.lblTongTienDVVal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTongTienDVVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(83)))), ((int)(((byte)(9)))));
            this.lblTongTienDVVal.Location = new System.Drawing.Point(12, 30);
            this.lblTongTienDVVal.Name = "lblTongTienDVVal";
            this.lblTongTienDVVal.Size = new System.Drawing.Size(46, 25);
            this.lblTongTienDVVal.TabIndex = 1;
            this.lblTongTienDVVal.Text = "0 đ";
            // 
            // lblKpiTienDV
            // 
            this.lblKpiTienDV.AutoSize = true;
            this.lblKpiTienDV.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblKpiTienDV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(64)))), ((int)(((byte)(14)))));
            this.lblKpiTienDV.Location = new System.Drawing.Point(12, 10);
            this.lblKpiTienDV.Name = "lblKpiTienDV";
            this.lblKpiTienDV.Size = new System.Drawing.Size(107, 15);
            this.lblKpiTienDV.TabIndex = 0;
            this.lblKpiTienDV.Text = "TỔNG TIỀN DỊCH VỤ";
            // 
            // panelKpiTienPhong
            // 
            this.panelKpiTienPhong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.panelKpiTienPhong.Controls.Add(this.lblTongTienPhongVal);
            this.panelKpiTienPhong.Controls.Add(this.lblKpiTienPhong);
            this.panelKpiTienPhong.Location = new System.Drawing.Point(260, 10);
            this.panelKpiTienPhong.Name = "panelKpiTienPhong";
            this.panelKpiTienPhong.Size = new System.Drawing.Size(240, 65);
            this.panelKpiTienPhong.TabIndex = 1;
            // 
            // lblTongTienPhongVal
            // 
            this.lblTongTienPhongVal.AutoSize = true;
            this.lblTongTienPhongVal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTongTienPhongVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.lblTongTienPhongVal.Location = new System.Drawing.Point(12, 30);
            this.lblTongTienPhongVal.Name = "lblTongTienPhongVal";
            this.lblTongTienPhongVal.Size = new System.Drawing.Size(46, 25);
            this.lblTongTienPhongVal.TabIndex = 1;
            this.lblTongTienPhongVal.Text = "0 đ";
            // 
            // lblKpiTienPhong
            // 
            this.lblKpiTienPhong.AutoSize = true;
            this.lblKpiTienPhong.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblKpiTienPhong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.lblKpiTienPhong.Location = new System.Drawing.Point(12, 10);
            this.lblKpiTienPhong.Name = "lblKpiTienPhong";
            this.lblKpiTienPhong.Size = new System.Drawing.Size(126, 15);
            this.lblKpiTienPhong.TabIndex = 0;
            this.lblKpiTienPhong.Text = "TỔNG TIỀN GIỜ HÁT";
            // 
            // panelKpiDoanhThu
            // 
            this.panelKpiDoanhThu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.panelKpiDoanhThu.Controls.Add(this.lblTongDoanhThuVal);
            this.panelKpiDoanhThu.Controls.Add(this.lblKpiDoanhThu);
            this.panelKpiDoanhThu.Location = new System.Drawing.Point(12, 10);
            this.panelKpiDoanhThu.Name = "panelKpiDoanhThu";
            this.panelKpiDoanhThu.Size = new System.Drawing.Size(240, 65);
            this.panelKpiDoanhThu.TabIndex = 0;
            // 
            // lblTongDoanhThuVal
            // 
            this.lblTongDoanhThuVal.AutoSize = true;
            this.lblTongDoanhThuVal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTongDoanhThuVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblTongDoanhThuVal.Location = new System.Drawing.Point(12, 30);
            this.lblTongDoanhThuVal.Name = "lblTongDoanhThuVal";
            this.lblTongDoanhThuVal.Size = new System.Drawing.Size(46, 25);
            this.lblTongDoanhThuVal.TabIndex = 1;
            this.lblTongDoanhThuVal.Text = "0 đ";
            // 
            // lblKpiDoanhThu
            // 
            this.lblKpiDoanhThu.AutoSize = true;
            this.lblKpiDoanhThu.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblKpiDoanhThu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(83)))), ((int)(((byte)(45)))));
            this.lblKpiDoanhThu.Location = new System.Drawing.Point(12, 10);
            this.lblKpiDoanhThu.Name = "lblKpiDoanhThu";
            this.lblKpiDoanhThu.Size = new System.Drawing.Size(109, 15);
            this.lblKpiDoanhThu.TabIndex = 0;
            this.lblKpiDoanhThu.Text = "TỔNG DOANH THU";
            // 
            // tabThongKe
            // 
            this.tabThongKe.Controls.Add(this.tabBieuDo);
            this.tabThongKe.Controls.Add(this.tabChiTietHD);
            this.tabThongKe.Controls.Add(this.tabTopDichVu);
            this.tabThongKe.Controls.Add(this.tabTheoPhong);
            this.tabThongKe.Controls.Add(this.tabNhanVien);
            this.tabThongKe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabThongKe.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.tabThongKe.Location = new System.Drawing.Point(0, 183);
            this.tabThongKe.Name = "tabThongKe";
            this.tabThongKe.SelectedIndex = 0;
            this.tabThongKe.Size = new System.Drawing.Size(1024, 487);
            this.tabThongKe.TabIndex = 3;
            // 
            // tabBieuDo
            // 
            this.tabBieuDo.Controls.Add(this.chartDoanhThu);
            this.tabBieuDo.Location = new System.Drawing.Point(4, 26);
            this.tabBieuDo.Name = "tabBieuDo";
            this.tabBieuDo.Padding = new System.Windows.Forms.Padding(10);
            this.tabBieuDo.Size = new System.Drawing.Size(1016, 457);
            this.tabBieuDo.TabIndex = 0;
            this.tabBieuDo.Text = "  Biểu Đồ Doanh Thu  ";
            this.tabBieuDo.UseVisualStyleBackColor = true;
            // 
            // chartDoanhThu
            // 
            chartArea1.Name = "ChartArea1";
            this.chartDoanhThu.ChartAreas.Add(chartArea1);
            this.chartDoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartDoanhThu.Legends.Add(legend1);
            this.chartDoanhThu.Location = new System.Drawing.Point(10, 10);
            this.chartDoanhThu.Name = "chartDoanhThu";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartDoanhThu.Series.Add(series1);
            this.chartDoanhThu.Size = new System.Drawing.Size(996, 437);
            this.chartDoanhThu.TabIndex = 0;
            this.chartDoanhThu.Text = "chart1";
            // 
            // tabChiTietHD
            // 
            this.tabChiTietHD.Controls.Add(this.dgvHoaDonChiTiet);
            this.tabChiTietHD.Location = new System.Drawing.Point(4, 26);
            this.tabChiTietHD.Name = "tabChiTietHD";
            this.tabChiTietHD.Padding = new System.Windows.Forms.Padding(3);
            this.tabChiTietHD.Size = new System.Drawing.Size(1016, 457);
            this.tabChiTietHD.TabIndex = 1;
            this.tabChiTietHD.Text = "  Danh Sách Hóa Đơn  ";
            this.tabChiTietHD.UseVisualStyleBackColor = true;
            // 
            // dgvHoaDonChiTiet
            // 
            this.dgvHoaDonChiTiet.AllowUserToAddRows = false;
            this.dgvHoaDonChiTiet.AllowUserToDeleteRows = false;
            this.dgvHoaDonChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.dgvHoaDonChiTiet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHoaDonChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHoaDonChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHoaDonChiTiet.Location = new System.Drawing.Point(3, 3);
            this.dgvHoaDonChiTiet.MultiSelect = false;
            this.dgvHoaDonChiTiet.Name = "dgvHoaDonChiTiet";
            this.dgvHoaDonChiTiet.ReadOnly = true;
            this.dgvHoaDonChiTiet.RowHeadersVisible = false;
            this.dgvHoaDonChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHoaDonChiTiet.Size = new System.Drawing.Size(1010, 451);
            this.dgvHoaDonChiTiet.TabIndex = 0;
            // 
            // tabTopDichVu
            // 
            this.tabTopDichVu.Controls.Add(this.dgvTopDichVu);
            this.tabTopDichVu.Location = new System.Drawing.Point(4, 26);
            this.tabTopDichVu.Name = "tabTopDichVu";
            this.tabTopDichVu.Padding = new System.Windows.Forms.Padding(3);
            this.tabTopDichVu.Size = new System.Drawing.Size(1016, 457);
            this.tabTopDichVu.TabIndex = 2;
            this.tabTopDichVu.Text = "  Top Dịch Vụ Tiêu Thụ  ";
            this.tabTopDichVu.UseVisualStyleBackColor = true;
            // 
            // dgvTopDichVu
            // 
            this.dgvTopDichVu.AllowUserToAddRows = false;
            this.dgvTopDichVu.AllowUserToDeleteRows = false;
            this.dgvTopDichVu.BackgroundColor = System.Drawing.Color.White;
            this.dgvTopDichVu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTopDichVu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTopDichVu.Location = new System.Drawing.Point(3, 3);
            this.dgvTopDichVu.MultiSelect = false;
            this.dgvTopDichVu.Name = "dgvTopDichVu";
            this.dgvTopDichVu.ReadOnly = true;
            this.dgvTopDichVu.RowHeadersVisible = false;
            this.dgvTopDichVu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTopDichVu.Size = new System.Drawing.Size(1010, 451);
            this.dgvTopDichVu.TabIndex = 0;
            // 
            // tabTheoPhong
            // 
            this.tabTheoPhong.Controls.Add(this.dgvTheoPhong);
            this.tabTheoPhong.Location = new System.Drawing.Point(4, 26);
            this.tabTheoPhong.Name = "tabTheoPhong";
            this.tabTheoPhong.Padding = new System.Windows.Forms.Padding(3);
            this.tabTheoPhong.Size = new System.Drawing.Size(1016, 457);
            this.tabTheoPhong.TabIndex = 3;
            this.tabTheoPhong.Text = "  Doanh Thu Theo Phòng  ";
            this.tabTheoPhong.UseVisualStyleBackColor = true;
            // 
            // dgvTheoPhong
            // 
            this.dgvTheoPhong.AllowUserToAddRows = false;
            this.dgvTheoPhong.AllowUserToDeleteRows = false;
            this.dgvTheoPhong.BackgroundColor = System.Drawing.Color.White;
            this.dgvTheoPhong.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTheoPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTheoPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTheoPhong.Location = new System.Drawing.Point(3, 3);
            this.dgvTheoPhong.MultiSelect = false;
            this.dgvTheoPhong.Name = "dgvTheoPhong";
            this.dgvTheoPhong.ReadOnly = true;
            this.dgvTheoPhong.RowHeadersVisible = false;
            this.dgvTheoPhong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTheoPhong.Size = new System.Drawing.Size(1010, 451);
            this.dgvTheoPhong.TabIndex = 0;
            // 
            // tabNhanVien
            // 
            this.tabNhanVien.Controls.Add(this.dgvNhanVien);
            this.tabNhanVien.Location = new System.Drawing.Point(4, 26);
            this.tabNhanVien.Name = "tabNhanVien";
            this.tabNhanVien.Padding = new System.Windows.Forms.Padding(3);
            this.tabNhanVien.Size = new System.Drawing.Size(1016, 457);
            this.tabNhanVien.TabIndex = 4;
            this.tabNhanVien.Text = "  Doanh Thu Theo Nhân Viên  ";
            this.tabNhanVien.UseVisualStyleBackColor = true;
            // 
            // dgvNhanVien
            // 
            this.dgvNhanVien.AllowUserToAddRows = false;
            this.dgvNhanVien.AllowUserToDeleteRows = false;
            this.dgvNhanVien.BackgroundColor = System.Drawing.Color.White;
            this.dgvNhanVien.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNhanVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNhanVien.Location = new System.Drawing.Point(3, 3);
            this.dgvNhanVien.MultiSelect = false;
            this.dgvNhanVien.Name = "dgvNhanVien";
            this.dgvNhanVien.ReadOnly = true;
            this.dgvNhanVien.RowHeadersVisible = false;
            this.dgvNhanVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhanVien.Size = new System.Drawing.Size(1010, 451);
            this.dgvNhanVien.TabIndex = 0;
            // 
            // FrmThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 670);
            this.Controls.Add(this.tabThongKe);
            this.Controls.Add(this.panelKpis);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmThongKe";
            this.Text = "Thống Kê & Báo Cáo";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelKpis.ResumeLayout(false);
            this.panelKpiSoHD.ResumeLayout(false);
            this.panelKpiSoHD.PerformLayout();
            this.panelKpiTienDV.ResumeLayout(false);
            this.panelKpiTienDV.PerformLayout();
            this.panelKpiTienPhong.ResumeLayout(false);
            this.panelKpiTienPhong.PerformLayout();
            this.panelKpiDoanhThu.ResumeLayout(false);
            this.panelKpiDoanhThu.PerformLayout();
            this.tabThongKe.ResumeLayout(false);
            this.tabBieuDo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).EndInit();
            this.tabChiTietHD.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDonChiTiet)).EndInit();
            this.tabTopDichVu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopDichVu)).EndInit();
            this.tabTheoPhong.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTheoPhong)).EndInit();
            this.tabNhanVien.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).EndInit();
            this.designColumn_dgvHoaDonChiTiet_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvHoaDonChiTiet_1.DataPropertyName = "MaHD";
            this.designColumn_dgvHoaDonChiTiet_1.HeaderText = "Mã HD";
            this.designColumn_dgvHoaDonChiTiet_1.Width = 70;
            this.designColumn_dgvHoaDonChiTiet_1.Name = "designColumn_dgvHoaDonChiTiet_1";
            this.dgvHoaDonChiTiet.Columns.Add(this.designColumn_dgvHoaDonChiTiet_1);
            this.designColumn_dgvHoaDonChiTiet_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvHoaDonChiTiet_2.DataPropertyName = "NgayThanhToan";
            this.designColumn_dgvHoaDonChiTiet_2.HeaderText = "Ngày TT";
            this.designColumn_dgvHoaDonChiTiet_2.Width = 130;
            this.designColumn_dgvHoaDonChiTiet_2.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            this.designColumn_dgvHoaDonChiTiet_2.Name = "designColumn_dgvHoaDonChiTiet_2";
            this.dgvHoaDonChiTiet.Columns.Add(this.designColumn_dgvHoaDonChiTiet_2);
            this.designColumn_dgvHoaDonChiTiet_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvHoaDonChiTiet_3.DataPropertyName = "TenPhong";
            this.designColumn_dgvHoaDonChiTiet_3.HeaderText = "Phòng";
            this.designColumn_dgvHoaDonChiTiet_3.Width = 90;
            this.designColumn_dgvHoaDonChiTiet_3.Name = "designColumn_dgvHoaDonChiTiet_3";
            this.dgvHoaDonChiTiet.Columns.Add(this.designColumn_dgvHoaDonChiTiet_3);
            this.designColumn_dgvHoaDonChiTiet_4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvHoaDonChiTiet_4.DataPropertyName = "SoGioHat";
            this.designColumn_dgvHoaDonChiTiet_4.HeaderText = "Giờ hát";
            this.designColumn_dgvHoaDonChiTiet_4.Width = 75;
            this.designColumn_dgvHoaDonChiTiet_4.DefaultCellStyle.Format = "0.##";
            this.designColumn_dgvHoaDonChiTiet_4.Name = "designColumn_dgvHoaDonChiTiet_4";
            this.dgvHoaDonChiTiet.Columns.Add(this.designColumn_dgvHoaDonChiTiet_4);
            this.designColumn_dgvHoaDonChiTiet_5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvHoaDonChiTiet_5.DataPropertyName = "TienPhong";
            this.designColumn_dgvHoaDonChiTiet_5.HeaderText = "Tiền phòng";
            this.designColumn_dgvHoaDonChiTiet_5.Width = 100;
            this.designColumn_dgvHoaDonChiTiet_5.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvHoaDonChiTiet_5.Name = "designColumn_dgvHoaDonChiTiet_5";
            this.dgvHoaDonChiTiet.Columns.Add(this.designColumn_dgvHoaDonChiTiet_5);
            this.designColumn_dgvHoaDonChiTiet_6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvHoaDonChiTiet_6.DataPropertyName = "TienDichVu";
            this.designColumn_dgvHoaDonChiTiet_6.HeaderText = "Tiền dịch vụ";
            this.designColumn_dgvHoaDonChiTiet_6.Width = 100;
            this.designColumn_dgvHoaDonChiTiet_6.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvHoaDonChiTiet_6.Name = "designColumn_dgvHoaDonChiTiet_6";
            this.dgvHoaDonChiTiet.Columns.Add(this.designColumn_dgvHoaDonChiTiet_6);
            this.designColumn_dgvHoaDonChiTiet_7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvHoaDonChiTiet_7.DataPropertyName = "GiamGia";
            this.designColumn_dgvHoaDonChiTiet_7.HeaderText = "Giảm giá";
            this.designColumn_dgvHoaDonChiTiet_7.Width = 90;
            this.designColumn_dgvHoaDonChiTiet_7.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvHoaDonChiTiet_7.Name = "designColumn_dgvHoaDonChiTiet_7";
            this.dgvHoaDonChiTiet.Columns.Add(this.designColumn_dgvHoaDonChiTiet_7);
            this.designColumn_dgvHoaDonChiTiet_8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvHoaDonChiTiet_8.DataPropertyName = "ThanhTien";
            this.designColumn_dgvHoaDonChiTiet_8.HeaderText = "Thành tiền";
            this.designColumn_dgvHoaDonChiTiet_8.Width = 120;
            this.designColumn_dgvHoaDonChiTiet_8.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvHoaDonChiTiet_8.Name = "designColumn_dgvHoaDonChiTiet_8";
            this.dgvHoaDonChiTiet.Columns.Add(this.designColumn_dgvHoaDonChiTiet_8);
            this.designColumn_dgvHoaDonChiTiet_9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvHoaDonChiTiet_9.DataPropertyName = "PhuongThucTT";
            this.designColumn_dgvHoaDonChiTiet_9.HeaderText = "Hình thức";
            this.designColumn_dgvHoaDonChiTiet_9.Width = 100;
            this.designColumn_dgvHoaDonChiTiet_9.Name = "designColumn_dgvHoaDonChiTiet_9";
            this.dgvHoaDonChiTiet.Columns.Add(this.designColumn_dgvHoaDonChiTiet_9);
            this.designColumn_dgvHoaDonChiTiet_10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvHoaDonChiTiet_10.DataPropertyName = "NhanVienLap";
            this.designColumn_dgvHoaDonChiTiet_10.HeaderText = "Thu ngân";
            this.designColumn_dgvHoaDonChiTiet_10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.designColumn_dgvHoaDonChiTiet_10.Name = "designColumn_dgvHoaDonChiTiet_10";
            this.dgvHoaDonChiTiet.Columns.Add(this.designColumn_dgvHoaDonChiTiet_10);
            this.designColumn_dgvTopDichVu_11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvTopDichVu_11.DataPropertyName = "MaDV";
            this.designColumn_dgvTopDichVu_11.HeaderText = "Mã DV";
            this.designColumn_dgvTopDichVu_11.Width = 80;
            this.designColumn_dgvTopDichVu_11.Name = "designColumn_dgvTopDichVu_11";
            this.dgvTopDichVu.Columns.Add(this.designColumn_dgvTopDichVu_11);
            this.designColumn_dgvTopDichVu_12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvTopDichVu_12.DataPropertyName = "TenDV";
            this.designColumn_dgvTopDichVu_12.HeaderText = "Tên dịch vụ / món";
            this.designColumn_dgvTopDichVu_12.Width = 200;
            this.designColumn_dgvTopDichVu_12.Name = "designColumn_dgvTopDichVu_12";
            this.dgvTopDichVu.Columns.Add(this.designColumn_dgvTopDichVu_12);
            this.designColumn_dgvTopDichVu_13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvTopDichVu_13.DataPropertyName = "DonViTinh";
            this.designColumn_dgvTopDichVu_13.HeaderText = "ĐVT";
            this.designColumn_dgvTopDichVu_13.Width = 70;
            this.designColumn_dgvTopDichVu_13.Name = "designColumn_dgvTopDichVu_13";
            this.dgvTopDichVu.Columns.Add(this.designColumn_dgvTopDichVu_13);
            this.designColumn_dgvTopDichVu_14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvTopDichVu_14.DataPropertyName = "TongSoLuong";
            this.designColumn_dgvTopDichVu_14.HeaderText = "Tổng SL bán";
            this.designColumn_dgvTopDichVu_14.Width = 110;
            this.designColumn_dgvTopDichVu_14.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvTopDichVu_14.Name = "designColumn_dgvTopDichVu_14";
            this.dgvTopDichVu.Columns.Add(this.designColumn_dgvTopDichVu_14);
            this.designColumn_dgvTopDichVu_15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvTopDichVu_15.DataPropertyName = "TongDoanhThu";
            this.designColumn_dgvTopDichVu_15.HeaderText = "Tổng doanh thu";
            this.designColumn_dgvTopDichVu_15.Width = 150;
            this.designColumn_dgvTopDichVu_15.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvTopDichVu_15.Name = "designColumn_dgvTopDichVu_15";
            this.dgvTopDichVu.Columns.Add(this.designColumn_dgvTopDichVu_15);
            this.designColumn_dgvTopDichVu_16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvTopDichVu_16.DataPropertyName = "TiLeDoanhThu";
            this.designColumn_dgvTopDichVu_16.HeaderText = "Tỷ lệ (%)";
            this.designColumn_dgvTopDichVu_16.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.designColumn_dgvTopDichVu_16.DefaultCellStyle.Format = "0.# '%'";
            this.designColumn_dgvTopDichVu_16.Name = "designColumn_dgvTopDichVu_16";
            this.dgvTopDichVu.Columns.Add(this.designColumn_dgvTopDichVu_16);
            this.designColumn_dgvTheoPhong_17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvTheoPhong_17.DataPropertyName = "MaPhong";
            this.designColumn_dgvTheoPhong_17.HeaderText = "Mã phòng";
            this.designColumn_dgvTheoPhong_17.Width = 100;
            this.designColumn_dgvTheoPhong_17.Name = "designColumn_dgvTheoPhong_17";
            this.dgvTheoPhong.Columns.Add(this.designColumn_dgvTheoPhong_17);
            this.designColumn_dgvTheoPhong_18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvTheoPhong_18.DataPropertyName = "TenPhong";
            this.designColumn_dgvTheoPhong_18.HeaderText = "Tên phòng";
            this.designColumn_dgvTheoPhong_18.Width = 130;
            this.designColumn_dgvTheoPhong_18.Name = "designColumn_dgvTheoPhong_18";
            this.dgvTheoPhong.Columns.Add(this.designColumn_dgvTheoPhong_18);
            this.designColumn_dgvTheoPhong_19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvTheoPhong_19.DataPropertyName = "LoaiPhong";
            this.designColumn_dgvTheoPhong_19.HeaderText = "Loại phòng";
            this.designColumn_dgvTheoPhong_19.Width = 120;
            this.designColumn_dgvTheoPhong_19.Name = "designColumn_dgvTheoPhong_19";
            this.dgvTheoPhong.Columns.Add(this.designColumn_dgvTheoPhong_19);
            this.designColumn_dgvTheoPhong_20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvTheoPhong_20.DataPropertyName = "SoLuotSuDung";
            this.designColumn_dgvTheoPhong_20.HeaderText = "Lượt sử dụng";
            this.designColumn_dgvTheoPhong_20.Width = 110;
            this.designColumn_dgvTheoPhong_20.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvTheoPhong_20.Name = "designColumn_dgvTheoPhong_20";
            this.dgvTheoPhong.Columns.Add(this.designColumn_dgvTheoPhong_20);
            this.designColumn_dgvTheoPhong_21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvTheoPhong_21.DataPropertyName = "TongTienPhong";
            this.designColumn_dgvTheoPhong_21.HeaderText = "Tiền phòng";
            this.designColumn_dgvTheoPhong_21.Width = 130;
            this.designColumn_dgvTheoPhong_21.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvTheoPhong_21.Name = "designColumn_dgvTheoPhong_21";
            this.dgvTheoPhong.Columns.Add(this.designColumn_dgvTheoPhong_21);
            this.designColumn_dgvTheoPhong_22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvTheoPhong_22.DataPropertyName = "TongTienDichVu";
            this.designColumn_dgvTheoPhong_22.HeaderText = "Tiền dịch vụ";
            this.designColumn_dgvTheoPhong_22.Width = 130;
            this.designColumn_dgvTheoPhong_22.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvTheoPhong_22.Name = "designColumn_dgvTheoPhong_22";
            this.dgvTheoPhong.Columns.Add(this.designColumn_dgvTheoPhong_22);
            this.designColumn_dgvTheoPhong_23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvTheoPhong_23.DataPropertyName = "TongDoanhThu";
            this.designColumn_dgvTheoPhong_23.HeaderText = "Tổng doanh thu";
            this.designColumn_dgvTheoPhong_23.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.designColumn_dgvTheoPhong_23.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvTheoPhong_23.Name = "designColumn_dgvTheoPhong_23";
            this.dgvTheoPhong.Columns.Add(this.designColumn_dgvTheoPhong_23);
            this.designColumn_dgvNhanVien_24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvNhanVien_24.DataPropertyName = "MaNV";
            this.designColumn_dgvNhanVien_24.HeaderText = "Mã NV";
            this.designColumn_dgvNhanVien_24.Width = 100;
            this.designColumn_dgvNhanVien_24.Name = "designColumn_dgvNhanVien_24";
            this.dgvNhanVien.Columns.Add(this.designColumn_dgvNhanVien_24);
            this.designColumn_dgvNhanVien_25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvNhanVien_25.DataPropertyName = "TenNV";
            this.designColumn_dgvNhanVien_25.HeaderText = "Họ và tên";
            this.designColumn_dgvNhanVien_25.Width = 200;
            this.designColumn_dgvNhanVien_25.Name = "designColumn_dgvNhanVien_25";
            this.dgvNhanVien.Columns.Add(this.designColumn_dgvNhanVien_25);
            this.designColumn_dgvNhanVien_26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvNhanVien_26.DataPropertyName = "ChucVu";
            this.designColumn_dgvNhanVien_26.HeaderText = "Chức vụ";
            this.designColumn_dgvNhanVien_26.Width = 140;
            this.designColumn_dgvNhanVien_26.Name = "designColumn_dgvNhanVien_26";
            this.dgvNhanVien.Columns.Add(this.designColumn_dgvNhanVien_26);
            this.designColumn_dgvNhanVien_27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvNhanVien_27.DataPropertyName = "SoHoaDon";
            this.designColumn_dgvNhanVien_27.HeaderText = "Số HĐ đã lập";
            this.designColumn_dgvNhanVien_27.Width = 120;
            this.designColumn_dgvNhanVien_27.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvNhanVien_27.Name = "designColumn_dgvNhanVien_27";
            this.dgvNhanVien.Columns.Add(this.designColumn_dgvNhanVien_27);
            this.designColumn_dgvNhanVien_28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designColumn_dgvNhanVien_28.DataPropertyName = "TongDoanhThu";
            this.designColumn_dgvNhanVien_28.HeaderText = "Tổng doanh thu";
            this.designColumn_dgvNhanVien_28.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.designColumn_dgvNhanVien_28.DefaultCellStyle.Format = "N0";
            this.designColumn_dgvNhanVien_28.Name = "designColumn_dgvNhanVien_28";
            this.dgvNhanVien.Columns.Add(this.designColumn_dgvNhanVien_28);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Button btnHomNay;
        private System.Windows.Forms.Button btn7Ngay;
        private System.Windows.Forms.Button btnThangNay;
        private System.Windows.Forms.Button btnNamNay;
        private System.Windows.Forms.Button btnXemThongKe;
        private System.Windows.Forms.Button btnXuatBaoCao;
        private System.Windows.Forms.Panel panelKpis;
        private System.Windows.Forms.Panel panelKpiDoanhThu;
        private System.Windows.Forms.Label lblKpiDoanhThu;
        private System.Windows.Forms.Label lblTongDoanhThuVal;
        private System.Windows.Forms.Panel panelKpiTienPhong;
        private System.Windows.Forms.Label lblTongTienPhongVal;
        private System.Windows.Forms.Label lblKpiTienPhong;
        private System.Windows.Forms.Panel panelKpiTienDV;
        private System.Windows.Forms.Label lblTongTienDVVal;
        private System.Windows.Forms.Label lblKpiTienDV;
        private System.Windows.Forms.Panel panelKpiSoHD;
        private System.Windows.Forms.Label lblTongSoHDVal;
        private System.Windows.Forms.Label lblKpiSoHD;
        private System.Windows.Forms.TabControl tabThongKe;
        private System.Windows.Forms.TabPage tabBieuDo;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThu;
        private System.Windows.Forms.TabPage tabChiTietHD;
        private System.Windows.Forms.DataGridView dgvHoaDonChiTiet;
        private System.Windows.Forms.TabPage tabTopDichVu;
        private System.Windows.Forms.DataGridView dgvTopDichVu;
        private System.Windows.Forms.TabPage tabTheoPhong;
        private System.Windows.Forms.DataGridView dgvTheoPhong;
        private System.Windows.Forms.TabPage tabNhanVien;
        private System.Windows.Forms.DataGridView dgvNhanVien;

        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvHoaDonChiTiet_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvHoaDonChiTiet_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvHoaDonChiTiet_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvHoaDonChiTiet_4;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvHoaDonChiTiet_5;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvHoaDonChiTiet_6;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvHoaDonChiTiet_7;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvHoaDonChiTiet_8;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvHoaDonChiTiet_9;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvHoaDonChiTiet_10;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvTopDichVu_11;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvTopDichVu_12;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvTopDichVu_13;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvTopDichVu_14;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvTopDichVu_15;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvTopDichVu_16;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvTheoPhong_17;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvTheoPhong_18;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvTheoPhong_19;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvTheoPhong_20;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvTheoPhong_21;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvTheoPhong_22;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvTheoPhong_23;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvNhanVien_24;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvNhanVien_25;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvNhanVien_26;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvNhanVien_27;
        private System.Windows.Forms.DataGridViewTextBoxColumn designColumn_dgvNhanVien_28;
    }
}
