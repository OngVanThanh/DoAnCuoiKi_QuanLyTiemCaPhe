using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class FrmLogin : Form
    {
        private readonly AuthService _authService = new AuthService();

        public FrmLogin()
        {
            InitializeComponent();
            this.AcceptButton = btnLogin;
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '●';
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            var user = _authService.ValidateUser(username, password);
            if (user != null)
            {
                UserSession.Login(user);
                FrmMain frmMain = new FrmMain(user);
                this.Hide();
                frmMain.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.SelectAll();
                txtPassword.Focus();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lnkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (FrmForgotPassword frm = new FrmForgotPassword())
            {
                frm.ShowDialog(this);
            }
        }
        private bool _isPasswordHidden = true;
        private void btnTogglePassword_Click(object sender, EventArgs e)
        {
            _isPasswordHidden = !_isPasswordHidden; // Đảo trạng thái

            if (_isPasswordHidden)
            {
                // Chuyển về dạng ẩn mật khẩu (hiển thị ký tự *)
                txtPassword.PasswordChar = '*';

                // Đổi text hoặc icon trên nút (ví dụ dùng ký tự con mắt)
                btnTogglePassword.Text = "👁";
            }
            else
            {
                // Chuyển về dạng hiện mật khẩu (ký tự '\0' nghĩa là hiển thị bình thường không che)
                txtPassword.PasswordChar = '\0';

                // Đổi text hoặc icon trên nút
                btnTogglePassword.Text = "👁‍🗨";
            }

            // Đưa con trỏ chuột về lại cuối đoạn text trong ô mật khẩu để trải nghiệm nhập liệu mượt mà
            txtPassword.SelectionStart = txtPassword.Text.Length;
        }
    }
}