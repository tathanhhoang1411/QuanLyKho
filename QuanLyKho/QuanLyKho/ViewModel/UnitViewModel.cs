using System;
using QuanLyKho.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    public class UnitViewModel : BaseViewModel
    {
        private ObservableCollection<Unites> _ListDonViDoes;
        public ObservableCollection<Unites> ListDonViDoes { get => _ListDonViDoes; set { _ListDonViDoes = value; OnPropertyChanged(); } }

        private string _TenDonViDo;
        public string TenDonViDo { get => _TenDonViDo; set { _TenDonViDo = value; OnPropertyChanged(); } }


        //command cho các nút chức năng thêm xóa sửa
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

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
            AddCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MainWindow mainVM = new MainWindow();
                mainVM.Hide();
                CustomerWindow wd = new CustomerWindow();
                wd.ShowDialog();
            });
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
        public ObservableCollection<Unites> LoadComboboxUnit()
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
            return ListDonViDoes;
        }
    }
}
