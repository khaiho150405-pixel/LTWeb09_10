CREATE DATABASE QL_BanHang
GO

USE QL_BanHang
GO

CREATE TABLE tblKhachHang (
    MaKH INT PRIMARY KEY,
    TenKH NVARCHAR(255) NOT NULL,
    DiaChi VARCHAR(255),
    SoDienThoai VARCHAR(20),
    TenDangNhap VARCHAR(50) UNIQUE,
    MatKhau VARCHAR(255)
);

CREATE TABLE tblSanPham (
    MaSP INT PRIMARY KEY,
    TenSP NVARCHAR(255) NOT NULL,
    DonGia DECIMAL(10, 2) NOT NULL,
	HinhAnh varchar(100),
    MoTa NVARCHAR(MAX),
    SoLuong INT NOT NULL
);

CREATE TABLE tblHoaDon (
    MaHoaDon INT PRIMARY KEY,
    NgayHoaDon DATE NOT NULL,
    MaKH INT,
    FOREIGN KEY (MaKH) REFERENCES tblKhachHang(MaKH)
);

CREATE TABLE tblChiTiet (
    MaHD INT,
    MaSP INT,
    SoLuong INT NOT NULL,
	PRIMARY KEY(MaHD,MaSP),
    FOREIGN KEY (MaHD) REFERENCES tblHoaDon(MaHoaDon),
    FOREIGN KEY (MaSP) REFERENCES tblSanPham(MaSP)
);

INSERT INTO tblKhachHang (MaKH, TenKH, DiaChi, SoDienThoai, TenDangNhap, MatKhau)
VALUES
(1, N'Nguyễn Văn A', N'Hà Nội', '0912345678', 'nguyenvana', '123456'),
(2, N'Lê Thị B', N'Hồ Chí Minh', '0987654321', 'lethib', 'abcdef'),
(3, N'Trần Văn C', N'Đà Nẵng', '0909123123', 'tranvanc', 'pass123'),
(4, N'Phạm Thị D', N'Cần Thơ', '0933445566', 'phamthid', 'matkhau'),
(5, N'Hoàng Văn E', N'Hải Phòng', '0977888999', 'hoangvane', '123abc');

INSERT INTO tblSanPham (MaSP, TenSP, DonGia, HinhAnh, MoTa, SoLuong)
VALUES
(1, N'Dế Mèn Phiêu Lưu Ký', 85000, 'demen.jpg', N'Tác phẩm nổi tiếng của Tô Hoài viết cho thiếu nhi, kể về cuộc phiêu lưu của Dế Mèn.', 20),
(2, N'Truyện Kiều', 99000, 'truyenkieu.jpg', N'Tác phẩm kinh điển của Nguyễn Du, thể hiện tài năng ngôn ngữ và giá trị nhân văn sâu sắc.', 15),
(3, N'Tôi Thấy Hoa Vàng Trên Cỏ Xanh', 120000, 'hoavang.jpg', N'Truyện dài của Nguyễn Nhật Ánh, kể về tuổi thơ trong sáng và đầy xúc cảm.', 25),
(4, N'Đắc Nhân Tâm', 130000, 'dacnhantam.jpg', N'Sách kỹ năng sống nổi tiếng của Dale Carnegie, giúp rèn luyện giao tiếp và lãnh đạo.', 30),
(5, N'Nhà Giả Kim', 145000, 'nhagiakim.jpg', N'Tiểu thuyết của Paulo Coelho nói về hành trình đi tìm ước mơ và ý nghĩa cuộc sống.', 18),
(6, N'Bí Mật Của May Mắn', 115000, 'bimatmayman.jpg', N'Câu chuyện ngụ ngôn về việc tạo ra vận may cho chính mình, đầy cảm hứng và triết lý.', 22),
(7, N'Sapiens: Lược Sử Loài Người', 185000, 'sapiens.jpg', N'Tác phẩm nổi tiếng của Yuval Noah Harari, nói về sự hình thành và phát triển của loài người.', 12),
(8, N'7 Thói Quen Hiệu Quả', 159000, '7thoiquen.jpg', N'Sách phát triển bản thân nổi tiếng của Stephen Covey, hướng dẫn xây dựng thói quen tích cực.', 17),
(9, N'Không Gia Đình', 99000, 'khonggiadinh.jpg', N'Tiểu thuyết cảm động về cậu bé Rémi và hành trình tìm lại gia đình, của Hector Malot.', 25),
(10, N'Tắt Đèn', 87000, 'tatden.jpg', N'Tác phẩm hiện thực phê phán nổi tiếng của Ngô Tất Tố, phản ánh cuộc sống nông dân Việt Nam.', 14);

INSERT INTO tblHoaDon (MaHoaDon, NgayHoaDon, MaKH)
VALUES
(1001, '2025-10-25', 1),
(1002, '2025-10-26', 2),
(1003, '2025-10-27', 3),
(1004, '2025-10-28', 4),
(1005, '2025-10-29', 5);


INSERT INTO tblChiTiet (MaHD, MaSP, SoLuong)
VALUES
(1001, 1, 1),  -- Nguyễn Văn A mua "Dế Mèn Phiêu Lưu Ký"
(1001, 4, 1),  -- và "Đắc Nhân Tâm"
(1002, 3, 2),  -- Lê Thị B mua 2 cuốn "Tôi Thấy Hoa Vàng Trên Cỏ Xanh"
(1003, 5, 1),  -- Trần Văn C mua "Nhà Giả Kim"
(1004, 2, 1),  -- Phạm Thị D mua "Truyện Kiều"
(1004, 6, 1),  -- và "Bí Mật Của May Mắn"
(1005, 7, 1),  -- Hoàng Văn E mua "Sapiens"
(1005, 9, 1);  -- và "Không Gia Đình"

