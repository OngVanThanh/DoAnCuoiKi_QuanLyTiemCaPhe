using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    internal class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int EmployeeID { get; set; }
        public bool? IsActive { get; set; }
    }
}
