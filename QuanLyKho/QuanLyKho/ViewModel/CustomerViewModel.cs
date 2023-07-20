using System;
using QuanLyKho.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace QuanLyKho.ViewModel
{
    public class CustomerViewModel : BaseViewModel
    {
        private ObservableCollection<KhachHang> _ListCustomer;
        public ObservableCollection<KhachHang > ListCustomer { get => _ListCustomer; set { _ListCustomer = value; OnPropertyChanged(); } }
        public CustomerViewModel()
        {
    
        }
    }
}
