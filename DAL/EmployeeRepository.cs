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

        // 1. Lấy toàn bộ nhân viên chuyển sang EmployeeDTO (có kèm thông tin Lương từ bảng Salaries)
        public List<EmployeeDTO> GetAllEmployees()
        {
            using (var db = GetContext())
            {
                var query =
                    from e in db.Employees
                    join r in db.Roles on e.RoleId equals r.Id
                    join s in db.Salaries on e.RoleId equals s.RoleId into salaries
                    from s in salaries.DefaultIfEmpty()
                    select new EmployeeDTO
                    {
                        Id = e.Id,
                        FullName = e.FullName,
                        BirthDate = e.BirthDate,
                        Phone = e.Phone,
                        Address = e.Address,
                        Position = r.RoleName,
                        RoleId = e.RoleId,
                        HireDate = e.HireDate,
                        Salary = s != null ? (decimal?)s.Amount : null,
                        Image = e.Image
                    };
                return query.ToList();
            }
        }
        // Lấy thông tin nhân viên theo Id chuyển sang EmployeeDTO (có kèm thông tin Lương từ bảng Salaries)
        public EmployeeDTO GetEmployeeDTOById(int employeeId)
        {
            using (var db = GetContext())
            {
                var query =
                    from e in db.Employees
                    where e.Id == employeeId
                    join r in db.Roles on e.RoleId equals r.Id
                    join s in db.Salaries on e.RoleId equals s.RoleId into salaries
                    from s in salaries.DefaultIfEmpty()
                    select new EmployeeDTO
                    {
                        Id = e.Id,
                        FullName = e.FullName,
                        BirthDate = e.BirthDate,
                        Phone = e.Phone,
                        Address = e.Address,
                        Position = r.RoleName,
                        RoleId = e.RoleId,
                        HireDate = e.HireDate,
                        Salary = s != null ? (decimal?)s.Amount : null,
                        Image = e.Image
                    };
                return query.FirstOrDefault();
            }
        }

        // 2. Tìm kiếm nhân viên theo Họ tên hoặc SĐT
        public List<EmployeeDTO> SearchEmployees(string keyword)
        {
            using (var db = GetContext())
            {
                var query =
                    from e in db.Employees
                    join r in db.Roles on e.RoleId equals r.Id
                    join s in db.Salaries on e.RoleId equals s.RoleId into salaries
                    from s in salaries.DefaultIfEmpty()
                    where e.FullName.Contains(keyword) || e.Phone.Contains(keyword)
                    select new EmployeeDTO
                    {
                        Id = e.Id,
                        FullName = e.FullName,
                        BirthDate = e.BirthDate,
                        Phone = e.Phone,
                        Address = e.Address,
                        Position = r.RoleName,
                        RoleId = e.RoleId,
                        HireDate = e.HireDate,
                        Salary = s != null ? (decimal?)s.Amount : null,
                        Image = e.Image
                    };
                return query.ToList();
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
                        BirthDate = dto.BirthDate,
                        Phone = string.IsNullOrEmpty(dto.Phone) ? null : dto.Phone.Trim(),
                        Address = string.IsNullOrEmpty(dto.Address) ? null : dto.Address.Trim(),
                        HireDate = dto.HireDate ?? DateTime.Now,
                        RoleId = dto.RoleId,
                        Image = dto.Image
                    };

                    db.Employees.InsertOnSubmit(newEmp);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        // 4. Sửa thông tin nhân viên (Cập nhật đầy đủ từ EmployeeDTO)
        public bool UpdateEmployee(EmployeeDTO dto)
        {
            if (dto == null) return false;

            try
            {
                using (var db = GetContext())
                {
                    var existing = db.Employees.FirstOrDefault(e => e.Id == dto.Id);
                    if (existing == null) return false;

                    existing.FullName = dto.FullName?.Trim();
                    existing.BirthDate = dto.BirthDate;
                    existing.Phone = dto.Phone?.Trim();
                    existing.Address = dto.Address?.Trim();
                    existing.RoleId = dto.RoleId;

                    if (!string.IsNullOrEmpty(dto.Image))
                    {
                        existing.Image = dto.Image;
                    }

                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
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
                                RoleId = e.RoleId,
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
        // Phương thức lấy thông tin lương theo RoleId
        public (decimal Amount, string TimeUnit) GetSalaryInfoByRoleId(int roleId)
        {
            using (var db = GetContext())
            {
                var salaryConfig = db.Salaries.FirstOrDefault(s => s.RoleId == roleId);
                if (salaryConfig != null)
                {
                    return (salaryConfig.Amount, salaryConfig.TimeUnit);
                }
                return (0, "Tháng"); // Giá trị mặc định nếu chưa cấu hình
            }
        }
    }
}