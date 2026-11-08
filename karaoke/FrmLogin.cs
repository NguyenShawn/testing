
using System;
using System.Windows.Forms;
using karaoke.Services;
namespace karaoke
{
    public partial class FrmLogin : Form
    {
        private readonly AuthService authService =
            new AuthService();

        public FrmLogin()
        {
            InitializeComponent();
            this.AcceptButton = btnDangNhap;
            txtMatKhau.UseSystemPasswordChar = true;
        }
        
        private void btnDangNhap_Click(
            object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;

            lblThongBao.Text = "";

            if (tenDangNhap == "" || matKhau == "")
            {
                lblThongBao.Text =
                    "Vui lòng nhập tài khoản và mật khẩu!";
                return;
            }

            try
            {
                btnDangNhap.Enabled = false;

                bool thanhCong = authService.DangNhap(
                    tenDangNhap, matKhau
                );

                if (!thanhCong)
                {
                    lblThongBao.Text =
                        "Thông tin đăng nhập không hợp lệ!";
                    return;
                }

                // Mo Dashboard sau khi dang nhap thanh cong
                this.Hide();

                using (FrmMain main = new FrmMain())
                {
                    main.ShowDialog();
                }

                // Dong Login khi FrmMain da dong
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể đăng nhập: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                btnDangNhap.Enabled = true;
            }
        }
    }
}
