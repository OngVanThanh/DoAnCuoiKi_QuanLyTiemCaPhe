namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class FrmCustomer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvCustomer = new System.Windows.Forms.DataGridView();
            this.coffeeShopDBDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.coffeeShopDBDataSet = new DoAnCuoiKi_QuanLyTiemCaPhe.CoffeeShopDBDataSet();
            this.txtCustomerId = new System.Windows.Forms.TextBox();
            this.txtRewardPoints = new System.Windows.Forms.TextBox();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblMembershipTier = new System.Windows.Forms.Label();
            this.pnlAddData = new System.Windows.Forms.Panel();
            this.txtMembershipTier = new System.Windows.Forms.TextBox();
            this.lblCreatedDate = new System.Windows.Forms.Label();
            this.txtCreatedDate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.coffeeShopDBDataSetBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RewardPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MembershipTier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coffeeShopDBDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coffeeShopDBDataSet)).BeginInit();
            this.pnlAddData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.coffeeShopDBDataSetBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCustomer
            // 
            this.dgvCustomer.AllowUserToAddRows = false;
            this.dgvCustomer.AllowUserToDeleteRows = false;
            this.dgvCustomer.AutoGenerateColumns = false;
            this.dgvCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.CustomerName,
            this.Phone,
            this.RewardPoints,
            this.MembershipTier});
            this.dgvCustomer.DataSource = this.coffeeShopDBDataSetBindingSource1;
            this.dgvCustomer.Location = new System.Drawing.Point(365, 30);
            this.dgvCustomer.Name = "dgvCustomer";
            this.dgvCustomer.ReadOnly = true;
            this.dgvCustomer.Size = new System.Drawing.Size(438, 271);
            this.dgvCustomer.TabIndex = 0;
            this.dgvCustomer.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustomer_CellClick);
            this.dgvCustomer.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustomer_CellContentClick);
            // 
            // coffeeShopDBDataSetBindingSource
            // 
            this.coffeeShopDBDataSetBindingSource.DataSource = this.coffeeShopDBDataSet;
            this.coffeeShopDBDataSetBindingSource.Position = 0;
            // 
            // coffeeShopDBDataSet
            // 
            this.coffeeShopDBDataSet.DataSetName = "CoffeeShopDBDataSet";
            this.coffeeShopDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // txtCustomerId
            // 
            this.txtCustomerId.Location = new System.Drawing.Point(95, 3);
            this.txtCustomerId.Name = "txtCustomerId";
            this.txtCustomerId.Size = new System.Drawing.Size(100, 20);
            this.txtCustomerId.TabIndex = 1;
            this.txtCustomerId.TextChanged += new System.EventHandler(this.txtCustomerId_TextChanged);
            // 
            // txtRewardPoints
            // 
            this.txtRewardPoints.Location = new System.Drawing.Point(95, 81);
            this.txtRewardPoints.Name = "txtRewardPoints";
            this.txtRewardPoints.Size = new System.Drawing.Size(100, 20);
            this.txtRewardPoints.TabIndex = 2;
            this.txtRewardPoints.TextChanged += new System.EventHandler(this.txtRewardPoints_TextChanged);
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(95, 29);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(100, 20);
            this.txtCustomerName.TabIndex = 3;
            this.txtCustomerName.TextChanged += new System.EventHandler(this.txtCustomerName_TextChanged);
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(95, 55);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(100, 20);
            this.txtPhone.TabIndex = 4;
            this.txtPhone.TextChanged += new System.EventHandler(this.txtPhone_TextChanged);
            // 
            // lblMembershipTier
            // 
            this.lblMembershipTier.AutoSize = true;
            this.lblMembershipTier.Location = new System.Drawing.Point(3, 107);
            this.lblMembershipTier.Name = "lblMembershipTier";
            this.lblMembershipTier.Size = new System.Drawing.Size(56, 13);
            this.lblMembershipTier.TabIndex = 5;
            this.lblMembershipTier.Text = "Xếp hạng:";
            this.lblMembershipTier.Click += new System.EventHandler(this.lblMembershipTier_Click);
            // 
            // pnlAddData
            // 
            this.pnlAddData.Controls.Add(this.txtMembershipTier);
            this.pnlAddData.Controls.Add(this.lblCreatedDate);
            this.pnlAddData.Controls.Add(this.txtCreatedDate);
            this.pnlAddData.Controls.Add(this.label4);
            this.pnlAddData.Controls.Add(this.label3);
            this.pnlAddData.Controls.Add(this.label2);
            this.pnlAddData.Controls.Add(this.label1);
            this.pnlAddData.Controls.Add(this.btnDelete);
            this.pnlAddData.Controls.Add(this.btnEdit);
            this.pnlAddData.Controls.Add(this.btnAdd);
            this.pnlAddData.Controls.Add(this.txtCustomerId);
            this.pnlAddData.Controls.Add(this.lblMembershipTier);
            this.pnlAddData.Controls.Add(this.txtRewardPoints);
            this.pnlAddData.Controls.Add(this.txtPhone);
            this.pnlAddData.Controls.Add(this.txtCustomerName);
            this.pnlAddData.Location = new System.Drawing.Point(0, 1);
            this.pnlAddData.Name = "pnlAddData";
            this.pnlAddData.Size = new System.Drawing.Size(359, 199);
            this.pnlAddData.TabIndex = 6;
            // 
            // txtMembershipTier
            // 
            this.txtMembershipTier.Location = new System.Drawing.Point(95, 107);
            this.txtMembershipTier.Name = "txtMembershipTier";
            this.txtMembershipTier.Size = new System.Drawing.Size(100, 20);
            this.txtMembershipTier.TabIndex = 15;
            this.txtMembershipTier.TextChanged += new System.EventHandler(this.txtMembershipTier_TextChanged);
            // 
            // lblCreatedDate
            // 
            this.lblCreatedDate.AutoSize = true;
            this.lblCreatedDate.Location = new System.Drawing.Point(3, 133);
            this.lblCreatedDate.Name = "lblCreatedDate";
            this.lblCreatedDate.Size = new System.Drawing.Size(77, 13);
            this.lblCreatedDate.TabIndex = 14;
            this.lblCreatedDate.Text = "Ngày đăng ký:";
            // 
            // txtCreatedDate
            // 
            this.txtCreatedDate.Location = new System.Drawing.Point(95, 133);
            this.txtCreatedDate.Name = "txtCreatedDate";
            this.txtCreatedDate.Size = new System.Drawing.Size(100, 20);
            this.txtCreatedDate.TabIndex = 13;
            this.txtCreatedDate.TextChanged += new System.EventHandler(this.txtCreatedDate_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Số điện thoại:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Điểm tích lũy:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Họ tên:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Mã Khách hàng:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(166, 173);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(85, 173);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 7;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(4, 173);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(530, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(365, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(159, 20);
            this.txtSearch.TabIndex = 13;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // coffeeShopDBDataSetBindingSource1
            // 
            this.coffeeShopDBDataSetBindingSource1.DataSource = this.coffeeShopDBDataSet;
            this.coffeeShopDBDataSetBindingSource1.Position = 0;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Mã Khách hàng";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // CustomerName
            // 
            this.CustomerName.DataPropertyName = "CustomerName";
            this.CustomerName.HeaderText = "Họ tên";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            // 
            // Phone
            // 
            this.Phone.DataPropertyName = "Phone";
            this.Phone.HeaderText = "Số điện thoại";
            this.Phone.Name = "Phone";
            this.Phone.ReadOnly = true;
            // 
            // RewardPoints
            // 
            this.RewardPoints.DataPropertyName = "RewardPoints";
            this.RewardPoints.HeaderText = "Điểm tích lũy";
            this.RewardPoints.Name = "RewardPoints";
            this.RewardPoints.ReadOnly = true;
            // 
            // MembershipTier
            // 
            this.MembershipTier.DataPropertyName = "MembershipTier";
            this.MembershipTier.HeaderText = "Xếp hạng";
            this.MembershipTier.Name = "MembershipTier";
            this.MembershipTier.ReadOnly = true;
            // 
            // FrmCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.pnlAddData);
            this.Controls.Add(this.dgvCustomer);
            this.Name = "FrmCustomer";
            this.Text = "FrmCustomer";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coffeeShopDBDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coffeeShopDBDataSet)).EndInit();
            this.pnlAddData.ResumeLayout(false);
            this.pnlAddData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.coffeeShopDBDataSetBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCustomer;
        private System.Windows.Forms.TextBox txtCustomerId;
        private System.Windows.Forms.TextBox txtRewardPoints;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblMembershipTier;
        private System.Windows.Forms.Panel pnlAddData;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblCreatedDate;
        private System.Windows.Forms.TextBox txtCreatedDate;
        private System.Windows.Forms.BindingSource coffeeShopDBDataSetBindingSource;
        private CoffeeShopDBDataSet coffeeShopDBDataSet;
        private System.Windows.Forms.TextBox txtMembershipTier;
        private System.Windows.Forms.BindingSource coffeeShopDBDataSetBindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn RewardPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn MembershipTier;
    }
}