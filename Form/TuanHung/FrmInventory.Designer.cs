namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class FrmInventory
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
            this.tabControlInventory = new System.Windows.Forms.TabControl();
            this.tabStock = new System.Windows.Forms.TabPage();
            this.grpMaterialInfo = new System.Windows.Forms.GroupBox();
            this.txtMinThreshold = new System.Windows.Forms.TextBox();
            this.txtQuantityInStock = new System.Windows.Forms.TextBox();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.txtMaterialName = new System.Windows.Forms.TextBox();
            this.txtMaterialId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpAction = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.dgvInventory = new System.Windows.Forms.DataGridView();
            this.pnlHistoryTop = new System.Windows.Forms.Panel();
            this.btnRefreshHistory = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.tabControlInventory.SuspendLayout();
            this.tabStock.SuspendLayout();
            this.grpMaterialInfo.SuspendLayout();
            this.tabHistory.SuspendLayout();
            this.grpAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).BeginInit();
            this.pnlHistoryTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(982, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(982, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ KHO NGUYÊN LIỆU";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControlInventory
            // 
            this.tabControlInventory.Controls.Add(this.tabStock);
            this.tabControlInventory.Controls.Add(this.tabHistory);
            this.tabControlInventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlInventory.Location = new System.Drawing.Point(0, 50);
            this.tabControlInventory.Name = "tabControlInventory";
            this.tabControlInventory.SelectedIndex = 0;
            this.tabControlInventory.Size = new System.Drawing.Size(982, 666);
            this.tabControlInventory.TabIndex = 1;
            // 
            // tabStock
            // 
            this.tabStock.Controls.Add(this.dgvInventory);
            this.tabStock.Controls.Add(this.grpMaterialInfo);
            this.tabStock.Location = new System.Drawing.Point(4, 29);
            this.tabStock.Name = "tabStock";
            this.tabStock.Padding = new System.Windows.Forms.Padding(3);
            this.tabStock.Size = new System.Drawing.Size(974, 633);
            this.tabStock.TabIndex = 0;
            this.tabStock.Text = "Kho hàng & Nhập / Xuất";
            this.tabStock.UseVisualStyleBackColor = true;
            // 
            // grpMaterialInfo
            // 
            this.grpMaterialInfo.Controls.Add(this.grpAction);
            this.grpMaterialInfo.Controls.Add(this.btnCancel);
            this.grpMaterialInfo.Controls.Add(this.btnSave);
            this.grpMaterialInfo.Controls.Add(this.btnDelete);
            this.grpMaterialInfo.Controls.Add(this.btnEdit);
            this.grpMaterialInfo.Controls.Add(this.btnAdd);
            this.grpMaterialInfo.Controls.Add(this.txtMinThreshold);
            this.grpMaterialInfo.Controls.Add(this.txtQuantityInStock);
            this.grpMaterialInfo.Controls.Add(this.txtUnit);
            this.grpMaterialInfo.Controls.Add(this.txtMaterialName);
            this.grpMaterialInfo.Controls.Add(this.txtMaterialId);
            this.grpMaterialInfo.Controls.Add(this.label5);
            this.grpMaterialInfo.Controls.Add(this.label4);
            this.grpMaterialInfo.Controls.Add(this.label3);
            this.grpMaterialInfo.Controls.Add(this.label2);
            this.grpMaterialInfo.Controls.Add(this.label1);
            this.grpMaterialInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.grpMaterialInfo.Location = new System.Drawing.Point(3, 3);
            this.grpMaterialInfo.Name = "grpMaterialInfo";
            this.grpMaterialInfo.Size = new System.Drawing.Size(380, 627);
            this.grpMaterialInfo.TabIndex = 0;
            this.grpMaterialInfo.TabStop = false;
            this.grpMaterialInfo.Text = "Thông tin nguyên liệu và Giao dịch";
            // 
            // txtMinThreshold
            // 
            this.txtMinThreshold.Location = new System.Drawing.Point(160, 170);
            this.txtMinThreshold.Name = "txtMinThreshold";
            this.txtMinThreshold.Size = new System.Drawing.Size(200, 27);
            this.txtMinThreshold.TabIndex = 9;
            this.txtMinThreshold.Text = "10";
            // 
            // txtQuantityInStock
            // 
            this.txtQuantityInStock.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtQuantityInStock.Location = new System.Drawing.Point(160, 135);
            this.txtQuantityInStock.Name = "txtQuantityInStock";
            this.txtQuantityInStock.ReadOnly = true;
            this.txtQuantityInStock.Size = new System.Drawing.Size(200, 27);
            this.txtQuantityInStock.TabIndex = 8;
            // 
            // txtUnit
            // 
            this.txtUnit.Location = new System.Drawing.Point(160, 100);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(200, 27);
            this.txtUnit.TabIndex = 7;
            this.txtUnit.Text = "Kg";
            // 
            // txtMaterialName
            // 
            this.txtMaterialName.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaterialName.Location = new System.Drawing.Point(160, 65);
            this.txtMaterialName.Name = "txtMaterialName";
            this.txtMaterialName.Size = new System.Drawing.Size(200, 27);
            this.txtMaterialName.TabIndex = 6;
            // 
            // txtMaterialId
            // 
            this.txtMaterialId.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtMaterialId.Location = new System.Drawing.Point(160, 30);
            this.txtMaterialId.Name = "txtMaterialId";
            this.txtMaterialId.ReadOnly = true;
            this.txtMaterialId.Size = new System.Drawing.Size(200, 27);
            this.txtMaterialId.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Định mức tối thiểu:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Tồn kho hiện tại:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Đơn vị tính:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tên nguyên liệu:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã nguyên liệu:";
            // 
            // tabHistory
            // 
            this.tabHistory.Controls.Add(this.dgvHistory);
            this.tabHistory.Controls.Add(this.pnlHistoryTop);
            this.tabHistory.Location = new System.Drawing.Point(4, 29);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabHistory.Size = new System.Drawing.Size(974, 633);
            this.tabHistory.TabIndex = 1;
            this.tabHistory.Text = "Lịch sử giao dịch";
            this.tabHistory.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(20, 215);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(95, 34);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(84)))), ((int)(((byte)(0)))));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(135, 215);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(95, 34);
            this.btnEdit.TabIndex = 11;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(250, 215);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(95, 34);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(80, 260);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 34);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(195, 260);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 34);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grpAction
            // 
            this.grpAction.Controls.Add(this.btnExport);
            this.grpAction.Controls.Add(this.btnImport);
            this.grpAction.Controls.Add(this.nudQuantity);
            this.grpAction.Controls.Add(this.label6);
            this.grpAction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.grpAction.Location = new System.Drawing.Point(15, 310);
            this.grpAction.Name = "grpAction";
            this.grpAction.Size = new System.Drawing.Size(350, 160);
            this.grpAction.TabIndex = 15;
            this.grpAction.TabStop = false;
            this.grpAction.Text = "Nhập / Xuất kho";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Số lượng:";
            // 
            // nudQuantity
            // 
            this.nudQuantity.DecimalPlaces = 1;
            this.nudQuantity.Location = new System.Drawing.Point(150, 32);
            this.nudQuantity.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(180, 27);
            this.nudQuantity.TabIndex = 1;
            this.nudQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnImport
            // 
            this.btnImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.ForeColor = System.Drawing.Color.White;
            this.btnImport.Location = new System.Drawing.Point(20, 85);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(145, 45);
            this.btnImport.TabIndex = 2;
            this.btnImport.Text = "NHẬP KHO";
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(185, 85);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(145, 45);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "XUẤT KHO";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dgvInventory
            // 
            this.dgvInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInventory.Location = new System.Drawing.Point(383, 3);
            this.dgvInventory.Name = "dgvInventory";
            this.dgvInventory.RowHeadersWidth = 51;
            this.dgvInventory.RowTemplate.Height = 24;
            this.dgvInventory.Size = new System.Drawing.Size(588, 627);
            this.dgvInventory.TabIndex = 1;
            this.dgvInventory.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInventory_CellClick);
            // 
            // pnlHistoryTop
            // 
            this.pnlHistoryTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlHistoryTop.Controls.Add(this.label7);
            this.pnlHistoryTop.Controls.Add(this.btnRefreshHistory);
            this.pnlHistoryTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHistoryTop.Location = new System.Drawing.Point(3, 3);
            this.pnlHistoryTop.Name = "pnlHistoryTop";
            this.pnlHistoryTop.Size = new System.Drawing.Size(968, 45);
            this.pnlHistoryTop.TabIndex = 0;
            // 
            // btnRefreshHistory
            // 
            this.btnRefreshHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.btnRefreshHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshHistory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshHistory.ForeColor = System.Drawing.Color.White;
            this.btnRefreshHistory.Location = new System.Drawing.Point(15, 8);
            this.btnRefreshHistory.Name = "btnRefreshHistory";
            this.btnRefreshHistory.Size = new System.Drawing.Size(160, 30);
            this.btnRefreshHistory.TabIndex = 0;
            this.btnRefreshHistory.Text = "Làm mới nhật ký";
            this.btnRefreshHistory.UseVisualStyleBackColor = false;
            this.btnRefreshHistory.Click += new System.EventHandler(this.btnRefreshHistory_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Gray;
            this.label7.Location = new System.Drawing.Point(190, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(225, 20);
            this.label7.TabIndex = 1;
            this.label7.Text = "* Nhật ký được ghi nhận tự động";
            // 
            // dgvHistory
            // 
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistory.Location = new System.Drawing.Point(3, 48);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.RowHeadersWidth = 51;
            this.dgvHistory.RowTemplate.Height = 24;
            this.dgvHistory.Size = new System.Drawing.Size(968, 582);
            this.dgvHistory.TabIndex = 1;
            // 
            // FrmInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(982, 716);
            this.Controls.Add(this.tabControlInventory);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmInventory";
            this.Text = "Quản lý Kho nguyên liệu";
            this.Load += new System.EventHandler(this.FrmInventory_Load);
            this.pnlHeader.ResumeLayout(false);
            this.tabControlInventory.ResumeLayout(false);
            this.tabStock.ResumeLayout(false);
            this.grpMaterialInfo.ResumeLayout(false);
            this.grpMaterialInfo.PerformLayout();
            this.tabHistory.ResumeLayout(false);
            this.grpAction.ResumeLayout(false);
            this.grpAction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).EndInit();
            this.pnlHistoryTop.ResumeLayout(false);
            this.pnlHistoryTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tabControlInventory;
        private System.Windows.Forms.TabPage tabStock;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.GroupBox grpMaterialInfo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMinThreshold;
        private System.Windows.Forms.TextBox txtQuantityInStock;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.TextBox txtMaterialName;
        private System.Windows.Forms.TextBox txtMaterialId;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox grpAction;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvInventory;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Panel pnlHistoryTop;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnRefreshHistory;
    }
}