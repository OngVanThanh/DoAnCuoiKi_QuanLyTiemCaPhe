using System;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    public class CustomerDTO
    {
        // Xóa hoặc bỏ: public string Id { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public int RewardPoints { get; set; }
        public string MembershipTier { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}