using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
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
    public partial class FrmCustomer : Form
    {
        private readonly CustomerService _customerService = new CustomerService();
        public FrmCustomer()
        {
            InitializeComponent();
            this.Load += FrmCustomer_Load;
        }
        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            LoadCustomerList();
            
        }

        private void LoadCustomerList()
        {
            try
            {
                List<CustomerDTO> list = _customerService.GetAll();

                dgvCustomer.AutoGenerateColumns = false;
                dgvCustomer.DataSource = null; // Xóa nguồn cũ
                dgvCustomer.DataSource = list; // Gán nguồn mới
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị danh sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int points = int.TryParse(txtRewardPoints.Text.Trim(), out int pts) ? pts : 0;

            CustomerDTO newCustomer = new CustomerDTO
            {
                Id = txtCustomerId.Text.Trim(),
                CustomerName = txtCustomerName.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                RewardPoints = points,
                CreatedDate = DateTime.Now
            };

            if (_customerService.Add(newCustomer, out string error))
            {
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCustomerList();
                ClearInputs();
            }
            else
            {
                MessageBox.Show(error, "Lỗi thêm mới", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int points = int.TryParse(txtRewardPoints.Text.Trim(), out int pts) ? pts : 0;

            CustomerDTO customer = new CustomerDTO
            {
                Id = txtCustomerId.Text.Trim(),
                CustomerName = txtCustomerName.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                RewardPoints = points
            };

            if (_customerService.Update(customer, out string error))
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCustomerList();
                ClearInputs();
            }
            else
            {
                MessageBox.Show(error, "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtCustomerId.Text.Trim();

            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Vui lòng chọn khách hàng muốn xóa từ danh sách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa khách hàng có mã [{id}] không?",
                                          "Xác nhận xóa",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                if (_customerService.Delete(id, out string error))
                {
                    MessageBox.Show("Đã xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCustomerList();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show(error, "Lỗi xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtCustomerName.Text.Trim(); // Hoặc lấy từ txtSearch nếu có ô tìm riêng
            dgvCustomer.DataSource = null;
            dgvCustomer.DataSource = _customerService.Search(keyword);
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void txtCustomerId_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtRewardPoints_TextChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCreatedDate_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblMembershipTier_Click(object sender, EventArgs e)
        {

        }

        private void txtMembershipTier_TextChanged(object sender, EventArgs e)
        {

        }
        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCustomer.CurrentRow != null)
            {
                var row = dgvCustomer.CurrentRow;
                txtCustomerId.Text = row.Cells["Id"].Value?.ToString() ?? "";
                txtCustomerName.Text = row.Cells["CustomerName"].Value?.ToString() ?? "";
                txtPhone.Text = row.Cells["Phone"].Value?.ToString() ?? "";
                txtRewardPoints.Text = row.Cells["RewardPoints"].Value?.ToString() ?? "0";
                lblMembershipTier.Text = row.Cells["MembershipTier"].Value?.ToString() ?? "Đồng";

                // Khóa ô Mã khách hàng lại để tránh sửa nhầm Primary Key
                txtCustomerId.ReadOnly = true;
            }
        }
        private void ClearInputs()
        {
            txtCustomerId.ReadOnly = false;
            txtCustomerId.Clear();
            txtCustomerName.Clear();
            txtPhone.Clear();
            txtRewardPoints.Clear();
            lblMembershipTier.Text = "Đồng";
            txtCustomerId.Focus();
        }
    }
}

