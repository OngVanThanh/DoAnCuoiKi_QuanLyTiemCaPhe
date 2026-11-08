using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
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
    public partial class FrmAddUser : Form
    {
        private PermissionService _permissionService = new PermissionService();
        private EmployeeRepository _employ = new EmployeeRepository();
        public FrmAddUser()
        {
            InitializeComponent();
        }

        private void FrmAddUser_Load(object sender, EventArgs e)
        {
            LoadRolesToComboBox();
            LoadUnlinkedEmployeesToComboBox();
            ClearInputs();
        }
        private void LoadRolesToComboBox()
        {
            cboRoles.DataSource = null;
            cboRoles.DisplayMember = "RoleName";
            cboRoles.ValueMember = "Id";
            cboRoles.DataSource = _permissionService.GetAllRoles();
            cboRoles.SelectedIndex = -1;
        }
        private void LoadUnlinkedEmployeesToComboBox()
        {
            if (cboFullNames == null) return;

            var listUnlinkedEmployees = _employ.GetEmployeesWithoutAccount();

            cboFullNames.DataSource = listUnlinkedEmployees;
            cboFullNames.DisplayMember = "FullName";
            cboFullNames.ValueMember = "Id";
            cboFullNames.SelectedIndex = -1;
        }
        private void ClearInputs()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtCheckPassWord.Clear();
            if (cboFullNames.Items.Count > 0) cboFullNames.SelectedIndex = -1;
            if (cboRoles.Items.Count > 0) cboRoles.SelectedIndex = -1;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validate các ô nhập liệu và ComboBox
                if (cboFullNames.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần cấp tài khoản!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboFullNames.Focus();
                    return;
                }

                if (cboRoles.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn vai trò (Role) cho tài khoản!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboRoles.Focus();
                    return;
                }

                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text;
                string confirmPassword = txtCheckPassWord.Text;

                if (string.IsNullOrWhiteSpace(username))
                {
                    MessageBox.Show("Tên đăng nhập không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Mật khẩu không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                // 2. Kiểm tra mật khẩu nhập lại (txtCheckPassWord) có khớp không
                if (password != confirmPassword)
                {
                    MessageBox.Show("Mật khẩu nhập lại không khớp! Vui lòng kiểm tra lại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCheckPassWord.Focus();
                    txtCheckPassWord.SelectAll();
                    return;
                }

                // 3. Đóng gói dữ liệu vào UserDTO
                UserDTO newUser = new UserDTO
                {
                    Username = username,
                    EmployeeId = (int)cboFullNames.SelectedValue,
                    RoleId = (int)cboRoles.SelectedValue,
                    IsActive = true
                };

                // 4. Gọi Service thực hiện tạo tài khoản
                bool isSuccess = _permissionService.CreateUser(newUser, password);

                if (isSuccess)
                {
                    MessageBox.Show("Tạo tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Trả về kết quả OK để form quản lý bên ngoài biết và load lại DataGridView
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tạo tài khoản thất bại! Tên đăng nhập có thể đã tồn tại trên hệ thống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra ngoại lệ: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút Hủy / Thoát form
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
