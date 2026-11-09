-- =============================================================
-- KỊCH BẢN CƠ SỞ DỮ LIỆU HỢP NHẤT TOÀN DIỆN (FULL DATABASE SCRIPT)
-- Dự án: Đồ án cuối kỳ - Quản lý tiệm cà phê (DoAnCuoiKi_QuanLyTiemCaPhe)
-- Tích hợp đầy đủ cho cả 4 thành viên: Tuấn Hưng, Văn Thanh, Tấn Phát, Quang Hưng
-- =============================================================

USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'CoffeeShopDB')
BEGIN
    ALTER DATABASE CoffeeShopDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE CoffeeShopDB;
END
GO

CREATE DATABASE CoffeeShopDB;
GO

USE CoffeeShopDB;
GO

-- 1. BẢNG VAI TRÒ (Roles) - Văn Thanh
CREATE TABLE [dbo].[Roles] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [RoleName] NVARCHAR (50) NOT NULL UNIQUE
);
GO

-- 2. BẢNG MỨC LƯƠNG THEO CHỨC VỤ (Salaries) - Văn Thanh
CREATE TABLE [dbo].[Salaries] (
    [Id]       INT             IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [RoleId]   INT             NOT NULL,
    [Amount]   DECIMAL (18, 2) NOT NULL,
    [TimeUnit] NVARCHAR (20)  NOT NULL,
    FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id])
);
GO

-- 3. BẢNG NHÂN VIÊN (Employees) - Văn Thanh
CREATE TABLE [dbo].[Employees] (
    [Id]        INT             IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [FullName]  NVARCHAR (100)  NOT NULL,
    [Phone]     VARCHAR (15)    NOT NULL UNIQUE,
    [Address]   NVARCHAR (200)  NULL,
    [HireDate]  DATE            DEFAULT (getdate()) NOT NULL,
    [BirthDate] DATE            NULL,
    [Image]     VARCHAR (MAX)   NULL,
    [RoleId]    INT             NOT NULL,
    [Salary]    DECIMAL (18, 2) NULL DEFAULT 0,
    FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]),
    CHECK ([Salary] >= 0)
);
GO

-- 4. BẢNG TÀI KHOẢN NGƯỜI DÙNG (Users) - Văn Thanh
CREATE TABLE [dbo].[Users] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [EmployeeId]   INT           NOT NULL,
    [Username]     VARCHAR (50)  NOT NULL UNIQUE,
    [PasswordHash] VARCHAR (256) NOT NULL,
    [Salt]         VARCHAR (100) NOT NULL DEFAULT '',
    [RoleId]       INT           NOT NULL,
    [IsActive]     BIT           DEFAULT (1) NULL,
    FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees] ([Id]) ON DELETE CASCADE,
    FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id])
);
GO

-- 5. BẢNG CA TRỰC (Shifts) - Văn Thanh
CREATE TABLE [dbo].[Shifts] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [ShiftName] NVARCHAR (50) NOT NULL,
    [StartTime] TIME (7)      NOT NULL,
    [EndTime]   TIME (7)      NOT NULL
);
GO

-- 6. BẢNG PHÂN CA NHÂN VIÊN (EmployeeShifts) - Văn Thanh
CREATE TABLE [dbo].[EmployeeShifts] (
    [Id]         VARCHAR (20)  NOT NULL PRIMARY KEY,
    [EmployeeId] INT           NULL,
    [ShiftId]    INT           NULL,
    [WorkDate]   DATE          NULL,
    [Status]     NVARCHAR (50) DEFAULT (N'Chưa đăng ký') NULL,
    FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees] ([Id]),
    FOREIGN KEY ([ShiftId]) REFERENCES [dbo].[Shifts] ([Id])
);
GO

-- 7. BẢNG KHU VỰC BÀN (Areas) - Tấn Phát
CREATE TABLE [dbo].[Areas] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [AreaName]    NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (200) NULL
);
GO

-- 8. BẢNG BÀN ĂN (Tables) - Tấn Phát
CREATE TABLE [dbo].[Tables] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [TableName]  NVARCHAR (50) NOT NULL,
    [AreaId]     INT           NOT NULL,
    [Status]     NVARCHAR (20) DEFAULT (N'Trống') NULL,
    [CustomerId] VARCHAR (15)  NULL,
    FOREIGN KEY ([AreaId]) REFERENCES [dbo].[Areas] ([Id]) ON DELETE CASCADE
);
GO

-- 9. BẢNG KHÁCH HÀNG (Customers) - Quang Hưng & Văn Thanh
CREATE TABLE [dbo].[Customers] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [CustomerName]     NVARCHAR (100) NOT NULL,
    [Phone]            VARCHAR (15)   NOT NULL UNIQUE,
    [RewardPoints]     INT            DEFAULT (0) NULL,
    [MembershipTier]   NVARCHAR (20)  DEFAULT (N'Thường') NULL,
    [RegistrationDate] DATETIME       DEFAULT (getdate()) NULL,
    CHECK ([RewardPoints] >= 0)
);
GO

-- 10. BẢNG DANH MỤC MÓN (Categories) - Tuấn Hưng
CREATE TABLE [dbo].[Categories] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [CategoryName] NVARCHAR (100) NOT NULL UNIQUE,
    [Description]  NVARCHAR (200) NULL
);
GO

-- 11. BẢNG MÓN ĂN / ĐỒ UỐNG (Products) - Tuấn Hưng
CREATE TABLE [dbo].[Products] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [ProductName] NVARCHAR (100)  NOT NULL,
    [CategoryId]  INT             NOT NULL,
    [Price]       DECIMAL (18, 2) NOT NULL,
    [Unit]        NVARCHAR (20)   NOT NULL,
    [ImagePath]   NVARCHAR (500)  NULL,
    [IsActive]    BIT             DEFAULT (1) NULL,
    FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id]) ON DELETE CASCADE,
    CHECK ([Price] >= 0)
);
GO

-- 12. BẢNG CHƯƠNG TRÌNH KHUYẾN MÃI (Promotions) - Tuấn Hưng
CREATE TABLE [dbo].[Promotions] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [PromoCode]       VARCHAR (50) NOT NULL UNIQUE,
    [DiscountPercent] FLOAT (53)   NOT NULL,
    [StartDate]       DATETIME     NOT NULL,
    [EndDate]         DATETIME     NOT NULL,
    [IsActive]        BIT          DEFAULT (1) NULL,
    CHECK ([DiscountPercent] > 0 AND [DiscountPercent] <= 100),
    CONSTRAINT [CK_Dates] CHECK ([EndDate] >= [StartDate])
);
GO

-- 13. BẢNG NGUYÊN LIỆU KHO (Materials) - Tuấn Hưng
CREATE TABLE [dbo].[Materials] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [MaterialName]    NVARCHAR (100) NOT NULL UNIQUE,
    [Unit]            NVARCHAR (20)  NOT NULL,
    [QuantityInStock] FLOAT (53)     DEFAULT (0) NOT NULL,
    [MinThreshold]    FLOAT (53)     DEFAULT (0) NOT NULL,
    CHECK ([QuantityInStock] >= 0),
    CHECK ([MinThreshold] >= 0)
);
GO

-- 14. BẢNG LỊCH SỬ GIAO DỊCH KHO (InventoryTransactions) - Tuấn Hưng
CREATE TABLE [dbo].[InventoryTransactions] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [MaterialId]      INT           NOT NULL,
    [EmployeeId]      INT           NOT NULL,
    [TransactionType] NVARCHAR (20) NOT NULL,
    [Quantity]        FLOAT (53)    NOT NULL,
    [TransactionDate] DATETIME      DEFAULT (getdate()) NULL,
    FOREIGN KEY ([MaterialId]) REFERENCES [dbo].[Materials] ([Id]),
    FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees] ([Id]),
    CHECK ([TransactionType] IN (N'Xuất', N'Nhập', N'Xuất kho', N'Nhập kho')),
    CHECK ([Quantity] > 0)
);
GO

-- 15. BẢNG ĐƠN HÀNG (Orders) - Tấn Phát
CREATE TABLE [dbo].[Orders] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [TableId]        INT             NOT NULL,
    [EmployeeId]     INT             NOT NULL,
    [CustomerId]     INT             NULL,
    [Phone]          VARCHAR (15)    NULL,
    [OrderDate]      DATETIME        DEFAULT (getdate()) NULL,
    [TotalAmount]    DECIMAL (18, 2) NOT NULL DEFAULT 0,
    [DiscountAmount] DECIMAL (18, 2) DEFAULT (0) NULL,
    [FinalAmount]    DECIMAL (18, 2) NOT NULL DEFAULT 0,
    [Status]         NVARCHAR (20)   DEFAULT (N'Chưa thanh toán') NULL,
    [PromotionId]    INT             NULL,
    [OrderId]        INT             NULL,
    FOREIGN KEY ([TableId]) REFERENCES [dbo].[Tables] ([Id]),
    FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees] ([Id]),
    FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([Id]),
    FOREIGN KEY ([PromotionId]) REFERENCES [dbo].[Promotions] ([Id]),
    FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Users] ([Id]),
    CHECK ([TotalAmount] >= 0),
    CHECK ([DiscountAmount] >= 0),
    CHECK ([FinalAmount] >= 0)
);
GO

-- 16. BẢNG CHI TIẾT ĐƠN HÀNG (OrderDetails) - Tấn Phát
CREATE TABLE [dbo].[OrderDetails] (
    [Id]         INT             IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [OrderId]    INT             NOT NULL,
    [ProductId]  INT             NOT NULL,
    [Quantity]   INT             NOT NULL,
    [UnitPrice]  DECIMAL (18, 2) NOT NULL,
    [TotalPrice] DECIMAL (18, 2) NOT NULL,
    FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([Id]) ON DELETE CASCADE,
    FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id]),
    CHECK ([Quantity] > 0),
    CHECK ([UnitPrice] >= 0),
    CHECK ([TotalPrice] >= 0)
);
GO

-- =============================================================
-- DỮ LIỆU MẪU BAN ĐẦU (SEED DATA ĐẦY ĐỦ)
-- =============================================================

-- 1. Roles
INSERT INTO Roles (RoleName) VALUES 
(N'Quản lý'), (N'Thu ngân'), (N'Phục vụ'), (N'Pha chế');
GO

-- 2. Salaries
INSERT INTO Salaries (RoleId, Amount, TimeUnit) VALUES 
(1, 15000000, N'Tháng'),
(2, 28000, N'Giờ'),
(3, 23000, N'Giờ'),
(4, 25000, N'Giờ');
GO

-- 3. Employees
INSERT INTO Employees (FullName, Phone, Address, HireDate, BirthDate, RoleId, Salary) VALUES 
(N'Nguyễn Văn Quản Lý', '0901234567', N'Quận 1, TP.HCM', '2024-01-01', '1995-05-15', 1, 15000000),
(N'Trần Thị Thu Ngân', '0912345678', N'Quận 3, TP.HCM', '2024-02-15', '2000-08-20', 2, 8000000);
GO

-- 4. Users (Mật khẩu admin123 & 123456)
INSERT INTO Users (EmployeeId, Username, PasswordHash, Salt, RoleId, IsActive) VALUES 
(1, 'admin', 'admin123', '', 1, 1),
(2, 'thungan', '123456', '', 2, 1);
GO

-- 5. Shifts
INSERT INTO Shifts (ShiftName, StartTime, EndTime) VALUES 
(N'Ca 1 (Sáng)', '06:00:00', '12:00:00'),
(N'Ca 2 (Chiều)', '11:30:00', '17:30:00'),
(N'Ca 3 (Tối)', '16:00:00', '22:00:00');
GO

-- 6. Areas
INSERT INTO Areas (AreaName, Description) VALUES 
(N'Khu máy lạnh (Tầng 1)', N'Không gian yên tĩnh, làm việc'),
(N'Sân vườn', N'Không gian ngoài trời thoáng đãng'),
(N'Phòng VIP (Tầng 2)', N'Phòng họp nhóm riêng tư');
GO

-- 7. Tables
INSERT INTO Tables (TableName, AreaId, Status) VALUES 
(N'Bàn 01', 1, N'Trống'),
(N'Bàn 02', 1, N'Trống'),
(N'Bàn 03', 1, N'Trống'),
(N'Bàn 04', 2, N'Trống'),
(N'Bàn 05', 2, N'Trống'),
(N'Bàn VIP 01', 3, N'Trống');
GO

-- 8. Categories
INSERT INTO Categories (CategoryName, Description) VALUES 
(N'Cà phê truyền thống', N'Cà phê pha phin, pha máy'),
(N'Trà & Trà sữa', N'Các loại trà trái cây và trà sữa'),
(N'Nước ép & Sinh tố', N'Nước ép nguyên chất tươi mát'),
(N'Bánh ngọt', N'Bánh ăn kèm cà phê');
GO

-- 9. Products
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

-- 10. Promotions
INSERT INTO Promotions (PromoCode, DiscountPercent, StartDate, EndDate, IsActive) VALUES 
('CHAOMUNG', 10, '2024-01-01', '2027-12-31', 1),
('VIPCOFFEE', 15, '2024-01-01', '2027-12-31', 1);
GO

-- 11. Materials
INSERT INTO Materials (MaterialName, Unit, QuantityInStock, MinThreshold) VALUES 
(N'Hạt Cà Phê Robusta', N'Kg', 50.0, 10.0),
(N'Sữa đặc Ngôi Sao', N'Lon', 100.0, 20.0),
(N'Sữa tươi thanh trùng', N'Hộp', 60.0, 15.0),
(N'Trà đen cao cấp', N'Kg', 20.0, 5.0),
(N'Đường kính trắng', N'Kg', 40.0, 10.0);
GO

-- 12. Customers
INSERT INTO Customers (CustomerName, Phone, RewardPoints, MembershipTier, RegistrationDate) VALUES 
(N'Lê Hoàng Anh', '0988776655', 120, N'Bạc', '2024-03-01'),
(N'Phạm Thuỳ Dung', '0933221100', 50, N'Đồng', '2024-03-10');
GO
