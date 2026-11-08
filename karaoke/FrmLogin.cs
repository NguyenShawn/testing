
using System;
using System.Windows.Forms;
using System.Threading.Tasks;
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
        
        private async void btnDangNhap_Click(
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
                lblThongBao.Text = "Đang kiểm tra tài khoản...";

                // Keep exceptions on the worker thread until they can be reported on the UI
                // thread. Visual Studio otherwise stops at a user-unhandled exception before
                // the await continuation reaches the outer catch.
                LoginAttemptResult loginResult = await Task.Run(
                    () => authService.TryDangNhap(tenDangNhap, matKhau));
                if (IsDisposed || Disposing)
                {
                    karaoke.Helpers.UserSession.Clear();
                    return;
                }
                lblThongBao.Text = "";

                if (loginResult.Error != null)
                {
                    karaoke.Helpers.UserSession.Clear();
                    MessageBox.Show(
                        "Không thể kiểm tra đăng nhập. Hãy kiểm tra kết nối cơ sở dữ liệu rồi thử lại.\n\n" +
                        loginResult.Error.GetBaseException().Message,
                        "Lỗi đăng nhập",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                if (!loginResult.Success)
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
                karaoke.Helpers.UserSession.Clear();
                if (IsDisposed || Disposing) return;
                lblThongBao.Text = "";
                this.Show();
                MessageBox.Show(
                    "Không thể đăng nhập: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                if (!IsDisposed) btnDangNhap.Enabled = true;
            }
        }
    }
}
