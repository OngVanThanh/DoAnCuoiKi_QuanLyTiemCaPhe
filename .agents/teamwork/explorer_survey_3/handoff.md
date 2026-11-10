# BÁO CÁO KHẢO SÁT TOÀN DIỆN MÃ NGUỒN (EXPLORER 3)
**Lĩnh vực khảo sát**: Nghiệp vụ Quản lý Quán Cafe, Hóa đơn & In ấn, Bảo mật & Xác thực, Kiểm toán Lỗ hổng & Chất lượng mã nguồn.  
**Thư mục làm việc**: `d:\DoAnCuoiKi_QuanLyTiemCaPhe`  
**Ngày thực hiện**: 2026-11-10  

---

## 1. OBSERVATION (QUAN SÁT THỰC TẾ TRONG MÃ NGUỒN)

### 1.1. Nghiệp vụ Quản lý Quán Cafe & Đặt bàn (Table Management)
- **Cơ sở dữ liệu**:
  - Bảng `Tables` trong `CoffeeShopDB_Full.sql` (dòng 110-120):
    ```sql
    CREATE TABLE [dbo].[Tables] (
        [Id]         INT           IDENTITY (1, 1) NOT NULL PRIMARY KEY,
        [TableName]  NVARCHAR (80) NOT NULL,
        [AreaId]     INT           NOT NULL,
        [Status]     NVARCHAR (20) DEFAULT (N'Trống') NULL,
        [CustomerId] VARCHAR (15)  NULL,
        [StartTime]  DATETIME      NULL,
        [EndTime]    DATETIME      NULL,
        FOREIGN KEY ([AreaId]) REFERENCES [dbo].[Areas] ([Id]) ON DELETE CASCADE
    );
    ```
  - Dữ liệu mẫu (dòng 302-309):
    ```sql
    INSERT INTO Tables (TableName, AreaId, Status) VALUES 
    (N'Khách tại quầy / Mang đi', 1, N'Trống'),
    (N'Bàn 01', 1, N'Trống'),
    (N'Bàn 02', 1, N'Trống'),
    (N'Bàn 03', 1, N'Trống'),
    (N'Bàn 04', 2, N'Trống'),
    (N'Bàn 05', 2, N'Trống'),
    (N'Bàn VIP 01', 3, N'Trống');
    ```
- **Hành vi hiển thị trên giao diện `FrmOrder`**:
  - Tại `BLL\TableService.cs` (dòng 33-46), hàm lọc bàn hoạt động:
    ```csharp
    public List<TableDTO> GetActiveReservedOrOccupiedTables()
    {
        var counter = GetOrCreateCounterTable();
        return _tableRepository.GetAllTablesWithArea()
            .Where(t => t.Id != counter.Id &&
                        !t.TableName.Contains("quầy") &&
                        !t.TableName.Contains("Mang đi") &&
                       (string.Equals(t.Status, "Đặt trước", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(t.Status, "Có khách", StringComparison.OrdinalIgnoreCase) ||
                        t.StartTime.HasValue))
            .OrderBy(t => t.StartTime ?? DateTime.MaxValue)
            .ThenBy(t => t.Id)
            .ToList();
    }
    ```
  - Tại `Form\TanPhat\FrmOrder.cs` (dòng 155-173), danh sách bàn `flpTableList` chỉ hiển thị kết quả từ `GetActiveReservedOrOccupiedTables()`.
  - **Hệ quả quan sát**: Khi quán vừa mở cửa, toàn bộ các bàn trong CSDL (`Bàn 01`, `Bàn 02`, `Bàn 03`, `Bàn 04`, `Bàn 05`, `Bàn VIP 01`) đều có `Status = "Trống"` và `StartTime = null`. Do đó điều kiện `Where` loại bỏ 100% các bàn này. Trên giao diện POS bên trái chỉ có duy nhất 1 nút `⚡ KHÁCH TẠI QUẦY / MANG ĐI`. Nhân viên hoàn toàn không thấy danh sách bàn thực tế của quán, không thể click chọn bàn trống để phục vụ khách vào ngồi trực tiếp nếu không qua chức năng "+ Thêm bàn đặt trước".
  - Khi thanh toán xong hóa đơn tại bàn (`OrderRepository.CompleteOrderPayment`, dòng 558-563):
    ```csharp
    if (table != null && table.Id > 1)
    {
        table.Status = "Trống";
        table.StartTime = null;
        table.EndTime = null;
    }
    ```
    Bàn lập tức chuyển về `Status = "Trống"` và biến mất hoàn toàn khỏi danh sách `flpTableList`.

### 1.2. Quy trình Gọi món, Chuyển bàn & Gộp bàn (Ordering Workflow)
- **Gọi món & thêm món**:
  - `Form\TanPhat\FrmOrder.cs` cho phép chọn món từ `UcProductCard` hoặc `btnAddProduct` với `nudQuantity`, thêm vào bàn thông qua `_orderService.AddProductToTableOrder`.
  - Xóa món qua `btnRemoveProduct` gọi `_orderService.RemoveProductFromTableOrder`.
- **Chuyển bàn & Gộp bàn (Switch / Merge Table)**:
  - Mã nguồn đã cài đặt hoàn chỉnh nghiệp vụ backend tại `DAL\OrderRepository.cs` (dòng 582-696) với giao dịch `TransactionScope`, kiểm tra bàn nguồn/đích, gộp danh sách `OrderDetails`, cộng dồn số lượng món trùng, xóa order cũ và tính lại tổng tiền:
    ```csharp
    public bool SwitchOrMergeTable(int sourceTableId, int targetTableId)
    ```
  - Và được chuyển tiếp qua `BLL\OrderService.cs` (dòng 276-317):
    ```csharp
    public bool SwitchOrMergeTable(int sourceTableId, int targetTableId, out string message)
    ```
  - **Hệ quả quan sát**: Kiểm tra toàn bộ `Form\TanPhat\FrmOrder.cs` và `Form\TanPhat\FrmOrder.Designer.cs` cho thấy: **Không tồn tại bất kỳ Button, ContextMenuStrip hay ComboBox nào trên giao diện để thu ngân gọi hàm `SwitchOrMergeTable`**. Tính năng chuyển/gộp bàn hoàn toàn bị "chết" ở tầng UI.

### 1.3. Tính toán Hóa đơn & In ấn (Billing & Invoice Printing)
- **Tính toán hóa đơn (`OrderService.CalculateOrderSummary`)**:
  - Tổng tiền: cộng dồn `TotalPrice` các món trong bảng chi tiết.
  - Giảm giá: hỗ trợ nhập mã KM (Promotion), phần trăm (%) hoặc số tiền VNĐ trực tiếp.
  - Thuế VAT: lấy từ cấu hình `App.config` (`VATPercent`, mặc định 8%).
  - Tiền thanh toán cuối cùng: `finalAmount = totalAmount - discountAmount + vatAmount`.
  - **Thiếu sót quan sát**: Trên `FrmOrder` và `FrmPrintReview`, **hoàn toàn không có ô nhập "Tiền khách đưa" (Cash Tendered) và không tính "Tiền thối lại / Tiền thừa" (Change)**. Trong quy trình nghiệp vụ thực tế của thu ngân tiệm cafe, việc nhập số tiền khách trả bằng tiền mặt (ví dụ 100.000đ, 200.000đ, 500.000đ) và hiển thị số tiền thối lại trên màn hình + in lên bill là yêu cầu cơ bản.
- **In ấn & Xuất hóa đơn**:
  - Được cài đặt tại `Form\TanPhat\FrmPrintReview.cs` và `Utilities\PrintHelper.cs` với 2 chế độ (Dual-tab):
    1. **Tab 1 - RDLC Report**: Sử dụng Microsoft ReportViewer liên kết file `Form\TanPhat\RptInvoiceK80.rdlc`, truyền dữ liệu qua `ReportDataSource("DataSetInvoiceDetails", _orderDetails)` và 12 tham số `ReportParameter` (`pShopName`, `pShopAddress`, `pShopPhone`, `pOrderId`, `pTableName`, `pOrderDate`, `pCashier`, `pTotalAmount`, `pDiscountAmount`, `pVatAmount`, `pFinalAmount`, `pWifiInfo`). Hỗ trợ xuất PDF/Excel/Word chuẩn và in trực tiếp qua EMF rendering (`PrepareRdlcPrintStreams`).
    2. **Tab 2 - Canvas GDI+ Visual Preview**: Sử dụng `PrintHelper.DrawInvoiceK80` vẽ định dạng bill nhiệt K80 (khổ 80mm / 315px) lên `PrintDocument`.
  - Tại `Form\QuangHung\FrmReportWord.cs`: hỗ trợ hiển thị 3 báo cáo RDLC (`RptRevenue.rdlc`, `RptBestSeller.rdlc`, `RptTopCustomers.rdlc`) và xuất sang file Microsoft Word (.docx) qua `WordExportHelper.ExportReportToWord` (`report.Render("WORDOPENXML")`).

### 1.4. Quản lý Kho & Nguyên vật liệu (Warehouse / Inventory)
- **Mã nguồn đã có**:
  - Bảng CSDL: `Materials` và `InventoryTransactions` trong `CoffeeShopDB_Full.sql` (dòng 184-208).
  - Tầng DTO: `InventoryDTO.cs`, `InventoryTransactionDTO.cs`.
  - Tầng DAL: `DAL\InventoryRepository.cs` (có `GetAllMaterials`, `ImportStock`, `ExportStock`, `GetTransactionHistory`).
  - Tầng BLL: `BLL\InventoryService.cs` (có `SaveMaterial`, `DeleteMaterial`, `ImportMaterial`, `ExportMaterial` kiểm tra chống âm kho và cảnh báo định mức tồn kho tối thiểu).
- **Thiếu sót nghiêm trọng quan sát**:
  - Kiểm tra toàn bộ project: **Không có bất kỳ Form nào cho Quản lý Kho / Nguyên vật liệu** (không có `FrmInventory`).
  - `BLL\InventoryService.cs` không được bất kỳ Form nào khởi tạo hay sử dụng (Orphaned Service).

### 1.5. Bảo mật & Xác thực (Security & Authentication)
#### a. Cơ chế băm mật khẩu (Password Hashing)
- Lớp `Utilities\SecurityHelper.cs`: Sử dụng thuật toán PBKDF2 (`Rfc2898DeriveBytes`) với:
  - Salt ngẫu nhiên 16 bytes qua `RandomNumberGenerator.Create()`.
  - Số vòng lặp: 100.000 iterations.
  - Độ dài hash: 32 bytes (256 bits).
  - So sánh mật khẩu bằng kỹ thuật so khớp hằng số thời gian (`difference |= expected[i] ^ actual[i]`) chống tấn công Timing Attack.
- Được áp dụng trong:
  - Đăng nhập: `AuthService.ValidateUser` -> `PermissionService.VerifyPassword`.
  - Tạo tài khoản mới: `PermissionService.CreateUser` (sinh Salt và Hash mới).
  - Đổi mật khẩu: `PermissionService.ChangePassword`.
  - Quên / Reset mật khẩu: `PermissionService.ResetPasswordWithVerification`.

#### b. Lỗ hổng nghiêm trọng ở Dữ liệu mẫu (Seed Data Bug)
- Trong `CoffeeShopDB_Full.sql` (dòng 275-277) và `CoffeeShopDB.sql` (dòng 181-183):
  ```sql
  -- 4. Users (Mật khẩu admin123 & 123456)
  INSERT INTO Users (EmployeeId, Username, PasswordHash, Salt, RoleId, IsActive) VALUES 
  (1, 'admin', 'admin123', '', 1, 1),
  (2, 'thungan', '123456', '', 2, 1);
  ```
- Trong `BLL\PermissionService.cs` (dòng 64-70):
  ```csharp
  if (user == null ||
      string.IsNullOrEmpty(password) ||
      string.IsNullOrEmpty(user.PasswordHash) ||
      string.IsNullOrEmpty(user.Salt))
  {
      return false;
  }
  ```
- **Hậu quả trực tiếp**: Mật khẩu mẫu trong CSDL được lưu ở dạng văn bản thô (Plain Text) `'admin123'` và `'123456'`, đồng thời cột `Salt` để chuỗi rỗng `''`. Khi người dùng chạy script tạo DB mới và đăng nhập bằng tài khoản `admin / admin123` hoặc `thungan / 123456`, hàm `VerifyPassword` phát hiện `user.Salt` rỗng nên **ngay lập tức trả về `false`**. Ngoài ra, nếu có Salt thì `Convert.FromBase64String("admin123")` cũng sẽ văng ngoại lệ `FormatException` vì `'admin123'` không phải chuỗi Base64 hợp lệ của hash 32 bytes. Kết quả là **toàn bộ người dùng mặc định không thể đăng nhập vào hệ thống**.

#### c. Bất cập trong quy trình Đổi / Reset mật khẩu từ Form Phân quyền
- Tại `Form\VanThanh\FrmPermission.cs` (dòng 159-175), khi Quản lý bấm nút "⚙️" -> "Đổi mật khẩu" trên tài khoản nhân viên, mã nguồn mở form `FrmChangePassword`:
  ```csharp
  FrmChangePassword frmChange = new FrmChangePassword(username);
  frmChange.ShowDialog();
  ```
- Nhưng trong `FrmChangePassword.cs` (dòng 30-41): Form yêu cầu bắt buộc phải nhập `txtOldPassword` (mật khẩu cũ của nhân viên).
- **Hậu quả**: Quản lý cấp cao muốn cấp lại mật khẩu cho nhân viên bị quên mật khẩu nhưng không thể thực hiện được vì không thể biết mật khẩu cũ của nhân viên.

#### d. Phân quyền người dùng (Role-Based Access Control - RBAC)
- Bảng `Roles`: 1 (Quản lý), 2 (Thu ngân), 3 (Phục vụ), 4 (Pha chế).
- **Cơ chế phân quyền hiện tại**: Chỉ thực hiện bằng cách ẩn nút trên thanh điều hướng tại `FrmMain.ApplyPermissionsToSidebar()`:
  - Quản lý (RoleId = 1): hiển thị đầy đủ.
  - Thu ngân (RoleId = 2): ẩn `btnNavProduct`, `btnNavCategory`, `btnNavEmployee`, `btnNavPermission`, `btnNavAppConfig`.
  - Pha chế/Phục vụ: ẩn thêm `btnNavPromotion`, `btnNavChart`, `btnNavReport`.
- **Lỗ hổng phân quyền phát hiện**:
  1. **Bỏ sót kiểm tra tầng Form**: Các form nhạy cảm (`FrmEmployee`, `FrmPermission`, `FrmAppConfig`, `FrmProduct`, `FrmCategory`, `FrmChart`, `FrmReportWord`) không hề có bất kỳ câu lệnh kiểm tra quyền nào trong Constructor hoặc sự kiện `Load`. Nếu form được mở từ shortcut hoặc component khác, nhân viên hoàn toàn có thể thao tác.
  2. **Bỏ sót kiểm tra tầng Nghiệp vụ (BLL/DAL)**: Không có tầng kiểm soát phân quyền độc lập tại BLL/DAL.
  3. **Lỗ hổng Default to Admin khi null**: Tại `FrmMain.cs` (dòng 97):
     ```csharp
     int roleId = _currentUser != null ? _currentUser.RoleId : 1;
     ```
     Nếu `FrmMain` được khởi tạo bằng constructor mặc định `public FrmMain() : this(null)`, biến `_currentUser` là null, hệ thống sẽ tự động gán `roleId = 1` (Quản lý) và cho phép truy cập toàn bộ chức năng quản trị!
  4. **Cho phép Quản lý tự xóa chính mình**: Tại `FrmPermission.cs` (dòng 108-158), không có dòng lệnh kiểm tra:
     `if (username == UserSession.CurrentUser.Username) ...`
     Quản lý có thể tự xóa tài khoản của chính mình hoặc xóa tài khoản Quản lý duy nhất trong hệ thống, dẫn đến mất quyền quản trị hoàn toàn.

---

### 1.6. Kiểm toán Lỗ hổng & Chất lượng mã nguồn (Vulnerability & Code Quality)

#### a. Nguy cơ SQL Injection
- **Kết quả rà soát**: Kiểm tra 100% các tệp DAL (`UserRepository.cs`, `OrderRepository.cs`, `ProductRepository.cs`, `TableRepository.cs`, `EmployeeRepository.cs`, `InventoryRepository.cs`, `ShiftRepository.cs`, `ReportRepository.cs`, `CustomerRepository.cs`, `RecipeRepository.cs`).
- **Đánh giá**: Toàn bộ thao tác truy vấn CSDL đều sử dụng LINQ to SQL DataContext (`CoffeeShopDataContextDataContext`). Không tồn tại câu lệnh ghép chuỗi SQL thô (`SqlCommand`, `ExecuteNonQuery`, `ExecuteReader`, `db.ExecuteCommand("SELECT ... " + input)`). Do đó, **nguy cơ SQL Injection là KHÔNG CÓ (Zero / Safe by Design)**.

#### b. Lỗi sập ứng dụng không được xử lý (Unhandled Exception / Crash Vectors)
- **Crash Vector 1 - Xóa nhân viên dính khóa ngoại (Foreign Key Constraint Crash)**:
  - Tại `DAL\EmployeeRepository.cs` (dòng 161-174):
    ```csharp
    public bool DeleteEmployee(int employeeId)
    {
        using (var db = GetContext())
        {
            var existing = db.Employees.FirstOrDefault(e => e.Id == employeeId);
            if (existing != null)
            {
                db.Employees.DeleteOnSubmit(existing);
                db.SubmitChanges();
                return true;
            }
            return false;
        }
    }
    ```
  - Trong hàm này **hoàn toàn không có khối `try-catch`**.
  - Các bảng `Orders`, `EmployeeShifts`, `InventoryTransactions` đều có khóa ngoại trỏ tới `Employees(Id)` mà không có `ON DELETE CASCADE`.
  - Tại `Form\VanThanh\FrmEmployee.cs` (dòng 186-187), hàm `ExecuteDeleteAction` gọi `_employeeService.DeleteEmployee` cũng **không có `try-catch`**.
  - **Hệ quả**: Khi người dùng nhấn xóa bất kỳ nhân viên nào đã từng tạo đơn hàng, có ca trực hoặc có phiếu kho, `db.SubmitChanges()` lập tức ném ra ngoại lệ `SqlException` (The DELETE statement conflicted with the REFERENCE constraint) làm toàn bộ chương trình văng tắt đột ngột (Crash).
- **Crash Vector 2 - Khóa tệp hình ảnh khi chọn ảnh đại diện (File Lock Crash)**:
  - Tại `UserControl\UCEmployeeAdd.cs` (dòng 149) và `UserControl\UCEmployeeEdit.cs` (dòng 59):
    ```csharp
    ptbAvatar.Image = Image.FromFile(_imagePath);
    ```
  - `Image.FromFile` giữ khóa tệp (File Lock) độc quyền trên ổ đĩa. Nếu tệp ảnh bị sửa, xóa hoặc chọn lại, ứng dụng sẽ văng ngoại lệ `System.IO.IOException`. Đồng thời biến `_imagePath` lưu đường dẫn tuyệt đối (`ofd.FileName`) vi phạm tiêu chí giảng viên.

#### c. Lỗi kiểm tra dữ liệu (Data Validation Bugs)
- **Lỗi ép kiểu chuỗi không an toàn (`int.Parse`)**:
  - Tại `Form\TuanHung\FrmCategory.cs` dòng 84 (`int id = int.Parse(txtCategoryId.Text);`) và dòng 102 (`int.Parse(txtCategoryId.Text)`).
  - Tại `Form\TuanHung\FrmProduct.cs` dòng 143 và dòng 173 (`int.Parse(txtProductId.Text)`).
  - Tại `Form\TuanHung\FrmPromotion.cs` dòng 99 và 127 (`int.Parse(txtPromoId.Text)`).
  - Nếu TextBox bị trống hoặc người dùng thao tác ngoài ý muốn, `int.Parse` ném ngoại lệ `FormatException`. Phải thay bằng `int.TryParse`.
- **Lỗi số âm trong Điểm thưởng Khách hàng (`RewardPoints`)**:
  - Tại `Form\QuangHung\FrmCustomer.cs` dòng 60:
    ```csharp
    int points = int.TryParse(txtRewardPoints.Text.Trim(), out int pts) ? pts : 0;
    ```
  - Nếu người dùng nhập số âm (ví dụ `-50`), `pts` nhận giá trị âm. Lớp `BLL\CustomerService.cs` (hàm `Add` và `Update`) không kiểm tra `RewardPoints >= 0`. Khi lưu xuống DB, câu lệnh vi phạm ràng buộc `CHECK ([RewardPoints] >= 0)` trong bảng `Customers` gây lỗi `SqlException`.
- **Lỗi tìm nhân viên theo Họ tên dạng chuỗi trong Phân ca (`FrmShiftSchedule`)**:
  - Tại `Form\VanThanh\FrmShiftSchedule.cs` dòng 216:
    ```csharp
    var emp = _empRepo.GetEmployeeByName(empName);
    ```
  - ComboBox trên DataGridView lưu chuỗi họ tên thay vì `EmployeeId`. Nếu quán có 2 nhân viên trùng họ tên (ví dụ cùng tên "Nguyễn Văn A"), hệ thống sẽ luôn gán toàn bộ ca cho nhân viên đầu tiên tìm thấy.

#### d. Vi phạm quy định bắt buộc của Giảng viên: LINQ Lambda vs LINQ Query Syntax
- **Tiêu chí chấm điểm**: `LINQ Lambda: 3 điểm` ("100% các thao tác truy vấn dữ liệu CRUD trên cơ sở dữ liệu sử dụng cú pháp LINQ Lambda, không còn câu lệnh SQL thô hoặc LINQ Query syntax trong mã nguồn").
- **Thực tế quan sát**: Có rất nhiều truy vấn trong DAL vẫn viết bằng cú pháp LINQ Query Syntax (`from ... in ... join ... select ...`):
  - `DAL\UserRepository.cs` (dòng 41-58): hàm `GetAllUsersWithRoles()`.
  - `DAL\EmployeeRepository.cs` (dòng 24-46, 51-73, 79-102, 181-193): các hàm `GetAllEmployees()`, `GetEmployeeById()`, `GetEmployeesWithoutAccount()`.
  - `DAL\ProductRepository.cs` (dòng 94-110, 118-134, 140-156): các hàm `GetAllProducts()`, `GetProductsByCategory()`, `SearchProductsByName()`.
  - `DAL\OrderRepository.cs` (dòng 315-333, 337-355): các hàm `GetOrderDetailsByOrderId()`.
  - `DAL\RecipeRepository.cs` (dòng 22-38): hàm `GetRecipesByProductId()`.
  - `DAL\TableRepository.cs` (dòng 153-168, 173-186): các hàm `GetAllTablesWithArea()`.
  - `DAL\InventoryRepository.cs` (dòng 145-163): hàm `GetTransactionHistory()`.
- **Đánh giá**: Nếu không chuyển đổi toàn bộ các khối lệnh này sang cú pháp LINQ Lambda Extension Methods (`.Where()`, `.Select()`, `.Join()`, `.OrderBy()`), dự án sẽ bị trừ điểm nặng ở tiêu chí 3 điểm này.

#### e. Vi phạm quy định bắt buộc: Tạo Form động từ Code-behind (`ShowAdminAuthDialog`)
- **Tiêu chí bắt buộc**: "Bắt buộc có Design form (không dùng AI generate từ behind code)".
- **Thực tế quan sát**: Tại `Form\VanThanh\FrmForgotPassword.cs` (dòng 71-153):
  ```csharp
  // 4. HIỂN THỊ HỘP THOẠI XÁC THỰC QUẢN LÝ NGAY TRONG CODE (KHÔNG CẦN TẠO FORM MỚI)
  private bool ShowAdminAuthDialog()
  {
      using (Form authForm = new Form())
      {
          authForm.Width = 420;
          ...
          authForm.Controls.Add(lblNotice);
          ...
          authForm.ShowDialog();
          ...
      }
  }
  ```
  Hàm này tự khởi tạo và cấu hình một Form hoàn chỉnh trực tiếp từ code-behind mà không có file Designer. Điều này vi phạm trực tiếp lưu ý của giảng viên.

#### f. Thiếu thư viện giao diện bên thứ ba (UI 3rd Party)
- **Tiêu chí chấm điểm**: `UI 3rd party: 1 điểm`.
- **Thực tế quan sát**: Trong `packages.config` và file `.csproj`, hoàn toàn không có gói thư viện UI bên thứ ba nào (như Guna.UI2, Bunifu, Krypton Suite, MaterialSkin, v.v.). Hệ thống đang dùng 100% WinForms chuẩn kết hợp vẽ màu qua `UIHelper.cs`.

---

## 2. LOGIC CHAIN (CHUỖI LẬP LUẬN TỪ QUAN SÁT ĐẾN KẾT LUẬN)

1. **Từ Quan sát 1.1**:
   - `TableService.GetActiveReservedOrOccupiedTables` chỉ lấy các bàn có trạng thái "Đặt trước", "Có khách" hoặc có `StartTime`.
   - CSDL khởi tạo mặc định tất cả các bàn thực tế của quán (`Bàn 01` đến `Bàn VIP 01`) với trạng thái "Trống" và `StartTime = null`.
   - $\rightarrow$ **Suy luận**: Người dùng khi mở phần mềm POS sẽ không nhìn thấy bất kỳ chiếc bàn nào của quán để phục vụ khách đến uống cafe trực tiếp tại quán (Dine-in). Bàn sau khi thanh toán xong chuyển về "Trống" cũng biến mất luôn. Đây là khiếm khuyết nghiệp vụ cốt lõi của phần mềm Quản lý quán cafe. Cần bổ sung sơ đồ bàn trực quan (hiển thị đầy đủ tất cả các bàn trong quán theo từng khu vực, tô màu phân biệt Trống / Có người / Đặt trước).

2. **Từ Quan sát 1.2**:
   - Backend DAL/BLL đã có hàm `SwitchOrMergeTable` đầy đủ logic nghiệp vụ chuyển bàn và gộp bàn.
   - Nhưng giao diện `FrmOrder` không hề có nút bấm nào để gọi hàm này.
   - $\rightarrow$ **Suy luận**: Chức năng Chuyển bàn / Gộp bàn bị đứt gãy giữa Frontend và Backend. Cần bổ sung nút bấm và hộp thoại chọn bàn chuyển/gộp trên `FrmOrder`.

3. **Từ Quan sát 1.3**:
   - `OrderService.CalculateOrderSummary` chỉ tính `totalAmount`, `discountAmount`, `vatAmount`, `finalAmount`.
   - Không có trường hoặc điều khiển cho "Tiền khách đưa" và "Tiền thối lại".
   - $\rightarrow$ **Suy luận**: Quy trình thanh toán thiếu bước nghiệp vụ tiền mặt. Cần bổ sung ô nhập tiền khách đưa, tự động tính tiền thối và thể hiện trên giao diện lẫn phiếu in K80.

4. **Từ Quan sát 1.4**:
   - CSDL, DTO, Repository, Service của Kho nguyên liệu đã được lập trình sẵn với nghiệp vụ chống âm kho.
   - Nhưng không có form giao diện nào để người dùng quản lý kho.
   - $\rightarrow$ **Suy luận**: Nghiệp vụ quản lý kho nguyên vật liệu chưa hoàn thiện ở tầng trình diễn UI. Cần tạo form quản lý kho chuẩn Designer để kết nối `InventoryService`.

5. **Từ Quan sát 1.5b**:
   - CSDL seed mật khẩu là chuỗi thường `'admin123'` và `'123456'` với `Salt = ''`.
   - Hàm `VerifyPassword` trả về `false` ngay khi `Salt` rỗng, hoặc văng lỗi `FormatException` khi giải mã Base64.
   - $\rightarrow$ **Suy luận**: Dự án ở trạng thái hiện tại sẽ bị liệt chức năng Đăng nhập nếu chạy trên CSDL mới tạo từ file script. Cần cập nhật script SQL và cơ chế khởi tạo với Hash và Salt PBKDF2 chuẩn.

6. **Từ Quan sát 1.5d**:
   - Phân quyền chỉ là `Visible = false` ở menu, nếu constructor không truyền user sẽ default về RoleId = 1 (Quản lý).
   - $\rightarrow$ **Suy luận**: Cơ chế RBAC còn lỏng lẻo, dễ bị bypass và tiềm ẩn rủi ro an ninh khi người dùng xóa nhầm tài khoản của chính mình. Cần chặn tự xóa chính mình và siết chặt kiểm tra phiên làm việc.

7. **Từ Quan sát 1.6b**:
   - `EmployeeRepository.DeleteEmployee` không có `try-catch` và không kiểm tra ràng buộc khóa ngoại với đơn hàng/ca trực.
   - $\rightarrow$ **Suy luận**: Bất kỳ thao tác xóa nhân viên đã có dữ liệu liên quan sẽ làm sập chương trình. Cần bọc `try-catch`, kiểm tra ràng buộc hoặc sử dụng cờ vô hiệu hóa (`IsActive = false`) thay vì xóa cứng (Hard Delete).

8. **Từ Quan sát 1.6d**:
   - Rất nhiều câu lệnh truy vấn CSDL trong 7 repository vẫn dùng LINQ Query Syntax (`from ... join ... select`).
   - Giảng viên yêu cầu bắt buộc 100% LINQ Lambda (3 điểm).
   - $\rightarrow$ **Suy luận**: Đây là nguy cơ mất điểm trực tiếp cao nhất trong đồ án. Toàn bộ các câu lệnh Query Syntax phải được refactor đồng loạt sang LINQ Lambda Extension Methods.

---

## 3. CAVEATS (CÁC ĐIỂM CẦN LƯU Ý & GIỚI HẠN)

1. **Khả năng tương thích cơ sở dữ liệu**:
   - File cấu hình `App.config` hiện đang để `Data Source=.`, `Integrated Security=True`. Lớp `ConfigHelper.cs` đã có cơ chế tự động dò tìm các instance cục bộ (`.`, `localhost`, `.\SQLEXPRESS`, `(localdb)\MSSQLLocalDB`), giúp ứng dụng chạy linh hoạt trên nhiều máy chấm điểm khác nhau.
2. **Chế độ chạy Offline (Fallback Mode)**:
   - Các file `TableRepository.cs`, `OrderRepository.cs` có chứa biến tĩnh Fallback (`FallbackTables`, `FallbackOrders`) để hiển thị dữ liệu mẫu khi SQL Server chưa bật. Khi sửa mã nguồn cần đảm bảo giữ tính nhất quán giữa chế độ kết nối SQL thật và chế độ Fallback này.
3. **Thư viện bên thứ ba**:
   - Nếu cài đặt thêm thư viện UI bên thứ ba (như Guna.UI2), cần đảm bảo DLL được nhúng chuẩn vào thư mục build hoặc qua NuGet packages.config để không gây lỗi thiếu DLL khi giảng viên mang sang máy khác chấm.

---

## 4. CONCLUSION (KẾT LUẬN & ĐỀ XUẤT HÀNH ĐỘNG CỤ THỂ)

### Bảng tổng hợp đánh giá theo Ma trận tiêu chí của Giảng viên

| Tiêu chí | Điểm tối đa | Hiện trạng mã nguồn | Mức độ rủi ro | Hành động khắc phục bắt buộc |
| :--- | :---: | :--- | :---: | :--- |
| **LINQ Lambda** | **3.0** | Rất nhiều file DAL còn dùng LINQ Query Syntax (`from ... in ... select`) | **CỰC KỲ CAO** | Chuyển đổi 100% tất cả các truy vấn trong toàn bộ thư mục `DAL\` sang cú pháp Lambda (`.Where()`, `.Select()`, `.Join()`, `.OrderBy()`). |
| **15 Form có Design** | **2.0** | Dự án có 18 form có Designer, nhưng form xác thực quyền quản lý trong `FrmForgotPassword` đang sinh động qua behind-code | **TRUNG BÌNH** | Tách `ShowAdminAuthDialog` thành một Form con có file Designer chuẩn (ví dụ `FrmAdminAuth.cs`). |
| **Mã hóa & Phân quyền** | **3.0** | Thuật toán PBKDF2 rất tốt, nhưng dữ liệu mẫu SQL bị lưu Plain Text làm hỏng đăng nhập; phân quyền chưa chặn tự xóa admin | **CỰC KỲ CAO** | Cập nhật file SQL với Hash + Salt PBKDF2 chuẩn; thêm kiểm tra không cho phép tự xóa tài khoản đang đăng nhập; siết RBAC. |
| **Nghiệp vụ + In ấn** | **2.0** | In ấn K80 (RDLC + GDI+) và xuất Word rất hoàn thiện; nhưng thiếu hiển thị bàn Trống, thiếu nút Chuyển/Gộp bàn, thiếu Tiền khách đưa/Tiền thối, thiếu Form Kho | **CAO** | 1. Sửa `FrmOrder` hiển thị sơ đồ tất cả các bàn trong quán (Trống / Có khách / Đặt trước).<br>2. Thêm nút Chuyển/Gộp bàn gọi `SwitchOrMergeTable`.<br>3. Bổ sung ô Tiền khách đưa & Tiền thối.<br>4. Tạo `FrmInventory` để quản lý nhập/xuất kho. |
| **App.config** | **1.0** | Đã cấu hình connectionString và appSettings chuẩn, có `FrmAppConfig` quản lý | **AN TOÀN** | Đạt yêu cầu. |
| **UI 3rd party** | **1.0** | Chưa tích hợp gói UI bên thứ ba nào | **CAO** | Tích hợp thư viện UI bên thứ ba (hoặc Guna.UI2 / Krypton) cho các nút bấm, dashboard. |
| **Chart** | **1.0** | Đã có 2 biểu đồ Chart (Cột doanh thu & Tròn Top sản phẩm) trong `FrmChart` | **AN TOÀN** | Đạt yêu cầu. |
| **Đóng gói** | **1.0** | Dự án có cấu hình ClickOnce publish | **AN TOÀN** | Đạt yêu cầu. |
| **Địa chỉ tương đối** | **Lưu ý BB** | Sản phẩm dùng tương đối, nhưng Nhân viên (`UCEmployeeAdd`) vẫn lưu đường dẫn tuyệt đối của ảnh | **TRUNG BÌNH** | Chuẩn hóa toàn bộ ảnh nhân viên sang thư mục tương đối `Images\Employees\` bằng `UIHelper.LoadImageToPictureBox`. |

---

## 5. VERIFICATION METHOD (PHƯƠNG PHÁP KIỂM CHỨNG ĐỘC LẬP)

1. **Kiểm chứng Lỗi Đăng nhập Dữ liệu mẫu**:
   - Chạy lệnh SQL từ `CoffeeShopDB_Full.sql` trên SQL Server.
   - Mở ứng dụng, nhập `admin` / `admin123` $\rightarrow$ Kết quả: Báo lỗi "Tên đăng nhập hoặc mật khẩu không chính xác" do `Salt` bị rỗng.
   - Sửa file SQL bằng cách cập nhật chuỗi Hash và Salt PBKDF2 hợp lệ được tạo bởi `SecurityHelper` $\rightarrow$ Đăng nhập thành công.
2. **Kiểm chứng Lỗi Bàn trống không xuất hiện trên POS**:
   - Mở `FrmOrder`, quan sát cột danh sách bàn bên trái $\rightarrow$ Chỉ thấy nút Khách tại quầy, hoàn toàn không thấy `Bàn 01`, `Bàn 02`, `Bàn 03` dù trong DB có 6 bàn.
3. **Kiểm chứng Thiếu nút Chuyển/Gộp bàn**:
   - Tìm kiếm từ khóa `SwitchOrMergeTable` trên toàn bộ thư mục `Form\` $\rightarrow$ Không có kết quả nào (chỉ có trong BLL và DAL).
4. **Kiểm chứng LINQ Query Syntax**:
   - Chạy PowerShell:
     ```powershell
     Select-String -Path "d:\DoAnCuoiKi_QuanLyTiemCaPhe\DAL\*.cs" -Pattern "from .* in .* select"
     ```
     Sẽ thấy hàng loạt kết quả trả về từ `UserRepository.cs`, `EmployeeRepository.cs`, `ProductRepository.cs`, `OrderRepository.cs`, `TableRepository.cs`.
5. **Kiểm chứng Crash Vector Xóa nhân viên**:
   - Thêm 1 đơn hàng gắn với nhân viên ID = 1.
   - Vào `FrmEmployee`, bấm ⚙️ -> Xóa nhân viên ID = 1 $\rightarrow$ Ngoại lệ `SqlException` không được bắt làm sập ứng dụng.
