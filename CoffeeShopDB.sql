-- =============================================================
-- KỊCH BẢN TẠO CƠ SỞ DỮ LIỆU: CoffeeShopDB
-- Dự án: Đồ án cuối kỳ - Quản lý tiệm cà phê
-- =============================================================

CREATE DATABASE CoffeeShopDB;
GO

USE CoffeeShopDB;
GO

-- 1. Bảng Khu vực (Areas)
CREATE TABLE Areas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AreaName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(200) NULL
);
GO

-- 2. Bảng Bàn (Tables)
CREATE TABLE Tables (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TableName NVARCHAR(50) NOT NULL,
    AreaId INT NOT NULL,
    Status NVARCHAR(20) NULL DEFAULT N'Trống',
    CONSTRAINT FK_Tables_Areas FOREIGN KEY (AreaId) REFERENCES Areas(Id) ON DELETE CASCADE
);
GO

-- 3. Bảng Danh mục món (Categories)
CREATE TABLE Categories (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(200) NULL
);
GO

-- 4. Bảng Món / Sản phẩm (Products)
CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(100) NOT NULL,
    CategoryId INT NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    Unit NVARCHAR(20) NOT NULL,
    ImagePath NVARCHAR(500) NULL,
    IsActive BIT DEFAULT 1,
    CONSTRAINT FK_Products_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE CASCADE
);
GO

-- 5. Bảng Nhân viên (Employees)
CREATE TABLE Employees (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Phone VARCHAR(15) NULL,
    Address NVARCHAR(200) NULL,
    Position NVARCHAR(50) NOT NULL,
    Salary DECIMAL(18,2) NULL,
    HireDate DATE NULL
);
GO

-- 6. Bảng Vai trò / Phân quyền (Roles)
CREATE TABLE Roles (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RoleName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(200) NULL
);
GO

-- 7. Bảng Tài khoản (Users)
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeId INT NOT NULL,
    Username VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR(256) NOT NULL,
    Salt VARCHAR(100) NOT NULL DEFAULT '',
    RoleId INT NOT NULL,
    IsActive BIT DEFAULT 1,
    CONSTRAINT FK_Users_Employees FOREIGN KEY (EmployeeId) REFERENCES Employees(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleId) REFERENCES Roles(Id)
);
GO

-- 8. Bảng Khách hàng (Customers)
CREATE TABLE Customers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName NVARCHAR(100) NOT NULL,
    Phone VARCHAR(15) NOT NULL,
    RewardPoints INT DEFAULT 0,
    MembershipTier NVARCHAR(20) DEFAULT N'Đồng'
);
GO

-- 9. Bảng Khuyến mãi (Promotions)
CREATE TABLE Promotions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PromoCode VARCHAR(50) NOT NULL UNIQUE,
    DiscountPercent FLOAT NOT NULL,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    IsActive BIT DEFAULT 1
);
GO

-- 10. Bảng Đơn hàng (Orders)
CREATE TABLE Orders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TableId INT NOT NULL,
    EmployeeId INT NOT NULL,
    CustomerId INT NULL,
    OrderDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    DiscountAmount DECIMAL(18,2) DEFAULT 0,
    FinalAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    Status NVARCHAR(20) DEFAULT N'Chưa thanh toán',
    PromotionId INT NULL,
    CONSTRAINT FK_Orders_Tables FOREIGN KEY (TableId) REFERENCES Tables(Id),
    CONSTRAINT FK_Orders_Employees FOREIGN KEY (EmployeeId) REFERENCES Employees(Id),
    CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id),
    CONSTRAINT FK_Orders_Promotions FOREIGN KEY (PromotionId) REFERENCES Promotions(Id)
);
GO

-- 11. Bảng Chi tiết đơn hàng (OrderDetails)
CREATE TABLE OrderDetails (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    TotalPrice DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_OrderDetails_Orders FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE,
    CONSTRAINT FK_OrderDetails_Products FOREIGN KEY (ProductId) REFERENCES Products(Id)
);
GO

-- 12. Bảng Nguyên liệu kho (Materials)
CREATE TABLE Materials (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MaterialName NVARCHAR(100) NOT NULL,
    Unit NVARCHAR(20) NOT NULL,
    QuantityInStock FLOAT NOT NULL DEFAULT 0,
    MinThreshold FLOAT NOT NULL DEFAULT 0
);
GO

-- 13. Bảng Giao dịch kho (InventoryTransactions)
CREATE TABLE InventoryTransactions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MaterialId INT NOT NULL,
    EmployeeId INT NOT NULL,
    TransactionType NVARCHAR(20) NOT NULL,
    Quantity FLOAT NOT NULL,
    TransactionDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_InvTrans_Materials FOREIGN KEY (MaterialId) REFERENCES Materials(Id),
    CONSTRAINT FK_InvTrans_Employees FOREIGN KEY (EmployeeId) REFERENCES Employees(Id)
);
GO

-- =============================================================
-- DỮ LIỆU MẪU BAN ĐẦU (SEED DATA)
-- =============================================================

-- 1. Vai trò (Roles) - Chú ý: RoleId = 1 là Quản lý cao nhất
INSERT INTO Roles (RoleName, Description) VALUES 
(N'Quản lý', N'Toàn quyền quản trị hệ thống và cấu hình'),
(N'Thu ngân', N'Phụ trách thanh toán và xuất hóa đơn'),
(N'Phục vụ', N'Order món và phục vụ tại bàn'),
(N'Pha chế', N'Chế biến đồ uống theo đơn');
GO

-- 2. Nhân viên (Employees)
INSERT INTO Employees (FullName, Phone, Address, Position, Salary, HireDate) VALUES 
(N'Nguyễn Văn Quản Lý', '0901234567', N'Quận 1, TP.HCM', N'Quản lý', 15000000, '2024-01-01'),
(N'Trần Thị Thu Ngân', '0912345678', N'Quận 3, TP.HCM', N'Thu ngân', 8000000, '2024-02-15');
GO

-- 3. Tài khoản đăng nhập (Users)
-- Mật khẩu được lưu trực tiếp dạng chuỗi như logic AuthService.cs
INSERT INTO Users (EmployeeId, Username, PasswordHash, Salt, RoleId, IsActive) VALUES 
(1, 'admin', 'admin123', '', 1, 1),
(2, 'thungan', '123456', '', 2, 1);
GO

-- 4. Khu vực (Areas)
INSERT INTO Areas (AreaName, Description) VALUES 
(N'Khu máy lạnh (Tầng 1)', N'Không gian yên tĩnh, làm việc'),
(N'Sân vườn', N'Không gian ngoài trời thoáng đãng'),
(N'Phòng VIP (Tầng 2)', N'Phòng họp nhóm riêng tư');
GO

-- 5. Bàn (Tables)
INSERT INTO Tables (TableName, AreaId, Status) VALUES 
(N'Bàn 01', 1, N'Trống'),
(N'Bàn 02', 1, N'Trống'),
(N'Bàn 03', 1, N'Trống'),
(N'Bàn 04', 2, N'Trống'),
(N'Bàn 05', 2, N'Trống'),
(N'Bàn VIP 01', 3, N'Trống');
GO

-- 6. Danh mục món (Categories)
INSERT INTO Categories (CategoryName, Description) VALUES 
(N'Cà phê truyền thống', N'Cà phê pha phin, pha máy'),
(N'Trà & Trà sữa', N'Các loại trà trái cây và trà sữa'),
(N'Nước ép & Sinh tố', N'Nước ép nguyên chất tươi mát'),
(N'Bánh ngọt', N'Bánh ăn kèm cà phê');
GO

-- 7. Món (Products)
INSERT INTO Products (ProductName, CategoryId, Price, Unit, ImagePath, IsActive) VALUES 
(N'Cà phê đen đá', 1, 25000, N'Ly', '', 1),
(N'Cà phê sữa đá', 1, 29000, N'Ly', '', 1),
(N'Bạc xỉu', 1, 32000, N'Ly', '', 1),
(N'Trà đào cam sả', 2, 40000, N'Ly', '', 1),
(N'Trà sữa trân châu hoàng gia', 2, 45000, N'Ly', '', 1),
(N'Nước ép cam tươi', 3, 38000, N'Ly', '', 1),
(N'Sinh tố bơ sáp', 3, 45000, N'Ly', '', 1),
(N'Bánh Tiramisu', 4, 35000, N'Cái', '', 1),
(N'Bánh Croissant bơ tỏi', 4, 30000, N'Cái', '', 1);
GO

-- 8. Khách hàng (Customers)
INSERT INTO Customers (CustomerName, Phone, RewardPoints, MembershipTier) VALUES 
(N'Lê Hoàng Anh', '0988776655', 120, N'Bạc'),
(N'Phạm Thuỳ Dung', '0933221100', 50, N'Đồng');
GO

-- 9. Khuyến mãi (Promotions)
INSERT INTO Promotions (PromoCode, DiscountPercent, StartDate, EndDate, IsActive) VALUES 
('CHAOMUNG', 10, '2024-01-01', '2027-12-31', 1),
('VIPCOFFEE', 15, '2024-01-01', '2027-12-31', 1);
GO

-- 10. Nguyên liệu kho (Materials)
INSERT INTO Materials (MaterialName, Unit, QuantityInStock, MinThreshold) VALUES 
(N'Hạt Cà Phê Robusta', N'Kg', 50.0, 10.0),
(N'Sữa đặc Ngôi Sao', N'Lon', 100.0, 20.0),
(N'Sữa tươi thanh trùng', N'Hộp', 60.0, 15.0),
(N'Trà đen cao cấp', N'Kg', 20.0, 5.0),
(N'Đường kính trắng', N'Kg', 40.0, 10.0);
GO
