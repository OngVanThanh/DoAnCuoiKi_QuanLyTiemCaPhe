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

        // 3. Lấy toàn bộ tài khoản
        public List<User> GetAllUsers()
        {
            using (var db = GetContext())
            {
                return db.Users.ToList();
            }
        }

        // 4. Lấy tài khoản kèm tên vai trò để hiển thị
        public List<UserDTO> GetAllUsersWithRoles()
        {
            using (var db = GetContext())
            {
                var query =
                    from user in db.Users
                    join role in db.Roles
                        on user.RoleId equals role.Id
                    select new UserDTO
                    {
                        Id = user.Id,
                        Username = user.Username,
                        RoleName = role.RoleName,
                        IsActive = user.IsActive
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
        public bool InsertUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            using (var db = GetContext())
            {
                db.Users.InsertOnSubmit(user);
                db.SubmitChanges();
                return true;
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
        public bool DeleteUser(int userId)
        {
            using (var db = GetContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == userId);
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
    }
}
