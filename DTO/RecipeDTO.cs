using System;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string IngredientName { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public string Note { get; set; }
    }
}