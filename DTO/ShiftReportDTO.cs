using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
    {
        public class ShiftReportDTO
        {
            public DateTime? WorkDate { get; set; }
            public int ShiftId { get; set; }
            public string MissionName { get; set; }
            public string EmployeeName { get; set; }
            public string Status { get; set; }
        }
    }
}
