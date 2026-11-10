namespace karaoke
{
    partial class FrmBaoCaoRdlc
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.viewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            this.viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer.Name = "viewer";
            this.viewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            this.Controls.Add(this.viewer);
            this.Name = "FrmBaoCaoRdlc";
            this.Text = "Báo cáo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ClientSize = new System.Drawing.Size(934, 681);
            this.ResumeLayout(false);
        }
        private Microsoft.Reporting.WinForms.ReportViewer viewer;
    }
}
