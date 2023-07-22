using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                   // SelectedItemUnit ;
                }
            }
        }
        //List combobox đơn vị đo
        private ObservableCollection<Unites> _ListUnit;
        public ObservableCollection<Unites> ListUnit { get => _ListUnit; set { _ListUnit = value; OnPropertyChanged(); } }

        private Unites _SelectedItemUnit;
        public Unites SelectedItemUnit
        {
            get => _SelectedItemUnit;
            set
            {
                _SelectedItemUnit = value;
                OnPropertyChanged();
                if (SelectedItemUnit  != null)
                {

                }
            }
        }
        //List combobox Nhà cung cấp
        private ObservableCollection<Supplieres> _ListSupp;
        public ObservableCollection<Supplieres> ListSupp { get => _ListSupp; set { _ListSupp = value; OnPropertyChanged(); } }

        private Customers _SelectedItemSupp;
        public Customers SelectedItemSupp
        {
            get => _SelectedItemSupp;
            set
            {
                _SelectedItemSupp = value;
                OnPropertyChanged();
                if (_SelectedItemSupp != null)
                {

                }
            }
        }

        public VattuViewModel()
            {
                LoadVattu();
            LoadComBoBoxUnit();//đổ unit vào combobox
            LoadComBoBoxCus();//đổ cus vào combobox

            }
        public void LoadComBoBoxUnit()
        {
            UnitViewModel unit =new UnitViewModel();
            ListUnit = unit.LoadComboboxUnit();
        }
        public void LoadComBoBoxCus()
        {
SupplierViewModel sup=new SupplierViewModel();
            ListSupp= sup.LoadComboboxSuppl();
        }
        public void LoadVattu()
            {
            ListVattus = new ObservableCollection<VatTus>();
            List<VatTu> ListVatTu = DataProvider.Ins.DB.VatTus.ToList();
            //Biến i sẽ là STT tăng dần
            int i = 1;
            foreach (VatTu item in ListVatTu)
            {
                List<ThongTinBangNhap> ListInputInfo = DataProvider.Ins.DB.ThongTinBangNhaps.Where(p => p.IdVatTu == item.Id).ToList();
                List<ThongTinBangXuat> ListOutputInfo = DataProvider.Ins.DB.ThongTinBangXuats.Where(p => p.IdVatTu == item.Id).ToList();
                List<DonViDo> DonViDo = DataProvider.Ins.DB.DonViDoes.Where(p => p.Id == item.IdDonViDo).ToList();
                int sumInput = 0;
                int sumOutput = 0;
                int a = ListVatTu.Count();
                if (ListInputInfo != null)
                {
                    sumInput = (int)ListInputInfo.Sum(p => p.Count);
                    //tổng số lượng của tất cả Danh sách inputinfo thỏa điều kiện: IdVatTu==Id của vật tư 
                }
                if (ListOutputInfo != null)
                {
                    sumOutput = (int)ListOutputInfo.Sum(p => p.Count);
                    //tổng số lượng của tất cả Danh sách outputinfo thỏa điều kiện: IdVatTu==Id của vật tư 
                }

               VatTus vtu = new VatTus();
                //Đổ số thứ tự vật tư 
                vtu.STT = i;
                //Đổ số lượng vật tư  
                vtu.Count = sumInput - sumOutput;
                //Đổ vật tư 
                vtu.VatTu = item;
                vtu.NhaCungCap = item.NhaCungCap;
                vtu.DonViDo = DonViDo[0];
                ListVattus.Add(vtu);

                i++;
            }
        }
        }
    }

