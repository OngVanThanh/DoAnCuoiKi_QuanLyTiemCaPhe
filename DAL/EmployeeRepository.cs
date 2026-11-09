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
                var query =
                    from e in db.Employees
                    join r in db.Roles on e.RoleId equals r.Id
                    select new EmployeeDTO
                    {
                        Id = e.Id,
                        FullName = e.FullName,
                        BirthDate = e.BirthDate,
                        Phone = e.Phone,
                        Address = e.Address,
                        Position = r.RoleName,
                        RoleId = e.RoleId ?? 0,
                        Salary = e.Salary,
                        HireDate = e.HireDate,
                        Image = e.Image // Hoặc tên cột chứa đường dẫn trong bảng Employees của bạn
                    };
                return query.ToList();
            }
        }

        // 2. Tìm kiếm nhân viên theo Họ tên hoặc SĐT
        public List<EmployeeDTO> SearchEmployees(string keyword)
        {
            using (var db = GetContext())
            {
                return db.Employees
                    .Join(db.Roles,
                        e => e.RoleId,
                        r => r.Id,
                        (e, r) => new { e, r })
                    .Where(x => x.e.FullName.Contains(keyword) || x.e.Phone.Contains(keyword))
                    .Select(x => new EmployeeDTO
                    {
                        Id = x.e.Id,
                        FullName = x.e.FullName,
                        Phone = x.e.Phone,
                        Address = x.e.Address,
                        RoleId = x.e.RoleId ?? 0,
                        Position = x.r.RoleName,
                        Salary = x.e.Salary,
                        HireDate = x.e.HireDate
                    })
                    .ToList();
            }
        }

        // 3. Thêm nhân viên mới (Nhận vào EmployeeDTO)
        public bool InsertEmployee(EmployeeDTO dto)
        {
            try
            {
                using (var db = GetContext())
                {
                    var newEmp = new Employee
                    {
                        FullName = dto.FullName.Trim(),
                        Phone = string.IsNullOrEmpty(dto.Phone) ? null : dto.Phone.Trim(),
                        Address = string.IsNullOrEmpty(dto.Address) ? null : dto.Address.Trim(),
                        Salary = dto.Salary,
                        HireDate = dto.HireDate ?? DateTime.Now,
                        RoleId = dto.RoleId // Gắn khóa ngoại RoleId
                    };

                    db.Employees.InsertOnSubmit(newEmp);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần


                return false;
            }
        }

        // 4. Sửa thông tin nhân viên
        public bool UpdateEmployee(EmployeeDTO dto)
        {
            if (dto == null) return false;

            using (var db = GetContext())
            {
                // Tìm nhân viên cần sửa theo Id
                var existing = db.Employees.FirstOrDefault(e => e.Id == dto.Id);
                if (existing == null) return false;

                // Cập nhật thông tin cơ bản từ EmployeeDTO
                existing.FullName = dto.FullName;
                existing.Phone = dto.Phone;
                existing.Address = dto.Address;
                existing.Salary = dto.Salary;
                existing.HireDate = dto.HireDate;
                existing.RoleId = dto.RoleId;
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
        //6. Lấy danh sách nhân viên chưa có tài khoản
        public List<EmployeeDTO> GetEmployeesWithoutAccount()
        {
            using (var db = GetContext())
            {
                // Truy vấn lấy những nhân viên có ID không nằm trong bảng Users
                var query = from e in db.Employees
                            where !(from u in db.Users select u.EmployeeId).Contains(e.Id)
                            join r in db.Roles on e.RoleId equals r.Id into roles
                            from r in roles.DefaultIfEmpty()
                            select new EmployeeDTO
                            {
                                Id = e.Id,
                                FullName = e.FullName,
                                Phone = e.Phone,
                                Position = r != null ? r.RoleName : "Chưa có"
                            };

                return query.ToList();
            }
        }
        // Phương thức tìm nhân viên theo tên (BLL sẽ gọi hàm này)
        public Employee GetEmployeeByName(string fullName)
        {
            using (var db = GetContext())
            {
                return db.Employees.FirstOrDefault(e => e.FullName.Trim().ToLower() == fullName.Trim().ToLower());
            }
        }
    }
}