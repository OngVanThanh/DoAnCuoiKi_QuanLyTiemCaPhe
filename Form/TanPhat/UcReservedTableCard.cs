using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class UcReservedTableCard : UserControl
    {
        public TableDTO TableData { get; private set; }
        public event EventHandler<TableDTO> TableSelected;
        public event EventHandler<TableDTO> TableCancelClicked;

        public UcReservedTableCard()
        {
            InitializeComponent();
        }

        public void BindData(TableDTO table, bool isSelected)
        {
            TableData = table;
            if (table == null) return;

            lblTableName.Text = table.TableName;
            lblAreaName.Text = $"Khu vực: {table.AreaName}";
            lblStatusBadge.Text = table.Status;

            if (table.StartTime.HasValue && table.EndTime.HasValue)
            {
                TimeSpan remaining = table.EndTime.Value - DateTime.Now;
                string remainStr = remaining.TotalMinutes > 0
                    ? $"còn {(int)remaining.TotalMinutes}p"
                    : "Sắp hết giờ";
                lblTimeRange.Text = $"⏰ {table.StartTime:HH:mm} - {table.EndTime:HH:mm} ({remainStr})";
            }
            else
            {
                lblTimeRange.Text = "⏰ Đang phục vụ tại bàn";
            }

            if (string.Equals(table.Status, "Có khách", StringComparison.OrdinalIgnoreCase))
            {
                lblStatusBadge.BackColor = Color.FromArgb(230, 126, 34);
            }
            else
            {
                lblStatusBadge.BackColor = Color.FromArgb(52, 152, 219);
            }

            if (isSelected)
            {
                pnlCard.BackColor = Color.FromArgb(245, 234, 220);
                lblTableName.ForeColor = UIHelper.PrimaryCoffee;
            }
            else
            {
                pnlCard.BackColor = Color.White;
                lblTableName.ForeColor = Color.FromArgb(60, 40, 35);
            }
        }

        private void Card_Click(object sender, EventArgs e)
        {
            if (TableData != null)
            {
                TableSelected?.Invoke(this, TableData);
            }
        }

        private void btnCancelTable_Click(object sender, EventArgs e)
        {
            if (TableData != null)
            {
                TableCancelClicked?.Invoke(this, TableData);
            }
        }
    }
}