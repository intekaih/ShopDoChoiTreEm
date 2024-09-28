
--Tạo bảng chi tiết hóa đơn
CREATE TABLE ChiTietHoaDon (
    ID INT IDENTITY(1,1) PRIMARY KEY,
	TabHoaDonID INT,
    DonHangID INT,                                  -- Khóa ngoại liên kết với bảng HoaDon
    SanPhamID INT,                                 -- Khóa ngoại liên kết với bảng SanPham
    SoLuong INT NOT NULL,                          -- Số lượng sản phẩm
	GiaGiam DECIMAL(10, 0) DEFAULT NULL,
	ThanhTien Decimal(10,0) Not null,
    Enable BIT DEFAULT 0,                         -- Cột Enable để đánh dấu chi tiết còn hoạt động hay không
    FOREIGN KEY (DonHangID) REFERENCES HoaDon(ID),
    FOREIGN KEY (SanPhamID) REFERENCES SanPham(ID),
    FOREIGN KEY (TabHoaDonID) REFERENCES TabHoaDon(ID),

);




INSERT INTO ChiTietHoaDon (TabHoaDonID, DonHangID, SanPhamID, SoLuong, GiaGiam, ThanhTien, Enable)
VALUES 
(1, 8, 1, 2, 50000, 200000, 1),
(1, 8, 2, 1, 15000, 200000, 1),
(1, 8, 3, 1, 50000, 300000, 1),
(1, 8, 1, 3, 60000, 300000, 1),
(1, 8, 2, 2, 50000, 400000, 1);

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
    FOREIGN KEY (DoTuoiID) REFERENCES DoTuoi(ID),
);

-- Thêm dữ liệu mẫu vào bảng SanPham
INSERT INTO SanPham (Ten, LoaiID, HangID, XuatXuID, DoTuoiID, GiaNhap, GiaBan, Ton, MoTa, HinhAnhURL, Enable)
VALUES 
('Búp bê Barbie', 1, 1, 1, 1, 100000, 150000, 50, 'Búp bê Barbie thời trang, nhiều kiểu dáng.', 'url_to_image_barbie', 1),
('Xe hơi điều khiển từ xa', 2, 2, 2, 2, 200000, 250000, 30, 'Xe hơi điều khiển từ xa với tốc độ cao.', 'url_to_image_car', 1),
('Xếp hình Lego', 1, 1, 1, 3, 300000, 350000, 20, 'Bộ xếp hình Lego cho trẻ em.', 'url_to_image_lego', 1),
('Gấu bông mềm mại', 3, 2, 2, 1, 400000, 450000, 15, 'Gấu bông mềm mại, dễ thương.', 'url_to_image_bear', 1),
('Đồ chơi xếp hình', 2, 3, 3, 2, 500000, 550000, 10, 'Đồ chơi xếp hình đa dạng.', 'url_to_image_puzzle', 1),
('Búp bê bếp', 1, 1, 1, 3, 600000, 650000, 25, 'Búp bê bếp với nhiều phụ kiện.', 'url_to_image_doll', 1),
('Bánh xe tập đi', 3, 2, 2, 1, 700000, 750000, 5, 'Bánh xe tập đi cho bé.', 'url_to_image_walker', 1),
('Đồ chơi âm nhạc', 2, 3, 3, 2, 800000, 850000, 12, 'Đồ chơi âm nhạc cho trẻ em.', 'url_to_image_music', 1),
('Ô tô mô hình', 1, 1, 1, 3, 900000, 950000, 18, 'Ô tô mô hình với chi tiết tinh xảo.', 'url_to_image_modelcar', 1),
('Xe đạp trẻ em', 3, 2, 2, 1, 1000000, 1050000, 8, 'Xe đạp dành cho trẻ em.', 'url_to_image_bike', 1),
('Bảng xóa nhanh', 2, 3, 3, 2, 1100000, 1150000, 14, 'Bảng xóa nhanh cho bé học tập.', 'url_to_image_board', 1),
('Hộp màu vẽ', 1, 1, 1, 3, 1200000, 1250000, 22, 'Hộp màu vẽ cho trẻ em sáng tạo.', 'url_to_image_paint', 1),
('Tranh ghép hình', 3, 2, 2, 1, 1300000, 1350000, 6, 'Tranh ghép hình thú vị.', 'url_to_image_puzzleart', 1),
('Đồ chơi mô phỏng', 2, 3, 3, 2, 1400000, 1450000, 11, 'Đồ chơi mô phỏng các hoạt động thực tế.', 'url_to_image_simulation', 1),
('Lều trẻ em', 1, 1, 1, 3, 1500000, 1550000, 16, 'Lều chơi cho trẻ em.', 'url_to_image_tent', 1),
('Đồ chơi lắp ráp', 3, 2, 2, 1, 1600000, 1650000, 9, 'Đồ chơi lắp ráp với nhiều chi tiết.', 'url_to_image_assembly', 1),
('Đồ chơi thể thao', 2, 3, 3, 2, 1700000, 1750000, 13, 'Đồ chơi thể thao cho bé.', 'url_to_image_sport', 1),
('Tranh tô màu', 1, 1, 1, 3, 1800000, 1850000, 21, 'Tranh tô màu cho trẻ em.', 'url_to_image_color', 1),
('Đồ chơi bể bơi', 3, 2, 2, 1, 1900000, 1950000, 7, 'Đồ chơi bể bơi cho bé.', 'url_to_image_pooltoy', 1),
('Bộ đồ chơi Lego', 2, 3, 3, 2, 2000000, 2050000, 10, 'Bộ đồ chơi Lego với nhiều chủ đề.', 'url_to_image_legoset', 1),
('Xe mô hình điều khiển', 1, 1, 1, 3, 2100000, 2150000, 20, 'Xe mô hình điều khiển từ xa với nhiều tính năng.', 'url_to_image_rccar', 1),
('Bộ búp bê gia đình', 3, 2, 2, 1, 2200000, 2250000, 5, 'Bộ búp bê gia đình với nhiều nhân vật.', 'url_to_image_familydoll', 1);



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
	TabHoaDonID INT,
    KhachID INT,
    NgayLap DATETIME DEFAULT CURRENT_TIMESTAMP,
    TongTienHang DECIMAL(10, 0),                -- Tổng tiền trước giảm giá và phí
    TrangThai nVARCHAR(50) DEFAULT N'Chờ xử lý' ,                                -- Khóa ngoại liên kết với bảng TrangThaiHoaDon
    NguoiBanID INT,                                -- Cột NguoiBanID để liên kết với người bán hàng
    GiamGiaTien DECIMAL(10, 0) DEFAULT 0.00,       -- Giảm giá theo số tiền
    GiamGiaPhanTram DECIMAL(5, 0) DEFAULT 0.00,
    ThueVAT DECIMAL(5, 0) DEFAULT 0.00,     
	PhiKhac DECIMAL(10, 0) DEFAULT 0.00,            -- Các phí khác
    TongThanhToan DECIMAL(10, 0) DEFAULT 0.00,  
	KhachTra DECIMAL(10, 0) DEFAULT 0.00,
	ThanhToanID INT,
	GhiChu nvarchar(max),
	IsSaved BIT DEFAULT 0,
    Enable BIT DEFAULT 0,                          -- Cột Enable để đánh dấu hóa đơn còn hoạt động hay không
    FOREIGN KEY (KhachID) REFERENCES KhachHang(ID),
    FOREIGN KEY (NguoiBanID) REFERENCES TaiKhoan(ID),
	FOREIGN KEY (ThanhToanID) REFERENCES PhuongThucThanhToan(ID),
    FOREIGN KEY (TabHoaDonID) REFERENCES TabHoaDon(ID)
);






 -- Thêm dữ liệu mẫu vào bảng HoaDon
INSERT INTO HoaDon (TabHoaDonID, KhachID, TongTienHang, TrangThai, NguoiBanID, GiamGiaTien, GiamGiaPhanTram, ThueVAT, PhiKhac, TongThanhToan, GhiChu, Enable)
VALUES (1, 1, 500000, 'Đã thanh toán', 1, 50000, 10, 5, 20000, 450000, 'Khách hàng thanh toán đầy đủ.', 1);

INSERT INTO HoaDon (TabHoaDonID, KhachID, TongTienHang, TrangThai, NguoiBanID, GiamGiaTien, GiamGiaPhanTram, ThueVAT, PhiKhac, TongThanhToan, GhiChu, Enable)
VALUES (2, 2, 300000, 'Chưa thanh toán', 2, 30000, 5, 0, 10000, 270000, 'Khách hàng chưa thanh toán.', 1);

INSERT INTO HoaDon (TabHoaDonID, KhachID, TongTienHang, TrangThai, NguoiBanID, GiamGiaTien, GiamGiaPhanTram, ThueVAT, PhiKhac, TongThanhToan, GhiChu, Enable)
VALUES (3, 1, 1000000, 'Đã thanh toán', 2, 100000, 10, 10, 50000, 945000, 'Hóa đơn đã thanh toán toàn bộ.', 1);





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

-- Tạo bảng PhuongThucThanhToan
CREATE TABLE PhuongThucThanhToan (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Ten NVARCHAR(100) NOT NULL
);

-- Tạo bảng TabHoaDon
CREATE TABLE TabHoaDon (
    ID INT IDENTITY(1,1) PRIMARY KEY,  -- Cột ID là khóa chính và tự động tăng
    Ten NVARCHAR(100) NOT NULL          -- Cột Ten không cho phép giá trị NULL
);

INSERT INTO PhuongThucThanhToan (Ten) VALUES 
(N'Tiền mặt'),
(N'Thẻ'),
(N'Chuyển khoản');


--Tạo bảng khách hàng
CREATE TABLE KhachHang (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    HoTen nVARCHAR(100) NOT NULL,
    DienThoai VARCHAR(20),
    DiaChi NVARCHAR(MAX),
    Enable BIT DEFAULT 1,
    
);

-- Thêm dữ liệu mẫu vào bảng KhachHang
INSERT INTO KhachHang (HoTen, DienThoai, DiaChi, Enable)
VALUES ('Khách lẻ', '', '', 1);

INSERT INTO KhachHang (HoTen, DienThoai, DiaChi, Enable)
VALUES ('Trần Thị B', '0987654321', 'Số 2, Đường DEF, Quận 2, TP.HCM', 1);



-- Tạo bảng loại sản phẩm
CREATE TABLE LoaiSP (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Ten nVARCHAR(100) NOT NULL UNIQUE,
    Enable BIT DEFAULT 1,
   
);

-- Thêm dữ liệu mẫu vào bảng LoaiSP
INSERT INTO LoaiSP (Ten, Enable)
VALUES ('Đồ chơi giáo dục', 1);

INSERT INTO LoaiSP (Ten, Enable)
VALUES ('Đồ chơi lắp ráp', 1);

INSERT INTO LoaiSP (Ten, Enable)
VALUES ('Đồ chơi mô hình', 1);

INSERT INTO LoaiSP (Ten, Enable)
VALUES ('Đồ chơi ngoài trời', 1);


--Tạo bảng hãng sản xuất
CREATE TABLE HangSX (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Ten NVARCHAR(100) NOT NULL unique,   -- Tên của hãng sản xuất
    Enable BIT DEFAULT 1          -- Trạng thái hoạt động của hãng
);

-- Thêm dữ liệu mẫu vào bảng HangSX
INSERT INTO HangSX (Ten, Enable)
VALUES ('Samsung', 1);

INSERT INTO HangSX (Ten, Enable)
VALUES ('Sony', 1);

INSERT INTO HangSX (Ten, Enable)
VALUES ('Apple', 1);


--Tạo bảng Xuất Xứ
CREATE TABLE XuatXu (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Ten NVARCHAR(100) NOT NULL unique,
    Enable BIT DEFAULT 1           -- Trạng thái hoạt động của quốc gia xuất xứ
);

-- Thêm dữ liệu mẫu vào bảng XuatXu
INSERT INTO XuatXu (Ten, Enable)
VALUES ('Việt Nam', 1);

INSERT INTO XuatXu (Ten, Enable)
VALUES ('Trung Quốc', 1);

INSERT INTO XuatXu (Ten, Enable)
VALUES ('Hàn Quốc', 1);

INSERT INTO XuatXu (Ten, Enable)
VALUES ('Nhật Bản', 1);

--Tạo bảng độ tuổi 
CREATE TABLE DoTuoi (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Ten nVARCHAR(100) NOT NULL unique,
    Enable BIT DEFAULT 1,
);

-- Thêm dữ liệu mẫu vào bảng DoTuoi
INSERT INTO DoTuoi (Ten, Enable)
VALUES ('Dưới 1 tuổi', 1);
INSERT INTO DoTuoi (Ten, Enable)
VALUES ('1-3 tuổi', 1);
INSERT INTO DoTuoi (Ten, Enable)
VALUES ('4-6 tuổi', 1);
INSERT INTO DoTuoi (Ten, Enable)
VALUES ('Trên 6 tuổi', 1);


--Tạo bảng tài khoản
CREATE TABLE TaiKhoan (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap VARCHAR(50) UNIQUE NOT NULL,
    MatKhau VARCHAR(255) NOT NULL,
    HoTen nVARCHAR(100),
    VaiTro VARCHAR(50) NOT NULL, 
    Enable BIT DEFAULT 1
);

-- Thêm tài khoản mẫu 1
INSERT INTO TaiKhoan (TenDangNhap, MatKhau, HoTen, VaiTro, Enable)
VALUES ('a', 'a', 'Nguyễn Văn A', 'admin', 1);

-- Thêm tài khoản mẫu 2
INSERT INTO TaiKhoan (TenDangNhap, MatKhau, HoTen, VaiTro, Enable)
VALUES ('user', 'a', 'Trần Thị B', 'user', 1);


--Lệnh code tùy ý

select * from TaiKhoan
select * from LoaiSP
select * from HangSX
select * from DoTuoi
select * from KhachHang
select * from HoaDon
select * from ChiTietHoaDon
select * from SanPham
select * from XuatXu


select *
from ChiTietHoaDon
where 
	 DonHangID = 12

update ChiTietHoaDon
set Enable = 1
where 
	 DonHangID = 12


SELECT  SanPhamID , SoLuong
FROM ChiTietHoaDon
WHERE TabHoaDonID = 1
  AND DonHangID = 55
group by SanPhamID, SoLuong
  
UPDATE SanPham 
SET Ton = Ton - ct.SoLuong 
FROM SanPham sp JOIN ChiTietHoaDon ct ON sp.ID = ct.SanPhamID 
WHERE ct.TabHoaDonID = 1 AND ct.DonHangID = 55


select * from tabhoadon
select * from PhuongThucThanhToan



drop table HoaDon
drop table SanPham
drop table ChiTietHoaDon
drop table TaiKhoan
drop table LoaiSP
drop table HangSX
drop table XuatXu
drop table DoTuoi
drop table KhachHang

-- Top 10 SP bán chạy
SELECT TOP 10 
    sp.Ten, 
    SUM(ct.SoLuong) AS TongSoLuong
FROM 
    ChiTietHoaDon ct
JOIN 
    HoaDon hd ON ct.DonHangID = hd.ID
JOIN 
    SanPham sp ON ct.SanPhamID = sp.ID
WHERE 
    hd.NgayLap >= DATEADD(DAY, -7, GETDATE())
GROUP BY 
    sp.Ten
ORDER BY 
    TongSoLuong DESC;