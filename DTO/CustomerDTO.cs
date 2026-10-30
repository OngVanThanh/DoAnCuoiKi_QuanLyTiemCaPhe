using System;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    public class CustomerDTO
    {
        // 1. Các thuộc tính đại diện cho một khách hàng
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public int RewardPoints { get; set; }
        public string MembershipTier { get; set; }
        public DateTime CreatedDate { get; set; }

        // 2. Constructor mặc định (Parameterless Constructor)
        public CustomerDTO()
        {
            RewardPoints = 0;
            MembershipTier = "Thường";
            CreatedDate = DateTime.Now;
        }

        // 3. Constructor đầy đủ tham số (hỗ trợ khởi tạo nhanh khi đọc từ Database)
        public CustomerDTO(string id, string customerName, string phone, int rewardPoints, string membershipTier, DateTime createdDate)
        {
            Id = id;
            CustomerName = customerName;
            Phone = phone;
            RewardPoints = rewardPoints;
            MembershipTier = membershipTier;
            CreatedDate = createdDate;
        }

        // 4. (Tùy chọn) Ghi đè ToString để tiện debug hoặc hiển thị ComboBox
        public override string ToString()
        {
            return $"{CustomerName} - {Phone} ({MembershipTier})";
        }
    }
}