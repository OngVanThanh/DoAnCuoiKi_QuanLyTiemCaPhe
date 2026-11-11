using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Tự động dò tìm SQL Server/LocalDB, tạo CSDL CoffeeShopDB và nạp toàn bộ 15 bảng + dữ liệu mẫu (Zero-Config Setup)
            try
            {
                Utilities.DatabaseMigrationHelper.AutoInitializeDatabase();
            }
            catch { }

            Application.Run(new FrmLogin());
        }
    }
}
