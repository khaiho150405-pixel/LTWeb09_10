using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaiTH10.Models
{
    public class GioHang
    {
        public List<CartItem> ds;
        public GioHang()
        {
            ds = new List<CartItem>();
        }
        public GioHang(List<CartItem> DS)
        {
            ds = DS;
        }
        public int SoMathang()
        {
            return ds.Count;
        }
        public int TongSLHang()
        {
            return ds.Sum(s => s.iSoLuong);
        }
        public double TongThanhTien()
        {
            return ds.Sum(s => s.ThanhTien);
        }
        public int Them(int iMaSach)
        {
            CartItem sp = ds.Find(s => s.iMaSach == iMaSach);
            if (sp == null)
            {
                CartItem sach = new CartItem(iMaSach);
                if (sach == null)
                    return -1;
                ds.Add(sach);
            }
            else
            {
                sp.iSoLuong++;
            }
            return 1;
        }
    }
}