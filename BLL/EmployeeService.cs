using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using System;
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
            if (phone.Length != 10)
            {
                errorMsg = "Số điện thoại không đúng định dạng (từ 9 đến 12 chữ số)!";
                return false;
            }

            if (dto.RoleId <= 0)
            {
                errorMsg = "Vui lòng chọn hoặc nhập mã chức vụ (1. Quản lý, 2.Thu ngân, 3. Pha chế, 4. Phục vụ)!";
                return false;
            }
            dto.FullName = dto.FullName.Trim();
            dto.Phone = phone;
            dto.Address = dto.Address?.Trim();
            dto.HireDate = dto.HireDate ?? DateTime.Now;

            if (dto.Id <= 0)
            {
                return _employeeRepo.InsertEmployee(dto);
            }
            else
            {
                return _employeeRepo.UpdateEmployee(dto);
            }
        }

        public bool DeleteEmployee(int employeeId)
        {
            return _employeeRepo.DeleteEmployee(employeeId);
        }
    }
}