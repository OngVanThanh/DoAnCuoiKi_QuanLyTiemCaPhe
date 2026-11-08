using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
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
                _employeeId = emp.Id;
                txtFullName.Text = emp.FullName;
                txtPhone.Text = emp.Phone;
                txtAddress.Text = emp.Address;
                txtSalary.Text = emp.Salary?.ToString();
                if (emp.HireDate.HasValue) dtpHireDate.Value = emp.HireDate.Value;
                cbPosition.SelectedValue = emp.RoleId;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validate dữ liệu cơ bản trên giao diện
                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    MessageBox.Show("Họ tên nhân viên không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFullName.Focus();
                    return;
                }

                if (cbPosition.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn chức vụ cho nhân viên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Gom dữ liệu vào EmployeeDTO
                EmployeeDTO dto = new EmployeeDTO
                {
                    Id = _employeeId, // Nếu là thêm mới thì Id không quan trọng (DB tự tăng)
                    FullName = txtFullName.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    Salary = string.IsNullOrEmpty(txtSalary.Text) ? (decimal?)null : decimal.Parse(txtSalary.Text),
                    HireDate = dtpHireDate.Value,
                    RoleId = (int)cbPosition.SelectedValue
                };
                string errorMsg;
                // 3. Gọi BLL lưu dữ liệu và nhận kết quả trả về
                bool isSuccess = _employeeService.SaveEmployee(dto, out errorMsg);
                if (isSuccess)
                {
                    MessageBox.Show("Lưu thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SaveCompleted?.Invoke(this, EventArgs.Empty); // Gọi sự kiện để thông báo cho Form chính
                }
                else
                {
                    MessageBox.Show($"Lưu thông tin nhân viên thất bại: {errorMsg}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi ngoại lệ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
