using System;
using System.Collections.Generic;
using System.Linq;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DAL
{
    public class CustomerRepository
    {
        // 1. Lấy toàn bộ danh sách khách hàng
        public List<CustomerDTO> GetAllCustomers()
        {
            using (var db = new CoffeeShopDataContextDataContext())
            {
                var query = from c in db.Customers
                            orderby c.RegistrationDate ascending
                            select new CustomerDTO
                            {
                                Id = c.Id.ToString(),
                                CustomerName = c.CustomerName,
                                Phone = c.Phone,
                                RewardPoints = c.RewardPoints ?? 0,
                                MembershipTier = c.MembershipTier,
                                RegistrationDate = (c.RegistrationDate ?? DateTime.Now).Date
                            };
                return query.ToList();
            }
        }

        // 2. Kiểm tra trùng số điện thoại
        public bool IsPhoneExists(string phone, string excludeId = null)
        {
            using (var db = new CoffeeShopDataContextDataContext())
            {
                if (string.IsNullOrEmpty(excludeId))
                {
                    return db.Customers.Any(c => c.Phone == phone);
                }
                // Khi Sửa: Cho phép giữ nguyên SĐT của chính khách hàng đó
                return db.Customers.Any(c => c.Phone == phone && c.Id.ToString() != excludeId);
            }
        }

        // 3. Thêm mới khách hàng
        public bool InsertCustomer(CustomerDTO dto)
        {
            using (var db = new CoffeeShopDataContextDataContext())
            {
                var entity = new Customer
                {
                    Id = int.Parse(dto.Id), // Gán ID từ DTO mà người dùng đã nhập
                    CustomerName = dto.CustomerName,
                    Phone = dto.Phone,
                    RewardPoints = dto.RewardPoints,
                    MembershipTier = string.IsNullOrWhiteSpace(dto.MembershipTier) ? "Thường" : dto.MembershipTier,
                    RegistrationDate = dto.RegistrationDate.Date
                };

                db.Customers.InsertOnSubmit(entity);
                db.SubmitChanges();
                return true;
            }
        }

        // 4. Sửa khách hàng
        public bool UpdateCustomer(CustomerDTO dto)
        {
            using (var db = new CoffeeShopDataContextDataContext())
            {
                var customer = db.Customers.SingleOrDefault(c => c.Id.ToString() == dto.Id);
                if (customer != null)
                {
                    customer.CustomerName = dto.CustomerName;
                    customer.Phone = dto.Phone;
                    customer.RewardPoints = dto.RewardPoints;
                    customer.MembershipTier = dto.MembershipTier;
                    // Không cần cập nhật RegistrationDate để giữ nguyên ngày đăng ký ban đầu

                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
        }

        // 5. Xóa khách hàng
        public bool DeleteCustomer(string id)
        {
            using (var db = new CoffeeShopDataContextDataContext())
            {
                var customer = db.Customers.SingleOrDefault(c => c.Id.ToString() == id);
                if (customer != null)
                {
                    db.Customers.DeleteOnSubmit(customer);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
        }

        // 6. Tìm kiếm theo SĐT hoặc Tên
        public List<CustomerDTO> SearchCustomers(string keyword)
        {
            using (var db = new CoffeeShopDataContextDataContext())
            {
                var query = from c in db.Customers
                            where c.Phone.Contains(keyword) || c.CustomerName.Contains(keyword)
                            orderby c.Id descending
                            select new CustomerDTO
                            {
                                Id = c.Id.ToString(),
                                CustomerName = c.CustomerName,
                                Phone = c.Phone,
                                RewardPoints = c.RewardPoints ?? 0,
                                MembershipTier = c.MembershipTier,
                                RegistrationDate = c.RegistrationDate ?? DateTime.Now
                            };
                return query.ToList();
            }
        }
        // Tìm kiếm chính xác khách hàng theo Id
        public CustomerDTO GetCustomerById(int id)
        {
            using (var db = new CoffeeShopDataContextDataContext()) // Đổi theo tên DataContext của bạn
            {
                var customer = db.Customers.SingleOrDefault(c => c.Id == id);
                if (customer != null)
                {
                    return new CustomerDTO
                    {
                        Id = customer.Id.ToString(),
                        CustomerName = customer.CustomerName,
                        Phone = customer.Phone,
                        RewardPoints = customer.RewardPoints ?? 0,
                        MembershipTier = customer.MembershipTier,
                        RegistrationDate = customer.RegistrationDate ?? DateTime.Now
                    };
                }
                return null;
            }
        }
    }
}