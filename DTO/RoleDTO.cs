using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    public class RoleDTO
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }

        public RoleDTO() { }

        public RoleDTO(int id, string roleName, string description = "")
        {
            Id = id;
            RoleName = roleName;
            Description = description;
        }

        public override string ToString()
        {
            return RoleName;
        }
    }
}
