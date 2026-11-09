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
                if (dgvCustomer.Columns.Contains("colRegistrationDate"))
                {
                    dgvCustomer.Columns["colRegistrationDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                else if (dgvCustomer.Columns.Count > 5)
                {
                    // Nếu bạn đang dùng vị trí cột (index 5)
                    dgvCustomer.Columns[5].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
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
                MembershipTier = _customerService.CalculateRank(points),
                RegistrationDate = DateTime.Today // Chỉ lấy ngày hiện tại (không có giờ)
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
            string searchKeyword = txtSearch.Text.Trim();

            // 1. Bắt buộc người dùng phải nhập thông tin trước khi tìm
            if (string.IsNullOrWhiteSpace(searchKeyword))
            {
                MessageBox.Show("Vui lòng nhập Mã khách hàng (ID) vào ô tìm kiếm!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                txtSearch.Focus();
                return;
            }

            // 2. Kiểm tra xem người dùng có nhập đúng định dạng số không
            if (!int.TryParse(searchKeyword, out int customerId))
            {
                MessageBox.Show("Mã khách hàng tìm kiếm phải là chữ số!",
                                "Lỗi định dạng",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                txtSearch.SelectAll();
                txtSearch.Focus();
                return;
            }

            // 3. Thực hiện tìm kiếm
            CustomerDTO result = _customerService.GetById(customerId);

            if (result != null)
            {
                // Hiển thị khách hàng tìm thấy lên bảng DataGridView
                dgvCustomer.DataSource = null;
                dgvCustomer.DataSource = new List<CustomerDTO> { result };
            }
            else
            {
                MessageBox.Show($"Không tìm thấy khách hàng nào có Mã ID: {customerId}!",
                                "Kết quả tìm kiếm",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
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
            if (int.TryParse(txtRewardPoints.Text.Trim(), out int points))
            {
                txtMembershipTier.Text = _customerService.CalculateRank(points);
            }
            else
            {
                txtMembershipTier.Text = "Thường";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Enabled = !string.IsNullOrWhiteSpace(txtSearch.Text);
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

                // Cột 0: Mã Khách hàng (Id)
                txtCustomerId.Text = row.Cells[0].Value?.ToString() ?? "";

                // Cột 1: Tên Khách hàng (CustomerName)
                if (row.Cells.Count > 1)
                    txtCustomerName.Text = row.Cells[1].Value?.ToString() ?? "";

                // Cột 2: Số điện thoại (Phone)
                if (row.Cells.Count > 2)
                    txtPhone.Text = row.Cells[2].Value?.ToString() ?? "";

                // Cột 3: Điểm tích lũy (RewardPoints)
                if (row.Cells.Count > 3)
                    txtRewardPoints.Text = row.Cells[3].Value?.ToString() ?? "0";

                // Cột 4: Hạng thành viên (MembershipTier)
                if (row.Cells.Count > 4)
                    txtMembershipTier.Text = row.Cells[4].Value?.ToString() ?? "Bronze";

                // Cột 5: Ngày đăng ký (RegistrationDate)
                if (row.Cells.Count > 5)
                {
                    object regVal = row.Cells[5].Value;
                    if (regVal != null && DateTime.TryParse(regVal.ToString(), out DateTime regDate))
                    {
                        txtRegistrationDate.Text = regDate.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        txtRegistrationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    }
                }

                // Khóa mã ID lại khi xem
                txtCustomerId.ReadOnly = true;
            }
        }
        

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // 1. Tải lại toàn bộ dữ liệu từ Database lên bảng
            LoadCustomerList();

            // 2. Xóa ô tìm kiếm
            txtSearch.Clear();

            // 3. Xóa trắng các ô thông tin nhập liệu
            ClearInputs();

            // 4. Focus lại ô tìm kiếm hoặc ô nhập tên
            txtSearch.Focus();
        }
        private void ClearInputs()
        {
            txtCustomerId.ReadOnly = false;
            txtCustomerId.Clear();
            txtCustomerName.Clear();
            txtPhone.Clear();
            txtRewardPoints.Clear();
            txtMembershipTier.Text = "";
            if (txtRegistrationDate != null) txtRegistrationDate.Clear();
            if (txtSearch != null) txtSearch.Clear();
            txtCustomerId.Focus();
        }

        private void txtRegistrationDate_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

