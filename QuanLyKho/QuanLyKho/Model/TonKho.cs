using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model
{
    public class TonKho
    {
        public int STT { get; set; }
        public VatTu Vattu { get; set;}
        public int Count { get; set; }
        public DonViDo  DonViDo { get; set; }
    }
}
