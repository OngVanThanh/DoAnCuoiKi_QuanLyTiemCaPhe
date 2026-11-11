using System;
using System.Data.SqlClient;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.Utilities
{
    /// <summary>
    /// Tiện ích hỗ trợ quản trị và tự động đồng bộ/khởi tạo toàn diện cấu trúc CSDL (Schema Migration / Auto-Initialization).
    /// Đảm bảo tự động tạo đầy đủ các bảng Roles, Employees, Users, Areas, Tables, Categories, Products, Recipes, Materials... nếu CSDL người dùng chưa có.
    /// </summary>
    public static class DatabaseMigrationHelper
    {
        public static bool EnsureDatabaseSchema(out string message)
        {
            try
            {
                string connStr = ConfigHelper.GetConnectionString("CoffeeShopDB");
                if (string.IsNullOrWhiteSpace(connStr))
                {
                    message = "Chưa cấu hình chuỗi kết nối CoffeeShopDB!";
                    return false;
                }

                var builder = new SqlConnectionStringBuilder(connStr)
                {
                    ConnectTimeout = 5,
                    TrustServerCertificate = true
                };

                using (var conn = new SqlConnection(builder.ConnectionString))
                {
                    conn.Open();
                    string script = @"
-- 1. BẢNG VAI TRÒ (Roles)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Roles')
BEGIN
    CREATE TABLE [dbo].[Roles] (
        [Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
        [RoleName] NVARCHAR(50) NOT NULL UNIQUE
    );
    INSERT INTO [dbo].[Roles] ([RoleName]) VALUES 
    (N'Quản lý'), (N'Thu ngân'), (N'Pha chế/Phục vụ');
END;

-- 2. BẢNG MỨC LƯƠNG (Salaries)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Salaries')
BEGIN
    CREATE TABLE [dbo].[Salaries] (
        [Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
        [RoleId] INT NOT NULL UNIQUE,
        [Amount] DECIMAL(18, 2) NOT NULL,
        [TimeUnit] NVARCHAR(20) NOT NULL,
        FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles]([Id])
    );
    INSERT INTO [dbo].[Salaries] ([RoleId], [Amount], [TimeUnit]) VALUES
    (1, 15000000, N'Tháng'), (2, 8000000, N'Tháng'), (3, 7000000, N'Tháng');
END;

-- 3. BẢNG NHÂN VIÊN (Employees)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Employees')
BEGIN
    CREATE TABLE [dbo].[Employees] (
        [Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
        [FullName] NVARCHAR(100) NOT NULL,
        [Phone] VARCHAR(15) NOT NULL UNIQUE,
        [Address] NVARCHAR(200) NULL,
        [HireDate] DATE DEFAULT(getdate()) NOT NULL,
        [BirthDate] DATE NULL,
        [Image] VARCHAR(MAX) NULL,
        [RoleId] INT NOT NULL,
        [Salary] DECIMAL(18, 2) NULL DEFAULT 0,
        FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles]([Id])
    );
    INSERT INTO [dbo].[Employees] ([FullName], [Phone], [Address], [RoleId], [Salary]) VALUES
    (N'Nguyễn Văn Quản Lý', '0901234567', N'Quận 1, TP.HCM', 1, 15000000),
    (N'Trần Thị Thu Ngân', '0912345678', N'Quận 3, TP.HCM', 2, 8000000);
END;

-- 4. BẢNG TÀI KHOẢN (Users)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE [dbo].[Users] (
        [Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
        [EmployeeId] INT NOT NULL,
        [Username] VARCHAR(50) NOT NULL UNIQUE,
        [PasswordHash] VARCHAR(256) NOT NULL,
        [Salt] VARCHAR(100) NOT NULL DEFAULT '',
        [RoleId] INT NOT NULL,
        [IsActive] BIT DEFAULT(1) NULL,
        FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees]([Id]) ON DELETE CASCADE,
        FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles]([Id])
    );
    -- Seed tài khoản admin (admin123) và thungan (123456)
    INSERT INTO [dbo].[Users] ([EmployeeId], [Username], [PasswordHash], [Salt], [RoleId], [IsActive]) VALUES
    (1, 'admin', '4YUma6Q69CiQ97YVG17kFFFVD1HGqb2exAjnTpdQe0g=', 'MvzYIcst8H+HCiQecovlBA==', 1, 1),
    (2, 'thungan', 'ExGmL2DJ+2fEVbrgGAtbpkBl0c2eNsKncAH/rmQawuY=', 'RAq3VsoVEEY92GOgn+/wCA==', 2, 1);
END;

-- 5. BẢNG KHU VỰC (Areas)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Areas')
BEGIN
    CREATE TABLE [dbo].[Areas] (
        [Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
        [AreaName] NVARCHAR(50) NOT NULL UNIQUE,
        [Description] NVARCHAR(200) NULL
    );
    INSERT INTO [dbo].[Areas] ([AreaName], [Description]) VALUES
    (N'Khu máy lạnh (Tầng 1)', N'Không gian yên tĩnh, làm việc'),
    (N'Sân vườn', N'Không gian ngoài trời thoáng đãng'),
    (N'Phòng VIP (Tầng 2)', N'Phòng họp nhóm riêng tư');
END;

-- 6. BẢNG BÀN (Tables)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Tables')
BEGIN
    CREATE TABLE [dbo].[Tables] (
        [Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
        [TableName] NVARCHAR(80) NOT NULL,
        [AreaId] INT NOT NULL,
        [Status] NVARCHAR(20) DEFAULT(N'Trống') NULL,
        [CustomerId] VARCHAR(15) NULL,
        [StartTime] DATETIME NULL,
        [EndTime] DATETIME NULL,
        FOREIGN KEY ([AreaId]) REFERENCES [dbo].[Areas]([Id]) ON DELETE CASCADE
    );
    SET IDENTITY_INSERT [dbo].[Tables] ON;
    INSERT INTO [dbo].[Tables] ([Id], [TableName], [AreaId], [Status]) VALUES 
    (1, N'Khách tại quầy / Mang đi', 1, N'Trống');
    SET IDENTITY_INSERT [dbo].[Tables] OFF;
END;

-- 7. BẢNG DANH MỤC MÓN (Categories)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Categories')
BEGIN
    CREATE TABLE [dbo].[Categories] (
        [Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
        [CategoryName] NVARCHAR(100) NOT NULL UNIQUE,
        [Description] NVARCHAR(200) NULL
    );
    INSERT INTO [dbo].[Categories] ([CategoryName], [Description]) VALUES
    (N'Cà phê truyền thống', N'Đậm đà hương vị Việt'),
    (N'Trà & Macchiato', N'Thanh mát sảng khoái'),
    (N'Đá xay & Sinh tố', N'Mát lạnh ngày hè'),
    (N'Bánh ngọt & Tráng miệng', N'Thơm ngon mỗi ngày');
END;

-- 8. BẢNG SẢN PHẨM (Products)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Products')
BEGIN
    CREATE TABLE [dbo].[Products] (
        [Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
        [ProductName] NVARCHAR(100) NOT NULL UNIQUE,
        [CategoryId] INT NOT NULL,
        [Price] DECIMAL(18, 2) NOT NULL,
        [Unit] NVARCHAR(20) NOT NULL,
        [ImagePath] NVARCHAR(500) NULL,
        [IsActive] BIT DEFAULT(1) NULL,
        [PreparationSteps] NVARCHAR(MAX) NULL,
        FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories]([Id]) ON DELETE CASCADE
    );
    INSERT INTO [dbo].[Products] ([ProductName], [CategoryId], [Price], [Unit]) VALUES
    (N'Cà phê đen đá', 1, 25000, N'Ly'),
    (N'Cà phê sữa đá', 1, 29000, N'Ly'),
    (N'Bạc xỉu Sài Gòn', 1, 32000, N'Ly'),
    (N'Trà đào cam sả', 2, 38000, N'Ly'),
    (N'Trà ô long sen vàng', 2, 42000, N'Ly'),
    (N'Matcha đá xay', 3, 45000, N'Ly'),
    (N'Bánh Tiramisu', 4, 35000, N'Phần');
END;

-- 9. BẢNG CÔNG THỨC PHA CHẾ (Recipes)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Recipes')
BEGIN
    CREATE TABLE [dbo].[Recipes] (
        [Id]             INT            IDENTITY (1, 1) NOT NULL PRIMARY KEY,
        [ProductId]      INT            NOT NULL,
        [IngredientName] NVARCHAR (100) NOT NULL,
        [Quantity]       FLOAT (53)     NOT NULL,
        [Unit]           NVARCHAR (20)  NOT NULL,
        [Note]           NVARCHAR (200) NULL,
        FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id]) ON DELETE CASCADE
    );
    INSERT INTO [dbo].[Recipes] ([ProductId], [IngredientName], [Quantity], [Unit], [Note]) VALUES 
    (1, N'Hạt Cà Phê Robusta', 0.025, N'Kg', N'Xay mức mịn vừa'),
    (1, N'Đường kính trắng', 0.010, N'Kg', N'Gia giảm theo yêu cầu'),
    (2, N'Hạt Cà Phê Robusta', 0.025, N'Kg', N'Cốt phin 45ml'),
    (2, N'Sữa đặc Ngôi Sao', 0.030, N'Lon', N'Rót đáy ly'),
    (3, N'Hạt Cà Phê Robusta', 0.015, N'Kg', N'Tạo tầng cà phê'),
    (3, N'Sữa đặc Ngôi Sao', 0.035, N'Lon', N'Khuấy đều cùng sữa tươi'),
    (4, N'Trà đen cao cấp', 0.010, N'Kg', N'Ủ nước 90 độ C');
END;

-- 10. BẢNG KHUYẾN MÃI (Promotions)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Promotions')
BEGIN
    CREATE TABLE [dbo].[Promotions] (
        [Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
        [PromoCode] VARCHAR(50) NOT NULL UNIQUE,
        [DiscountPercent] FLOAT(53) NOT NULL,
        [StartDate] DATETIME NOT NULL,
        [EndDate] DATETIME NOT NULL,
        [IsActive] BIT DEFAULT(1) NULL
    );
    INSERT INTO [dbo].[Promotions] ([PromoCode], [DiscountPercent], [StartDate], [EndDate], [IsActive]) VALUES
    ('WELCOME10', 10, '2024-01-01', '2030-12-31', 1),
    ('VIPCOFFEE', 15, '2024-01-01', '2030-12-31', 1);
END;

-- 11. BẢNG KHÁCH HÀNG (Customers)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Customers')
BEGIN
    CREATE TABLE [dbo].[Customers] (
        [Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
        [CustomerName] NVARCHAR(100) NOT NULL,
        [Phone] VARCHAR(15) NOT NULL UNIQUE,
        [RewardPoints] INT DEFAULT(0) NULL,
        [MembershipTier] NVARCHAR(20) DEFAULT(N'Thường') NULL,
        [RegistrationDate] DATETIME DEFAULT(getdate()) NULL
    );
    INSERT INTO [dbo].[Customers] ([CustomerName], [Phone], [RewardPoints], [MembershipTier]) VALUES
    (N'Khách vãng lai', '0999999999', 0, N'Thường'),
    (N'Nguyễn Thị Lan', '0988776655', 120, N'VIP');
END;

-- 12. BẢNG NHIỆM VỤ (Mision)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Mision')
BEGIN
    CREATE TABLE [dbo].[Mision] (
        [Id]           INT           IDENTITY (1, 1) NOT NULL PRIMARY KEY,
        [MissisonName] NVARCHAR (50) NOT NULL
    );
    INSERT INTO [dbo].[Mision] ([MissisonName]) VALUES (N'Phục vụ'), (N'Pha chế'), (N'Thu ngân');
END;

-- 15. BẢNG CA LÀM VIỆC (Shifts)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Shifts')
BEGIN
    CREATE TABLE [dbo].[Shifts] (
        [Id]        INT           IDENTITY (1, 1) NOT NULL PRIMARY KEY,
        [ShiftName] NVARCHAR (50) NOT NULL,
        [StartTime] TIME (7)      NOT NULL,
        [EndTime]   TIME (7)      NOT NULL
    );
    INSERT INTO [dbo].[Shifts] ([ShiftName], [StartTime], [EndTime]) VALUES
    (N'Ca 1 (Sáng)', '06:00:00', '12:00:00'),
    (N'Ca 2 (Chiều)', '11:30:00', '17:30:00'),
    (N'Ca 3 (Tối)', '16:00:00', '22:00:00');
END;

-- 16. BẢNG PHÂN CA (EmployeeShifts)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'EmployeeShifts')
BEGIN
    CREATE TABLE [dbo].[EmployeeShifts] (
        [Id]         VARCHAR (20)  NOT NULL PRIMARY KEY,
        [EmployeeId] INT           NULL,
        [ShiftId]    INT           NULL,
        [WorkDate]   DATE          NULL,
        [Status]     NVARCHAR (50) DEFAULT (N'Chưa đăng ký') NULL,
        [MisionId]   INT           NULL,
        FOREIGN KEY ([MisionId]) REFERENCES [dbo].[Mision] ([Id]),
        FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees] ([Id]),
        FOREIGN KEY ([ShiftId]) REFERENCES [dbo].[Shifts] ([Id])
    );
END;

-- 17. BẢNG ĐƠN HÀNG (Orders)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Orders')
BEGIN
    CREATE TABLE [dbo].[Orders] (
        [Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
        [TableId] INT NOT NULL,
        [EmployeeId] INT NOT NULL,
        [CustomerId] INT NULL,
        [Phone] VARCHAR(15) NULL,
        [OrderDate] DATETIME DEFAULT(getdate()) NULL,
        [TotalAmount] DECIMAL(18, 2) NOT NULL DEFAULT 0,
        [DiscountAmount] DECIMAL(18, 2) DEFAULT(0) NULL,
        [FinalAmount] DECIMAL(18, 2) NOT NULL DEFAULT 0,
        [Status] NVARCHAR(20) DEFAULT(N'Chưa thanh toán') NULL,
        [PromotionId] INT NULL,
        [OrderId] INT NULL,
        FOREIGN KEY ([TableId]) REFERENCES [dbo].[Tables]([Id]),
        FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees]([Id]),
        FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers]([Id]) ON DELETE SET NULL,
        FOREIGN KEY ([PromotionId]) REFERENCES [dbo].[Promotions]([Id]) ON DELETE SET NULL
    );
END;

-- 18. BẢNG CHI TIẾT ĐƠN HÀNG (OrderDetails)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'OrderDetails')
BEGIN
    CREATE TABLE [dbo].[OrderDetails] (
        [Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
        [OrderId] INT NOT NULL,
        [ProductId] INT NOT NULL,
        [Quantity] INT NOT NULL,
        [UnitPrice] DECIMAL(18, 2) NOT NULL,
        [TotalPrice] DECIMAL(18, 2) NOT NULL,
        FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders]([Id]) ON DELETE CASCADE,
        FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products]([Id])
    );
END;

-- 19. BỔ SUNG CỘT BỊ THIẾU NẾU DB CŨ TỪ TRƯỚC
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Products')
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Products') AND name = 'PreparationSteps')
    BEGIN
        ALTER TABLE [dbo].[Products] ADD [PreparationSteps] NVARCHAR (MAX) NULL;
    END
END;

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Employees')
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Employees') AND name = 'Image')
    BEGIN
        ALTER TABLE [dbo].[Employees] ADD [Image] VARCHAR (MAX) NULL;
    END
END;
";
                    using (var cmd = new SqlCommand(script, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                ConfigHelper.InvalidateCache();
                message = "Đã kiểm tra và đồng bộ đầy đủ toàn bộ cấu trúc bảng CSDL thành công!";
                return true;
            }
            catch (Exception ex)
            {
                message = "Lỗi khi đồng bộ cấu trúc CSDL: " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Tự động dò tìm SQL Server / LocalDB khả dụng, tự động tạo CSDL CoffeeShopDB và khởi tạo toàn bộ 15 bảng + dữ liệu mẫu.
        /// Đảm bảo người dùng hoặc giảng viên mở app là chạy ngay (Zero-Config Deployment).
        /// </summary>
        public static bool AutoInitializeDatabase()
        {
            try
            {
                // Nếu máy sử dụng SQL Server LocalDB, đảm bảo instance MSSQLLocalDB đã được khởi động
                try
                {
                    var psi = new System.Diagnostics.ProcessStartInfo("sqllocaldb", "start MSSQLLocalDB")
                    {
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
                    };
                    using (var p = System.Diagnostics.Process.Start(psi))
                    {
                        p?.WaitForExit(2000);
                    }
                }
                catch
                {
                    // Bỏ qua nếu máy không có sqllocaldb CLI
                }

                string[] candidateServers = new string[] { ".", "localhost", @".\SQLEXPRESS", @"(localdb)\MSSQLLocalDB" };
                string workingServer = null;

                foreach (var server in candidateServers)
                {
                    try
                    {
                        string masterConn = $"Data Source={server};Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=2;";
                        using (var conn = new SqlConnection(masterConn))
                        {
                            conn.Open();
                            workingServer = server;

                            using (var cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = @"
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'CoffeeShopDB')
BEGIN
    CREATE DATABASE [CoffeeShopDB];
END";
                                cmd.ExecuteNonQuery();
                            }
                            break;
                        }
                    }
                    catch
                    {
                        // Thử máy chủ tiếp theo
                    }
                }

                if (string.IsNullOrEmpty(workingServer))
                {
                    return false;
                }

                string appConn = $"Data Source={workingServer};Initial Catalog=CoffeeShopDB;Integrated Security=True;TrustServerCertificate=True;";
                ConfigHelper.SetWorkingConnectionString("CoffeeShopDB", appConn);

                return EnsureDatabaseSchema(out _);
            }
            catch
            {
                return false;
            }
        }
    }
}
