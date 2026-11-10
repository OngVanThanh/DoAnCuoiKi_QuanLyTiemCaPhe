namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class FrmPromotion
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dgvAltRowStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle percentStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle startDateStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle endDateStyle = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpPromotionInfo = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDiscountPercent = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPromoCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPromoId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvPromotion = new System.Windows.Forms.DataGridView();
            this.colPromoId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPromoCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiscountPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEndDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colIsValidNow = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pnlHeader.SuspendLayout();
            this.grpPromotionInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPromotion)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1020, 54);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1020, 54);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "QUẢN LÝ CHƯƠNG TRÌNH KHUYẾN MÃI & GIẢM GIÁ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.UseMnemonic = false;
            // 
            // grpPromotionInfo
            // 
            this.grpPromotionInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(242)))));
            this.grpPromotionInfo.Controls.Add(this.btnCancel);
            this.grpPromotionInfo.Controls.Add(this.btnSave);
            this.grpPromotionInfo.Controls.Add(this.btnDelete);
            this.grpPromotionInfo.Controls.Add(this.btnEdit);
            this.grpPromotionInfo.Controls.Add(this.btnAdd);
            this.grpPromotionInfo.Controls.Add(this.chkIsActive);
            this.grpPromotionInfo.Controls.Add(this.dtpEndDate);
            this.grpPromotionInfo.Controls.Add(this.dtpStartDate);
            this.grpPromotionInfo.Controls.Add(this.label5);
            this.grpPromotionInfo.Controls.Add(this.label4);
            this.grpPromotionInfo.Controls.Add(this.txtDiscountPercent);
            this.grpPromotionInfo.Controls.Add(this.label3);
            this.grpPromotionInfo.Controls.Add(this.txtPromoCode);
            this.grpPromotionInfo.Controls.Add(this.label2);
            this.grpPromotionInfo.Controls.Add(this.txtPromoId);
            this.grpPromotionInfo.Controls.Add(this.label1);
            this.grpPromotionInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.grpPromotionInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.grpPromotionInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.grpPromotionInfo.Location = new System.Drawing.Point(0, 54);
            this.grpPromotionInfo.Name = "grpPromotionInfo";
            this.grpPromotionInfo.Size = new System.Drawing.Size(380, 526);
            this.grpPromotionInfo.TabIndex = 1;
            this.grpPromotionInfo.TabStop = false;
            this.grpPromotionInfo.Text = "Thông tin mã khuyến mãi";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(20, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã ID:";
            // 
            // txtPromoId
            // 
            this.txtPromoId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.txtPromoId.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.txtPromoId.Location = new System.Drawing.Point(150, 37);
            this.txtPromoId.Name = "txtPromoId";
            this.txtPromoId.ReadOnly = true;
            this.txtPromoId.Size = new System.Drawing.Size(205, 29);
            this.txtPromoId.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(20, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Mã giảm giá (*):";
            // 
            // txtPromoCode
            // 
            this.txtPromoCode.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.txtPromoCode.Location = new System.Drawing.Point(150, 81);
            this.txtPromoCode.Name = "txtPromoCode";
            this.txtPromoCode.Size = new System.Drawing.Size(205, 29);
            this.txtPromoCode.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(20, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "% Giảm giá (*):";
            // 
            // txtDiscountPercent
            // 
            this.txtDiscountPercent.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.txtDiscountPercent.Location = new System.Drawing.Point(150, 125);
            this.txtDiscountPercent.Name = "txtDiscountPercent";
            this.txtDiscountPercent.Size = new System.Drawing.Size(205, 29);
            this.txtDiscountPercent.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(20, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "Ngày bắt đầu:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "dd/MM/yyyy";
            this.dtpStartDate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(150, 169);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(205, 29);
            this.dtpStartDate.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(20, 216);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 21);
            this.label5.TabIndex = 8;
            this.label5.Text = "Ngày kết thúc:";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "dd/MM/yyyy";
            this.dtpEndDate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(150, 213);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(205, 29);
            this.dtpEndDate.TabIndex = 9;
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Checked = true;
            this.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsActive.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.chkIsActive.Location = new System.Drawing.Point(150, 258);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(166, 25);
            this.chkIsActive.TabIndex = 10;
            this.chkIsActive.Text = "Kích hoạt áp dụng";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(20, 315);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(102, 38);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "+ Thêm";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(84)))), ((int)(((byte)(0)))));
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(136, 315);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(102, 38);
            this.btnEdit.TabIndex = 12;
            this.btnEdit.Text = "✎ Sửa";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(253, 315);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(102, 38);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Text = "✖ Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(70, 368);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 38);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "💾 Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(195, 368);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 38);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "↺ Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dgvPromotion
            // 
            this.dgvPromotion.AllowUserToAddRows = false;
            this.dgvPromotion.AllowUserToDeleteRows = false;
            this.dgvPromotion.AllowUserToResizeRows = false;
            dgvAltRowStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(242)))));
            this.dgvPromotion.AlternatingRowsDefaultCellStyle = dgvAltRowStyle;
            this.dgvPromotion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPromotion.BackgroundColor = System.Drawing.Color.White;
            this.dgvPromotion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPromotion.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPromotion.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dgvHeaderStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvHeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            dgvHeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dgvHeaderStyle.ForeColor = System.Drawing.Color.White;
            dgvHeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            dgvHeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvHeaderStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPromotion.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            this.dgvPromotion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPromotion.ColumnHeadersHeight = 40;
            this.dgvPromotion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPromoId,
            this.colPromoCode,
            this.colDiscountPercent,
            this.colStartDate,
            this.colEndDate,
            this.colIsActive,
            this.colIsValidNow});
            dgvCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvCellStyle.BackColor = System.Drawing.Color.White;
            dgvCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            dgvCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dgvCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(224)))), ((int)(((byte)(210)))));
            dgvCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.dgvPromotion.DefaultCellStyle = dgvCellStyle;
            this.dgvPromotion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPromotion.EnableHeadersVisualStyles = false;
            this.dgvPromotion.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(230)))), ((int)(((byte)(225)))));
            this.dgvPromotion.Location = new System.Drawing.Point(380, 54);
            this.dgvPromotion.MultiSelect = false;
            this.dgvPromotion.Name = "dgvPromotion";
            this.dgvPromotion.ReadOnly = true;
            this.dgvPromotion.RowHeadersVisible = false;
            this.dgvPromotion.RowTemplate.Height = 34;
            this.dgvPromotion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPromotion.Size = new System.Drawing.Size(640, 526);
            this.dgvPromotion.TabIndex = 2;
            this.dgvPromotion.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPromotion_CellClick);
            // 
            // colPromoId
            // 
            this.colPromoId.DataPropertyName = "Id";
            this.colPromoId.FillWeight = 12F;
            this.colPromoId.HeaderText = "Mã ID";
            this.colPromoId.MinimumWidth = 6;
            this.colPromoId.Name = "colPromoId";
            this.colPromoId.ReadOnly = true;
            // 
            // colPromoCode
            // 
            this.colPromoCode.DataPropertyName = "PromoCode";
            this.colPromoCode.FillWeight = 24F;
            this.colPromoCode.HeaderText = "Mã giảm giá";
            this.colPromoCode.MinimumWidth = 6;
            this.colPromoCode.Name = "colPromoCode";
            this.colPromoCode.ReadOnly = true;
            // 
            // colDiscountPercent
            // 
            this.colDiscountPercent.DataPropertyName = "DiscountPercent";
            percentStyle.Format = "0\'%\'";
            this.colDiscountPercent.DefaultCellStyle = percentStyle;
            this.colDiscountPercent.FillWeight = 16F;
            this.colDiscountPercent.HeaderText = "% Giảm";
            this.colDiscountPercent.MinimumWidth = 6;
            this.colDiscountPercent.Name = "colDiscountPercent";
            this.colDiscountPercent.ReadOnly = true;
            // 
            // colStartDate
            // 
            this.colStartDate.DataPropertyName = "StartDate";
            startDateStyle.Format = "dd/MM/yyyy";
            this.colStartDate.DefaultCellStyle = startDateStyle;
            this.colStartDate.FillWeight = 20F;
            this.colStartDate.HeaderText = "Ngày bắt đầu";
            this.colStartDate.MinimumWidth = 6;
            this.colStartDate.Name = "colStartDate";
            this.colStartDate.ReadOnly = true;
            // 
            // colEndDate
            // 
            this.colEndDate.DataPropertyName = "EndDate";
            endDateStyle.Format = "dd/MM/yyyy";
            this.colEndDate.DefaultCellStyle = endDateStyle;
            this.colEndDate.FillWeight = 20F;
            this.colEndDate.HeaderText = "Ngày kết thúc";
            this.colEndDate.MinimumWidth = 6;
            this.colEndDate.Name = "colEndDate";
            this.colEndDate.ReadOnly = true;
            // 
            // colIsActive
            // 
            this.colIsActive.DataPropertyName = "IsActive";
            this.colIsActive.FillWeight = 14F;
            this.colIsActive.HeaderText = "Kích hoạt";
            this.colIsActive.MinimumWidth = 6;
            this.colIsActive.Name = "colIsActive";
            this.colIsActive.ReadOnly = true;
            // 
            // colIsValidNow
            // 
            this.colIsValidNow.DataPropertyName = "IsValidNow";
            this.colIsValidNow.FillWeight = 16F;
            this.colIsValidNow.HeaderText = "Còn hiệu lực";
            this.colIsValidNow.MinimumWidth = 6;
            this.colIsValidNow.Name = "colIsValidNow";
            this.colIsValidNow.ReadOnly = true;
            // 
            // FrmPromotion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1020, 580);
            this.Controls.Add(this.dgvPromotion);
            this.Controls.Add(this.grpPromotionInfo);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.Name = "FrmPromotion";
            this.Text = "Quản lý Khuyến mãi & Giảm giá";
            this.Load += new System.EventHandler(this.FrmPromotion_Load);
            this.pnlHeader.ResumeLayout(false);
            this.grpPromotionInfo.ResumeLayout(false);
            this.grpPromotionInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPromotion)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpPromotionInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPromoId;
        private System.Windows.Forms.TextBox txtPromoCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDiscountPercent;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.DataGridView dgvPromotion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPromoId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPromoCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiscountPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStartDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEndDate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsActive;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsValidNow;
    }
}