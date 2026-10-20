using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    internal class PermissionService
    {
        private CoffeeShopDataContextDataContext GetContext()
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["CoffeeShopDB"]?.ConnectionString;
            return !string.IsNullOrEmpty(connString) ? new CoffeeShopDataContextDataContext(connString) : new CoffeeShopDataContextDataContext();
        }
        
        // 1. Lấy danh sách các vai trò (Roles) đỗ vào combobox (cboRoles)
        public List<Role> GetAllRoles()
        {
            using (var db = GetContext())
            {
                return db.Roles.ToList();
            }
        }

        // 2. Lấy danh sách tài khoản người dùng kèm thông tin vai trò hiển thị trên DataGridView (dgvUsers)
        public object GetAllUsers()
        {
            using (var db = GetContext())
            {
                return (from u in db.Users
                        join r in db.Roles on u.RoleId equals r.Id
                        select new
                        {
                            u.Id,
                            u.Username,
                            RoleName = r.RoleName,
                            u.IsActive
                        }).ToList();
            }
        }

        // 3. Tạo tài khoản người dùng mới
/*        public bool CreateUser(string username, string password, int roleId, int employeeId)
        {
            using (var db = GetContext())
            {
                if (db.Users.Any(u => u.Username == username))
                {
                    return false; // Username đã tồn tại
                }

                string hashedPassword = AuthService.HashPassword(password);
                User newUser = new User
                {
                    Username = username,
                    PasswordHash = hashedPassword,
                    RoleId = roleId,
                    EmployeeId = employeeId,
                    IsActive = true
                };
                db.Users.InsertOnSubmit(newUser);
                db.SubmitChanges();
                return true;
            }
        }*/
        // 4. Cập nhật phân quyền / vai trò cho tài khoản
        public bool UpdateUserRole(int userId, int newRoleId)
        {
            using (var db = GetContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == userId);
                if (user != null)
                {
                    user.RoleId = newRoleId;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
