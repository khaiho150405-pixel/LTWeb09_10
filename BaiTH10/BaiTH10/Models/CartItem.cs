using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaiTH10.Models
{
    public class CartItem
    {
        public int iMaSach { get; set; }
        public string sTenSach { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }

        public double ThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }

        Model1 data = new Model1();
        public CartItem() { }
        public CartItem(int MaSach)
        {
            tblSanPham sach = data.tblSanPhams.SingleOrDefault(s => s.MaSP == MaSach);
            if (sach != null)
            {
                iMaSach = MaSach;
                sTenSach = sach.TenSP;
                sAnhBia = sach.HinhAnh;
                dDonGia = double.Parse(sach.DonGia.ToString());
                iSoLuong = 1;
            }
        }
    }
}