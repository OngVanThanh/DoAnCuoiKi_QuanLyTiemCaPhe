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

        public List<CustomerDTO> GetAll()
        {
            return _repository.GetAllCustomers();
        }

        public CustomerDTO GetByPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return null;
            return _repository.GetByPhone(phone.Trim());
        }

        public string CalculateRank(int points)
        {
            if (points >= 1000) return "Kim Cương";
            if (points >= 500) return "Vàng";
            if (points >= 200) return "Bạc";
            return "Thường";
        }

        public bool Add(CustomerDTO customer, out string error)
        {
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(customer.CustomerName))
            {
                error = "Tên khách hàng không được để trống!";
                return false;
            }
            if (string.IsNullOrWhiteSpace(customer.Phone))
            {
                error = "Số điện thoại không được để trống!";
                return false;
            }
            if (_repository.PhoneExists(customer.Phone))
            {
                error = "Số điện thoại này đã tồn tại trong hệ thống!";
                return false;
            }

            return _repository.AddCustomer(customer, out error);
        }

        public bool Update(CustomerDTO customer, out string error)
        {
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(customer.Phone))
            {
                error = "Số điện thoại không hợp lệ!";
                return false;
            }

            return _repository.UpdateCustomer(customer, out error);
        }

        public bool Delete(string phone, out string error)
        {
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(phone))
            {
                error = "Vui lòng chọn số điện thoại cần xóa!";
                return false;
            }

            return _repository.DeleteCustomer(phone, out error);
        }
    }
}