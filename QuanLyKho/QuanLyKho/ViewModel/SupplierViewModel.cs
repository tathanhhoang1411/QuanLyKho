using System;
using QuanLyKho.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace QuanLyKho.ViewModel
{
    public class SupplierViewModel : BaseViewModel
    {
        private ObservableCollection<Supplieres> _ListSuppls;
        public ObservableCollection<Supplieres> ListSuppls { get => _ListSuppls; set { _ListSuppls = value; OnPropertyChanged(); } }
        //các trường sẽ binding qua view
        private string _TenNhaCungCap;
        public string TenNhaCungCap { get => _TenNhaCungCap; set { _TenNhaCungCap = value; OnPropertyChanged(); } }

        private string _SDT;
        public string  SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private string _ThongTinThem;
        public string  ThongTinThem { get => _ThongTinThem; set { _ThongTinThem = value; OnPropertyChanged(); } }

        private DateTime _NgayHopTac;
        public DateTime NgayHopTac { get => _NgayHopTac; set { _NgayHopTac = value; OnPropertyChanged(); } }

        private Supplieres _SelectedItem;
        public Supplieres SelectedItem { 
            get=> _SelectedItem; 
            set { _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem!=null)
                {
                    TenNhaCungCap = SelectedItem.NhaCungCap.Ten;
                    SDT = SelectedItem.NhaCungCap.SDT;
                    Email = SelectedItem.NhaCungCap.Email;
                    ThongTinThem = SelectedItem.NhaCungCap.ThongTinThem;
                    NgayHopTac = (DateTime)SelectedItem.NhaCungCap.NgayHopTac;
                }
            } }


        public SupplierViewModel()
        {
            LoadSuppl();
        }
        public ObservableCollection<Supplieres> LoadComboboxSuppl()
        {
      SupplierViewModel supplVM= new SupplierViewModel();
            var a = supplVM.ListSuppls;
            return a;
        }
        public void LoadSuppl()
        {
            ListSuppls = new ObservableCollection<Supplieres>();
            List<NhaCungCap> listNhaCungCap = DataProvider.Ins.DB.NhaCungCaps.ToList();
            //Biến i sẽ là STT tăng dần
            int i = 1;
            foreach (NhaCungCap item in listNhaCungCap)

            {

                Supplieres supp = new Supplieres();
                //Đổ số thứ tự Khách hàng
                supp.STT = i;
                //Đổ tai khoản
                supp.NhaCungCap= item;
                ListSuppls.Add(supp);
                i++;
            }
        }
    }
}
