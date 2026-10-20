using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.BLL
{
    internal class AppConfigService
    {
        public bool TestConnection(string server, string database, bool isIntegratedSecurity, string username, string password)
        {
            string connString;
            if (isIntegratedSecurity)
            {
                connString = $"Data Source={server};Initial Catalog={database};Integrated Security=True;TrustServerCertificate=True;";
            }
            else
            {
                connString = $"Data Source={server};Initial Catalog={database};User ID={username};Password={password};TrustServerCertificate=True;";
            }

            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    connection.Open();
                    return true; // Kết nối thành công
                }
                catch (Exception)
                {
                    return false; // Kết nối thất bại
                }
            }
        }
        public void SaveConfig(string server, string database, bool isIntegratedSecurity, string username, string password, string shopName, string shopAddress, string shopPhone, string vat)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string connString;
            if (isIntegratedSecurity)
            {
                connString = $"Data Source={server};Initial Catalog={database};Integrated Security=True;TrustServerCertificate=True;";
            }
            else
            {
                connString = $"Data Source={server};Initial Catalog={database};User ID={username};Password={password};TrustServerCertificate=True;";
            }

            if (config.ConnectionStrings.ConnectionStrings["CoffeeShopDB"] != null)
            {
                config.ConnectionStrings.ConnectionStrings["CoffeeShopDB"].ConnectionString = connString;
            }
            else
            {
                config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("CoffeeShopDB", connString, "System.Data.SqlClient"));
            }

            // Cập nhật các thông tin khác vào Appsettings
            UpdateAppSettings(config, "ShopName", shopName);
            UpdateAppSettings(config, "ShopAddress", shopAddress);
            UpdateAppSettings(config, "ShopPhone", shopPhone);
            UpdateAppSettings(config, "ShopVAT", vat);

            // Lưu thay đổi vào file cấu hình thực tế
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
            ConfigurationManager.RefreshSection("appSettings");
        }

        // Hàm hỗ trợ cập nhật AppSettings
        private void UpdateAppSettings(Configuration config, string key, string value)
        {
            if (config.AppSettings.Settings[key] != null)
            {
                config.AppSettings.Settings[key].Value = value;
            }
            else
            {
                config.AppSettings.Settings.Add(key, value);
            }
        }
    }
}
