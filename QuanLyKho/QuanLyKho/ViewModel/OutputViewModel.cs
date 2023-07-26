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
    public class OutputViewModel : BaseViewModel
    {
        private ObservableCollection<Outputs> _ListOutputs;
        public ObservableCollection<Outputs> ListOutputs { get => _ListOutputs; set { _ListOutputs = value; OnPropertyChanged(); } }

        private ObservableCollection<Accounts> _ListAcc;
        public ObservableCollection<Accounts> ListAcc { get => _ListAcc; set { _ListAcc = value; OnPropertyChanged(); } }
        //Các trường này sẽ binding qua view 
        private string _SDT;
        public string  SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }

        private string _TenKhachHang;
        public string TenKhachHang { get => _TenKhachHang; set { _TenKhachHang = value; OnPropertyChanged(); } }

        private string _TenVatTu;
        public string TenVatTu { get => _TenVatTu; set { _TenVatTu = value; OnPropertyChanged(); } }

        private int _Count;
        public int Count { get => _Count; set { _Count = value; OnPropertyChanged(); } }

        private string _HoVaTenNhanVienXuatKho;
        public string HoVaTenNhanVienXuatKho { get => _HoVaTenNhanVienXuatKho; set { _HoVaTenNhanVienXuatKho = value; OnPropertyChanged(); } }

        private string _SDTNhanVienXuatKho;
        public string SDTNhanVienXuatKho { get => _SDTNhanVienXuatKho; set { _SDTNhanVienXuatKho = value; OnPropertyChanged(); } }

        private double _GiaXuat;
        public double GiaXuat { get => _GiaXuat; set { _GiaXuat = value; OnPropertyChanged(); } }

        private string _TrangThai;
        public string TrangThai { get => _TrangThai; set { _TrangThai = value; OnPropertyChanged(); } }

        private DateTime _NgayXuat;
        public DateTime NgayXuat { get => _NgayXuat; set { _NgayXuat = value; OnPropertyChanged(); } }

        private Outputs _SelectedItemOutPut;
        public Outputs SelectedItemOutPut { 
            get=> _SelectedItemOutPut; 
            set { _SelectedItemOutPut = value;
                OnPropertyChanged();
                if (SelectedItemOutPut != null)
                {
                    SDT = SelectedItemOutPut.KhachHang.SDT;
                    TenKhachHang = SelectedItemOutPut.KhachHang.Ten;
                    TenVatTu = SelectedItemOutPut.Vattu.Ten;
                    Count = (int)SelectedItemOutPut.ThongTinBangXuat.Count;
                    GiaXuat =(double)SelectedItemOutPut.ThongTinBangXuat.GiaXuat;
                    HoVaTenNhanVienXuatKho = SelectedItemOutPut.TaiKhoan.HoVaTen;
                    SDTNhanVienXuatKho = SelectedItemOutPut.TaiKhoan.SDT;
                    switch ((int)SelectedItemOutPut.ThongTinBangXuat.TrangThai)
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
                    NgayXuat =(DateTime)SelectedItemOutPut.BangXuat.NgayXuat;
                } 
            }
        }
        //
        private Accounts _SelectedItemAcc;
       public Accounts SelectedItemAcc { get => _SelectedItemAcc; set { _SelectedItemAcc = value; OnPropertyChanged();
                if (SelectedItemAcc != null) 
                {
                    SDTNhanVienXuatKho = SelectedItemAcc.TaiKhoan.SDT;
                }
            } 
        }
        //command cho các nút chức năng thêm xóa sửa
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public OutputViewModel()
        {
            LoadOutput();
            LoadComBoBoxAcc();
            AddCommand = new RelayCommand<object>((p) => { return false; }, (p) => { });
            EditCommand = new RelayCommand<object>((p) => { return false; }, (p) => { });
            DeleteCommand = new RelayCommand<object>((p) => { return false; }, (p) => { });
        }
        public void LoadComBoBoxAcc() 
        {
            AcocuntsViewModel accVM=new AcocuntsViewModel();
            ListAcc = accVM.LoadComboBoxAcc();
        }
        public void LoadOutput()
        {
            //Biến i sẽ là STT tăng dần
            int i = 1;
            ListOutputs = new ObservableCollection<Outputs>();
            var listOutput = (
                from outp in DataProvider.Ins.DB.BangXuats
                join acc in DataProvider.Ins.DB.TaiKhoans on outp.IdTaiKhoan equals acc.Id
                join outpinf in DataProvider.Ins.DB.ThongTinBangXuats on outp.Id equals outpinf.IdBangXuat
                where outpinf.TrangThai==0
                join vattu in DataProvider.Ins.DB.VatTus on outpinf.IdVatTu equals vattu.Id
                join suppl in DataProvider.Ins.DB.NhaCungCaps on vattu.IdNhaCungCap equals suppl.Id
                join cus in DataProvider.Ins.DB.KhachHangs on outpinf.IdKhachHang equals cus.Id
                select new { outp, acc, outpinf, vattu, suppl,cus }).ToList();
            foreach (var item in listOutput)
            {
                Outputs outps = new Outputs();
                outps.STT = i;
                //Đổ bangnhap
                outps.BangXuat = item.outp;
                outps.TaiKhoan = item.acc;
                outps.ThongTinBangXuat = item.outpinf;
                outps.Vattu = item.vattu;
                outps.NhaCungCap = item.suppl;
                outps.KhachHang= item.cus;
                string trangThai = "";
                switch ((int)item.outpinf.TrangThai)
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

                outps.TrangThai = trangThai.ToString();
                ListOutputs.Add(outps);
                i++;
            }


        }
    }
}
