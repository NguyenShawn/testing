namespace karaoke
{
    partial class FrmDoiMatKhau
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.title = new System.Windows.Forms.Label();
            this.labelCurrent = new System.Windows.Forms.Label();
            this.labelReplacement = new System.Windows.Forms.Label();
            this.labelConfirmation = new System.Windows.Forms.Label();
            this.current = new System.Windows.Forms.TextBox();
            this.replacement = new System.Windows.Forms.TextBox();
            this.confirmation = new System.Windows.Forms.TextBox();
            this.save = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.title.Name = "title"; this.title.Text = "ĐỔI MẬT KHẨU";
            this.title.Dock = System.Windows.Forms.DockStyle.Top; this.title.Height = 45;
            this.title.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.title.ForeColor = System.Drawing.Color.White;
            this.title.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft; this.title.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.labelCurrent.Name = "labelCurrent"; this.labelCurrent.Text = "Mật khẩu hiện tại"; this.labelCurrent.AutoSize = true; this.labelCurrent.Location = new System.Drawing.Point(28, 62);
            this.current.Name = "current"; this.current.Location = new System.Drawing.Point(28, 82); this.current.Size = new System.Drawing.Size(350, 23); this.current.UseSystemPasswordChar = true;
            this.labelReplacement.Name = "labelReplacement"; this.labelReplacement.Text = "Mật khẩu mới (tối thiểu 8 ký tự)"; this.labelReplacement.AutoSize = true; this.labelReplacement.Location = new System.Drawing.Point(28, 117);
            this.replacement.Name = "replacement"; this.replacement.Location = new System.Drawing.Point(28, 137); this.replacement.Size = new System.Drawing.Size(350, 23); this.replacement.UseSystemPasswordChar = true;
            this.labelConfirmation.Name = "labelConfirmation"; this.labelConfirmation.Text = "Nhập lại mật khẩu mới"; this.labelConfirmation.AutoSize = true; this.labelConfirmation.Location = new System.Drawing.Point(28, 172);
            this.confirmation.Name = "confirmation"; this.confirmation.Location = new System.Drawing.Point(28, 192); this.confirmation.Size = new System.Drawing.Size(350, 23); this.confirmation.UseSystemPasswordChar = true;
            this.save.Name = "save"; this.save.Text = "Lưu mật khẩu"; this.save.Location = new System.Drawing.Point(164, 242); this.save.Size = new System.Drawing.Size(115, 32);
            this.save.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.save.ForeColor = System.Drawing.Color.White; this.save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.save.Click += new System.EventHandler(this.Save_Click);
            this.cancel.Name = "cancel"; this.cancel.Text = "Hủy"; this.cancel.Location = new System.Drawing.Point(289, 242); this.cancel.Size = new System.Drawing.Size(90, 32); this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Controls.Add(this.title);
            this.Controls.Add(this.labelCurrent);
            this.Controls.Add(this.labelReplacement);
            this.Controls.Add(this.labelConfirmation);
            this.Controls.Add(this.current);
            this.Controls.Add(this.replacement);
            this.Controls.Add(this.confirmation);
            this.Controls.Add(this.save);
            this.Controls.Add(this.cancel);
            this.Text = "Đổi mật khẩu"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog; this.MaximizeBox = false; this.MinimizeBox = false;
            this.ClientSize = new System.Drawing.Size(410, 292); this.BackColor = System.Drawing.Color.White;
            this.AcceptButton = this.save; this.CancelButton = this.cancel;
            this.ResumeLayout(false); this.PerformLayout();
        }
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label labelCurrent;
        private System.Windows.Forms.Label labelReplacement;
        private System.Windows.Forms.Label labelConfirmation;
        private System.Windows.Forms.TextBox current;
        private System.Windows.Forms.TextBox replacement;
        private System.Windows.Forms.TextBox confirmation;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button cancel;
    }
}
