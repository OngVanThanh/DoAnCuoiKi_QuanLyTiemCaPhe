using System;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    public class PromotionDTO
    {
        public int Id { get; set; }
        public string PromoCode { get; set; }
        public double DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? IsActive { get; set; }

        // Kiểm tra xem mã còn hiệu lực theo thời gian hiện tại không
        public bool IsValidNow => (IsActive == true) && (DateTime.Now >= StartDate && DateTime.Now <= EndDate);
    }
}