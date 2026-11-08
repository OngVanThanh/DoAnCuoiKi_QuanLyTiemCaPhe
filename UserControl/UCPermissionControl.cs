using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class UCPermissionControl : UserControl
    {
        public event EventHandler ChangePasswordClicked;
        public event EventHandler DeleteClicked;
        public UCPermissionControl()
        {
            InitializeComponent();
        }
        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            ChangePasswordClicked.Invoke(this, EventArgs.Empty);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
