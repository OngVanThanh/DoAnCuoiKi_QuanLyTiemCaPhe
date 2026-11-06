using System;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    public class TopProductDTO
    {
        // 1. Các trường thông tin cho báo cáo Top món bán chạy
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int QuantitySold { get; set; }
        public decimal TotalAmount { get; set; }

        // 2. Constructor không tham số (Bắt buộc cho RDLC)
        public TopProductDTO()
        {
            ProductId = 0;
            ProductName = string.Empty;
            CategoryName = string.Empty;
            QuantitySold = 0;
            TotalAmount = 0m;
        }

        // 3. Constructor đầy đủ tham số
        public TopProductDTO(int productId, string productName, string categoryName, int quantitySold, decimal totalAmount)
        {
            ProductId = productId;
            ProductName = productName;
            CategoryName = categoryName;
            QuantitySold = quantitySold;
            TotalAmount = totalAmount;
        }

        public override string ToString()
        {
            return $"{ProductName} ({CategoryName}) - Đã bán: {QuantitySold} - Thành tiền: {TotalAmount:N0} đ";
        }
    }
}