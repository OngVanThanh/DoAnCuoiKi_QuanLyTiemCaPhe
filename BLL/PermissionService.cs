
using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

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


        // 5. Tạo tài khoản mới
        public bool CreateUser(UserDTO dto, string password)
        {
            // 1. Kiểm tra tính hợp lệ của dữ liệu đầu vào
            if (dto == null || string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            string username = dto.Username.Trim();

            // 2. Kiểm tra xem RoleId có tồn tại không
            if (!_userRepository.RoleExists(dto.RoleId))
            {
                return false;
            }

            // 3. Kiểm tra trùng tên tài khoản
            if (_userRepository.GetByUserName(username) != null)
            {
                return false;
            }

            // 4. Tạo Salt và băm mật khẩu
            string salt = SecurityHelper.GenerateSalt();
            string passwordHash = SecurityHelper.HashPassword(password, salt);
            
            dto.PasswordHash = passwordHash;
            dto.Salt = salt;

            // 6. Gọi Repository thực hiện thêm mới
            return _userRepository.InsertUser(dto);
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

        // 9. Xóa tài khoản (có kiểm tra tự xóa và quản lý duy nhất)
        public bool DeleteUser(string username, out string message)
        {
            message = string.Empty;

            if (string.IsNullOrWhiteSpace(username))
            {
                message = "Tên đăng nhập không hợp lệ!";
                return false;
            }

            username = username.Trim();

            var targetUser = _userRepository.GetByUserName(username);
            if (targetUser == null)
            {
                message = $"Không tìm thấy tài khoản [{username}] để xóa!";
                return false;
            }

            // 1. Chặn người dùng tự xóa chính mình nếu đang đăng nhập
            if (UserSession.CurrentUser != null &&
                string.Equals(UserSession.CurrentUser.Username, username, StringComparison.OrdinalIgnoreCase))
            {
                message = "Không thể tự xóa tài khoản đang đăng nhập trong phiên làm việc hiện tại!";
                return false;
            }

            // 2. Chặn xóa tài khoản Quản lý (RoleId = 1) nếu là Quản lý duy nhất đang hoạt động
            if (targetUser.RoleId == 1)
            {
                var allUsers = _userRepository.GetAllUsersWithRoles();
                int activeManagerCount = allUsers.Count(u => u.RoleId == 1 && (u.IsActive ?? true));
                if (activeManagerCount <= 1)
                {
                    message = "Không thể xóa tài khoản Quản lý này vì đây là Quản lý đang hoạt động duy nhất trong hệ thống!";
                    return false;
                }
            }

            bool result = _userRepository.DeleteUser(username);
            if (result)
            {
                message = $"Đã xóa tài khoản [{username}] thành công!";
                return true;
            }
            else
            {
                message = "Không thể xóa tài khoản do lỗi cơ sở dữ liệu hoặc ràng buộc dữ liệu.";
                return false;
            }
        }

        public bool DeleteUser(string username)
        {
            return DeleteUser(username, out _);
        }

        // 9b. Quản lý đặt lại mật khẩu cho nhân viên không cần mật khẩu cũ
        public bool AdminResetPassword(string username, string newPassword, out string message)
        {
            message = string.Empty;

            if (string.IsNullOrWhiteSpace(username))
            {
                message = "Tên đăng nhập không được để trống!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                message = "Mật khẩu mới không được để trống!";
                return false;
            }

            if (newPassword.Length < 6)
            {
                message = "Mật khẩu mới phải có độ dài tối thiểu 6 ký tự!";
                return false;
            }

            var user = _userRepository.GetByUserName(username.Trim());
            if (user == null)
            {
                message = $"Không tìm thấy tài khoản [{username}] trong hệ thống!";
                return false;
            }

            string newSalt = SecurityHelper.GenerateSalt();
            string newHash = SecurityHelper.HashPassword(newPassword, newSalt);

            bool result = _userRepository.UpdatePassword(username.Trim(), newHash, newSalt);
            if (result)
            {
                message = $"Đặt lại mật khẩu cho tài khoản [{username}] thành công!";
                return true;
            }
            else
            {
                message = "Đã xảy ra lỗi khi cập nhật mật khẩu trong cơ sở dữ liệu.";
                return false;
            }
        }

        // 10. Vô hiệu hóa tài khoản
        public bool DeactivateUser(int userId)
        {
            if (userId <= 0)
                return false;

            return _userRepository.DeactivateUser(userId);
        }
        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            // 1. Lấy thông tin user hiện tại
            var user = _userRepository.GetByUserName(username);
            if (user == null) return false;

            // 2. Kiểm tra mật khẩu cũ có chính xác không
            string oldHash = SecurityHelper.HashPassword(oldPassword, user.Salt);
            if (oldHash != user.PasswordHash)
            {
                return false; // Mật khẩu cũ không khớp
            }

            // 3. Nếu đúng, tạo Salt và Hash cho mật khẩu mới
            string newSalt = SecurityHelper.GenerateSalt();
            string newHash = SecurityHelper.HashPassword(newPassword, newSalt);

            // 4. Gọi DAL để cập nhật
            return _userRepository.UpdatePassword(username, newHash, newSalt);
        }
        // 1. Hàm xác thực thông tin nhân viên và đổi mật khẩu mới (có mã hóa Salt/Hash)
        public bool ResetPasswordWithVerification(int employeeId, string phone, string newPassword)
        {
            if (employeeId <= 0 || string.IsNullOrWhiteSpace(phone) || string.IsNullOrEmpty(newPassword))
            {
                return false;
            }

            // Tạo Salt và Hash mới cho mật khẩu
            string newSalt = SecurityHelper.GenerateSalt();
            string newHash = SecurityHelper.HashPassword(newPassword, newSalt);

            // Gọi xuống DAL thực hiện cập nhật
            return _userRepository.ResetPasswordByManagerAndEmployee(employeeId, phone, newHash, newSalt);
        }

        // 2. Hàm kiểm tra tài khoản và mật khẩu của quản lý (Admin)
        public bool ValidateManagerCredentials(string managerUsername, string managerPassword)
        {
            if (string.IsNullOrWhiteSpace(managerUsername) || string.IsNullOrEmpty(managerPassword))
            {
                return false;
            }

            var user = _userRepository.GetByUserName(managerUsername.Trim());
            if (user == null) return false;

            // Kiểm tra mật khẩu của Quản lý dựa trên Salt của họ
            string hash = SecurityHelper.HashPassword(managerPassword, user.Salt);
            if (hash != user.PasswordHash) return false;

            // Kiểm tra quyền quản lý (Ví dụ: RoleId = 1 là Quản lý/Admin, hoặc tùy thuộc vào cấu trúc CSDL của bạn)
            return user.RoleId == 1;
        }
    }
}
