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
    }
}
