USE [QLSACH]
GO

-- Thêm dữ liệu vào bảng KhachHang
INSERT INTO [dbo].[KhachHang] ([HoTen], [NgaySinh], [GioiTinh], [DienThoai], [TaiKhoan], [MatKhau], [Email], [DiaChi]) VALUES
(N'Nguyễn Văn A', '1990-05-15', N'Nam', '0901112222', 'vana', 'pass123', 'vana@example.com', N'123 Đường ABC, Quận 1'),
(N'Trần Thị B', '1995-12-01', N'Nữ', '0903334444', 'thib', 'pass456', 'thib@example.com', N'456 Phố XYZ, Quận 3'),
(N'Lê Văn C', '1988-08-20', N'Nam', '0905556666', 'vanc', 'pass789', 'vanc@example.com', N'789 Đại lộ QWE, Quận 5');

-- Thêm dữ liệu vào bảng ChuDe
INSERT INTO [dbo].[ChuDe] ([MaChuDe], [TenChuDe]) VALUES
(1, N'Khoa học Viễn tưởng'),
(2, N'Văn học'),
(3, N'Kinh tế'),
(4, N'Sách thiếu nhi'),
(5, N'Lịch sử');

-- Thêm dữ liệu vào bảng NhaXuatBan
INSERT INTO [dbo].[NhaXuatBan] ([MaNXB], [TenNXB], [DiaChi], [DienThoai]) VALUES
(101, N'Kim Đồng', N'TP. Hồ Chí Minh', '028111222'),
(102, N'Trẻ', N'TP. Hà Nội', '024333444'),
(103, N'Lao Động', N'TP. Hồ Chí Minh', '028555666'),
(104, N'Thế Giới', N'TP. Hồ Chí Minh', '028777888'),
(105, N'Văn Học', N'TP. Hà Nội', '024999000');

-- Thêm dữ liệu vào bảng TacGia
INSERT INTO [dbo].[TacGia] ([MaTacGia], [TenTacGia], [DiaChi], [TieuSu], [DienThoai]) VALUES
(501, N'Nguyễn Nhật Ánh', N'TP. Hồ Chí Minh', N'Nhà văn chuyên viết truyện thiếu nhi', '0912345678'),
(502, N'Yuval Noah Harari', N'Israel', N'Nhà sử học, tác giả sách nổi tiếng', '0987654321'),
(503, N'Nguyễn Quang Sáng', N'TP. Cần Thơ', N'Nhà văn Việt Nam', '0909090909'),
(504, N'J.K. Rowling', N'Anh', N'Tác giả Harry Potter', '0111223344'),
(505, N'Adam Khoo', N'Singapore', N'Chuyên gia phát triển bản thân', '0222334455'),
(506, N'Harper Lee', N'Mỹ', N'Nhà văn nổi tiếng', '0333445566'),
(507, N'Dale Carnegie', N'Mỹ', N'Tác giả sách kỹ năng sống', '0444556677'),
(508, N'Ngô Tất Tố', N'Hà Nội', N'Nhà văn hiện thực phê phán', '0555667788'),
(509, N'Eckhart Tolle', N'Đức', N'Chuyên gia tâm linh', '0666778899'),
(510, N'Tony Buzan', N'Anh', N'Cha đẻ bản đồ tư duy', '0777889900');

-- Thêm dữ liệu vào bảng Sach
INSERT INTO [dbo].[Sach] ([MaSach], [TenSach], [GiaBan], [MoTa], [NgayCapNhat], [AnhBia], [SoLuongTon], [MaChuDe], [MaNXB], [Moi]) VALUES
(1001, N'Mắt Biếc', 5000, N'Tiểu thuyết của Nguyễn Nhật Ánh', '2024-01-10', '/Content/Images/matbiec.jpg', 50, 2, 101, N'Có'),
(1002, N'Sapiens: Lược sử loài người', 7000, N'Sách khoa học và lịch sử', '2023-11-20', '/Content/Images/sapiens.jpg', 30, 1, 102, N'Không'),
(1003, N'Đất rừng phương Nam', 10000, N'Truyện dài của Nguyễn Quang Sáng', '2024-02-15', '/Content/Images/datrung.jpg', 60, 4, 103, N'Có'),
(1004, N'Từ tốt đến vĩ đại', 18000, N'Sách về quản trị kinh doanh', '2024-03-01', '/Content/Images/tutot.jpg', 45, 3, 102, N'Có'),
(1005, N'Harry Potter và Hòn đá Phù thủy', 15000, N'Phần 1 bộ truyện nổi tiếng', '2024-04-01', '/Content/Images/hinhanh.png', 70, 1, 104, N'Có'),
(1006, N'Tôi Tài Giỏi, Bạn Cũng Thế!', 14500, N'Sách phát triển bản thân cho học sinh', '2023-10-15', '/Content/Images/hinhanh.png', 55, 3, 101, N'Có'),
(1007, N'Giết con chim nhại', 11000, N'Tiểu thuyết cổ điển Mỹ', '2024-05-10', '/Content/Images/hinhanh.png', 40, 2, 105, N'Không'),
(1008, N'Đắc nhân tâm', 22000, N'Sách kỹ năng giao tiếp', '2023-09-01', '/Content/Images/hinhanh.png', 90, 3, 102, N'Có'),
(1009, N'Tắt đèn', 35000, N'Tác phẩm hiện thực phê phán Việt Nam', '2024-06-20', '/Content/Images/hinhanh.png', 50, 2, 103, N'Có'),
(1010, N'Sức mạnh của hiện tại', 30000, N'Sách tâm linh, thiền định', '2023-12-05', '/Content/Images/hinhanh.png', 35, 1, 104, N'Không'),
(1011, N'Bản đồ tư duy', 13000, N'Hướng dẫn sử dụng Mind Map', '2024-07-11', '/Content/Images/hinhanh.png', 65, 3, 101, N'Có'),
(1012, N'Lịch sử vạn vật', 210000, N'Sách khoa học phổ thông', '2024-08-01', '/Content/Images/hinhanh.png', 25, 1, 105, N'Không'),
(1013, N'Hoàng tử bé', 75000, N'Truyện thiếu nhi kinh điển', '2024-09-15', '/Content/Images/hinhanh.png', 80, 4, 102, N'Có'),
(1014, N'Bảy thói quen hiệu quả', 200000, N'Sách quản lý và phát triển cá nhân', '2023-10-01', '/Content/Images/hinhanh.png', 40, 3, 103, N'Không'),
(1015, N'Truyện Kiều', 100000, N'Tác phẩm văn học cổ điển Việt Nam', '2024-10-25', '/Content/Images/hinhanh.png', 50, 2, 105, N'Có');

-- Thêm dữ liệu vào bảng ThamGia
INSERT INTO [dbo].[ThamGia] ([MaSach], [MaTacGia], [VaiTro], [ViTri]) VALUES
(1001, 501, N'Tác giả', N'Chính'),
(1002, 502, N'Tác giả', N'Chính'),
(1003, 503, N'Tác giả', N'Chính'),
(1004, 505, N'Tác giả', N'Chính'),
(1005, 504, N'Tác giả', N'Chính'),
(1006, 505, N'Chủ biên', N'Chính'),
(1007, 506, N'Tác giả', N'Chính'),
(1008, 507, N'Tác giả', N'Chính'),
(1009, 508, N'Tác giả', N'Chính'),
(1010, 509, N'Tác giả', N'Chính'),
(1011, 510, N'Tác giả', N'Chính'),
(1012, 502, N'Biên tập', N'Chính'),
(1013, 503, N'Chủ biên', N'Chính'),
(1014, 507, N'Tác giả', N'Chính'),
(1015, 508, N'Tác giả', N'Chính');

-- Thêm dữ liệu vào bảng DonHang
INSERT INTO [dbo].[DonHang] ([MaDonHang], [NgayGiao], [NgayDat], [DaThanhToan], [TinhTrangGiaoHang], [MaKH]) VALUES
(1, '2024-11-05', '2024-11-01', N'Rồi', N'Đã giao', 1),
(2, '2024-11-07', '2024-11-02', N'Chưa', N'Đang giao', 2),
(3, '2024-11-10', '2024-11-03', N'Rồi', N'Chưa giao', 1);

-- Thêm dữ liệu vào bảng ChiTietDonHang
INSERT INTO [dbo].[ChiTietDonHang] ([MaDonHang], [MaSach], [SoLuong], [DonGia]) VALUES
(1, 1001, 2, 120000), -- ĐH 1 của KH 1 mua 2 cuốn Mắt Biếc
(1, 1003, 1, 95000),  -- ĐH 1 của KH 1 mua 1 cuốn Đất rừng phương Nam
(2, 1002, 1, 250000), -- ĐH 2 của KH 2 mua 1 cuốn Sapiens
(3, 1004, 3, 180000); -- ĐH 3 của KH 1 mua 3 cuốn Từ tốt đến vĩ đại

-- 1. Xem toàn bộ dữ liệu trong bảng KhachHang
SELECT * FROM [dbo].[KhachHang];

---

-- 2. Xem toàn bộ dữ liệu trong bảng ChuDe
SELECT * FROM [dbo].[ChuDe];

---

-- 3. Xem toàn bộ dữ liệu trong bảng NhaXuatBan
SELECT * FROM [dbo].[NhaXuatBan];

---

-- 4. Xem toàn bộ dữ liệu trong bảng TacGia
SELECT * FROM [dbo].[TacGia];

---

-- 5. Xem toàn bộ dữ liệu trong bảng Sach
SELECT * FROM [dbo].[Sach];

---

-- 6. Xem toàn bộ dữ liệu trong bảng ThamGia (Quan hệ Sách - Tác giả)
SELECT * FROM [dbo].[ThamGia];

---

-- 7. Xem toàn bộ dữ liệu trong bảng DonHang
SELECT * FROM [dbo].[DonHang];

---

-- 8. Xem toàn bộ dữ liệu trong bảng ChiTietDonHang
SELECT * FROM [dbo].[ChiTietDonHang];