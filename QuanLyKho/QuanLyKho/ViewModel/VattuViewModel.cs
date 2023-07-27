using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
  
        public class VattuViewModel : BaseViewModel
        {
            private ObservableCollection<VatTus> _ListVattus;
            public ObservableCollection<VatTus> ListVattus { get => _ListVattus; set { _ListVattus = value; OnPropertyChanged(); } }
        //các trường sẽ binding qua view
        private string _TenVatTu;
        public string TenVatTu { get => _TenVatTu; set { _TenVatTu = value; OnPropertyChanged(); } }

        private string _TenDonViDo;
        public string TenDonViDo { get => _TenDonViDo; set { _TenDonViDo = value; OnPropertyChanged(); } }

        private string _TenNhaCungCap;
        public string TenNhaCungCap { get => _TenNhaCungCap; set { _TenNhaCungCap = value; OnPropertyChanged(); } }

        //command cho các nút chức năng thêm xóa sửa
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UnDeleteCommand { get; set; }
        public ICommand ReloadCommand { get; set; }


        private VatTus _SelectedItem;
        public VatTus SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    TenVatTu = SelectedItem.VatTu.Ten;
                    TenDonViDo = SelectedItem.DonViDo.Ten;
                    TenNhaCungCap = SelectedItem.NhaCungCap.Ten;
                    SelectedItemSupp.NhaCungCap = SelectedItem.NhaCungCap;
                    SelectedItemUnit.DonViDo = SelectedItem.DonViDo;
                }
            }
        }
        //List combobox đơn vị đo
        private ObservableCollection<Unites> _ListUnit;
        public ObservableCollection<Unites> ListUnit { get => _ListUnit; set { _ListUnit = value; OnPropertyChanged(); } }
        //List combobox Nhà cung cấp
        private ObservableCollection<Supplieres> _ListSupp;
        public ObservableCollection<Supplieres> ListSupp { get => _ListSupp; set { _ListSupp = value; OnPropertyChanged(); } }


        private Unites _SelectedItemUnit;
        public Unites SelectedItemUnit
        {
            get => _SelectedItemUnit;
            set
            {
                _SelectedItemUnit = value;
                OnPropertyChanged();

            }
        }
        private Supplieres _SelectedItemSupp;
        public Supplieres SelectedItemSupp
        {
            get => _SelectedItemSupp;
            set
            {
                _SelectedItemSupp = value;
                OnPropertyChanged();

            }
        }

        public VattuViewModel()
            {
                LoadVattu();
            LoadComBoBoxUnit();//đổ unit vào combobox

            LoadComBoBoxSupp();//đổ cus vào combobox
            AddCommand = new RelayCommand<object>((p) => { return CanAddCommand(); }, (p) => { ExcutedAddCommand(); });
            EditCommand =new RelayCommand<object>((p)=>{ return CanEditCommand(); }, (p) => { ExcutedEditCommand(); });
            DeleteCommand = new RelayCommand<object>((p) => { return CanDelCommand(); }, (p) => { ExcutedDelCommand(); });
            UnDeleteCommand = new RelayCommand<object>((p) => { return CanDelCommand(); }, (p) => { ExcutedUnDelCommand(); });
            ReloadCommand = new RelayCommand<object>((p) => { return true; }, (p) => { Reload(); });
        }
        public void LoadComBoBoxUnit()
        {
            UnitViewModel unit =new UnitViewModel();
            ListUnit = unit.LoadComboboxUnit();
        }
        public void LoadComBoBoxSupp()
        {
SupplierViewModel sup=new SupplierViewModel();
            ListSupp= sup.LoadComboboxSuppl();
        }
        private void Reload()
        {
            LoadComBoBoxUnit();//đổ unit vào combobox
            LoadComBoBoxSupp();//đổ cus vào combobox
            SelectedItemSupp = null;
            SelectedItem = null;
            SelectedItemUnit = null;
            TenVatTu = "";
        }
        public ObservableCollection<VatTus> LoadComboboxVatTu()
        {


            ListVattus = new ObservableCollection<VatTus>();
            List<VatTu> listVatTu = DataProvider.Ins.DB.VatTus.Where(x => x.TrangThai == 1).ToList();
            //Biến i sẽ là STT tăng dần
            int i = 1;
            foreach (VatTu item in listVatTu)

            {

                VatTus vt = new VatTus();
                //Đổ số thứ tự Khách hàng
                vt.STT = i;
                //Đổ tai khoản
                vt.VatTu = item;
                ListVattus.Add(vt);
                i++;
            }
            return ListVattus;
        }
        public void LoadVattu()
            {
            ListVattus = new ObservableCollection<VatTus>();
            var ListVatTu = (from vattu in DataProvider.Ins.DB.VatTus
                             where vattu.TrangThai == 1
                                    join nhacc in DataProvider.Ins.DB.NhaCungCaps on vattu.IdNhaCungCap equals nhacc.Id
                                    where nhacc.TrangThai == 1
                                    join donvi in DataProvider.Ins.DB.DonViDoes on vattu.IdDonViDo equals donvi.Id
                                    where donvi.TrangThai == 1
                                    select new { vattu, nhacc, donvi}).ToList();
            //Biến i sẽ là STT tăng dần
            int i = 1;
            foreach (var item in ListVatTu)
            {
                List<ThongTinBangNhap> ListInputInfo = DataProvider.Ins.DB.ThongTinBangNhaps.Where(p => p.IdVatTu == item.vattu.Id).ToList();
                List<ThongTinBangXuat> ListOutputInfo = DataProvider.Ins.DB.ThongTinBangXuats.Where(p => p.IdVatTu == item.vattu.Id).ToList();
                int sumInput = 0;
                int sumOutput = 0;
                int a = ListVatTu.Count();
                if (ListInputInfo != null)
                {
                    sumInput = (int)ListInputInfo.Sum(p => p.Count);
                    //tổng số lượng của tất cả  danh sách thỏa điều kiện: IdVatTu==Id của vật tư 
                }
                if (ListOutputInfo != null)
                {
                    sumOutput = (int)ListOutputInfo.Sum(p => p.Count);
                    //tổng số lượng của tất cả Danh sách thỏa điều kiện: IdVatTu==Id của vật tư 
                }

               VatTus vtu = new VatTus();
                //Đổ số thứ tự vật tư 
                vtu.STT = i;
                //Đổ số lượng vật tư  
                vtu.Count = sumInput - sumOutput;
                //Đổ vật tư 
                vtu.VatTu = item.vattu;
                vtu.NhaCungCap = item.nhacc;
                switch (item.vattu.TrangThai)
                {
                    case 1:
                        vtu.TrangThai = "Kích hoạt";
                        break;
                    case 0:
                       vtu.TrangThai = "Đã tắt";
                        break;
                    default:
                        break;
                }
                i++;
                vtu.DonViDo = item.donvi;
                ListVattus.Add(vtu);
                DataProvider.Ins.DB.SaveChanges();

            }
        }
        private bool CanAddCommand()
        {
            if (string.IsNullOrWhiteSpace(TenVatTu) || SelectedItemUnit==null || SelectedItemSupp==null)
            {
                return false;
            }
            //nếu tên vattu có kí tự thì ....
            List<VatTu> list = DataProvider.Ins.DB.VatTus.Where(x => x.Ten == TenVatTu).ToList();
            if (list.Count() == 0)
            {
                foreach (VatTus item in ListVattus)
                {
                    if (item.VatTu.Ten != TenVatTu)
                    {
                        return true;
                    }
                }
            }
            return false;


        }
        private void  ExcutedAddCommand()
        {
            try
            {

                    VatTu vt = new VatTu();
                    vt.Ten = TenVatTu.Trim();
                    vt.IdNhaCungCap = SelectedItemSupp.NhaCungCap.Id;
                    vt.TrangThai = 1;
                    vt.IdDonViDo = SelectedItemUnit.DonViDo.Id;

                    DataProvider.Ins.DB.VatTus.Add(vt);
                    DataProvider.Ins.DB.SaveChanges();
                    LoadVattu();
                    MessageBox.Show("Thêm vật tư " + TenVatTu.Trim().ToUpper() + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiến trình thêm bị lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool CanEditCommand()
        {
            if (string.IsNullOrWhiteSpace(TenVatTu) || SelectedItemUnit == null || SelectedItemSupp == null || SelectedItem==null)
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
                VatTu vattu = DataProvider.Ins.DB.VatTus.Where(x => x.Id == SelectedItem.VatTu.Id).SingleOrDefault();
                vattu.Ten = TenVatTu.Trim();
                vattu.IdDonViDo=SelectedItemUnit.DonViDo.Id;
                vattu.IdNhaCungCap=SelectedItemSupp.NhaCungCap.Id;
                DataProvider.Ins.DB.SaveChanges();
                LoadVattu();
                MessageBox.Show("Sửa Vật tư " + TenVatTu.Trim().ToUpper() + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiến trình sửa bị lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                VatTu vattu = DataProvider.Ins.DB.VatTus.Where(x => x.Id == SelectedItem.VatTu.Id).SingleOrDefault();
                vattu.TrangThai = 0;//khong dùng
                DataProvider.Ins.DB.SaveChanges();
                LoadVattu();
                MessageBox.Show("Tắt dùng vật tư " + TenDonViDo.Trim().ToUpper() + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                var vattu = DataProvider.Ins.DB. VatTus.Where(x => x.Id == SelectedItem.VatTu.Id).SingleOrDefault();
                vattu.TrangThai = 1;// dùng
                DataProvider.Ins.DB.SaveChanges();
                LoadVattu();
                MessageBox.Show("Bật dùng đơn vị " + TenDonViDo.Trim().ToUpper() + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiến trình lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

