
using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class FrmPermission : Form
    {
        private readonly PermissionService _permissionService = new PermissionService();
        private UCPermissionControl _ucPermissionControl;

        private int _selectedRowIndex = -1;
        private bool _isLoading = false;
        private ToolStripDropDown _actionDropDown;

        public FrmPermission()
        {
            InitializeComponent();
        }

        // Khởi tạo form
        private void FrmPermission_Load(object sender, EventArgs e)
        {
            try
            {
                _isLoading = true;
                LoadRolesToFilter();
                FilterUsers();
                InitActionPopup();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }
        private void LoadRolesToFilter()
        {
            var filterList = new List<RoleDTO>
            {
                new RoleDTO(0, "Tất cả vai trò")
            };

            var roles = _permissionService.GetAllRoles();
            if (roles != null && roles.Count > 0)
            {
                foreach (var r in roles)
                {
                    filterList.Add(new RoleDTO(r.Id, r.RoleName));
                }
            }

            cboFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilter.DisplayMember = "RoleName";
            cboFilter.ValueMember = "Id";
            cboFilter.DataSource = filterList;

            if (cboFilter.Items.Count > 0)
            {
                cboFilter.SelectedIndex = 0;
            }
        }


        // Lọc người dùng theo Username / Họ và tên và Vai trò
        private void FilterUsers()
        {
            if (_isLoading) return;

            string keyword = txtSearch.Text.Trim();
            int selectedRoleId = 0;
            if (cboFilter.SelectedValue is int rId)
            {
                selectedRoleId = rId;
            }
            else if (cboFilter.SelectedItem is RoleDTO rDto)
            {
                selectedRoleId = rDto.Id;
            }

            List<UserDTO> allUsers = null;
            try
            {
                allUsers = _permissionService.GetAllUsers();
            }
            catch
            {
                allUsers = new List<UserDTO>();
            }

            if (allUsers == null) allUsers = new List<UserDTO>();

            var filtered = allUsers.Where(u =>
            {
                // 1. Lọc theo vai trò nếu được chọn (RoleId > 0)
                if (selectedRoleId > 0 && u.RoleId != selectedRoleId)
                    return false;

                // 2. Tìm kiếm theo Username hoặc Họ và tên
                if (!string.IsNullOrEmpty(keyword))
                {
                    bool matchUsername = !string.IsNullOrEmpty(u.Username)
                                      && u.Username.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
                    bool matchFullName = !string.IsNullOrEmpty(u.FullName)
                                      && u.FullName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
                    return matchUsername || matchFullName;
                }

                return true;
            }).ToList();

            dgvUsers.AutoGenerateColumns = false;
            dgvUsers.DataSource = filtered;
        }

        // Khởi tạo popup menu thao tác (dùng Label thay vì UserControl)
        private void InitActionPopup()
        {
            _ucPermissionControl = new UCPermissionControl();
            DataGridViewButtonColumn btnActionCol = dgvUsers.Columns["Control"] as DataGridViewButtonColumn;
            if (btnActionCol != null)
            {
                btnActionCol.Text = "⚙️";
                btnActionCol.UseColumnTextForButtonValue = true;
            }
            // Gắn sự kiện khi bấm nút bên trong User Control
            _ucPermissionControl.ChangePasswordClicked += (s, e) => {
                _actionDropDown.Close();
                ExecuteChangePasswordAction(_selectedRowIndex);
            };

            _ucPermissionControl.DeleteClicked += (s, e) => {
                _actionDropDown.Close();
                ExecuteDeleteAction(_selectedRowIndex);
            };

            // Đưa UserControl vào ToolStripDropDown để làm popup
            _actionDropDown = new ToolStripDropDown();
            ToolStripControlHost host = new ToolStripControlHost(_ucPermissionControl);
            host.Margin = Padding.Empty;
            host.Padding = Padding.Empty;
            _actionDropDown.Items.Add(host);
        }
        // Tải danh sách tài khoản
        private void LoadUsersToGrid()
        {
            FilterUsers();
        }

        // 10. Chọn tài khoản trên DataGridView
        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvUsers.Columns[e.ColumnIndex].Name == "Control")
            {
                // Nếu popup đang mở VÀ người dùng bấm lại chính đúng dòng đó -> Ẩn đi
                if (_actionDropDown.Visible && _selectedRowIndex == e.RowIndex)
                {
                    _actionDropDown.Close();
                    _selectedRowIndex = -1; // Reset lại vị trí
                    return;
                }

                // Nếu bấm sang dòng khác hoặc popup đang đóng -> Mở lên bình thường
                _selectedRowIndex = e.RowIndex;

                // Lấy tọa độ của ô hiện tại trên màn hình
                Rectangle cellRect = dgvUsers.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                Point point = new Point(cellRect.Right, cellRect.Top);

                if (_actionDropDown != null)
                {
                    _actionDropDown.Show(dgvUsers, point);
                }
            }
        }

        private void dgvUsers_Leave(object sender, EventArgs e)
        {
            dgvUsers.ClearSelection();
        }
        // 13. Xóa tài khoản (Được gọi từ Label chức năng hoặc nút xóa)
        private void ExecuteDeleteAction(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvUsers.Rows.Count)
                return;

            DataGridViewRow row = dgvUsers.Rows[rowIndex];

            // Lấy tên đăng nhập (Username) từ cột "Username" trên lưới
            // (Hãy đảm bảo tên cột trong giao diện thiết kế của bạn chính xác là "Username")
            string username = row.Cells["Username"].Value?.ToString();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Không tìm thấy tên tài khoản để xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hộp thoại xác nhận xóa kèm tên tài khoản cho trực quan
            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa tài khoản [{username}] không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                // Gọi Service thực hiện xóa bằng Username
                bool success = _permissionService.DeleteUser(username);

                if (success)
                {
                    MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _isLoading = true;
                    LoadUsersToGrid(); // Tải lại danh sách trên lưới
                    _isLoading = false;
                }
                else
                {
                    MessageBox.Show("Không thể xóa tài khoản. Vui lòng thử lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                _isLoading = false;
                MessageBox.Show("Có lỗi khi xóa tài khoản:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExecuteChangePasswordAction(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvUsers.Rows.Count) return;

            // Lấy tên đăng nhập từ DataGridView
            string username = dgvUsers.Rows[rowIndex].Cells["Username"].Value?.ToString();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Không tìm thấy thông tin tài khoản!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mở form đổi mật khẩu và truyền username vào
            FrmChangePassword frmChange = new FrmChangePassword(username);
            frmChange.ShowDialog();
        }
        private void btnCreateUser_Click_1(object sender, EventArgs e)
        {
            // Mở Form FrmAddUser dưới dạng Dialog để chặn tương tác form cha cho đến khi đóng form con
            FrmAddUser frmAdd = new FrmAddUser();

            // Khi form FrmAddUser được đóng lại và trả về kết quả OK (tạo thành công)
            if (frmAdd.ShowDialog() == DialogResult.OK)
            {
                // Tiến hành load lại danh sách tài khoản trên DataGridView
                LoadUsersToGrid();
            }
        }
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterUsers();
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FilterUsers();
                e.SuppressKeyPress = true; // Chặn tiếng kêu "beep" khi nhấn Enter
            }
        }

        private void CboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterUsers();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            FilterUsers();
        }
    }
}
