using System;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public string ImagePath { get; set; }
        public bool? IsActive { get; set; }
    }
}