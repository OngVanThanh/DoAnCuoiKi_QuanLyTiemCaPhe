using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Configuration;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.BLL
{
    internal class AppConfigService
    {
        public string GetSetting(string key)
        {
            return ConfigHelper.GetAppSetting(key);
        }

        public string GetConnectionString(string name)
        {
            return ConfigHelper.GetConnectionString(name);
        }

        public bool UpdateAppSetting(string key, string value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(key)) return false;

                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                if (config.AppSettings.Settings[key] == null)
                {
                    config.AppSettings.Settings.Add(key, value ?? "");
                }
                else
                {
                    config.AppSettings.Settings[key].Value = value ?? "";
                }

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi ghi App.config: " + ex.Message);
                return false;
            }
        }

        public bool UpdateConnectionString(string name, string connectionString)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(connectionString))
                    return false;

                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                if (config.ConnectionStrings.ConnectionStrings[name] != null)
                {
                    config.ConnectionStrings.ConnectionStrings[name].ConnectionString = connectionString;
                }
                else
                {
                    config.ConnectionStrings.ConnectionStrings.Add(
                        new ConnectionStringSettings(name, connectionString, "System.Data.SqlClient"));
                }

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");
                ConfigHelper.InvalidateCache();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi ghi ConnectionStrings: " + ex.Message);
                return false;
            }
        }
    }
}
