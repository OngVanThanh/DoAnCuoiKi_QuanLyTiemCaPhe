using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class FrmQuickAddCustomer : Form
    {
        private readonly CustomerService _customerService = new CustomerService();
        private readonly string _initialPhone;

        public CustomerDTO CreatedCustomer { get; private set; }

        public FrmQuickAddCustomer(string initialPhone = "")
        {
            InitializeComponent();
            _initialPhone = initialPhone ?? string.Empty;
        }

        private void FrmQuickAddCustomer_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_initialPhone))
            {
                txtPhone.Text = _initialPhone.Trim();
                txtName.Focus();
            }
            else
            {
                txtPhone.Focus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string phone = txtPhone.Text.Trim();
            string name = txtName.Text.Trim();

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return;
            }

            // Kiểm tra định dạng số điện thoại (tối thiểu 9 số, tối đa 12 số)
            if (!Regex.IsMatch(phone, @"^[0-9]{9,12}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ (phải gồm 9-12 chữ số)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Vui lòng nhập họ và tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            // Kiểm tra xem số điện thoại đã tồn tại chưa
            var existing = _customerService.GetByPhone(phone);
            if (existing != null)
            {
                var ask = MessageBox.Show(
                    $"Số điện thoại này đã đăng ký cho khách hàng '{existing.CustomerName}' (Hạng: {existing.MembershipTier}, Điểm: {existing.RewardPoints}).\nBạn có muốn chọn khách hàng này vào đơn hàng hiện tại không?",
                    "Khách hàng đã tồn tại",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (ask == DialogResult.Yes)
                {
                    CreatedCustomer = existing;
                    DialogResult = DialogResult.OK;
                    Close();
                }
                return;
            }

            var newCustomer = new CustomerDTO
            {
                Phone = phone,
                CustomerName = name,
                RewardPoints = 0,
                MembershipTier = "Thường",
                RegistrationDate = DateTime.Today
            };

            if (_customerService.Add(newCustomer, out string error))
            {
                CreatedCustomer = newCustomer;
                MessageBox.Show($"Thêm thành viên '{name}' thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(error, "Không thể thêm khách hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtName.Focus();
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSave_Click(sender, EventArgs.Empty);
            }
        }
    }
}
