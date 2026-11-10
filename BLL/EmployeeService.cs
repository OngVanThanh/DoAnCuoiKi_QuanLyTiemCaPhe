using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.BLL
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _employeeRepo = new EmployeeRepository();

        public List<EmployeeDTO> GetAllEmployees()
        {
            return _employeeRepo.GetAllEmployees();
        }

        public List<EmployeeDTO> SearchEmployees(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return GetAllEmployees();
            return _employeeRepo.SearchEmployees(keyword.Trim());
        }

        // Lưu thông tin nhân viên chung (đã bao gồm phân nhánh Thêm/Sửa)
        public bool SaveEmployee(EmployeeDTO dto, out string errorMsg)
        {
            errorMsg = string.Empty;

            if (dto == null)
            {
                errorMsg = "Dữ liệu nhân viên không hợp lệ!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(dto.FullName))
            {
                errorMsg = "Họ và tên nhân viên không được để trống!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(dto.Phone))
            {
                errorMsg = "Số điện thoại không được để trống!";
                return false;
            }

            // Kiểm tra độ dài số điện thoại (Ví dụ quy định đúng 10 chữ số)
            string phone = dto.Phone.Trim();
            if (phone.Length != 10)
            {
                errorMsg = "Số điện thoại phải có đúng 10 chữ số!";
                return false;
            }

            if (dto.RoleId <= 0)
            {
                errorMsg = "Vui lòng chọn hoặc nhập mã chức vụ (1. Quản lý, 2. Thu ngân, 3. Pha chế, 4. Phục vụ)!";
                return false;
            }

            dto.FullName = dto.FullName.Trim();
            dto.Phone = phone;
            dto.Address = dto.Address?.Trim();
            dto.HireDate = dto.HireDate ?? DateTime.Now;

            // Chuẩn hóa ảnh: Nếu dto.Image là đường dẫn tuyệt đối thì copy vào Images\Employees và chuyển thành tương đối
            if (!string.IsNullOrWhiteSpace(dto.Image) && Path.IsPathRooted(dto.Image) && File.Exists(dto.Image))
            {
                try
                {
                    string baseDir = Utilities.UIHelper.GetApplicationDirectory();
                    string destFolder = Path.Combine(baseDir, "Images", "Employees");
                    if (!Directory.Exists(destFolder))
                    {
                        Directory.CreateDirectory(destFolder);
                    }

                    string extension = Path.GetExtension(dto.Image);
                    string fileName = $"emp_{DateTime.Now.Ticks}{extension}";
                    string destFullPath = Path.Combine(destFolder, fileName);

                    File.Copy(dto.Image, destFullPath, true);

                    try
                    {
                        string currentFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images", "Employees");
                        if (!string.Equals(currentFolder, destFolder, StringComparison.OrdinalIgnoreCase))
                        {
                            if (!Directory.Exists(currentFolder)) Directory.CreateDirectory(currentFolder);
                            File.Copy(dto.Image, Path.Combine(currentFolder, fileName), true);
                        }
                    }
                    catch { }

                    dto.Image = Path.Combine("Images", "Employees", fileName);
                }
                catch
                {
                    // Fallback an toàn nếu có ngoại lệ copy
                }
            }

            if (dto.Id <= 0)
            {
                return _employeeRepo.InsertEmployee(dto);
            }
            else
            {
                return _employeeRepo.UpdateEmployee(dto);
            }
        }

        // Lưu thông tin nhân viên kèm file ảnh nguồn (chuẩn hóa đường dẫn tương đối)
        public bool SaveEmployee(EmployeeDTO dto, string sourceImagePath, out string errorMsg)
        {
            errorMsg = string.Empty;

            if (dto == null)
            {
                errorMsg = "Dữ liệu nhân viên không hợp lệ!";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(sourceImagePath) && File.Exists(sourceImagePath))
            {
                try
                {
                    string baseDir = Utilities.UIHelper.GetApplicationDirectory();
                    string destFolder = Path.Combine(baseDir, "Images", "Employees");
                    if (!Directory.Exists(destFolder))
                    {
                        Directory.CreateDirectory(destFolder);
                    }

                    string extension = Path.GetExtension(sourceImagePath);
                    string fileName = $"emp_{DateTime.Now.Ticks}{extension}";
                    string destFullPath = Path.Combine(destFolder, fileName);

                    File.Copy(sourceImagePath, destFullPath, true);

                    try
                    {
                        string currentFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images", "Employees");
                        if (!string.Equals(currentFolder, destFolder, StringComparison.OrdinalIgnoreCase))
                        {
                            if (!Directory.Exists(currentFolder)) Directory.CreateDirectory(currentFolder);
                            File.Copy(sourceImagePath, Path.Combine(currentFolder, fileName), true);
                        }
                    }
                    catch { }

                    dto.Image = Path.Combine("Images", "Employees", fileName);
                }
                catch (Exception ex)
                {
                    errorMsg = "Lỗi khi lưu ảnh nhân viên: " + ex.Message;
                    return false;
                }
            }

            return SaveEmployee(dto, out errorMsg);
        }

        public bool SaveEmployee(EmployeeDTO dto, string sourceImagePath)
        {
            return SaveEmployee(dto, sourceImagePath, out _);
        }

        // Phương thức riêng biệt để thêm mới nhân viên
        public bool AddEmployee(EmployeeDTO dto, out string errorMsg)
        {
            if (dto != null)
            {
                dto.Id = 0; // Buộc Id về 0 để chắc chắn là thêm mới
            }
            return SaveEmployee(dto, out errorMsg);
        }

        // Phương thức riêng biệt để cập nhật thông tin nhân viên
        public bool UpdateEmployee(EmployeeDTO dto, out string errorMsg)
        {
            errorMsg = string.Empty;

            if (dto == null || dto.Id <= 0)
            {
                errorMsg = "Mã nhân viên không hợp lệ để cập nhật!";
                return false;
            }

            return SaveEmployee(dto, out errorMsg);
        }

        public bool DeleteEmployee(int employeeId, out string message)
        {
            message = string.Empty;
            if (employeeId <= 0)
            {
                message = "Mã nhân viên không hợp lệ!";
                return false;
            }

            return _employeeRepo.DeleteEmployee(employeeId, out message);
        }

        public bool DeleteEmployee(int employeeId)
        {
            return DeleteEmployee(employeeId, out _);
        }
    }
}