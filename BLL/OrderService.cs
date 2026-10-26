using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.BLL
{
    /// <summary>
    /// Lớp xử lý nghiệp vụ (BLL) cho màn hình Gọi món (Order), Thanh toán và Chuyển/Gộp bàn.
    /// </summary>
    internal class OrderService
    {
        private readonly OrderRepository _orderRepository = new OrderRepository();
        private readonly TableRepository _tableRepository = new TableRepository();

        // 1. Lấy danh sách nhóm món phục vụ lọc thực đơn
        public List<Category> GetAllCategories()
        {
            return _orderRepository.GetAllCategories();
        }

        // 2. Tìm kiếm và lọc danh sách món đang kinh doanh
        public List<Product> SearchProducts(int? categoryId, string keyword)
        {
            return _orderRepository.GetActiveProducts(categoryId, keyword);
        }

        // 3. Lấy hóa đơn chưa thanh toán hiện tại của bàn
        public Order GetUnpaidOrderByTable(int tableId)
        {
            if (tableId <= 0)
                return null;

            return _orderRepository.GetUnpaidOrderByTableId(tableId);
        }

        // 4. Lấy danh sách chi tiết các món trong hóa đơn của bàn đang chọn
        public List<OrderDetailDTO> GetOrderDetailsByTable(int tableId)
        {
            var unpaidOrder = GetUnpaidOrderByTable(tableId);
            if (unpaidOrder == null)
            {
                return new List<OrderDetailDTO>();
            }

            return _orderRepository.GetOrderDetailsByOrderId(unpaidOrder.Id);
        }

        // 5. Thêm món vào hóa đơn của bàn (nếu bàn chưa có hóa đơn sẽ tự động mở hóa đơn mới)
        public bool AddProductToTableOrder(int tableId, int employeeId, int productId, int quantity, out string message)
        {
            if (tableId <= 0)
            {
                message = "Vui lòng chọn bàn trước khi thêm món!";
                return false;
            }

            if (productId <= 0)
            {
                message = "Vui lòng chọn một món trong thực đơn!";
                return false;
            }

            if (quantity <= 0)
            {
                message = "Số lượng món phải lớn hơn 0!";
                return false;
            }

            var table = _tableRepository.GetTableById(tableId);
            if (table == null)
            {
                message = "Bàn được chọn không tồn tại!";
                return false;
            }

            // Kiểm tra bàn đã có đơn chưa thanh toán chưa, nếu chưa thì tạo mới
            var currentOrder = _orderRepository.GetUnpaidOrderByTableId(tableId);
            int orderId = currentOrder != null
                ? currentOrder.Id
                : _orderRepository.CreateOrder(tableId, employeeId > 0 ? employeeId : 1);

            bool success = _orderRepository.AddOrUpdateOrderDetail(orderId, productId, quantity);
            message = success ? "Đã thêm món vào bàn thành công!" : "Không thể thêm món vào hóa đơn.";
            return success;
        }

        // 6. Xóa món khỏi hóa đơn của bàn
        public bool RemoveProductFromTableOrder(int tableId, int productId, out string message)
        {
            if (tableId <= 0)
            {
                message = "Vui lòng chọn bàn cần thao tác!";
                return false;
            }

            if (productId <= 0)
            {
                message = "Vui lòng chọn món cần xóa trong bảng chi tiết hóa đơn!";
                return false;
            }

            var currentOrder = _orderRepository.GetUnpaidOrderByTableId(tableId);
            if (currentOrder == null)
            {
                message = "Bàn hiện tại chưa có hóa đơn để xóa món!";
                return false;
            }

            bool success = _orderRepository.RemoveOrderDetail(currentOrder.Id, productId);
            message = success ? "Đã xóa món khỏi hóa đơn!" : "Không tìm thấy món cần xóa trong hóa đơn.";
            return success;
        }

        // 7. Đọc tỷ lệ thuế VAT (%) từ App.config (mặc định 0% hoặc cấu hình trong App.config)
        public decimal GetVatPercent()
        {
            string vatStr = ConfigHelper.GetAppSetting("VATPercent");
            if (decimal.TryParse(vatStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal vat) && vat >= 0)
            {
                return vat;
            }
            return 0m;
        }

        // 8. Tính toán giảm giá (hỗ trợ nhập Mã khuyến mãi, % giảm giá hoặc số tiền giảm trực tiếp)
        public decimal CalculateDiscount(decimal totalAmount, string discountInput, out int? promotionId, out string discountDescription)
        {
            promotionId = null;
            discountDescription = string.Empty;

            if (totalAmount <= 0 || string.IsNullOrWhiteSpace(discountInput))
                return 0m;

            string raw = discountInput.Trim();

            // Trường hợp 1: Người dùng nhập số (% giảm giá nếu <= 100, hoặc số tiền VNĐ nếu > 100)
            if (decimal.TryParse(raw.Replace(",", "").Replace(".", ""), out decimal numericValue) && numericValue > 0)
            {
                if (numericValue <= 100)
                {
                    decimal discount = Math.Round(totalAmount * (numericValue / 100m), 0);
                    discountDescription = $"Giảm {numericValue}%";
                    return discount > totalAmount ? totalAmount : discount;
                }
                else
                {
                    discountDescription = $"Giảm trực tiếp {numericValue:N0} đ";
                    return numericValue > totalAmount ? totalAmount : numericValue;
                }
            }

            // Trường hợp 2: Người dùng nhập mã khuyến mãi (ví dụ: CHAOMUNG, VIPCOFFEE)
            var promo = _orderRepository.GetActivePromotionByCode(raw);
            if (promo != null)
            {
                promotionId = promo.Id;
                decimal percent = (decimal)promo.DiscountPercent;
                decimal discount = Math.Round(totalAmount * (percent / 100m), 0);
                discountDescription = $"Mã {promo.PromoCode} (-{percent}%)";
                return discount > totalAmount ? totalAmount : discount;
            }

            discountDescription = "Mã KM không hợp lệ/hết hạn";
            return 0m;
        }

        // 9. Tính toán tổng hợp tiền hàng, giảm giá, thuế VAT và thành tiền thanh toán
        public void CalculateOrderSummary(
            int tableId,
            string discountInput,
            out decimal totalAmount,
            out decimal discountAmount,
            out decimal vatPercent,
            out decimal vatAmount,
            out decimal finalAmount,
            out int? promotionId,
            out string discountInfo)
        {
            totalAmount = 0m;
            discountAmount = 0m;
            vatPercent = GetVatPercent();
            vatAmount = 0m;
            finalAmount = 0m;
            promotionId = null;
            discountInfo = string.Empty;

            var details = GetOrderDetailsByTable(tableId);
            foreach (var item in details)
            {
                totalAmount += item.TotalPrice;
            }

            if (totalAmount <= 0)
                return;

            discountAmount = CalculateDiscount(totalAmount, discountInput, out promotionId, out discountInfo);
            decimal amountAfterDiscount = Math.Max(0m, totalAmount - discountAmount);
            vatAmount = Math.Round(amountAfterDiscount * (vatPercent / 100m), 0);
            finalAmount = amountAfterDiscount + vatAmount;
        }

        // 10. Thanh toán hóa đơn cho bàn và trả về dữ liệu để mở màn hình Xem trước & In hóa đơn (FrmPrintReview)
        public bool CheckoutTableOrder(
            int tableId,
            string discountInput,
            out OrderDTO paidOrderDto,
            out List<OrderDetailDTO> orderDetails,
            out string message)
        {
            paidOrderDto = null;
            orderDetails = new List<OrderDetailDTO>();

            if (tableId <= 0)
            {
                message = "Vui lòng chọn bàn cần thanh toán!";
                return false;
            }

            var unpaidOrder = _orderRepository.GetUnpaidOrderByTableId(tableId);
            if (unpaidOrder == null)
            {
                message = "Bàn hiện tại chưa có hóa đơn cần thanh toán!";
                return false;
            }

            orderDetails = _orderRepository.GetOrderDetailsByOrderId(unpaidOrder.Id);
            if (orderDetails == null || orderDetails.Count == 0)
            {
                message = "Hóa đơn chưa có món nào, không thể thanh toán!";
                return false;
            }

            CalculateOrderSummary(
                tableId,
                discountInput,
                out decimal totalAmount,
                out decimal discountAmount,
                out decimal vatPercent,
                out decimal vatAmount,
                out decimal finalAmount,
                out int? promotionId,
                out string discountInfo);

            bool completed = _orderRepository.CompleteOrderPayment(
                unpaidOrder.Id,
                totalAmount,
                discountAmount,
                finalAmount,
                promotionId);

            if (!completed)
            {
                message = "Đã xảy ra lỗi khi lưu thanh toán hóa đơn!";
                return false;
            }

            paidOrderDto = _orderRepository.GetOrderDTOById(unpaidOrder.Id);
            if (paidOrderDto != null)
            {
                paidOrderDto.VATPercent = vatPercent;
                paidOrderDto.VATAmount = vatAmount;
                if (!string.IsNullOrEmpty(discountInfo) && string.IsNullOrEmpty(paidOrderDto.PromoCode))
                {
                    paidOrderDto.PromoCode = discountInfo;
                }
            }

            message = "Thanh toán hóa đơn thành công!";
            return true;
        }

        // 11. Chuyển bàn hoặc Gộp bàn
        public bool SwitchOrMergeTable(int sourceTableId, int targetTableId, out string message)
        {
            if (sourceTableId <= 0)
            {
                message = "Vui lòng chọn bàn nguồn đang có khách trước khi chuyển/gộp!";
                return false;
            }

            if (targetTableId <= 0)
            {
                message = "Vui lòng chọn bàn đích muốn chuyển/gộp tới!";
                return false;
            }

            if (sourceTableId == targetTableId)
            {
                message = "Bàn đích phải khác với bàn hiện tại!";
                return false;
            }

            var sourceOrder = _orderRepository.GetUnpaidOrderByTableId(sourceTableId);
            if (sourceOrder == null)
            {
                message = "Bàn hiện tại đang trống (chưa có hóa đơn), không thể chuyển hoặc gộp!";
                return false;
            }

            var targetOrder = _orderRepository.GetUnpaidOrderByTableId(targetTableId);
            bool isMerge = targetOrder != null;

            bool success = _orderRepository.SwitchOrMergeTable(sourceTableId, targetTableId);
            if (success)
            {
                message = isMerge
                    ? "Đã gộp hóa đơn sang bàn đích thành công!"
                    : "Đã chuyển bàn thành công!";
                return true;
            }

            message = "Không thể thực hiện chuyển/gộp bàn. Vui lòng kiểm tra lại!";
            return false;
        }
    }
}
