using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using System;
using System.Collections.Generic;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.BLL
{
    public class PromotionService
    {
        private readonly PromotionRepository _promotionRepo = new PromotionRepository();

        public List<PromotionDTO> GetAllPromotions()
        {
            return _promotionRepo.GetAllPromotions();
        }

        // Lưu khuyến mãi kèm kiểm tra ràng buộc thời gian và % giảm
        public bool SavePromotion(PromotionDTO dto, out string errorMsg)
        {
            errorMsg = string.Empty;

            if (dto == null)
            {
                errorMsg = "Dữ liệu khuyến mãi không hợp lệ!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(dto.PromoCode))
            {
                errorMsg = "Mã khuyến mãi không được để trống!";
                return false;
            }

            // Kiểm tra tỷ lệ giảm giá từ 1% đến 100%
            if (dto.DiscountPercent <= 0 || dto.DiscountPercent > 100)
            {
                errorMsg = "Phần trăm giảm giá phải nằm trong khoảng từ 1% đến 100%!";
                return false;
            }

            // Kiểm tra ngày kết thúc phải >= ngày bắt đầu
            if (dto.EndDate < dto.StartDate)
            {
                errorMsg = "Ngày kết thúc khuyến mãi phải lớn hơn hoặc bằng ngày bắt đầu!";
                return false;
            }

            var promotion = new Promotion
            {
                Id = dto.Id,
                PromoCode = dto.PromoCode.Trim().ToUpper(), // Tự động viết hoa mã code
                DiscountPercent = dto.DiscountPercent,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsActive = dto.IsActive ?? true
            };

            if (dto.Id == 0)
            {
                bool success = _promotionRepo.InsertPromotion(promotion);
                if (!success)
                {
                    errorMsg = "Mã khuyến mãi này đã tồn tại trong hệ thống!";
                }
                return success;
            }
            else
            {
                return _promotionRepo.UpdatePromotion(promotion);
            }
        }

        public bool DeletePromotion(int promotionId, out string errorMsg)
        {
            errorMsg = string.Empty;
            return _promotionRepo.DeletePromotion(promotionId);
        }
    }
}