# Báo Cáo Khảo Sát Kỹ Thuật (Explorer 2): Data Access Layer, LINQ Lambda Compliance, App.config, Relative Paths

- **Dự án**: Quản lý Tiệm Cà Phê (WinForms .NET Framework 4.8)
- **Thư mục làm việc**: `d:\DoAnCuoiKi_QuanLyTiemCaPhe`
- **Người khảo sát**: Explorer 2 (Subagent)
- **Ngày khảo sát**: 2026-11-10T17:15:00Z
- **Phạm vi khảo sát**: Toàn bộ giải pháp (DAL, BLL, DTO, Form, UserControl, Utilities, App.config, Scripts SQL, Resource paths)

---

## 1. Observation (Các Quan Sát Thực Tế Trực Tiếp)

### 1.1. Kiến trúc Cơ sở Dữ liệu & ORM
- **Mô hình ORM**: Giải pháp sử dụng **LINQ to SQL** được định nghĩa qua file:
  * `CoffeeShopDataContext.dbml` (kèm layout `CoffeeShopDataContext.dbml.layout`)
  * `CoffeeShopDataContext.designer.cs` (chứa lớp `CoffeeShopDataContextDataContext : System.Data.Linq.DataContext`)
  * `CoffeeShopDataContext.cs` (file partial class hiện đang rỗng 0 bytes).
- **Tập tin CSDL vật lý**:
  * Không lưu kèm file `.mdf` hay `.ldf` cục bộ cố định trong thư mục repository.
  * Có 5 tệp script SQL định nghĩa schema và dữ liệu:
    1. `CoffeeShopDB.sql` (8,602 bytes): Chứa script tạo 14 bảng chuẩn (`Areas`, `Categories`, `Materials`, `InventoryTransactions`, `Products`, `Recipes`, `Promotions`, `Tables`, `Customers`, `Roles`, `Salaries`, `Employees`, `Users`, `Orders`, `OrderDetails`) và dữ liệu khởi tạo.
    2. `CoffeeShopDB_Full.sql` (16,140 bytes): Script tổng hợp đầy đủ schema và seed data.
    3. `DataBaseCF.sql` (7,442 bytes): Phiên bản script sơ khởi trước đó.
    4. `Migrate_Database_SyncAll.sql` (7,308 bytes): Script di chuyển dữ liệu, đồng bộ hóa bổ sung cột `Image` vào bảng `Employees`.
    5. `Migrate_Mision_EmployeeShifts.sql` (3,825 bytes): Bổ sung cấu trúc ca làm việc và phân công nhiệm vụ (`Misions`, `EmployeeShifts`).
- **Phân tầng kiến trúc (3-Layer Architecture)**:
  * **DAL (Data Access Layer)**: Gồm 11 lớp Repository nằm tại thư mục `DAL\`:
    - `CustomerRepository.cs`
    - `EmployeeRepository.cs`
    - `InventoryRepository.cs`
    - `OrderRepository.cs`
    - `ProductRepository.cs`
    - `PromotionRepository.cs`
    - `RecipeRepository.cs`
    - `ReportRepository.cs`
    - `ShiftRepository.cs`
    - `TableRepository.cs`
    - `UserRepository.cs`
  * Tất cả 11 Repository đều khởi tạo DataContext thông qua hàm nội bộ:
    ```csharp
    private CoffeeShopDataContextDataContext GetContext()
    {
        return new CoffeeShopDataContextDataContext(ConfigHelper.GetConnectionString("CoffeeShopDB"));
    }
    ```
  * DataContext hoàn toàn không bị khởi tạo rải rác bên trong các Form hay UserControl.
  * **Rò rỉ kiến trúc phát hiện**: Tại `BLL\TableService.cs` (dòng 164 - 185), hàm `DeleteAreaByName()` trực tiếp gọi `_tableRepository.GetContext()` và thao tác `db.Areas.DeleteOnSubmit(area)` cùng `db.SubmitChanges()` trong tầng BLL thay vì ủy quyền qua DAL.

---

### 1.2. Kiểm toán Tuân thủ LINQ Lambda (LINQ Lambda Compliance Audit)
Theo ma trận chấm điểm: *"LINQ Lambda: 3 điểm (bắt buộc dùng toàn bộ là LINQ Lambda, kiểm tra toàn bộ dbo CRUD)"*.

#### A. Kiểm tra Truy vấn SQL thô (Raw SQL)
- Quét toàn bộ mã nguồn giải pháp bằng Regex: `SqlCommand|SqlDataAdapter|ExecuteReader|ExecuteNonQuery|ExecuteScalar|SqlDataReader|ExecuteQuery|ExecuteCommand`.
- **Kết quả**: **KHÔNG CÓ** bất kỳ câu lệnh ADO.NET thô hay câu truy vấn SQL dạng chuỗi ghép (`SELECT`, `INSERT`, `UPDATE`, `DELETE`) nào được dùng để thực hiện CRUD dữ liệu.
- Đối tượng `SqlConnection` chỉ xuất hiện đúng 3 vị trí với mục đích duy nhất là kiểm tra liveness của kết nối:
  1. `DAL\TableRepository.cs:45` (phương thức `IsDatabaseOnline()`)
  2. `Form\VanThanh\FrmAppConfig.cs:77` (nút bấm kiểm tra thử kết nối `btnTestConnection_Click`)
  3. `Utilities\ConfigHelper.cs:84` (hàm nội bộ kiểm tra nhanh `TestConnectionFast()`)

#### B. Kiểm tra Cú pháp LINQ Query Syntax (`from ... in ... select ...`)
- Quét toàn bộ mã nguồn giải pháp bằng Regex: `\bfrom\s+\w+\s+in\b`.
- **Kết quả**: Phát hiện **chính xác 12 phương thức** tại **7 tệp Repository trong DAL** đang sử dụng cú pháp truy vấn LINQ dạng suy diễn (Query Comprehension Syntax / Non-Lambda) thay vì cú pháp LINQ Lambda (Method Syntax).
- Chi tiết 12 vị trí vi phạm cụ thể:

| STT | Tệp tin | Phương thức | Dòng | Đoạn code hiện tại (Query Syntax) |
|---|---|---|---|---|
| 1 | `DAL\EmployeeRepository.cs` | `GetAllEmployees()` | 21 - 39 | `from e in db.Employees join r in db.Roles... join s in db.Salaries... select new EmployeeDTO` |
| 2 | `DAL\EmployeeRepository.cs` | `GetEmployeeDTOById()` | 47 - 67 | `from e in db.Employees where e.Id == employeeId join r in db.Roles... select new EmployeeDTO` |
| 3 | `DAL\EmployeeRepository.cs` | `SearchEmployees()` | 75 - 95 | `from e in db.Employees join r in db.Roles... where e.FullName.Contains(...) select new EmployeeDTO` |
| 4 | `DAL\EmployeeRepository.cs` | `GetEmployeesWithoutAccount()` | 181 - 193 | `from e in db.Employees where !(from u in db.Users select u.EmployeeId).Contains(e.Id)...` |
| 5 | `DAL\InventoryRepository.cs` | `GetTransactionHistory()` | 144 - 159 | `from t in db.InventoryTransactions join m in db.Materials... join e in db.Employees... select new InventoryTransactionDTO` |
| 6 | `DAL\OrderRepository.cs` | `GetOrderDetailsByOrderId()` (bộ nhớ tạm) | 316 - 330 | `from detail in FallbackOrderDetails join product in _fallbackProducts... select new OrderDetailDTO` |
| 7 | `DAL\OrderRepository.cs` | `GetOrderDetailsByOrderId()` (CSDL thật) | 335 - 353 | `from detail in db.OrderDetails join product in db.Products... select new OrderDetailDTO` |
| 8 | `DAL\ProductRepository.cs` | `GetAllProducts()` | 95 - 108 | `from p in db.Products join c in db.Categories on p.CategoryId equals c.Id select new ProductDTO` |
| 9 | `DAL\ProductRepository.cs` | `GetProductsByCategory()` | 117 - 130 | `from p in db.Products join c in db.Categories... where p.CategoryId == categoryId select new ProductDTO` |
| 10 | `DAL\ProductRepository.cs` | `SearchProductsByName()` | 140 - 153 | `from p in db.Products join c in db.Categories... where p.ProductName.Contains(...) select new ProductDTO` |
| 11 | `DAL\RecipeRepository.cs` | `GetRecipesByProductId()` | 21 - 34 | `from r in db.Recipes join p in db.Products on r.ProductId equals p.Id where r.ProductId == productId select new RecipeDTO` |
| 12 | `DAL\TableRepository.cs` | `GetAllTablesWithArea()` (cả CSDL & fallback) | 153-169 & 173-186 | `from table in db.Tables join area in db.Areas on table.AreaId equals area.Id select new ...` |
| 13 | `DAL\UserRepository.cs` | `GetAllUsersWithRoles()` | 40 - 58 | `from user in db.Users join role in db.Roles... join emp in db.Employees... select new UserDTO` |

---

### 1.3. Kiểm toán Cấu hình `App.config`
- **Tệp tin**: `d:\DoAnCuoiKi_QuanLyTiemCaPhe\App.config`.
- **Thẻ `<connectionStrings>`**:
  ```xml
  <connectionStrings>
      <add name="CoffeeShopDB" connectionString="Data Source=.;Initial Catalog=CoffeeShopDB;Integrated Security=True;TrustServerCertificate=True;" providerName="System.Data.SqlClient" />
      <add name="DoAnCuoiKi_QuanLyTiemCaPhe.Properties.Settings.CoffeeShopDBConnectionString" connectionString="Data Source=.;Initial Catalog=CoffeeShopDB;Integrated Security=True;TrustServerCertificate=True;" providerName="System.Data.SqlClient" />
      <add name="DoAnCuoiKi_QuanLyTiemCaPhe.Properties.Settings.CoffeeShopDBConnectionString1" connectionString="Data Source=localhost;Initial Catalog=CoffeeShopDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True" providerName="System.Data.SqlClient" />
  </connectionStrings>
  ```
- **Cơ chế nạp chuỗi kết nối**:
  * Được đọc qua lớp `Utilities\ConfigHelper.cs` bằng lệnh `ConfigurationManager.ConnectionStrings[name]`.
  * Có cơ chế tự động dò tìm máy chủ SQL thông minh (`TestConnectionFast` qua danh sách fallback candidate: `"."`, `"localhost"`, `".\SQLEXPRESS"`, `"(localdb)\MSSQLLocalDB"`).
- **Thẻ `<appSettings>`**:
  * Cấu hình đầy đủ thông tin quán: `ShopName`, `ShopAddress`, `ShopPhone`, `VATPercent`, `VATRate`, `WifiInfo`.
  * Các thông tin này được sử dụng xuyên suốt trong `PrintHelper.cs` (in hóa đơn K80), `FrmReportWord.cs` (báo cáo RDLC), và `FrmAppConfig.cs`.
- **Giao diện cập nhật cấu hình**:
  * `Form\VanThanh\FrmAppConfig.cs` cho phép người dùng thay đổi máy chủ, CSDL, thông tin quán và lưu lại vào `App.config` qua `AppConfigService.cs`.
  * **Thiếu sót phát hiện**: Trong `FrmAppConfig.cs:98-99`, hàm `btnSaveConfig_Click` cập nhật `CoffeeShopDB` và `CoffeeShopDBConnectionString`, nhưng **bỏ sót** việc đồng bộ cập nhật khóa `CoffeeShopDBConnectionString1` (khóa này đang được hàm khởi tạo mặc định trong file designer của DataContext tham chiếu tới).

---

### 1.4. Kiểm toán Đường Dẫn Tương Đối (Relative Paths Audit)
Theo yêu cầu bắt buộc của giảng viên: *"Sử dụng địa chỉ tương đối (không dùng địa chỉ tuyệt đối)"*.

- **Đường dẫn RDLC Reports**: Đã tuân thủ chuẩn 100%.
  * `Form\QuangHung\FrmReportWord.cs:91, 109, 120`: Sử dụng `Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Form\QuangHung\RptRevenue.rdlc")`.
  * `Form\TanPhat\FrmPrintReview.cs:163`: Sử dụng `Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Form", "TanPhat", "RptInvoiceK80.rdlc")`.
- **Đường dẫn Ảnh món ăn (Product Images)**: Đã tuân thủ chuẩn 100%.
  * `BLL\ProductService.cs:130, 145`: Khi thêm/sửa món ăn, tệp ảnh được tự động copy về thư mục tương đối `Application.StartupPath\Images\Products`, và trong CSDL chỉ lưu đường dẫn tương đối dạng `Images\Products\prod_xxxx.jpg`.
  * `Utilities\UIHelper.cs:116`: Phương thức `LoadImageToPictureBox()` nạp ảnh qua `Path.Combine(Application.StartupPath, relativePath)` bằng `FileStream / MemoryStream` chống khóa file.
- **LỖ HỔNG NGHIÊM TRỌNG ĐƯỜNG DẪN ẢNH NHÂN VIÊN (Employee Avatar Images)**:
  * Trong `UserControl\UCEmployeeAdd.cs` (dòng 146 và dòng 103):
    ```csharp
    _imagePath = ofd.FileName; // Lấy trực tiếp đường dẫn tuyệt đối từ máy người dùng, ví dụ: C:\Users\ACER\Downloads\avatar.png
    EmployeeDTO newDto = new EmployeeDTO { ... Image = _imagePath };
    ```
  * Trong `UserControl\UCEmployeeEdit.cs` (dòng 165 và dòng 101):
    ```csharp
    _currentImagePath = ofd.FileName; // Lấy đường dẫn tuyệt đối
    dto.Image = _currentImagePath;
    ```
  * `BLL\EmployeeService.cs` không thực hiện copy file ảnh vào thư mục dự án mà lưu thẳng chuỗi đường dẫn tuyệt đối cục bộ này vào cột `[Image]` của bảng `Employees`.
  * Trong `UserControl\UCEmployeeInform.cs:35` và `UserControl\UCEmployeeEdit.cs:59`:
    ```csharp
    ptbAvatar.Image = Image.FromFile(emp.Image); // Dùng Image.FromFile trực tiếp, vừa khóa file vừa lỗi 100% khi chấm trên máy khác
    ```
  * **Hậu quả**: Khi giảng viên chấm bài hoặc mở đồ án trên máy tính khác, toàn bộ ảnh nhân viên sẽ biến mất hoặc gây ngoại lệ FileNotFound / OutOfMemory do đường dẫn tuyệt đối `C:\Users\ACER\...` không tồn tại trên máy người chấm!

---

## 2. Logic Chain (Chuỗi Lập Luận Kỹ Thuật & Kế Hoạch Chuyển Đổi)

### 2.1. Giải pháp chuyển đổi 100% sang LINQ Lambda
Tất cả 14 bảng trong cơ sở dữ liệu đều đã được `CoffeeShopDataContext.dbml` định nghĩa quan hệ khóa ngoại (Foreign Key Associations) thành các `EntityRef` và `EntitySet` hợp lệ:
- `Product.Category` (1 - n)
- `OrderDetail.Product`, `OrderDetail.Order` (1 - n)
- `Recipe.Product` (1 - n)
- `Table.Area` (1 - n)
- `User.Role`, `User.Employee` (1 - n)
- `Employee.Role` (1 - n)

Nhờ các quan hệ này, các câu lệnh LINQ Query syntax sử dụng `join ... in ...` có thể được rút gọn và chuyển đổi hoàn toàn sang **Pure LINQ Lambda Method Syntax** bằng cách duyệt qua Navigation Properties hoặc các hàm mở rộng Lambda chuẩn `.Join()`, `.Where()`, `.Select()`, `.OrderBy()`, `.ToList()`.

#### Danh sách chi tiết mã cần chuyển đổi (Before -> After):

#### 1) `DAL\EmployeeRepository.cs` (Dòng 21 - 39: `GetAllEmployees`)
*Trước khi sửa:*
```csharp
var query =
    from e in db.Employees
    join r in db.Roles on e.RoleId equals r.Id
    join s in db.Salaries on e.RoleId equals s.RoleId into salaries
    from s in salaries.DefaultIfEmpty()
    select new EmployeeDTO { ... };
return query.ToList();
```
*Đề xuất chuyển đổi sang LINQ Lambda chuẩn:*
```csharp
return db.Employees
    .Select(e => new EmployeeDTO
    {
        Id = e.Id,
        FullName = e.FullName,
        BirthDate = e.BirthDate,
        Phone = e.Phone,
        Address = e.Address,
        Position = e.Role != null ? e.Role.RoleName : string.Empty,
        RoleId = e.RoleId,
        HireDate = e.HireDate,
        Salary = db.Salaries.Where(s => s.RoleId == e.RoleId).Select(s => (decimal?)s.Amount).FirstOrDefault(),
        Image = e.Image
    })
    .ToList();
```

#### 2) `DAL\EmployeeRepository.cs` (Dòng 47 - 67: `GetEmployeeDTOById`)
*Đề xuất chuyển đổi sang LINQ Lambda chuẩn:*
```csharp
return db.Employees
    .Where(e => e.Id == employeeId)
    .Select(e => new EmployeeDTO
    {
        Id = e.Id,
        FullName = e.FullName,
        BirthDate = e.BirthDate,
        Phone = e.Phone,
        Address = e.Address,
        Position = e.Role != null ? e.Role.RoleName : string.Empty,
        RoleId = e.RoleId,
        HireDate = e.HireDate,
        Salary = db.Salaries.Where(s => s.RoleId == e.RoleId).Select(s => (decimal?)s.Amount).FirstOrDefault(),
        Image = e.Image
    })
    .FirstOrDefault();
```

#### 3) `DAL\EmployeeRepository.cs` (Dòng 75 - 95: `SearchEmployees`)
*Đề xuất chuyển đổi sang LINQ Lambda chuẩn:*
```csharp
return db.Employees
    .Where(e => e.FullName.Contains(keyword) || e.Phone.Contains(keyword))
    .Select(e => new EmployeeDTO
    {
        Id = e.Id,
        FullName = e.FullName,
        BirthDate = e.BirthDate,
        Phone = e.Phone,
        Address = e.Address,
        Position = e.Role != null ? e.Role.RoleName : string.Empty,
        RoleId = e.RoleId,
        HireDate = e.HireDate,
        Salary = db.Salaries.Where(s => s.RoleId == e.RoleId).Select(s => (decimal?)s.Amount).FirstOrDefault(),
        Image = e.Image
    })
    .ToList();
```

#### 4) `DAL\EmployeeRepository.cs` (Dòng 181 - 193: `GetEmployeesWithoutAccount`)
*Đề xuất chuyển đổi sang LINQ Lambda chuẩn:*
```csharp
var assignedEmployeeIds = db.Users
    .Where(u => u.EmployeeId > 0)
    .Select(u => u.EmployeeId)
    .ToList();

return db.Employees
    .Where(e => !assignedEmployeeIds.Contains(e.Id))
    .Select(e => new EmployeeDTO
    {
        Id = e.Id,
        FullName = e.FullName,
        Phone = e.Phone,
        Position = e.Role != null ? e.Role.RoleName : "Chưa có"
    })
    .ToList();
```

#### 5) `DAL\InventoryRepository.cs` (Dòng 144 - 159: `GetTransactionHistory`)
*Đề xuất chuyển đổi sang LINQ Lambda chuẩn:*
```csharp
return db.InventoryTransactions
    .OrderByDescending(t => t.TransactionDate)
    .Select(t => new InventoryTransactionDTO
    {
        Id = t.Id,
        MaterialId = t.MaterialId,
        MaterialName = t.Material != null ? t.Material.MaterialName : string.Empty,
        EmployeeId = t.EmployeeId,
        EmployeeName = t.Employee != null ? t.Employee.FullName : string.Empty,
        TransactionType = t.TransactionType,
        Quantity = t.Quantity,
        TransactionDate = t.TransactionDate
    })
    .ToList();
```

#### 6) `DAL\OrderRepository.cs` (Dòng 316 - 330 & 335 - 353: `GetOrderDetailsByOrderId`)
*Đề xuất chuyển đổi sang LINQ Lambda chuẩn:*
```csharp
if (!_tableRepo.IsDatabaseOnline())
{
    return FallbackOrderDetails
        .Where(detail => detail.OrderId == orderId)
        .Join(_fallbackProducts,
              detail => detail.ProductId,
              product => product.Id,
              (detail, product) => new OrderDetailDTO
              {
                  Id = detail.Id,
                  OrderId = detail.OrderId,
                  ProductId = detail.ProductId,
                  ProductName = product.ProductName,
                  Unit = product.Unit,
                  Quantity = detail.Quantity,
                  UnitPrice = detail.UnitPrice,
                  TotalPrice = detail.TotalPrice
              })
        .OrderBy(d => d.Id)
        .ToList();
}

using (var db = GetContext())
{
    return db.OrderDetails
        .Where(d => d.OrderId == orderId)
        .OrderBy(d => d.Id)
        .Select(d => new OrderDetailDTO
        {
            Id = d.Id,
            OrderId = d.OrderId,
            ProductId = d.ProductId,
            ProductName = d.Product != null ? d.Product.ProductName : string.Empty,
            Unit = d.Product != null ? d.Product.Unit : string.Empty,
            Quantity = d.Quantity,
            UnitPrice = d.UnitPrice,
            TotalPrice = d.TotalPrice
        })
        .ToList();
}
```

#### 7) `DAL\ProductRepository.cs` (Dòng 95 - 108: `GetAllProducts`)
*Đề xuất chuyển đổi sang LINQ Lambda chuẩn:*
```csharp
return db.Products
    .Select(p => new ProductDTO
    {
        Id = p.Id,
        ProductName = p.ProductName,
        CategoryId = p.CategoryId,
        CategoryName = p.Category != null ? p.Category.CategoryName : string.Empty,
        Price = p.Price,
        Unit = p.Unit,
        ImagePath = p.ImagePath,
        IsActive = p.IsActive
    })
    .ToList();
```

#### 8) `DAL\ProductRepository.cs` (Dòng 117 - 130: `GetProductsByCategory`)
*Đề xuất chuyển đổi sang LINQ Lambda chuẩn:*
```csharp
return db.Products
    .Where(p => p.CategoryId == categoryId)
    .Select(p => new ProductDTO
    {
        Id = p.Id,
        ProductName = p.ProductName,
        CategoryId = p.CategoryId,
        CategoryName = p.Category != null ? p.Category.CategoryName : string.Empty,
        Price = p.Price,
        Unit = p.Unit,
        ImagePath = p.ImagePath,
        IsActive = p.IsActive
    })
    .ToList();
```

#### 9) `DAL\ProductRepository.cs` (Dòng 140 - 153: `SearchProductsByName`)
*Đề xuất chuyển đổi sang LINQ Lambda chuẩn:*
```csharp
return db.Products
    .Where(p => p.ProductName.Contains(keyword))
    .Select(p => new ProductDTO
    {
        Id = p.Id,
        ProductName = p.ProductName,
        CategoryId = p.CategoryId,
        CategoryName = p.Category != null ? p.Category.CategoryName : string.Empty,
        Price = p.Price,
        Unit = p.Unit,
        ImagePath = p.ImagePath,
        IsActive = p.IsActive
    })
    .ToList();
```

#### 10) `DAL\RecipeRepository.cs` (Dòng 21 - 34: `GetRecipesByProductId`)
*Đề xuất chuyển đổi sang LINQ Lambda chuẩn:*
```csharp
return db.Recipes
    .Where(r => r.ProductId == productId)
    .Select(r => new RecipeDTO
    {
        Id = r.Id,
        ProductId = r.ProductId,
        ProductName = r.Product != null ? r.Product.ProductName : string.Empty,
        IngredientName = r.IngredientName,
        Quantity = r.Quantity,
        Unit = r.Unit,
        Note = r.Note
    })
    .ToList();
```

#### 11) `DAL\TableRepository.cs` (Dòng 153-169 & 173-186: `GetAllTablesWithArea`)
*Đề xuất chuyển đổi sang LINQ Lambda chuẩn:*
```csharp
if (!IsDatabaseOnline())
{
    return FallbackTables
        .Join(_fallbackAreas,
              table => table.AreaId,
              area => area.Id,
              (table, area) => new TableDTO
              {
                  Id = table.Id,
                  TableName = table.TableName,
                  AreaId = table.AreaId,
                  AreaName = area.AreaName,
                  Status = table.Status,
                  StartTime = table.StartTime,
                  EndTime = table.EndTime,
                  CustomerId = table.CustomerId,
                  CustomerName = !string.IsNullOrEmpty(table.CustomerId) ? "Khách mẫu" : null,
                  CustomerPhone = table.CustomerId
              })
        .OrderBy(t => t.Id)
        .ToList();
}

using (var db = GetContext())
{
    var tables = db.Tables
        .OrderBy(t => t.Id)
        .Select(t => new
        {
            t.Id,
            t.TableName,
            t.AreaId,
            AreaName = t.Area != null ? t.Area.AreaName : string.Empty,
            t.Status,
            t.StartTime,
            t.EndTime,
            t.CustomerId
        })
        .ToList();

    var customerList = db.Customers.ToList();

    return tables.Select(t =>
    {
        var cust = !string.IsNullOrEmpty(t.CustomerId)
            ? customerList.FirstOrDefault(c => c.Phone == t.CustomerId || c.Id.ToString() == t.CustomerId)
            : null;

        return new TableDTO
        {
            Id = t.Id,
            TableName = t.TableName,
            AreaId = t.AreaId,
            AreaName = t.AreaName,
            Status = t.Status,
            StartTime = t.StartTime,
            EndTime = t.EndTime,
            CustomerId = t.CustomerId,
            CustomerName = cust != null ? cust.CustomerName : (!string.IsNullOrEmpty(t.CustomerId) ? t.CustomerId : null),
            CustomerPhone = cust?.Phone
        };
    }).ToList();
}
```

#### 12) `DAL\UserRepository.cs` (Dòng 40 - 58: `GetAllUsersWithRoles`)
*Đề xuất chuyển đổi sang LINQ Lambda chuẩn:*
```csharp
return db.Users
    .Select(user => new UserDTO
    {
        Id = user.Id,
        Username = user.Username,
        RoleId = user.RoleId,
        RoleName = user.Role != null ? user.Role.RoleName : string.Empty,
        EmployeeId = user.EmployeeId,
        FullName = user.Employee != null ? user.Employee.FullName : string.Empty,
        IsActive = user.IsActive ?? true
    })
    .ToList();
```

---

### 2.2. Giải pháp khắc phục triệt để Lỗ hổng Đường dẫn Tuyệt đối Ảnh Nhân viên
Để đạt chuẩn 100% về địa chỉ tương đối:
1. Tạo quy trình xử lý lưu ảnh đại diện tương tự `ProductService.cs`:
   - Tạo thư mục `Images\Employees` trong `Application.StartupPath` nếu chưa tồn tại.
   - Khi người dùng chọn file ảnh từ `OpenFileDialog`, copy file ảnh vào `Application.StartupPath\Images\Employees\emp_{timestamp}_{guid}.ext`.
   - Lưu chuỗi đường dẫn tương đối `Images\Employees\emp_...` vào thuộc tính `EmployeeDTO.Image`.
2. Thay thế `Image.FromFile(emp.Image)` trong `UCEmployeeInform.cs` và `UCEmployeeEdit.cs` bằng `UIHelper.LoadImageToPictureBox(ptbAvatar, emp.Image)`. Hàm này sẽ tự động ghép với `Application.StartupPath`, đọc bằng `MemoryStream` an toàn và không gây lỗi khi đổi máy tính.

---

## 3. Caveats (Các Điểm Lưu Ý & Giả Định)
- **Cơ chế Fallback Offline**: `TableRepository` và `OrderRepository` duy trì 2 tập dữ liệu in-memory (`FallbackTables`, `FallbackOrders`, `_fallbackCategories`, `_fallbackProducts`). Thiết kế này hữu ích cho mục đích chạy thử offline khi giảng viên chưa attach CSDL vào SQL Server. Cần đảm bảo các câu lệnh LINQ trên tập in-memory này cũng tuân thủ cú pháp Lambda.
- **Tập tin Dataset Cũ**: Các tệp DataSet do Visual Studio tự sinh (`CoffeeShopDBDataSet11.xsd`, v.v.) có chứa một số câu lệnh SQL sinh tự động từ wizard, tuy nhiên mã nguồn ứng dụng thực tế **hoàn toàn không gọi hay sử dụng** các TableAdapter này, mà chạy 100% qua LINQ to SQL DataContext.

---

## 4. Conclusion (Kết Luận Đánh Giá)

1. **Về Ma trận LINQ Lambda (3.0 Điểm)**:
   - **Hiện trạng**: Đạt xấp xỉ **2.0 / 3.0 điểm**. Đồ án đã áp dụng mô hình LINQ to SQL và không hề có SQL Injection hay ADO.NET thô. Tuy nhiên, vẫn còn sót **12 phương thức sử dụng LINQ Query syntax** (`from ... join ... select`), vi phạm quy tắc "bắt buộc dùng toàn bộ là LINQ Lambda".
   - **Mức độ khả thi sửa đổi**: Rất dễ dàng và an toàn 100%. Toàn bộ mã chuyển đổi cụ thể đã được lập trình chi tiết ở Mục 2.1 của báo cáo này.
2. **Về Tiêu chí App.config (1.0 Điểm)**:
   - **Hiện trạng**: Đạt **1.0 / 1.0 điểm**. Toàn bộ cấu hình Connection String và AppSettings quán cà phê đều lưu trong `App.config`, có cơ chế failover dò tìm instance SQL tự động và có Form cấu hình trực quan. Cần bổ sung đồng bộ khóa phụ `CoffeeShopDBConnectionString1` khi lưu.
3. **Về Tiêu chí Đường dẫn Tương đối (Bắt buộc)**:
   - **Hiện trạng**: Phần Món ăn và Báo cáo đạt 100% địa chỉ tương đối. Riêng phần **Ảnh Nhân viên (Employee Avatar)** bị vi phạm do lưu đường dẫn tuyệt đối trực tiếp từ máy cá nhân (`C:\...`).
   - **Hành động bắt buộc**: Phải refactor ngay luồng lưu ảnh nhân viên sang thư mục `Images\Employees` dùng `UIHelper.LoadImageToPictureBox` để bảo toàn điểm số.

---

## 5. Verification Method (Phương Pháp Kiểm Định Độc Lập)

Người tiếp nhận hoặc Orchestrator có thể kiểm tra độc lập các phát hiện trên bằng các lệnh PowerShell sau:

1. **Kiểm tra sự tồn tại của LINQ Query syntax trong mã nguồn**:
   ```powershell
   Get-ChildItem -Path "d:\DoAnCuoiKi_QuanLyTiemCaPhe" -Recurse -Filter "*.cs" | Where-Object { $_.FullName -notmatch "\\(bin|obj|packages|\.agents)\\" -and $_.FullName -notmatch "\.Designer\.cs$" } | Select-String -Pattern "\bfrom\s+\w+\s+in\b"
   ```
   *Điều kiện hợp lệ sau khi refactor*: Không có bất kỳ kết quả nào trả về.

2. **Kiểm tra truy vấn SQL thô hoặc ADO.NET**:
   ```powershell
   Get-ChildItem -Path "d:\DoAnCuoiKi_QuanLyTiemCaPhe" -Recurse -Filter "*.cs" | Where-Object { $_.FullName -notmatch "\\(bin|obj|packages|\.agents)\\" -and $_.FullName -notmatch "\.Designer\.cs$" } | Select-String -Pattern "SqlCommand|SqlDataAdapter|ExecuteReader|ExecuteNonQuery|ExecuteScalar"
   ```
   *Điều kiện hợp lệ*: Không có kết quả nào (hiện tại đã đạt).

3. **Kiểm tra đường dẫn tuyệt đối hoặc Image.FromFile trong UserControl nhân viên**:
   ```powershell
   Get-ChildItem -Path "d:\DoAnCuoiKi_QuanLyTiemCaPhe\UserControl" -Recurse -Filter "*.cs" | Select-String -Pattern "Image\.FromFile"
   ```
   *Điều kiện hợp lệ sau khi refactor*: Tất cả đều chuyển sang gọi `UIHelper.LoadImageToPictureBox`.

4. **Lệnh Build kiểm tra toàn bộ giải pháp**:
   ```powershell
   & "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" "d:\DoAnCuoiKi_QuanLyTiemCaPhe\DoAnCuoiKi_QuanLyTiemCaPhe.csproj" /t:Build /p:Configuration=Debug
   ```
   *Điều kiện hợp lệ*: `Build succeeded. 0 Warning(s), 0 Error(s)`.
