using System;
using QuanLyKho.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Forms.VisualStyles;
using System.Data.Entity.Core.Objects;

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
            AddCommand = new RelayCommand<object>((p) => { return CanAddCommand(); }, (p) => { ExcutedAddCommand(); });
        }
        private void ExcutedAddCommand()
        {
            DonViDo unit = new DonViDo();
            unit.Ten= TenDonViDo;
                DataProvider.Ins.DB.DonViDoes.Add(unit);
                DataProvider.Ins.DB.SaveChanges();

            //phương án 1: bỏ unit vào vị trí cuối của ListDonViDoes vì ListDonViDoes có OnPropertyChanged()
            //kết quả: không load lại ListDonViDoes
            //int countItem=ListDonViDoes.Count();
            //ListDonViDoes[countItem - 1].STT= 100;
            //ListDonViDoes[countItem - 1].DonViDo = unit;
            
            //phương án 2: gọi lại hàm LoadUnit() chắc chắc sẽ load lại ListDonViDoes vì trong hàm này có ListDonViDoes
            //Kết quả: thành công nhưng vì mất thời gian gọi nên sẽ lâu hơn với dữ liệu lớn 
            LoadUnit();

        }
        private bool CanAddCommand()
        {
            if (string.IsNullOrEmpty(TenDonViDo))//Nếu TenDonViDo rỗng(nó là 1 textbox được binding từ unitVM)
            {
                return false;
            }
            //nếu tên đơn vị đo có kí tự thì ....
            var list = DataProvider.Ins.DB.DonViDoes.Where(x => x.Ten == TenDonViDo);//Lấy ra List đơn vị đo có Tên == giá trị từ TenDonViDo 
            if ( list.Count() == 0)//Nếu List đơn vị đo chưa từng tồn tại DonViDo.Ten có giá trị TenDonViDo
            {
                return true;
            }

            return false;
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
