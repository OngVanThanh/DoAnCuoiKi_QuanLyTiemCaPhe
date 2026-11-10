namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class FrmShiftSchedule
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
            this.dgvSchedule = new System.Windows.Forms.DataGridView();
            this.coffeeShopDBDataSet1 = new DoAnCuoiKi_QuanLyTiemCaPhe.CoffeeShopDBDataSet();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Shift1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Shift2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Shift3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coffeeShopDBDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSchedule
            // 
            this.dgvSchedule.AllowUserToAddRows = false;
            this.dgvSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchedule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.Mision,
            this.Shift1,
            this.Shift2,
            this.Shift3});
            this.dgvSchedule.Location = new System.Drawing.Point(97, 61);
            this.dgvSchedule.Name = "dgvSchedule";
            this.dgvSchedule.RowHeadersWidth = 51;
            this.dgvSchedule.RowTemplate.Height = 24;
            this.dgvSchedule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSchedule.Size = new System.Drawing.Size(743, 276);
            this.dgvSchedule.TabIndex = 0;
            // 
            // coffeeShopDBDataSet1
            // 
            this.coffeeShopDBDataSet1.DataSetName = "CoffeeShopDBDataSet";
            this.coffeeShopDBDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(632, 381);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(753, 381);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Date
            // 
            this.Date.HeaderText = "Ngày";
            this.Date.MinimumWidth = 6;
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Date.Width = 125;
            // 
            // Mision
            // 
            this.Mision.HeaderText = "Nhiệm vụ";
            this.Mision.MinimumWidth = 6;
            this.Mision.Name = "Mision";
            this.Mision.Width = 125;
            // 
            // Shift1
            // 
            this.Shift1.DataPropertyName = "Shift1";
            this.Shift1.HeaderText = "Ca 1";
            this.Shift1.MinimumWidth = 6;
            this.Shift1.Name = "Shift1";
            this.Shift1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Shift1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Shift1.Width = 125;
            // 
            // Shift2
            // 
            this.Shift2.DataPropertyName = "Shift2";
            this.Shift2.HeaderText = "Ca 2";
            this.Shift2.MinimumWidth = 6;
            this.Shift2.Name = "Shift2";
            this.Shift2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Shift2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Shift2.Width = 125;
            // 
            // Shift3
            // 
            this.Shift3.DataPropertyName = "Shift3";
            this.Shift3.HeaderText = "Ca 3";
            this.Shift3.MinimumWidth = 6;
            this.Shift3.Name = "Shift3";
            this.Shift3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Shift3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Shift3.Width = 125;
            // 
            // FrmShiftSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 450);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvSchedule);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmShiftSchedule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmShiftSchedule";
            this.Load += new System.EventHandler(this.FrmShiftSchedule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coffeeShopDBDataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSchedule;
        private CoffeeShopDBDataSet coffeeShopDBDataSet1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mision;
        private System.Windows.Forms.DataGridViewComboBoxColumn Shift1;
        private System.Windows.Forms.DataGridViewComboBoxColumn Shift2;
        private System.Windows.Forms.DataGridViewComboBoxColumn Shift3;
    }
}