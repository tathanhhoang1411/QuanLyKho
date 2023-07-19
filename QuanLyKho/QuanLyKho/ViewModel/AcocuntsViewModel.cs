using System;
using QuanLyKho.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.ViewModel
{
   public class AcocuntsViewModel:BaseViewModel
    {
        private List<TaiKhoan> _ListAccounts;
        public List<TaiKhoan> ListAccounts { get => _ListAccounts; set { _ListAccounts = value; OnPropertyChanged(); } }
        public AcocuntsViewModel()
        {
           ListAccounts=DataProvider.Ins.DB.TaiKhoans.ToList();
        }
    }
}
