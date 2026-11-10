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
            using (var db = GetContext())
            {
                var query = from r in db.Recipes
                            join p in db.Products on r.ProductId equals p.Id
                            where r.ProductId == productId
                            select new RecipeDTO
                            {
                                Id = r.Id,
                                ProductId = r.ProductId,
                                ProductName = p.ProductName,
                                IngredientName = r.IngredientName,
                                Quantity = r.Quantity,
                                Unit = r.Unit,
                                Note = r.Note
                            };
                return query.ToList();
            }
        }

        // Lấy hướng dẫn các bước pha chế (PreparationSteps) của món
        public string GetPreparationSteps(int productId)
        {
            using (var db = GetContext())
            {
                var prod = db.Products.FirstOrDefault(p => p.Id == productId);
                return prod?.PreparationSteps ?? string.Empty;
            }
        }

        // Cập nhật hướng dẫn các bước pha chế cho Barista
        public bool UpdatePreparationSteps(int productId, string steps)
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

        // Thêm nguyên liệu vào công thức
        public bool InsertRecipe(Recipe recipe)
        {
            if (recipe == null) return false;
            using (var db = GetContext())
            {
                db.Recipes.InsertOnSubmit(recipe);
                db.SubmitChanges();
                return true;
            }
        }

        // Cập nhật định lượng nguyên liệu trong công thức
        public bool UpdateRecipe(Recipe recipe)
        {
            if (recipe == null) return false;
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

        // Xóa 1 thành phần khỏi công thức
        public bool DeleteRecipe(int recipeId)
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
    }
}