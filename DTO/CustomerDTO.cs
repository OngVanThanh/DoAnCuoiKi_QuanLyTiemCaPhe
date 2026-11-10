using System;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    public class CustomerDTO
    {
        // Khóa định danh chính: Số điện thoại (Phone)
        public string Phone { get; set; }
        public string CustomerName { get; set; }
        public int RewardPoints { get; set; }
        public string MembershipTier { get; set; }
        public DateTime RegistrationDate { get; set; }

        // Tương thích nếu có nơi gọi Id thì trả về Phone
        public string Id
        {
            get => Phone;
            set => Phone = value;
        }

        public CustomerDTO()
        {
            RewardPoints = 0;
            MembershipTier = "Thường";
            RegistrationDate = DateTime.Now;
        }

        public CustomerDTO(string customerName, string phone, int rewardPoints, string membershipTier, DateTime registrationDate)
        {
            CustomerName = customerName;
            Phone = phone;
            RewardPoints = rewardPoints;
            MembershipTier = membershipTier;
            RegistrationDate = registrationDate;
        }
    }
}
