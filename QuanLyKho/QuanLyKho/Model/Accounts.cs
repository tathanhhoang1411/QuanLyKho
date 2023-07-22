using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model
{
    public class Accounts
    {
        public int STT { get; set; }
        public TaiKhoan TaiKhoan { get; set; }
        public RoleTaiKhoan RoleTaiKhoan { get;set; }
    }

}
