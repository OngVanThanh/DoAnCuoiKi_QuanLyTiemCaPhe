using System;
using System.Collections.Generic;
using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.BLL
{
    public class CustomerService
    {
        private readonly CustomerRepository _repository = new CustomerRepository();

        public List<CustomerDTO> GetAll()
        {
            return _repository.GetAllCustomers();
        }

        public string CalculateRank(int points)
        {
            if (points >= 500) return "Kim Cương";
            if (points >= 300) return "Vàng";
            if (points >= 100) return "Bạc";
            return "Thường"; // Mặc định ban đầu
        }

        public bool Add(CustomerDTO dto, out string errorMsg)
        {
            errorMsg = string.Empty;

            // 1. Kiểm tra rỗng trước
            if (string.IsNullOrWhiteSpace(dto.Id))
            {
                errorMsg = "Mã khách hàng không được để trống!";
                return false;
            }

            // 2. Kiểm tra xem có đúng định dạng số nguyên dương không
            if (!int.TryParse(dto.Id, out int id) || id <= 0)
            {
                errorMsg = "Mã khách hàng phải là chữ số nguyên dương lớn hơn 0!";
                return false;
            }

            // 3. Kiểm tra xem ID đã tồn tại trong Database chưa
            if (_repository.IsIdExists(id))
            {
                errorMsg = $"Mã ID [{dto.Id}] đã tồn tại trong hệ thống! Vui lòng chọn ID khác.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(dto.CustomerName))
            {
                errorMsg = "Tên khách hàng không được để trống!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(dto.Phone) || dto.Phone.Length < 9)
            {
                errorMsg = "Số điện thoại không hợp lệ!";
                return false;
            }

            if (_repository.IsCustomerExists(dto.Id, dto.Phone))
            {
                errorMsg = "Số điện thoại đã tồn tại trong hệ thống!";
                return false;
            }

            dto.MembershipTier = CalculateRank(dto.RewardPoints);
            return _repository.InsertCustomer(dto);
        }

        public bool Update(CustomerDTO dto, out string errorMsg)
        {
            errorMsg = string.Empty;
            if (string.IsNullOrWhiteSpace(dto.Id))
            {
                errorMsg = "Vui lòng chọn khách hàng cần sửa!";
                return false;
            }

            dto.MembershipTier = CalculateRank(dto.RewardPoints);
            return _repository.UpdateCustomer(dto);
        }

        public bool Delete(string customerId, out string errorMsg)
        {
            errorMsg = string.Empty;
            if (string.IsNullOrWhiteSpace(customerId))
            {
                errorMsg = "Vui lòng chọn khách hàng cần xóa!";
                return false;
            }
            return _repository.DeleteCustomer(customerId);
        }

        public List<CustomerDTO> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return _repository.GetAllCustomers();
            return _repository.SearchCustomers(keyword.Trim());
        }
    }
}