using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class FrmLogin : Form
    {
        private AuthService _authService = new AuthService();
        public FrmLogin()
        {
            InitializeComponent();
            this.AcceptButton = btnLogin; // Nhấn Enter sẽ kích hoạt nút Login
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var user = _authService.ValidateUser(username, password);
            if (user != null)
            {

                // Đăng nhập thành công
                MessageBox.Show($"Đăng nhập thành công! Chào mừng bạn, {user.Username}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FrmMain frmMain = new FrmMain(user);
                UserSession.Login(user);
                this.Hide();
                frmMain.ShowDialog();
                this.Close();
            }
            else
            {
                // Đăng nhập thất bại
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
