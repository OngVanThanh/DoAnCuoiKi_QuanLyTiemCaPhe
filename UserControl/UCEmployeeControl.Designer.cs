namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class UCEmployeeControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnShowDetail = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnShowDetail
            // 
            this.btnShowDetail.Location = new System.Drawing.Point(12, 13);
            this.btnShowDetail.Name = "btnShowDetail";
            this.btnShowDetail.Size = new System.Drawing.Size(150, 38);
            this.btnShowDetail.TabIndex = 0;
            this.btnShowDetail.Text = "Xem thông tin đầy đủ";
            this.btnShowDetail.UseVisualStyleBackColor = true;
            this.btnShowDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(12, 57);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 39);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            //
            // UCEmployeeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnShowDetail);
            this.Name = "UCEmployeeControl";
            this.Size = new System.Drawing.Size(175, 99);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnShowDetail;
        private System.Windows.Forms.Button btnDelete;
        private CoffeeShopDBDataSet1TableAdapters.EmployeesTableAdapter employeesTableAdapter1;
        private CoffeeShopDBDataSet1TableAdapters.EmployeesTableAdapter employeesTableAdapter2;
    }
}
