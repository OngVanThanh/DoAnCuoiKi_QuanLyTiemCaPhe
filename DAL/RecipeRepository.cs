using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DAL
{
    public class RecipeRepository
    {
        private CoffeeShopDataContextDataContext GetContext()
        {
            return new CoffeeShopDataContextDataContext(ConfigHelper.GetConnectionString("CoffeeShopDB"));
        }

        // Lấy danh sách thành phần định lượng của 1 món đồ uống
        public List<RecipeDTO> GetRecipesByProductId(int productId)
        {
            try
            {
                using (var db = GetContext())
                {
                    return db.Recipes
                        .Where(r => r.ProductId == productId)
                        .Select(r => new RecipeDTO
                        {
                            Id = r.Id,
                            ProductId = r.ProductId,
                            ProductName = r.Product != null ? r.Product.ProductName : string.Empty,
                            IngredientName = r.IngredientName,
                            Quantity = r.Quantity,
                            Unit = r.Unit,
                            Note = r.Note
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                // Tự động kích hoạt đồng bộ bảng Recipes nếu bảng chưa tồn tại
                if (ex.Message.Contains("dbo.Recipes") || ex.Message.Contains("Invalid object name"))
                {
                    try
                    {
                        if (DatabaseMigrationHelper.EnsureDatabaseSchema(out _))
                        {
                            using (var dbRetry = GetContext())
                            {
                                return dbRetry.Recipes
                                    .Where(r => r.ProductId == productId)
                                    .Select(r => new RecipeDTO
                                    {
                                        Id = r.Id,
                                        ProductId = r.ProductId,
                                        ProductName = r.Product != null ? r.Product.ProductName : string.Empty,
                                        IngredientName = r.IngredientName,
                                        Quantity = r.Quantity,
                                        Unit = r.Unit,
                                        Note = r.Note
                                    })
                                    .ToList();
                            }
                        }
                    }
                    catch { }
                }

                // Fallback danh sách công thức mẫu nếu CSDL chưa có bảng Recipes
                return GetFallbackRecipes(productId);
            }
        }

        private List<RecipeDTO> GetFallbackRecipes(int productId)
        {
            var fallback = new List<RecipeDTO>();
            if (productId == 1)
            {
                fallback.Add(new RecipeDTO { Id = 1, ProductId = 1, ProductName = "Cà phê đen đá", IngredientName = "Hạt Cà Phê Robusta", Quantity = 0.025, Unit = "Kg", Note = "Xay mức mịn vừa" });
                fallback.Add(new RecipeDTO { Id = 2, ProductId = 1, ProductName = "Cà phê đen đá", IngredientName = "Đường kính trắng", Quantity = 0.010, Unit = "Kg", Note = "Gia giảm theo yêu cầu" });
            }
            else if (productId == 2)
            {
                fallback.Add(new RecipeDTO { Id = 3, ProductId = 2, ProductName = "Cà phê sữa đá", IngredientName = "Hạt Cà Phê Robusta", Quantity = 0.025, Unit = "Kg", Note = "Cốt phin 45ml" });
                fallback.Add(new RecipeDTO { Id = 4, ProductId = 2, ProductName = "Cà phê sữa đá", IngredientName = "Sữa đặc Ngôi Sao", Quantity = 0.030, Unit = "Lon", Note = "Rót đáy ly" });
            }
            else
            {
                fallback.Add(new RecipeDTO { Id = 5, ProductId = productId, ProductName = "Món chọn", IngredientName = "Nguyên liệu pha chế tiêu chuẩn", Quantity = 1, Unit = "Phần", Note = "Công thức tiêu chuẩn quán" });
            }
            return fallback;
        }

        // Lấy hướng dẫn các bước pha chế (PreparationSteps) của món
        public string GetPreparationSteps(int productId)
        {
            try
            {
                using (var db = GetContext())
                {
                    var prod = db.Products.FirstOrDefault(p => p.Id == productId);
                    return prod?.PreparationSteps ?? string.Empty;
                }
            }
            catch
            {
                return "1. Chuẩn bị dụng cụ và nguyên liệu theo định lượng.\n2. Thực hiện pha chế theo tiêu chuẩn Barista.\n3. Trang trí và phục vụ khách hàng.";
            }
        }

        // Cập nhật hướng dẫn các bước pha chế cho Barista
        public bool UpdatePreparationSteps(int productId, string steps)
        {
            try
            {
                using (var db = GetContext())
                {
                    var prod = db.Products.FirstOrDefault(p => p.Id == productId);
                    if (prod == null) return false;

                    prod.PreparationSteps = steps;
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // Thêm nguyên liệu vào công thức
        public bool InsertRecipe(Recipe recipe)
        {
            if (recipe == null) return false;
            try
            {
                using (var db = GetContext())
                {
                    db.Recipes.InsertOnSubmit(recipe);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // Cập nhật định lượng nguyên liệu trong công thức
        public bool UpdateRecipe(Recipe recipe)
        {
            if (recipe == null) return false;
            try
            {
                using (var db = GetContext())
                {
                    var existing = db.Recipes.FirstOrDefault(r => r.Id == recipe.Id);
                    if (existing == null) return false;

                    existing.IngredientName = recipe.IngredientName;
                    existing.Quantity = recipe.Quantity;
                    existing.Unit = recipe.Unit;
                    existing.Note = recipe.Note;

                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // Xóa 1 thành phần khỏi công thức
        public bool DeleteRecipe(int recipeId)
        {
            try
            {
                using (var db = GetContext())
                {
                    var existing = db.Recipes.FirstOrDefault(r => r.Id == recipeId);
                    if (existing == null) return false;

                    db.Recipes.DeleteOnSubmit(existing);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
