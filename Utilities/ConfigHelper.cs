using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.Utilities
{
    internal class ConfigHelper
    {
        public static string GetConnectionString(string name)
        {
            var setting = ConfigurationManager.ConnectionStrings[name];

            if (setting == null || string.IsNullOrWhiteSpace(setting.ConnectionString))
            {

                {
                    throw new ConfigurationErrorsException("Không tìm thấy connection string: " + name);

                }
            }
            return setting.ConnectionString;
        }
        
        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
