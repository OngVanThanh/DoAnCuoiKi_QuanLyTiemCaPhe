
using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class FrmPermission : Form
    {
        private readonly PermissionService _permissionService =new PermissionService();
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
                LoadUsersToGrid();
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
            dgvUsers.AutoGenerateColumns = false;
            dgvUsers.DataSource = _permissionService.GetAllUsers();
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
    }
}
