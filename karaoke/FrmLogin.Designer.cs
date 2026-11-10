namespace karaoke
{
    partial class FrmLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (txtMatKhau != null) txtMatKhau.IconRight = null;
                if (eyeIcon != null) eyeIcon.Dispose();
                if (eyeHiddenIcon != null) eyeHiddenIcon.Dispose();
                if (brandIcon != null && brandIcon.Image != null) brandIcon.Image.Dispose();
                if (forgotIcon != null && forgotIcon.Image != null) forgotIcon.Image.Dispose();
                if (components != null) components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.brandPanel = new System.Windows.Forms.Panel();
            this.brandIcon = new System.Windows.Forms.PictureBox();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.userIcon = new System.Windows.Forms.PictureBox();
            this.lockIcon = new System.Windows.Forms.PictureBox();
            this.forgotIcon = new System.Windows.Forms.PictureBox();
            this.linkQuenMatKhau = new System.Windows.Forms.LinkLabel();
            this.btnDangNhap = new Guna.UI2.WinForms.Guna2Button();
            this.txtTenDangNhap = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMatKhau = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblThongBao = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.passwordToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.guna2Panel1.SuspendLayout();
            this.brandPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brandIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lockIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.forgotIcon)).BeginInit();
            this.SuspendLayout();
            // brandPanel
            this.brandPanel.BackColor = System.Drawing.Color.White;
            this.brandPanel.Controls.Add(this.brandIcon);
            this.brandPanel.Location = new System.Drawing.Point(200, 14);
            this.brandPanel.Name = "brandPanel";
            this.brandPanel.Size = new System.Drawing.Size(440, 135);
            this.brandPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.brandPanel_Paint);
            // brandIcon
            this.brandIcon.BackColor = System.Drawing.Color.White;
            this.brandIcon.Location = new System.Drawing.Point(100, 0);
            this.brandIcon.Name = "brandIcon";
            this.brandIcon.Size = new System.Drawing.Size(240, 74);
            this.brandIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.brandIcon.TabStop = false;
            // guna2Panel1
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderColor = System.Drawing.Color.FromArgb(0, 145, 62);
            this.guna2Panel1.BorderRadius = 20;
            this.guna2Panel1.BorderThickness = 2;
            this.guna2Panel1.FillColor = System.Drawing.Color.White;
            this.guna2Panel1.Location = new System.Drawing.Point(190, 160);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(460, 370);
            this.guna2Panel1.Controls.Add(this.userIcon);
            this.guna2Panel1.Controls.Add(this.lockIcon);
            this.guna2Panel1.Controls.Add(this.guna2HtmlLabel1);
            this.guna2Panel1.Controls.Add(this.guna2HtmlLabel2);
            this.guna2Panel1.Controls.Add(this.txtTenDangNhap);
            this.guna2Panel1.Controls.Add(this.txtMatKhau);
            this.guna2Panel1.Controls.Add(this.lblThongBao);
            this.guna2Panel1.Controls.Add(this.btnDangNhap);
            this.guna2Panel1.Controls.Add(this.forgotIcon);
            this.guna2Panel1.Controls.Add(this.linkQuenMatKhau);
            // forgotIcon
            this.forgotIcon.BackColor = System.Drawing.Color.White;
            this.forgotIcon.Location = new System.Drawing.Point(85, 321);
            this.forgotIcon.Size = new System.Drawing.Size(22, 22);
            this.forgotIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.forgotIcon.TabStop = false;
            // linkQuenMatKhau
            this.linkQuenMatKhau.AutoSize = true;
            this.linkQuenMatKhau.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkQuenMatKhau.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.linkQuenMatKhau.LinkColor = System.Drawing.Color.FromArgb(0, 145, 62);
            this.linkQuenMatKhau.VisitedLinkColor = System.Drawing.Color.FromArgb(0, 145, 62);
            this.linkQuenMatKhau.Location = new System.Drawing.Point(112, 321);
            this.linkQuenMatKhau.Name = "linkQuenMatKhau";
            this.linkQuenMatKhau.TabIndex = 3;
            this.linkQuenMatKhau.Text = "Quên mật khẩu / Liên hệ Admin";
            this.linkQuenMatKhau.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkQuenMatKhau_LinkClicked);
            // userIcon
            this.userIcon.BackColor = System.Drawing.Color.White;
            this.userIcon.Location = new System.Drawing.Point(43, 28);
            this.userIcon.Name = "userIcon";
            this.userIcon.Size = new System.Drawing.Size(24, 24);
            this.userIcon.TabStop = false;
            this.userIcon.Paint += new System.Windows.Forms.PaintEventHandler(this.userIcon_Paint);
            // lockIcon
            this.lockIcon.BackColor = System.Drawing.Color.White;
            this.lockIcon.Location = new System.Drawing.Point(43, 114);
            this.lockIcon.Name = "lockIcon";
            this.lockIcon.Size = new System.Drawing.Size(24, 24);
            this.lockIcon.TabStop = false;
            this.lockIcon.Paint += new System.Windows.Forms.PaintEventHandler(this.lockIcon_Paint);
            // guna2HtmlLabel1
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.FromArgb(20, 30, 24);
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(75, 27);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Text = "Tên đăng nhập";
            // guna2HtmlLabel2
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.guna2HtmlLabel2.ForeColor = System.Drawing.Color.FromArgb(20, 30, 24);
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(75, 113);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Text = "Mật khẩu";
            // txtTenDangNhap
            this.txtTenDangNhap.BorderColor = System.Drawing.Color.FromArgb(195, 205, 199);
            this.txtTenDangNhap.BorderRadius = 8;
            this.txtTenDangNhap.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenDangNhap.DefaultText = "";
            this.txtTenDangNhap.FocusedState.BorderColor = System.Drawing.Color.FromArgb(0, 145, 62);
            this.txtTenDangNhap.HoverState.BorderColor = System.Drawing.Color.FromArgb(0, 145, 62);
            this.txtTenDangNhap.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtTenDangNhap.ForeColor = System.Drawing.Color.FromArgb(20, 30, 24);
            this.txtTenDangNhap.Location = new System.Drawing.Point(43, 56);
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.PlaceholderText = "";
            this.txtTenDangNhap.SelectedText = "";
            this.txtTenDangNhap.Size = new System.Drawing.Size(374, 44);
            this.txtTenDangNhap.TabIndex = 0;
            // txtMatKhau
            this.txtMatKhau.BorderColor = System.Drawing.Color.FromArgb(195, 205, 199);
            this.txtMatKhau.BorderRadius = 8;
            this.txtMatKhau.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMatKhau.DefaultText = "";
            this.txtMatKhau.FocusedState.BorderColor = System.Drawing.Color.FromArgb(0, 145, 62);
            this.txtMatKhau.HoverState.BorderColor = System.Drawing.Color.FromArgb(0, 145, 62);
            this.txtMatKhau.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtMatKhau.ForeColor = System.Drawing.Color.FromArgb(20, 30, 24);
            this.txtMatKhau.IconRightCursor = System.Windows.Forms.Cursors.Hand;
            this.txtMatKhau.IconRightSize = new System.Drawing.Size(24, 24);
            this.txtMatKhau.Location = new System.Drawing.Point(43, 142);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PasswordChar = '*';
            this.txtMatKhau.UseSystemPasswordChar = false;
            this.txtMatKhau.PlaceholderText = "";
            this.txtMatKhau.SelectedText = "";
            this.txtMatKhau.Size = new System.Drawing.Size(374, 44);
            this.txtMatKhau.TabIndex = 1;
            this.txtMatKhau.IconRightClick += new System.EventHandler(this.txtMatKhau_IconRightClick);
            // lblThongBao
            this.lblThongBao.AutoSize = false;
            this.lblThongBao.BackColor = System.Drawing.Color.Transparent;
            this.lblThongBao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblThongBao.ForeColor = System.Drawing.Color.FromArgb(185, 45, 45);
            this.lblThongBao.Location = new System.Drawing.Point(43, 194);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Size = new System.Drawing.Size(374, 52);
            this.lblThongBao.Text = "";
            // btnDangNhap
            this.btnDangNhap.BorderRadius = 10;
            this.btnDangNhap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDangNhap.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
            this.btnDangNhap.DisabledState.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnDangNhap.FillColor = System.Drawing.Color.FromArgb(20, 116, 51);
            this.btnDangNhap.HoverState.FillColor = System.Drawing.Color.FromArgb(0, 145, 62);
            this.btnDangNhap.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.btnDangNhap.ForeColor = System.Drawing.Color.White;
            this.btnDangNhap.Location = new System.Drawing.Point(43, 255);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(374, 48);
            this.btnDangNhap.TabIndex = 2;
            this.btnDangNhap.Text = "Đăng nhập";
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            // FrmLogin
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(840, 580);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.brandPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập - Karaoke404";
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.brandPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.brandIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lockIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.forgotIcon)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel brandPanel;
        private System.Windows.Forms.PictureBox brandIcon;
        private System.Windows.Forms.PictureBox forgotIcon;
        private System.Windows.Forms.LinkLabel linkQuenMatKhau;
        private System.Windows.Forms.PictureBox userIcon;
        private System.Windows.Forms.PictureBox lockIcon;
        private Guna.UI2.WinForms.Guna2Button btnDangNhap;
        private Guna.UI2.WinForms.Guna2TextBox txtMatKhau;
        private Guna.UI2.WinForms.Guna2TextBox txtTenDangNhap;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblThongBao;
        private System.Windows.Forms.ToolTip passwordToolTip;
    }
}
