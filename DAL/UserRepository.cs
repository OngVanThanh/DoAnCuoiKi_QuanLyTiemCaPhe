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
            using (var db = GetContext())
            {
                return db.Users.FirstOrDefault(u => u.Username == username);
            }
        }



        // 4. Lấy tài khoản kèm tên vai trò và tên nhân viên để hiển thị
        public List<UserDTO> GetAllUsersWithRoles()
        {
            using (var db = GetContext())
            {
                var query =
                    from user in db.Users
                    join role in db.Roles
                        on user.RoleId equals role.Id
                    join emp in db.Employees
                        on user.EmployeeId equals emp.Id into empGroup
                    from employee in empGroup.DefaultIfEmpty()
                    select new UserDTO
                    {
                        Id = user.Id,
                        Username = user.Username,
                        RoleId = user.RoleId,
                        RoleName = role.RoleName,
                        EmployeeId = user.EmployeeId,
                        FullName = employee.FullName,
                        IsActive = user.IsActive ?? true
                    };

                return query.ToList();
            }
        }
        // 5. Lấy danh sách vai trò
        public List<Role> GetAllRoles()
        {
            using (var db = GetContext())
            {
                return db.Roles.ToList();
            }
        }
        // 6. Lấy vai trò theo Id
        public Role GetRoleById(int roleId)
        {
            using (var db = GetContext())
            {
                return db.Roles.FirstOrDefault(
                    r => r.Id == roleId);
            }
        }

        // 7. Kiểm tra vai trò có tồn tại không
        public bool RoleExists(int roleId)
        {
            using (var db = GetContext())
            {
                return db.Roles.Any(r => r.Id == roleId);
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
        // 10. Cập nhật vai trò
        public bool UpdateUserRole(int userId, int newRoleId)
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

        // 11. Xóa tài khoản theo Id
        public bool DeleteUser(string username)
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

        // 12. Vô hiệu hóa tài khoản
        public bool DeactivateUser(int userId)
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
