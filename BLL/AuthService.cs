using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    internal class AuthService
    {
        private PermissionService _permissionService = new PermissionService();
        private UserRepository _userRepository = new UserRepository();
        public User ValidateUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) return null;

            var user = _userRepository.GetByUserName(username.Trim());

            if (user == null || user.IsActive != true) return null;

            if (!_permissionService.VerifyPassword(password, user)) return null;

            return user;
        }
    }
}
