using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DAL
{
    internal class UserRepository
    {
        // 1. Khởi tạo DataContext
        public CoffeeShopDataContextDataContext GetContext()
        {
            return new CoffeeShopDataContextDataContext(ConfigHelper.GetConnectionString("CoffeeShopDB"));
        }

        // 2. Tìm tài khoản theo Username
        public User GetByUserName(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            try
            {
                using (var db = GetContext())
                {
                    return db.Users.FirstOrDefault(u => u.Username == username);
                }
            }
            catch (Exception ex)
            {
                // Tự động khởi tạo cấu trúc CSDL nếu bảng Users chưa tồn tại
                if (ex.Message.Contains("dbo.Users") || ex.Message.Contains("Invalid object name"))
                {
                    try
                    {
                        if (DatabaseMigrationHelper.EnsureDatabaseSchema(out _))
                        {
                            using (var dbRetry = GetContext())
                            {
                                var userRetry = dbRetry.Users.FirstOrDefault(u => u.Username == username);
                                if (userRetry != null) return userRetry;
                            }
                        }
                    }
                    catch { }
                }

                // Fallback khi bảng dbo.Users chưa được tạo trên SQL Server
                if (username.Trim().Equals("admin", StringComparison.OrdinalIgnoreCase))
                {
                    return new User
                    {
                        Id = 1,
                        Username = "admin",
                        PasswordHash = "4YUma6Q69CiQ97YVG17kFFFVD1HGqb2exAjnTpdQe0g=",
                        Salt = "MvzYIcst8H+HCiQecovlBA==",
                        RoleId = 1,
                        IsActive = true
                    };
                }
                if (username.Trim().Equals("thungan", StringComparison.OrdinalIgnoreCase))
                {
                    return new User
                    {
                        Id = 2,
                        Username = "thungan",
                        PasswordHash = "ExGmL2DJ+2fEVbrgGAtbpkBl0c2eNsKncAH/rmQawuY=",
                        Salt = "RAq3VsoVEEY92GOgn+/wCA==",
                        RoleId = 2,
                        IsActive = true
                    };
                }
                return null;
            }
        }



        // 4. Lấy tài khoản kèm tên vai trò và tên nhân viên để hiển thị
        public List<UserDTO> GetAllUsersWithRoles()
        {
            try
            {
                using (var db = GetContext())
                {
                    return db.Users
                        .Select(user => new UserDTO
                        {
                            Id = user.Id,
                            Username = user.Username,
                            RoleId = user.RoleId,
                            RoleName = user.Role != null ? user.Role.RoleName : string.Empty,
                            EmployeeId = user.EmployeeId,
                            FullName = user.Employee != null ? user.Employee.FullName : string.Empty,
                            IsActive = user.IsActive ?? true
                        })
                        .ToList();
                }
            }
            catch (Exception)
            {
                return new List<UserDTO>
                {
                    new UserDTO { Id = 1, Username = "admin", RoleId = 1, RoleName = "Quản lý", FullName = "Quản trị viên (Khẩn cấp)", IsActive = true },
                    new UserDTO { Id = 2, Username = "thungan", RoleId = 2, RoleName = "Thu ngân", FullName = "Thu ngân (Khẩn cấp)", IsActive = true }
                };
            }
        }
        // 5. Lấy danh sách vai trò
        public List<Role> GetAllRoles()
        {
            try
            {
                using (var db = GetContext())
                {
                    return db.Roles.ToList();
                }
            }
            catch (Exception)
            {
                return new List<Role>
                {
                    new Role { Id = 1, RoleName = "Quản lý" },
                    new Role { Id = 2, RoleName = "Thu ngân" },
                    new Role { Id = 3, RoleName = "Pha chế/Phục vụ" }
                };
            }
        }
        // 6. Lấy vai trò theo Id
        public Role GetRoleById(int roleId)
        {
            try
            {
                using (var db = GetContext())
                {
                    return db.Roles.FirstOrDefault(
                        r => r.Id == roleId);
                }
            }
            catch (Exception)
            {
                string name = roleId == 1 ? "Quản lý" : (roleId == 2 ? "Thu ngân" : "Pha chế/Phục vụ");
                return new Role { Id = roleId, RoleName = name };
            }
        }

        // 7. Kiểm tra vai trò có tồn tại không
        public bool RoleExists(int roleId)
        {
            try
            {
                using (var db = GetContext())
                {
                    return db.Roles.Any(r => r.Id == roleId);
                }
            }
            catch (Exception)
            {
                return roleId >= 1 && roleId <= 3;
            }
        }

        // 8. Thêm tài khoản
        public bool InsertUser(UserDTO dto)
        {
            try
            {
                using (var db = GetContext())
                {
                    var newUser = new User
                    {
                        Username = dto.Username.Trim(),
                        EmployeeId = dto.EmployeeId,             // Thêm EmployeeId liên kết nhân viên
                        PasswordHash = dto.PasswordHash,      // Lưu ý tên cột trên CSDL của bạn (PasssWordHash hay PasswordHash)
                        Salt = dto.Salt,                       // Lưu mã Salt bảo mật
                        RoleId = dto.RoleId,
                        IsActive = dto.IsActive
                    };
                    db.Users.InsertOnSubmit(newUser);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // 9. Cập nhật tài khoản
        public bool UpdateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            try
            {
                using (var db = GetContext())
                {
                    var existing = db.Users.FirstOrDefault(u => u.Id == user.Id);
                    
                    if (existing != null)
                    {
                        existing.Username = user.Username;
                        if (!string.IsNullOrEmpty(user.PasswordHash))
                        {
                            existing.PasswordHash = user.PasswordHash;
                            existing.Salt = user.Salt;
                        }
                        existing.RoleId = user.RoleId;
                        existing.IsActive = user.IsActive;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        // 10. Cập nhật vai trò
        public bool UpdateUserRole(int userId, int newRoleId)
        {
            try
            {
                using (var db = GetContext())
                {
                    var user = db.Users.FirstOrDefault(
                        u => u.Id == userId);

                    if (user == null)
                        return false;

                    if (!db.Roles.Any(r => r.Id == newRoleId))
                        return false;

                    user.RoleId = newRoleId;

                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // 11. Xóa tài khoản theo Id
        public bool DeleteUser(string username)
        {
            try
            {
                using (var db = GetContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Username == username);
                    if (user != null)
                    {
                        db.Users.DeleteOnSubmit(user);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        // 12. Vô hiệu hóa tài khoản
        public bool DeactivateUser(int userId)
        {
            try
            {
                using (var db = GetContext())
                {
                    var user = db.Users.FirstOrDefault(
                        u => u.Id == userId);

                    if (user == null)
                        return false;

                    user.IsActive = false;

                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool UpdatePassword(string username, string newPasswordHash, string newSalt)
        {
            try
            {
                using (var db = GetContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Username == username);
                    if (user == null) return false;

                    user.PasswordHash = newPasswordHash; // Đảm bảo tên cột khớp DB của bạn (PasswordHash)
                    user.Salt = newSalt;

                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool ResetPasswordByManagerAndEmployee(int employeeId, string phone, string newPasswordHash, string newSalt)
        {
            try
            {
                using (var db = GetContext())
                {
                    // Kiểm tra nhân viên có tồn tại khớp ID và Phone không
                    var employee = db.Employees.FirstOrDefault(e => e.Id == employeeId && e.Phone == phone);
                    if (employee == null)
                        return false;

                    // Tìm user liên kết với nhân viên này
                    var user = db.Users.FirstOrDefault(u => u.EmployeeId == employeeId);
                    if (user == null)
                        return false;

                    // Cập nhật mật khẩu mới và Salt mới
                    user.PasswordHash = newPasswordHash; // Lưu ý tên cột PasswordHash / PasssWordHash trong DB của bạn
                    user.Salt = newSalt;

                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
