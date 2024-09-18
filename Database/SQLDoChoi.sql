﻿
--Tạo bảng chi tiết hóa đơn
CREATE TABLE ChiTietHoaDon (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    DonHangID INT,                                  -- Khóa ngoại liên kết với bảng HoaDon
    SanPhamID INT,                                 -- Khóa ngoại liên kết với bảng SanPham
    SoLuong INT NOT NULL,                          -- Số lượng sản phẩm
	GiaGiam DECIMAL(10, 0) DEFAULT NULL,
	ThanhTien Decimal(10,0) Not null,
    Enable BIT DEFAULT 1,                         -- Cột Enable để đánh dấu chi tiết còn hoạt động hay không
    FOREIGN KEY (DonHangID) REFERENCES HoaDon(ID),
    FOREIGN KEY (SanPhamID) REFERENCES SanPham(ID),
   
);

select a.SoLuong, b.GiaBan , a.GiaGiam, a.ThanhTien
from ChiTietHoaDon a, sanpham b
where a.SanPhamID = b.ID 


-- Tạo bảng sản phẩm
CREATE TABLE SanPham (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Ten NVARCHAR(100) NOT NULL,
    LoaiID INT,                   -- Khóa ngoại đến bảng LoaiSP
    HangID INT,                   -- Khóa ngoại đến bảng HangSX (Hãng sản xuất)
    XuatXuID INT,                 -- Khóa ngoại đến bảng XuatXu (Xuất xứ)
    DoTuoiID INT,                 -- Khóa ngoại đến bảng DoTuoi
    GiaNhap DECIMAL(10, 0) NOT NULL,
    GiaBan DECIMAL(10, 0) NOT NULL,
    Ton INT NOT NULL,             -- Số lượng tồn kho
    MoTa nTEXT,
    HinhAnhURL nVARCHAR(255),
    Enable BIT DEFAULT 1,         -- Cột Enable để đánh dấu sản phẩm còn hoạt động hay không
    FOREIGN KEY (LoaiID) REFERENCES LoaiSP(ID),
    FOREIGN KEY (HangID) REFERENCES HangSX(ID),
    FOREIGN KEY (XuatXuID) REFERENCES XuatXu(ID),   -- Liên kết tới bảng Xuất Xứ
    FOREIGN KEY (DoTuoiID) REFERENCES DoTuoi(ID)
);



--Tìm sản phẩm sắp hết hàng:
SELECT ID, Ten, GiaNhap, GiaBan, Ton
FROM SanPham
WHERE Ton < 10;

--Tìm sản phẩm sắp hết hàng và sắp xếp theo giá bán:
SELECT ID, Ten, GiaNhap, GiaBan, Ton
FROM SanPham
WHERE Ton < 10
ORDER BY GiaBan;





--Tìm tất cả sản phẩm đang hoạt động:
SELECT ID, Ten, GiaNhap, GiaBan, Ton
FROM SanPham
WHERE Enable = TRUE;

--Tìm tất cả sản phẩm không còn hoạt động (đã bị xóa mềm):
SELECT ID, Ten, GiaNhap, GiaBan, Ton
FROM SanPham
WHERE Enable = FALSE;

--Cập nhật trạng thái sản phẩm để đánh dấu là không hoạt động (thay vì xóa):
UPDATE SanPham
SET Enable = FALSE
WHERE ID = 1; -- Thay ID = 1 bằng ID của sản phẩm cần đánh dấu

--Xóa sản phẩm vĩnh viễn (nếu cần):
DELETE FROM SanPham
WHERE ID = 1; -- Thay ID = 1 bằng ID của sản phẩm cần xóa



--Tạo bảng hóa đơn
CREATE TABLE HoaDon (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    KhachID INT,
    NgayLap DATETIME DEFAULT CURRENT_TIMESTAMP,
    TongTienHang DECIMAL(10, 0) NOT NULL,                -- Tổng tiền trước giảm giá và phí
    TrangThai nVARCHAR(50) ,                                -- Khóa ngoại liên kết với bảng TrangThaiHoaDon
    NguoiBanID INT,                                -- Cột NguoiBanID để liên kết với người bán hàng
    GiamGiaTien DECIMAL(10, 0) DEFAULT 0.00,       -- Giảm giá theo số tiền
    GiamGiaPhanTram DECIMAL(5, 0) DEFAULT 0.00,
    ThueVAT DECIMAL(5, 0) DEFAULT 0.00,     
	PhiKhac DECIMAL(10, 0) DEFAULT 0.00,            -- Các phí khác
    TongThanhToan DECIMAL(10, 0) DEFAULT 0.00,  
	GhiChu nvarchar(max),
    Enable BIT DEFAULT 1,                          -- Cột Enable để đánh dấu hóa đơn còn hoạt động hay không
    FOREIGN KEY (KhachID) REFERENCES KhachHang(ID),
    FOREIGN KEY (NguoiBanID) REFERENCES TaiKhoan(ID),
   
);
 


-- Thêm dữ liệu vào bảng
INSERT INTO LoaiGiamGia (TenLoaiGiamGia) VALUES ('TheoSo');
INSERT INTO LoaiGiamGia (TenLoaiGiamGia) VALUES ('TheoPhanTram');



--Tính tổng tiền sau khi áp dụng giảm giá, cộng phí ship và phí khác:
SELECT 
    ID AS HoaDonID, 
    TongTien, 
    LoaiGiamGia, 
    GiamGiaTien, 
    GiamGiaPhanTram,
    PhiShip,
    PhiKhac,
    CASE
        WHEN LoaiGiamGia = 'TheoPhanTram' THEN 
            (TongTien - (TongTien * GiamGiaPhanTram / 100))
        ELSE 
            (TongTien - GiamGiaTien)
    END AS TongTienSauGiamGia,
    CASE
        WHEN LoaiGiamGia = 'TheoPhanTram' THEN 
            (TongTien - (TongTien * GiamGiaPhanTram / 100)) + PhiShip + PhiKhac
        ELSE 
            (TongTien - GiamGiaTien) + PhiShip + PhiKhac
    END AS TongTienCuoiCung
FROM HoaDon
WHERE Enable = TRUE;



--Cập nhật giảm giá theo số tiền cho một hóa đơn cụ thể:
UPDATE HoaDon
SET LoaiGiamGia = 'TheoSo', 
    GiamGiaTien = 100.00 -- Ví dụ giảm giá 100 đơn vị tiền tệ
WHERE ID = X; -- Thay X bằng ID của hóa đơn cần cập nhật

--Cập nhật giảm giá theo phần trăm cho một hóa đơn cụ thể:
UPDATE HoaDon
SET LoaiGiamGia = 'TheoPhanTram', 
    GiamGiaPhanTram = 10.00 -- Ví dụ giảm giá 10%
WHERE ID = X; -- Thay X bằng ID của hóa đơn cần cập nhật


--Cập nhật giảm giá cho một hóa đơn cụ thể:
UPDATE HoaDon
SET GiamGia = 10.00 -- Ví dụ giảm giá 10%
WHERE ID = X; -- Thay X bằng ID của hóa đơn cần cập nhật

--Cập nhật phí ship và phí khác cho một hóa đơn cụ thể:
UPDATE HoaDon
SET PhiShip = 50.00,       -- Ví dụ phí ship 50 đơn vị tiền tệ
    PhiKhac = 20.00        -- Ví dụ phí khác 20 đơn vị tiền tệ
WHERE ID = X;              -- Thay X bằng ID của hóa đơn cần cập nhật





--Tạo bảng khách hàng
CREATE TABLE KhachHang (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    HoTen nVARCHAR(100) NOT NULL,
    DienThoai VARCHAR(20),
    DiaChi NVARCHAR(MAX),
    Enable BIT DEFAULT 1,
    
);




-- Tạo bảng loại sản phẩm
CREATE TABLE LoaiSP (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Ten nVARCHAR(100) NOT NULL UNIQUE,
    Enable BIT DEFAULT 1,
   
);



--Tạo bảng hãng sản xuất
CREATE TABLE HangSX (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Ten NVARCHAR(100) NOT NULL unique,   -- Tên của hãng sản xuất
    Enable BIT DEFAULT 1          -- Trạng thái hoạt động của hãng
);

--Tạo bảng Xuất Xứ
CREATE TABLE XuatXu (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Ten NVARCHAR(100) NOT NULL unique,
    Enable BIT DEFAULT 1           -- Trạng thái hoạt động của quốc gia xuất xứ
);



--Tạo bảng độ tuổi 
CREATE TABLE DoTuoi (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Ten nVARCHAR(100) NOT NULL unique,
    Enable BIT DEFAULT 1,
);



--Tạo bảng tài khoản
CREATE TABLE TaiKhoan (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap VARCHAR(50) UNIQUE NOT NULL,
    MatKhau VARCHAR(255) NOT NULL,
    HoTen nVARCHAR(100),
    VaiTro VARCHAR(50) NOT NULL, 
    Enable BIT DEFAULT 1
);


--Lệnh code tùy ý
select * from SanPham
select * from HoaDon
select * from TaiKhoan
select * from HoaDon
select * from TaiKhoan
select * from LoaiSP
select * from HangSX
select * from XuatXu
select * from DoTuoi
select * from KhachHang
select * from ChiTietHoaDon

drop table HoaDon
drop table SanPham
drop table ChiTietHoaDon
drop table TaiKhoan
drop table LoaiSP
drop table HangSX
drop table XuatXu
drop table DoTuoi
drop table KhachHang

delete from khachHang





INSERT INTO TaiKhoan (TenDangNhap, MatKhau, HoTen, VaiTro, Enable)
VALUES ('admin', 'a', 'Nguyen Van A', 'Admin', 1);

INSERT INTO TaiKhoan (TenDangNhap, MatKhau, HoTen, VaiTro, Enable)
VALUES ('user', 'a', 'Tran Thi B', 'NguoiDung', 1);

-- Thêm dữ liệu mẫu vào bảng ChiTietHoaDon
INSERT INTO ChiTietHoaDon (DonHangID, SanPhamID, SoLuong, GiaGiam, ThanhTien)
VALUES 
(1, 1, 2, 10000, 200000),   -- Chi tiết hóa đơn cho đơn hàng 1, sản phẩm 1
(1, 2, 1, NULL, 150000),    -- Chi tiết hóa đơn cho đơn hàng 1, sản phẩm 2, không có giảm giá
(2, 3, 5, 5000, 450000),    -- Chi tiết hóa đơn cho đơn hàng 2, sản phẩm 3
(3, 1, 3, NULL, 300000),    -- Chi tiết hóa đơn cho đơn hàng 3, sản phẩm 1, không có giảm giá
(3, 4, 10, 20000, 1000000); -- Chi tiết hóa đơn cho đơn hàng 3, sản phẩm 4


-- Chèn dữ liệu mẫu vào HoaDon
INSERT INTO HoaDon (KhachID, TongTien, TrangThai, NguoiBanID, LoaiGiamGia, GiamGiaTien, GiamGiaPhanTram, PhiShip, PhiKhac, Enable)
VALUES 
(1, 500000, N'Chờ xử lý', 1, 'So', 10000, 5.00, 30000, 10000, 1),
(2, 750000, N'Đang xử lý', 2, 'PhanTram', 5000, 10.00, 40000, 20000, 1),
(3, 600000, N'Đã giao', 1, 'So', 15000, 7.00, 25000, 5000, 1);


-- Chèn dữ liệu mẫu vào KhachHang
INSERT INTO KhachHang (HoTen, DienThoai, DiaChi, Enable)
VALUES 
('Nguyen Van A', '0912345678', 'Hanoi', 1),
('Tran Thi B', '0987654321', 'Ho Chi Minh City', 1),
('Le Thi C', '0123456789', 'Da Nang', 1);

-- Chèn dữ liệu mẫu vào SanPham
INSERT INTO SanPham (Ten, LoaiID, HangID, DoTuoiID, GiaNhap, GiaBan, Ton, MoTa, HinhAnhURL, Enable)
VALUES 
(N'Xe Ô Tô Điều Khiển', 1, 1, 3, 150000, 200000, 50, N'Xe ô tô điều khiển từ xa cho trẻ em', 'http://example.com/xeco.jpg', 1),
(N'Bộ Xếp Hình Sáng Tạo', 2, 2, 2, 80000, 120000, 100, N'Bộ xếp hình giúp phát triển tư duy', 'http://example.com/xep_hinh.jpg', 1),
(N'Búp Bê Bán Quán', 3, 3, 1, 120000, 160000, 30, N'Búp bê bán hàng cho trẻ em', 'http://example.com/bup_be.jpg', 1),
(N'Hộp Lego Đa Năng', 1, 1, 3, 200000, 250000, 20, N'Hộp Lego để lắp ráp và sáng tạo', 'http://example.com/lego.jpg', 1);

-- Chèn dữ liệu mẫu vào LoaiSP
INSERT INTO LoaiSP (Ten, Enable)
VALUES 
(N'Đồ Chơi Học Tập', 1),
(N'Đồ Chơi Giải Trí', 1),
(N'Đồ Chơi Sáng Tạo', 1);

-- Chèn dữ liệu mẫu vào HangSX
INSERT INTO HangSX (Ten, Enable)
VALUES 
(N'Hãng A', 1),
(N'Hãng B', 1),
(N'Hãng C', 1);

-- Chèn dữ liệu mẫu vào XuatXu
INSERT INTO XuatXu (Ten, Enable)
VALUES 
( N'Việt Nam', 1),
( N'Trung Quốc', 1),
(N'Nhật Bản', 1);

-- Chèn dữ liệu mẫu vào DoTuoi
INSERT INTO DoTuoi (Ten, Enable)
VALUES 
(N'Từ 1 đến 3 tuổi', 1),
(N'Từ 4 đến 6 tuổi', 1),
(N'Từ 7 đến 12 tuổi', 1);


SELECT COUNT(*) FROM sanpham WHERE Ten = N'Hộp Lego Đa Năng' AND LoaiID = '1'

SELECT * FROM dbo.HangSX WHERE ID = 2;

SELECT ID, Ten, LoaiID, GiaNhap, GiaBan, Ton, enable, MoTa, hinhanhurl  
                   FROM SanPham  
                   WHERE LoaiID IN (SELECT ID FROM LoaiSP WHERE Ten = N'Đồ Chơi Giải Trí')

select * from sanpham 
where LoaiID = 1 and XuatXuID = 1
