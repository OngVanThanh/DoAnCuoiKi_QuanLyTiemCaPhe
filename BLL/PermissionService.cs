
using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using System;
using System.Collections.Generic;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    internal class PermissionService
    {
        private readonly UserRepository _userRepository = new UserRepository();

        // 1. Lấy danh sách vai trò
        public List<Role> GetAllRoles()
        {
            return _userRepository.GetAllRoles();
        }

        // 2. Lấy danh sách người dùng kèm vai trò
        public List<UserDTO> GetAllUsers()
        {
            return _userRepository.GetAllUsersWithRoles();
        }

        // 3. Danh sách các màn hình trong hệ thống
        public List<string> GetAllSystemPermisisons()
        {
            return new List<string>
            {
                "FrmOrder - Màn hình Bán hàng & Gọi món",
                "FrmProduct - Quản lý Danh mục Thực đơn",
                "FrmTable - Quản lý Sơ đồ Bàn",
                "FrmEmployee - Quản lý Nhân sự & Luân chuyển",
                "FrmCustomer - Quản lý Khách hàng & Điểm thưởng",
                "FrmInventory - Quản lý Kho & Nguyên vật liệu",
                "FrmReport - Xem Báo cáo & Thống kê Doanh thu",
                "FrmReportWord - Kết xuất Báo cáo Word",
                "FrmPermission - Phân quyền & Quản lý Tài khoản",
                "FrmAppConfig - Cấu hình Hệ thống & CSDL"
            };
        }

        // Giữ tên hàm cũ để tương thích với Form hiện tại
        public List<string> GetAllSystemPermisison()
        {
            return GetAllSystemPermisisons();
        }

        // 4. Lấy quyền theo vai trò
        public List<string> GetPermissionsByRoleId(int roleId)
        {
            var role = _userRepository.GetRoleById(roleId);

            if (role == null)
                return new List<string>();

            if (string.Equals(
                role.RoleName, "Admin",
                StringComparison.OrdinalIgnoreCase))
            {
                return GetAllSystemPermisisons();
            }

            if (!string.IsNullOrEmpty(role.RoleName) &&
                role.RoleName.Contains("Thu ngân"))
            {
                return new List<string>
                {
                    "FrmOrder - Màn hình Bán hàng & Gọi món",
                    "FrmCustomer - Quản lý Khách hàng & Điểm thưởng",
                    "FrmReport - Xem Báo cáo & Thống kê Doanh thu"
                };
            }

            if (!string.IsNullOrEmpty(role.RoleName) &&
                role.RoleName.Contains("Pha chế"))
            {
                return new List<string>
                {
                    "FrmOrder - Màn hình Bán hàng & Gọi món",
                    "FrmInventory - Quản lý Kho & Nguyên vật liệu"
                };
            }

            return new List<string>();
        }

        // 5. Tạo tài khoản mới
        public bool CreateUser(string username, string password, int roleId, int employeeId = 1)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password)) return false;

            username = username.Trim();

            if (!_userRepository.RoleExists(roleId)) return false;

            if (_userRepository.GetByUserName(username) != null) return false;

            string salt = SecurityHelper.GenerateSalt();
            string passwordHash =
                SecurityHelper.HashPassword(password, salt);

            var newUser = new User
            {
                Username = username,
                PasswordHash = passwordHash,
                Salt = salt,
                RoleId = roleId,
                EmployeeId = employeeId,
                IsActive = true
            };

            return _userRepository.InsertUser(newUser);
        }

        // 6. Xác minh mật khẩu đăng nhập
        public bool VerifyPassword(string password, User user)
        {
            if (user == null ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(user.PasswordHash) ||
                string.IsNullOrEmpty(user.Salt))
            {
                return false;
            }

            return SecurityHelper.VerifyPassword(
                password,
                user.PasswordHash,
                user.Salt);
        }

        // 7. Cập nhật vai trò
        public bool UpdateUserRole(int userId, int newRoleId)
        {
            if (userId <= 0 || newRoleId <= 0)
                return false;

            if (!_userRepository.RoleExists(newRoleId))
                return false;

            return _userRepository.UpdateUserRole(
                userId, newRoleId);
        }

        // 8. Cập nhật tài khoản
        // newPassword = null nghĩa là giữ nguyên mật khẩu hiện tại.
        public bool UpdateUser(
            User user,
            string newPassword = null)
        {
            if (user == null ||
                user.Id <= 0 ||
                string.IsNullOrWhiteSpace(user.Username))
            {
                return false;
            }

            user.Username = user.Username.Trim();

            if (!_userRepository.RoleExists(user.RoleId))
                return false;

            if (!string.IsNullOrEmpty(newPassword))
            {
                string newSalt = SecurityHelper.GenerateSalt();

                user.PasswordHash =
                    SecurityHelper.HashPassword(newPassword, newSalt);

                user.Salt = newSalt;
            }
            else
            {
                // Yêu cầu Repository giữ nguyên hash và salt trong DB.
                user.PasswordHash = null;
                user.Salt = null;
            }

            return _userRepository.UpdateUser(user);
        }

        // 9. Xóa tài khoản
        public bool DeleteUser(int userId)
        {
            if (userId <= 0)
                return false;

            return _userRepository.DeleteUser(userId);
        }

        // 10. Vô hiệu hóa tài khoản
        public bool DeactivateUser(int userId)
        {
            if (userId <= 0)
                return false;

            return _userRepository.DeactivateUser(userId);
        }
    }
}
