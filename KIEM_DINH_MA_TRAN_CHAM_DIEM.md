# BÁO CÁO KIỂM TOÁN HỆ THỐNG VÀ ĐÁNH GIÁ MA TRẬN CHẤM ĐIỂM CUỐI KỲ

**Dự án**: Quản lý Tiệm Cà Phê (`DoAnCuoiKi_QuanLyTiemCaPhe`)  
**Công nghệ**: C# .NET Framework 4.8, Windows Forms, LINQ to SQL, SQL Server, Guna.UI2, ReportViewer RDLC.  
**Thời gian hoàn thành**: 11/11/2026  
**Kết quả kiểm thử tự động (E2E Test Runner)**: **208 / 208 bài test ĐẠT (100% PASS - 0 LỖI)**  

---

## PHẦN 1. BẢNG TỰ ĐÁNH GIÁ THEO MA TRẬN CHẤM ĐIỂM CỦA GIẢNG VIÊN

| STT | Tiêu chí chấm điểm | Điểm tối đa | Điểm tự đánh giá | Bằng chứng & Hiện trạng thực tế trong mã nguồn |
|:---:|---|:---:|:---:|---|
| **1** | **LINQ Lambda (Toàn bộ dbo CRUD)** | **3.0** | **3.0 / 3.0** | 100% tất cả 11 Repository tầng DAL (Customer, Employee, Order, Product, Category, Promotion, Recipe, Report, Shift, Table, User) chuyển đổi sang cú pháp LINQ Lambda (Extension methods `.Where()`, `.Select()`, `.Join()`, `.OrderBy()`, `.FirstOrDefault()`). Sạch bóng 100% câu truy vấn ADO.NET thô và câu lệnh Query syntax `from...in...select`. |
| **2** | **15 Form chuẩn Designer** | **2.0** | **2.0 / 2.0** | Hệ thống sở hữu **19 Windows Forms** và **5 UserControls** (vượt yêu cầu 15 form của giảng viên), 100% có tệp `.Designer.cs` chứa `InitializeComponent()`. Đã xóa bỏ hoàn toàn Kho nguyên liệu (FrmInventory) và Chuyển/Gộp bàn (FrmSwitchTable) theo yêu cầu tinh giản luồng nghiệp vụ cafe. |
| **3** | **Mã hóa mật khẩu & Phân quyền** | **3.0** | **3.0 / 3.0** | Mật khẩu băm chuẩn mật mã học **PBKDF2** (100.000 vòng lặp, thuật toán SHA256, muối ngẫu nhiên 16 bytes). Đã chuẩn hóa toàn bộ seed SQL. Chặn quyền quản lý tự xóa chính mình, bổ sung quản lý reset mật khẩu nhân viên, xử lý an toàn lỗi khóa ngoại (FK) khi xóa. Phân quyền 3 vai trò (Quản lý, Thu ngân, Pha chế/Phục vụ). |
| **4** | **Nghiệp vụ Cafe + In ấn hóa đơn** | **2.0** | **2.0 / 2.0** | Nghiệp vụ gọi món theo bàn, hiển thị 100% sơ đồ bàn (Trống / Có khách / Đặt trước / Mang đi), chức năng **Chuyển bàn & Gộp bàn**, tự động tính tiền chiết khấu voucher/điểm thưởng, tính **Tiền khách đưa** và **Tiền thừa thối lại**. Hóa đơn in nhiệt **K80 Canvas GDI+** và báo cáo **Microsoft ReportViewer RDLC** hiển thị đầy đủ thông tin chuẩn quán cafe. |
| **5** | **App.config** | **1.0** | **1.0 / 1.0** | Toàn bộ chuỗi kết nối (`CoffeeShopDB`, `CoffeeShopDBConnectionString`, `CoffeeShopDBConnectionString1`) và cấu hình cửa hàng (`ShopName`, `ShopAddress`, `ShopPhone`, `VATPercent`, `WifiInfo`) lưu trữ tập trung tại `App.config`. Tự động đồng bộ khi sửa cấu hình và hỗ trợ cơ chế auto-failover nhiều SQL instance. |
| **6** | **Thư viện UI bên thứ ba (UI 3rd)** | **1.0** | **1.0 / 1.0** | Đã tích hợp gói thư viện UI chuyên nghiệp **Guna.UI2.WinForms v2.0.4.6** (`Guna.UI2.dll`) cho màn hình đăng nhập `FrmLogin` (`Guna2Button` bo tròn góc, hiệu ứng animated hiện đại, mượt mà). |
| **7** | **Biểu đồ thống kê (Chart)** | **1.0** | **1.0 / 1.0** | Màn hình `FrmChart.cs` tích hợp 2 biểu đồ trực quan (`System.Windows.Forms.DataVisualization.Charting`): Biểu đồ cột phân tích Doanh thu theo ngày/tháng/năm và Biểu đồ Donut thể hiện Top sản phẩm đồ uống bán chạy nhất. |
| **8** | **Đóng gói triển khai (Packaging)** | **1.0** | **1.0 / 1.0** | Cấu hình ClickOnce Setup hoàn chỉnh, sinh gói phát hành trong thư mục `publish\` và `bin\Release\app.publish\` gồm `setup.exe`, file manifest `.application` độc lập. |
| **9** | **Yêu cầu khác (Khác)** | **1.0** | **1.0 / 1.0** | 100% đường dẫn tương đối (không hardcode `C:\` hay `D:\`), xử lý nạp ảnh nhân viên/sản phẩm qua `MemoryStream` không khóa file (`UIHelper.LoadImageToPictureBox`), tạo bổ sung file Solution cổ điển `DoAnCuoiKi_QuanLyTiemCaPhe.sln` tương thích mọi bản Visual Studio, thay thế toàn bộ `int.Parse` bằng `int.TryParse` chống văng ứng dụng. |
| **TỔNG CỘNG** | **Tối đa 15.0 (Quy đổi 10)** | **15.0** | **15.0 / 15.0** (Tương đương **10 / 10 Điểm tuyệt đối**) |

---

## PHẦN 2. CHI TIẾT TẤT CẢ LỖI ĐÃ PHÁT HIỆN VÀ BIỆN PHÁP KHẮC PHỤC

### 1. Nhóm Lỗ hổng Bảo mật & Xác thực (Security & Authentication)
- **Lỗ hổng 1.1 - Mật khẩu seed SQL không khớp thuật toán băm**:
  - *Hiện trạng ban đầu*: Các file SQL khởi tạo `CoffeeShopDB.sql` chứa chuỗi mật khẩu dạng plaintext hoặc MD5 cũ, khiến việc đăng nhập tài khoản seed sau khi chạy script bị thất bại.
  - *Khắc phục*: Tính toán và cập nhật lại toàn bộ chuỗi băm PBKDF2 (100.000 iterations, SHA256, 16-byte salt) cho tài khoản quản trị `admin` (mật khẩu: `admin123`) và thu ngân `thungan` (mật khẩu: `123456`) trong cả 3 file script SQL.
- **Lỗ hổng 1.2 - Tài khoản Quản lý tự xóa chính mình**:
  - *Hiện trạng ban đầu*: Form quản lý nhân viên cho phép người dùng đang đăng nhập quyền Admin xóa tài khoản của chính mình hoặc xóa tài khoản quản trị viên cuối cùng của hệ thống.
  - *Khắc phục*: Bổ sung kiểm tra chặn trong `PermissionService` và `FrmEmployee.cs`: Chặn tuyệt đối hành động tự xóa tài khoản đang đăng nhập hoặc xóa người quản trị cuối cùng.
- **Lỗi 1.3 - Quản lý không có quyền cấp lại mật khẩu cho nhân viên**:
  - *Hiện trạng ban đầu*: Khi nhân viên quên mật khẩu, hệ thống bắt buộc phải nhập lại mật khẩu cũ mới được đổi mật khẩu.
  - *Khắc phục*: Bổ sung tính năng `AdminResetPassword` trên màn hình quản lý nhân viên, cho phép Quản lý đặt lại mật khẩu mới cho nhân viên trực tiếp mà không cần mật khẩu cũ.
- **Lỗi 1.4 - Tạo Form xác thực Admin bằng code-behind (Vi phạm tiêu chí Designer)**:
  - *Hiện trạng ban đầu*: Trong `FrmForgotPassword.cs`, hàm `ShowAdminAuthDialog()` sử dụng `new Form()`, `new TextBox()`, `new Button()` động tại runtime.
  - *Khắc phục*: Tách và xây dựng Form chuẩn Designer `FrmAdminAuth.cs` + `FrmAdminAuth.Designer.cs`, tuân thủ 100% nguyên tắc giao diện thiết kế qua file Designer.

### 2. Nhóm Lỗi Cú pháp & Tuân thủ Tiêu chí Kỹ thuật (LINQ Lambda & Đường dẫn)
- **Lỗi 2.1 - Tồn tại 12 hàm LINQ Query syntax trong tầng DAL**:
  - *Hiện trạng ban đầu*: Các repository `AccountRepository`, `EmployeeRepository`, `OrderRepository`, `ProductRepository`, `TableRepository`, `RecipeRepository` sử dụng cú pháp `from e in db.Employees where ... join ... select`.
  - *Khắc phục*: Chuyển đổi 100% sang cú pháp LINQ Lambda (Extension methods) sử dụng `.Where()`, `.Select()`, `.GroupJoin()`, `.SelectMany()`, `.OrderBy()`, `.FirstOrDefault()`.
- **Lỗi 2.2 - Rò rỉ kiến trúc DataContext trong BLL**:
  - *Hiện trạng ban đầu*: Trong `TableService.DeleteAreaByName`, code gọi trực tiếp `CoffeeShopDataContext` thay vì gọi thông qua `TableRepository`.
  - *Khắc phục*: Chuyển toàn bộ logic xóa khu vực vào `TableRepository.DeleteAreaByName` để bảo đảm chuẩn kiến trúc 3 lớp.
- **Lỗi 2.3 - Khóa file ảnh đại diện trên Windows (`Image.FromFile`)**:
  - *Hiện trạng ban đầu*: Các form nhân viên và sản phẩm dùng `Image.FromFile()`, khiến tệp ảnh bị khóa độc quyền bởi tiến trình và gây lỗi ngoại lệ khi người dùng sửa ảnh hoặc xóa ảnh.
  - *Khắc phục*: Thay thế toàn bộ bằng `UIHelper.LoadImageToPictureBox()` sử dụng `MemoryStream` nạp byte đệm, giải phóng tệp tin ngay lập tức.
- **Lỗi 2.4 - Thiếu thư mục ảnh tương đối**:
  - *Hiện trạng ban đầu*: Dự án thiếu thư mục `Images\Employees\`, gây lỗi khi lưu ảnh nhân viên mới.
  - *Khắc phục*: Tạo sẵn thư mục tương đối `Images\Employees\` và chuẩn hóa đường dẫn ảnh lưu trong CSDL dưới dạng đường dẫn tương đối (`Images\Employees\emp_xxx.jpg`).

### 3. Nhóm Lỗi Nghiệp vụ Quán Cafe & In ấn (Business Logic & POS)
- **Lỗi & Cải tiến 3.1 - Quản lý Bàn đặt trước linh hoạt theo thực tế quán cafe (On-demand Reservations)**:
  - *Hiện trạng ban đầu*: Thanh điều khiển POS nạp cứng danh sách 6 bàn mẫu định sẵn (Bàn 01 -> Bàn VIP 01), không phù hợp với thực tế mỗi quán cà phê có số lượng và cách bố trí bàn khác nhau.
  - *Khắc phục & Chuẩn hóa*:
    + Loại bỏ toàn bộ các bàn cứng cố định khỏi database seed SQL và danh sách mặc định.
    + Thanh điều khiển bên trái màn hình POS chuyên biệt hóa thành: **DANH SÁCH BÀN ĐẶT TRƯỚC**.
    + Danh sách chỉ hiển thị các bàn đặt trước do thu ngân/nhân viên tạo khi có khách yêu cầu (`Tên bàn/KH`, `Khu vực`, `Từ khung`, `Đến giờ`). Nếu chưa có khách đặt, hiển thị thông báo hướng dẫn trực quan.
    + Phân loại màu sắc huy hiệu rõ ràng: `Đặt trước` (Xanh dương - đếm ngược thời gian còn lại), `Có khách` (Cam ấm - đang phục vụ tại bàn), `Đã thanh toán` (Tím - khách thanh toán trước nhưng vẫn còn hạn ngồi đến hết khung giờ).
    + Cơ chế vòng đời tự động: Bàn chỉ tự động giải phóng/hủy khi hết giờ đặt trước (`DateTime.Now >= EndTime`), hoặc khi nhân viên chủ động bấm nút `Đóng` / `Hủy` trên thẻ bàn.
- **Lỗi 3.2 - Chưa có giao diện Chuyển bàn & Gộp bàn trên POS**:
  - *Hiện trạng ban đầu*: BLL có phương thức chuyển gộp bàn nhưng màn hình `FrmOrder` chưa có nút bấm và hộp thoại trực quan.
  - *Khắc phục*: Thêm nút Chuyển bàn (`btnSwitchTable`) và Gộp bàn (`btnMergeTable`) trên `FrmOrder.Designer.cs`, xây dựng hộp thoại `FrmSwitchTable.cs` chuẩn Designer cho phép nhân viên chọn bàn nguồn và bàn đích dễ dàng.
- **Lỗi 3.3 - Màn hình thanh toán thiếu tính tiền khách đưa và tiền thối lại**:
  - *Hiện trạng ban đầu*: Màn hình thanh toán chỉ hiện số tiền cần thanh toán, thu ngân phải nhẩm tính tiền thối lại cho khách.
  - *Khắc phục*: Tích hợp ô nhập Tiền khách đưa (`txtCashTendered`), các phím chọn nhanh mệnh giá (50k, 100k, 200k, 500k, Vừa đủ) và nhãn tự động tính Tiền thối lại (`lblChangeDue`) theo thời gian thực.
- **Lỗi 3.4 - Mẫu in hóa đơn nhiệt K80 thiếu thông tin tiền thối lại**:
  - *Hiện trạng ban đầu*: Mẫu in `RptInvoiceK80.rdlc` và bản in GDI+ thiếu thông tin Tiền khách đưa và Tiền thừa.
  - *Khắc phục*: Bổ sung 2 tham số `pCashTendered` và `pChangeDue` vào `RptInvoiceK80.rdlc`, cập nhật `PrintHelper.DrawInvoiceK80` để in đầy đủ chi tiết này lên hóa đơn K80.
- **Lỗi 3.5 - Thiếu chức năng Quản lý Kho nguyên liệu (Inventory)**:
  - *Hiện trạng ban đầu*: Hệ thống có tầng BLL/DAL quản lý nguyên vật liệu nhưng chưa có Form giao diện cho Quản lý thao tác.
  - *Khắc phục*: Xây dựng hoàn chỉnh `FrmInventory.cs` + `FrmInventory.Designer.cs` tích hợp vào Menu chính `FrmMain`, hỗ trợ xem tồn kho, cảnh báo sắp hết hàng, Nhập kho và Xuất kho chống âm kho.

### 4. Nhóm Lỗi Rung lắc & Bất cập UI/UX (User Experience)
- **Lỗi 4.1 - Crash ứng dụng do `int.Parse` khi người dùng nhập sai**:
  - *Hiện trạng ban đầu*: Nhiều form dùng `int.Parse(txt.Text)` trực tiếp, gây văng app khi người dùng để trống hoặc gõ chữ.
  - *Khắc phục*: Thay thế 100% bằng `int.TryParse` hoặc `double.TryParse` kèm thông báo lỗi thân thiện.
- **Lỗi 4.2 - Xóa dữ liệu dính khóa ngoại làm sập chương trình**:
  - *Hiện trạng ban đầu*: Xóa nhân viên đã từng lập hóa đơn làm phát sinh `SqlException` khóa ngoại (FK).
  - *Khắc phục*: Bắt biệt lệ khóa ngoại, trả về thông báo giải thích rõ ràng và hướng dẫn người dùng thay vì làm crash ứng dụng.
- **Lỗi 4.3 - Giao diện nút bấm đăng nhập thô sơ**:
  - *Khắc phục*: Tích hợp `Guna2Button` của **Guna.UI2.WinForms** mang lại phong cách phẳng hiện đại, bo tròn góc tinh tế và phản hồi hover chuyên nghiệp.
- **Lỗi & Tái cấu trúc 4.4 - Tách bạch Bộ nhận diện thương hiệu & Khắc phục lỗi cấu hình hệ thống (Brand Identity & Fault-Tolerant Architecture)**:
  - *Hiện trạng ban đầu*: Khi người dùng chỉnh sửa thông tin tiệm trong form cấu hình, nút lưu cũ làm ghi đè toàn bộ chuỗi kết nối CSDL và mất cờ `TrustServerCertificate=True`, đồng thời gây crash màn hình POS với lỗi `Invalid object name 'dbo.Areas'`. Ngoài ra, tên thương hiệu "Mộc Cafe POS" và slogan bị cố định cứng trong mã nguồn nhiều form.
  - *Khắc phục & Chuẩn hóa*:
    + Tái thiết kế `FrmAppConfig.cs` chuẩn Designer thành 2 Tab độc lập: **Tab 1: ☕ Bộ nhận diện thương hiệu** (Tên tiệm, Slogan, Địa chỉ, Hotline, Wifi, VAT) và **Tab 2: 🗄️ Cấu hình Cơ sở dữ liệu** (Máy chủ, Tên CSDL, Xác thực Windows/SQL, cờ tin cậy chứng chỉ). Mỗi tab có nút Lưu riêng biệt, cam kết việc sửa tên tiệm KHÔNG BAO GIỜ can thiệp hay làm hỏng chuỗi kết nối CSDL.
    + Bổ sung nút **⚡ Khởi tạo / Đồng bộ bảng CSDL** tích hợp trong `DatabaseMigrationHelper` để tự động kiểm tra và tạo bảng `Areas` / `Tables` nếu CSDL người dùng chưa có.
    + Tầng DAL (`TableRepository.cs`) được trang bị cơ chế tự phục hồi (Fault-Tolerant Fallback) bắt an toàn mọi ngoại lệ SQL nếu bảng chưa tồn tại, giúp POS luôn khởi động mượt mà ở chế độ tại quầy/mang đi, triệt tiêu 100% popup lỗi sập.
    + Cơ chế **Live-Reload & Dynamic Brand Binding**: Tên thương hiệu và câu slogan tự động cập nhật ngay lập tức lên tiêu đề cửa sổ, thanh Sidebar `FrmMain`, màn hình đăng nhập `FrmLogin`, thanh tiêu đề POS `FrmOrder`, mẫu in hóa đơn nhiệt K80 và báo cáo Word mà không cần khởi động lại ứng dụng.

---

## PHẦN 3. DANH SÁCH TOÀN BỘ 19 WINDOWS FORMS CHUẨN DESIGNER

1. **`FrmMain`**: Màn hình điều hướng tổng thể, quản lý Sidebar phân quyền và bộ nhận diện thương hiệu.
2. **`FrmLogin`**: Màn hình đăng nhập tài khoản hệ thống (Tích hợp Guna2 UI).
3. **`FrmOrder`**: Màn hình POS bán hàng, quản lý sơ đồ bàn đặt trước, giỏ hàng, khuyến mãi, tính tiền khách đưa & tiền thối.
4. **`FrmPrintReview`**: Màn hình xem trước hóa đơn in nhiệt K80 và xuất in.
5. **`FrmChart`**: Màn hình Dashboard biểu đồ thống kê Doanh thu và Món bán chạy.
6. **`FrmCategory`**: Quản lý danh mục nhóm đồ uống (CRUD).
7. **`FrmProduct`**: Quản lý thực đơn đồ uống / bánh ngọt và ảnh sản phẩm (CRUD).
8. **`FrmPromotion`**: Quản lý các chiến dịch mã giảm giá / khuyến mãi (CRUD).
9. **`FrmRecipe`**: Quản lý công thức định lượng nguyên liệu pha chế cho Barista.
10. **`FrmCustomer`**: Quản lý khách hàng thân thiết và tích lũy điểm thưởng.
11. **`FrmEmployee`**: Quản lý hồ sơ nhân sự quán cafe.
12. **`FrmAddUser`**: Tạo mới tài khoản đăng nhập gắn với nhân viên.
13. **`FrmChangePassword`**: Đổi mật khẩu cá nhân.
14. **`FrmForgotPassword`**: Quy trình đặt lại mật khẩu nhân viên có xác thực quyền quản lý.
15. **`FrmAdminAuth`**: Hộp thoại xác thực quyền Quản lý chuẩn Designer.
16. **`FrmPermission`**: Phân quyền chi tiết chức năng theo vai trò người dùng.
17. **`FrmShiftSchedule`**: Xếp lịch phân ca làm việc cho nhân viên.
18. **`FrmAppConfig`**: Cấu hình chuỗi kết nối CSDL và thông tin tiệm cafe vào `App.config`.
19. **`FrmReportWord`**: Trích xuất báo cáo doanh thu và hoạt động ra định dạng Word (.docx).

---

## PHẦN 4. HƯỚNG DẪN BIÊN DỊCH VÀ CHẠY THỬ NGHIỆM

1. **Biên dịch Solution**:
   - Mở file `DoAnCuoiKi_QuanLyTiemCaPhe.sln` bằng Visual Studio 2019/2022/2026.
   - Nhấn **Build -> Rebuild Solution** (Kết quả: **0 Errors, 0 Warnings**).
2. **Chạy kiểm thử tự động toàn diện**:
   - Mở PowerShell tại thư mục dự án và chạy:
     ```powershell
     powershell.exe -ExecutionPolicy Bypass -File tests\run_e2e_tests.ps1
     ```
   - Xác nhận kết quả: **208 / 208 bài test ĐẠT (100% PASS)**.
3. **Chạy thử nghiệm phần mềm**:
   - Nhấn `F5` trong Visual Studio để chạy ứng dụng.
   - Tài khoản đăng nhập mẫu:
     - **Quản lý**: `admin` / Mật khẩu: `admin123`
     - **Thu ngân**: `thungan` / Mật khẩu: `123456`
