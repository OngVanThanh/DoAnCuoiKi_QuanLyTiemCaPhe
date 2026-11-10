namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class FrmRecipe
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
            System.Windows.Forms.DataGridViewCellStyle prodAltStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle prodHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle prodCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle recAltStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle recHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle recCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle qtyStyle = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.colProdId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProdName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProdUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlProductFilter = new System.Windows.Forms.Panel();
            this.cboCategoryFilter = new System.Windows.Forms.ComboBox();
            this.lblFilterCat = new System.Windows.Forms.Label();
            this.txtSearchProduct = new System.Windows.Forms.TextBox();
            this.lblSearchProd = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.grpIngredients = new System.Windows.Forms.GroupBox();
            this.dgvRecipeIngredients = new System.Windows.Forms.DataGridView();
            this.colRecId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIngredientName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRecUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlIngredientEditor = new System.Windows.Forms.Panel();
            this.btnCancelIng = new System.Windows.Forms.Button();
            this.btnSaveIng = new System.Windows.Forms.Button();
            this.btnDeleteIng = new System.Windows.Forms.Button();
            this.btnEditIng = new System.Windows.Forms.Button();
            this.btnAddIng = new System.Windows.Forms.Button();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.cboUnit = new System.Windows.Forms.ComboBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtIngredientName = new System.Windows.Forms.TextBox();
            this.lblIngredientName = new System.Windows.Forms.Label();
            this.txtRecipeId = new System.Windows.Forms.TextBox();
            this.grpBaristaSteps = new System.Windows.Forms.GroupBox();
            this.txtPreparationSteps = new System.Windows.Forms.TextBox();
            this.pnlStepsFooter = new System.Windows.Forms.Panel();
            this.btnSaveSteps = new System.Windows.Forms.Button();
            this.lblStepsHint = new System.Windows.Forms.Label();
            this.pnlSelectedBanner = new System.Windows.Forms.Panel();
            this.lblSelectedProduct = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.pnlProductFilter.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.grpIngredients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecipeIngredients)).BeginInit();
            this.pnlIngredientEditor.SuspendLayout();
            this.grpBaristaSteps.SuspendLayout();
            this.pnlStepsFooter.SuspendLayout();
            this.pnlSelectedBanner.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(74, 44, 42);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1120, 52);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1120, 52);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "CÔNG THỨC PHA CHẾ CHUẨN & QUY TRÌNH BARISTA";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.UseMnemonic = false;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(248, 245, 240);
            this.pnlLeft.Controls.Add(this.dgvProducts);
            this.pnlLeft.Controls.Add(this.pnlProductFilter);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 52);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(10);
            this.pnlLeft.Size = new System.Drawing.Size(410, 618);
            this.pnlLeft.TabIndex = 1;
            // 
            // pnlProductFilter
            // 
            this.pnlProductFilter.BackColor = System.Drawing.Color.White;
            this.pnlProductFilter.Controls.Add(this.cboCategoryFilter);
            this.pnlProductFilter.Controls.Add(this.lblFilterCat);
            this.pnlProductFilter.Controls.Add(this.txtSearchProduct);
            this.pnlProductFilter.Controls.Add(this.lblSearchProd);
            this.pnlProductFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProductFilter.Location = new System.Drawing.Point(10, 10);
            this.pnlProductFilter.Name = "pnlProductFilter";
            this.pnlProductFilter.Size = new System.Drawing.Size(390, 86);
            this.pnlProductFilter.TabIndex = 0;
            // 
            // lblFilterCat
            // 
            this.lblFilterCat.AutoSize = true;
            this.lblFilterCat.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblFilterCat.ForeColor = System.Drawing.Color.FromArgb(74, 44, 42);
            this.lblFilterCat.Location = new System.Drawing.Point(10, 14);
            this.lblFilterCat.Name = "lblFilterCat";
            this.lblFilterCat.Size = new System.Drawing.Size(88, 21);
            this.lblFilterCat.TabIndex = 0;
            this.lblFilterCat.Text = "Nhóm món:";
            // 
            // cboCategoryFilter
            // 
            this.cboCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategoryFilter.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.cboCategoryFilter.FormattingEnabled = true;
            this.cboCategoryFilter.Location = new System.Drawing.Point(108, 10);
            this.cboCategoryFilter.Name = "cboCategoryFilter";
            this.cboCategoryFilter.Size = new System.Drawing.Size(270, 29);
            this.cboCategoryFilter.TabIndex = 1;
            this.cboCategoryFilter.SelectedIndexChanged += new System.EventHandler(this.cboCategoryFilter_SelectedIndexChanged);
            // 
            // lblSearchProd
            // 
            this.lblSearchProd.AutoSize = true;
            this.lblSearchProd.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSearchProd.ForeColor = System.Drawing.Color.FromArgb(74, 44, 42);
            this.lblSearchProd.Location = new System.Drawing.Point(10, 50);
            this.lblSearchProd.Name = "lblSearchProd";
            this.lblSearchProd.Size = new System.Drawing.Size(72, 21);
            this.lblSearchProd.TabIndex = 2;
            this.lblSearchProd.Text = "Tìm món:";
            // 
            // txtSearchProduct
            // 
            this.txtSearchProduct.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.txtSearchProduct.Location = new System.Drawing.Point(108, 46);
            this.txtSearchProduct.Name = "txtSearchProduct";
            this.txtSearchProduct.Size = new System.Drawing.Size(270, 29);
            this.txtSearchProduct.TabIndex = 3;
            this.txtSearchProduct.TextChanged += new System.EventHandler(this.txtSearchProduct_TextChanged);
            // 
            // dgvProducts
            // 
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.AllowUserToResizeRows = false;
            prodAltStyle.BackColor = System.Drawing.Color.FromArgb(252, 249, 245);
            this.dgvProducts.AlternatingRowsDefaultCellStyle = prodAltStyle;
            this.dgvProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProducts.BackgroundColor = System.Drawing.Color.White;
            this.dgvProducts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProducts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvProducts.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            prodHeaderStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            prodHeaderStyle.BackColor = System.Drawing.Color.FromArgb(74, 44, 42);
            prodHeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            prodHeaderStyle.ForeColor = System.Drawing.Color.White;
            prodHeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(74, 44, 42);
            prodHeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvProducts.ColumnHeadersDefaultCellStyle = prodHeaderStyle;
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProducts.ColumnHeadersHeight = 38;
            this.dgvProducts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProdId,
            this.colProdName,
            this.colCatName,
            this.colProdUnit});
            prodCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            prodCellStyle.BackColor = System.Drawing.Color.White;
            prodCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            prodCellStyle.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
            prodCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(238, 224, 210);
            prodCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(74, 44, 42);
            this.dgvProducts.DefaultCellStyle = prodCellStyle;
            this.dgvProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProducts.EnableHeadersVisualStyles = false;
            this.dgvProducts.GridColor = System.Drawing.Color.FromArgb(235, 230, 225);
            this.dgvProducts.Location = new System.Drawing.Point(10, 96);
            this.dgvProducts.MultiSelect = false;
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersVisible = false;
            this.dgvProducts.RowTemplate.Height = 34;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(390, 512);
            this.dgvProducts.TabIndex = 1;
            this.dgvProducts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProducts_CellClick);
            // 
            // colProdId
            // 
            this.colProdId.DataPropertyName = "Id";
            this.colProdId.FillWeight = 15F;
            this.colProdId.HeaderText = "Mã";
            this.colProdId.MinimumWidth = 6;
            this.colProdId.Name = "colProdId";
            this.colProdId.ReadOnly = true;
            // 
            // colProdName
            // 
            this.colProdName.DataPropertyName = "ProductName";
            this.colProdName.FillWeight = 45F;
            this.colProdName.HeaderText = "Tên món đồ uống";
            this.colProdName.MinimumWidth = 6;
            this.colProdName.Name = "colProdName";
            this.colProdName.ReadOnly = true;
            // 
            // colCatName
            // 
            this.colCatName.DataPropertyName = "CategoryName";
            this.colCatName.FillWeight = 26F;
            this.colCatName.HeaderText = "Nhóm";
            this.colCatName.MinimumWidth = 6;
            this.colCatName.Name = "colCatName";
            this.colCatName.ReadOnly = true;
            // 
            // colProdUnit
            // 
            this.colProdUnit.DataPropertyName = "Unit";
            this.colProdUnit.FillWeight = 14F;
            this.colProdUnit.HeaderText = "ĐVT";
            this.colProdUnit.MinimumWidth = 6;
            this.colProdUnit.Name = "colProdUnit";
            this.colProdUnit.ReadOnly = true;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.White;
            this.pnlRight.Controls.Add(this.grpIngredients);
            this.pnlRight.Controls.Add(this.grpBaristaSteps);
            this.pnlRight.Controls.Add(this.pnlSelectedBanner);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(410, 52);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(10);
            this.pnlRight.Size = new System.Drawing.Size(710, 618);
            this.pnlRight.TabIndex = 2;
            // 
            // pnlSelectedBanner
            // 
            this.pnlSelectedBanner.BackColor = System.Drawing.Color.FromArgb(245, 238, 230);
            this.pnlSelectedBanner.Controls.Add(this.lblSelectedProduct);
            this.pnlSelectedBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSelectedBanner.Location = new System.Drawing.Point(10, 10);
            this.pnlSelectedBanner.Name = "pnlSelectedBanner";
            this.pnlSelectedBanner.Size = new System.Drawing.Size(690, 40);
            this.pnlSelectedBanner.TabIndex = 0;
            // 
            // lblSelectedProduct
            // 
            this.lblSelectedProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSelectedProduct.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSelectedProduct.ForeColor = System.Drawing.Color.FromArgb(111, 78, 55);
            this.lblSelectedProduct.Location = new System.Drawing.Point(0, 0);
            this.lblSelectedProduct.Name = "lblSelectedProduct";
            this.lblSelectedProduct.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblSelectedProduct.Size = new System.Drawing.Size(690, 40);
            this.lblSelectedProduct.TabIndex = 0;
            this.lblSelectedProduct.Text = "ĐANG XEM CÔNG THỨC: (Chọn một món bên trái)";
            this.lblSelectedProduct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpBaristaSteps
            // 
            this.grpBaristaSteps.BackColor = System.Drawing.Color.FromArgb(252, 249, 245);
            this.grpBaristaSteps.Controls.Add(this.txtPreparationSteps);
            this.grpBaristaSteps.Controls.Add(this.pnlStepsFooter);
            this.grpBaristaSteps.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpBaristaSteps.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpBaristaSteps.ForeColor = System.Drawing.Color.FromArgb(74, 44, 42);
            this.grpBaristaSteps.Location = new System.Drawing.Point(10, 418);
            this.grpBaristaSteps.Name = "grpBaristaSteps";
            this.grpBaristaSteps.Padding = new System.Windows.Forms.Padding(10);
            this.grpBaristaSteps.Size = new System.Drawing.Size(690, 190);
            this.grpBaristaSteps.TabIndex = 2;
            this.grpBaristaSteps.TabStop = false;
            this.grpBaristaSteps.Text = "Quy trình các bước pha chế chuẩn (Barista SOP)";
            // 
            // txtPreparationSteps
            // 
            this.txtPreparationSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPreparationSteps.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.txtPreparationSteps.Location = new System.Drawing.Point(10, 27);
            this.txtPreparationSteps.Multiline = true;
            this.txtPreparationSteps.Name = "txtPreparationSteps";
            this.txtPreparationSteps.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPreparationSteps.Size = new System.Drawing.Size(670, 111);
            this.txtPreparationSteps.TabIndex = 0;
            // 
            // pnlStepsFooter
            // 
            this.pnlStepsFooter.Controls.Add(this.btnSaveSteps);
            this.pnlStepsFooter.Controls.Add(this.lblStepsHint);
            this.pnlStepsFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStepsFooter.Location = new System.Drawing.Point(10, 138);
            this.pnlStepsFooter.Name = "pnlStepsFooter";
            this.pnlStepsFooter.Size = new System.Drawing.Size(670, 42);
            this.pnlStepsFooter.TabIndex = 1;
            // 
            // lblStepsHint
            // 
            this.lblStepsHint.AutoSize = true;
            this.lblStepsHint.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblStepsHint.ForeColor = System.Drawing.Color.DimGray;
            this.lblStepsHint.Location = new System.Drawing.Point(4, 12);
            this.lblStepsHint.Name = "lblStepsHint";
            this.lblStepsHint.Size = new System.Drawing.Size(370, 19);
            this.lblStepsHint.TabIndex = 0;
            this.lblStepsHint.Text = "* Ghi rõ từng bước: Ủ trà/chiết xuất -> Phối trộn -> Đánh bọt/Toppings";
            // 
            // btnSaveSteps
            // 
            this.btnSaveSteps.BackColor = System.Drawing.Color.FromArgb(111, 78, 55);
            this.btnSaveSteps.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSaveSteps.FlatAppearance.BorderSize = 0;
            this.btnSaveSteps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveSteps.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSaveSteps.ForeColor = System.Drawing.Color.White;
            this.btnSaveSteps.Location = new System.Drawing.Point(470, 0);
            this.btnSaveSteps.Name = "btnSaveSteps";
            this.btnSaveSteps.Size = new System.Drawing.Size(200, 42);
            this.btnSaveSteps.TabIndex = 1;
            this.btnSaveSteps.Text = "Lưu quy trình pha chế";
            this.btnSaveSteps.UseVisualStyleBackColor = false;
            this.btnSaveSteps.Click += new System.EventHandler(this.btnSaveSteps_Click);
            // 
            // grpIngredients
            // 
            this.grpIngredients.Controls.Add(this.dgvRecipeIngredients);
            this.grpIngredients.Controls.Add(this.pnlIngredientEditor);
            this.grpIngredients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpIngredients.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpIngredients.ForeColor = System.Drawing.Color.FromArgb(74, 44, 42);
            this.grpIngredients.Location = new System.Drawing.Point(10, 50);
            this.grpIngredients.Name = "grpIngredients";
            this.grpIngredients.Padding = new System.Windows.Forms.Padding(10);
            this.grpIngredients.Size = new System.Drawing.Size(690, 368);
            this.grpIngredients.TabIndex = 1;
            this.grpIngredients.TabStop = false;
            this.grpIngredients.Text = "Bảng định lượng thành phần nguyên liệu (Bill of Materials)";
            // 
            // pnlIngredientEditor
            // 
            this.pnlIngredientEditor.BackColor = System.Drawing.Color.FromArgb(248, 245, 240);
            this.pnlIngredientEditor.Controls.Add(this.btnCancelIng);
            this.pnlIngredientEditor.Controls.Add(this.btnSaveIng);
            this.pnlIngredientEditor.Controls.Add(this.btnDeleteIng);
            this.pnlIngredientEditor.Controls.Add(this.btnEditIng);
            this.pnlIngredientEditor.Controls.Add(this.btnAddIng);
            this.pnlIngredientEditor.Controls.Add(this.txtNote);
            this.pnlIngredientEditor.Controls.Add(this.lblNote);
            this.pnlIngredientEditor.Controls.Add(this.cboUnit);
            this.pnlIngredientEditor.Controls.Add(this.lblUnit);
            this.pnlIngredientEditor.Controls.Add(this.txtQuantity);
            this.pnlIngredientEditor.Controls.Add(this.lblQuantity);
            this.pnlIngredientEditor.Controls.Add(this.txtIngredientName);
            this.pnlIngredientEditor.Controls.Add(this.lblIngredientName);
            this.pnlIngredientEditor.Controls.Add(this.txtRecipeId);
            this.pnlIngredientEditor.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlIngredientEditor.Location = new System.Drawing.Point(10, 27);
            this.pnlIngredientEditor.Name = "pnlIngredientEditor";
            this.pnlIngredientEditor.Size = new System.Drawing.Size(670, 118);
            this.pnlIngredientEditor.TabIndex = 0;
            // 
            // txtRecipeId
            // 
            this.txtRecipeId.Location = new System.Drawing.Point(620, 8);
            this.txtRecipeId.Name = "txtRecipeId";
            this.txtRecipeId.ReadOnly = true;
            this.txtRecipeId.Size = new System.Drawing.Size(38, 29);
            this.txtRecipeId.TabIndex = 0;
            this.txtRecipeId.Visible = false;
            // 
            // lblIngredientName
            // 
            this.lblIngredientName.AutoSize = true;
            this.lblIngredientName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblIngredientName.Location = new System.Drawing.Point(12, 12);
            this.lblIngredientName.Name = "lblIngredientName";
            this.lblIngredientName.Size = new System.Drawing.Size(85, 20);
            this.lblIngredientName.TabIndex = 1;
            this.lblIngredientName.Text = "Nguyên liệu:";
            // 
            // txtIngredientName
            // 
            this.txtIngredientName.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.txtIngredientName.Location = new System.Drawing.Point(102, 8);
            this.txtIngredientName.Name = "txtIngredientName";
            this.txtIngredientName.Size = new System.Drawing.Size(210, 29);
            this.txtIngredientName.TabIndex = 2;
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblQuantity.Location = new System.Drawing.Point(325, 12);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(82, 20);
            this.lblQuantity.TabIndex = 3;
            this.lblQuantity.Text = "Định lượng:";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.txtQuantity.Location = new System.Drawing.Point(412, 8);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(85, 29);
            this.txtQuantity.TabIndex = 4;
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblUnit.Location = new System.Drawing.Point(508, 12);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(51, 20);
            this.lblUnit.TabIndex = 5;
            this.lblUnit.Text = "Đơn vị:";
            // 
            // cboUnit
            // 
            this.cboUnit.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.cboUnit.FormattingEnabled = true;
            this.cboUnit.Items.AddRange(new object[] {
            "ml",
            "gr",
            "muỗng",
            "gói",
            "lát",
            "quả",
            "viên",
            "phần"});
            this.cboUnit.Location = new System.Drawing.Point(565, 8);
            this.cboUnit.Name = "cboUnit";
            this.cboUnit.Size = new System.Drawing.Size(92, 29);
            this.cboUnit.TabIndex = 6;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblNote.Location = new System.Drawing.Point(12, 46);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(86, 20);
            this.lblNote.TabIndex = 7;
            this.lblNote.Text = "Ghi chú pha:";
            // 
            // txtNote
            // 
            this.txtNote.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.txtNote.Location = new System.Drawing.Point(102, 42);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(555, 29);
            this.txtNote.TabIndex = 8;
            // 
            // btnAddIng
            // 
            this.btnAddIng.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnAddIng.FlatAppearance.BorderSize = 0;
            this.btnAddIng.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddIng.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddIng.ForeColor = System.Drawing.Color.White;
            this.btnAddIng.Location = new System.Drawing.Point(102, 78);
            this.btnAddIng.Name = "btnAddIng";
            this.btnAddIng.Size = new System.Drawing.Size(102, 33);
            this.btnAddIng.TabIndex = 9;
            this.btnAddIng.Text = "Thêm NL";
            this.btnAddIng.UseVisualStyleBackColor = false;
            this.btnAddIng.Click += new System.EventHandler(this.btnAddIng_Click);
            // 
            // btnEditIng
            // 
            this.btnEditIng.BackColor = System.Drawing.Color.FromArgb(243, 156, 18);
            this.btnEditIng.FlatAppearance.BorderSize = 0;
            this.btnEditIng.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditIng.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEditIng.ForeColor = System.Drawing.Color.White;
            this.btnEditIng.Location = new System.Drawing.Point(214, 78);
            this.btnEditIng.Name = "btnEditIng";
            this.btnEditIng.Size = new System.Drawing.Size(102, 33);
            this.btnEditIng.TabIndex = 10;
            this.btnEditIng.Text = "Sửa NL";
            this.btnEditIng.UseVisualStyleBackColor = false;
            this.btnEditIng.Click += new System.EventHandler(this.btnEditIng_Click);
            // 
            // btnDeleteIng
            // 
            this.btnDeleteIng.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDeleteIng.FlatAppearance.BorderSize = 0;
            this.btnDeleteIng.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteIng.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeleteIng.ForeColor = System.Drawing.Color.White;
            this.btnDeleteIng.Location = new System.Drawing.Point(326, 78);
            this.btnDeleteIng.Name = "btnDeleteIng";
            this.btnDeleteIng.Size = new System.Drawing.Size(102, 33);
            this.btnDeleteIng.TabIndex = 11;
            this.btnDeleteIng.Text = "Xóa NL";
            this.btnDeleteIng.UseVisualStyleBackColor = false;
            this.btnDeleteIng.Click += new System.EventHandler(this.btnDeleteIng_Click);
            // 
            // btnSaveIng
            // 
            this.btnSaveIng.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnSaveIng.FlatAppearance.BorderSize = 0;
            this.btnSaveIng.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveIng.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSaveIng.ForeColor = System.Drawing.Color.White;
            this.btnSaveIng.Location = new System.Drawing.Point(438, 78);
            this.btnSaveIng.Name = "btnSaveIng";
            this.btnSaveIng.Size = new System.Drawing.Size(108, 33);
            this.btnSaveIng.TabIndex = 12;
            this.btnSaveIng.Text = "Lưu NL";
            this.btnSaveIng.UseVisualStyleBackColor = false;
            this.btnSaveIng.Click += new System.EventHandler(this.btnSaveIng_Click);
            // 
            // btnCancelIng
            // 
            this.btnCancelIng.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnCancelIng.FlatAppearance.BorderSize = 0;
            this.btnCancelIng.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelIng.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelIng.ForeColor = System.Drawing.Color.White;
            this.btnCancelIng.Location = new System.Drawing.Point(555, 78);
            this.btnCancelIng.Name = "btnCancelIng";
            this.btnCancelIng.Size = new System.Drawing.Size(102, 33);
            this.btnCancelIng.TabIndex = 13;
            this.btnCancelIng.Text = "Hủy";
            this.btnCancelIng.UseVisualStyleBackColor = false;
            this.btnCancelIng.Click += new System.EventHandler(this.btnCancelIng_Click);
            // 
            // dgvRecipeIngredients
            // 
            this.dgvRecipeIngredients.AllowUserToAddRows = false;
            this.dgvRecipeIngredients.AllowUserToDeleteRows = false;
            this.dgvRecipeIngredients.AllowUserToResizeRows = false;
            recAltStyle.BackColor = System.Drawing.Color.FromArgb(252, 249, 245);
            this.dgvRecipeIngredients.AlternatingRowsDefaultCellStyle = recAltStyle;
            this.dgvRecipeIngredients.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecipeIngredients.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecipeIngredients.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRecipeIngredients.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvRecipeIngredients.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            recHeaderStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            recHeaderStyle.BackColor = System.Drawing.Color.FromArgb(111, 78, 55);
            recHeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            recHeaderStyle.ForeColor = System.Drawing.Color.White;
            recHeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(111, 78, 55);
            recHeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvRecipeIngredients.ColumnHeadersDefaultCellStyle = recHeaderStyle;
            this.dgvRecipeIngredients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvRecipeIngredients.ColumnHeadersHeight = 36;
            this.dgvRecipeIngredients.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRecId,
            this.colIngredientName,
            this.colQuantity,
            this.colRecUnit,
            this.colNote});
            recCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            recCellStyle.BackColor = System.Drawing.Color.White;
            recCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            recCellStyle.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
            recCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(238, 224, 210);
            recCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(74, 44, 42);
            this.dgvRecipeIngredients.DefaultCellStyle = recCellStyle;
            this.dgvRecipeIngredients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecipeIngredients.EnableHeadersVisualStyles = false;
            this.dgvRecipeIngredients.GridColor = System.Drawing.Color.FromArgb(235, 230, 225);
            this.dgvRecipeIngredients.Location = new System.Drawing.Point(10, 145);
            this.dgvRecipeIngredients.MultiSelect = false;
            this.dgvRecipeIngredients.Name = "dgvRecipeIngredients";
            this.dgvRecipeIngredients.ReadOnly = true;
            this.dgvRecipeIngredients.RowHeadersVisible = false;
            this.dgvRecipeIngredients.RowTemplate.Height = 32;
            this.dgvRecipeIngredients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecipeIngredients.Size = new System.Drawing.Size(670, 213);
            this.dgvRecipeIngredients.TabIndex = 1;
            this.dgvRecipeIngredients.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecipeIngredients_CellClick);
            // 
            // colRecId
            // 
            this.colRecId.DataPropertyName = "Id";
            this.colRecId.HeaderText = "Id";
            this.colRecId.MinimumWidth = 6;
            this.colRecId.Name = "colRecId";
            this.colRecId.ReadOnly = true;
            this.colRecId.Visible = false;
            // 
            // colIngredientName
            // 
            this.colIngredientName.DataPropertyName = "IngredientName";
            this.colIngredientName.FillWeight = 34F;
            this.colIngredientName.HeaderText = "Thành phần nguyên liệu";
            this.colIngredientName.MinimumWidth = 6;
            this.colIngredientName.Name = "colIngredientName";
            this.colIngredientName.ReadOnly = true;
            // 
            // colQuantity
            // 
            this.colQuantity.DataPropertyName = "Quantity";
            qtyStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            qtyStyle.Format = "N1";
            this.colQuantity.DefaultCellStyle = qtyStyle;
            this.colQuantity.FillWeight = 16F;
            this.colQuantity.HeaderText = "Định lượng";
            this.colQuantity.MinimumWidth = 6;
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.ReadOnly = true;
            // 
            // colRecUnit
            // 
            this.colRecUnit.DataPropertyName = "Unit";
            this.colRecUnit.FillWeight = 14F;
            this.colRecUnit.HeaderText = "Đơn vị";
            this.colRecUnit.MinimumWidth = 6;
            this.colRecUnit.Name = "colRecUnit";
            this.colRecUnit.ReadOnly = true;
            // 
            // colNote
            // 
            this.colNote.DataPropertyName = "Note";
            this.colNote.FillWeight = 36F;
            this.colNote.HeaderText = "Ghi chú pha chế";
            this.colNote.MinimumWidth = 6;
            this.colNote.Name = "colNote";
            this.colNote.ReadOnly = true;
            // 
            // FrmRecipe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1120, 670);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.Name = "FrmRecipe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Công thức pha chế & Quy trình Barista";
            this.Load += new System.EventHandler(this.FrmRecipe_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.pnlProductFilter.ResumeLayout(false);
            this.pnlProductFilter.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.grpIngredients.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecipeIngredients)).EndInit();
            this.pnlIngredientEditor.ResumeLayout(false);
            this.pnlIngredientEditor.PerformLayout();
            this.grpBaristaSteps.ResumeLayout(false);
            this.grpBaristaSteps.PerformLayout();
            this.pnlStepsFooter.ResumeLayout(false);
            this.pnlStepsFooter.PerformLayout();
            this.pnlSelectedBanner.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProdId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProdName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProdUnit;
        private System.Windows.Forms.Panel pnlProductFilter;
        private System.Windows.Forms.ComboBox cboCategoryFilter;
        private System.Windows.Forms.Label lblFilterCat;
        private System.Windows.Forms.TextBox txtSearchProduct;
        private System.Windows.Forms.Label lblSearchProd;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.GroupBox grpIngredients;
        private System.Windows.Forms.DataGridView dgvRecipeIngredients;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRecId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIngredientName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRecUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNote;
        private System.Windows.Forms.Panel pnlIngredientEditor;
        private System.Windows.Forms.Button btnCancelIng;
        private System.Windows.Forms.Button btnSaveIng;
        private System.Windows.Forms.Button btnDeleteIng;
        private System.Windows.Forms.Button btnEditIng;
        private System.Windows.Forms.Button btnAddIng;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.ComboBox cboUnit;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtIngredientName;
        private System.Windows.Forms.Label lblIngredientName;
        private System.Windows.Forms.TextBox txtRecipeId;
        private System.Windows.Forms.GroupBox grpBaristaSteps;
        private System.Windows.Forms.TextBox txtPreparationSteps;
        private System.Windows.Forms.Panel pnlStepsFooter;
        private System.Windows.Forms.Button btnSaveSteps;
        private System.Windows.Forms.Label lblStepsHint;
        private System.Windows.Forms.Panel pnlSelectedBanner;
        private System.Windows.Forms.Label lblSelectedProduct;
    }
}