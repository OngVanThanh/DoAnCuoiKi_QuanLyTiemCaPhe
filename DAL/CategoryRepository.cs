using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DAL
{
    /// <summary>
    /// Repository quản lý Danh mục nhóm sản phẩm (Categories).
    /// Tuân thủ 100% cú pháp LINQ Lambda (Zero Raw SQL, Zero Query Syntax).
    /// </summary>
    public class CategoryRepository
    {
        private CoffeeShopDataContextDataContext GetContext()
        {
            return new CoffeeShopDataContextDataContext(ConfigHelper.GetConnectionString("CoffeeShopDB"));
        }

        // 1. Lấy toàn bộ nhóm món
        public List<CategoryDTO> GetAllCategories()
        {
            try
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
            catch
            {
                return new List<CategoryDTO>
                {
                    new CategoryDTO { Id = 1, CategoryName = "Cà phê truyền thống", Description = "Cà phê pha phin, pha máy" },
                    new CategoryDTO { Id = 2, CategoryName = "Trà & Macchiato", Description = "Thanh mát sảng khoái" },
                    new CategoryDTO { Id = 3, CategoryName = "Đá xay & Sinh tố", Description = "Mát lạnh ngày hè" },
                    new CategoryDTO { Id = 4, CategoryName = "Bánh ngọt & Tráng miệng", Description = "Thơm ngon mỗi ngày" }
                };
            }
        }

        // 2. Thêm nhóm món mới
        public bool InsertCategory(Category category)
        {
            if (category == null) return false;
            try
            {
                using (var db = GetContext())
                {
                    db.Categories.InsertOnSubmit(category);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // 3. Cập nhật nhóm món
        public bool UpdateCategory(Category category)
        {
            if (category == null) return false;
            try
            {
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
            catch
            {
                return false;
            }
        }

        // 4. Xóa nhóm món (Kiểm tra nếu còn món thuộc nhóm thì không cho xóa)
        public bool DeleteCategory(int categoryId)
        {
            try
            {
                using (var db = GetContext())
                {
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
            catch
            {
                return false;
            }
        }
    }
}
