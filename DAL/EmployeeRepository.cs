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
            try
            {
                using (var db = GetContext())
                {
                    return db.Employees
                        .Select(e => new EmployeeDTO
                        {
                            Id = e.Id,
                            FullName = e.FullName,
                            BirthDate = e.BirthDate,
                            Phone = e.Phone,
                            Address = e.Address,
                            Position = e.Role != null ? e.Role.RoleName : string.Empty,
                            RoleId = e.RoleId,
                            HireDate = e.HireDate,
                            Salary = db.Salaries.Where(s => s.RoleId == e.RoleId).Select(s => (decimal?)s.Amount).FirstOrDefault(),
                            Image = e.Image
                        })
                        .ToList();
                }
            }
            catch
            {
                return new List<EmployeeDTO>
                {
                    new EmployeeDTO { Id = 1, FullName = "Nguyễn Văn Quản Lý", RoleId = 1, Position = "Quản lý", Phone = "0901234567" },
                    new EmployeeDTO { Id = 2, FullName = "Trần Thị Thu Ngân", RoleId = 2, Position = "Thu ngân", Phone = "0912345678" }
                };
            }
        }
        // Lấy thông tin nhân viên theo Id chuyển sang EmployeeDTO (có kèm thông tin Lương từ bảng Salaries)
        public EmployeeDTO GetEmployeeDTOById(int employeeId)
        {
            try
            {
                using (var db = GetContext())
                {
                    return db.Employees
                        .Where(e => e.Id == employeeId)
                        .Select(e => new EmployeeDTO
                        {
                            Id = e.Id,
                            FullName = e.FullName,
                            BirthDate = e.BirthDate,
                            Phone = e.Phone,
                            Address = e.Address,
                            Position = e.Role != null ? e.Role.RoleName : string.Empty,
                            RoleId = e.RoleId,
                            HireDate = e.HireDate,
                            Salary = db.Salaries.Where(s => s.RoleId == e.RoleId).Select(s => (decimal?)s.Amount).FirstOrDefault(),
                            Image = e.Image
                        })
                        .FirstOrDefault();
                }
            }
            catch
            {
                return null;
            }
        }

        // 2. Tìm kiếm nhân viên theo Họ tên, SĐT hoặc ID, kèm lọc theo Vai trò
        public List<EmployeeDTO> SearchEmployees(string keyword, int? roleId = null)
        {
            using (var db = GetContext())
            {
                var query = db.Employees.AsQueryable();

                if (roleId.HasValue && roleId.Value > 0)
                {
                    query = query.Where(e => e.RoleId == roleId.Value);
                }

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    string kw = keyword.Trim();
                    bool isNum = int.TryParse(kw, out int empId);

                    if (isNum)
                    {
                        query = query.Where(e => e.Id == empId || (e.Phone != null && e.Phone.Contains(kw)) || e.FullName.Contains(kw));
                    }
                    else
                    {
                        query = query.Where(e => e.FullName.Contains(kw) || (e.Phone != null && e.Phone.Contains(kw)));
                    }
                }

                return query.Select(e => new EmployeeDTO
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    BirthDate = e.BirthDate,
                    Phone = e.Phone,
                    Address = e.Address,
                    Position = e.Role != null ? e.Role.RoleName : string.Empty,
                    RoleId = e.RoleId,
                    HireDate = e.HireDate,
                    Salary = db.Salaries.Where(s => s.RoleId == e.RoleId).Select(s => (decimal?)s.Amount).FirstOrDefault(),
                    Image = e.Image
                }).ToList();
            }
        }

        public List<EmployeeDTO> SearchEmployees(string keyword)
        {
            return SearchEmployees(keyword, null);
        }

        // Lấy danh sách tất cả vai trò
        public List<RoleDTO> GetAllRoles()
        {
            using (var db = GetContext())
            {
                return db.Roles.Select(r => new RoleDTO
                {
                    Id = r.Id,
                    RoleName = r.RoleName
                }).ToList();
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

        // 5. Xóa nhân viên kèm kiểm tra ràng buộc khóa ngoại
        public bool DeleteEmployee(int employeeId, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using (var db = GetContext())
                {
                    var existing = db.Employees.FirstOrDefault(e => e.Id == employeeId);
                    if (existing == null)
                    {
                        errorMessage = "Không tìm thấy nhân viên cần xóa!";
                        return false;
                    }

                    // Kiểm tra ràng buộc dữ liệu liên kết trước khi xóa
                    bool hasOrders = db.Orders.Any(o => o.EmployeeId == employeeId);
                    bool hasShifts = db.EmployeeShifts.Any(s => s.EmployeeId == employeeId);
                    bool hasUser = db.Users.Any(u => u.EmployeeId == employeeId);

                    if (hasOrders || hasShifts || hasUser)
                    {
                        errorMessage = "Không thể xóa nhân viên đã có dữ liệu giao dịch hoặc ca làm việc liên kết!";
                        return false;
                    }

                    db.Employees.DeleteOnSubmit(existing);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (System.Data.SqlClient.SqlException)
            {
                errorMessage = "Không thể xóa nhân viên đã có dữ liệu giao dịch hoặc ca làm việc liên kết!";
                return false;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi xóa nhân viên: " + ex.Message;
                return false;
            }
        }

        public bool DeleteEmployee(int employeeId)
        {
            return DeleteEmployee(employeeId, out _);
        }
        //6. Lấy danh sách nhân viên chưa có tài khoản
        public List<EmployeeDTO> GetEmployeesWithoutAccount()
        {
            using (var db = GetContext())
            {
                var userEmpIds = db.Users.Select(u => u.EmployeeId).ToList();
                return db.Employees
                    .Where(e => !userEmpIds.Contains(e.Id))
                    .GroupJoin(
                        db.Roles,
                        e => e.RoleId,
                        r => r.Id,
                        (e, roles) => new { e, roles }
                    )
                    .SelectMany(
                        x => x.roles.DefaultIfEmpty(),
                        (x, r) => new EmployeeDTO
                        {
                            Id = x.e.Id,
                            FullName = x.e.FullName,
                            Phone = x.e.Phone,
                            RoleId = x.e.RoleId,
                            Position = r != null ? r.RoleName : "Chưa có"
                        }
                    )
                    .ToList();
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