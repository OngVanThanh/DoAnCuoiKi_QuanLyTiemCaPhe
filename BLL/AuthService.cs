using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    internal class AuthService
    {
        private CoffeeShopDataContextDataContext GetContext()
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["CoffeeShopDB"]?.ConnectionString;
            return !string.IsNullOrEmpty(connString) ? new CoffeeShopDataContextDataContext(connString) : new CoffeeShopDataContextDataContext();
        }
        /*        //Hàm mã hóa SHA256
                public static string HashPassword(string password)
                {
                    using (SHA256 sha256 = SHA256.Create())
                    {
                        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                        StringBuilder builder = new StringBuilder();
                        foreach (byte b in bytes)
                        {
                            builder.Append(b.ToString("x2"));
                        }
                        return builder.ToString();
                    }
                }*/

        //Hàm kiểm tra đăng nhập
        public User ValidateUser(string username, string password)
        {
            using (var db = GetContext())
            {
                // So sánh trực tiếp username và password thô từ CSDL
                return db.Users.FirstOrDefault(u => u.Username == username
                                                 && u.PasswordHash == password
                                                 && u.IsActive == true);
            }
        }
    }
}
