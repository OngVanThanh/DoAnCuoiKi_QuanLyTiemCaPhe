using System;
using System.Collections.Generic;
using System.Linq;
using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.BLL
{
    public class CustomerService
    {
        private readonly CustomerRepository _repository = new CustomerRepository();

        // 1. Lấy toàn bộ danh sách khách hàng
        public List<CustomerDTO> GetAll()
        {
            return _repository.GetAllCustomers();
        }

        // 2. Lấy khách hàng theo Số điện thoại (Khóa chính)
        public CustomerDTO GetByPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return null;
            return _repository.GetByPhone(phone.Trim());
        }

        // 3. Tìm kiếm theo Số điện thoại hoặc Họ tên
        public List<CustomerDTO> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAll();
            return _repository.SearchCustomers(keyword.Trim());
        }

        // 4. Tính xếp hạng dựa trên điểm tích lũy
        public string CalculateRank(int points)
        {
            if (points >= 1000) return "Kim Cương";
            if (points >= 500) return "Vàng";
            if (points >= 200) return "Bạc";
            return "Thường";
        }

        // 5. Thêm mới khách hàng (Khóa định danh là Số điện thoại)
        public bool Add(CustomerDTO customer, out string error)
        {
            error = string.Empty;
            if (customer == null)
            {
                error = "Dữ liệu khách hàng không hợp lệ!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(customer.Phone))
            {
                error = "Số điện thoại không được để trống!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(customer.CustomerName))
            {
                error = "Tên khách hàng không được để trống!";
                return false;
            }

            if (customer.RewardPoints < 0)
            {
                error = "Điểm tích lũy không được nhỏ hơn 0!";
                return false;
            }

            if (_repository.PhoneExists(customer.Phone))
            {
                error = "Số điện thoại này đã tồn tại trong hệ thống!";
                return false;
            }

            return _repository.AddCustomer(customer, out error);
        }

        // 6. Cập nhật khách hàng theo Số điện thoại
        public bool Update(CustomerDTO customer, out string error)
        {
            error = string.Empty;
            if (customer == null || string.IsNullOrWhiteSpace(customer.Phone))
            {
                error = "Số điện thoại không hợp lệ!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(customer.CustomerName))
            {
                error = "Tên khách hàng không được để trống!";
                return false;
            }

            if (customer.RewardPoints < 0)
            {
                error = "Điểm tích lũy không được nhỏ hơn 0!";
                return false;
            }

            return _repository.UpdateCustomer(customer, out error);
        }

        // 7. Xóa khách hàng theo Số điện thoại
        public bool Delete(string phone, out string error)
        {
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(phone))
            {
                error = "Vui lòng chọn số điện thoại cần xóa!";
                return false;
            }

            return _repository.DeleteCustomer(phone.Trim(), out error);
        }

        // 8. Lấy Top khách hàng cho báo cáo RDLC
        public List<CustomerDTO> GetTopCustomersByPoints(int topCount = 10)
        {
            return _repository.GetTopCustomersByPoints(topCount);
        }

        // 9. Tương thích ngược: Lấy theo Id số nếu còn nơi nào gọi
        public CustomerDTO GetById(int id)
        {
            return _repository.GetCustomerById(id);
        }

        // 10. Cộng điểm theo tổng tiền thanh toán và trừ điểm nếu có quy đổi khuyến mãi
        public bool AddRewardPoints(string phone, int pointsToAdd, int pointsToDeduct, out int updatedPoints, out string updatedTier, out string error)
        {
            return _repository.AddRewardPoints(phone, pointsToAdd, pointsToDeduct, out updatedPoints, out updatedTier, out error);
        }
    }
}
