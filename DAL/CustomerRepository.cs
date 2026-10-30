using System;
using System.Collections.Generic;
using System.Linq;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DAL
{
    public class CustomerRepository
    {
        public bool IsIdExists(int id)
        {
            using (var db = new CoffeeShopDataContextDataContext()) // Đổi tên DataContext của bạn
            {
                // Trả về true nếu trong DB đang có ID này, false nếu chưa có hoặc đã bị xóa
                return db.Customers.Any(c => c.Id == id);
            }
        }
        // 1. Lấy toàn bộ danh sách khách hàng dùng LINQ
        public List<CustomerDTO> GetAllCustomers()
        {
            using (var db = new CoffeeShopDataContextDataContext()) // Đổi tên DataContext theo project của bạn
            {
                var query = from c in db.Customers
                            select new CustomerDTO
                            {
                                Id = c.Id.ToString(),
                                CustomerName = c.CustomerName,
                                Phone = c.Phone,
                                RewardPoints = c.RewardPoints ?? 0,
                                MembershipTier = c.MembershipTier,
                                
                            };
                return query.ToList();
            }
        }

        // 2. Kiểm tra xem Mã KH hoặc SĐT đã tồn tại chưa bằng LINQ (Any)
        public bool IsCustomerExists(string customerId, string phone)
        {
            using (var db = new CoffeeShopDataContextDataContext())
            {
                return db.Customers.Any(c => c.Id.ToString() == customerId || c.Phone == phone);
            }
        }

        // 3. THÊM khách hàng (Insert)
        public bool InsertCustomer(CustomerDTO dto)
        {
            using (var db = new CoffeeShopDataContextDataContext())
            {
                var entity = new Customer
                {
                    Id = int.Parse(dto.Id),
                    CustomerName = dto.CustomerName,
                    Phone = dto.Phone,
                    RewardPoints = dto.RewardPoints,
                    MembershipTier = dto.MembershipTier,
                    
                };

                db.Customers.InsertOnSubmit(entity); // Nếu dùng EF thì là: db.KhachHangs.Add(entity);
                db.SubmitChanges();                   // Nếu dùng EF thì là: db.SaveChanges();
                return true;
            }
        }

        // 4. SỬA thông tin khách hàng bằng LINQ (SingleOrDefault / FirstOrDefault)
        public bool UpdateCustomer(CustomerDTO dto)
        {
            using (var db = new CoffeeShopDataContextDataContext())
            {
                // Dùng LINQ tìm đúng bản ghi cần sửa theo MaKH
                var customer = db.Customers.SingleOrDefault(c => c.Id.ToString() == dto.Id);
                if (customer != null)
                {
                    customer.CustomerName = dto.CustomerName;
                    customer.Phone = dto.Phone;
                    customer.RewardPoints = dto.RewardPoints;
                    customer.MembershipTier = dto.MembershipTier;

                    db.SubmitChanges(); // Lưu thay đổi xuống SQL Server
                    return true;
                }
                return false;
            }
        }

        // 5. XÓA khách hàng bằng LINQ
        public bool DeleteCustomer(string customerId)
        {
            using (var db = new CoffeeShopDataContextDataContext())
            {
                var customer = db.Customers.SingleOrDefault(c => c.Id.ToString() == customerId);
                if (customer != null)
                {
                    db.Customers.DeleteOnSubmit(customer); // Nếu dùng EF thì là: db.KhachHangs.Remove(customer);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
        }

        // 6. TÌM KIẾM khách hàng theo Tên hoặc Số điện thoại bằng LINQ
        public List<CustomerDTO> SearchCustomers(string keyword)
        {
            using (var db = new CoffeeShopDataContextDataContext())
            {
                var query = from c in db.Customers
                            where c.CustomerName.Contains(keyword) || c.Phone.Contains(keyword) || c.Id.ToString().Contains(keyword)
                            select new CustomerDTO
                            {
                                Id = c.Id.ToString(),
                                CustomerName = c.CustomerName,
                                Phone = c.Phone,
                                RewardPoints = c.RewardPoints ?? 0,
                                MembershipTier = c.MembershipTier,
                                
                            };
                return query.ToList();
            }
        }
    }
}