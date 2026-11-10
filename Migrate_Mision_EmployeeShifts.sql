-- ============================================================================
-- SCRIPT MIGRATION: CẬP NHẬT BẢNG MISION VÀ EMPLOYEESHIFTS
-- Dự án: Hệ thống Quản Lý Tiệm Cà Phê (DoAnCuoiKi_QuanLyTiemCaPhe)
-- Mục đích: Bổ sung bảng Nhiệm vụ (Mision) và liên kết phân ca theo nhiệm vụ
-- ============================================================================

USE CoffeeShopDB;
GO

PRINT N'Bắt đầu quá trình cập nhật cấu trúc Database...';
GO

-- 1. TẠO BẢNG MISION NẾU CHƯA TỒN TẠI
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Mision')
BEGIN
    CREATE TABLE [dbo].[Mision] (
        [Id]           INT           IDENTITY (1, 1) NOT NULL PRIMARY KEY,
        [MissisonName] NVARCHAR (50) NOT NULL
    );
    PRINT N'[OK] Đã tạo bảng Mision thành công.';
END
ELSE
BEGIN
    PRINT N'[INFO] Bảng Mision đã tồn tại sẵn.';
END
GO

-- 2. THÊM 3 NHIỆM VỤ MẪU NẾU BẢNG MISION ĐANG TRỐNG
IF NOT EXISTS (SELECT 1 FROM [dbo].[Mision])
BEGIN
    INSERT INTO [dbo].[Mision] ([MissisonName]) VALUES 
    (N'Phục vụ'), 
    (N'Pha chế'), 
    (N'Thu ngân');
    PRINT N'[OK] Đã thêm 3 nhiệm vụ mặc định: Phục vụ, Pha chế, Thu ngân.';
END
ELSE
BEGIN
    PRINT N'[INFO] Bảng Mision đã có dữ liệu nhiệm vụ.';
END
GO

-- 3. XỬ LÝ BẢNG EMPLOYEESHIFTS
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'EmployeeShifts')
BEGIN
    -- Xóa các bản ghi ca làm việc cũ để form tự động sinh mã ca chuẩn mới (yyMMdd + C + ca + mã nhiệm vụ)
    DELETE FROM [dbo].[EmployeeShifts];
    PRINT N'[OK] Đã dọn dẹp các bản ghi phân ca cũ.';

    -- Bổ sung cột MisionId nếu chưa có
    IF NOT EXISTS (
        SELECT * FROM sys.columns 
        WHERE object_id = OBJECT_ID('dbo.EmployeeShifts') AND name = 'MisionId'
    )
    BEGIN
        ALTER TABLE [dbo].[EmployeeShifts]
        ADD [MisionId] INT NULL;
        PRINT N'[OK] Đã thêm cột MisionId vào bảng EmployeeShifts.';
    END

    -- Bổ sung ràng buộc khóa ngoại FK_EmployeeShifts_Mision nếu chưa có
    IF NOT EXISTS (
        SELECT * FROM sys.foreign_keys 
        WHERE parent_object_id = OBJECT_ID('dbo.EmployeeShifts') 
          AND name = 'FK_EmployeeShifts_Mision'
    )
    BEGIN
        ALTER TABLE [dbo].[EmployeeShifts]
        ADD CONSTRAINT FK_EmployeeShifts_Mision 
        FOREIGN KEY ([MisionId]) REFERENCES [dbo].[Mision] ([Id]);
        PRINT N'[OK] Đã tạo khóa ngoại FK_EmployeeShifts_Mision liên kết sang bảng Mision.';
    END
END
ELSE
BEGIN
    -- Trường hợp bảng EmployeeShifts chưa từng được tạo
    CREATE TABLE [dbo].[EmployeeShifts] (
        [Id]         VARCHAR (20)  NOT NULL PRIMARY KEY,
        [EmployeeId] INT           NULL,
        [ShiftId]    INT           NULL,
        [WorkDate]   DATE          NULL,
        [Status]     NVARCHAR (50) DEFAULT (N'Chưa đăng ký') NULL,
        [MisionId]   INT           NULL,
        CONSTRAINT FK_EmployeeShifts_Mision FOREIGN KEY ([MisionId]) REFERENCES [dbo].[Mision] ([Id]),
        CONSTRAINT FK_EmployeeShifts_Employees FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees] ([Id]),
        CONSTRAINT FK_EmployeeShifts_Shifts FOREIGN KEY ([ShiftId]) REFERENCES [dbo].[Shifts] ([Id])
    );
    PRINT N'[OK] Đã tạo mới bảng EmployeeShifts đầy đủ cột và khóa ngoại.';
END
GO

PRINT N'============================================================================';
PRINT N'=== HOÀN TẤT CẬP NHẬT DATABASE CHO CHỨC NĂNG PHÂN CA & NHIỆM VỤ! ===';
PRINT N'============================================================================';
GO
