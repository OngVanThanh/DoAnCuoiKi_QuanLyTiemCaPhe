using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        private EmployeeRepository _employeeRepository = new EmployeeRepository();
        private int _employeeId = -1;
        private string _currentImagePath = string.Empty;
        public UCEmployeeEdit()
        {
            InitializeComponent();
            LoadRolesToComboBox();
        }
        private void LoadRolesToComboBox()
        {
            try
            {
                var roles = _permissionService.GetAllRoles();
                cbPosition.DataSource = roles;
                cbPosition.DisplayMember = "RoleName";
                cbPosition.ValueMember = "Id";
                cbPosition.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi tải danh sách chức vụ: " + ex.Message);
            }
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

                // Hiển thị ảnh đại diện thông qua UIHelper (đường dẫn tương đối, non-locking)
                UIHelper.LoadImageToPictureBox(ptbAvatar, emp.Image);
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
                bool isSuccess = _employeeService.UpdateEmployee(updatedDto, out string errorMsg);

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
                    // Lấy thông tin lương (Amount và TimeUnit) từ bảng Roles qua Service
                    var salaryInfo = _employeeRepository.GetSalaryInfoByRoleId(selectedRoleId);

                    // Gán chuỗi định dạng đầy đủ vào txtSalary
                    if (txtSalary != null)
                    {
                        txtSalary.Text = $"{salaryInfo.Amount:N0} VNĐ / {salaryInfo.TimeUnit}";
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý ngoại lệ nếu cần thiết
                    Console.WriteLine("Lỗi load lương theo Role: " + ex.Message);
                }
            }
        }
        private void btnAddAvatar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Chọn ảnh đại diện nhân viên";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // 1. Chuẩn bị thư mục đích Images\Employees
                        string baseDir = UIHelper.GetApplicationDirectory();
                        string destFolder = Path.Combine(baseDir, "Images", "Employees");
                        if (!Directory.Exists(destFolder))
                        {
                            Directory.CreateDirectory(destFolder);
                        }

                        // 2. Tạo tên file định danh duy nhất kèm ticks thời gian
                        string extension = Path.GetExtension(ofd.FileName);
                        string fileName = $"emp_{DateTime.Now.Ticks}{extension}";
                        string destFullPath = Path.Combine(destFolder, fileName);

                        // 3. Sao chép file được chọn vào thư mục dự án
                        File.Copy(ofd.FileName, destFullPath, true);

                        try
                        {
                            string currentFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images", "Employees");
                            if (!string.Equals(currentFolder, destFolder, StringComparison.OrdinalIgnoreCase))
                            {
                                if (!Directory.Exists(currentFolder)) Directory.CreateDirectory(currentFolder);
                                File.Copy(ofd.FileName, Path.Combine(currentFolder, fileName), true);
                            }
                        }
                        catch { }

                        // 4. Lưu ĐƯỜNG DẪN TƯƠNG ĐỐI vào biến toàn cục (chuẩn kiến trúc không dùng đường dẫn tuyệt đối)
                        _currentImagePath = Path.Combine("Images", "Employees", fileName);

                        // 5. Hiển thị ảnh lên PictureBox thông qua UIHelper (MemoryStream, non-locking)
                        UIHelper.LoadImageToPictureBox(ptbAvatar, _currentImagePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi tải ảnh đại diện: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
