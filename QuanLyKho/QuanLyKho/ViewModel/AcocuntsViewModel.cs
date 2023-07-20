using System;
using QuanLyKho.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace QuanLyKho.ViewModel
{
   public class AcocuntsViewModel:BaseViewModel
    {
        private ObservableCollection<TaiKhoan> _ListAccounts;
        public ObservableCollection<TaiKhoan> ListAccounts { get => _ListAccounts; set { _ListAccounts = value; OnPropertyChanged(); } }
        public AcocuntsViewModel()
        {
        }
    }
}
