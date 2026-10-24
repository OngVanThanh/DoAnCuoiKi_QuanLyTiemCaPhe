using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
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
            if (string.IsNullOrWhiteSpace(key)) return false;

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (config.AppSettings.Settings[key] == null)
                config.AppSettings.Settings.Add(key, value);
            else
                config.AppSettings.Settings[key].Value = value;

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            return true;

        }
        
    }
}
