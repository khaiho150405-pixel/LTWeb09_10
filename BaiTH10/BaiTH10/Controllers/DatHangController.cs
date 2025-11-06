using BaiTH10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTH10.Controllers
{
    public class DatHangController : Controller
    {
        Model1 data = new Model1();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemMatHang(int maSP)
        {
            GioHang gh = (GioHang)Session["gh"];
            if (gh == null)
                gh = new GioHang();

            gh.Them(maSP);
            Session["gh"] = gh;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult XemGioHang()
        {
            GioHang gh = (GioHang)Session["gh"];
            return View(gh);
        }

        public ActionResult XoaMatHang(int maSP)
        {
            GioHang gh = (GioHang)Session["gh"];
            if (gh != null)
            {
                var item = gh.ds.FirstOrDefault(s => s.iMaSach == maSP);
                if (item != null)
                {
                    gh.ds.Remove(item);
                    Session["gh"] = gh;
                }
            }
            return RedirectToAction("XemGioHang");
        }

        [HttpPost]
        public ActionResult CapNhatGioHang(List<CartItem> ds)
        {
            GioHang gh = (GioHang)Session["gh"];
            if (gh != null)
            {
                foreach (var item in ds)
                {
                    var sp = gh.ds.FirstOrDefault(s => s.iMaSach == item.iMaSach);
                    if (sp != null)
                    {
                        sp.iSoLuong = item.iSoLuong;
                    }
                }
                Session["gh"] = gh;
            }
            return RedirectToAction("XemGioHang");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["KhachHang"] == null)
            {
                TempData["ThongBao"] = "Bạn cần đăng nhập để đặt hàng.";
                return RedirectToAction("DangNhap", "Home");
            }

            GioHang gh = Session["gh"] as GioHang;
            if (gh == null || gh.ds.Count == 0)
            {
                TempData["ThongBao"] = "Giỏ hàng của bạn đang trống.";
                return RedirectToAction("XemGioHang");
            }

            return View(gh);
        }

        [HttpPost]
        public ActionResult XacNhanDatHang(string TenKH, string SoDienThoai, string DiaChi)
        {
            if (Session["KhachHang"] == null)
            {
                TempData["ThongBao"] = "Bạn cần đăng nhập để đặt hàng.";
                return RedirectToAction("DangNhap", "Home");
            }

            GioHang gh = Session["gh"] as GioHang;
            var khach = Session["KhachHang"] as tblKhachHang;

            if (gh == null || gh.ds.Count == 0)
            {
                TempData["ThongBao"] = "Giỏ hàng của bạn đang trống.";
                return RedirectToAction("XemGioHang");
            }

            try
            {
                // 🔹 Sinh mã hóa đơn tự động (vì MaHoaDon không tự tăng)
                int nextMaHD = data.tblHoaDons.Any() ? data.tblHoaDons.Max(h => h.MaHoaDon) + 1 : 1;

                // 🔹 Tạo hóa đơn
                tblHoaDon hoaDon = new tblHoaDon
                {
                    MaHoaDon = nextMaHD,
                    NgayHoaDon = DateTime.Now,
                    MaKH = khach.MaKH
                };

                data.tblHoaDons.Add(hoaDon);
                data.SaveChanges();

                // 🔹 Thêm các chi tiết hóa đơn
                foreach (var item in gh.ds)
                {
                    tblChiTiet chiTiet = new tblChiTiet
                    {
                        MaHD = hoaDon.MaHoaDon,
                        MaSP = item.iMaSach,
                        SoLuong = item.iSoLuong
                    };
                    data.tblChiTiets.Add(chiTiet);
                }

                data.SaveChanges();

                Session["gh"] = null;

                TempData["ThongBao"] = "Đặt hàng thành công!";
                return RedirectToAction("ThongBao");
            }
            catch (Exception ex)
            {
                TempData["ThongBao"] = "Lỗi khi đặt hàng: " + ex.Message;
                return RedirectToAction("XemGioHang");
            }
        }


        // Trang hiển thị thông báo
        public ActionResult ThongBao()
        {
            ViewBag.ThongBao = TempData["ThongBao"];
            return View();
        }
    }
}