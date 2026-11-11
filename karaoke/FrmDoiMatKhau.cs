using System;
using System.Drawing;
using System.Windows.Forms;
using karaoke.Services;
using karaoke.UI;

namespace karaoke
{
    public sealed partial class FrmDoiMatKhau : Form
    {
        public FrmDoiMatKhau()
        {
            InitializeComponent();
            ButtonActive.RoundButton(save); ButtonActive.RoundButton(cancel);
            cancel.Click += (sender, e) => Close();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (replacement.Text != confirmation.Text)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp.", "Đổi mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                confirmation.Focus(); return;
            }
            try
            {
                new AccountService().ChangeOwnPassword(current.Text, replacement.Text);
                MessageBox.Show("Đổi mật khẩu thành công.", "Đổi mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK; Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message, "Không thể đổi mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
