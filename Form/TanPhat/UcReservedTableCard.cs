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

            string displayName = table.TableName ?? "";
            if (!string.IsNullOrWhiteSpace(displayName) && !displayName.StartsWith("KH: ", StringComparison.OrdinalIgnoreCase))
            {
                displayName = $"KH: {displayName}";
            }

            lblTableName.Text = displayName;
            lblAreaName.Text = $"Khu vực: {table.AreaName}";

            bool isReserved = string.Equals(table.Status, "Đặt trước", StringComparison.OrdinalIgnoreCase);
            bool isOccupied = string.Equals(table.Status, "Có khách", StringComparison.OrdinalIgnoreCase);
            bool isPaid = string.Equals(table.Status, "Đã thanh toán", StringComparison.OrdinalIgnoreCase);

            if (isPaid)
            {
                lblStatusBadge.Text = "Đã thanh toán";
                lblStatusBadge.BackColor = Color.FromArgb(142, 68, 173); // Purple

                if (table.EndTime.HasValue)
                {
                    TimeSpan remaining = table.EndTime.Value - DateTime.Now;
                    string remainStr = remaining.TotalMinutes > 0 ? $"còn {(int)remaining.TotalMinutes}p" : "Hết giờ";
                    lblTimeRange.Text = $"💳 Đã t.toán (Khung {table.EndTime:HH:mm} - {remainStr})";
                }
                else
                {
                    lblTimeRange.Text = "💳 Đã thanh toán (Chờ khách rời)";
                }
                btnCancelTable.Text = "Đóng";
            }
            else if (isReserved && table.StartTime.HasValue && table.EndTime.HasValue)
            {
                lblStatusBadge.Text = "Đặt trước";
                lblStatusBadge.BackColor = Color.FromArgb(52, 152, 219); // Blue
                TimeSpan remaining = table.EndTime.Value - DateTime.Now;
                string remainStr = remaining.TotalMinutes > 0
                    ? $"còn {(int)remaining.TotalMinutes}p"
                    : "Sắp hết giờ";
                lblTimeRange.Text = $"⏰ {table.StartTime:HH:mm} - {table.EndTime:HH:mm} ({remainStr})";
                btnCancelTable.Text = "Hủy";
            }
            else if (isOccupied)
            {
                lblStatusBadge.Text = "Có khách";
                lblStatusBadge.BackColor = Color.FromArgb(230, 126, 34); // Warm orange
                lblTimeRange.Text = table.EndTime.HasValue
                    ? $"☕ Đang phục vụ (Hạn {table.EndTime:HH:mm})"
                    : "☕ Đang phục vụ tại bàn";
                btnCancelTable.Text = "Hủy";
            }
            else
            {
                lblStatusBadge.Text = string.IsNullOrWhiteSpace(table.Status) ? "Trống" : table.Status;
                lblStatusBadge.BackColor = Color.FromArgb(39, 174, 96); // Green
                lblTimeRange.Text = "🟢 Bàn trống (sẵn sàng đón khách)";
                btnCancelTable.Text = "Hủy";
            }

            // Cho phép hủy/đóng mọi thẻ bàn đặt trước (Id > 1)
            btnCancelTable.Visible = (table.Id > 1);

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