using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    public class ShiftRTO 
    {
        public string Id { get; set; }
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public int ShiftId { get; set; }
        public DateTime WorkDate { get; set; }
    }
}
