using System;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    public class CustomerDTO
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public int RewardPoints { get; set; }
        public string MembershipTier { get; set; }
        public DateTime RegistrationDate { get; set; } // Cột mới thêm

        public CustomerDTO()
        {
            RewardPoints = 0;
            MembershipTier = "Thường";
            RegistrationDate = DateTime.Now;
        }

        public CustomerDTO(string id, string customerName, string phone, int rewardPoints, string membershipTier, DateTime registrationDate)
        {
            Id = id;
            CustomerName = customerName;
            Phone = phone;
            RewardPoints = rewardPoints;
            MembershipTier = membershipTier;
            RegistrationDate = registrationDate;
        }
    }
}