using BaiTH10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTH10.Controllers
{
    public class HomeController : Controller
    {
        Model1 data = new Model1();
        public ActionResult Index()
        {
            List<tblSanPham> ds = data.tblSanPhams.ToList();
            return View(ds);
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
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(tblKhachHang kh)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra trùng tài khoản
                var check = data.tblKhachHangs.FirstOrDefault(k => k.TenDangNhap == kh.TenDangNhap);
                if (check != null)
                {
                    ViewBag.ThongBao = "⚠️ Tên tài khoản đã tồn tại!";
                    return View();
                }

                // Tự sinh mã khách hàng 
                kh.MaKH = data.tblKhachHangs.Any()
                    ? data.tblKhachHangs.Max(k => k.MaKH) + 1
                    : 1;

                data.tblKhachHangs.Add(kh);
                data.SaveChanges();

                ViewBag.ThongBao = "✅ Đăng ký thành công! Hãy đăng nhập.";
                return RedirectToAction("DangNhap");
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string TaiKhoan, string MatKhau)
        {
            var kh = data.tblKhachHangs.FirstOrDefault(k => k.TenDangNhap == TaiKhoan && k.MatKhau == MatKhau);
            if (kh != null)
            {
                Session["KhachHang"] = kh;
                Session["TenKH"] = kh.TenKH;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ThongBao = "Sai tên tài khoản hoặc mật khẩu";
            return View();
        }
        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}