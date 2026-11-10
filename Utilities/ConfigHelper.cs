using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Data.SqlClient;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.Utilities
{
    internal class ConfigHelper
    {
        private static readonly ConcurrentDictionary<string, string> _workingConnectionStrings = new ConcurrentDictionary<string, string>();
        private static readonly object _lockObj = new object();

        /// <summary>
        /// Lấy chuỗi kết nối từ App.config kèm cơ chế tự động dò tìm máy chủ SQL cục bộ nếu cấu hình gốc không kết nối được
        /// </summary>
        public static string GetConnectionString(string name)
        {
            if (_workingConnectionStrings.TryGetValue(name, out string cachedConn))
            {
                return cachedConn;
            }

            lock (_lockObj)
            {
                if (_workingConnectionStrings.TryGetValue(name, out cachedConn))
                {
                    return cachedConn;
                }

                var setting = ConfigurationManager.ConnectionStrings[name];
                string baseConnString = setting != null ? setting.ConnectionString : string.Empty;

                if (string.IsNullOrWhiteSpace(baseConnString))
                {
                    // Fallback mặc định
                    baseConnString = "Data Source=.;Initial Catalog=CoffeeShopDB;Integrated Security=True;TrustServerCertificate=True;";
                }

                // 1. Thử chuỗi cấu hình hiện tại trước
                if (TestConnectionFast(baseConnString))
                {
                    _workingConnectionStrings[name] = baseConnString;
                    return baseConnString;
                }

                // 2. Nếu cấu hình hiện tại không chạy được (ví dụ đổi máy tính), tự dò các instance phổ biến
                string[] candidateSources = new string[] { ".", "localhost", @".\SQLEXPRESS", @"(localdb)\MSSQLLocalDB" };
                foreach (var source in candidateSources)
                {
                    try
                    {
                        var builder = new SqlConnectionStringBuilder(baseConnString)
                        {
                            DataSource = source,
                            ConnectTimeout = 1
                        };
                        string candidateConn = builder.ConnectionString;
                        if (TestConnectionFast(candidateConn))
                        {
                            _workingConnectionStrings[name] = candidateConn;
                            return candidateConn;
                        }
                    }
                    catch
                    {
                        // Bỏ qua và thử nguồn kế tiếp
                    }
                }

                // Nếu tất cả đều không mở được ngay, vẫn trả về chuỗi cấu hình để các tầng trên bắt lỗi rõ ràng
                _workingConnectionStrings[name] = baseConnString;
                return baseConnString;
            }
        }

        private static bool TestConnectionFast(string connectionString)
        {
            try
            {
                var builder = new SqlConnectionStringBuilder(connectionString)
                {
                    ConnectTimeout = 1 // Timeout cực ngắn 1s để không làm chậm ứng dụng
                };
                using (var conn = new SqlConnection(builder.ConnectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Xóa cache chuỗi kết nối (dùng khi người dùng vừa đổi thông tin trên FrmAppConfig)
        /// </summary>
        public static void InvalidateCache()
        {
            _workingConnectionStrings.Clear();
        }

        public static string GetAppSetting(string key, string defaultValue = "")
        {
            string val = ConfigurationManager.AppSettings[key];
            return !string.IsNullOrWhiteSpace(val) ? val : defaultValue;
        }

        public static string GetShopName() => GetAppSetting("ShopName", "Mộc Cafe POS");
        public static string GetShopSlogan() => GetAppSetting("ShopSlogan", "Hệ thống Quản lý Tiệm Cà phê");
        public static string GetShopAddress() => GetAppSetting("ShopAddress", "123 Nguyễn Huệ, Q.1, TP.HCM");
        public static string GetShopPhone() => GetAppSetting("ShopPhone", "0909 123 456");
        public static decimal GetVATRate()
        {
            string val = GetAppSetting("VATRate", GetAppSetting("VATPercent", "8"));
            return decimal.TryParse(val, out decimal rate) ? rate : 8m;
        }
        public static string GetWifiInfo() => GetAppSetting("WifiInfo", "MocCafe_Free | Pass: moccafe2026");
    }
}
