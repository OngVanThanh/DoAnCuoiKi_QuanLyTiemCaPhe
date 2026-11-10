using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using System;
using System.Collections.Generic;
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
            dgvCustomer.AutoGenerateColumns = false;
            LoadCustomerList();
            txtRegistrationDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
        }

        private void LoadCustomerList()
        {
            try
            {
                List<CustomerDTO> list = _customerService.GetAll();
                dgvCustomer.AutoGenerateColumns = false;
                dgvCustomer.DataSource = null;
                dgvCustomer.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị danh sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string phone = txtPhone.Text.Trim();
            string name = txtCustomerName.Text.Trim();

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Vui lòng nhập Số điện thoại khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Vui lòng nhập Tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerName.Focus();
                return;
            }

            int points = 0;
            if (!string.IsNullOrWhiteSpace(txtRewardPoints.Text))
            {
                if (!int.TryParse(txtRewardPoints.Text.Trim(), out points) || points < 0)
                {
                    MessageBox.Show("Điểm tích lũy không hợp lệ (phải là số nguyên không âm >= 0)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRewardPoints.Focus();
                    return;
                }
            }

            CustomerDTO newCustomer = new CustomerDTO
            {
                Phone = phone,
                CustomerName = name,
                RewardPoints = points,
                MembershipTier = _customerService.CalculateRank(points),
                RegistrationDate = DateTime.Today
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
            string phone = txtPhone.Text.Trim();
            string name = txtCustomerName.Text.Trim();

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Vui lòng chọn khách hàng từ danh sách để sửa thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Tên khách hàng không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerName.Focus();
                return;
            }

            int points = 0;
            if (!string.IsNullOrWhiteSpace(txtRewardPoints.Text))
            {
                if (!int.TryParse(txtRewardPoints.Text.Trim(), out points) || points < 0)
                {
                    MessageBox.Show("Điểm tích lũy không hợp lệ (phải là số nguyên không âm >= 0)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRewardPoints.Focus();
                    return;
                }
            }

            CustomerDTO customer = new CustomerDTO
            {
                Phone = phone,
                CustomerName = name,
                RewardPoints = points,
                MembershipTier = _customerService.CalculateRank(points)
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
            string phone = txtPhone.Text.Trim();

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Vui lòng chọn khách hàng muốn xóa từ danh sách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa khách hàng có số điện thoại [{phone}] không?",
                                          "Xác nhận xóa",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                if (_customerService.Delete(phone, out string error))
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
            string searchKeyword = txtSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchKeyword))
            {
                MessageBox.Show("Vui lòng nhập Số điện thoại hoặc Họ tên để tìm kiếm!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                txtSearch.Focus();
                return;
            }

            var results = _customerService.Search(searchKeyword);
            dgvCustomer.AutoGenerateColumns = false;
            dgvCustomer.DataSource = null;
            dgvCustomer.DataSource = results;

            if (results == null || results.Count == 0)
            {
                MessageBox.Show($"Không tìm thấy khách hàng nào khớp với: \"{searchKeyword}\"!",
                                "Kết quả tìm kiếm",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCustomer.CurrentRow != null)
            {
                var row = dgvCustomer.CurrentRow;

                if (row.DataBoundItem is CustomerDTO dto)
                {
                    txtPhone.Text = dto.Phone ?? "";
                    txtCustomerName.Text = dto.CustomerName ?? "";
                    txtRewardPoints.Text = dto.RewardPoints.ToString();
                    txtMembershipTier.Text = dto.MembershipTier ?? "Thường";
                    txtRegistrationDate.Text = dto.RegistrationDate.ToString("dd/MM/yyyy");
                }
                else
                {
                    txtPhone.Text = row.Cells["Phone"].Value?.ToString() ?? "";
                    txtCustomerName.Text = row.Cells["CustomerName"].Value?.ToString() ?? "";
                    txtRewardPoints.Text = row.Cells["RewardPoints"].Value?.ToString() ?? "0";
                    txtMembershipTier.Text = row.Cells["MembershipTier"].Value?.ToString() ?? "Thường";

                    object regVal = row.Cells["RegistrationDate"].Value;
                    if (regVal != null && DateTime.TryParse(regVal.ToString(), out DateTime regDate))
                    {
                        txtRegistrationDate.Text = regDate.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        txtRegistrationDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    }
                }

                txtPhone.ReadOnly = true; // Khóa trường SĐT khi chọn khách hàng đã có sẵn
            }
        }

        private void txtRewardPoints_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtRewardPoints.Text.Trim(), out int points))
            {
                txtMembershipTier.Text = _customerService.CalculateRank(points);
            }
            else
            {
                txtMembershipTier.Text = "Thường";
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                LoadCustomerList();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCustomerList();
            ClearInputs();
        }

        private void ClearInputs()
        {
            txtPhone.ReadOnly = false;
            txtPhone.Clear();
            txtCustomerName.Clear();
            txtRewardPoints.Clear();
            txtMembershipTier.Text = "Thường";
            txtRegistrationDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtSearch.Clear();
            txtPhone.Focus();
        }
    }
}
