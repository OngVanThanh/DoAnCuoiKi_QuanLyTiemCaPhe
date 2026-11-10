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
            dgvPromotion.AutoGenerateColumns = false;
            UIHelper.SetFormState(grpPromotionInfo, false, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            LoadPromotionList();
        }

        private void LoadPromotionList()
        {
            dgvPromotion.AutoGenerateColumns = false;
            dgvPromotion.DataSource = null;
            dgvPromotion.DataSource = _promotionService.GetAllPromotions();
        }

        private void dgvPromotion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvPromotion.Rows.Count)
            {
                var row = dgvPromotion.Rows[e.RowIndex];
                txtPromoId.Text = row.Cells["colPromoId"].Value?.ToString();
                txtPromoCode.Text = row.Cells["colPromoCode"].Value?.ToString();
                txtDiscountPercent.Text = row.Cells["colDiscountPercent"].Value?.ToString();

                if (DateTime.TryParse(row.Cells["colStartDate"].Value?.ToString(), out DateTime startDate))
                    dtpStartDate.Value = startDate;

                if (DateTime.TryParse(row.Cells["colEndDate"].Value?.ToString(), out DateTime endDate))
                    dtpEndDate.Value = endDate;

                chkIsActive.Checked = Convert.ToBoolean(row.Cells["colIsActive"].Value ?? true);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _isAdding = true;

            txtPromoId.Clear();
            txtPromoCode.Clear();
            txtDiscountPercent.Clear();
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
            if (!double.TryParse(txtDiscountPercent.Text.Trim(), out double discount))
            {
                MessageBox.Show("Phần trăm giảm giá không hợp lệ (phải là số)!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int promoId = 0;
            if (!_isAdding && !string.IsNullOrEmpty(txtPromoId.Text))
            {
                if (!int.TryParse(txtPromoId.Text, out promoId))
                {
                    MessageBox.Show("Mã khuyến mãi không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            var dto = new PromotionDTO
            {
                Id = promoId,
                PromoCode = txtPromoCode.Text,
                DiscountPercent = discount,
                StartDate = dtpStartDate.Value,
                EndDate = dtpEndDate.Value,
                IsActive = chkIsActive.Checked
            };

            if (_promotionService.SavePromotion(dto, out string errorMsg))
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

            if (!int.TryParse(txtPromoId.Text, out int id))
            {
                MessageBox.Show("Mã khuyến mãi không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa mã '{txtPromoCode.Text}' không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                if (_promotionService.DeletePromotion(id, out string errorMsg))
                {
                    MessageBox.Show("Xóa khuyến mãi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPromotionList();
                    txtPromoId.Clear();
                    txtPromoCode.Clear();
                    txtDiscountPercent.Clear();
                }
                else
                {
                    MessageBox.Show(errorMsg, "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}