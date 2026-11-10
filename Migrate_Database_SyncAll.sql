-- ============================================================================
-- SCRIPT MIGRATION TỔNG HỢP: ĐỒNG BỘ TOÀN DIỆN CƠ SỞ DỮ LIỆU
-- Dự án: Hệ thống Quản Lý Tiệm Cà Phê (DoAnCuoiKi_QuanLyTiemCaPhe)
-- Mục đích: Đảm bảo database hiện tại trên SQL Server có đầy đủ 100% bảng & cột
-- An toàn: Có kiểm tra IF NOT EXISTS, không làm mất dữ liệu sẵn có
-- ============================================================================

USE CoffeeShopDB;
GO

PRINT N'Bắt đầu kiểm tra và đồng bộ cấu trúc Database...';
GO

-- 1. BẢNG EMPLOYEES: Bổ sung cột Image nếu thiếu
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Employees')
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Employees') AND name = 'Image')
    BEGIN
        ALTER TABLE [dbo].[Employees] ADD [Image] VARCHAR (MAX) NULL;
        PRINT N'[OK] Đã thêm cột Image vào bảng Employees.';
    END
END
GO

-- 2. BẢNG TABLES: Bổ sung StartTime, EndTime phục vụ đặt bàn POS nếu thiếu
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Tables')
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Tables') AND name = 'StartTime')
    BEGIN
        ALTER TABLE [dbo].[Tables] ADD [StartTime] DATETIME NULL;
        PRINT N'[OK] Đã thêm cột StartTime vào bảng Tables.';
    END

    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Tables') AND name = 'EndTime')
    BEGIN
        ALTER TABLE [dbo].[Tables] ADD [EndTime] DATETIME NULL;
        PRINT N'[OK] Đã thêm cột EndTime vào bảng Tables.';
    END
END
GO

-- 3. BẢNG PRODUCTS: Bổ sung PreparationSteps phục vụ công thức Barista nếu thiếu
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Products')
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Products') AND name = 'PreparationSteps')
    BEGIN
        ALTER TABLE [dbo].[Products] ADD [PreparationSteps] NVARCHAR (MAX) NULL;
        PRINT N'[OK] Đã thêm cột PreparationSteps vào bảng Products.';
    END
END
GO

-- 4. BẢNG ORDERS: Bổ sung cột Phone nếu thiếu
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Orders')
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Orders') AND name = 'Phone')
    BEGIN
        ALTER TABLE [dbo].[Orders] ADD [Phone] VARCHAR (15) NULL;
        PRINT N'[OK] Đã thêm cột Phone vào bảng Orders.';
    END
END
GO

-- 5. BẢNG MISION (NHIỆM VỤ PHÂN CA): Tạo bảng và seed data nếu thiếu
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Mision')
BEGIN
    CREATE TABLE [dbo].[Mision] (
        [Id]           INT           IDENTITY (1, 1) NOT NULL PRIMARY KEY,
        [MissisonName] NVARCHAR (50) NOT NULL
    );
    PRINT N'[OK] Đã tạo bảng Mision thành công.';
END
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Mision])
BEGIN
    INSERT INTO [dbo].[Mision] ([MissisonName]) VALUES 
    (N'Phục vụ'), 
    (N'Pha chế'), 
    (N'Thu ngân');
    PRINT N'[OK] Đã thêm 3 nhiệm vụ mẫu: Phục vụ, Pha chế, Thu ngân.';
END
GO

-- 6. BẢNG EMPLOYEESHIFTS: Bổ sung MisionId và khóa ngoại liên kết Mision nếu thiếu
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'EmployeeShifts')
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('dbo.EmployeeShifts') AND name = 'MisionId')
    BEGIN
        ALTER TABLE [dbo].[EmployeeShifts] ADD [MisionId] INT NULL;
        PRINT N'[OK] Đã thêm cột MisionId vào bảng EmployeeShifts.';
    END

    IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE parent_object_id = OBJECT_ID('dbo.EmployeeShifts') AND name = 'FK_EmployeeShifts_Mision')
    BEGIN
        ALTER TABLE [dbo].[EmployeeShifts]
        ADD CONSTRAINT FK_EmployeeShifts_Mision FOREIGN KEY ([MisionId]) REFERENCES [dbo].[Mision] ([Id]);
        PRINT N'[OK] Đã tạo khóa ngoại FK_EmployeeShifts_Mision.';
    END
END
GO

-- 7. BẢNG RECIPES (CÔNG THỨC PHA CHẾ CHO BARISTA): Tạo bảng và seed data nếu thiếu
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Recipes')
BEGIN
    CREATE TABLE [dbo].[Recipes] (
        [Id]             INT            IDENTITY (1, 1) NOT NULL PRIMARY KEY,
        [ProductId]      INT            NOT NULL,
        [IngredientName] NVARCHAR (100) NOT NULL,
        [Quantity]       FLOAT (53)     NOT NULL,
        [Unit]           NVARCHAR (20)  NOT NULL,
        [Note]           NVARCHAR (200) NULL,
        FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id]) ON DELETE CASCADE,
        CHECK ([Quantity] > 0)
    );
    PRINT N'[OK] Đã tạo bảng Recipes thành công.';

    -- Thêm dữ liệu công thức mẫu
    INSERT INTO [dbo].[Recipes] ([ProductId], [IngredientName], [Quantity], [Unit], [Note]) VALUES 
    (1, N'Hạt Cà Phê Robusta', 0.025, N'Kg', N'Xay mức mịn vừa'),
    (1, N'Đường kính trắng', 0.010, N'Kg', N'Gia giảm theo yêu cầu'),
    (2, N'Hạt Cà Phê Robusta', 0.025, N'Kg', N'Cốt phin 45ml'),
    (2, N'Sữa đặc Ngôi Sao', 0.030, N'Lon', N'Rót đáy ly'),
    (3, N'Hạt Cà Phê Robusta', 0.015, N'Kg', N'Tạo tầng cà phê'),
    (3, N'Sữa đặc Ngôi Sao', 0.035, N'Lon', N'Khuấy đều cùng sữa tươi'),
    (3, N'Sữa tươi thanh trùng', 0.060, N'Hộp', N'Sữa tươi không đường'),
    (4, N'Trà đen cao cấp', 0.010, N'Kg', N'Ủ nước 90 độ C'),
    (4, N'Đường kính trắng', 0.020, N'Kg', N'Nước đường tỉ lệ 1:1');
    PRINT N'[OK] Đã thêm dữ liệu công thức mẫu vào bảng Recipes.';
END
GO

-- 8. BẢNG SALARIES: Thêm ràng buộc kiểm tra lương không âm nếu chưa có
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Salaries')
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Salaries_Amount')
    BEGIN
        ALTER TABLE [dbo].[Salaries] ADD CONSTRAINT CK_Salaries_Amount CHECK ([Amount] >= 0);
        PRINT N'[OK] Đã bổ sung ràng buộc kiểm tra lương không âm.';
    END
END
GO

PRINT N'============================================================================';
PRINT N'=== HOÀN TẤT ĐỒNG BỘ 100% TOÀN BỘ CẤU TRÚC DATABASE! ===';
PRINT N'============================================================================';
GO
