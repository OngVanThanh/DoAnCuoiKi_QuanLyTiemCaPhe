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
            using (Form authForm = new Form())
            {
                authForm.Width = 420;
                authForm.Height = 240;
                authForm.Text = "Xác nhận quyền Quản lý";
                authForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                authForm.MaximizeBox = false;
                authForm.MinimizeBox = false;
                authForm.StartPosition = FormStartPosition.CenterParent;

                // Dòng thông báo
                Label lblNotice = new Label()
                {
                    Left = 20,
                    Top = 15,
                    Width = 360,
                    Height = 30,
                    Text = "Vui lòng đăng nhập tài khoản quản lý để tiếp tục:",
                    Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Bold)
                };

                // Label & TextBox Username
                Label lblUser = new Label() { Left = 20, Top = 60, Width = 90, Text = "Tài khoản:" };
                TextBox txtAdminUser = new TextBox() { Left = 120, Top = 58, Width = 250 };

                // Label & TextBox Password
                Label lblPass = new Label() { Left = 20, Top = 100, Width = 90, Text = "Mật khẩu:" };
                TextBox txtAdminPass = new TextBox() { Left = 120, Top = 98, Width = 250, UseSystemPasswordChar = true };

                // Nút Xác nhận
                Button btnConfirm = new Button() { Text = "Xác nhận", Left = 150, Top = 145, Width = 100, Height = 30 };

                // Nút Thoát / Hủy
                Button btnExit = new Button() { Text = "Thoát", Left = 270, Top = 145, Width = 100, Height = 30 };

                bool isSuccess = false;

                btnConfirm.Click += (sender, e) => {
                    string adminUser = txtAdminUser.Text.Trim();
                    string adminPass = txtAdminPass.Text;

                    if (string.IsNullOrEmpty(adminUser) || string.IsNullOrEmpty(adminPass))
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu quản lý!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Gọi BLL kiểm tra tài khoản quản lý
                    bool isValid = _permissionService.ValidateManagerCredentials(adminUser, adminPass);
                    if (isValid)
                    {
                        isSuccess = true;
                        authForm.Close();
                    }
                    else
                    {
                        MessageBox.Show("Tài khoản quản lý không đúng hoặc không có quyền!", "Lỗi bảo mật", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtAdminUser.Focus();
                    }
                };

                btnExit.Click += (sender, e) => {
                    isSuccess = false;
                    authForm.Close();
                };

                authForm.Controls.Add(lblNotice);
                authForm.Controls.Add(lblUser);
                authForm.Controls.Add(txtAdminUser);
                authForm.Controls.Add(lblPass);
                authForm.Controls.Add(txtAdminPass);
                authForm.Controls.Add(btnConfirm);
                authForm.Controls.Add(btnExit);

                authForm.AcceptButton = btnConfirm;
                authForm.CancelButton = btnExit;

                authForm.ShowDialog();
                return isSuccess;
            }
        }

        private void btnExitNewPassword_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
