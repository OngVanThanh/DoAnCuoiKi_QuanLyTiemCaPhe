using System;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    /// <summary>
    /// Lớp DTO chứa thông tin tổng quan của Đơn hàng / Hóa đơn thanh toán.
    /// </summary>
    public class OrderDTO
    {
        // Mã đơn hàng / hóa đơn
        public int Id { get; set; }

        // Mã bàn phục vụ
        public int TableId { get; set; }

        // Tên bàn phục vụ (dùng khi hiển thị và in hóa đơn)
        public string TableName { get; set; }

        // Mã nhân viên lập đơn / thu ngân
        public int EmployeeId { get; set; }

        // Họ tên nhân viên lập đơn / thu ngân
        public string EmployeeName { get; set; }

        // Mã khách hàng thành viên (nếu có)
        public int? CustomerId { get; set; }

        // Tên khách hàng (nếu có)
        public string CustomerName { get; set; }

        // Ngày giờ tạo hóa đơn
        public DateTime? OrderDate { get; set; }

        // Tổng tiền hàng trước giảm giá và thuế
        public decimal TotalAmount { get; set; }

        // Số tiền được giảm giá
        public decimal DiscountAmount { get; set; }

        // Phần trăm thuế VAT (lấy từ App.config)
        public decimal VATPercent { get; set; }

        // Số tiền thuế VAT
        public decimal VATAmount { get; set; }

        // Tổng tiền khách phải thanh toán cuối cùng
        public decimal FinalAmount { get; set; }

        // Trạng thái hóa đơn: "Chưa thanh toán", "Đã thanh toán"
        public string Status { get; set; }

        // Mã chương trình khuyến mãi áp dụng (nếu có)
        public int? PromotionId { get; set; }

        // Mã code khuyến mãi (ví dụ: CHAOMUNG, VIPCOFFEE)
        public string PromoCode { get; set; }

        // Tiền khách đưa khi thanh toán
        public decimal CashTendered { get; set; }

        // Tiền thối lại cho khách
        public decimal ChangeDue { get; set; }
    }
}
