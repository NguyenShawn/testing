namespace karaoke
{
    partial class FrmInHoaDon
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnIn = new System.Windows.Forms.Button();
            this.rtbHoaDon = new System.Windows.Forms.RichTextBox();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(520, 45);
            this.panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(206, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "HÓA ĐƠN THANH TOÁN";
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelBottom.Controls.Add(this.btnDong);
            this.panelBottom.Controls.Add(this.btnIn);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 565);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(520, 55);
            this.panelBottom.TabIndex = 1;
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDong.FlatAppearance.BorderSize = 0;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(408, 10);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(100, 35);
            this.btnDong.TabIndex = 1;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnIn
            // 
            this.btnIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.btnIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIn.FlatAppearance.BorderSize = 0;
            this.btnIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIn.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnIn.ForeColor = System.Drawing.Color.White;
            this.btnIn.Location = new System.Drawing.Point(282, 10);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(120, 35);
            this.btnIn.TabIndex = 0;
            this.btnIn.Text = "In Hóa Đơn";
            this.btnIn.UseVisualStyleBackColor = false;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // rtbHoaDon
            // 
            this.rtbHoaDon.BackColor = System.Drawing.Color.White;
            this.rtbHoaDon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbHoaDon.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbHoaDon.Location = new System.Drawing.Point(0, 45);
            this.rtbHoaDon.Name = "rtbHoaDon";
            this.rtbHoaDon.ReadOnly = true;
            this.rtbHoaDon.WordWrap = false;
            this.rtbHoaDon.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.rtbHoaDon.Size = new System.Drawing.Size(520, 520);
            this.rtbHoaDon.TabIndex = 2;
            this.rtbHoaDon.Text = "";
            // 
            // FrmInHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(520, 620);
            this.Controls.Add(this.rtbHoaDon);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmInHoaDon";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hóa Đơn Thanh Toán";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.RichTextBox rtbHoaDon;
    }
}
