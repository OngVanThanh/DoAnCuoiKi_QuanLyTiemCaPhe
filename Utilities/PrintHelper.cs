using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.Utilities
{
    /// <summary>
    /// Lớp tiện ích hỗ trợ vẽ và định dạng Hóa đơn bán hàng khổ K80 (80mm) trên đối tượng Graphics.
    /// Đọc thông tin cửa hàng (ShopName, ShopAddress, ShopPhone) trực tiếp từ App.config.
    /// </summary>
    public static class PrintHelper
    {
        // Chiều rộng chuẩn của hóa đơn nhiệt K80 (khoảng 280px - 300px ở 96 DPI)
        private const int ReceiptWidth = 285;

        /// <summary>
        /// Vẽ toàn bộ nội dung hóa đơn thanh toán khổ K80 lên trang in.
        /// </summary>
        public static void DrawInvoiceK80(
            Graphics g,
            OrderDTO order,
            List<OrderDetailDTO> details,
            int pageWidth = 0)
        {
            if (g == null)
                return;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Lấy thông tin quán từ App.config
            string shopName = ConfigHelper.GetAppSetting("ShopName") ?? "THE COFFEE HOUSE";
            string shopAddress = ConfigHelper.GetAppSetting("ShopAddress") ?? "123 Nguyễn Huệ, Q.1, TP.HCM";
            string shopPhone = ConfigHelper.GetAppSetting("ShopPhone") ?? "0909123456";

            // Tính toán lề trái để căn giữa khổ K80 khi xem trước trên trang in lớn
            int startX = 15;
            if (pageWidth > ReceiptWidth + 40)
            {
                startX = (pageWidth - ReceiptWidth) / 2;
            }

            int currentY = 20;
            int contentWidth = ReceiptWidth;

            using (Font fontShopName = new Font("Segoe UI", 12, FontStyle.Bold))
            using (Font fontTitle = new Font("Segoe UI", 11, FontStyle.Bold))
            using (Font fontHeader = new Font("Segoe UI", 8.5f, FontStyle.Bold))
            using (Font fontRegular = new Font("Segoe UI", 8.5f, FontStyle.Regular))
            using (Font fontItalic = new Font("Segoe UI", 8f, FontStyle.Italic))
            using (Font fontTotal = new Font("Segoe UI", 10f, FontStyle.Bold))
            using (Pen dashPen = new Pen(Color.Black, 1) { DashStyle = DashStyle.Dash })
            {
                StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center };
                StringFormat rightFormat = new StringFormat { Alignment = StringAlignment.Far };
                StringFormat leftFormat = new StringFormat { Alignment = StringAlignment.Near };

                // 1. Tiêu đề thông tin quán
                g.DrawString(shopName.ToUpper(), fontShopName, Brushes.Black,
                    new RectangleF(startX, currentY, contentWidth, 24), centerFormat);
                currentY += 24;

                g.DrawString("Đ/c: " + shopAddress, fontRegular, Brushes.Black,
                    new RectangleF(startX, currentY, contentWidth, 18), centerFormat);
                currentY += 18;

                g.DrawString("SĐT: " + shopPhone, fontRegular, Brushes.Black,
                    new RectangleF(startX, currentY, contentWidth, 18), centerFormat);
                currentY += 22;

                // Đường kẻ nét đứt
                g.DrawLine(dashPen, startX, currentY, startX + contentWidth, currentY);
                currentY += 10;

                // 2. Tiêu đề Hóa đơn & Thông tin chung
                g.DrawString("HÓA ĐƠN THANH TOÁN", fontTitle, Brushes.Black,
                    new RectangleF(startX, currentY, contentWidth, 22), centerFormat);
                currentY += 26;

                int orderId = order != null ? order.Id : 0;
                string tableName = order != null && !string.IsNullOrEmpty(order.TableName) ? order.TableName : "Mang về";
                string employeeName = order != null && !string.IsNullOrEmpty(order.EmployeeName) ? order.EmployeeName : "Thu ngân";
                DateTime orderDate = order?.OrderDate ?? DateTime.Now;

                g.DrawString($"Số HĐ: #{orderId:D5}", fontRegular, Brushes.Black, startX, currentY);
                g.DrawString($"Bàn: {tableName}", fontHeader, Brushes.Black,
                    new RectangleF(startX, currentY, contentWidth, 18), rightFormat);
                currentY += 18;

                g.DrawString($"Ngày: {orderDate:dd/MM/yyyy HH:mm}", fontRegular, Brushes.Black, startX, currentY);
                currentY += 18;

                g.DrawString($"Thu ngân: {employeeName}", fontRegular, Brushes.Black, startX, currentY);
                currentY += 20;

                // 3. Tiêu đề bảng chi tiết món
                g.DrawLine(dashPen, startX, currentY, startX + contentWidth, currentY);
                currentY += 6;

                int colNameWidth = 125;
                int colQtyX = startX + 128;
                int colQtyWidth = 28;
                int colPriceX = startX + 158;
                int colPriceWidth = 55;
                int colTotalX = startX + 215;
                int colTotalWidth = contentWidth - 215;

                g.DrawString("Tên món", fontHeader, Brushes.Black, startX, currentY);
                g.DrawString("SL", fontHeader, Brushes.Black,
                    new RectangleF(colQtyX, currentY, colQtyWidth, 18), centerFormat);
                g.DrawString("Đ.Giá", fontHeader, Brushes.Black,
                    new RectangleF(colPriceX, currentY, colPriceWidth, 18), rightFormat);
                g.DrawString("T.Tiền", fontHeader, Brushes.Black,
                    new RectangleF(colTotalX, currentY, colTotalWidth, 18), rightFormat);
                currentY += 20;

                g.DrawLine(dashPen, startX, currentY, startX + contentWidth, currentY);
                currentY += 8;

                // 4. Danh sách các món trong hóa đơn
                if (details != null && details.Count > 0)
                {
                    foreach (var item in details)
                    {
                        RectangleF nameRect = new RectangleF(startX, currentY, colNameWidth, 36);
                        SizeF measuredName = g.MeasureString(item.ProductName, fontRegular, colNameWidth);
                        int rowHeight = Math.Max(18, (int)Math.Ceiling(measuredName.Height) + 2);

                        g.DrawString(item.ProductName, fontRegular, Brushes.Black, nameRect, leftFormat);
                        g.DrawString(item.Quantity.ToString(), fontRegular, Brushes.Black,
                            new RectangleF(colQtyX, currentY, colQtyWidth, rowHeight), centerFormat);
                        g.DrawString(item.UnitPrice.ToString("N0"), fontRegular, Brushes.Black,
                            new RectangleF(colPriceX, currentY, colPriceWidth, rowHeight), rightFormat);
                        g.DrawString(item.TotalPrice.ToString("N0"), fontRegular, Brushes.Black,
                            new RectangleF(colTotalX, currentY, colTotalWidth, rowHeight), rightFormat);

                        currentY += rowHeight + 2;
                    }
                }
                else
                {
                    g.DrawString("(Chưa có món trong hóa đơn)", fontItalic, Brushes.Black,
                        new RectangleF(startX, currentY, contentWidth, 18), centerFormat);
                    currentY += 20;
                }

                currentY += 4;
                g.DrawLine(dashPen, startX, currentY, startX + contentWidth, currentY);
                currentY += 10;

                // 5. Tổng kết tiền hàng, giảm giá, VAT và Tổng thanh toán
                decimal totalAmount = order?.TotalAmount ?? 0m;
                decimal discountAmount = order?.DiscountAmount ?? 0m;
                decimal vatPercent = order?.VATPercent ?? 0m;
                decimal vatAmount = order?.VATAmount ?? 0m;
                decimal finalAmount = order?.FinalAmount ?? 0m;

                // Dòng Tổng tiền hàng
                g.DrawString("Tổng tiền hàng:", fontRegular, Brushes.Black, startX, currentY);
                g.DrawString($"{totalAmount:N0} đ", fontRegular, Brushes.Black,
                    new RectangleF(startX, currentY, contentWidth, 18), rightFormat);
                currentY += 18;

                // Dòng Giảm giá
                string discountLabel = "Giảm giá:";
                if (order != null && !string.IsNullOrWhiteSpace(order.PromoCode))
                {
                    discountLabel = $"Giảm giá ({order.PromoCode}):";
                }
                g.DrawString(discountLabel, fontRegular, Brushes.Black, startX, currentY);
                g.DrawString($"-{discountAmount:N0} đ", fontRegular, Brushes.Black,
                    new RectangleF(startX, currentY, contentWidth, 18), rightFormat);
                currentY += 18;

                // Dòng Thuế VAT (nếu có cấu hình VAT > 0)
                if (vatPercent > 0 || vatAmount > 0)
                {
                    g.DrawString($"Thuế VAT ({vatPercent:0.##}%):", fontRegular, Brushes.Black, startX, currentY);
                    g.DrawString($"+{vatAmount:N0} đ", fontRegular, Brushes.Black,
                        new RectangleF(startX, currentY, contentWidth, 18), rightFormat);
                    currentY += 18;
                }

                currentY += 4;
                g.DrawLine(dashPen, startX, currentY, startX + contentWidth, currentY);
                currentY += 8;

                // Dòng Tổng thanh toán cuối cùng
                g.DrawString("TỔNG THANH TOÁN:", fontTotal, Brushes.Black, startX, currentY);
                g.DrawString($"{finalAmount:N0} đ", fontTotal, Brushes.Black,
                    new RectangleF(startX, currentY, contentWidth, 22), rightFormat);
                currentY += 28;

                g.DrawLine(dashPen, startX, currentY, startX + contentWidth, currentY);
                currentY += 12;

                // 6. Lời cảm ơn cuối trang
                g.DrawString("Cảm ơn Quý khách & Hẹn gặp lại!", fontItalic, Brushes.Black,
                    new RectangleF(startX, currentY, contentWidth, 18), centerFormat);
                currentY += 18;
                g.DrawString("--- Wifi: TheCoffeeHouse / Pass: 12345678 ---", fontItalic, Brushes.Gray,
                    new RectangleF(startX, currentY, contentWidth, 18), centerFormat);
            }
        }
    }
}
