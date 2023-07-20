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
        private ObservableCollection<Customers> _ListCustomers;
        public ObservableCollection<Customers> ListCustomers { get => _ListCustomers; set { _ListCustomers = value; OnPropertyChanged(); } }
        public CustomerViewModel()
        {
            LoadCustomer();
        }
       public void LoadCustomer()
        {
            ListCustomers = new ObservableCollection<Customers>();
            List<KhachHang> listKhachHang = DataProvider.Ins.DB.KhachHangs.ToList();
            //Biến i sẽ là STT tăng dần
            int i = 1;
            foreach (KhachHang item in listKhachHang)

            {

                Customers cus = new Customers();
                //Đổ số thứ tự Khách hàng
                cus.STT = i;
                //Đổ tai khoản
                cus.KhachHang = item;
                ListCustomers.Add(cus);
                i++;
            }
        }
    }
}
