namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class FrmTable
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
            this.lblSummaryStats = new System.Windows.Forms.Label();
            this.lblTitleInput = new System.Windows.Forms.Label();
            this.pnlInput = new System.Windows.Forms.Panel();
            this.grpTableInfo = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.chkFilterByArea = new System.Windows.Forms.CheckBox();
            this.cboTableStatus = new System.Windows.Forms.ComboBox();
            this.lblTableStatus = new System.Windows.Forms.Label();
            this.cboArea = new System.Windows.Forms.ComboBox();
            this.lblArea = new System.Windows.Forms.Label();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.lblTableName = new System.Windows.Forms.Label();
            this.txtTableId = new System.Windows.Forms.TextBox();
            this.lblTableId = new System.Windows.Forms.Label();
            this.dgvTable = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.pnlInput.SuspendLayout();
            this.grpTableInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.pnlHeader.Controls.Add(this.lblSummaryStats);
            this.pnlHeader.Controls.Add(this.lblTitleInput);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(920, 52);
            this.pnlHeader.TabIndex = 2;
            // 
            // lblTitleInput
            // 
            this.lblTitleInput.AutoSize = true;
            this.lblTitleInput.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleInput.ForeColor = System.Drawing.Color.White;
            this.lblTitleInput.Location = new System.Drawing.Point(16, 11);
            this.lblTitleInput.Name = "lblTitleInput";
            this.lblTitleInput.Size = new System.Drawing.Size(348, 30);
            this.lblTitleInput.TabIndex = 0;
            this.lblTitleInput.Text = "QUẢN LÝ SƠ ĐỒ BÀN & KHU VỰC";
            this.lblTitleInput.UseMnemonic = false;
            // 
            // lblSummaryStats
            // 
            this.lblSummaryStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSummaryStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(78)))), ((int)(((byte)(55)))));
            this.lblSummaryStats.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummaryStats.ForeColor = System.Drawing.Color.White;
            this.lblSummaryStats.Location = new System.Drawing.Point(565, 11);
            this.lblSummaryStats.Name = "lblSummaryStats";
            this.lblSummaryStats.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.lblSummaryStats.Size = new System.Drawing.Size(339, 30);
            this.lblSummaryStats.TabIndex = 1;
            this.lblSummaryStats.Text = "Tổng: 0 bàn | Trống: 0 | Có khách: 0";
            this.lblSummaryStats.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlInput
            // 
            this.pnlInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(242)))));
            this.pnlInput.Controls.Add(this.grpTableInfo);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlInput.Location = new System.Drawing.Point(0, 52);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Padding = new System.Windows.Forms.Padding(10);
            this.pnlInput.Size = new System.Drawing.Size(335, 458);
            this.pnlInput.TabIndex = 0;
            // 
            // grpTableInfo
            // 
            this.grpTableInfo.BackColor = System.Drawing.Color.White;
            this.grpTableInfo.Controls.Add(this.btnSave);
            this.grpTableInfo.Controls.Add(this.btnDelete);
            this.grpTableInfo.Controls.Add(this.btnEdit);
            this.grpTableInfo.Controls.Add(this.btnAdd);
            this.grpTableInfo.Controls.Add(this.chkFilterByArea);
            this.grpTableInfo.Controls.Add(this.cboTableStatus);
            this.grpTableInfo.Controls.Add(this.lblTableStatus);
            this.grpTableInfo.Controls.Add(this.cboArea);
            this.grpTableInfo.Controls.Add(this.lblArea);
            this.grpTableInfo.Controls.Add(this.txtTableName);
            this.grpTableInfo.Controls.Add(this.lblTableName);
            this.grpTableInfo.Controls.Add(this.txtTableId);
            this.grpTableInfo.Controls.Add(this.lblTableId);
            this.grpTableInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTableInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTableInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.grpTableInfo.Location = new System.Drawing.Point(10, 10);
            this.grpTableInfo.Name = "grpTableInfo";
            this.grpTableInfo.Size = new System.Drawing.Size(315, 438);
            this.grpTableInfo.TabIndex = 0;
            this.grpTableInfo.TabStop = false;
            this.grpTableInfo.Text = "Thông tin chi tiết Bàn";
            // 
            // lblTableId
            // 
            this.lblTableId.AutoSize = true;
            this.lblTableId.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTableId.Location = new System.Drawing.Point(16, 34);
            this.lblTableId.Name = "lblTableId";
            this.lblTableId.Size = new System.Drawing.Size(60, 20);
            this.lblTableId.TabIndex = 1;
            this.lblTableId.Text = "Mã bàn:";
            // 
            // txtTableId
            // 
            this.txtTableId.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTableId.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTableId.Location = new System.Drawing.Point(19, 57);
            this.txtTableId.Name = "txtTableId";
            this.txtTableId.ReadOnly = true;
            this.txtTableId.Size = new System.Drawing.Size(276, 29);
            this.txtTableId.TabIndex = 2;
            // 
            // lblTableName
            // 
            this.lblTableName.AutoSize = true;
            this.lblTableName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTableName.Location = new System.Drawing.Point(16, 98);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(63, 20);
            this.lblTableName.TabIndex = 3;
            this.lblTableName.Text = "Tên bàn:";
            // 
            // txtTableName
            // 
            this.txtTableName.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTableName.Location = new System.Drawing.Point(19, 121);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(276, 29);
            this.txtTableName.TabIndex = 4;
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArea.Location = new System.Drawing.Point(16, 162);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(64, 20);
            this.lblArea.TabIndex = 5;
            this.lblArea.Text = "Khu vực:";
            // 
            // cboArea
            // 
            this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArea.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboArea.FormattingEnabled = true;
            this.cboArea.Location = new System.Drawing.Point(19, 185);
            this.cboArea.Name = "cboArea";
            this.cboArea.Size = new System.Drawing.Size(276, 29);
            this.cboArea.TabIndex = 6;
            this.cboArea.SelectedIndexChanged += new System.EventHandler(this.cboArea_SelectedIndexChanged);
            // 
            // chkFilterByArea
            // 
            this.chkFilterByArea.AutoSize = true;
            this.chkFilterByArea.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFilterByArea.Location = new System.Drawing.Point(19, 220);
            this.chkFilterByArea.Name = "chkFilterByArea";
            this.chkFilterByArea.Size = new System.Drawing.Size(230, 23);
            this.chkFilterByArea.TabIndex = 7;
            this.chkFilterByArea.Text = "Chỉ hiển thị bàn thuộc khu vực này";
            this.chkFilterByArea.UseVisualStyleBackColor = true;
            this.chkFilterByArea.CheckedChanged += new System.EventHandler(this.chkFilterByArea_CheckedChanged);
            // 
            // lblTableStatus
            // 
            this.lblTableStatus.AutoSize = true;
            this.lblTableStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTableStatus.Location = new System.Drawing.Point(16, 252);
            this.lblTableStatus.Name = "lblTableStatus";
            this.lblTableStatus.Size = new System.Drawing.Size(107, 20);
            this.lblTableStatus.TabIndex = 8;
            this.lblTableStatus.Text = "Trạng thái bàn:";
            // 
            // cboTableStatus
            // 
            this.cboTableStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTableStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTableStatus.FormattingEnabled = true;
            this.cboTableStatus.Location = new System.Drawing.Point(19, 275);
            this.cboTableStatus.Name = "cboTableStatus";
            this.cboTableStatus.Size = new System.Drawing.Size(276, 29);
            this.cboTableStatus.TabIndex = 9;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(19, 326);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(132, 38);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "+ Thêm bàn";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(84)))), ((int)(((byte)(0)))));
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(163, 326);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(132, 38);
            this.btnEdit.TabIndex = 11;
            this.btnEdit.Text = "✎ Sửa bàn";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(19, 376);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(132, 38);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "✕ Xóa bàn";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(78)))), ((int)(((byte)(55)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(163, 376);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(132, 38);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "↻ Làm mới";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgvTable
            // 
            this.dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTable.Location = new System.Drawing.Point(335, 52);
            this.dgvTable.Name = "dgvTable";
            this.dgvTable.RowHeadersWidth = 51;
            this.dgvTable.RowTemplate.Height = 28;
            this.dgvTable.Size = new System.Drawing.Size(585, 458);
            this.dgvTable.TabIndex = 1;
            this.dgvTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTable_CellClick);
            // 
            // FrmTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(920, 510);
            this.Controls.Add(this.dgvTable);
            this.Controls.Add(this.pnlInput);
            this.Controls.Add(this.pnlHeader);
            this.Name = "FrmTable";
            this.Text = "Quản lý Sơ đồ Bàn & Khu vực";
            this.Load += new System.EventHandler(this.FrmTable_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlInput.ResumeLayout(false);
            this.grpTableInfo.ResumeLayout(false);
            this.grpTableInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblSummaryStats;
        private System.Windows.Forms.Label lblTitleInput;
        private System.Windows.Forms.Panel pnlInput;
        private System.Windows.Forms.GroupBox grpTableInfo;
        private System.Windows.Forms.Label lblTableId;
        private System.Windows.Forms.TextBox txtTableId;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.ComboBox cboArea;
        private System.Windows.Forms.CheckBox chkFilterByArea;
        private System.Windows.Forms.Label lblTableStatus;
        private System.Windows.Forms.ComboBox cboTableStatus;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvTable;
    }
}
