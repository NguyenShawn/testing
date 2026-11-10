namespace karaoke
{
    partial class FrmQuanLyNhomQuyen
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.header = new System.Windows.Forms.Label();
            this.body = new System.Windows.Forms.Panel();
            this.labelGroups = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelPermissions = new System.Windows.Forms.Label();
            this.groups = new System.Windows.Forms.ListBox();
            this.permissions = new System.Windows.Forms.CheckedListBox();
            this.name = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout(); this.body.SuspendLayout();
            this.header.Name = "header"; this.header.Dock = System.Windows.Forms.DockStyle.Top; this.header.Height = 46; this.header.Text = "QUẢN LÝ NHÓM QUYỀN"; this.header.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.header.ForeColor = System.Drawing.Color.White; this.header.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold); this.header.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0); this.header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.body.Name = "body"; this.body.Size = new System.Drawing.Size(820, 494); this.body.Dock = System.Windows.Forms.DockStyle.Fill; this.body.Padding = new System.Windows.Forms.Padding(15); this.body.AutoScroll = true;
            this.labelGroups.Name = "labelGroups"; this.labelGroups.Text = "Nhóm quyền"; this.labelGroups.AutoSize = true; this.labelGroups.Location = new System.Drawing.Point(20, 15);
            this.labelName.Name = "labelName"; this.labelName.Text = "Tên nhóm"; this.labelName.AutoSize = true; this.labelName.Location = new System.Drawing.Point(260, 15);
            this.labelPermissions.Name = "labelPermissions"; this.labelPermissions.Text = "Chức năng được phép"; this.labelPermissions.AutoSize = true; this.labelPermissions.Location = new System.Drawing.Point(260, 75);
            this.groups.Name = "groups"; this.groups.Location = new System.Drawing.Point(20, 40); this.groups.Size = new System.Drawing.Size(220, 355); this.groups.DisplayMember = "TenNhom"; this.groups.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left; this.groups.SelectedIndexChanged += new System.EventHandler(this.Groups_Changed);
            this.name.Name = "name"; this.name.Location = new System.Drawing.Point(260, 38); this.name.Size = new System.Drawing.Size(480, 23); this.name.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.permissions.Name = "permissions"; this.permissions.CheckOnClick = true; this.permissions.Location = new System.Drawing.Point(260, 100); this.permissions.Size = new System.Drawing.Size(480, 290); this.permissions.DisplayMember = "TenQuyen"; this.permissions.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.btnAdd.Name = "btnAdd"; this.btnAdd.Text = "Nhóm mới"; this.btnAdd.Location = new System.Drawing.Point(20, 435); this.btnAdd.Size = new System.Drawing.Size(118, 34); this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom; this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnAdd.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.btnAdd.ForeColor = System.Drawing.Color.White; this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnSave.Name = "btnSave"; this.btnSave.Text = "Lưu nhóm"; this.btnSave.Location = new System.Drawing.Point(150, 435); this.btnSave.Size = new System.Drawing.Size(118, 34); this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom; this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnSave.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.btnSave.ForeColor = System.Drawing.Color.White; this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnDelete.Name = "btnDelete"; this.btnDelete.Text = "Xóa nhóm"; this.btnDelete.Location = new System.Drawing.Point(280, 435); this.btnDelete.Size = new System.Drawing.Size(118, 34); this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom; this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnDelete.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); this.btnDelete.ForeColor = System.Drawing.Color.White; this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.body.Controls.Add(this.labelGroups);
            this.body.Controls.Add(this.labelName);
            this.body.Controls.Add(this.labelPermissions);
            this.body.Controls.Add(this.groups);
            this.body.Controls.Add(this.permissions);
            this.body.Controls.Add(this.name);
            this.body.Controls.Add(this.btnAdd);
            this.body.Controls.Add(this.btnSave);
            this.body.Controls.Add(this.btnDelete);
            this.Controls.Add(this.body); this.Controls.Add(this.header); this.Name = "FrmQuanLyNhomQuyen"; this.Text = "Quản lý nhóm quyền"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent; this.ClientSize = new System.Drawing.Size(820, 540); this.MinimumSize = new System.Drawing.Size(700, 480); this.BackColor = System.Drawing.Color.White;
            this.body.ResumeLayout(false); this.body.PerformLayout(); this.ResumeLayout(false);
        }
        private System.Windows.Forms.Label header;
        private System.Windows.Forms.Panel body;
        private System.Windows.Forms.Label labelGroups;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelPermissions;
        private System.Windows.Forms.ListBox groups;
        private System.Windows.Forms.CheckedListBox permissions;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
    }
}
