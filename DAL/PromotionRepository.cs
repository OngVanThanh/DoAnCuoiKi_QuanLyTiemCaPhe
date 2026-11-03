using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DAL
{
    public class PromotionRepository
    {
        private CoffeeShopDataContextDataContext GetContext()
        {
            return new CoffeeShopDataContextDataContext(ConfigHelper.GetConnectionString("CoffeeShopDB"));
        }

        // 1. Lấy toàn bộ danh sách khuyến mãi
        public List<PromotionDTO> GetAllPromotions()
        {
            using (var db = GetContext())
            {
                return db.Promotions
                    .Select(p => new PromotionDTO
                    {
                        Id = p.Id,
                        PromoCode = p.PromoCode,
                        DiscountPercent = p.DiscountPercent,
                        StartDate = p.StartDate,
                        EndDate = p.EndDate,
                        IsActive = p.IsActive
                    })
                    .ToList();
            }
        }

        // 2. Tìm mã khuyến mãi theo mã code (Dùng cho bạn Tấn Phát khi Order món áp dụng voucher)
        public Promotion GetPromotionByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return null;
            using (var db = GetContext())
            {
                return db.Promotions.FirstOrDefault(p => p.PromoCode == code && p.IsActive == true);
            }
        }

        // 3. Thêm mã khuyến mãi mới
        public bool InsertPromotion(Promotion promotion)
        {
            if (promotion == null) return false;
            using (var db = GetContext())
            {
                // Kiểm tra trùng mã code
                bool exists = db.Promotions.Any(p => p.PromoCode == promotion.PromoCode);
                if (exists) return false;

                db.Promotions.InsertOnSubmit(promotion);
                db.SubmitChanges();
                return true;
            }
        }

        // 4. Sửa mã khuyến mãi
        public bool UpdatePromotion(Promotion promotion)
        {
            if (promotion == null) return false;
            using (var db = GetContext())
            {
                var existing = db.Promotions.FirstOrDefault(p => p.Id == promotion.Id);
                if (existing == null) return false;

                existing.DiscountPercent = promotion.DiscountPercent;
                existing.StartDate = promotion.StartDate;
                existing.EndDate = promotion.EndDate;
                existing.IsActive = promotion.IsActive;

                db.SubmitChanges();
                return true;
            }
        }

        // 5. Xóa mã khuyến mãi
        public bool DeletePromotion(int promotionId)
        {
            using (var db = GetContext())
            {
                var existing = db.Promotions.FirstOrDefault(p => p.Id == promotionId);
                if (existing != null)
                {
                    db.Promotions.DeleteOnSubmit(existing);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
    }
}