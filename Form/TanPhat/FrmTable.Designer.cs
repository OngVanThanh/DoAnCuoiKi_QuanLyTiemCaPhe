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
            this.pnlInput = new System.Windows.Forms.Panel();
            this.lblTitleInput = new System.Windows.Forms.Label();
            this.lblTableId = new System.Windows.Forms.Label();
            this.txtTableId = new System.Windows.Forms.TextBox();
            this.lblTableName = new System.Windows.Forms.Label();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.lblArea = new System.Windows.Forms.Label();
            this.cboArea = new System.Windows.Forms.ComboBox();
            this.lblTableStatus = new System.Windows.Forms.Label();
            this.cboTableStatus = new System.Windows.Forms.ComboBox();
            this.chkFilterByArea = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvTable = new System.Windows.Forms.DataGridView();
            this.pnlInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlInput
            // 
            this.pnlInput.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlInput.Controls.Add(this.btnSave);
            this.pnlInput.Controls.Add(this.btnDelete);
            this.pnlInput.Controls.Add(this.btnEdit);
            this.pnlInput.Controls.Add(this.btnAdd);
            this.pnlInput.Controls.Add(this.chkFilterByArea);
            this.pnlInput.Controls.Add(this.cboTableStatus);
            this.pnlInput.Controls.Add(this.lblTableStatus);
            this.pnlInput.Controls.Add(this.cboArea);
            this.pnlInput.Controls.Add(this.lblArea);
            this.pnlInput.Controls.Add(this.txtTableName);
            this.pnlInput.Controls.Add(this.lblTableName);
            this.pnlInput.Controls.Add(this.txtTableId);
            this.pnlInput.Controls.Add(this.lblTableId);
            this.pnlInput.Controls.Add(this.lblTitleInput);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlInput.Location = new System.Drawing.Point(0, 0);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Padding = new System.Windows.Forms.Padding(12);
            this.pnlInput.Size = new System.Drawing.Size(290, 480);
            this.pnlInput.TabIndex = 0;
            // 
            // lblTitleInput
            // 
            this.lblTitleInput.AutoSize = true;
            this.lblTitleInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleInput.Location = new System.Drawing.Point(16, 16);
            this.lblTitleInput.Name = "lblTitleInput";
            this.lblTitleInput.Size = new System.Drawing.Size(210, 24);
            this.lblTitleInput.TabIndex = 0;
            this.lblTitleInput.Text = "QUẢN LÝ SƠ ĐỒ BÀN";
            // 
            // lblTableId
            // 
            this.lblTableId.AutoSize = true;
            this.lblTableId.Location = new System.Drawing.Point(16, 56);
            this.lblTableId.Name = "lblTableId";
            this.lblTableId.Size = new System.Drawing.Size(55, 16);
            this.lblTableId.TabIndex = 1;
            this.lblTableId.Text = "Mã bàn:";
            // 
            // txtTableId
            // 
            this.txtTableId.Location = new System.Drawing.Point(19, 76);
            this.txtTableId.Name = "txtTableId";
            this.txtTableId.ReadOnly = true;
            this.txtTableId.Size = new System.Drawing.Size(250, 22);
            this.txtTableId.TabIndex = 2;
            // 
            // lblTableName
            // 
            this.lblTableName.AutoSize = true;
            this.lblTableName.Location = new System.Drawing.Point(16, 112);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(60, 16);
            this.lblTableName.TabIndex = 3;
            this.lblTableName.Text = "Tên bàn:";
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(19, 132);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(250, 22);
            this.txtTableName.TabIndex = 4;
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(16, 168);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(59, 16);
            this.lblArea.TabIndex = 5;
            this.lblArea.Text = "Khu vực:";
            // 
            // cboArea
            // 
            this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArea.FormattingEnabled = true;
            this.cboArea.Location = new System.Drawing.Point(19, 188);
            this.cboArea.Name = "cboArea";
            this.cboArea.Size = new System.Drawing.Size(250, 24);
            this.cboArea.TabIndex = 6;
            this.cboArea.SelectedIndexChanged += new System.EventHandler(this.cboArea_SelectedIndexChanged);
            // 
            // chkFilterByArea
            // 
            this.chkFilterByArea.AutoSize = true;
            this.chkFilterByArea.Location = new System.Drawing.Point(19, 218);
            this.chkFilterByArea.Name = "chkFilterByArea";
            this.chkFilterByArea.Size = new System.Drawing.Size(208, 20);
            this.chkFilterByArea.TabIndex = 7;
            this.chkFilterByArea.Text = "Chỉ hiển thị bàn thuộc khu vực này";
            this.chkFilterByArea.UseVisualStyleBackColor = true;
            this.chkFilterByArea.CheckedChanged += new System.EventHandler(this.chkFilterByArea_CheckedChanged);
            // 
            // lblTableStatus
            // 
            this.lblTableStatus.AutoSize = true;
            this.lblTableStatus.Location = new System.Drawing.Point(16, 250);
            this.lblTableStatus.Name = "lblTableStatus";
            this.lblTableStatus.Size = new System.Drawing.Size(96, 16);
            this.lblTableStatus.TabIndex = 8;
            this.lblTableStatus.Text = "Trạng thái bàn:";
            // 
            // cboTableStatus
            // 
            this.cboTableStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTableStatus.FormattingEnabled = true;
            this.cboTableStatus.Location = new System.Drawing.Point(19, 270);
            this.cboTableStatus.Name = "cboTableStatus";
            this.cboTableStatus.Size = new System.Drawing.Size(250, 24);
            this.cboTableStatus.TabIndex = 9;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(19, 320);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(118, 36);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "Thêm bàn";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(151, 320);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(118, 36);
            this.btnEdit.TabIndex = 11;
            this.btnEdit.Text = "Sửa bàn";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(19, 368);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(118, 36);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "Xóa bàn";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(151, 368);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(118, 36);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Lưu / Làm mới";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgvTable
            // 
            this.dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTable.Location = new System.Drawing.Point(290, 0);
            this.dgvTable.Name = "dgvTable";
            this.dgvTable.RowHeadersWidth = 51;
            this.dgvTable.RowTemplate.Height = 24;
            this.dgvTable.Size = new System.Drawing.Size(560, 480);
            this.dgvTable.TabIndex = 1;
            this.dgvTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTable_CellClick);
            // 
            // FrmTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 480);
            this.Controls.Add(this.dgvTable);
            this.Controls.Add(this.pnlInput);
            this.Name = "FrmTable";
            this.Text = "Quản lý Sơ đồ Bàn & Khu vực";
            this.Load += new System.EventHandler(this.FrmTable_Load);
            this.pnlInput.ResumeLayout(false);
            this.pnlInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlInput;
        private System.Windows.Forms.Label lblTitleInput;
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
