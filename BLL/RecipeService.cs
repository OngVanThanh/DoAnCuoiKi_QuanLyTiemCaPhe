using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using System;
using System.Collections.Generic;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.BLL
{
    public class RecipeService
    {
        private readonly RecipeRepository _recipeRepo = new RecipeRepository();

        public List<RecipeDTO> GetRecipesByProduct(int productId)
        {
            if (productId <= 0) return new List<RecipeDTO>();
            return _recipeRepo.GetRecipesByProductId(productId);
        }

        public string GetPreparationSteps(int productId)
        {
            if (productId <= 0) return string.Empty;
            return _recipeRepo.GetPreparationSteps(productId);
        }

        public bool SavePreparationSteps(int productId, string steps, out string errorMsg)
        {
            errorMsg = string.Empty;
            if (productId <= 0)
            {
                errorMsg = "Vui lòng chọn một món đồ uống bên danh sách trái trước!";
                return false;
            }

            try
            {
                return _recipeRepo.UpdatePreparationSteps(productId, steps?.Trim() ?? string.Empty);
            }
            catch (Exception ex)
            {
                errorMsg = "Lỗi khi lưu quy trình pha chế: " + ex.Message;
                return false;
            }
        }

        public bool SaveRecipe(RecipeDTO dto, out string errorMsg)
        {
            errorMsg = string.Empty;
            if (dto == null || dto.ProductId <= 0)
            {
                errorMsg = "Vui lòng chọn món đồ uống cần thiết lập công thức!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(dto.IngredientName))
            {
                errorMsg = "Tên thành phần nguyên liệu không được để trống!";
                return false;
            }

            if (dto.Quantity <= 0)
            {
                errorMsg = "Định lượng phải lớn hơn 0!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(dto.Unit))
            {
                errorMsg = "Vui lòng chọn hoặc nhập đơn vị định lượng (ml, gr, muỗng...)!";
                return false;
            }

            var entity = new Recipe
            {
                Id = dto.Id,
                ProductId = dto.ProductId,
                IngredientName = dto.IngredientName.Trim(),
                Quantity = dto.Quantity,
                Unit = dto.Unit.Trim(),
                Note = dto.Note?.Trim()
            };

            try
            {
                if (dto.Id == 0)
                    return _recipeRepo.InsertRecipe(entity);
                else
                    return _recipeRepo.UpdateRecipe(entity);
            }
            catch (Exception ex)
            {
                errorMsg = "Lỗi lưu thành phần công thức: " + ex.Message;
                return false;
            }
        }

        public bool DeleteRecipe(int recipeId, out string errorMsg)
        {
            errorMsg = string.Empty;
            if (recipeId <= 0)
            {
                errorMsg = "Vui lòng chọn một thành phần nguyên liệu để xóa!";
                return false;
            }

            try
            {
                return _recipeRepo.DeleteRecipe(recipeId);
            }
            catch (Exception ex)
            {
                errorMsg = "Lỗi khi xóa thành phần: " + ex.Message;
                return false;
            }
        }
    }
}