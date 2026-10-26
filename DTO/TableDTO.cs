using System;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    /// <summary>
    /// Lớp DTO chứa thông tin Bàn ăn kèm tên Khu vực để hiển thị lên giao diện.
    /// </summary>
    public class TableDTO
    {
        // Mã bàn
        public int Id { get; set; }

        // Tên bàn (ví dụ: Bàn 01, Bàn VIP 01)
        public string TableName { get; set; }

        // Mã khu vực mà bàn thuộc về
        public int AreaId { get; set; }

        // Tên khu vực để hiển thị trên lưới và sơ đồ bàn
        public string AreaName { get; set; }

        // Trạng thái bàn: "Trống", "Có khách", "Đặt trước"
        public string Status { get; set; }
    }
}
