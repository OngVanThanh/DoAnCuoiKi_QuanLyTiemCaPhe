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
    public partial class FrmForgotPassword : Form
    {
        private PermissionService _permissionService = new PermissionService();
        public FrmForgotPassword()
        {
            InitializeComponent();
        }
        private void btnSaveNewPassword_Click(object sender, EventArgs e)
        {
            // 1. Validate Id nhân viên
            if (!int.TryParse(txtId.Text.Trim(), out int employeeId))
            {
                MessageBox.Show("Mã nhân viên (Id) phải là một số nguyên hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtId.Focus();
                return;
            }

            string phone = txtPhone.Text.Trim();
            string newPass = txtNewPassword.Text;
            string checkPass = txtCheckPassword.Text;

            // 2. Validate dữ liệu trống
            if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(checkPass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin số điện thoại và mật khẩu mới!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Kiểm tra khớp mật khẩu
            if (newPass != checkPass)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp với mật khẩu mới. Vui lòng kiểm tra lại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCheckPassword.Focus();
                txtCheckPassword.SelectAll();
                return;
            }

            // 4. HIỂN THỊ HỘP THOẠI XÁC THỰC QUẢN LÝ NGAY TRONG CODE (KHÔNG CẦN TẠO FORM MỚI)
            bool isAdminVerified = ShowAdminAuthDialog();
            if (!isAdminVerified)
            {
                return; // Nếu bấm thoát hoặc nhập sai tài khoản admin thì dừng lại
            }

            // 5. Nếu Admin đã xác thực thành công, tiến hành gọi BLL đổi mật khẩu
            bool success = _permissionService.ResetPasswordWithVerification(employeeId, phone, newPass);

            if (success)
            {
                MessageBox.Show("Đặt lại mật khẩu thành công dưới sự phê duyệt của quản lý!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Mã nhân viên (Id) hoặc Số điện thoại không khớp với dữ liệu trên hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtId.Focus();
            }
        }
        private bool ShowAdminAuthDialog()
        {
            using (FrmAdminAuth authForm = new FrmAdminAuth())
            {
                DialogResult dr = authForm.ShowDialog(this);
                return dr == DialogResult.OK && authForm.IsSuccess;
            }
        }

        private void btnExitNewPassword_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
