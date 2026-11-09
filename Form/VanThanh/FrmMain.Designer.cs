namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class FrmMain
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
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.menuSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAppConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemEmployee = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCustomer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBusiness = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemInventory = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemPromotion = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemReportWord = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChart = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPermission = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemPermission = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.lblCurrentUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCurrentRole = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSystemTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.menuItemChangePassWord = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripMain.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSystem,
            this.menuCategory,
            this.menuBusiness,
            this.menuReport,
            this.menuPermission});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(800, 28);
            this.menuStripMain.TabIndex = 1;
            this.menuStripMain.Text = "&Hệ thống";
            // 
            // menuSystem
            // 
            this.menuSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAppConfig,
            this.menuItemChangePassWord,
            this.menuItemLogout,
            this.toolStripMenuItem1,
            this.menuItemExit});
            this.menuSystem.Name = "menuSystem";
            this.menuSystem.Size = new System.Drawing.Size(85, 24);
            this.menuSystem.Text = "&Hệ thống";
            // 
            // menuItemAppConfig
            // 
            this.menuItemAppConfig.Name = "menuItemAppConfig";
            this.menuItemAppConfig.Size = new System.Drawing.Size(255, 26);
            this.menuItemAppConfig.Text = "Cấu hình hệ thống & CSDL";
            this.menuItemAppConfig.Click += new System.EventHandler(this.menuItemAppConfig_Click);
            // 
            // menuItemLogout
            // 
            this.menuItemLogout.Name = "menuItemLogout";
            this.menuItemLogout.Size = new System.Drawing.Size(255, 26);
            this.menuItemLogout.Text = "Đăng xuất";
            this.menuItemLogout.Click += new System.EventHandler(this.menuItemLogout_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(252, 6);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new System.Drawing.Size(255, 26);
            this.menuItemExit.Text = "Thoát ứng dụng";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuCategory
            // 
            this.menuCategory.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemProduct,
            this.menuItemCategory,
            this.menuItemTable,
            this.toolStripMenuItem2,
            this.menuItemEmployee,
            this.menuItemCustomer});
            this.menuCategory.Name = "menuCategory";
            this.menuCategory.Size = new System.Drawing.Size(90, 24);
            this.menuCategory.Text = "&Danh mục";
            // 
            // menuItemProduct
            // 
            this.menuItemProduct.Name = "menuItemProduct";
            this.menuItemProduct.Size = new System.Drawing.Size(296, 26);
            this.menuItemProduct.Text = "Quản lý Món ăn / Thức uống";
            this.menuItemProduct.Click += new System.EventHandler(this.menuItemProduct_Click);
            // 
            // menuItemCategory
            // 
            this.menuItemCategory.Name = "menuItemCategory";
            this.menuItemCategory.Size = new System.Drawing.Size(296, 26);
            this.menuItemCategory.Text = "Quản lý Nhóm sản phẩm";
            this.menuItemCategory.Click += new System.EventHandler(this.menuItemCategory_Click);
            // 
            // menuItemTable
            // 
            this.menuItemTable.Name = "menuItemTable";
            this.menuItemTable.Size = new System.Drawing.Size(296, 26);
            this.menuItemTable.Text = "Quản lý Sơ đồ Bàn";
            this.menuItemTable.Click += new System.EventHandler(this.menuItemTable_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(293, 6);
            // 
            // menuItemEmployee
            // 
            this.menuItemEmployee.Name = "menuItemEmployee";
            this.menuItemEmployee.Size = new System.Drawing.Size(296, 26);
            this.menuItemEmployee.Text = "Quản lý Nhân viên";
            this.menuItemEmployee.Click += new System.EventHandler(this.menuItemEmployee_Click);
            // 
            // menuItemCustomer
            // 
            this.menuItemCustomer.Name = "menuItemCustomer";
            this.menuItemCustomer.Size = new System.Drawing.Size(296, 26);
            this.menuItemCustomer.Text = "Quản lý Khách hàng & Tích điểm";
            this.menuItemCustomer.Click += new System.EventHandler(this.menuItemCustomer_Click);
            // 
            // menuBusiness
            // 
            this.menuBusiness.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemOrder,
            this.menuItemInventory,
            this.menuItemPromotion});
            this.menuBusiness.Name = "menuBusiness";
            this.menuBusiness.Size = new System.Drawing.Size(91, 24);
            this.menuBusiness.Text = "&Nghiệp vụ";
            // 
            // menuItemOrder
            // 
            this.menuItemOrder.Name = "menuItemOrder";
            this.menuItemOrder.Size = new System.Drawing.Size(252, 26);
            this.menuItemOrder.Text = "Order";
            this.menuItemOrder.Click += new System.EventHandler(this.menuItemOrder_Click);
            // 
            // menuItemInventory
            // 
            this.menuItemInventory.Name = "menuItemInventory";
            this.menuItemInventory.Size = new System.Drawing.Size(252, 26);
            this.menuItemInventory.Text = "Quản lý Kho nguyên liệu";
            this.menuItemInventory.Click += new System.EventHandler(this.menuItemInventory_Click);
            // 
            // menuItemPromotion
            // 
            this.menuItemPromotion.Name = "menuItemPromotion";
            this.menuItemPromotion.Size = new System.Drawing.Size(252, 26);
            this.menuItemPromotion.Text = "Quản lý Khuyến mãi";
            this.menuItemPromotion.Click += new System.EventHandler(this.menuItemPromotion_Click);
            // 
            // menuReport
            // 
            this.menuReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemReportWord,
            this.menuItemChart});
            this.menuReport.Name = "menuReport";
            this.menuReport.Size = new System.Drawing.Size(77, 24);
            this.menuReport.Text = "&Báo cáo";
            // 
            // menuItemReportWord
            // 
            this.menuItemReportWord.Name = "menuItemReportWord";
            this.menuItemReportWord.Size = new System.Drawing.Size(287, 26);
            this.menuItemReportWord.Text = "Xuất báo cáo Microsoft Word";
            this.menuItemReportWord.Click += new System.EventHandler(this.menuItemReprotWord_Click);
            // 
            // menuItemChart
            // 
            this.menuItemChart.Name = "menuItemChart";
            this.menuItemChart.Size = new System.Drawing.Size(287, 26);
            this.menuItemChart.Text = "Biểu đồ thống kê doanh thu";
            this.menuItemChart.Click += new System.EventHandler(this.menuItemChart_Click);
            // 
            // menuPermission
            // 
            this.menuPermission.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemPermission});
            this.menuPermission.Name = "menuPermission";
            this.menuPermission.Size = new System.Drawing.Size(99, 24);
            this.menuPermission.Text = "&Phân quyền";
            // 
            // menuItemPermission
            // 
            this.menuItemPermission.Name = "menuItemPermission";
            this.menuItemPermission.Size = new System.Drawing.Size(292, 26);
            this.menuItemPermission.Text = "Quản lý Tài khoản & Phân quyền";
            this.menuItemPermission.Click += new System.EventHandler(this.menuItemPermission_Click);
            // 
            // statusStripMain
            // 
            this.statusStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCurrentUser,
            this.lblCurrentRole,
            this.lblSystemTime,
            this.toolStripStatusLabel1});
            this.statusStripMain.Location = new System.Drawing.Point(0, 424);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStripMain.Size = new System.Drawing.Size(800, 26);
            this.statusStripMain.TabIndex = 3;
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.AutoSize = false;
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.lblCurrentUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCurrentUser.Size = new System.Drawing.Size(248, 20);
            this.lblCurrentUser.Text = "Người dùng: [Chưa đăng nhập]";
            this.lblCurrentUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCurrentRole
            // 
            this.lblCurrentRole.AutoSize = false;
            this.lblCurrentRole.Name = "lblCurrentRole";
            this.lblCurrentRole.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.lblCurrentRole.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCurrentRole.Size = new System.Drawing.Size(249, 20);
            this.lblCurrentRole.Text = "Quyền: ---";
            this.lblCurrentRole.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSystemTime
            // 
            this.lblSystemTime.AutoSize = false;
            this.lblSystemTime.Name = "lblSystemTime";
            this.lblSystemTime.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.lblSystemTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblSystemTime.Size = new System.Drawing.Size(213, 20);
            this.lblSystemTime.Spring = true;
            this.lblSystemTime.Text = "Thời gian: 00/00/0000 00:00:00";
            this.lblSystemTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(75, 20);
            this.toolStripStatusLabel1.Text = "-----------";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 28);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(800, 396);
            this.pnlContent.TabIndex = 4;
            // 
            // menuItemChangePassWord
            // 
            this.menuItemChangePassWord.Name = "menuItemChangePassWord";
            this.menuItemChangePassWord.Size = new System.Drawing.Size(255, 26);
            this.menuItemChangePassWord.Text = "Đổi mật khẩu";
            this.menuItemChangePassWord.Click += new System.EventHandler(this.menuItemChangePassWord_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(248)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.menuStripMain);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "FrmMain";
            this.Text = "Màn hình chính";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem menuSystem;
        private System.Windows.Forms.ToolStripMenuItem menuItemAppConfig;
        private System.Windows.Forms.ToolStripMenuItem menuItemLogout;
        private System.Windows.Forms.ToolStripMenuItem menuCategory;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.ToolStripMenuItem menuItemProduct;
        private System.Windows.Forms.ToolStripMenuItem menuItemCategory;
        private System.Windows.Forms.ToolStripMenuItem menuItemTable;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuItemEmployee;
        private System.Windows.Forms.ToolStripMenuItem menuItemCustomer;
        private System.Windows.Forms.ToolStripMenuItem menuBusiness;
        private System.Windows.Forms.ToolStripMenuItem menuItemOrder;
        private System.Windows.Forms.ToolStripMenuItem menuItemInventory;
        private System.Windows.Forms.ToolStripMenuItem menuItemPromotion;
        private System.Windows.Forms.ToolStripMenuItem menuReport;
        private System.Windows.Forms.ToolStripMenuItem menuItemReportWord;
        private System.Windows.Forms.ToolStripMenuItem menuItemChart;
        private System.Windows.Forms.ToolStripMenuItem menuPermission;
        private System.Windows.Forms.ToolStripMenuItem menuItemPermission;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.ToolStripStatusLabel lblSystemTime;
        private System.Windows.Forms.ToolStripStatusLabel lblCurrentUser;
        private System.Windows.Forms.ToolStripStatusLabel lblCurrentRole;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem menuItemChangePassWord;
    }
}