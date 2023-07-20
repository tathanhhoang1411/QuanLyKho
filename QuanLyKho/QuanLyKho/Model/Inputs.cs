using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model
{
    public class Inputs
    {
        public int STT { get; set; }
        public BangNhap BangNhap{ get; set; }
        public VatTu Vattu { get; set; }
        public ThongTinBangNhap ThongTinBangNhap { get; set; }
        public TaiKhoan TaiKhoan { get; set; }
        public NhaCungCap NhaCungCap { get; set; }
    }
}
