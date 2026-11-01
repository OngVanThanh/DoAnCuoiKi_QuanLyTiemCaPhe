using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.BLL
{
    public class ProductService
    {
        private readonly ProductRepository _productRepo = new ProductRepository();

        // ==========================================
        // QUẢN LÝ NHÓM MÓN (CATEGORIES)
        // ==========================================
        public List<CategoryDTO> GetAllCategories()
        {
            return _productRepo.GetAllCategories();
        }

        public bool SaveCategory(CategoryDTO dto, out string errorMsg)
        {
            errorMsg = string.Empty;

            if (dto == null)
            {
                errorMsg = "Dữ liệu nhóm món không hợp lệ!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(dto.CategoryName))
            {
                errorMsg = "Tên nhóm món không được để trống!";
                return false;
            }

            var category = new Category
            {
                Id = dto.Id,
                CategoryName = dto.CategoryName.Trim(),
                Description = dto.Description?.Trim()
            };

            if (dto.Id == 0)
            {
                return _productRepo.InsertCategory(category);
            }
            else
            {
                return _productRepo.UpdateCategory(category);
            }
        }

        public bool DeleteCategory(int categoryId, out string errorMsg)
        {
            errorMsg = string.Empty;
            bool result = _productRepo.DeleteCategory(categoryId);

            if (!result)
            {
                errorMsg = "Không thể xóa nhóm món này vì đang có món ăn thuộc về nhóm!";
            }
            return result;
        }

        // ==========================================
        // QUẢN LÝ MÓN ĂN (PRODUCTS)
        // ==========================================
        public List<ProductDTO> GetAllProducts()
        {
            return _productRepo.GetAllProducts();
        }

        public List<ProductDTO> GetProductsByCategory(int categoryId)
        {
            if (categoryId <= 0) return GetAllProducts();
            return _productRepo.GetProductsByCategory(categoryId);
        }

        public List<ProductDTO> SearchProducts(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return GetAllProducts();
            return _productRepo.SearchProductsByName(keyword.Trim());
        }

        // Lưu món ăn: Kiểm tra tính hợp lệ & Copy ảnh vào thư mục tương đối
        public bool SaveProduct(ProductDTO dto, string sourceImagePath, out string errorMsg)
        {
            errorMsg = string.Empty;

            if (dto == null)
            {
                errorMsg = "Dữ liệu món ăn không hợp lệ!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(dto.ProductName))
            {
                errorMsg = "Tên món ăn không được để trống!";
                return false;
            }

            if (dto.CategoryId <= 0)
            {
                errorMsg = "Vui lòng chọn nhóm món ăn hợp lệ!";
                return false;
            }

            if (dto.Price <= 0)
            {
                errorMsg = "Giá bán món ăn phải lớn hơn 0 VNĐ!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(dto.Unit))
            {
                errorMsg = "Vui lòng nhập đơn vị tính (Ly, Tách, Đĩa...)!";
                return false;
            }

            // XỬ LÝ LƯU ẢNH THEO ĐƯỜNG DẪN TƯƠNG ĐỐI (Đúng chuẩn Giảng viên)
            string savedRelativePath = dto.ImagePath;

            if (!string.IsNullOrEmpty(sourceImagePath) && File.Exists(sourceImagePath))
            {
                try
                {
                    // Thư mục đích: Images\Products nằm ngay trong thư mục chạy của đồ án
                    string destFolder = Path.Combine(Application.StartupPath, "Images", "Products");
                    if (!Directory.Exists(destFolder))
                    {
                        Directory.CreateDirectory(destFolder);
                    }

                    // Đặt tên file duy nhất kèm mốc thời gian để không bị trùng
                    string fileExtension = Path.GetExtension(sourceImagePath);
                    string newFileName = $"prod_{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid().ToString().Substring(0, 5)}{fileExtension}";
                    string destFullPath = Path.Combine(destFolder, newFileName);

                    // Copy file ảnh
                    File.Copy(sourceImagePath, destFullPath, true);

                    // Chỉ lưu đường dẫn tương đối vào CSDL (ví dụ: Images\Products\prod_123.jpg)
                    savedRelativePath = Path.Combine("Images", "Products", newFileName);
                }
                catch (Exception ex)
                {
                    errorMsg = "Lỗi khi lưu ảnh món ăn: " + ex.Message;
                    return false;
                }
            }

            var product = new Product
            {
                Id = dto.Id,
                ProductName = dto.ProductName.Trim(),
                CategoryId = dto.CategoryId,
                Price = dto.Price,
                Unit = dto.Unit.Trim(),
                ImagePath = savedRelativePath,
                IsActive = dto.IsActive ?? true
            };

            if (dto.Id == 0)
            {
                return _productRepo.InsertProduct(product);
            }
            else
            {
                return _productRepo.UpdateProduct(product);
            }
        }

        public bool DeleteProduct(int productId, out string errorMsg)
        {
            errorMsg = string.Empty;
            return _productRepo.DeleteProduct(productId);
        }
    }
}