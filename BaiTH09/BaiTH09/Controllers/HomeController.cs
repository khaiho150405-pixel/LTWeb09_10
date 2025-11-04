using BaiTH09.Models;
using BaiTH09.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTH09.Controllers
{
    public class HomeController : Controller
    {
        private Model1 data = new Model1();
        public ActionResult Index()
        {
            var sachList = data.Sach.ToList();
            return View(sachList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // Hiển thị danh sách sách theo chủ đề
        public ActionResult SachTheoCD(int MaCD)
        {
            var sachTheoChuDe = data.Sach
                .Include(s => s.ChuDe)
                .Include(s => s.NhaXuatBan)
                .Where(s => s.MaChuDe == MaCD)
                .OrderBy(s => s.GiaBan)
                .ToList();

            var chuDe = data.ChuDe.FirstOrDefault(cd => cd.MaChuDe == MaCD);
            ViewBag.TenChuDe = chuDe != null ? chuDe.TenChuDe : "Không xác định";

            return View(sachTheoChuDe);
        }

        // PartialView hiển thị danh sách chủ đề bên trái
        public ActionResult ChuDePartial()
        {
            var chude = data.ChuDe.ToList();
            return PartialView(chude);
        }

        // Hiển thị danh sách sách theo nhà xuất bản
        public ActionResult SachTheoNXB(int MaNXB)
        {
            var sachTheoNXB = data.Sach
                .Include(s => s.ChuDe)
                .Include(s => s.NhaXuatBan)
                .Where(s => s.MaNXB == MaNXB)
                .OrderBy(s => s.GiaBan)
                .ToList();

            var nxb = data.NhaXuatBan.FirstOrDefault(n => n.MaNXB == MaNXB);
            ViewBag.TenNXB = nxb != null ? nxb.TenNXB : "Không xác định";

            return View(sachTheoNXB);
        }

        // PartialView hiển thị danh sách nhà xuất bản bên trái
        public ActionResult NhaXuatBanPartial()
        {
            var nhaXuatBan = data.NhaXuatBan.ToList();
            return PartialView(nhaXuatBan);
        }


        public ActionResult ChiTietSach(int id)
        {
            // ===== Lấy thông tin chi tiết sách =====
            var sach = data.Sach
                .Include(s => s.ChuDe)
                .Include(s => s.NhaXuatBan)
                .FirstOrDefault(s => s.MaSach == id);

            if (sach == null)
                return HttpNotFound();

            // ===== Lấy tác giả của sách (qua bảng ThamGia) =====
            var tacGiaList = data.ThamGia
                .Where(tg => tg.MaSach == id)
                .Select(tg => tg.TacGia)
                .ToList();

            // ===== Lấy các sản phẩm cùng chủ đề =====
            var sanPhamCungChuDe = data.Sach
                .Where(s => s.MaChuDe == sach.MaChuDe && s.MaSach != id)
                .ToList();

            // ===== Lấy các sản phẩm cùng nhà xuất bản =====
            var sanPhamCungNXB = data.Sach
                .Where(s => s.MaNXB == sach.MaNXB && s.MaSach != id)
                .ToList();

            // ===== Gộp vào ViewModel =====
            var viewModel = new ChiTietSachViewModel
            {
                Sach = sach,
                TacGiaList = tacGiaList,
                SanPhamCungChuDe = sanPhamCungChuDe,
                SanPhamCungNXB = sanPhamCungNXB
            };

            return View(viewModel);
        }

        // GET: Hiển thị form Tìm kiếm nâng cao
        public ActionResult TimKiemNangCao()
        {
            // 1. Chuẩn bị dữ liệu cho Dropdown Chủ đề
            // Gửi danh sách Chủ đề sang View bằng ViewBag hoặc ViewModel
            ViewBag.MaChuDe = new SelectList(data.ChuDe.OrderBy(cd => cd.TenChuDe), "MaChuDe", "TenChuDe");

            // 2. Định nghĩa các khoảng giá (tĩnh hoặc có thể tùy chỉnh)
            // Đây là cách đơn giản để gửi danh sách Checkbox sang View
            ViewBag.KhoangGia = new List<SelectListItem>
        {
            new SelectListItem { Value = "0-10000", Text = "0-10.000 VNĐ" },
            new SelectListItem { Value = "11000-20000", Text = "11.000-20.000 VNĐ" },
            new SelectListItem { Value = "21000-40000", Text = "21.000-40.000 VNĐ" },
            new SelectListItem { Value = "40001-MAX", Text = "Lớn hơn 40.000 VNĐ" }
        };

            // Khởi tạo một danh sách rỗng để hiển thị lần đầu
            return View(new List<Sach>());
        }

        // POST: Xử lý tìm kiếm và hiển thị kết quả
        [HttpPost]
        public ActionResult TimKiemNangCao(string keyword, int? MaChuDe, List<string> PriceRange)
        {
            // 1. Bắt đầu với tất cả sách
            IQueryable<Sach> ketQuaTimKiem = data.Sach.Include(s => s.ChuDe).Include(s => s.NhaXuatBan);

            // 2. Lọc theo Tên sản phẩm (keyword)
            if (!string.IsNullOrEmpty(keyword))
            {
                ketQuaTimKiem = ketQuaTimKiem.Where(s => s.TenSach.Contains(keyword));
            }

            // 3. Lọc theo Chủ đề
            if (MaChuDe.HasValue && MaChuDe.Value > 0)
            {
                ketQuaTimKiem = ketQuaTimKiem.Where(s => s.MaChuDe == MaChuDe.Value);
            }

            // 4. Lọc theo Khoảng giá (Sử dụng logic OR)
            if (PriceRange != null && PriceRange.Any())
            {
                // Khởi tạo một điều kiện WHERE luôn SAI (hoặc không áp dụng)
                // Sau đó, chúng ta sẽ xây dựng điều kiện OR cho từng khoảng giá
                System.Linq.Expressions.Expression<Func<Sach, bool>> pricePredicate = s => false;

                foreach (var range in PriceRange)
                {
                    var parts = range.Split('-');
                    decimal min = decimal.Parse(parts[0]);
                    decimal max = parts[1] == "MAX" ? decimal.MaxValue : decimal.Parse(parts[1]);

                    // Tạo điều kiện cho khoảng giá hiện tại
                    System.Linq.Expressions.Expression<Func<Sach, bool>> currentRangePredicate =
                        s => s.GiaBan >= min && s.GiaBan <= max;

                    // Kết hợp với điều kiện trước đó bằng logic OR
                    // Cần sử dụng thư viện như System.Linq.Dynamic.Core hoặc LinqKit
                    // Để đơn giản, ta sẽ dùng điều kiện OR trực tiếp (chỉ hoạt động tốt trong 1 lần)

                    // CÁCH KHẮC PHỤC ĐƠN GIẢN NHẤT VÀ HIỆU QUẢ NHẤT:
                    // Lưu các điều kiện lọc giá vào một danh sách và áp dụng sau.
                    // NHƯNG, CÁCH DƯỚI ĐÂY LÀ KHUYẾN NGHỊ:

                    // THAY VÌ XỬ LÝ PHỨC TẠP, TA SẼ SỬ DỤNG MỘT BIẾN TẠM THỜI (ketQuaGiaTam)
                }

                // KHẮC PHỤC LỖI BẰNG CÁCH CHUYỂN LOGIC UNION SANG BIẾN TẠM THỜI
                IQueryable<Sach> ketQuaGiaTam = Enumerable.Empty<Sach>().AsQueryable();

                foreach (var range in PriceRange)
                {
                    var parts = range.Split('-');
                    decimal min = decimal.Parse(parts[0]);
                    decimal max = parts[1] == "MAX" ? decimal.MaxValue : decimal.Parse(parts[1]);

                    // Lấy danh sách sách thỏa mãn khoảng giá và các điều kiện trước đó
                    var currentKetQua = ketQuaTimKiem.Where(s => s.GiaBan >= min && s.GiaBan <= max);

                    if (!ketQuaGiaTam.Any())
                    {
                        // Lần đầu tiên: gán trực tiếp
                        ketQuaGiaTam = currentKetQua;
                    }
                    else
                    {
                        // Lần tiếp theo: sử dụng Union
                        ketQuaGiaTam = ketQuaGiaTam.Union(currentKetQua);
                    }
                }

                // Áp dụng lọc giá vào kết quả tìm kiếm chính
                ketQuaTimKiem = ketQuaGiaTam;
            }

            // Sắp xếp kết quả cuối cùng (có thể sắp xếp theo Giá, Ngày cập nhật...)
            var danhSachSach = ketQuaTimKiem.OrderByDescending(s => s.NgayCapNhat).ToList();

            // Gửi lại dữ liệu cho form (Dropdownlist và Checkbox) để giữ lại trạng thái người dùng đã chọn
            ViewBag.MaChuDe = new SelectList(data.ChuDe.OrderBy(cd => cd.TenChuDe), "MaChuDe", "TenChuDe", MaChuDe);

            ViewBag.KhoangGia = new List<SelectListItem>
        {
            new SelectListItem { Value = "0-10000", Text = "0-10.000 VNĐ", Selected = PriceRange != null && PriceRange.Contains("0-10000") },
            new SelectListItem { Value = "11000-20000", Text = "11.000-20.000 VNĐ", Selected = PriceRange != null && PriceRange.Contains("11000-20000") },
            new SelectListItem { Value = "21000-40000", Text = "21.000-40.000 VNĐ", Selected = PriceRange != null && PriceRange.Contains("21000-40000") },
            new SelectListItem { Value = "40001-MAX", Text = "Lớn hơn 40.000 VNĐ", Selected = PriceRange != null && PriceRange.Contains("40001-MAX") }
        };


            return View(danhSachSach);
        }

        // GET: Hiển thị form Đăng ký thành viên
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        // POST: Xử lý Đăng ký thành viên
        [HttpPost]
        [ValidateAntiForgeryToken] // Nên có để bảo mật
        public ActionResult DangKy(KhachHang model, string MatKhauNhapLai)
        {
            // Kiểm tra xem đã thỏa mãn các ràng buộc [Required] trong Model chưa
            if (ModelState.IsValid)
            {
                // 1. Kiểm tra Mật khẩu nhập lại
                if (model.MatKhau != MatKhauNhapLai)
                {
                    ModelState.AddModelError("MatKhauNhapLai", "Mật khẩu nhập lại không khớp.");
                    return View(model);
                }

                // 2. Kiểm tra Tên đăng nhập (Tài khoản) đã tồn tại chưa
                if (data.KhachHang.Any(kh => kh.TaiKhoan == model.TaiKhoan))
                {
                    ModelState.AddModelError("TaiKhoan", "Tên đăng nhập này đã có người sử dụng.");
                    return View(model);
                }

                // 3. Xử lý lưu vào Database
                try
                {
                    // *LƯU Ý QUAN TRỌNG: NÊN HASH MẬT KHẨU TRƯỚC KHI LƯU*
                    // Ở đây ta lưu plain text theo yêu cầu cơ bản

                    // Gán các giá trị mặc định nếu cần
                    // model.NgaySinh = ... (nếu bạn có trường này trong form)

                    data.KhachHang.Add(model);
                    data.SaveChanges();

                    // Đăng ký thành công, chuyển hướng về trang chủ hoặc trang Đăng nhập
                    TempData["Message"] = "Đăng ký thành viên thành công! Mời bạn đăng nhập.";
                    return RedirectToAction("DangNhap");
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi DB hoặc lỗi khác
                    ModelState.AddModelError("", "Lỗi trong quá trình đăng ký: " + ex.Message);
                    return View(model);
                }
            }

            // Nếu ModelState không hợp lệ, trả về View với lỗi
            return View(model);
        }

        // ===============================================
        //               B. CHỨC NĂNG ĐĂNG NHẬP
        // ===============================================

        // GET: Hiển thị form Đăng nhập
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        // POST: Xử lý Đăng nhập
        [HttpPost]
        public ActionResult DangNhap(string TaiKhoan, string MatKhau)
        {
            // Tạo đối tượng Model tạm thời để lưu lại dữ liệu người dùng nhập
            var model = new KhachHang { TaiKhoan = TaiKhoan };

            if (string.IsNullOrEmpty(TaiKhoan) || string.IsNullOrEmpty(MatKhau))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu.";
                return View(model);
            }

            // 2. Tìm kiếm Khách hàng trong DB
            var khachHang = data.KhachHang.SingleOrDefault(
                kh => kh.TaiKhoan == TaiKhoan && kh.MatKhau == MatKhau
            );

            if (khachHang != null)
            {
                // ===============================================
                // === BỔ SUNG: LƯU THÔNG TIN KHÁCH HÀNG VÀO SESSION ===
                // ===============================================
                Session["TaiKhoan"] = khachHang;
                Session["HoTen"] = khachHang.HoTen;
                Session["MaKH"] = khachHang.MaKH;

                // Đăng nhập thành công, chuyển hướng về trang chủ
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không chính xác.";
                return View(model);
            }
        }

        // GET: Xử lý Đăng xuất
        public ActionResult DangXuat()
        {
            Session.Clear(); // Xóa tất cả Session
            return RedirectToAction("Index", "Home"); // Chuyển hướng về trang chủ
        }
    }
}