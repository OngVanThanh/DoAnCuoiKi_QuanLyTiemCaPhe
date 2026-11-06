using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    /// <summary>
    /// UserControl hiển thị thẻ món ăn / thức uống (UcProductCard) trên màn hình POS FrmOrder.
    /// Thiết kế theo phong cách hiện đại của các chuỗi cà phê (Mộc Cafe, The Coffee House, Highlands).
    /// Cho phép chọn món hoặc nhấn nút "+ Thêm" để đưa món trực tiếp vào hóa đơn tính tiền.
    /// </summary>
    public partial class UcProductCard : UserControl
    {
        public Product ProductData { get; private set; }
        public string CategoryName { get; private set; }
        private bool _isSelected = false;

        // Sự kiện khi người dùng click chọn thẻ món
        public event EventHandler<Product> ProductSelected;

        // Sự kiện khi người dùng nhấn nút "+ Thêm" trên thẻ món
        public event EventHandler<Product> ProductAddClicked;

        public UcProductCard()
        {
            InitializeComponent();
        }

        // 1. Gán dữ liệu món ăn/thức uống lên thẻ UserControl
        public void BindData(Product product, string categoryName = "")
        {
            ProductData = product;
            CategoryName = string.IsNullOrWhiteSpace(categoryName) ? GetDefaultCategoryLabel(product?.CategoryId ?? 1) : categoryName;

            if (product == null)
                return;

            lblProductName.Text = product.ProductName;
            lblUnitBadge.Text = string.IsNullOrWhiteSpace(product.Unit) ? "Ly" : product.Unit;
            lblCategoryBadge.Text = CategoryName;
            lblProductDesc.Text = $"{CategoryName} · Đơn vị: {lblUnitBadge.Text}";
            lblPrice.Text = $"{product.Price:N0} đ";

            // Thử tải ảnh thật từ đường dẫn tương đối qua UIHelper, nếu chưa có ảnh thì vẽ minh họa đồ uống/bánh ngọt
            UIHelper.LoadImageToPictureBox(picProduct, product.ImagePath);
            if (picProduct.Image == null)
            {
                picProduct.Image = CreateDrinkIllustration(product.ProductName, product.CategoryId);
            }
        }

        // 2. Cập nhật trạng thái nổi bật khi thẻ món đang được chọn
        public void SetSelected(bool selected)
        {
            _isSelected = selected;
            if (_isSelected)
            {
                pnlCard.BackColor = Color.FromArgb(236, 248, 240);
                btnQuickAdd.BackColor = UIHelper.PrimaryCoffee;
            }
            else
            {
                pnlCard.BackColor = Color.White;
                btnQuickAdd.BackColor = Color.FromArgb(30, 111, 67);
            }
        }

        // 3. Lấy nhãn danh mục mặc định theo CategoryId
        private string GetDefaultCategoryLabel(int categoryId)
        {
            switch (categoryId)
            {
                case 1: return "Cà phê";
                case 2: return "Trà & Trà sữa";
                case 3: return "Nước ép & Sinh tố";
                case 4: return "Bánh ngọt";
                default: return "Thực đơn";
            }
        }

        // 4. Vẽ hình minh họa ly cà phê / trà trái cây / sinh tố / bánh ngọt khi món chưa có file ảnh
        private Bitmap CreateDrinkIllustration(string productName, int categoryId)
        {
            Bitmap bmp = new Bitmap(184, 112);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Màu nền dịu theo từng nhóm món
                Color bgTop = Color.FromArgb(246, 240, 230);
                Color bgBottom = Color.FromArgb(234, 222, 206);
                Color accent = Color.FromArgb(111, 78, 55);

                if (categoryId == 2) // Trà & Trà sữa
                {
                    bgTop = Color.FromArgb(253, 242, 228);
                    bgBottom = Color.FromArgb(245, 222, 195);
                    accent = Color.FromArgb(211, 84, 0);
                }
                else if (categoryId == 3) // Nước ép & Sinh tố
                {
                    bgTop = Color.FromArgb(235, 247, 236);
                    bgBottom = Color.FromArgb(212, 239, 223);
                    accent = Color.FromArgb(39, 174, 96);
                }
                else if (categoryId == 4) // Bánh ngọt
                {
                    bgTop = Color.FromArgb(251, 238, 230);
                    bgBottom = Color.FromArgb(237, 215, 201);
                    accent = Color.FromArgb(160, 64, 0);
                }

                using (LinearGradientBrush bgBrush = new LinearGradientBrush(
                    new Rectangle(0, 0, 184, 112), bgTop, bgBottom, LinearGradientMode.Vertical))
                {
                    g.FillRectangle(bgBrush, 0, 0, 184, 112);
                }

                // Vẽ hình tròn trang trí phía sau
                using (SolidBrush circleBrush = new SolidBrush(Color.FromArgb(45, accent)))
                {
                    g.FillEllipse(circleBrush, 52, 18, 80, 80);
                }

                // Vẽ biểu tượng ly/bánh ở trung tâm
                using (Pen cupPen = new Pen(accent, 2.5f))
                using (SolidBrush cupFill = new SolidBrush(Color.FromArgb(210, accent)))
                {
                    if (categoryId == 4)
                    {
                        // Hình bánh ngọt
                        Point[] cakePoints =
                        {
                            new Point(64, 78),
                            new Point(120, 78),
                            new Point(112, 46),
                            new Point(72, 46)
                        };
                        g.FillPolygon(cupFill, cakePoints);
                        g.DrawPolygon(cupPen, cakePoints);
                        g.FillEllipse(Brushes.Crimson, 87, 36, 10, 10);
                    }
                    else
                    {
                        // Hình ly đồ uống
                        Point[] cupPoints =
                        {
                            new Point(70, 42),
                            new Point(114, 42),
                            new Point(107, 88),
                            new Point(77, 88)
                        };
                        g.FillPolygon(cupFill, cupPoints);
                        g.DrawPolygon(cupPen, cupPoints);

                        // Nắp ly & ống hút
                        g.DrawLine(cupPen, 66, 42, 118, 42);
                        g.DrawLine(cupPen, 96, 42, 106, 24);
                    }
                }
            }
            return bmp;
        }

        // 5. Sự kiện click chọn thẻ món
        private void Card_Click(object sender, EventArgs e)
        {
            if (ProductData != null)
            {
                ProductSelected?.Invoke(this, ProductData);
            }
        }

        // 6. Sự kiện nhấn nút "+ Thêm" để đưa món ngay vào hóa đơn
        private void btnQuickAdd_Click(object sender, EventArgs e)
        {
            if (ProductData != null)
            {
                ProductSelected?.Invoke(this, ProductData);
                ProductAddClicked?.Invoke(this, ProductData);
            }
        }
    }
}
