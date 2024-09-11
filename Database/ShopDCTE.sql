
-- Tạo bảng sản phẩm
CREATE TABLE SanPham (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Ten nVARCHAR(100) NOT NULL,
    LoaiID INT,
    HangID INT, --Hãng
    DoTuoiID INT,
    GiaNhap DECIMAL(10, 0) NOT NULL,
    GiaBan DECIMAL(10, 0) NOT NULL,
    Ton INT NOT NULL, --Số Lượng
    MoTa TEXT,
    HinhAnhURL VARCHAR(255),
    Enable BIT DEFAULT 1,  -- Cột Enable để đánh dấu sản phẩm còn hoạt động hay không
    FOREIGN KEY (LoaiID) REFERENCES LoaiSP(ID),
    FOREIGN KEY (HangID) REFERENCES HangSX(ID),
    FOREIGN KEY (DoTuoiID) REFERENCES DoTuoi(ID),
      
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

--Tạo bảng chi tiết hóa đơn
CREATE TABLE ChiTietHoaDon (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    DonHangID INT,                                  -- Khóa ngoại liên kết với bảng HoaDon
    SanPhamID INT,                                 -- Khóa ngoại liên kết với bảng SanPham
    SoLuong INT NOT NULL,                          -- Số lượng sản phẩm
    Gia DECIMAL(10, 0) NOT NULL,                   -- Giá sản phẩm tại thời điểm mua
	GiaGiam DECIMAL(10, 0) DEFAULT NULL,
    Enable BIT DEFAULT 1,                         -- Cột Enable để đánh dấu chi tiết còn hoạt động hay không
    FOREIGN KEY (DonHangID) REFERENCES HoaDon(ID),
    FOREIGN KEY (SanPhamID) REFERENCES SanPham(ID),
   
);

--Tạo bảng hóa đơn
CREATE TABLE HoaDon (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    KhachID INT,
    NgayLap DATETIME DEFAULT CURRENT_TIMESTAMP,
    TongTien DECIMAL(10, 0) NOT NULL,                -- Tổng tiền trước giảm giá và phí
    TrangThai nVARCHAR(50) ,                                -- Khóa ngoại liên kết với bảng TrangThaiHoaDon
    NguoiBanID INT,                                -- Cột NguoiBanID để liên kết với người bán hàng
    LoaiGiamGia nvarchar(50),                             -- Khóa ngoại liên kết với bảng LoaiGiamGia
    GiamGiaTien DECIMAL(10, 0) DEFAULT 0.00,       -- Giảm giá theo số tiền
    GiamGiaPhanTram DECIMAL(5, 0) DEFAULT 0.00,     -- Giảm giá theo phần trăm
    PhiShip DECIMAL(10, 0) DEFAULT 0.00,            -- Phí ship
    PhiKhac DECIMAL(10, 0) DEFAULT 0.00,            -- Các phí khác
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
    DiaChi TEXT,
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
    Ten nVARCHAR(100) NOT NULL,
    XuatXu nVARCHAR(100),
    Enable BIT DEFAULT 1,
    
);



--Tạo bảng độ tuổi 
CREATE TABLE DoTuoi (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    MoTa nVARCHAR(100) NOT NULL,
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
select * from LoaiSP
select * from HangSX
select * from DoTuoi
select * from KhachHang
select * from ChiTietHoaDon

drop table HoaDon
drop table SanPham
drop table ChiTietHoaDon
drop table TaiKhoan
drop table LoaiSP
drop table HangSX
drop table DoTuoi
drop table KhachHang







INSERT INTO TaiKhoan (TenDangNhap, MatKhau, HoTen, VaiTro, Enable)
VALUES ('admin', 'a', 'Nguyen Van A', 'Admin', 1);

INSERT INTO TaiKhoan (TenDangNhap, MatKhau, HoTen, VaiTro, Enable)
VALUES ('user', 'a', 'Tran Thi B', 'NguoiDung', 1);

INSERT INTO ChiTietHoaDon (DonHangID, SanPhamID, SoLuong, Gia, GiaGiam, Enable)
VALUES 
(1, 9, 2, 100000, 95000, 1),
(1, 5, 1, 200000, NULL, 1),
(1, 3, 3, 150000, 140000, 1),
(2, 8, 2, 200000, NULL, 1),
(2, 7, 1, 250000, 230000, 1),
(3, 6, 4, 100000, NULL, 1);

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
INSERT INTO HangSX (Ten, XuatXu, Enable)
VALUES 
(N'Hãng A', N'Việt Nam', 1),
(N'Hãng B', N'Trung Quốc', 1),
(N'Hãng C', N'Nhật Bản', 1);

-- Chèn dữ liệu mẫu vào DoTuoi
INSERT INTO DoTuoi (MoTa, Enable)
VALUES 
(N'Từ 1 đến 3 tuổi', 1),
(N'Từ 4 đến 6 tuổi', 1),
(N'Từ 7 đến 12 tuổi', 1);