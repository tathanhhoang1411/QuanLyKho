using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model
{
    public class Outputs
    {
        public int STT { get; set; }
        public BangXuat BangXuat { get; set; }
        public VatTu Vattu { get; set; }
        public ThongTinBangXuat ThongTinBangXuat { get; set; }
        public TaiKhoan TaiKhoan { get; set; }
        public NhaCungCap NhaCungCap { get; set; }
        public String TrangThai { get; set; }
        public KhachHang KhachHang { get; set; }
    }
}
