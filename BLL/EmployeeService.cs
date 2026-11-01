using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using System.Collections.Generic;

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

        // Lưu thông tin nhân viên kèm kiểm tra ràng buộc nghiệp vụ
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

            // Kiểm tra độ dài số điện thoại (từ 9 đến 12 ký tự)
            string phone = dto.Phone.Trim();
            if (phone.Length < 9 || phone.Length > 12)
            {
                errorMsg = "Số điện thoại không đúng định dạng (từ 9 đến 12 chữ số)!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(dto.Position))
            {
                errorMsg = "Vui lòng chọn hoặc nhập chức vụ (Thu ngân, Pha chế, Phục vụ, Quản lý)!";
                return false;
            }

            if (dto.Salary.HasValue && dto.Salary.Value < 0)
            {
                errorMsg = "Mức lương cơ bản không được âm!";
                return false;
            }

            var employee = new Employee
            {
                Id = dto.Id,
                FullName = dto.FullName.Trim(),
                Phone = phone,
                Address = dto.Address?.Trim(),
                Position = dto.Position.Trim(),
                Salary = dto.Salary,
                HireDate = dto.HireDate
            };

            if (dto.Id == 0)
            {
                return _employeeRepo.InsertEmployee(employee);
            }
            else
            {
                return _employeeRepo.UpdateEmployee(employee);
            }
        }

        public bool DeleteEmployee(int employeeId, out string errorMsg)
        {
            errorMsg = string.Empty;
            return _employeeRepo.DeleteEmployee(employeeId);
        }
    }
}