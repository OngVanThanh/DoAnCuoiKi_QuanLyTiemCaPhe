using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    internal class ShiftRTO
    {
        public string Id { get; set; }              // Mã ca làm (nếu có trong bảng EmployeeShifts)
        public int EmployeeId { get; set; }      // Mã nhân viên
        public string FullName { get; set; }     // Họ tên nhân viên (tiện cho việc hiển thị lên UI)
        public int ShiftId { get; set; }         // Ca làm số mấy (1: Sáng, 2: Chiều, 3: Tối)
        public DateTime WorkDate { get; set; }   // Ngày làm việc
    }
}
