USE [MyStore2026]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 20/11/2025 10:00:18 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 20/11/2025 10:00:18 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerName] [nvarchar](max) NOT NULL,
	[CustomerPhone] [nvarchar](15) NOT NULL,
	[CustomerEmail] [nvarchar](max) NULL,
	[CustomerAddress] [nvarchar](max) NULL,
	[Username] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 20/11/2025 10:00:18 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[OrderDate] [date] NOT NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[PaymentStatus] [nvarchar](max) NULL,
	[AddressDelivery] [nvarchar](max) NOT NULL,
	[PaymentMethod] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 20/11/2025 10:00:18 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[OrderID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 20/11/2025 10:00:18 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[ProductName] [nvarchar](max) NOT NULL,
	[ProductDecription] [nvarchar](max) NOT NULL,
	[ProductPrice] [decimal](18, 2) NOT NULL,
	[ProductImage] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 20/11/2025 10:00:18 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Username] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[UserRole] [nchar](1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (4, N'Iphone')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (5, N'Ipad')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (7, N'Mac')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (8, N'Watch')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 
INSERT [dbo].[Customer] ([CustomerID], [CustomerName], [CustomerPhone], [CustomerEmail], [CustomerAddress], [Username]) VALUES (16, N'phung anh kiet', N'0937378483', N'34343e3@gmail.com', N'q10', N'phunganhkietdz1252006')
INSERT [dbo].[Customer] ([CustomerID], [CustomerName], [CustomerPhone], [CustomerEmail], [CustomerAddress], [Username]) VALUES (17, N'phunkiet', N'0963730809', N'34343e3@gmail.com', N'q10', N'kiet12345')
INSERT [dbo].[Customer] ([CustomerID], [CustomerName], [CustomerPhone], [CustomerEmail], [CustomerAddress], [Username]) VALUES (18, N'phung anh kiet', N'0937378483', N'343433@gmail.com', N'q10', N'12kiet12')
INSERT [dbo].[Customer] ([CustomerID], [CustomerName], [CustomerPhone], [CustomerEmail], [CustomerAddress], [Username]) VALUES (19, N'phung anh kiet', N'0937378483', N'343433@gmail.com', N'q10', N'kiet123456')
INSERT [dbo].[Customer] ([CustomerID], [CustomerName], [CustomerPhone], [CustomerEmail], [CustomerAddress], [Username]) VALUES (20, N'phung anh kiet', N'0937378483', N'343433@gmail.com', N'q10', N'kiet1234567')
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 
INSERT [dbo].[Order] ([OrderID], [CustomerID], [OrderDate], [TotalAmount], [PaymentStatus], [AddressDelivery], [PaymentMethod]) VALUES (4, 17, CAST(N'2025-11-16' AS Date), CAST(45234000000.00 AS Decimal(18, 2)), N'Chưa thanh toán', N'q10', N'Ti?n m?t (COD)')
INSERT [dbo].[Order] ([OrderID], [CustomerID], [OrderDate], [TotalAmount], [PaymentStatus], [AddressDelivery], [PaymentMethod]) VALUES (5, 17, CAST(N'2025-11-16' AS Date), CAST(30111000.00 AS Decimal(18, 2)), N'Chưa thanh toán', N'q10', N'Ti?n m?t (COD)')
INSERT [dbo].[Order] ([OrderID], [CustomerID], [OrderDate], [TotalAmount], [PaymentStatus], [AddressDelivery], [PaymentMethod]) VALUES (6, 17, CAST(N'2025-11-16' AS Date), CAST(124600000.00 AS Decimal(18, 2)), N'Chưa thanh toán', N'q10', N'Ti?n m?t (COD)')
INSERT [dbo].[Order] ([OrderID], [CustomerID], [OrderDate], [TotalAmount], [PaymentStatus], [AddressDelivery], [PaymentMethod]) VALUES (7, 17, CAST(N'2025-11-16' AS Date), CAST(1600000.00 AS Decimal(18, 2)), N'Chưa thanh toán', N'q10', N'Ti?n m?t (COD)')
INSERT [dbo].[Order] ([OrderID], [CustomerID], [OrderDate], [TotalAmount], [PaymentStatus], [AddressDelivery], [PaymentMethod]) VALUES (8, 17, CAST(N'2025-11-16' AS Date), CAST(1600000.00 AS Decimal(18, 2)), N'Chưa thanh toán', N'q10', N'Ti?n m?t (COD)')
INSERT [dbo].[Order] ([OrderID], [CustomerID], [OrderDate], [TotalAmount], [PaymentStatus], [AddressDelivery], [PaymentMethod]) VALUES (9, 18, CAST(N'2025-11-16' AS Date), CAST(132000000.00 AS Decimal(18, 2)), N'Chưa thanh toán', N'q10,eeee', N'Ti?n m?t (COD)')
INSERT [dbo].[Order] ([OrderID], [CustomerID], [OrderDate], [TotalAmount], [PaymentStatus], [AddressDelivery], [PaymentMethod]) VALUES (10, 19, CAST(N'2025-11-17' AS Date), CAST(80000000.00 AS Decimal(18, 2)), N'Chưa thanh toán', N'q10', N'Ti?n m?t (COD)')
INSERT [dbo].[Order] ([OrderID], [CustomerID], [OrderDate], [TotalAmount], [PaymentStatus], [AddressDelivery], [PaymentMethod]) VALUES (11, 20, CAST(N'2025-11-17' AS Date), CAST(1600000.00 AS Decimal(18, 2)), N'Chưa thanh toán', N'q11', N'Ti?n m?t (COD)')
INSERT [dbo].[Order] ([OrderID], [CustomerID], [OrderDate], [TotalAmount], [PaymentStatus], [AddressDelivery], [PaymentMethod]) VALUES (12, 17, CAST(N'2025-11-18' AS Date), CAST(16000000.00 AS Decimal(18, 2)), N'Chưa thanh toán', N'q10', N'Ti?n m?t (COD)')
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetail] ON 
INSERT [dbo].[OrderDetail] ([ID], [ProductID], [OrderID], [Quantity], [UnitPrice]) VALUES (1, 21, 4, 1, CAST(45234000000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetail] ([ID], [ProductID], [OrderID], [Quantity], [UnitPrice]) VALUES (2, 20, 5, 1, CAST(30111000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetail] ([ID], [ProductID], [OrderID], [Quantity], [UnitPrice]) VALUES (3, 33, 6, 1, CAST(1600000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetail] ([ID], [ProductID], [OrderID], [Quantity], [UnitPrice]) VALUES (4, 23, 6, 1, CAST(123000000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetail] ([ID], [ProductID], [OrderID], [Quantity], [UnitPrice]) VALUES (5, 33, 7, 1, CAST(1600000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetail] ([ID], [ProductID], [OrderID], [Quantity], [UnitPrice]) VALUES (6, 33, 8, 1, CAST(1600000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetail] ([ID], [ProductID], [OrderID], [Quantity], [UnitPrice]) VALUES (7, 36, 9, 1, CAST(132000000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetail] ([ID], [ProductID], [OrderID], [Quantity], [UnitPrice]) VALUES (8, 34, 10, 5, CAST(16000000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetail] ([ID], [ProductID], [OrderID], [Quantity], [UnitPrice]) VALUES (9, 33, 11, 1, CAST(1600000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetail] ([ID], [ProductID], [OrderID], [Quantity], [UnitPrice]) VALUES (10, 34, 12, 1, CAST(16000000.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[OrderDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 
INSERT [dbo].[Product] ([ProductID], [CategoryID], [ProductName], [ProductDecription], [ProductPrice], [ProductImage]) VALUES (12, 4, N'Iphone 17 Air', N'Thiết kế siêu mỏng – siêu nhẹ

Màn hình 6.7 inch OLED 120Hz

Chip A18 / A19 Bionic

Camera 48MP 3 ống kính

Pin 4500mAh + sạc nhanh USB-C

Khung Aluminum hoặc Titanium siêu nhẹ', CAST(25000000.00 AS Decimal(18, 2)), N'~/Content/Images/Products/0049406_iphone-air-256gb_240 (1).png')
INSERT [dbo].[Product] ([ProductID], [CategoryID], [ProductName], [ProductDecription], [ProductPrice], [ProductImage]) VALUES (13, 5, N'Ipad Pro M5', N'Chip Apple M5 – hiệu năng vượt trội, tối ưu AI & đồ họa 3D

Màn hình OLED ProMotion 120Hz – độ sáng cao, màu sắc cực sống động

Camera 12MP + LiDAR – chụp đẹp, scan 3D chuẩn xác

Face ID + USB-C Thunderbolt – tốc độ truyền dữ liệu cực nhanh

Hỗ trợ Apple Pencil thế hệ mới & Magic Keyboard Pro

Pin dùng cả ngày – tối ưu tiết kiệm điện năng', CAST(16000000.00 AS Decimal(18, 2)), N'~/Content/Images/Products/0051666_ipad-pro-m5-11-inch-wi-fi-256gb_240.png')
INSERT [dbo].[Product] ([ProductID], [CategoryID], [ProductName], [ProductDecription], [ProductPrice], [ProductImage]) VALUES (15, 5, N'Ipad Pro M4', N'Chip Apple M5 – hiệu năng vượt trội, tối ưu AI & đồ họa 3D

Màn hình OLED ProMotion 120Hz – độ sáng cao, màu sắc cực sống động

Camera 12MP + LiDAR – chụp đẹp, scan 3D chuẩn xác

Face ID + USB-C Thunderbolt – tốc độ truyền dữ liệu cực nhanh

Hỗ trợ Apple Pencil thế hệ mới & Magic Keyboard Pro

Pin dùng cả ngày – tối ưu tiết kiệm điện năng', CAST(14999000.00 AS Decimal(18, 2)), N'~/Content/Images/Products/0051114_ipad-pro-m5-11-inch-wi-fi-cellular-256gb_240.jpeg')
INSERT [dbo].[Product] ([ProductID], [CategoryID], [ProductName], [ProductDecription], [ProductPrice], [ProductImage]) VALUES (16, 7, N'Macbook pro m5', N'Chip Apple M5 – hiệu năng vượt trội, tối ưu AI & đồ họa 3D

Màn hình OLED ProMotion 120Hz – độ sáng cao, màu sắc cực sống động

Camera 12MP + LiDAR – chụp đẹp, scan 3D chuẩn xác

Face ID + USB-C Thunderbolt – tốc độ truyền dữ liệu cực nhanh

Hỗ trợ Apple Pencil thế hệ mới & Magic Keyboard Pro

Pin dùng cả ngày – tối ưu tiết kiệm điện năng', CAST(26000000.00 AS Decimal(18, 2)), N'~/Content/Images/Products/0051653_macbook-pro-14-inch-m5-2025-10-core-gpu-10-core-cpu-16gb-ram-512gb-ssd_240.png')
INSERT [dbo].[Product] ([ProductID], [CategoryID], [ProductName], [ProductDecription], [ProductPrice], [ProductImage]) VALUES (18, 7, N'Macbook Air m4', N'Chip Apple M5 – hiệu năng vượt trội, tối ưu AI & đồ họa 3D

Màn hình OLED ProMotion 120Hz – độ sáng cao, màu sắc cực sống động

Camera 12MP + LiDAR – chụp đẹp, scan 3D chuẩn xác

Face ID + USB-C Thunderbolt – tốc độ truyền dữ liệu cực nhanh

Hỗ trợ Apple Pencil thế hệ mới & Magic Keyboard Pro

Pin dùng cả ngày – tối ưu tiết kiệm điện năng', CAST(15000000.00 AS Decimal(18, 2)), N'~/Content/Images/Products/0036059_macbook-air-m4-13-inch-10-core-gpu-16gb-ram-512gb-ssd_240.jpeg')
INSERT [dbo].[Product] ([ProductID], [CategoryID], [ProductName], [ProductDecription], [ProductPrice], [ProductImage]) VALUES (19, 4, N'Iphone 15', N'Dung lượng: 128 GB, 256 GB, 512 GB 
Hỗ Trợ Apple

Kích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ Apple

Màn hình: OLED 6,1″ Super Retina XDR, Dynamic Island, độ sáng tối đa 2.000 nit 
Hỗ Trợ Apple

Kháng nước / bụi: IP68 (tối đa 6m / 30 phút) 
Hỗ Trợ Apple

Chip: A16 Bionic 
Hỗ Trợ Apple

Camera sau: 48 MP (chính) + 12 MP siêu rộng, zoom 2x quang học 
Hỗ Trợ Apple

Camera trước: 12 MP 
CellphoneS

Cổng kết nối: USB-C (thay Lightning) 
Apple

Khả năng ảnh chụp: HDR, Night mode, Photonic Engine, chế độ chân dung 
Hỗ Trợ Apple

Khả năng quay video: Hỗ trợ quay chất lượng cao (theo Apple) 
Apple

Hệ điều hành: iOS 17 khi ra mắt 
Apple

', CAST(22999000.00 AS Decimal(18, 2)), N'~/Content/Images/Products/0049405_iphone-17-256gb_240 (1).png')
INSERT [dbo].[Product] ([ProductID], [CategoryID], [ProductName], [ProductDecription], [ProductPrice], [ProductImage]) VALUES (20, 4, N'Iphone 15 pro max', N'Dung lượng: 128 GB, 256 GB, 512 GB 
Hỗ Trợ Apple

Kích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ Apple

Màn hình: OLED 6,1″ Super Retina XDR, Dynamic Island, độ sáng tối đa 2.000 nit 
Hỗ Trợ Apple

Kháng nước / bụi: IP68 (tối đa 6m / 30 phút) 
Hỗ Trợ Apple

Chip: A16 Bionic 
Hỗ Trợ Apple

Camera sau: 48 MP (chính) + 12 MP siêu rộng, zoom 2x quang học 
Hỗ Trợ Apple

Camera trước: 12 MP 
CellphoneS

Cổng kết nối: USB-C (thay Lightning) 
Apple

Khả năng ảnh chụp: HDR, Night mode, Photonic Engine, chế độ chân dung 
Hỗ Trợ Apple

Khả năng quay video: Hỗ trợ quay chất lượng cao (theo Apple) 
Apple

Hệ điều hành: iOS 17 khi ra mắt 
Apple

', CAST(30111000.00 AS Decimal(18, 2)), N'~/Content/Images/Products/0049406_iphone-air-256gb_240 (1).png')
INSERT [dbo].[Product] ([ProductID], [CategoryID], [ProductName], [ProductDecription], [ProductPrice], [ProductImage]) VALUES (21, 4, N'Iphone 13 pro max', N'Dung lượng: 128 GB, 256 GB, 512 GB 
Hỗ Trợ Apple

Kích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ Apple

Màn hình: OLED 6,1″ Super Retina XDR, Dynamic Island, độ sáng tối đa 2.000 nit 
Hỗ Trợ Apple

Kháng nước / bụi: IP68 (tối đa 6m / 30 phút) 
Hỗ Trợ Apple

Chip: A16 Bionic 
Hỗ Trợ Apple

Camera sau: 48 MP (chính) + 12 MP siêu rộng, zoom 2x quang học 
Hỗ Trợ Apple

Camera trước: 12 MP 
CellphoneS

Cổng kết nối: USB-C (thay Lightning) 
Apple

Khả năng ảnh chụp: HDR, Night mode, Photonic Engine, chế độ chân dung 
Hỗ Trợ Apple

Khả năng quay video: Hỗ trợ quay chất lượng cao (theo Apple) 
Apple

Hệ điều hành: iOS 17 khi ra mắt 
Apple

', CAST(45234000000.00 AS Decimal(18, 2)), N'~/Content/Images/Products/0049406_iphone-air-256gb_240 (1).png')
INSERT [dbo].[Product] ([ProductID], [CategoryID], [ProductName], [ProductDecription], [ProductPrice], [ProductImage]) VALUES (22, 5, N'Ipad Pro M6', N'

Kích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ Apple
Kích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ Apple
Màn hình: OLED 6,1″ Super Retina XDR, Dynamic Island, độ sáng tối đa 2.000 nit 
Hỗ Trợ Apple

Kháng nước / bụi: IP68 (tối đa 6m / 30 phút) 
Hỗ Trợ Apple

Chip: A16 Bionic 
Hỗ Trợ Apple

Camera sau: 48 MP (chính) + 12 MP siêu rộng, zoom 2x quang học 
Hỗ Trợ Apple

Camera trước: 12 MP 
CellphoneS

Cổng kết nối: USB-C (thay Lightning) 
Apple

Khả năng ảnh chụp: HDR, Night mode, Photonic Engine, chế độ chân dung 
Hỗ Trợ Apple

Khả năng quay video: Hỗ trợ quay chất lượng cao (theo Apple) 
Apple

', CAST(12000000.00 AS Decimal(18, 2)), N'~/Content/Images/Products/0051114_ipad-pro-m5-11-inch-wi-fi-cellular-256gb_240.jpeg')
INSERT [dbo].[Product] ([ProductID], [CategoryID], [ProductName], [ProductDecription], [ProductPrice], [ProductImage]) VALUES (23, 5, N'Ipad Pro M8', N'Kích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ AppleKích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ Kích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ Apple', CAST(123000000.00 AS Decimal(18, 2)), N'~/Content/Images/Products/0051666_ipad-pro-m5-11-inch-wi-fi-256gb_240.png')
INSERT [dbo].[Product] ([ProductID], [CategoryID], [ProductName], [ProductDecription], [ProductPrice], [ProductImage]) VALUES (24, 5, N'Ipad Pro M7', N'Kích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ AppleKích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ AppleKích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ Apple', CAST(34000000.00 AS Decimal(18, 2)), N'~/Content/Images/Products/0051666_ipad-pro-m5-11-inch-wi-fi-256gb_240.png')
INSERT [dbo].[Product] ([ProductID], [CategoryID], [ProductName], [ProductDecription], [ProductPrice], [ProductImage]) VALUES (25, 7, N'Macbook Air m7', N'Kích thước & trọng lượng
Hỗ Trợ AppleKích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ AppleKích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ Apple', CAST(56000000.00 AS Decimal(18, 2)), N'~/Content/Images/Products/0036014_macbook-air-m4-13-inch-8-core-gpu-16gb-ram-256gb-ssd_240 (1).jpeg')
INSERT [dbo].[Product] ([ProductID], [CategoryID], [ProductName], [ProductDecription], [ProductPrice], [ProductImage]) VALUES (26, 7, N'Macbook Air m8', N'Kích thước & trọng lượng: 14eee × 71,6 × 7,80 mm – 171g 
Hỗ Trợ Apple
Kích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ AppleKích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ Apple', CAST(12300000.00 AS Decimal(18, 2)), N'~/Content/Images/Products/0036014_macbook-air-m4-13-inch-8-core-gpu-16gb-ram-256gb-ssd_240 (1).jpeg')
INSERT [dbo].[Product] ([ProductID], [CategoryID], [ProductName], [ProductDecription], [ProductPrice], [ProductImage]) VALUES (28, 7, N'Macbook Air m10', N'Kích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ AppleKích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ AppleKích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ Apple', CAST(143500000.00 AS Decimal(18, 2)), N'~/Content/Images/Products/0036014_macbook-air-m4-13-inch-8-core-gpu-16gb-ram-256gb-ssd_240 (1).jpeg')
INSERT [dbo].[Product] ([ProductID], [CategoryID], [ProductName], [ProductDecription], [ProductPrice], [ProductImage]) VALUES (29, 8, N'Watch series 10', N'Kích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ AppleKích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ AppleKích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ AppleKích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ AppleKích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ AppleKích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ Apple', CAST(5000000.00 AS Decimal(18, 2)), N'~/Content/Images/Products/0030829_apple-watch-series-10-nhom-gps-cellular-46mm-sport-band_240.png')
INSERT [dbo].[Product] ([ProductID], [CategoryID], [ProductName], [ProductDecription], [ProductPrice], [ProductImage]) VALUES (30, 8, N'Watch series 11', N'Kích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ AppleKích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ AppleKích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ AppleKích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ Apple', CAST(100000.00 AS Decimal(18, 2)), N'~/Content/Images/Products/0030829_apple-watch-series-10-nhom-gps-cellular-46mm-sport-band_240.png')
INSERT [dbo].[Product] ([ProductID], [CategoryID], [ProductName], [ProductDecription], [ProductPrice], [ProductImage]) VALUES (31, 8, N'Watch series 12', N'Kích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ AppleKích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ AppleKích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ Apple', CAST(1500000.00 AS Decimal(18, 2)), N'~/Content/Images/Products/0049492_apple-watch-series-11-nhom-gps-42mm-sport-band-size-sm_240.jpeg')
INSERT [dbo].[Product] ([ProductID], [CategoryID], [ProductName], [ProductDecription], [ProductPrice], [ProductImage]) VALUES (32, 8, N'Watch series 1555', N'Kích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ AppleKích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ AppleKích thước & trọng lượng: 147,6 × 71,6 × 7,80 mm – 171g 
Hỗ Trợ Apple', CAST(45000000.00 AS Decimal(18, 2)), N'~/Content/Images/Products/0049492_apple-watch-series-11-nhom-gps-42mm-sport-band-size-sm_240.jpeg')
INSERT [dbo].[Product] ([ProductID], [CategoryID], [ProductName], [ProductDecription], [ProductPrice], [ProductImage]) VALUES (33, 4, N'Iphone 16 plus', N'Màn hình lớn (6.7″) rất hợp để xem video, chơi game hoặc lướt web.

Thời lượng pin cao, sử dụng thoải mái hơn – nhiều đánh giá khen “pin trâu”. 
The Guardian

Hiệu năng tốt với chip A18 mới, đủ mạnh cho hầu hết nhu cầu.

Camera chất lượng, đặc biệt là camera chính 48 MP.

Có các tính năng an toàn như SOS qua vệ tinh, phát hiện tai nạn.', CAST(1600000.00 AS Decimal(18, 2)), N'~/Content/Images/Products/0030772_iphone-16-plus-128gb_240.png')
INSERT [dbo].[Product] ([ProductID], [CategoryID], [ProductName], [ProductDecription], [ProductPrice], [ProductImage]) VALUES (34, 4, N'Iphone 16e', N'5G (sub-6 GHz) 
Apple

Wi-Fi 6, Bluetooth 5.3 
Apple

USB-C (sạc và dữ liệu) 
The Verge

Camera sau:

48 MP (chính) + 12 MP Tele 2x 
Apple

Hỗ trợ Zoom kỹ thuật số lên tới 10x 
Apple

Camera trước: 12 MP, TrueDepth, Face ID 
Apple
+1

Pin:

4005 mAh (theo PhoneArena) 
PhoneArena

Thời gian xem video lên đến ~26 giờ 
Apple

Sạc 50% trong ~30 phút với adapter ≥ 20W 
Apple

Hỗ trợ sạc không dây chuẩn Qi (7,5W), không có MagSafe 
Reddit
+1

Tính năng an toàn:

Face ID

Emergency SOS qua vệ tinh 
Apple

Crash Detection 
Apple

Môi trường: Thành phần nhôm tái chế cao, nhiều thành phần tái chế trong máy', CAST(16000000.00 AS Decimal(18, 2)), N'~/Content/Images/Products/0034910_iphone-16e-128gb_240.png')
INSERT [dbo].[Product] ([ProductID], [CategoryID], [ProductName], [ProductDecription], [ProductPrice], [ProductImage]) VALUES (36, 4, N'Iphone 18e', N'dsabhdhdhdhdhdhdhdhdhdhadasda', CAST(132000000.00 AS Decimal(18, 2)), N'~/Content/Images/Products/0034910_iphone-16e-128gb_240.png')
INSERT [dbo].[Product] ([ProductID], [CategoryID], [ProductName], [ProductDecription], [ProductPrice], [ProductImage]) VALUES (37, 4, N'iphhone ăê', N'dedeeeeeee', CAST(1000000000.00 AS Decimal(18, 2)), N'~/Content/Images/Products/0045655_iphone-15-128gb_240.png')
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
INSERT [dbo].[User] ([Username], [Password], [UserRole]) VALUES (N'12kiet12', N'AMWFwXgNjx0pkRUANK3fhjseLWIpJjhB7z5A91eZTsV2M6QvQfwjNgsb8SK0utyWZQ==', N'C')
INSERT [dbo].[User] ([Username], [Password], [UserRole]) VALUES (N'kiet12@#$', N'kiet1252006                                       ', N'1')
INSERT [dbo].[User] ([Username], [Password], [UserRole]) VALUES (N'kiet12345', N'AFt+p+uRH6xdtQAXL5doeqRjFiMsqb/xm2W4Lbrv8PtSzWIvHh4Ya0oCKfnw6aoZ9Q==', N'C')
INSERT [dbo].[User] ([Username], [Password], [UserRole]) VALUES (N'kiet123456', N'ALjkCFNHb/fC9hxc8LijFAVD9KdvRnMF6+23w2DmqCwcE7x8nxU0Zj4Hrx7IRqkdsg==', N'C')
INSERT [dbo].[User] ([Username], [Password], [UserRole]) VALUES (N'kiet1234567', N'AKhlP2jYohWmv8c5AyeiaN4G0JcDUpujhzwARuk05ow2om6tbPOXtJHeQHoLmxRzsQ==', N'C')
INSERT [dbo].[User] ([Username], [Password], [UserRole]) VALUES (N'kiet1r2@#$', N'kiet12520006                                      ', N'1')
INSERT [dbo].[User] ([Username], [Password], [UserRole]) VALUES (N'phunganhkietdz1252006', N'AESstC0TTTt+swX6BHlIqOGZ88FW4ZuSlGDzcfiekGokwTw4kwueuQWpnW5X9Dn+MA==', N'A')
GO
ALTER TABLE [dbo].[Order] ADD  DEFAULT ('Ti?n m?t (COD)') FOR [PaymentMethod]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_User_Customer] FOREIGN KEY([Username])
REFERENCES [dbo].[User] ([Username])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_User_Customer]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([OrderID])
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Pro_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([CategoryID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Pro_Category]
GO
