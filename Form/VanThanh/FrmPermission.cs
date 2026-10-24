
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class FrmPermission : Form
    {
        private readonly PermissionService _permissionService =
            new PermissionService();

        private int _selectedUserId = -1;
        private bool _isLoading = false;

        public FrmPermission()
        {
            InitializeComponent();
        }

        // 1. Khởi tạo form
        private void FrmPermission_Load(object sender, EventArgs e)
        {
            try
            {
                _isLoading = true;

                LoadSystemFormsToCheckedListBox();
                LoadRolesToComboBox();
                LoadUsersToGrid();
                ResetInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Đã xảy ra lỗi khi tải dữ liệu:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        // 2. Tải danh sách tài khoản
        private void LoadUsersToGrid()
        {
            dgvUsers.DataSource = null;
            dgvUsers.DataSource = _permissionService.GetAllUsers();

            dgvUsers.ClearSelection();

            if (dgvUsers.Columns.Contains("Id"))
                dgvUsers.Columns["Id"].HeaderText = "Mã tài khoản";

            if (dgvUsers.Columns.Contains("Username"))
                dgvUsers.Columns["Username"].HeaderText = "Tên đăng nhập";

            if (dgvUsers.Columns.Contains("RoleName"))
                dgvUsers.Columns["RoleName"].HeaderText = "Vai trò";

            if (dgvUsers.Columns.Contains("IsActive"))
                dgvUsers.Columns["IsActive"].HeaderText = "Trạng thái";

            // Không hiển thị thông tin nhạy cảm nếu DTO có các cột này.
            if (dgvUsers.Columns.Contains("PasswordHash"))
                dgvUsers.Columns["PasswordHash"].Visible = false;

            if (dgvUsers.Columns.Contains("Salt"))
                dgvUsers.Columns["Salt"].Visible = false;

            dgvUsers.ReadOnly = true;
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.MultiSelect = false;
            dgvUsers.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }

        // 3. Tải danh sách vai trò
        private void LoadRolesToComboBox()
        {
            cboRoles.DataSource = null;
            cboRoles.DisplayMember = "RoleName";
            cboRoles.ValueMember = "Id";
            cboRoles.DataSource = _permissionService.GetAllRoles();
            cboRoles.SelectedIndex = -1;
        }

        // 4. Tải danh sách màn hình quyền
        private void LoadSystemFormsToCheckedListBox()
        {
            clbPermissions.Items.Clear();

            List<string> systemForms =
                _permissionService.GetAllSystemPermisison();

            foreach (string permission in systemForms)
            {
                clbPermissions.Items.Add(permission, false);
            }

            // Danh sách quyền hiện tại chỉ dùng để hiển thị.
            clbPermissions.Enabled = false;
        }

        // 5. Xóa trạng thái đánh dấu quyền
        private void ClearPermissionChecks()
        {
            for (int i = 0; i < clbPermissions.Items.Count; i++)
            {
                clbPermissions.SetItemChecked(i, false);
            }
        }

        // 6. Đặt lại các trường nhập liệu
        private void ResetInputFields()
        {
            _selectedUserId = -1;

            txtUsername.Clear();
            txtPassword.Clear();

            cboRoles.SelectedIndex = -1;

            ClearPermissionChecks();
            dgvUsers.ClearSelection();
        }

        // 7. Lấy RoleId đang chọn
        private bool TrySelectedRoleId(out int roleId)
        {
            roleId = -1;

            if (cboRoles.SelectedValue == null)
                return false;

            return int.TryParse(
                cboRoles.SelectedValue.ToString(),
                out roleId);
        }

        // 8. Hiển thị quyền theo vai trò được chọn
        private void cboRoles_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (_isLoading)
                return;

            if (!TrySelectedRoleId(out int roleId))
            {
                ClearPermissionChecks();
                return;
            }

            List<string> grantedPermissions =
                _permissionService.GetPermissionsByRoleId(roleId);

            for (int i = 0; i < clbPermissions.Items.Count; i++)
            {
                string permission =
                    clbPermissions.Items[i].ToString();

                clbPermissions.SetItemChecked(
                    i, grantedPermissions.Contains(permission));
            }
        }

        // 9. Tạo tài khoản
        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!TrySelectedRoleId(out int roleId))
            {
                MessageBox.Show(
                    "Vui lòng chọn vai trò cho tài khoản!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool success = _permissionService.CreateUser(
                    username, password, roleId);

                if (success)
                {
                    MessageBox.Show(
                        "Tạo tài khoản thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    _isLoading = true;
                    ResetInputFields();
                    LoadUsersToGrid();
                    _isLoading = false;
                }
                else
                {
                    MessageBox.Show(
                        "Không thể tạo tài khoản. Tên đăng nhập có thể " +
                        "đã tồn tại hoặc dữ liệu không hợp lệ.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                _isLoading = false;

                MessageBox.Show(
                    "Có lỗi khi tạo tài khoản:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // 10. Chọn tài khoản trên DataGridView
        private void dgvUsers_CellClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvUsers.Rows[e.RowIndex];

            if (row.Cells["Id"].Value == null)
                return;

            if (!int.TryParse(
                row.Cells["Id"].Value.ToString(),
                out int userId))
            {
                return;
            }

            _selectedUserId = userId;

            txtUsername.Text =
                row.Cells["Username"].Value?.ToString() ?? "";

            // Không hiển thị lại mật khẩu khi chọn tài khoản.
            txtPassword.Clear();

            string roleName =
                row.Cells["RoleName"].Value?.ToString();

            _isLoading = true;

            if (!string.IsNullOrWhiteSpace(roleName))
            {
                int roleIndex = cboRoles.FindStringExact(roleName);
                cboRoles.SelectedIndex = roleIndex;
            }
            else
            {
                cboRoles.SelectedIndex = -1;
            }

            _isLoading = false;

            // Cập nhật danh sách quyền của vai trò đang chọn.
            if (TrySelectedRoleId(out int roleId))
            {
                List<string> permissions =
                    _permissionService.GetPermissionsByRoleId(roleId);

                for (int i = 0; i < clbPermissions.Items.Count; i++)
                {
                    string item = clbPermissions.Items[i].ToString();

                    clbPermissions.SetItemChecked(
                        i, permissions.Contains(item));
                }
            }
            else
            {
                ClearPermissionChecks();
            }
        }

        // 11. Lưu thay đổi vai trò
        private void btnSavePermission_Click(
            object sender, EventArgs e)
        {
            if (_selectedUserId <= 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn tài khoản cần cập nhật!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!TrySelectedRoleId(out int newRoleId))
            {
                MessageBox.Show(
                    "Vui lòng chọn vai trò mới!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool success = _permissionService.UpdateUserRole(
                    _selectedUserId, newRoleId);

                if (success)
                {
                    int updatedUserId = _selectedUserId;

                    MessageBox.Show(
                        "Cập nhật vai trò thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    _isLoading = true;
                    LoadUsersToGrid();
                    SelectUserInGrid(updatedUserId);
                    _isLoading = false;
                }
                else
                {
                    MessageBox.Show(
                        "Không thể cập nhật vai trò. " +
                        "Vui lòng kiểm tra tài khoản và vai trò.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                _isLoading = false;

                MessageBox.Show(
                    "Có lỗi khi cập nhật vai trò:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // 12. Chọn lại tài khoản sau khi tải lại lưới
        private void SelectUserInGrid(int userId)
        {
            foreach (DataGridViewRow row in dgvUsers.Rows)
            {
                if (row.Cells["Id"].Value == null)
                    continue;

                if (Convert.ToInt32(row.Cells["Id"].Value) != userId)
                    continue;

                row.Selected = true;
                dgvUsers.CurrentCell = row.Cells["Id"];

                _selectedUserId = userId;

                txtUsername.Text =
                    row.Cells["Username"].Value?.ToString() ?? "";

                txtPassword.Clear();

                string roleName =
                    row.Cells["RoleName"].Value?.ToString();

                int roleIndex = cboRoles.FindStringExact(roleName ?? "");

                if (roleIndex >= 0)
                    cboRoles.SelectedIndex = roleIndex;

                return;
            }

            _selectedUserId = -1;
        }

        // 13. Xóa tài khoản
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (_selectedUserId <= 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn tài khoản cần xóa!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa tài khoản ID: " +
                _selectedUserId + " không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                bool success =
                    _permissionService.DeleteUser(_selectedUserId);

                if (success)
                {
                    MessageBox.Show(
                        "Xóa tài khoản thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    _isLoading = true;
                    ResetInputFields();
                    LoadUsersToGrid();
                    _isLoading = false;
                }
                else
                {
                    MessageBox.Show(
                        "Không tìm thấy tài khoản hoặc không thể xóa.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                _isLoading = false;

                MessageBox.Show(
                    "Có lỗi khi xóa tài khoản. Có thể tài khoản " +
                    "đang liên kết với dữ liệu khác.\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
