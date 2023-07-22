using System;
using QuanLyKho.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace QuanLyKho.ViewModel
{
    public class InputViewModel : BaseViewModel 
    {
        private ObservableCollection<Inputs> _ListInputs;
        public ObservableCollection<Inputs> ListInputs { get => _ListInputs; set { _ListInputs = value; OnPropertyChanged(); } }

        private ObservableCollection<Supplieres> _ListSupp;
        public ObservableCollection<Supplieres> ListSupp { get => _ListSupp; set { _ListSupp = value; OnPropertyChanged(); } }

        private ObservableCollection<Accounts> _ListAcc;
        public ObservableCollection<Accounts> ListAcc { get => _ListAcc; set { _ListAcc = value; OnPropertyChanged(); } }

        //Các trường này sẽ binding qua view 

        private string _TenVatTu;
        public string TenVatTu { get => _TenVatTu; set { _TenVatTu = value; OnPropertyChanged(); } }

        private int _Count;
        public int Count { get => _Count; set { _Count = value; OnPropertyChanged(); } }

        private string _HoVaTenNhanVienNhapKho;
        public string HoVaTenNhanVienNhapKho { get => _HoVaTenNhanVienNhapKho; set { _HoVaTenNhanVienNhapKho = value; OnPropertyChanged(); } }

        private string _SDTNhanVienNhapKho;
        public string SDTNhanVienNhapKho { get => _SDTNhanVienNhapKho; set { _SDTNhanVienNhapKho = value; OnPropertyChanged(); } }


        private double _GiaNhap;
        public double GiaNhap { get => _GiaNhap; set { _GiaNhap = value; OnPropertyChanged(); } }

        private string _TenNhaCungCap;
        public string TenNhaCungCap { get => _TenNhaCungCap; set { _TenNhaCungCap = value; OnPropertyChanged(); } }

        private string _SDTNhaCungCap;
        public string SDTNhaCungCap { get => _SDTNhaCungCap; set { _SDTNhaCungCap = value; OnPropertyChanged(); } }

        
        private string _TrangThai;
        public string TrangThai { get => _TrangThai; set { _TrangThai = value; OnPropertyChanged(); } }

        private DateTime _NgayNhap;
        public DateTime NgayNhap { get => _NgayNhap; set { _NgayNhap = value; OnPropertyChanged(); } }
        //chọn item bảng nhập
        private Inputs _SelectedItemInput;
        public Inputs SelectedItemInput {
            get => _SelectedItemInput;
            set{_SelectedItemInput = value; 
                OnPropertyChanged();
                if (SelectedItemInput != null)
                {
                    HoVaTenNhanVienNhapKho = SelectedItemInput.TaiKhoan.HoVaTen;
                    SDTNhanVienNhapKho = SelectedItemInput.TaiKhoan.SDT;
                    TenVatTu = SelectedItemInput.Vattu.Ten;
                    Count = (int)SelectedItemInput.ThongTinBangNhap.Count;
                    TenNhaCungCap = SelectedItemInput.NhaCungCap.Ten;
                    SDTNhaCungCap = SelectedItemInput.NhaCungCap.SDT;
                    GiaNhap = (double)SelectedItemInput.ThongTinBangNhap.GiaNhap;

                    switch ((int)SelectedItemInput.ThongTinBangNhap.TrangThai)
                    {
                        case 0:
                            TrangThai = "Hoàn thành";
                            break;
                        case 1:
                            TrangThai = "Đang trong quá trình";
                            break;
                        default:
                            break;
                    }
                    NgayNhap = (DateTime)SelectedItemInput.BangNhap.NgayNhap;
                }
            }
        }
        //chọn item nhà cung cấp 
        private Supplieres _SelectedItemSupp;
        public Supplieres SelectedItemSupp
        {
            get => _SelectedItemSupp; set
            {
                _SelectedItemSupp = value; 
                OnPropertyChanged();
                if (SelectedItemSupp != null)
                {
                    SDTNhaCungCap = SelectedItemSupp.NhaCungCap.SDT;
                    }
                 
                }
            }

        //chọn item tài khoản
        private Accounts _SelectedItemAcc;
        public Accounts SelectedItemAcc
        {
            get => _SelectedItemAcc; set
            {
                _SelectedItemAcc = value;
                OnPropertyChanged();
                if (SelectedItemAcc != null)
                {
                    SDTNhanVienNhapKho = SelectedItemAcc.TaiKhoan.SDT;
                }

            }
        }


        public InputViewModel()
        {
            LoadInput();
            LoadComBoBoxSupp();
            LoadComBoBoxAcc();
        }
        public void LoadComBoBoxAcc()
        {
            AcocuntsViewModel accVM = new AcocuntsViewModel();
            ListAcc = accVM.LoadComboBoxAcc();
        }
        public void LoadComBoBoxSupp()
        {
           SupplierViewModel suppVM = new SupplierViewModel();
            ListSupp = suppVM.LoadComboboxSuppl();
        }
        public void LoadInput()
        {
            //Biến i sẽ là STT tăng dần
            int i = 1;
            ListInputs = new ObservableCollection<Inputs>();
            var  listInput = (
                from inp in DataProvider.Ins.DB.BangNhaps
                join acc in DataProvider.Ins.DB.TaiKhoans on inp.IdTaiKhoan equals acc.Id
                join inpinf in DataProvider.Ins.DB.ThongTinBangNhaps on inp.Id equals inpinf.IdBangNhap
                join vattu in DataProvider.Ins.DB.VatTus on inpinf.IdVatTu equals vattu.Id
                join suppl in DataProvider.Ins.DB.NhaCungCaps on vattu.IdNhaCungCap equals suppl.Id
                select new  { inp,acc,inpinf,vattu,suppl}).ToList();
            foreach(var  item in listInput )
            {
                Inputs inps = new Inputs();
                inps.STT = i;
                //Đổ bangnhap
                inps.BangNhap = item.inp;
                inps.TaiKhoan = item.acc;
                inps.ThongTinBangNhap= item.inpinf;
                inps.Vattu = item.vattu;
                inps.NhaCungCap = item.suppl;
                string trangThai="";
                switch ((int)item.inpinf.TrangThai)
                {
                    case 0:
                        trangThai = "Hoàn thành";
                        break;
                    case 1:
                        trangThai = "Đang thực hiện";
                        break;
                    default:
                        break;
                }

                inps.TrangThai = trangThai.ToString();
                ListInputs.Add(inps);
                i++;
            }


        }
    }
}
