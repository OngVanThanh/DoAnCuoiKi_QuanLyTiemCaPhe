using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

        // Xếp hạng tự động theo điểm thưởng
        public string CalculateRank(int points)
        {
            if (points >= 500) return "Kim Cương";
            if (points >= 300) return "Vàng";
            if (points >= 100) return "Bạc";
            return "Thường";
        }

        private bool ValidateCustomerData(CustomerDTO dto, bool isUpdate, out string errorMsg)
        {
            errorMsg = string.Empty;

            if (string.IsNullOrWhiteSpace(dto.CustomerName))
            {
                errorMsg = "Tên khách hàng không được để trống!";
                return false;
            }

            // Kiểm tra SĐT có bắt đầu bằng số 0 và đúng 10 số hay không
            if (string.IsNullOrWhiteSpace(dto.Phone) || !Regex.IsMatch(dto.Phone.Trim(), @"^0[0-9]{9}$"))
            {
                errorMsg = "Số điện thoại không hợp lệ! Phải bắt đầu bằng số 0 và có đúng 10 chữ số.";
                return false;
            }

            // Kiểm tra trùng lặp SĐT trong hệ thống
            if (_repository.IsPhoneExists(dto.Phone.Trim(), isUpdate ? dto.Id : null))
            {
                errorMsg = "Số điện thoại này đã được sử dụng cho một khách hàng khác!";
                return false;
            }

            return true;
        }

        public bool Add(CustomerDTO dto, out string errorMsg)
        {
            if (!ValidateCustomerData(dto, false, out errorMsg))
            {
                return false;
            }

            dto.MembershipTier = CalculateRank(dto.RewardPoints);
            dto.RegistrationDate = DateTime.Now;
            return _repository.InsertCustomer(dto);
        }

        public bool Update(CustomerDTO dto, out string errorMsg)
        {
            if (string.IsNullOrWhiteSpace(dto.Id))
            {
                errorMsg = "Vui lòng chọn khách hàng cần chỉnh sửa từ bảng!";
                return false;
            }

            if (!ValidateCustomerData(dto, true, out errorMsg))
            {
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
                errorMsg = "Vui lòng chọn khách hàng muốn xóa!";
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
        // Tìm khách hàng theo ID
        public CustomerDTO GetById(int id)
        {
            return _repository.GetCustomerById(id);
        }
    }
}