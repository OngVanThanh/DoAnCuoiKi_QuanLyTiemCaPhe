using System;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    public class ShiftDTO
    {
        public string DateString { get; set; }    // Ví dụ: Thứ Hai (17/11/2026)
        public string MissionName { get; set; }   // Phục vụ / Pha chế / Thu ngân
        public string Shift1 { get; set; }        // Ca 1 (06:00 - 12:00)
        public string Shift2 { get; set; }        // Ca 2 (11:30 - 17:30)
        public string Shift3 { get; set; }        // Ca 3 (16:00 - 22:00)
    }
}