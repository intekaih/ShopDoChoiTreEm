
-- Tạo bảng sản phẩm
CREATE TABLE SanPham (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Ten VARCHAR(100) NOT NULL,
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

--Tạo bảng hóa đơn
CREATE TABLE HoaDon (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    KhachID INT,
    NgayLap DATETIME DEFAULT CURRENT_TIMESTAMP,
    TongTien DECIMAL(10, 0) NOT NULL,                -- Tổng tiền trước giảm giá và phí
    TrangThai VARCHAR(50) ,                                -- Khóa ngoại liên kết với bảng TrangThaiHoaDon
    NguoiBanID INT,                                -- Cột NguoiBanID để liên kết với người bán hàng
    LoaiGiamGiaID INT,                             -- Khóa ngoại liên kết với bảng LoaiGiamGia
    GiamGiaTien DECIMAL(10, 0) DEFAULT 0.00,       -- Giảm giá theo số tiền
    GiamGiaPhanTram DECIMAL(5, 0) DEFAULT 0.00,     -- Giảm giá theo phần trăm
    PhiShip DECIMAL(10, 0) DEFAULT 0.00,            -- Phí ship
    PhiKhac DECIMAL(10, 0) DEFAULT 0.00,            -- Các phí khác
    Enable BIT DEFAULT 1,                          -- Cột Enable để đánh dấu hóa đơn còn hoạt động hay không
    FOREIGN KEY (KhachID) REFERENCES KhachHang(ID),
    FOREIGN KEY (NguoiBanID) REFERENCES TaiKhoan(ID),
    FOREIGN KEY (LoaiGiamGiaID) REFERENCES LoaiGiamGia(LoaiGiamGiaID),
   
);
--Bảng Loại giảm giá
CREATE TABLE LoaiGiamGia (
    LoaiGiamGiaID INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiGiamGia VARCHAR(50) UNIQUE NOT NULL
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


--Tạo bảng khách hàng
CREATE TABLE KhachHang (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    HoTen VARCHAR(100) NOT NULL,
    DienThoai VARCHAR(20),
    DiaChi TEXT,
    Enable BIT DEFAULT 1,
    
);


-- Tạo bảng loại sản phẩm
CREATE TABLE LoaiSP (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Ten VARCHAR(100) NOT NULL UNIQUE,
    Enable BIT DEFAULT 1,
   
);



--Tạo bảng hãng sản xuất
CREATE TABLE HangSX (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Ten VARCHAR(100) NOT NULL,
    XuatXu VARCHAR(100),
    Enable BIT DEFAULT 1,
    
);



--Tạo bảng độ tuổi 
CREATE TABLE DoTuoi (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    MoTa VARCHAR(100) NOT NULL,
    Enable BIT DEFAULT 1,
);


--Tạo bảng tài khoản
CREATE TABLE TaiKhoan (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap VARCHAR(50) UNIQUE NOT NULL,
    MatKhau VARCHAR(255) NOT NULL,
    HoTen VARCHAR(100),
    VaiTro VARCHAR(50) NOT NULL, 
    Enable BIT DEFAULT 1
);


--Lệnh code tùy ý
select * from LoaiGiamGia