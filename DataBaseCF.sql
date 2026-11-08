CREATE TABLE [dbo].[Categories] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [CategoryName] NVARCHAR (100) NOT NULL,
    [Description]  NVARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([CategoryName] ASC)
);
CREATE TABLE [dbo].[Customers] (
    [Phone]          VARCHAR (15)   NOT NULL,
    [CustomerName]   NVARCHAR (100) NOT NULL,
    [RewardPoints]   INT            DEFAULT ((0)) NULL,
    [MembershipTier] NVARCHAR (20)  DEFAULT (N'Thường') NULL,
    [RegisterDate]   DATE           DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Phone] ASC),
    CHECK ([RewardPoints] >= (0)),
    CHECK ([MembershipTier] = N'Kim Cương'
           OR [MembershipTier] = N'Vàng'
           OR [MembershipTier] = N'Bạc'
           OR [MembershipTier] = N'Thường')
);
CREATE TABLE [dbo].[Employees] (
    [Id]        INT             IDENTITY (1, 1) NOT NULL,
    [FullName]  NVARCHAR (100)  NOT NULL,
    [Phone]     VARCHAR (15)    NOT NULL,
    [Address]   NVARCHAR (200)  NOT NULL,
    [Salary]    DECIMAL (18, 2) NULL,
    [HireDate]  DATE            DEFAULT (getdate()) NOT NULL,
    [BirthDate] DATE            NULL,
    [Image]     VARCHAR (MAX)   NULL,
    [RoleId]    INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Phone] ASC),
    FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]),
    CHECK ([Salary]>=(0))
);
CREATE TABLE [dbo].[EmployeeShifts] (
    [Id]         AS            ((((right(CONVERT([varchar](4),datepart(year,[WorkDate])),(2))+right('0'+CONVERT([varchar](2),datepart(month,[WorkDate])),(2)))+right('0'+CONVERT([varchar](2),datepart(day,[WorkDate])),(2)))+'C')+CONVERT([varchar](5),[ShiftId])) PERSISTED NOT NULL,
    [EmployeeId] INT           NOT NULL,
    [ShiftId]    INT           NOT NULL,
    [WorkDate]   DATE          NOT NULL,
    [Status]     NVARCHAR (50) DEFAULT (N'Đã đăng ký') NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_Employee_Shift_Date] UNIQUE NONCLUSTERED ([EmployeeId] ASC, [ShiftId] ASC, [WorkDate] ASC),
    CONSTRAINT [FK_EmployeeShifts_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees] ([Id]),
    CONSTRAINT [FK_EmployeeShifts_Shifts] FOREIGN KEY ([ShiftId]) REFERENCES [dbo].[Shifts] ([Id])
);
CREATE TABLE [dbo].[InventoryTransactions] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [MaterialId]      INT           NOT NULL,
    [EmployeeId]      INT           NOT NULL,
    [TransactionType] NVARCHAR (20) NOT NULL,
    [Quantity]        FLOAT (53)    NOT NULL,
    [TransactionDate] DATETIME      DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([MaterialId]) REFERENCES [dbo].[Materials] ([Id]),
    FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees] ([Id]),
    CHECK ([TransactionType]=N'Xuất' OR [TransactionType]=N'Nhập'),
    CHECK ([Quantity]>(0))
);
CREATE TABLE [dbo].[Materials] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [MaterialName]    NVARCHAR (100) NOT NULL,
    [Unit]            NVARCHAR (20)  NOT NULL,
    [QuantityInStock] FLOAT (53)     DEFAULT ((0)) NOT NULL,
    [MinThreshold]    FLOAT (53)     DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([MaterialName] ASC),
    CHECK ([QuantityInStock]>=(0)),
    CHECK ([MinThreshold]>=(0))
);
CREATE TABLE [dbo].[OrderDetails] (
    [OrderId]    INT             NOT NULL,
    [ProductId]  INT             NOT NULL,
    [Quantity]   INT             NOT NULL,
    [UnitPrice]  DECIMAL (18, 2) NOT NULL,
    [TotalPrice] DECIMAL (18, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([ProductId] ASC, [OrderId] ASC),
    FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([Id]) ON DELETE CASCADE,
    FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id]),
    CHECK ([Quantity]>(0)),
    CHECK ([UnitPrice]>=(0)),
    CHECK ([TotalPrice]>=(0))
);
CREATE TABLE [dbo].[Orders] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [Phone]          VARCHAR (15)    NULL,
    [OrderDate]      DATETIME        DEFAULT (getdate()) NULL,
    [TotalAmount]    DECIMAL (18, 2) NOT NULL,
    [DiscountAmount] DECIMAL (18, 2) DEFAULT ((0)) NULL,
    [FinalAmount]    DECIMAL (18, 2) NOT NULL,
    [PromotionId]    INT             NULL,
    [OrderId]        INT             NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Users] ([Id]),
    FOREIGN KEY ([Phone]) REFERENCES [dbo].[Customers] ([Phone]),
    FOREIGN KEY ([PromotionId]) REFERENCES [dbo].[Promotions] ([Id]),
    CHECK ([TotalAmount]>=(0)),
    CHECK ([DiscountAmount]>=(0)),
    CHECK ([FinalAmount]>=(0))
);
CREATE TABLE [dbo].[Products] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [ProductName] NVARCHAR (100)  NOT NULL,
    [CategoryId]  INT             NOT NULL,
    [Price]       DECIMAL (18, 2) NOT NULL,
    [Unit]        NVARCHAR (20)   NOT NULL,
    [ImagePath]   NVARCHAR (500)  NULL,
    [IsActive]    BIT             DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id]) ON DELETE CASCADE,
    CHECK ([Price]>=(0))
);
CREATE TABLE [dbo].[Promotions] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [PromoCode]       VARCHAR (50) NOT NULL,
    [DiscountPercent] FLOAT (53)   NOT NULL,
    [StartDate]       DATETIME     NOT NULL,
    [EndDate]         DATETIME     NOT NULL,
    [IsActive]        BIT          DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([PromoCode] ASC),
    CHECK ([DiscountPercent] > (0)
           AND [DiscountPercent] <= (100)),
    CONSTRAINT [CK_Dates] CHECK ([EndDate] >= [StartDate])
);
CREATE TABLE [dbo].[Shifts] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [ShiftName] NVARCHAR (50) NOT NULL,
    [StartTime] TIME (7)      NOT NULL,
    [EndTime]   TIME (7)      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[Roles] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [RoleName] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([RoleName] ASC)
);
CREATE TABLE [dbo].[Tables] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [TableName]  NVARCHAR (50) NOT NULL,
    [Status]     NVARCHAR (20) DEFAULT (N'Trống') NULL,
    [CustomerId] VARCHAR (15)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CHECK ([Status]=N'Đã đặt' OR [Status]=N'Trống')
);
CREATE TABLE [dbo].[Users] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [EmployeeId]   INT           NOT NULL,
    [Username]     VARCHAR (50)  NOT NULL,
    [PasswordHash] VARCHAR (256) NOT NULL,
    [Salt]         VARCHAR (100) NOT NULL,
    [RoleId]       INT           NOT NULL,
    [IsActive]     BIT           DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Username] ASC),
    FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees] ([Id]) ON DELETE CASCADE,
    FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id])
);
-- Dữ liệu bảng Shift
-- 1	Ca 1 (Sáng)	06:00:00	12:00:00
-- 2	Ca 2 (Chiều)	11:30:00	17:30:00
-- 3	Ca 3 (Tối)	16:00:00	22:00:00