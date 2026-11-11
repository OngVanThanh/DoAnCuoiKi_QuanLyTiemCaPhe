using System;
using System.Collections.Generic;
using System.Linq;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DAL
{
    public class CustomerRepository
    {
        private CoffeeShopDataContextDataContext GetContext()
        {
            return new CoffeeShopDataContextDataContext(ConfigHelper.GetConnectionString("CoffeeShopDB"));
        }
        // 1. Lấy toàn bộ danh sách khách hàng
        public List<CustomerDTO> GetAllCustomers()
        {
            using (var db = GetContext())
            {
                return db.Customers
                         .OrderByDescending(c => c.RewardPoints ?? 0)
                         .Select(c => new CustomerDTO
                         {
                             CustomerId = c.Id,
                             CustomerName = c.CustomerName,
                             Phone = c.Phone,
                             RewardPoints = c.RewardPoints ?? 0,
                             MembershipTier = c.MembershipTier ?? "Thường",
                             RegistrationDate = (c.RegistrationDate ?? DateTime.Now).Date
                         })
                         .ToList();
            }
        }

        // 2. Lấy khách hàng theo Số điện thoại (Khóa chính)
        public CustomerDTO GetByPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return null;

            using (var db = GetContext())
            {
                var customer = db.Customers.FirstOrDefault(c => c.Phone == phone.Trim());
                if (customer == null) return null;

                return new CustomerDTO
                {
                    CustomerId = customer.Id,
                    CustomerName = customer.CustomerName,
                    Phone = customer.Phone,
                    RewardPoints = customer.RewardPoints ?? 0,
                    MembershipTier = customer.MembershipTier ?? "Thường",
                    RegistrationDate = (customer.RegistrationDate ?? DateTime.Now).Date
                };
            }
        }

        // 3. Kiểm tra số điện thoại đã tồn tại chưa
        public bool PhoneExists(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;

            using (var db = GetContext())
            {
                return db.Customers.Any(c => c.Phone == phone.Trim());
            }
        }

        // 4. Thêm khách hàng mới với Số điện thoại là khóa duy nhất
        public bool AddCustomer(CustomerDTO dto, out string error)
        {
            error = string.Empty;
            try
            {
                using (var db = GetContext())
                {
                    if (PhoneExists(dto.Phone))
                    {
                        error = "Số điện thoại này đã tồn tại trong hệ thống!";
                        return false;
                    }

                    var newCustomer = new Customer
                    {
                        CustomerName = dto.CustomerName.Trim(),
                        Phone = dto.Phone.Trim(),
                        RewardPoints = dto.RewardPoints,
                        MembershipTier = string.IsNullOrWhiteSpace(dto.MembershipTier) ? "Thường" : dto.MembershipTier,
                        RegistrationDate = dto.RegistrationDate
                    };

                    db.Customers.InsertOnSubmit(newCustomer);
                    db.SubmitChanges();
                    dto.CustomerId = newCustomer.Id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                error = "Lỗi thêm khách hàng: " + ex.Message;
                return false;
            }
        }

        // 5. Cập nhật khách hàng theo Số điện thoại
        public bool UpdateCustomer(CustomerDTO dto, out string error)
        {
            error = string.Empty;
            try
            {
                using (var db = GetContext())
                {
                    var customer = db.Customers.FirstOrDefault(c => c.Phone == dto.Phone.Trim());
                    if (customer == null)
                    {
                        error = "Không tìm thấy khách hàng có số điện thoại này!";
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

        // 6. Xóa khách hàng theo Số điện thoại
        public bool DeleteCustomer(string phone, out string error)
        {
            error = string.Empty;
            try
            {
                using (var db = GetContext())
                {
                    var customer = db.Customers.FirstOrDefault(c => c.Phone == phone.Trim());
                    if (customer == null)
                    {
                        error = "Không tìm thấy khách hàng cần xóa!";
                        return false;
                    }

                    db.Customers.DeleteOnSubmit(customer);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                error = "Lỗi xóa khách hàng: " + ex.Message;
                return false;
            }
        }

        // 7. Tìm kiếm theo Số điện thoại hoặc Họ tên
        public List<CustomerDTO> SearchCustomers(string keyword)
        {
            using (var db = GetContext())
            {
                return db.Customers
                         .Where(c => c.Phone.Contains(keyword) || c.CustomerName.Contains(keyword))
                         .OrderByDescending(c => c.RewardPoints ?? 0)
                         .Select(c => new CustomerDTO
                         {
                             CustomerId = c.Id,
                             CustomerName = c.CustomerName,
                             Phone = c.Phone,
                             RewardPoints = c.RewardPoints ?? 0,
                             MembershipTier = c.MembershipTier ?? "Thường",
                             RegistrationDate = (c.RegistrationDate ?? DateTime.Now).Date
                         })
                         .ToList();
            }
        }

        // 8. Lấy Top khách hàng cho báo cáo RDLC
        public List<CustomerDTO> GetTopCustomersByPoints(int topCount = 10)
        {
            using (var db = GetContext())
            {
                return db.Customers
                         .OrderByDescending(c => c.RewardPoints ?? 0)
                         .Take(topCount)
                         .Select(c => new CustomerDTO
                         {
                             CustomerId = c.Id,
                             CustomerName = c.CustomerName,
                             Phone = c.Phone,
                             RewardPoints = c.RewardPoints ?? 0,
                             MembershipTier = c.MembershipTier ?? "Thường",
                             RegistrationDate = (c.RegistrationDate ?? DateTime.Now).Date
                         })
                         .ToList();
            }
        }

        // 9. Hàm hỗ trợ tìm theo ID số (nếu có module cũ gọi)
        public CustomerDTO GetCustomerById(int id)
        {
            using (var db = GetContext())
            {
                var customer = db.Customers.FirstOrDefault(c => c.Id == id);
                if (customer == null) return null;

                return new CustomerDTO
                {
                    CustomerId = customer.Id,
                    CustomerName = customer.CustomerName,
                    Phone = customer.Phone,
                    RewardPoints = customer.RewardPoints ?? 0,
                    MembershipTier = customer.MembershipTier ?? "Thường",
                    RegistrationDate = (customer.RegistrationDate ?? DateTime.Now).Date
                };
            }
        }

        // 10. Cộng/trừ điểm tích lũy và cập nhật hạng thành viên trực tiếp vào CSDL khi thanh toán / in hóa đơn
        public bool AddRewardPoints(string phone, int pointsToAdd, int pointsToDeduct, out int updatedPoints, out string updatedTier, out string error)
        {
            error = string.Empty;
            updatedPoints = 0;
            updatedTier = "Thường";

            if (string.IsNullOrWhiteSpace(phone))
            {
                error = "Số điện thoại không hợp lệ!";
                return false;
            }

            try
            {
                using (var db = GetContext())
                {
                    var customer = db.Customers.FirstOrDefault(c => c.Phone == phone.Trim());
                    if (customer == null)
                    {
                        error = "Không tìm thấy khách hàng có số điện thoại này!";
                        return false;
                    }

                    int current = customer.RewardPoints ?? 0;
                    int netPoints = Math.Max(0, current - pointsToDeduct + pointsToAdd);
                    customer.RewardPoints = netPoints;

                    // Tính lại hạng thành viên theo thang điểm
                    if (netPoints >= 1000) customer.MembershipTier = "Kim Cương";
                    else if (netPoints >= 500) customer.MembershipTier = "Vàng";
                    else if (netPoints >= 200) customer.MembershipTier = "Bạc";
                    else customer.MembershipTier = "Thường";

                    db.SubmitChanges();

                    updatedPoints = customer.RewardPoints ?? 0;
                    updatedTier = customer.MembershipTier;
                    return true;
                }
            }
            catch (Exception ex)
            {
                error = "Lỗi cập nhật điểm: " + ex.Message;
                return false;
            }
        }
    }
}
