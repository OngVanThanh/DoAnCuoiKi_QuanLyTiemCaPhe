using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using System;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class FrmAdminAuth : Form
    {
        private readonly PermissionService _permissionService = new PermissionService();

        public bool IsSuccess { get; private set; } = false;

        public FrmAdminAuth()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
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
                IsSuccess = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Tài khoản quản lý không đúng hoặc không có quyền!", "Lỗi bảo mật", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAdminUser.Focus();
                txtAdminUser.SelectAll();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            IsSuccess = false;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
