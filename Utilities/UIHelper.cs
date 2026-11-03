using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.Utilities
{
    public static class UIHelper
    {
        // 1. BẢNG MÀU CHỦ ĐẠO (Warm Coffee Theme)
        public static readonly Color PrimaryCoffee = Color.FromArgb(74, 44, 42);      // Nâu Espresso đậm (Header/Chính)
        public static readonly Color SecondaryCoffee = Color.FromArgb(111, 78, 55);  // Nâu Cà phê vừa
        public static readonly Color LightCream = Color.FromArgb(250, 247, 242);     // Be kem nhạt (Dòng xen kẽ)
        public static readonly Color SelectedRowColor = Color.FromArgb(141, 110, 99); // Nâu nhạt khi chọn dòng

        public static readonly Color SuccessColor = Color.FromArgb(39, 174, 96);     // Xanh lá (Lưu)
        public static readonly Color DangerColor = Color.FromArgb(192, 57, 43);      // Đỏ gạch (Xóa)
        public static readonly Color WarningColor = Color.FromArgb(211, 84, 0);      // Cam đất (Sửa)
        public static readonly Color GrayColor = Color.FromArgb(127, 140, 141);      // Xám (Hủy)

        // 2. CHUẨN HÓA BẢNG DATAGRIDVIEW
        public static void StyleDataGridView(DataGridView dgv)
        {
            if (dgv == null) return;

            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(235, 230, 225);

            // Cấu hình chọn nguyên dòng và bỏ chọn nhiều dòng
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Chiều cao dòng 32px thoáng đãng, font Segoe UI 9.5pt
            dgv.RowTemplate.Height = 32;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
            dgv.DefaultCellStyle.SelectionBackColor = SelectedRowColor;
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;

            // Dòng xen kẽ (Zebra striping)
            dgv.AlternatingRowsDefaultCellStyle.BackColor = LightCream;

            // Tiêu đề cột (Header) màu Nâu Espresso
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersHeight = 38;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = PrimaryCoffee;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        // 3. ĐIỀU KHIỂN TRẠNG THÁI CRUD (State Machine: Xem vs Sửa/Thêm)
        public static void SetFormState(Control container, bool isEditing, Button btnAdd, Button btnEdit, Button btnDelete, Button btnSave, Button btnCancel)
        {
            // Bật/tắt các ô nhập dữ liệu trong Panel hoặc GroupBox
            foreach (Control ctrl in container.Controls)
            {
                if (ctrl is TextBox txt)
                {
                    // Ô mã ID luôn ReadOnly để DB tự tăng
                    if (!txt.Name.ToLower().Contains("id"))
                    {
                        txt.ReadOnly = !isEditing;
                        txt.BackColor = isEditing ? Color.White : Color.FromArgb(245, 245, 245);
                    }
                }
                else if (ctrl is ComboBox cbo)
                {
                    cbo.Enabled = isEditing;
                }
                else if (ctrl is NumericUpDown nud)
                {
                    nud.Enabled = isEditing;
                }
                else if (ctrl is DateTimePicker dtp)
                {
                    dtp.Enabled = isEditing;
                }
                else if (ctrl is CheckBox chk)
                {
                    chk.Enabled = isEditing;
                }
            }

            // Điều khiển các nút bấm
            if (btnAdd != null) btnAdd.Enabled = !isEditing;
            if (btnEdit != null) btnEdit.Enabled = !isEditing;
            if (btnDelete != null) btnDelete.Enabled = !isEditing;
            if (btnSave != null) btnSave.Enabled = isEditing;
            if (btnCancel != null) btnCancel.Enabled = isEditing;
        }

        // 4. LOAD ẢNH THEO ĐƯỜNG DẪN TƯƠNG ĐỐI (Đúng yêu cầu Giảng viên)
        public static void LoadImageToPictureBox(PictureBox pic, string relativePath)
        {
            if (pic == null) return;

            try
            {
                if (string.IsNullOrWhiteSpace(relativePath))
                {
                    pic.Image = null;
                    return;
                }

                // Kết hợp đường dẫn tương đối với thư mục chạy của phần mềm
                string fullPath = Path.Combine(Application.StartupPath, relativePath);

                if (File.Exists(fullPath))
                {
                    // Dùng MemoryStream để không bị khóa file (File Lock)
                    using (var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                    {
                        pic.Image = Image.FromStream(fs);
                    }
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    pic.Image = null;
                }
            }
            catch
            {
                pic.Image = null;
            }
        }

        // 5. ĐỊNH DẠNG TIỀN TỆ VNĐ
        public static string FormatVND(decimal amount)
        {
            return string.Format("{0:N0} đ", amount);
        }
    }
}