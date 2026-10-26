using System;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    /// <summary>
    /// Lớp DTO chứa thông tin chi tiết từng món trong hóa đơn (dùng cho DataGridView và in hóa đơn).
    /// </summary>
    public class OrderDetailDTO
    {
        // Mã dòng chi tiết hóa đơn
        public int Id { get; set; }

        // Mã hóa đơn cha
        public int OrderId { get; set; }

        // Mã sản phẩm / đồ uống
        public int ProductId { get; set; }

        // Tên món hiển thị trên bảng chi tiết và phiếu in
        public string ProductName { get; set; }

        // Đơn vị tính (Ly, Cái...)
        public string Unit { get; set; }

        // Số lượng gọi
        public int Quantity { get; set; }

        // Đơn giá tại thời điểm gọi món
        public decimal UnitPrice { get; set; }

        // Thành tiền = Số lượng * Đơn giá
        public decimal TotalPrice { get; set; }
    }
}
