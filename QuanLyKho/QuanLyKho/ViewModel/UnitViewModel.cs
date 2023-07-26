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
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Linq;

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
        public ICommand UnDeleteCommand { get; set; }


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
            EditCommand = new RelayCommand<object>((p) => { return false; }, (p) => { ExcutedEditCommand(); });
            DeleteCommand = new RelayCommand<object>((p) => { return CanDelCommand(); }, (p) => { ExcutedDelCommand(); });
            UnDeleteCommand = new RelayCommand<object>((p) => { return CanDelCommand(); }, (p) => { ExcutedUnDelCommand(); });
        }
        private void LoadUnit()
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
                unit.DonViDo= item;
                //Đổ tai khoản
                switch (item.TrangThai)
                {
                    case 1:
                        unit.TrangThai = "Kích hoạt";
                        break;
                    case 0:
                        unit.TrangThai = "Tắt";
                        break;
                    default:
                        break;
                }
                ListDonViDoes.Add(unit);
                i++;
            }
        }
        public ObservableCollection<Unites> LoadComboboxUnit()
        {

            ListDonViDoes=new ObservableCollection<Unites>();
            List<DonViDo> listDonViDo = DataProvider.Ins.DB.DonViDoes.Where(x=>x.TrangThai==1).ToList();//Combobox unit chỉ hiện những donvido có trang thai =1
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

        protected bool CanAddCommand()
        {
            if (string.IsNullOrWhiteSpace(TenDonViDo))//Nếu TenDonViDo rỗng(nó là 1 textbox được binding từ unitVM)
            {
                return false;
            }
            //nếu tên đơn vị đo có kí tự thì ....
            var list = DataProvider.Ins.DB.DonViDoes.Where(x => x.Ten == TenDonViDo);//Lấy ra List đơn vị đo có Tên == giá trị từ TenDonViDo 
            if (list.Count() == 0  )//Nếu List đơn vị đo chưa từng tồn tại DonViDo.Ten có giá trị TenDonViDo và không selectedItem
            {
                foreach(Unites item in ListDonViDoes)
                {
                    if(item.DonViDo.Ten != TenDonViDo)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void ExcutedAddCommand()
        {
            try
            {
                DonViDo unit = new DonViDo();
                unit.Ten = TenDonViDo.Trim();
                unit.TrangThai = 1;
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
               

                MessageBox.Show("Thêm đơn vị "+TenDonViDo.Trim().ToUpper() + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        protected bool CanEditCommand()
        {
            if (SelectedItem ==null && string.IsNullOrWhiteSpace(TenDonViDo))//Nếu không chọn item nào và tendonvido rỗng
            {
                return false;
            }
            else 
            { 

            return true;
            }
           
        }
        private void ExcutedEditCommand()
        {
            try
            {
                var donvido = DataProvider.Ins.DB.DonViDoes.Where(x => x.Id == SelectedItem.DonViDo.Id).SingleOrDefault();
                donvido.Ten = TenDonViDo.Trim();
                DataProvider.Ins.DB.SaveChanges();
                LoadUnit();
                MessageBox.Show("Sửa đơn vị " + TenDonViDo.Trim().ToUpper() + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiến trình lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        protected bool CanDelCommand()
        {
            if (SelectedItem == null)//Nếu không chọn item nào
            {
                return false;
            }
            else
            {

                return true;
            }

        }
        private void ExcutedDelCommand()
        {
            try
            {
                var donvido = DataProvider.Ins.DB.DonViDoes.Where(x => x.Id == SelectedItem.DonViDo.Id).SingleOrDefault();
                donvido.TrangThai = 0;//khong dùng
                DataProvider.Ins.DB.SaveChanges();
                SelectedItem.DonViDo.Ten = TenDonViDo.Trim();
                LoadUnit();
                MessageBox.Show("Tắt dùng đơn vị " + TenDonViDo.Trim().ToUpper() + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiến trình lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void ExcutedUnDelCommand()
        {
            try
            {
                var donvido = DataProvider.Ins.DB.DonViDoes.Where(x => x.Id == SelectedItem.DonViDo.Id).SingleOrDefault();
                donvido.TrangThai =1;// dùng
                DataProvider.Ins.DB.SaveChanges();
                SelectedItem.DonViDo.Ten = TenDonViDo.Trim();
                LoadUnit();
                MessageBox.Show("Bật dùng đơn vị " + TenDonViDo.Trim().ToUpper() + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiến trình lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
