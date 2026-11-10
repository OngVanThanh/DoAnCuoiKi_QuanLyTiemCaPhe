using System;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    /// <summary>
    /// Lớp DTO chứa thông tin Bàn ăn kèm tên Khu vực và khung giờ Đặt trước.
    /// </summary>
    public class TableDTO
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public string Status { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}