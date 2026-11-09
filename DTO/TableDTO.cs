using System;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    /// <summary>
    /// Lớp DTO chứa thông tin Bàn ăn (không phụ thuộc Area).
    /// </summary>
    public class TableDTO
    {
        // Mã bàn
        public int Id { get; set; }

        // Tên bàn (ví dụ: Bàn 01, Bàn 02)
        public string TableName { get; set; }

        // Trạng thái bàn: "Trống", "Có khách", "Đặt trước"
        public string Status { get; set; }

        // Mã khu vực (mặc định 1 để tương thích cơ sở dữ liệu nếu bảng Tables có foreign key AreaId)
        public int AreaId { get; set; } = 1;

        // Tên khu vực (để trống khi không dùng Area)
        public string AreaName { get; set; } = string.Empty;
    }
}
