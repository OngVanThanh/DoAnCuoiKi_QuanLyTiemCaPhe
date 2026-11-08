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

    public partial class UCEmployeeControl : UserControl
    {
        // Khai báo sự kiện khi bấm nút Xem chi tiết và Xóa
        public event EventHandler DetailClicked;
        public event EventHandler DeleteClicked;
        public UCEmployeeControl()
        {
            InitializeComponent();
        }
        private void btnDetail_Click(object sender, EventArgs e)
        {
            DetailClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteClicked?.Invoke(this, EventArgs.Empty);
        }
    }

    }
