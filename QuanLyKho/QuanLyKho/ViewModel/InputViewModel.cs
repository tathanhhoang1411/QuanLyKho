using System;
using QuanLyKho.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.ViewModel
{
    public class InputViewModel : BaseViewModel
    {
        private List<BangNhap> _ListInputs;
        public List<BangNhap> ListInputs { get => _ListInputs; set { _ListInputs = value; OnPropertyChanged(); } }
        public InputViewModel()
        {
            ListInputs= (from inp in DataProvider.Ins.DB.BangNhaps
                         join inpinf in DataProvider.Ins.DB.ThongTinBangNhaps on inp.Id equals inpinf.IdBangNhap
                         join acc in DataProvider.Ins.DB.TaiKhoans on inp.IdTaiKhoan equals acc.Id
                         orderby inp.NgayNhap
                         select inp
                         ).ToList(); ;
        }
    }
}
