namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class FrmCategory
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpCategoryInfo = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.lblCategoryName = new System.Windows.Forms.Label();
            this.txtCategoryId = new System.Windows.Forms.TextBox();
            this.lblCategoryId = new System.Windows.Forms.Label();
            this.dgvCategory = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.grpCategoryInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategory)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(832, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(240, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(352, 31);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "DANH MỤC NHÓM SẢN PHẨM";
            // 
            // grpCategoryInfo
            // 
            this.grpCategoryInfo.Controls.Add(this.btnCancel);
            this.grpCategoryInfo.Controls.Add(this.btnSave);
            this.grpCategoryInfo.Controls.Add(this.btnDelete);
            this.grpCategoryInfo.Controls.Add(this.btnEdit);
            this.grpCategoryInfo.Controls.Add(this.btnAdd);
            this.grpCategoryInfo.Controls.Add(this.txtDescription);
            this.grpCategoryInfo.Controls.Add(this.lblDescription);
            this.grpCategoryInfo.Controls.Add(this.txtCategoryName);
            this.grpCategoryInfo.Controls.Add(this.lblCategoryName);
            this.grpCategoryInfo.Controls.Add(this.txtCategoryId);
            this.grpCategoryInfo.Controls.Add(this.lblCategoryId);
            this.grpCategoryInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.grpCategoryInfo.Location = new System.Drawing.Point(0, 50);
            this.grpCategoryInfo.Name = "grpCategoryInfo";
            this.grpCategoryInfo.Size = new System.Drawing.Size(340, 423);
            this.grpCategoryInfo.TabIndex = 1;
            this.grpCategoryInfo.TabStop = false;
            this.grpCategoryInfo.Text = "Thông tin nhóm món";
            this.grpCategoryInfo.Enter += new System.EventHandler(this.grpCategoryInfo_Enter);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(192, 372);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(74, 372);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(228, 285);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(84)))), ((int)(((byte)(0)))));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(133, 285);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 7;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(38, 285);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtDescription.Location = new System.Drawing.Point(127, 152);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(207, 70);
            this.txtDescription.TabIndex = 5;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(12, 152);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(51, 20);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Mô tả:";
            // 
            // txtCategoryName
            // 
            this.txtCategoryName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCategoryName.Location = new System.Drawing.Point(127, 98);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.ReadOnly = true;
            this.txtCategoryName.Size = new System.Drawing.Size(207, 27);
            this.txtCategoryName.TabIndex = 3;
            // 
            // lblCategoryName
            // 
            this.lblCategoryName.AutoSize = true;
            this.lblCategoryName.Location = new System.Drawing.Point(12, 98);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.Size = new System.Drawing.Size(111, 20);
            this.lblCategoryName.TabIndex = 2;
            this.lblCategoryName.Text = "Tên nhóm món:";
            this.lblCategoryName.Click += new System.EventHandler(this.lblCategoryName_Click);
            // 
            // txtCategoryId
            // 
            this.txtCategoryId.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCategoryId.Location = new System.Drawing.Point(127, 44);
            this.txtCategoryId.Name = "txtCategoryId";
            this.txtCategoryId.ReadOnly = true;
            this.txtCategoryId.Size = new System.Drawing.Size(207, 27);
            this.txtCategoryId.TabIndex = 1;
            // 
            // lblCategoryId
            // 
            this.lblCategoryId.AutoSize = true;
            this.lblCategoryId.Location = new System.Drawing.Point(12, 44);
            this.lblCategoryId.Name = "lblCategoryId";
            this.lblCategoryId.Size = new System.Drawing.Size(109, 20);
            this.lblCategoryId.TabIndex = 0;
            this.lblCategoryId.Text = "Mã nhóm món:";
            // 
            // dgvCategory
            // 
            this.dgvCategory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCategory.Location = new System.Drawing.Point(340, 50);
            this.dgvCategory.Name = "dgvCategory";
            this.dgvCategory.RowHeadersWidth = 51;
            this.dgvCategory.RowTemplate.Height = 24;
            this.dgvCategory.Size = new System.Drawing.Size(492, 423);
            this.dgvCategory.TabIndex = 2;
            this.dgvCategory.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCategory_CellClick);
            this.dgvCategory.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCategory_CellContentClick);
            // 
            // FrmCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 473);
            this.Controls.Add(this.dgvCategory);
            this.Controls.Add(this.grpCategoryInfo);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmCategory";
            this.Text = "Quản lý Nhóm sản phẩm";
            this.Load += new System.EventHandler(this.FrmCategory_Load_1);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.grpCategoryInfo.ResumeLayout(false);
            this.grpCategoryInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpCategoryInfo;
        private System.Windows.Forms.TextBox txtCategoryId;
        private System.Windows.Forms.Label lblCategoryId;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtCategoryName;
        private System.Windows.Forms.Label lblCategoryName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgvCategory;
    }
}