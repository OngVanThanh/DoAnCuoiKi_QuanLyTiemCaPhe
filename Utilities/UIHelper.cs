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

        // Lấy thư mục gốc thực thi của phần mềm (hỗ trợ cả WinForms runtime lẫn Unit Test reflection)
        public static string GetApplicationDirectory()
        {
            try
            {
                string asmLocation = typeof(UIHelper).Assembly.Location;
                if (!string.IsNullOrEmpty(asmLocation) && File.Exists(asmLocation))
                {
                    return Path.GetDirectoryName(asmLocation);
                }
            }
            catch { }

            try
            {
                if (!string.IsNullOrEmpty(Application.StartupPath))
                {
                    return Application.StartupPath;
                }
            }
            catch { }

            return AppDomain.CurrentDomain.BaseDirectory;
        }

        // 4. LOAD ẢNH THEO ĐƯỜNG DẪN TƯƠNG ĐỐI (Đúng yêu cầu Giảng viên, không khóa file)
        public static void LoadImageToPictureBox(PictureBox pic, string relativePath)
        {
            if (pic == null) return;

            try
            {
                if (string.IsNullOrWhiteSpace(relativePath))
                {
                    if (pic.Image != null)
                    {
                        var oldImage = pic.Image;
                        pic.Image = null;
                        oldImage.Dispose();
                    }
                    return;
                }

                string fullPath = null;
                string trimmed = relativePath.Trim();
                string cleanRelative = trimmed.TrimStart('\\', '/');
                string appDir = GetApplicationDirectory();

                // 1. Kiểm tra nếu đã là đường dẫn tuyệt đối hợp lệ và file tồn tại
                if (Path.IsPathRooted(trimmed) && File.Exists(trimmed))
                {
                    fullPath = trimmed;
                }
                // 2. Kết hợp với Application Directory (bin\Debug hoặc runtime StartupPath)
                else if (File.Exists(Path.Combine(appDir, cleanRelative)))
                {
                    fullPath = Path.Combine(appDir, cleanRelative);
                }
                // 3. Kết hợp với Application.StartupPath
                else if (File.Exists(Path.Combine(Application.StartupPath, cleanRelative)))
                {
                    fullPath = Path.Combine(Application.StartupPath, cleanRelative);
                }
                // 4. Kết hợp với AppDomain.CurrentDomain.BaseDirectory
                else if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, cleanRelative)))
                {
                    fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, cleanRelative);
                }
                // 5. Kết hợp với Directory.GetCurrentDirectory()
                else if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), cleanRelative)))
                {
                    fullPath = Path.Combine(Directory.GetCurrentDirectory(), cleanRelative);
                }
                // 6. Fallback: Nếu chuỗi chứa "Images", tìm relative từ "Images" trở đi
                else
                {
                    int imgIdx = trimmed.IndexOf("Images", StringComparison.OrdinalIgnoreCase);
                    if (imgIdx >= 0)
                    {
                        string subRelative = trimmed.Substring(imgIdx).TrimStart('\\', '/');
                        string[] candidates = new string[]
                        {
                            Path.Combine(appDir, subRelative),
                            Path.Combine(Application.StartupPath, subRelative),
                            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, subRelative),
                            Path.Combine(Directory.GetCurrentDirectory(), subRelative)
                        };

                        foreach (var cand in candidates)
                        {
                            if (File.Exists(cand))
                            {
                                fullPath = cand;
                                break;
                            }
                        }
                    }
                }

                // Nếu tìm thấy file tồn tại trên đĩa
                if (!string.IsNullOrEmpty(fullPath) && File.Exists(fullPath))
                {
                    // Đọc toàn bộ byte vào MemoryStream rồi tạo bản sao Bitmap độc lập trong RAM
                    // Đảm bảo không bao giờ khóa file trên đĩa (Zero File Lock)
                    byte[] imageBytes = File.ReadAllBytes(fullPath);
                    using (var ms = new MemoryStream(imageBytes))
                    {
                        using (var temp = Image.FromStream(ms))
                        {
                            var oldImage = pic.Image;
                            pic.Image = new Bitmap(temp);
                            oldImage?.Dispose();
                        }
                    }
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    if (pic.Image != null)
                    {
                        var oldImage = pic.Image;
                        pic.Image = null;
                        oldImage.Dispose();
                    }
                }
            }
            catch
            {
                if (pic.Image != null)
                {
                    try
                    {
                        var oldImage = pic.Image;
                        pic.Image = null;
                        oldImage.Dispose();
                    }
                    catch { }
                }
            }
        }

        // 5. ĐỊNH DẠNG TIỀN TỆ VNĐ
        public static string FormatVND(decimal amount)
        {
            return string.Format("{0:N0} đ", amount);
        }
    }
}