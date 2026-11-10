using System;
using System.Collections.Generic;
using System.Linq;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DAL
{
    public class CustomerRepository
    {
        // 1. Hàm lấy toàn bộ danh sách khách hàng
        public List<CustomerDTO> GetAllCustomers()
        {
            using (var db = new CoffeeShopDataContextDataContext())
            {
                return db.Customers
                         .OrderByDescending(c => c.RewardPoints ?? 0)
                         .Select(c => new CustomerDTO
                         {
                             CustomerName = c.CustomerName,
                             Phone = c.Phone,
                             RewardPoints = c.RewardPoints ?? 0,
                             MembershipTier = c.MembershipTier ?? "Thường",
                             RegistrationDate = (c.RegistrationDate ?? DateTime.Now).Date
                         })
                         .ToList();
            }
        }

        // 2. Hàm lấy khách hàng theo số điện thoại
        public CustomerDTO GetByPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return null;

            using (var db = new CoffeeShopDataContextDataContext())
            {
                var customer = db.Customers.FirstOrDefault(c => c.Phone == phone.Trim());
                if (customer == null) return null;

                return new CustomerDTO
                {
                    CustomerName = customer.CustomerName,
                    Phone = customer.Phone,
                    RewardPoints = customer.RewardPoints ?? 0,
                    MembershipTier = customer.MembershipTier ?? "Thường",
                    RegistrationDate = (customer.RegistrationDate ?? DateTime.Now).Date
                };
            }
        }

        // 3. Hàm kiểm tra trùng số điện thoại
        public bool PhoneExists(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;

            using (var db = new CoffeeShopDataContextDataContext())
            {
                return db.Customers.Any(c => c.Phone == phone.Trim());
            }
        }

        // 4. Thêm khách hàng mới
        public bool AddCustomer(CustomerDTO dto, out string error)
        {
            error = string.Empty;
            try
            {
                using (var db = new CoffeeShopDataContextDataContext())
                {
                    if (PhoneExists(dto.Phone))
                    {
                        error = "Số điện thoại này đã tồn tại!";
                        return false;
                    }

                    var newCustomer = new Customer
                    {
                        CustomerName = dto.CustomerName.Trim(),
                        Phone = dto.Phone.Trim(),
                        RewardPoints = dto.RewardPoints,
                        MembershipTier = dto.MembershipTier,
                        RegistrationDate = dto.RegistrationDate
                    };

                    db.Customers.InsertOnSubmit(newCustomer);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                error = "Lỗi thêm: " + ex.Message;
                return false;
            }
        }

        // 5. Cập nhật khách hàng theo Phone
        public bool UpdateCustomer(CustomerDTO dto, out string error)
        {
            error = string.Empty;
            try
            {
                using (var db = new CoffeeShopDataContextDataContext())
                {
                    var customer = db.Customers.FirstOrDefault(c => c.Phone == dto.Phone.Trim());
                    if (customer == null)
                    {
                        error = "Không tìm thấy khách hàng!";
                        return false;
                    }

                    customer.CustomerName = dto.CustomerName.Trim();
                    customer.RewardPoints = dto.RewardPoints;
                    customer.MembershipTier = dto.MembershipTier;

                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                error = "Lỗi cập nhật: " + ex.Message;
                return false;
            }
        }

        // 6. Xóa khách hàng theo Phone
        public bool DeleteCustomer(string phone, out string error)
        {
            error = string.Empty;
            try
            {
                using (var db = new CoffeeShopDataContextDataContext())
                {
                    var customer = db.Customers.FirstOrDefault(c => c.Phone == phone.Trim());
                    if (customer == null)
                    {
                        error = "Không tìm thấy khách hàng để xóa!";
                        return false;
                    }

                    db.Customers.DeleteOnSubmit(customer);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                error = "Lỗi xóa: " + ex.Message;
                return false;
            }
        }

        // 7. Lấy Top khách hàng cho báo cáo RDLC
        public List<CustomerDTO> GetTopCustomersByPoints(int topCount = 10)
        {
            using (var db = new CoffeeShopDataContextDataContext())
            {
                return db.Customers
                         .OrderByDescending(c => c.RewardPoints ?? 0)
                         .Take(topCount)
                         .Select(c => new CustomerDTO
                         {
                             CustomerName = c.CustomerName,
                             Phone = c.Phone,
                             RewardPoints = c.RewardPoints ?? 0,
                             MembershipTier = c.MembershipTier ?? "Thường",
                             RegistrationDate = (c.RegistrationDate ?? DateTime.Now).Date
                         })
                         .ToList();
            }
        }
    }
}