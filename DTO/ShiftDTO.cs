using System;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    public class ShiftDTO
    {
        public string Id { get; set; }
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string DateString { get; set; }    // Ví dụ: Thứ Hai (17/11/2026)
        public string MissionName { get; set; }   // Phục vụ / Pha chế / Thu ngân
        public string Shift1 { get; set; }        // Ca 1 (06:00 - 12:00)
        public string Shift2 { get; set; }        // Ca 2 (11:30 - 17:30)
        public string Shift3 { get; set; }        // Ca 3 (16:00 - 22:00)
    }

    public class DailyShiftDTO
    {
        public int ShiftId { get; set; }          // 1, 2, 3
        public string ShiftName { get; set; }     // Ca 1 (06:00 - 12:00)
        public int MissionId { get; set; }        // 1, 2, 3
        public string MissionName { get; set; }   // Phục vụ, Pha chế, Thu ngân
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }  // Họ tên nhân viên
        public string Status { get; set; }        // Đã phân công / Chưa phân công
        public DateTime WorkDate { get; set; }    // Ngày làm việc
    }
}
