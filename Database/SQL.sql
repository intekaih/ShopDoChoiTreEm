CREATE TABLE QuanLyLoaiHang (
    MaLoai int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    TenLoai nvarchar(255) NOT NULL unique,
    GhiChu nvarchar(max) NULL
);

CREATE TABLE QuanLyHang (
    MaHangHoa int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    TenHang nvarchar(255) NOT NULL,
    MaLoaiHang int NOT NULL,
    GiaNhap decimal(18, 0) NOT NULL,
    GiaBan decimal(18, 0) NOT NULL,
    SoLuong int NOT NULL,
    TrangThai bit NOT NULL,
    GhiChu nvarchar(255) NULL,
    DuongDanAnh nvarchar(255) NULL,
    CONSTRAINT FK_QuanLyHang_QuanLyLoaiHang FOREIGN KEY (MaLoaiHang) REFERENCES QuanLyLoaiHang(MaLoai)
);
