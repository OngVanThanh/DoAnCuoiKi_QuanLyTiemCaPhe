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
    public partial class FrmChangePassword : Form
    {
        private readonly PermissionService _permissionService = new PermissionService();
        private string _targetUsername;
        public FrmChangePassword(string username)
        {
            InitializeComponent();
            btnTogglePassword.Text = "👁";
            btnTogglePasswordChange.Text = "👁";
            _targetUsername = username;
        }
        private void btnExitNewPassword_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSaveNewPassword_Click(object sender, EventArgs e)
        {
            string oldPass = txtOldPassword.Text;
            string newPass = txtNewPassword.Text;
            string confirmPass = txtCheckPassword.Text; // Lấy giá trị từ ô nhập lại mật khẩu

            // 1. Validate không để trống
            if (string.IsNullOrEmpty(oldPass))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu cũ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOldPassword.Focus();
                return;
            }

            if (string.IsNullOrEmpty(newPass))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return;
            }

            if (string.IsNullOrEmpty(confirmPass))
            {
                MessageBox.Show("Vui lòng xác nhận lại mật khẩu mới!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCheckPassword.Focus();
                return;
            }

            // 2. Kiểm tra mật khẩu mới và xác nhận mật khẩu có khớp nhau không
            if (newPass != confirmPass)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp với mật khẩu mới. Vui lòng kiểm tra lại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCheckPassword.Focus();
                txtCheckPassword.SelectAll();
                return;
            }

            // 3. Gọi BLL thực hiện đổi mật khẩu
            bool isSuccess = _permissionService.ChangePassword(_targetUsername, oldPass, newPass);

            if (isSuccess)
            {
                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Đổi mật khẩu thất bại! Mật khẩu cũ không chính xác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOldPassword.Focus();
                txtOldPassword.SelectAll();
            }
        }

        private bool _isPasswordHidden = true;

        private void btnTogglePassword_Click(object sender, EventArgs e)
        {
            _isPasswordHidden = !_isPasswordHidden; // Đảo trạng thái

            if (_isPasswordHidden)
            {
                // Chuyển về dạng ẩn mật khẩu (hiển thị ký tự *)
                txtNewPassword.PasswordChar = '*';

                // Đổi text hoặc icon trên nút (ví dụ dùng ký tự con mắt)
                btnTogglePassword.Text = "👁";
            }
            else
            {
                // Chuyển về dạng hiện mật khẩu (ký tự '\0' nghĩa là hiển thị bình thường không che)
                txtNewPassword.PasswordChar = '\0';

                // Đổi text hoặc icon trên nút
                btnTogglePassword.Text = "👁‍🗨";
            }

            // Đưa con trỏ chuột về lại cuối đoạn text trong ô mật khẩu để trải nghiệm nhập liệu mượt mà
            txtNewPassword.SelectionStart = txtNewPassword.Text.Length;
        }

        // Biến cờ đánh dấu trạng thái mật khẩu đang ẩn hay hiện (mặc định ban đầu là đang ẩn)
        private bool _isPasswordChangeHidden = true;

        private void btnTogglePasswordChange_Click(object sender, EventArgs e)
        {
            _isPasswordChangeHidden = !_isPasswordChangeHidden; // Đảo trạng thái

            if (_isPasswordChangeHidden)
            {
                // Chuyển về dạng ẩn mật khẩu (hiển thị ký tự *)
                txtCheckPassword.PasswordChar = '*';

                // Đổi text hoặc icon trên nút (ví dụ dùng ký tự con mắt)
                btnTogglePasswordChange.Text = "👁";
            }
            else
            {
                // Chuyển về dạng hiện mật khẩu (ký tự '\0' nghĩa là hiển thị bình thường không che)
                txtCheckPassword.PasswordChar = '\0';

                // Đổi text hoặc icon trên nút
                btnTogglePasswordChange.Text = "👁‍🗨";
            }

            // Đưa con trỏ chuột về lại cuối đoạn text trong ô mật khẩu để trải nghiệm nhập liệu mượt mà
            txtCheckPassword.SelectionStart = txtCheckPassword.Text.Length;
        }
    }
}
