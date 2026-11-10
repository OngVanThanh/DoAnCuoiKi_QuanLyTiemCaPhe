using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class UCEmployeeAdd : UserControl
    {
        public event EventHandler SaveCompleted;
        public event EventHandler CancelClicked;

        private EmployeeService _employeeService = new EmployeeService();
        private EmployeeRepository _employeeRepository = new EmployeeRepository();
        private PermissionService _permissionService = new PermissionService();
        private string _imagePath = string.Empty; // Biến lưu đường dẫn ảnh đại diện

        public UCEmployeeAdd()
        {
            InitializeComponent();
            LoadRolesToComboBox();

            // Đăng ký sự kiện tự động đổi lương khi chọn chức vụ
            cbPosition.SelectedIndexChanged += cbPosition_SelectedIndexChanged;
        }

        private void LoadRolesToComboBox()
        {
            try
            {
                var roles = _permissionService.GetAllRoles();
                cbPosition.DataSource = roles;
                cbPosition.DisplayMember = "RoleName";
                cbPosition.ValueMember = "Id";
                cbPosition.SelectedIndex = -1; // Mặc định chưa chọn gì
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách chức vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tự động cập nhật mức lương tương ứng khi người dùng đổi chức vụ
            if (cbPosition.SelectedValue != null && int.TryParse(cbPosition.SelectedValue.ToString(), out int selectedRoleId))
            {
                try
                {
                    // Lấy thông tin lương (Amount và TimeUnit) từ bảng Roles thông qua Service hoặc Repository
                    var salaryInfo = _employeeRepository.GetSalaryInfoByRoleId(selectedRoleId); // Hoặc gọi qua EmployeeRepository

                    // Hiển thị lên ô txtSalary (giả sử có TextBox txtSalary trên giao diện thêm)
                    if (txtSalary != null)
                    {
                        txtSalary.Text = $"{salaryInfo.Amount:N0} VNĐ / {salaryInfo.TimeUnit}";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi load lương theo Role: " + ex.Message);
                }
            }
        }

        // Sự kiện chọn ảnh đại diện (nếu bạn có nút chọn ảnh ptbAvatar / btnUpload)
        private void ptbAvatar_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Thu thập dữ liệu từ giao diện
                string fullName = txtFullName.Text.Trim();
                string phone = txtPhone.Text.Trim();
                string address = txtAddress.Text.Trim();

                int roleId = 0;
                if (cbPosition.SelectedValue != null)
                {
                    int.TryParse(cbPosition.SelectedValue.ToString(), out roleId);
                }

                DateTime? birthDate = dtpBirthDate.Checked ? (DateTime?)dtpBirthDate.Value : null;
                DateTime? hireDate = dtpHireDate.Checked ? (DateTime?)dtpHireDate.Value : null;

                // 2. Đóng gói vào DTO (Id = 0 để biểu thị thêm mới)
                EmployeeDTO newDto = new EmployeeDTO
                {
                    Id = 0,
                    FullName = fullName,
                    Phone = phone,
                    Address = address,
                    BirthDate = birthDate,
                    RoleId = roleId,
                    HireDate = hireDate,
                    Image = _imagePath
                };

                // 3. Gọi Service để kiểm tra validate và thêm vào cơ sở dữ liệu
                string errorMsg = string.Empty;
                bool isSuccess = _employeeService.AddEmployee(newDto, out errorMsg);

                if (isSuccess)
                {
                    MessageBox.Show("Thêm nhân viên mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Kích hoạt sự kiện hoàn tất để Form cha load lại DataGridView
                    SaveCompleted?.Invoke(this, EventArgs.Empty);

                    // Đóng/xóa UserControl khỏi Panel
                    this.Parent?.Controls.Remove(this);
                }
                else
                {
                    MessageBox.Show(errorMsg, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelClicked?.Invoke(this, EventArgs.Empty);
            this.Parent?.Controls.Remove(this);
        }
        private void btnAddAvatar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Chọn ảnh đại diện nhân viên";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Lưu lại đường dẫn tệp ảnh vào biến toàn cục (ví dụ: _currentImagePath hoặc _imagePath)
                    _imagePath = ofd.FileName;

                    // Hiển thị ảnh lên PictureBox và căn chỉnh vừa khít
                    ptbAvatar.Image = Image.FromFile(_imagePath);
                    ptbAvatar.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }
    }
}