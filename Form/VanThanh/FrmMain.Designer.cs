namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuItemChangePassWord = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlNavButtons = new System.Windows.Forms.Panel();
            this.btnNavAppConfig = new System.Windows.Forms.Button();
            this.btnNavPermission = new System.Windows.Forms.Button();
            this.btnNavEmployee = new System.Windows.Forms.Button();
            this.lblGroupAdmin = new System.Windows.Forms.Label();
            this.btnNavReport = new System.Windows.Forms.Button();
            this.btnNavChart = new System.Windows.Forms.Button();
            this.lblGroupReport = new System.Windows.Forms.Label();
            this.btnNavPromotion = new System.Windows.Forms.Button();
            this.btnNavCategory = new System.Windows.Forms.Button();
            this.btnNavProduct = new System.Windows.Forms.Button();
            this.lblGroupCatalog = new System.Windows.Forms.Label();
            this.btnNavCustomer = new System.Windows.Forms.Button();
            this.btnNavRecipe = new System.Windows.Forms.Button();
            this.btnNavOrder = new System.Windows.Forms.Button();
            this.lblGroupPos = new System.Windows.Forms.Label();
            this.pnlSidebarBottom = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.pnlBrand = new System.Windows.Forms.Panel();
            this.lblBrandSub = new System.Windows.Forms.Label();
            this.lblBrandName = new System.Windows.Forms.Label();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.lblCurrentUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCurrentRole = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSystemTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.tmrClock = new System.Windows.Forms.Timer(this.components);
            this.pnlSidebar.SuspendLayout();
            this.pnlNavButtons.SuspendLayout();
            this.pnlSidebarBottom.SuspendLayout();
            this.pnlBrand.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuItemChangePassWord
            // 
            this.menuItemChangePassWord.Name = "menuItemChangePassWord";
            this.menuItemChangePassWord.Size = new System.Drawing.Size(255, 26);
            this.menuItemChangePassWord.Text = "Đổi mật khẩu";
            this.menuItemChangePassWord.Click += new System.EventHandler(this.menuItemChangePassWord_Click);
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(29)))), ((int)(((byte)(26)))));
            this.pnlSidebar.Controls.Add(this.pnlNavButtons);
            this.pnlSidebar.Controls.Add(this.pnlSidebarBottom);
            this.pnlSidebar.Controls.Add(this.pnlBrand);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(242, 734);
            this.pnlSidebar.TabIndex = 0;
            // 
            // pnlNavButtons
            // 
            this.pnlNavButtons.AutoScroll = true;
            this.pnlNavButtons.Controls.Add(this.btnNavAppConfig);
            this.pnlNavButtons.Controls.Add(this.btnNavPermission);
            this.pnlNavButtons.Controls.Add(this.btnNavEmployee);
            this.pnlNavButtons.Controls.Add(this.lblGroupAdmin);
            this.pnlNavButtons.Controls.Add(this.btnNavReport);
            this.pnlNavButtons.Controls.Add(this.btnNavChart);
            this.pnlNavButtons.Controls.Add(this.lblGroupReport);
            this.pnlNavButtons.Controls.Add(this.btnNavPromotion);
            this.pnlNavButtons.Controls.Add(this.btnNavCategory);
            this.pnlNavButtons.Controls.Add(this.btnNavProduct);
            this.pnlNavButtons.Controls.Add(this.lblGroupCatalog);
            this.pnlNavButtons.Controls.Add(this.btnNavCustomer);
            this.pnlNavButtons.Controls.Add(this.btnNavRecipe);
            this.pnlNavButtons.Controls.Add(this.btnNavOrder);
            this.pnlNavButtons.Controls.Add(this.lblGroupPos);
            this.pnlNavButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNavButtons.Location = new System.Drawing.Point(0, 68);
            this.pnlNavButtons.Name = "pnlNavButtons";
            this.pnlNavButtons.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.pnlNavButtons.Size = new System.Drawing.Size(242, 574);
            this.pnlNavButtons.TabIndex = 1;
            // 
            // btnNavAppConfig
            // 
            this.btnNavAppConfig.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(29)))), ((int)(((byte)(26)))));
            this.btnNavAppConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavAppConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavAppConfig.FlatAppearance.BorderSize = 0;
            this.btnNavAppConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavAppConfig.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavAppConfig.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(232)))), ((int)(((byte)(225)))));
            this.btnNavAppConfig.Location = new System.Drawing.Point(8, 522);
            this.btnNavAppConfig.Name = "btnNavAppConfig";
            this.btnNavAppConfig.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnNavAppConfig.Size = new System.Drawing.Size(226, 40);
            this.btnNavAppConfig.TabIndex = 14;
            this.btnNavAppConfig.Text = "⚙️  Bộ nhận diện & Cấu hình";
            this.btnNavAppConfig.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavAppConfig.UseVisualStyleBackColor = false;
            this.btnNavAppConfig.Click += new System.EventHandler(this.btnNavAppConfig_Click);
            // 
            // btnNavPermission
            // 
            this.btnNavPermission.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(29)))), ((int)(((byte)(26)))));
            this.btnNavPermission.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavPermission.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavPermission.FlatAppearance.BorderSize = 0;
            this.btnNavPermission.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavPermission.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavPermission.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(232)))), ((int)(((byte)(225)))));
            this.btnNavPermission.Location = new System.Drawing.Point(8, 482);
            this.btnNavPermission.Name = "btnNavPermission";
            this.btnNavPermission.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnNavPermission.Size = new System.Drawing.Size(226, 40);
            this.btnNavPermission.TabIndex = 13;
            this.btnNavPermission.Text = "🔐  Tài khoản & Phân quyền";
            this.btnNavPermission.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavPermission.UseVisualStyleBackColor = false;
            this.btnNavPermission.Click += new System.EventHandler(this.btnNavPermission_Click);
            // 
            // btnNavEmployee
            // 
            this.btnNavEmployee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(29)))), ((int)(((byte)(26)))));
            this.btnNavEmployee.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavEmployee.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavEmployee.FlatAppearance.BorderSize = 0;
            this.btnNavEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavEmployee.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavEmployee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(232)))), ((int)(((byte)(225)))));
            this.btnNavEmployee.Location = new System.Drawing.Point(8, 442);
            this.btnNavEmployee.Name = "btnNavEmployee";
            this.btnNavEmployee.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnNavEmployee.Size = new System.Drawing.Size(226, 40);
            this.btnNavEmployee.TabIndex = 12;
            this.btnNavEmployee.Text = "👔  Nhân viên & Ca làm việc";
            this.btnNavEmployee.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavEmployee.UseVisualStyleBackColor = false;
            this.btnNavEmployee.Click += new System.EventHandler(this.btnNavEmployee_Click);
            // 
            // lblGroupAdmin
            // 
            this.lblGroupAdmin.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGroupAdmin.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblGroupAdmin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(150)))), ((int)(((byte)(130)))));
            this.lblGroupAdmin.Location = new System.Drawing.Point(8, 412);
            this.lblGroupAdmin.Name = "lblGroupAdmin";
            this.lblGroupAdmin.Padding = new System.Windows.Forms.Padding(6, 10, 0, 2);
            this.lblGroupAdmin.Size = new System.Drawing.Size(226, 30);
            this.lblGroupAdmin.TabIndex = 11;
            this.lblGroupAdmin.Text = "NHÂN SỰ & HỆ THỐNG";
            // 
            // btnNavReport
            // 
            this.btnNavReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(29)))), ((int)(((byte)(26)))));
            this.btnNavReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavReport.FlatAppearance.BorderSize = 0;
            this.btnNavReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavReport.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(232)))), ((int)(((byte)(225)))));
            this.btnNavReport.Location = new System.Drawing.Point(8, 372);
            this.btnNavReport.Name = "btnNavReport";
            this.btnNavReport.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnNavReport.Size = new System.Drawing.Size(226, 40);
            this.btnNavReport.TabIndex = 10;
            this.btnNavReport.Text = "📄  Xuất Báo cáo (Word/PDF)";
            this.btnNavReport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavReport.UseVisualStyleBackColor = false;
            this.btnNavReport.Click += new System.EventHandler(this.btnNavReport_Click);
            // 
            // btnNavChart
            // 
            this.btnNavChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(29)))), ((int)(((byte)(26)))));
            this.btnNavChart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavChart.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavChart.FlatAppearance.BorderSize = 0;
            this.btnNavChart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavChart.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavChart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(232)))), ((int)(((byte)(225)))));
            this.btnNavChart.Location = new System.Drawing.Point(8, 332);
            this.btnNavChart.Name = "btnNavChart";
            this.btnNavChart.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnNavChart.Size = new System.Drawing.Size(226, 40);
            this.btnNavChart.TabIndex = 9;
            this.btnNavChart.Text = "📊  Dashboard Thống kê";
            this.btnNavChart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavChart.UseVisualStyleBackColor = false;
            this.btnNavChart.Click += new System.EventHandler(this.btnNavChart_Click);
            // 
            // lblGroupReport
            // 
            this.lblGroupReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGroupReport.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblGroupReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(150)))), ((int)(((byte)(130)))));
            this.lblGroupReport.Location = new System.Drawing.Point(8, 302);
            this.lblGroupReport.Name = "lblGroupReport";
            this.lblGroupReport.Padding = new System.Windows.Forms.Padding(6, 10, 0, 2);
            this.lblGroupReport.Size = new System.Drawing.Size(226, 30);
            this.lblGroupReport.TabIndex = 8;
            this.lblGroupReport.Text = "THỐNG KÊ & BÁO CÁO";
            // 
            // btnNavPromotion
            // 
            this.btnNavPromotion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(29)))), ((int)(((byte)(26)))));
            this.btnNavPromotion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavPromotion.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavPromotion.FlatAppearance.BorderSize = 0;
            this.btnNavPromotion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavPromotion.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavPromotion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(232)))), ((int)(((byte)(225)))));
            this.btnNavPromotion.Location = new System.Drawing.Point(8, 262);
            this.btnNavPromotion.Name = "btnNavPromotion";
            this.btnNavPromotion.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnNavPromotion.Size = new System.Drawing.Size(226, 40);
            this.btnNavPromotion.TabIndex = 7;
            this.btnNavPromotion.Text = "🎁  Khuyến mãi & Giảm giá";
            this.btnNavPromotion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavPromotion.UseVisualStyleBackColor = false;
            this.btnNavPromotion.Click += new System.EventHandler(this.btnNavPromotion_Click);
            // 
            // btnNavCategory
            // 
            this.btnNavCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(29)))), ((int)(((byte)(26)))));
            this.btnNavCategory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavCategory.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavCategory.FlatAppearance.BorderSize = 0;
            this.btnNavCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavCategory.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(232)))), ((int)(((byte)(225)))));
            this.btnNavCategory.Location = new System.Drawing.Point(8, 222);
            this.btnNavCategory.Name = "btnNavCategory";
            this.btnNavCategory.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnNavCategory.Size = new System.Drawing.Size(226, 40);
            this.btnNavCategory.TabIndex = 6;
            this.btnNavCategory.Text = "📂  Nhóm sản phẩm";
            this.btnNavCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavCategory.UseVisualStyleBackColor = false;
            this.btnNavCategory.Click += new System.EventHandler(this.btnNavCategory_Click);
            // 
            // btnNavProduct
            // 
            this.btnNavProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(29)))), ((int)(((byte)(26)))));
            this.btnNavProduct.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavProduct.FlatAppearance.BorderSize = 0;
            this.btnNavProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavProduct.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(232)))), ((int)(((byte)(225)))));
            this.btnNavProduct.Location = new System.Drawing.Point(8, 182);
            this.btnNavProduct.Name = "btnNavProduct";
            this.btnNavProduct.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnNavProduct.Size = new System.Drawing.Size(226, 40);
            this.btnNavProduct.TabIndex = 5;
            this.btnNavProduct.Text = "☕  Thực đơn Món ăn/Uống";
            this.btnNavProduct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavProduct.UseVisualStyleBackColor = false;
            this.btnNavProduct.Click += new System.EventHandler(this.btnNavProduct_Click);
            // 
            // lblGroupCatalog
            // 
            this.lblGroupCatalog.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGroupCatalog.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblGroupCatalog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(150)))), ((int)(((byte)(130)))));
            this.lblGroupCatalog.Location = new System.Drawing.Point(8, 152);
            this.lblGroupCatalog.Name = "lblGroupCatalog";
            this.lblGroupCatalog.Padding = new System.Windows.Forms.Padding(6, 10, 0, 2);
            this.lblGroupCatalog.Size = new System.Drawing.Size(226, 30);
            this.lblGroupCatalog.TabIndex = 4;
            this.lblGroupCatalog.Text = "QUẢN LÝ THỰC ĐƠN & KM";
            // 
            // btnNavCustomer
            // 
            this.btnNavCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(29)))), ((int)(((byte)(26)))));
            this.btnNavCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavCustomer.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavCustomer.FlatAppearance.BorderSize = 0;
            this.btnNavCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavCustomer.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(232)))), ((int)(((byte)(225)))));
            this.btnNavCustomer.Location = new System.Drawing.Point(8, 112);
            this.btnNavCustomer.Name = "btnNavCustomer";
            this.btnNavCustomer.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnNavCustomer.Size = new System.Drawing.Size(226, 40);
            this.btnNavCustomer.TabIndex = 3;
            this.btnNavCustomer.Text = "👥  Khách hàng & Tích điểm";
            this.btnNavCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavCustomer.UseVisualStyleBackColor = false;
            this.btnNavCustomer.Click += new System.EventHandler(this.btnNavCustomer_Click);
            // 
            // btnNavRecipe
            // 
            this.btnNavRecipe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(29)))), ((int)(((byte)(26)))));
            this.btnNavRecipe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavRecipe.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavRecipe.FlatAppearance.BorderSize = 0;
            this.btnNavRecipe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavRecipe.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNavRecipe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(232)))), ((int)(((byte)(225)))));
            this.btnNavRecipe.Location = new System.Drawing.Point(8, 72);
            this.btnNavRecipe.Name = "btnNavRecipe";
            this.btnNavRecipe.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnNavRecipe.Size = new System.Drawing.Size(226, 40);
            this.btnNavRecipe.TabIndex = 2;
            this.btnNavRecipe.Text = "📖  Công thức & Barista";
            this.btnNavRecipe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavRecipe.UseVisualStyleBackColor = false;
            this.btnNavRecipe.Click += new System.EventHandler(this.btnNavRecipe_Click);
            // 
            // btnNavOrder
            // 
            this.btnNavOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(78)))), ((int)(((byte)(55)))));
            this.btnNavOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavOrder.FlatAppearance.BorderSize = 0;
            this.btnNavOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavOrder.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNavOrder.ForeColor = System.Drawing.Color.White;
            this.btnNavOrder.Location = new System.Drawing.Point(8, 32);
            this.btnNavOrder.Name = "btnNavOrder";
            this.btnNavOrder.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnNavOrder.Size = new System.Drawing.Size(226, 40);
            this.btnNavOrder.TabIndex = 1;
            this.btnNavOrder.Text = "🛒  POS Bán hàng & Đặt bàn";
            this.btnNavOrder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavOrder.UseVisualStyleBackColor = false;
            this.btnNavOrder.Click += new System.EventHandler(this.btnNavOrder_Click);
            // 
            // lblGroupPos
            // 
            this.lblGroupPos.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGroupPos.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblGroupPos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(150)))), ((int)(((byte)(130)))));
            this.lblGroupPos.Location = new System.Drawing.Point(8, 6);
            this.lblGroupPos.Name = "lblGroupPos";
            this.lblGroupPos.Padding = new System.Windows.Forms.Padding(6, 6, 0, 2);
            this.lblGroupPos.Size = new System.Drawing.Size(226, 26);
            this.lblGroupPos.TabIndex = 0;
            this.lblGroupPos.Text = "NGHIỆP VỤ QUẦY & BARISTA";
            // 
            // pnlSidebarBottom
            // 
            this.pnlSidebarBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(20)))), ((int)(((byte)(18)))));
            this.pnlSidebarBottom.Controls.Add(this.btnExit);
            this.pnlSidebarBottom.Controls.Add(this.btnLogout);
            this.pnlSidebarBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSidebarBottom.Location = new System.Drawing.Point(0, 642);
            this.pnlSidebarBottom.Name = "pnlSidebarBottom";
            this.pnlSidebarBottom.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.pnlSidebarBottom.Size = new System.Drawing.Size(242, 92);
            this.pnlSidebarBottom.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(10, 50);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(222, 34);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Thoát ứng dụng";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(84)))), ((int)(((byte)(0)))));
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(10, 8);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(222, 34);
            this.btnLogout.TabIndex = 0;
            this.btnLogout.Text = "Đăng xuất tài khoản";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // pnlBrand
            // 
            this.pnlBrand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(20)))), ((int)(((byte)(18)))));
            this.pnlBrand.Controls.Add(this.lblBrandSub);
            this.pnlBrand.Controls.Add(this.lblBrandName);
            this.pnlBrand.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBrand.Location = new System.Drawing.Point(0, 0);
            this.pnlBrand.Name = "pnlBrand";
            this.pnlBrand.Padding = new System.Windows.Forms.Padding(12, 10, 12, 8);
            this.pnlBrand.Size = new System.Drawing.Size(242, 68);
            this.pnlBrand.TabIndex = 0;
            // 
            // lblBrandSub
            // 
            this.lblBrandSub.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblBrandSub.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.lblBrandSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(185)))), ((int)(((byte)(175)))));
            this.lblBrandSub.Location = new System.Drawing.Point(12, 38);
            this.lblBrandSub.Name = "lblBrandSub";
            this.lblBrandSub.Size = new System.Drawing.Size(218, 22);
            this.lblBrandSub.TabIndex = 1;
            this.lblBrandSub.Text = "Hệ thống Quản lý Tiệm Cà phê";
            // 
            // lblBrandName
            // 
            this.lblBrandName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBrandName.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblBrandName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(128)))));
            this.lblBrandName.Location = new System.Drawing.Point(12, 10);
            this.lblBrandName.Name = "lblBrandName";
            this.lblBrandName.Size = new System.Drawing.Size(218, 28);
            this.lblBrandName.TabIndex = 0;
            this.lblBrandName.Text = "☕ MỘC CAFE POS";
            this.lblBrandName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStripMain
            // 
            this.statusStripMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(240)))), ((int)(((byte)(235)))));
            this.statusStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCurrentUser,
            this.lblCurrentRole,
            this.lblSystemTime});
            this.statusStripMain.Location = new System.Drawing.Point(242, 708);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(1118, 26);
            this.statusStripMain.TabIndex = 1;
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCurrentUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Padding = new System.Windows.Forms.Padding(8, 0, 16, 0);
            this.lblCurrentUser.Size = new System.Drawing.Size(169, 20);
            this.lblCurrentUser.Text = "Người dùng: admin";
            // 
            // lblCurrentRole
            // 
            this.lblCurrentRole.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCurrentRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(78)))), ((int)(((byte)(55)))));
            this.lblCurrentRole.Name = "lblCurrentRole";
            this.lblCurrentRole.Padding = new System.Windows.Forms.Padding(8, 0, 16, 0);
            this.lblCurrentRole.Size = new System.Drawing.Size(133, 20);
            this.lblCurrentRole.Text = "Vai trò: Quản lý";
            // 
            // lblSystemTime
            // 
            this.lblSystemTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSystemTime.ForeColor = System.Drawing.Color.DimGray;
            this.lblSystemTime.Name = "lblSystemTime";
            this.lblSystemTime.Size = new System.Drawing.Size(801, 20);
            this.lblSystemTime.Spring = true;
            this.lblSystemTime.Text = "Thời gian: 00/00/0000 00:00:00";
            this.lblSystemTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(248)))), ((int)(((byte)(245)))));
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(242, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1118, 708);
            this.pnlContent.TabIndex = 2;
            // 
            // tmrClock
            // 
            this.tmrClock.Enabled = true;
            this.tmrClock.Interval = 1000;
            this.tmrClock.Tick += new System.EventHandler(this.tmrClock_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(248)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1360, 734);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.pnlSidebar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Quản lý Tiệm Cà phê — Mộc Cafe POS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlNavButtons.ResumeLayout(false);
            this.pnlSidebarBottom.ResumeLayout(false);
            this.pnlBrand.ResumeLayout(false);
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlNavButtons;
        private System.Windows.Forms.Button btnNavAppConfig;
        private System.Windows.Forms.Button btnNavPermission;
        private System.Windows.Forms.Button btnNavEmployee;
        private System.Windows.Forms.Label lblGroupAdmin;
        private System.Windows.Forms.Button btnNavReport;
        private System.Windows.Forms.Button btnNavChart;
        private System.Windows.Forms.Label lblGroupReport;
        private System.Windows.Forms.Button btnNavPromotion;
        private System.Windows.Forms.Button btnNavCategory;
        private System.Windows.Forms.Button btnNavProduct;
        private System.Windows.Forms.Label lblGroupCatalog;
        private System.Windows.Forms.Button btnNavCustomer;
        private System.Windows.Forms.Button btnNavRecipe;
        private System.Windows.Forms.Button btnNavOrder;
        private System.Windows.Forms.Label lblGroupPos;
        private System.Windows.Forms.Panel pnlSidebarBottom;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel pnlBrand;
        private System.Windows.Forms.Label lblBrandSub;
        private System.Windows.Forms.Label lblBrandName;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripStatusLabel lblCurrentUser;
        private System.Windows.Forms.ToolStripStatusLabel lblCurrentRole;
        private System.Windows.Forms.ToolStripStatusLabel lblSystemTime;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Timer tmrClock;
        private System.Windows.Forms.ToolStripMenuItem menuItemChangePassWord;
    }
}