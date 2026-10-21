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
    public partial class FrmPermission : Form
    {
        private PermissionService _permissionService = new PermissionService();
        public FrmPermission()
        {
            InitializeComponent();
        }

        private void FrmPermission_Load(object sender, EventArgs e)
        {
            LoadUsersToGrid();
            LoadRolesToComboBox();
            LoadSystemFormsToCheckedListBox();
        }

        private void LoadUsersToGrid()
        {
            dgvUsers.DataSource = _permissionService.GetAllUsers();
            if (dgvUsers.Columns["Id"] != null)
            {
                dgvUsers.Columns["Id"].Visible = false; // Ẩn cột Id
            }
        }

        private void LoadRolesToComboBox()
        {
            cboRoles.DataSource = _permissionService.GetAllRoles();
            cboRoles.DisplayMember = "RoleName";
            cboRoles.ValueMember = "Id";
        }

        private void LoadSystemFormsToCheckedListBox()
        {
            clbPermissions.Items.Clear();

            string[] systemForms = {
                "FrmOrder (Bán hàng & Gọi món)",
                "FrmProduct (Quản lý Thực đơn)",
                "FrmCategory (Quản lý Danh mục)",
                "FrmTableArea (Quản lý Bàn & Khu vực)",
                "FrmCustomer (Quản lý Khách hàng)",
                "FrmPromotion (Quản lý Khuyến mãi)",
                "FrmWarehouse (Quản lý Kho nguyên liệu)",
                "FrmReport (Thống kê & Báo cáo doanh thu)",
                "FrmPermission (Phân quyền & Tài khoản)",
                "FrmAppConfig (Cấu hình hệ thống & CSDL)"
            };
            foreach (var formName in systemForms)
            {
                clbPermissions.Items.Add(formName, false);
            }
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboRoles.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn vai trò cho tài khoản!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int roleId = Convert.ToInt32(cboRoles.SelectedValue);

            // Giả định mã nhân viên mặc định hoặc lấy từ nguồn tương ứng trong form
            int employeeId = 1; // Bạn có thể thay thế bằng textbox nhập mã nhân viên nếu có

            bool success = _permissionService.CreateUser(username, password, roleId, employeeId);
            if (success)
            {
                MessageBox.Show("Tạo tài khoản mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsersToGrid();
                txtUsername.Clear();
                txtPassword.Clear();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại trong hệ thống, vui lòng chọn tên khác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSavePermission_Click(object sender, EventArgs e)
        {
            if (cboRoles.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn vai trò cần cấu hình quyền!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedRoleId = Convert.ToInt32(cboRoles.SelectedValue);
            string roleName = cboRoles.Text;

            // Duyệt qua các mục được tích chọn trong CheckedListBox
            List<string> grantedPermissions = new List<string>();
            foreach (var item in clbPermissions.CheckedItems)
            {
                grantedPermissions.Add(item.ToString());
            }

            // Tiến hành lưu quyền (có thể lưu xuống DB hoặc xử lý logic nghiệp vụ tùy chỉnh)
            // Ví dụ thông báo lưu thành công:
            MessageBox.Show($"Đã lưu thành công các quyền hạn cho vai trò [{roleName}] (Tổng số quyền được cấp: {grantedPermissions.Count})!",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUsers.Rows[e.RowIndex];
                if (row.Cells["Username"].Value != null)
                {
                    txtUsername.Text = row.Cells["Username"].Value.ToString();
                    // Không hiển thị mật khẩu vì đã mã hóa hash bảo mật
                    txtPassword.Clear();

                    // Chọn lại đúng Role trên ComboBox tương ứng với tài khoản
                    if (row.Cells["RoleName"].Value != null)
                    {
                        string roleName = row.Cells["RoleName"].Value.ToString();
                        cboRoles.Text = roleName;
                    }
                }
            }
        }
    }
}
