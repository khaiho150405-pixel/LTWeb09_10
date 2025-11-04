using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaiTH09.Models.ViewModels
{
    public class ChiTietSachViewModel
    {
        public Sach Sach { get; set; }
        public List<TacGia> TacGiaList { get; set; }
        public List<Sach> SanPhamCungChuDe { get; set; }
        public List<Sach> SanPhamCungNXB { get; set; }
    }
}