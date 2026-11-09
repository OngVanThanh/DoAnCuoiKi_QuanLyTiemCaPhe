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

        // Biến lưu trữ RoleId của nhân viên đang được chọn để cấp tài khoản
        private int _selectedRoleId = 0;

        public FrmAddUser()
        {
            InitializeComponent();
        }

        private void FrmAddUser_Load(object sender, EventArgs e)
        {
            LoadUnlinkedEmployeesToComboBox();
            ClearInputs();
            btnTogglePassword.Text = "👁";
            btnTogglePasswordChange.Text = "👁";
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
            txtRole.Clear();
            _selectedRoleId = 0;
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

                if (_selectedRoleId <= 0)
                {
                    MessageBox.Show("Không tìm thấy chức vụ hợp lệ của nhân viên này!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    RoleId = _selectedRoleId, // Dùng biến RoleId đã lưu tự động khi chọn nhân viên
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

        private void cboFullNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFullNames.SelectedItem != null)
            {
                try
                {
                    // Lấy đối tượng nhân viên đang được chọn trong ComboBox
                    var selectedEmp = cboFullNames.SelectedItem as EmployeeDTO;

                    if (selectedEmp != null)
                    {
                        // Lưu lại RoleId của nhân viên
                        _selectedRoleId = selectedEmp.RoleId;

                        // Hiển thị tên chức vụ tương ứng lên ô txtRole (nếu EmployeeDTO có chứa thuộc tính Position hoặc RoleName)
                        // Nếu DTO của bạn dùng tên thuộc tính khác (ví dụ: Position), hãy đổi lại cho khớp:
                        txtRole.Text = selectedEmp.Position ?? string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi đồng bộ vai trò theo nhân viên: " + ex.Message);
                }
            }
            else
            {
                txtRole.Clear();
                _selectedRoleId = 0;
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

        // Biến cờ đánh dấu trạng thái mật khẩu đang ẩn hay hiện (mặc định ban đầu là đang ẩn)
        private bool _isPasswordChangeHidden = true;

        private void btnTogglePasswordChange_Click(object sender, EventArgs e)
        {
            _isPasswordChangeHidden = !_isPasswordChangeHidden; // Đảo trạng thái

            if (_isPasswordChangeHidden)
            {
                // Chuyển về dạng ẩn mật khẩu (hiển thị ký tự *)
                txtCheckPassWord.PasswordChar = '*';

                // Đổi text hoặc icon trên nút (ví dụ dùng ký tự con mắt)
                btnTogglePasswordChange.Text = "👁";
            }
            else
            {
                // Chuyển về dạng hiện mật khẩu (ký tự '\0' nghĩa là hiển thị bình thường không che)
                txtCheckPassWord.PasswordChar = '\0';

                // Đổi text hoặc icon trên nút
                btnTogglePasswordChange.Text = "👁‍🗨";
            }

            // Đưa con trỏ chuột về lại cuối đoạn text trong ô mật khẩu để trải nghiệm nhập liệu mượt mà
            txtCheckPassWord.SelectionStart = txtCheckPassWord.Text.Length;
        }
    }
}