using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.Utilities
{
    internal class UserSession
    {
        public static User CurrentUser { get; private set; }
        public static bool IsLoggedIn
        {
            get { return CurrentUser != null; }
        }
        public static void Login(User user)
        {
            CurrentUser = user;
        }
        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}
