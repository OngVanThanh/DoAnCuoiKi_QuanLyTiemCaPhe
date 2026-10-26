using System;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    /// <summary>
    /// Lớp DTO chứa thông tin Khu vực bàn trong quán cà phê.
    /// </summary>
    public class AreaDTO
    {
        // Mã định danh khu vực
        public int Id { get; set; }

        // Tên khu vực (ví dụ: Tầng 1, Sân vườn, Phòng VIP)
        public string AreaName { get; set; }

        // Mô tả chi tiết khu vực
        public string Description { get; set; }
    }
}
