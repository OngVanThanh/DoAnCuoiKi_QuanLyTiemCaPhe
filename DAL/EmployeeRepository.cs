using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DAL
{
    public class EmployeeRepository
    {
        private CoffeeShopDataContextDataContext GetContext()
        {
            return new CoffeeShopDataContextDataContext(ConfigHelper.GetConnectionString("CoffeeShopDB"));
        }

        // 1. Lấy toàn bộ nhân viên chuyển sang EmployeeDTO
        public List<EmployeeDTO> GetAllEmployees()
        {
            using (var db = GetContext())
            {
                return db.Employees
                    .Select(e => new EmployeeDTO
                    {
                        Id = e.Id,
                        FullName = e.FullName,
                        Phone = e.Phone,
                        Address = e.Address,
                        Position = e.Position,
                        Salary = e.Salary,
                        HireDate = e.HireDate
                    })
                    .ToList();
            }
        }

        // 2. Tìm kiếm nhân viên theo Họ tên hoặc SĐT
        public List<EmployeeDTO> SearchEmployees(string keyword)
        {
            using (var db = GetContext())
            {
                return db.Employees
                    .Where(e => e.FullName.Contains(keyword) || e.Phone.Contains(keyword))
                    .Select(e => new EmployeeDTO
                    {
                        Id = e.Id,
                        FullName = e.FullName,
                        Phone = e.Phone,
                        Address = e.Address,
                        Position = e.Position,
                        Salary = e.Salary,
                        HireDate = e.HireDate
                    })
                    .ToList();
            }
        }

        // 3. Thêm nhân viên mới
        public bool InsertEmployee(Employee employee)
        {
            if (employee == null) return false;
            using (var db = GetContext())
            {
                db.Employees.InsertOnSubmit(employee);
                db.SubmitChanges();
                return true;
            }
        }

        // 4. Sửa thông tin nhân viên
        public bool UpdateEmployee(Employee employee)
        {
            if (employee == null) return false;
            using (var db = GetContext())
            {
                var existing = db.Employees.FirstOrDefault(e => e.Id == employee.Id);
                if (existing == null) return false;

                existing.FullName = employee.FullName;
                existing.Phone = employee.Phone;
                existing.Address = employee.Address;
                existing.Position = employee.Position;
                existing.Salary = employee.Salary;
                existing.HireDate = employee.HireDate;

                db.SubmitChanges();
                return true;
            }
        }

        // 5. Xóa nhân viên
        public bool DeleteEmployee(int employeeId)
        {
            using (var db = GetContext())
            {
                var existing = db.Employees.FirstOrDefault(e => e.Id == employeeId);
                if (existing != null)
                {
                    db.Employees.DeleteOnSubmit(existing);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
    }
}