using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class FrmPromotion : Form
    {
        private readonly PromotionService _promotionService = new PromotionService();
        private bool _isAdding = false;

        public FrmPromotion()
        {
            InitializeComponent();
        }

        private void FrmPromotion_Load(object sender, EventArgs e)
        {
            // Chuẩn hóa DataGridView qua UIHelper
            UIHelper.StyleDataGridView(dgvPromotion);

            // Khóa ô nhập, chỉ mở Thêm/Sửa/Xóa
            UIHelper.SetFormState(grpPromotionInfo, false, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);

            LoadPromotionList();
        }

        private void LoadPromotionList()
        {
            var list = _promotionService.GetAllPromotions();
            dgvPromotion.DataSource = list;

            if (dgvPromotion.Columns["Id"] != null) dgvPromotion.Columns["Id"].HeaderText = "Mã ID";
            if (dgvPromotion.Columns["PromoCode"] != null) dgvPromotion.Columns["PromoCode"].HeaderText = "Mã giảm giá";
            if (dgvPromotion.Columns["DiscountPercent"] != null)
            {
                dgvPromotion.Columns["DiscountPercent"].HeaderText = "% Giảm";
                dgvPromotion.Columns["DiscountPercent"].DefaultCellStyle.Format = "0'%'";
            }
            if (dgvPromotion.Columns["StartDate"] != null)
            {
                dgvPromotion.Columns["StartDate"].HeaderText = "Ngày bắt đầu";
                dgvPromotion.Columns["StartDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dgvPromotion.Columns["EndDate"] != null)
            {
                dgvPromotion.Columns["EndDate"].HeaderText = "Ngày kết thúc";
                dgvPromotion.Columns["EndDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dgvPromotion.Columns["IsActive"] != null) dgvPromotion.Columns["IsActive"].HeaderText = "Kích hoạt";
            if (dgvPromotion.Columns["IsValidNow"] != null) dgvPromotion.Columns["IsValidNow"].HeaderText = "Đang áp dụng";
        }

        private void dgvPromotion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvPromotion.Rows.Count)
            {
                var row = dgvPromotion.Rows[e.RowIndex];
                txtPromoId.Text = row.Cells["Id"].Value?.ToString();
                txtPromoCode.Text = row.Cells["PromoCode"].Value?.ToString();
                txtDiscountPercent.Text = row.Cells["DiscountPercent"].Value?.ToString();

                if (DateTime.TryParse(row.Cells["StartDate"].Value?.ToString(), out DateTime startDate))
                    dtpStartDate.Value = startDate;

                if (DateTime.TryParse(row.Cells["EndDate"].Value?.ToString(), out DateTime endDate))
                    dtpEndDate.Value = endDate;

                chkIsActive.Checked = Convert.ToBoolean(row.Cells["IsActive"].Value ?? true);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _isAdding = true;

            txtPromoId.Text = "";
            txtPromoCode.Text = "";
            txtDiscountPercent.Text = "";
            dtpStartDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now.AddMonths(1);
            chkIsActive.Checked = true;

            UIHelper.SetFormState(grpPromotionInfo, true, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            txtPromoCode.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPromoId.Text))
            {
                MessageBox.Show("Vui lòng chọn một chương trình khuyến mãi để chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _isAdding = false;
            UIHelper.SetFormState(grpPromotionInfo, true, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            txtPromoCode.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            UIHelper.SetFormState(grpPromotionInfo, false, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            if (dgvPromotion.CurrentRow != null)
            {
                dgvPromotion_CellClick(null, new DataGridViewCellEventArgs(0, dgvPromotion.CurrentRow.Index));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            double discount;
            if (!double.TryParse(txtDiscountPercent.Text.Trim(), out discount))
            {
                MessageBox.Show("Phần trăm giảm giá không hợp lệ (phải là số)!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = new PromotionDTO
            {
                Id = _isAdding ? 0 : int.Parse(txtPromoId.Text),
                PromoCode = txtPromoCode.Text,
                DiscountPercent = discount,
                StartDate = dtpStartDate.Value,
                EndDate = dtpEndDate.Value,
                IsActive = chkIsActive.Checked
            };

            string errorMsg;
            bool success = _promotionService.SavePromotion(dto, out errorMsg);

            if (success)
            {
                MessageBox.Show(_isAdding ? "Tạo mã khuyến mãi thành công!" : "Cập nhật khuyến mãi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPromotionList();
                UIHelper.SetFormState(grpPromotionInfo, false, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            }
            else
            {
                MessageBox.Show(errorMsg, "Lỗi kiểm tra dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPromoId.Text))
            {
                MessageBox.Show("Vui lòng chọn khuyến mãi cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int id = int.Parse(txtPromoId.Text);
            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa mã '{txtPromoCode.Text}' không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                string errorMsg;
                bool success = _promotionService.DeletePromotion(id, out errorMsg);
                if (success)
                {
                    MessageBox.Show("Xóa khuyến mãi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPromotionList();
                    txtPromoId.Text = "";
                    txtPromoCode.Text = "";
                    txtDiscountPercent.Text = "";
                }
                else
                {
                    MessageBox.Show(errorMsg, "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}