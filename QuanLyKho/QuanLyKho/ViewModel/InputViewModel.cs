using System;
using QuanLyKho.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

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


        private ObservableCollection<VatTus> _ListVattu;
        public ObservableCollection<VatTus> ListVattu { get => _ListVattu; set { _ListVattu = value; OnPropertyChanged(); } }

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
                    Count = (int)SelectedItemInput.ThongTinBangNhap.Count;
                    TenNhaCungCap = SelectedItemInput.NhaCungCap.Ten;
                    SDTNhaCungCap = SelectedItemInput.NhaCungCap.SDT;
                    GiaNhap = (double)SelectedItemInput.ThongTinBangNhap.GiaNhap;
                    TenVatTu = SelectedItemInput.Vattu.Ten;
                    switch ((int)SelectedItemInput.ThongTinBangNhap.TrangThai)
                    {
                        case 0:
                            TrangThai = "Hoàn thành";
                            break;
                        case 2:
                            TrangThai = "Đang trong quá trình";
                            break;
                        default:
                            break;
                    }
                    NgayNhap = (DateTime)SelectedItemInput.BangNhap.NgayNhap;
                    SelectedItemAcc.TaiKhoan = SelectedItemInput.TaiKhoan;
                    SelectedItemSupp.NhaCungCap=SelectedItemInput.NhaCungCap;
                    SelectedItemVattu.VatTu = SelectedItemInput.Vattu;
                }
            }
        }
        //chọn item nhà cung cấp 
        private VatTus _SelectedItemVattu;
        public VatTus SelectedItemVattu
        {
            get => _SelectedItemVattu; set
            {
                _SelectedItemVattu = value;
                OnPropertyChanged();


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


            }
        }
        //command cho các nút chức năng thêm xóa sửa
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ReLoadCommand { get; set; }

        public InputViewModel()
        {
            NgayNhap=DateTime.Now;
            LoadInput();
            LoadComBoBoxSupp();
            LoadComBoBoxAcc();
            LoadComBoBoxVattu();
            AddCommand = new RelayCommand<object>((p) => { return CanAddCommand(); }, (p) => { ExcutedAddCommand(); });
            EditCommand = new RelayCommand<object>((p) => { return CanEditCommand(); }, (p) => { ExcutedEditCommand(); });
            DeleteCommand = new RelayCommand<object>((p) => { return false; }, (p) => { });
            ReLoadCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                LoadInput();
                LoadComBoBoxSupp();
                LoadComBoBoxAcc();
                LoadComBoBoxVattu();
            });
        }
        public void LoadComBoBoxAcc()
        {
            AcocuntsViewModel accVM = new AcocuntsViewModel();
            ListAcc = accVM.LoadComboBoxAcc();
        }
        public void LoadComBoBoxVattu()
        {
            VattuViewModel vtVM = new VattuViewModel();
            ListVattu = vtVM.LoadComboboxVatTu();
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
                join acc in DataProvider.Ins.DB.TaiKhoans on inp.IdTaiKhoan equals acc.Id where acc.TrangThai == 1
                join inpinf in DataProvider.Ins.DB.ThongTinBangNhaps on inp.Id equals inpinf.IdBangNhap where inpinf.TrangThai==0
                join vattu in DataProvider.Ins.DB.VatTus on inpinf.IdVatTu equals vattu.Id where vattu.TrangThai==1
                join donvi in DataProvider.Ins.DB.DonViDoes on vattu.IdDonViDo equals donvi.Id where donvi.TrangThai==1
                join suppl in DataProvider.Ins.DB.NhaCungCaps on vattu.IdNhaCungCap equals suppl.Id where suppl.TrangThai==1
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

        private bool CanAddCommand()
        {

            if ( string.IsNullOrWhiteSpace(Count.ToString())
               || string.IsNullOrWhiteSpace(HoVaTenNhanVienNhapKho)
               || string.IsNullOrWhiteSpace(GiaNhap.ToString())
               || string.IsNullOrWhiteSpace(TenNhaCungCap)
               || string.IsNullOrWhiteSpace(NgayNhap.ToString()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CanEditCommand()
        {

            if (string.IsNullOrWhiteSpace(Count.ToString())
               || string.IsNullOrWhiteSpace(HoVaTenNhanVienNhapKho)
               || string.IsNullOrWhiteSpace(GiaNhap.ToString())
               || string.IsNullOrWhiteSpace(TenNhaCungCap)
               || string.IsNullOrWhiteSpace(NgayNhap.ToString())
               || SelectedItemInput==null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void ExcutedAddCommand()
        {
            try
            {
                //bảng nhập
                BangNhap bn = new BangNhap();
                if (NgayNhap > DateTime.Now)
                {
                    bn.NgayNhap = DateTime.Now;
                }
                else
                {
                    bn.NgayNhap = (DateTime)NgayNhap;
                }
                bn.IdTaiKhoan = SelectedItemAcc.TaiKhoan.Id;
                DataProvider.Ins.DB.BangNhaps.Add(bn);



                //thông tin bảng nhập
                ThongTinBangNhap ttbn = new ThongTinBangNhap();
                ttbn.IdVatTu = SelectedItemVattu.VatTu.Id;
                ttbn.IdBangNhap =bn.Id;
                ttbn.Count = Count;
                ttbn.GiaNhap = GiaNhap;
                ttbn.TrangThai = 0;
                DataProvider.Ins.DB.ThongTinBangNhaps.Add(ttbn);


                DataProvider.Ins.DB.SaveChanges();
                LoadInput();

                MessageBox.Show("Thêm thẻ nhập: " + SelectedItemVattu.VatTu.Ten.Trim() +" giá: "+Count+ " " + NgayNhap.ToString() +" do nhân viên: "+SelectedItemAcc.TaiKhoan.TenTaiKhoan+"nhập"+ " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiến trình thêm bị lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void ExcutedEditCommand()
        {
            try
            {
                var listInput = (
                (
                 from inp in DataProvider.Ins.DB.BangNhaps
                 join acc in DataProvider.Ins.DB.TaiKhoans on inp.IdTaiKhoan equals acc.Id
                 where acc.TrangThai == 1 && acc.Id == SelectedItemAcc.TaiKhoan.Id
                join inpinf in DataProvider.Ins.DB.ThongTinBangNhaps on inp.Id equals inpinf.IdBangNhap
                where inpinf.TrangThai == 0 
                 join vattu in DataProvider.Ins.DB.VatTus on inpinf.IdVatTu equals vattu.Id
                where vattu.TrangThai == 1 && vattu.Id == SelectedItemVattu.VatTu.Id
                 join suppl in DataProvider.Ins.DB.NhaCungCaps on vattu.IdNhaCungCap equals suppl.Id
                where suppl.TrangThai == 1 && suppl.Id == SelectedItemSupp.NhaCungCap.Id

                 join donvi in DataProvider.Ins.DB.DonViDoes on vattu.IdDonViDo equals donvi.Id
                 where donvi.TrangThai == 1
                 select new { inp, acc, inpinf, vattu, suppl })).SingleOrDefault();


                
                listInput.inpinf.GiaNhap = (double)GiaNhap;
                listInput.inpinf.Count = Count;
                if (NgayNhap > DateTime.Now)
                {
                    listInput.inp.NgayNhap = DateTime.Now;
                }
                else
                {
                    listInput.inp.NgayNhap = (DateTime)NgayNhap;
                }
                listInput.inp.IdTaiKhoan = SelectedItemAcc.TaiKhoan.Id;

                listInput.inpinf.IdVatTu = SelectedItemVattu.VatTu.Id;


                DataProvider.Ins.DB.SaveChanges();
                LoadInput();
                MessageBox.Show("Sửa thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiến trình lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
