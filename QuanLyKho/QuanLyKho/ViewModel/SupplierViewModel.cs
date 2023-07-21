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
        public SupplierViewModel()
        {
            LoadSuppl();
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
