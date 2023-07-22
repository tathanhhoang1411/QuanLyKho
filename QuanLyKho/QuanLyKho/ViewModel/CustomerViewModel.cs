using System;
using QuanLyKho.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace QuanLyKho.ViewModel
{
    public class CustomerViewModel : BaseViewModel
    {
        private ObservableCollection<Customers> _ListCustomers;
        public ObservableCollection<Customers> ListCustomers { get => _ListCustomers; set { _ListCustomers = value; OnPropertyChanged(); } }

        private string  _TenKhachHang;
        public string TenKhachHang { get => _TenKhachHang; set { _TenKhachHang = value; OnPropertyChanged(); } }

        private string _SDT;
        public string  SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private string _ThongTinThem;
        public string ThongTinThem { get => _ThongTinThem; set { _ThongTinThem = value; OnPropertyChanged(); } }

        private Customers _SeletedItem;
        public Customers SelectedItem { get => _SeletedItem; set { _SeletedItem = value; OnPropertyChanged(); 
                if (SelectedItem !=null) 
                {
                    TenKhachHang = SelectedItem.KhachHang.Ten;
                    SDT = SelectedItem.KhachHang.SDT;
                    Email = SelectedItem.KhachHang.Email;
                    ThongTinThem = SelectedItem.KhachHang.ThongTinThem;
                } 
            } 
        }
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
                //Đổ 
                cus.KhachHang = item;
                ListCustomers.Add(cus);
                i++;
            }
        }
    }
}
