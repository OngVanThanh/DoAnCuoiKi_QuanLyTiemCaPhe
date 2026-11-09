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

            if (dto.Id <= 0)
            {
                return _employeeRepo.InsertEmployee(dto);
            }
            else
            {
                return _employeeRepo.UpdateEmployee(dto);
            }
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

        public bool DeleteEmployee(int employeeId)
        {
            return _employeeRepo.DeleteEmployee(employeeId);
        }
    }
}