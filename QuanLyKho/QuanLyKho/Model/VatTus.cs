using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model
{
    public class VatTus
    {
        public int STT { get; set; }
        public VatTu VatTu { get; set; }
        public string  TenNhaCungCap { get; set; }
        public string TenDonViDo { get; set; }
    }

}
