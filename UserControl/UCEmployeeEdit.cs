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
    public partial class UCEmployeeEdit : UserControl
    {
        public event EventHandler SaveCompleted;
        public event EventHandler CancelClicked;

        private EmployeeService _employeeService = new EmployeeService();
        private PermissionService _permissionService = new PermissionService();
        private int _employeeId = -1;
        private string _currentImagePath = string.Empty;
        public UCEmployeeEdit()
        {
            InitializeComponent();
            LoadRolesToComboBox();
        }
        private void LoadRolesToComboBox()
        {
            cbPosition.DataSource = _permissionService.GetAllRoles();
            cbPosition.DisplayMember = "RoleName";
            cbPosition.ValueMember = "Id";
            cbPosition.DataSource = _permissionService.GetAllRoles();
            cbPosition.SelectedIndex = -1;
        }
        public void SetEmployeeData(EmployeeDTO emp)
        {
            if (emp != null)
            {
                _employeeId = emp.Id; // Khớp với biến _employeeId đã khai báo
                txtFullName.Text = emp.FullName;
                txtPhone.Text = emp.Phone;
                txtAddress.Text = emp.Address;
                txtSalary.Text = emp.Salary?.ToString();

                if (emp.BirthDate.HasValue) dtpBirthDate.Value = emp.BirthDate.Value;
                if (emp.HireDate.HasValue) dtpHireDate.Value = emp.HireDate.Value;

                cbPosition.SelectedValue = emp.RoleId;
                _currentImagePath = emp.Image; // Lưu lại đường dẫn ảnh cũ

                // Hiển thị ảnh nếu có
                if (!string.IsNullOrEmpty(emp.Image) && System.IO.File.Exists(emp.Image))
                {
                    // Giả sử bạn có ptbAvatar trên giao diện sửa
                    // ptbAvatar.Image = Image.FromFile(emp.Image);
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validate dữ liệu cơ bản trước khi lưu
                string fullName = txtFullName.Text.Trim();
                if (string.IsNullOrEmpty(fullName))
                {
                    MessageBox.Show("Họ và tên nhân viên không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFullName.Focus();
                    return;
                }

                string phone = txtPhone.Text.Trim();
                string address = txtAddress.Text.Trim();

                // Lấy RoleId từ ComboBox chức vụ
                if (cbPosition.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn chức vụ cho nhân viên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int roleId = Convert.ToInt32(cbPosition.SelectedValue);

                // Lấy các giá trị ngày tháng
                DateTime? birthDate = dtpBirthDate.Checked ? (DateTime?)dtpBirthDate.Value : null;
                DateTime? hireDate = dtpHireDate.Checked ? (DateTime?)dtpHireDate.Value : null;

                // 2. Đóng gói toàn bộ dữ liệu vào EmployeeDTO (Sửa _currentEmployeeId thành _employeeId)
                EmployeeDTO updatedDto = new EmployeeDTO
                {
                    Id = _employeeId,
                    FullName = fullName,
                    Phone = phone,
                    Address = address,
                    BirthDate = birthDate,
                    RoleId = roleId,
                    HireDate = hireDate,
                    Image = _currentImagePath
                };

                // 3. Gọi Service/Repository để thực hiện cập nhật xuống cơ sở dữ liệu
                // (Bạn có thể gọi qua _employeeService nếu có hàm Update, hoặc dùng trực tiếp EmployeeRepository)
                EmployeeRepository empRepo = new EmployeeRepository();
                bool isSuccess = empRepo.UpdateEmployee(updatedDto);

                if (isSuccess)
                {
                    MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Kích hoạt sự kiện hoàn tất để form cha biết và load lại dữ liệu
                    SaveCompleted?.Invoke(this, EventArgs.Empty);

                    // Đóng/ẩn UserControl sửa này đi
                    this.Parent?.Controls.Remove(this);
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại. Không tìm thấy nhân viên trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi lưu thông tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelClicked?.Invoke(this, EventArgs.Empty);
        }
        private void cbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem ComboBox đã chọn một Role hợp lệ chưa (tránh lỗi khi đang set giá trị ban đầu index = -1)
            if (cbPosition.SelectedValue != null && int.TryParse(cbPosition.SelectedValue.ToString(), out int selectedRoleId))
            {
                try
                {
                    EmployeeRepository empRepo = new EmployeeRepository();

                    // Lấy thông tin lương (Amount và TimeUnit) theo RoleId vừa chọn
                    var salaryInfo = empRepo.GetSalaryInfoByRoleId(selectedRoleId);

                    // Hiển thị số tiền lên ô txtSalary (định dạng số có dấu phẩy cho dễ đọc, ví dụ: 8,000,000)
                    txtSalary.Text = salaryInfo.Amount.ToString("N0");

                    // (Tùy chọn) Nếu trên giao diện của bạn có một Label hiện đơn vị như lblTimeUnit, bạn có thể cập nhật luôn:
                    // lblTimeUnit.Text = $"VNĐ / {salaryInfo.TimeUnit}";
                }
                catch (Exception ex)
                {
                    // Xử lý ngoại lệ nếu cần thiết
                    Console.WriteLine("Lỗi load lương theo Role: " + ex.Message);
                }
            }
        }
    }
}
