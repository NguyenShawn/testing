
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
            guna2Panel1.Size = new System.Drawing.Size(380, 310);
            lblThongBao.AutoSize = false;
            lblThongBao.Location = new System.Drawing.Point(20, 165);
            lblThongBao.Size = new System.Drawing.Size(340, 50);
            btnDangNhap.Location = new System.Drawing.Point(100, 220);
            linkLabel1.Location = new System.Drawing.Point(70, 275);
            linkLabel1.Text = "Quên mật khẩu? Liên hệ Admin";
            linkLabel1.LinkClicked += (sender, args) => MessageBox.Show(
                "Nhân viên quên mật khẩu cần liên hệ Admin để xác minh và đặt lại. Nếu quên mật khẩu Admin, liên hệ người quản trị database.",
                "Hỗ trợ đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.AcceptButton = btnDangNhap;
            txtMatKhau.UseSystemPasswordChar = true;
            txtTenDangNhap.MaxLength = 50; txtMatKhau.MaxLength = 72;
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
                Enabled = false;
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
                        "Không thể đăng nhập. Kiểm tra thông tin; nếu nhập sai nhiều lần, hãy chờ 15 phút hoặc liên hệ Admin.";
                    return;
                }

                // Program disposes this form before opening FrmMain.
                txtMatKhau.Clear();
                DialogResult = DialogResult.OK;
                Close();
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
                if (!IsDisposed) { Enabled = true; btnDangNhap.Enabled = true; }
            }
        }
    }
}
