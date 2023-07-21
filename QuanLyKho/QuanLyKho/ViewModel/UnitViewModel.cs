using System;
using QuanLyKho.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace QuanLyKho.ViewModel
{
    public class UnitViewModel : BaseViewModel
    {
        private ObservableCollection<Unites> _ListDonViDoes;
        public ObservableCollection<Unites> ListDonViDoes { get => _ListDonViDoes; set { _ListDonViDoes = value; OnPropertyChanged(); } }

        private string _TenDonViDo;
        public string TenDonViDo { get => _TenDonViDo; set { _TenDonViDo = value; OnPropertyChanged(); } }


        private Unites _SelectedItem;
        public Unites SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value; OnPropertyChanged();
                if (SelectedItem != null)//nếu người dùng chọn item trong listview
                {
                    TenDonViDo = SelectedItem.DonViDo.Ten;
                }
            }
        }
        public UnitViewModel()
        {

            LoadUnit();
        }
        public void LoadUnit()
        {
            ListDonViDoes = new ObservableCollection<Unites>();
            List<DonViDo> listDonViDo = DataProvider.Ins.DB.DonViDoes.ToList();
            //Biến i sẽ là STT tăng dần
            int i = 1;
            foreach (DonViDo item in listDonViDo)

            {

                Unites unit = new Unites();
                //Đổ số thứ tự Khách hàng
                unit.STT = i;
                //Đổ tai khoản
                unit.DonViDo = item;
                ListDonViDoes.Add(unit);
                i++;
            }
        }
    }
}
