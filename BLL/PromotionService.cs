using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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

        // Lấy tỷ lệ giảm giá trực tiếp theo hạng thành viên (Bạc 2%, Vàng 3%, Kim Cương 5%)
        public decimal GetTierDiscountPercent(string membershipTier)
        {
            if (string.IsNullOrWhiteSpace(membershipTier)) return 0m;

            if (membershipTier.IndexOf("Kim Cương", StringComparison.OrdinalIgnoreCase) >= 0)
                return 5m;
            if (membershipTier.IndexOf("Vàng", StringComparison.OrdinalIgnoreCase) >= 0)
                return 3m;
            if (membershipTier.IndexOf("Bạc", StringComparison.OrdinalIgnoreCase) >= 0)
                return 2m;

            return 0m;
        }

        // Lấy danh sách các lựa chọn khuyến mãi (bao gồm ưu đãi VIP và quy đổi điểm tích lũy)
        public List<PromotionItem> GetPromotionOptions(CustomerDTO customer, out int defaultSelectedIndex, List<PromotionDTO> cachedPromotions = null)
        {
            var result = new List<PromotionItem>();
            defaultSelectedIndex = 0;

            // 1. Mặc định: Không giảm giá
            result.Add(new PromotionItem
            {
                DisplayText = "-- Không áp dụng khuyến mãi --",
                DiscountPercent = 0m,
                FixedDiscountAmount = 0m,
                PointsToRedeem = 0,
                PromoCode = ""
            });

            // 2. Nếu đã có khách hàng thành viên
            if (customer != null)
            {
                string tier = customer.MembershipTier ?? "Thường";
                decimal vipBonus = GetTierDiscountPercent(tier);
                bool hasTierPrivilege = vipBonus > 0m;

                // 2a. Nếu là thành viên có ưu đãi (Bạc 2%, Vàng 3%, Kim Cương 5%) -> Ưu đãi giảm theo hạng
                if (hasTierPrivilege)
                {
                    result.Add(new PromotionItem
                    {
                        DisplayText = $"⭐ Ưu đãi hạng {tier} (-{vipBonus:0.##}%)",
                        DiscountPercent = vipBonus,
                        FixedDiscountAmount = 0m,
                        PointsToRedeem = 0,
                        PromoCode = $"VIP-{tier.ToUpper()}",
                        IsMemberPrivilege = true
                    });

                    // Tự động chọn ưu đãi hạng thành viên làm mặc định cho khách VIP
                    defaultSelectedIndex = 1;
                }

                // 2b. Quy đổi điểm tích lũy: 1.000 điểm = 10% discount (thành viên Bạc/Vàng/Kim Cương được cộng thêm % hạng)
                int pts = customer.RewardPoints;

                if (pts >= 1000)
                {
                    decimal d1000 = 10m + vipBonus;
                    string label1000 = hasTierPrivilege
                        ? $"🎁 Đổi 1.000 điểm (-10% + {vipBonus:0.##}% {tier} = -{d1000:0.##}%)"
                        : "🎁 Đổi 1.000 điểm (-10%)";
                    result.Add(new PromotionItem
                    {
                        DisplayText = label1000,
                        DiscountPercent = d1000,
                        PointsToRedeem = 1000,
                        PromoCode = hasTierPrivilege ? $"REDEEM1000+{tier}" : "REDEEM1000"
                    });
                }

                if (pts >= 2000)
                {
                    decimal d2000 = 20m + vipBonus;
                    string label2000 = hasTierPrivilege
                        ? $"🎁 Đổi 2.000 điểm (-20% + {vipBonus:0.##}% {tier} = -{d2000:0.##}%)"
                        : "🎁 Đổi 2.000 điểm (-20%)";
                    result.Add(new PromotionItem
                    {
                        DisplayText = label2000,
                        DiscountPercent = d2000,
                        PointsToRedeem = 2000,
                        PromoCode = hasTierPrivilege ? $"REDEEM2000+{tier}" : "REDEEM2000"
                    });
                }

                if (pts >= 3000)
                {
                    decimal d3000 = 30m + vipBonus;
                    string label3000 = hasTierPrivilege
                        ? $"🎁 Đổi 3.000 điểm (-30% + {vipBonus:0.##}% {tier} = -{d3000:0.##}%)"
                        : "🎁 Đổi 3.000 điểm (-30%)";
                    result.Add(new PromotionItem
                    {
                        DisplayText = label3000,
                        DiscountPercent = d3000,
                        PointsToRedeem = 3000,
                        PromoCode = hasTierPrivilege ? $"REDEEM3000+{tier}" : "REDEEM3000"
                    });
                }

                if (pts >= 500 && pts < 1000)
                {
                    decimal d500 = 5m + vipBonus;
                    string label500 = hasTierPrivilege
                        ? $"🎁 Đổi 500 điểm (-5% + {vipBonus:0.##}% {tier} = -{d500:0.##}%)"
                        : "🎁 Đổi 500 điểm (-5%)";
                    result.Add(new PromotionItem
                    {
                        DisplayText = label500,
                        DiscountPercent = d500,
                        PointsToRedeem = 500,
                        PromoCode = hasTierPrivilege ? $"REDEEM500+{tier}" : "REDEEM500"
                    });
                }
            }

            // 3. Nạp danh sách các mã khuyến mãi đang hoạt động của quán
            List<PromotionDTO> activePromos = cachedPromotions;
            if (activePromos == null)
            {
                try
                {
                    activePromos = (GetAllPromotions() ?? new List<PromotionDTO>())
                        .Where(p => p.IsValidNow)
                        .ToList();
                }
                catch
                {
                    activePromos = new List<PromotionDTO>();
                }
            }

            if (activePromos != null && activePromos.Count > 0)
            {
                string tier = customer != null ? (customer.MembershipTier ?? "Thường") : "Thường";
                decimal vipBonus = GetTierDiscountPercent(tier);
                bool hasTierPrivilege = vipBonus > 0m;

                foreach (var promo in activePromos)
                {
                    decimal totalDiscount = (decimal)promo.DiscountPercent + vipBonus;
                    string note = hasTierPrivilege
                        ? $" (-{promo.DiscountPercent}% + {vipBonus:0.##}% {tier} = -{totalDiscount:0.##}%)"
                        : $" (-{promo.DiscountPercent}%)";

                    result.Add(new PromotionItem
                    {
                        DisplayText = $"🎟️ Mã {promo.PromoCode}{note}",
                        DiscountPercent = totalDiscount,
                        PointsToRedeem = 0,
                        PromotionId = promo.Id,
                        PromoCode = promo.PromoCode
                    });
                }
            }

            return result;
        }

        // Tải trực tiếp danh sách khuyến mãi vào ComboBox
        public void LoadPromotionComboBox(ComboBox cboPromotion, CustomerDTO selectedCustomer, List<PromotionDTO> cachedPromotions = null)
        {
            if (cboPromotion == null) return;

            try
            {
                cboPromotion.BeginUpdate();
                cboPromotion.Items.Clear();

                var items = GetPromotionOptions(selectedCustomer, out int defaultSelectedIndex, cachedPromotions);
                foreach (var item in items)
                {
                    cboPromotion.Items.Add(item);
                }

                if (cboPromotion.Items.Count > defaultSelectedIndex)
                {
                    cboPromotion.SelectedIndex = defaultSelectedIndex;
                }
                else if (cboPromotion.Items.Count > 0)
                {
                    cboPromotion.SelectedIndex = 0;
                }
            }
            finally
            {
                cboPromotion.EndUpdate();
            }
        }
    }
}