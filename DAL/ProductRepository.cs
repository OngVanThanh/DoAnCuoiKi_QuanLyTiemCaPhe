using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DAL
{
    public class ProductRepository
    {
        // 1. Khởi tạo DataContext lấy connection string từ App.config
        private CoffeeShopDataContextDataContext GetContext()
        {
            return new CoffeeShopDataContextDataContext(ConfigHelper.GetConnectionString("CoffeeShopDB"));
        }

        // ==========================================================
        // A. CÁC THAO TÁC VỚI NHÓM MÓN (CATEGORIES)
        // ==========================================================

        // Lấy toàn bộ nhóm món
        public List<CategoryDTO> GetAllCategories()
        {
            using (var db = GetContext())
            {
                return db.Categories
                    .Select(c => new CategoryDTO
                    {
                        Id = c.Id,
                        CategoryName = c.CategoryName,
                        Description = c.Description
                    })
                    .ToList();
            }
        }

        // Thêm nhóm món mới
        public bool InsertCategory(Category category)
        {
            if (category == null) return false;
            using (var db = GetContext())
            {
                db.Categories.InsertOnSubmit(category);
                db.SubmitChanges();
                return true;
            }
        }

        // Cập nhật nhóm món
        public bool UpdateCategory(Category category)
        {
            if (category == null) return false;
            using (var db = GetContext())
            {
                var existing = db.Categories.FirstOrDefault(c => c.Id == category.Id);
                if (existing == null) return false;

                existing.CategoryName = category.CategoryName;
                existing.Description = category.Description;

                db.SubmitChanges();
                return true;
            }
        }

        // Xóa nhóm món (Kiểm tra nếu còn món thuộc nhóm thì không cho xóa)
        public bool DeleteCategory(int categoryId)
        {
            using (var db = GetContext())
            {
                // Kiểm tra ràng buộc khóa ngoại: có món nào đang thuộc nhóm này không?
                bool hasProducts = db.Products.Any(p => p.CategoryId == categoryId);
                if (hasProducts) return false;

                var existing = db.Categories.FirstOrDefault(c => c.Id == categoryId);
                if (existing != null)
                {
                    db.Categories.DeleteOnSubmit(existing);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
        }

        // ==========================================================
        // B. CÁC THAO TÁC VỚI MÓN ĂN / ĐỒ UỐNG (PRODUCTS)
        // ==========================================================

        // Lấy danh sách món kết hợp JOIN với Categories để lấy CategoryName
        public List<ProductDTO> GetAllProducts()
        {
            using (var db = GetContext())
            {
                return db.Products
                    .Select(p => new ProductDTO
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        CategoryId = p.CategoryId,
                        CategoryName = p.Category != null ? p.Category.CategoryName : string.Empty,
                        Price = p.Price,
                        Unit = p.Unit,
                        ImagePath = p.ImagePath,
                        IsActive = p.IsActive
                    })
                    .ToList();
            }
        }

        // Lọc danh sách món theo Nhóm danh mục
        public List<ProductDTO> GetProductsByCategory(int categoryId)
        {
            using (var db = GetContext())
            {
                return db.Products
                    .Where(p => p.CategoryId == categoryId)
                    .Select(p => new ProductDTO
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        CategoryId = p.CategoryId,
                        CategoryName = p.Category != null ? p.Category.CategoryName : string.Empty,
                        Price = p.Price,
                        Unit = p.Unit,
                        ImagePath = p.ImagePath,
                        IsActive = p.IsActive
                    })
                    .ToList();
            }
        }

        // Tìm kiếm món theo tên (Search)
        public List<ProductDTO> SearchProductsByName(string keyword)
        {
            using (var db = GetContext())
            {
                return db.Products
                    .Where(p => p.ProductName.Contains(keyword))
                    .Select(p => new ProductDTO
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        CategoryId = p.CategoryId,
                        CategoryName = p.Category != null ? p.Category.CategoryName : string.Empty,
                        Price = p.Price,
                        Unit = p.Unit,
                        ImagePath = p.ImagePath,
                        IsActive = p.IsActive
                    })
                    .ToList();
            }
        }

        // Thêm món mới
        public bool InsertProduct(Product product)
        {
            if (product == null) return false;
            using (var db = GetContext())
            {
                db.Products.InsertOnSubmit(product);
                db.SubmitChanges();
                return true;
            }
        }

        // Cập nhật thông tin món
        public bool UpdateProduct(Product product)
        {
            if (product == null) return false;
            using (var db = GetContext())
            {
                var existing = db.Products.FirstOrDefault(p => p.Id == product.Id);
                if (existing == null) return false;

                existing.ProductName = product.ProductName;
                existing.CategoryId = product.CategoryId;
                existing.Price = product.Price;
                existing.Unit = product.Unit;
                if (!string.IsNullOrEmpty(product.ImagePath))
                {
                    existing.ImagePath = product.ImagePath;
                }
                existing.IsActive = product.IsActive;

                db.SubmitChanges();
                return true;
            }
        }

        // Xóa món
        public bool DeleteProduct(int productId)
        {
            using (var db = GetContext())
            {
                var existing = db.Products.FirstOrDefault(p => p.Id == productId);
                if (existing != null)
                {
                    db.Products.DeleteOnSubmit(existing);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
    }
}