using System;
using QuanLyKho.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows;
using System.Windows.Documents;

namespace QuanLyKho.ViewModel
{
    public class OutputViewModel : BaseViewModel
    {
        private ObservableCollection<Outputs> _ListOutputs;
        public ObservableCollection<Outputs> ListOutputs { get => _ListOutputs; set { _ListOutputs = value; OnPropertyChanged(); } }

        private ObservableCollection<Customers> _ListCus;
        public ObservableCollection<Customers> ListCus { get => _ListCus; set { _ListCus = value; OnPropertyChanged(); } }

        private ObservableCollection<Supplieres> _ListSupp;
        public ObservableCollection<Supplieres> ListSupp { get => _ListSupp; set { _ListSupp = value; OnPropertyChanged(); } }

        private ObservableCollection<Accounts> _ListAcc;
        public ObservableCollection<Accounts> ListAcc { get => _ListAcc; set { _ListAcc = value; OnPropertyChanged(); } }


        private ObservableCollection<VatTus> _ListVattu;
        public ObservableCollection<VatTus> ListVattu { get => _ListVattu; set { _ListVattu = value; OnPropertyChanged(); } }
        //Các trường này sẽ binding qua view 

        private DateTime _StartDay;
        public DateTime StartDay { get => _StartDay; set { SetProperty(ref _StartDay, value); LoadFillterDay(); LoadOutput(); } }

        private DateTime _EndDay;
        public DateTime EndDay { get => _EndDay; set { SetProperty(ref _EndDay, value); LoadFillterDay(); LoadOutput(); } }


        private string _SDTKhachHang;
        public string SDTKhachHang { get => _SDTKhachHang; set { _SDTKhachHang = value; OnPropertyChanged(); } }
        

        private string _TenKhachHang;
        public string TenKhachHang { get => _TenKhachHang; set { _TenKhachHang = value; OnPropertyChanged(); } }

        private string _TenVatTu;
        public string TenVatTu { get => _TenVatTu; set { _TenVatTu = value; OnPropertyChanged(); } }
        
         private string _TenTaiKhoan;
        public string TenTaiKhoan { get => _TenTaiKhoan; set { _TenTaiKhoan = value; OnPropertyChanged(); } }

        private int _Count;
        public int Count { get => _Count; set { _Count = value; OnPropertyChanged(); } }

        private string  _TenNhaCungCap;
        public string TenNhaCungCap { get => _TenNhaCungCap; set { _TenNhaCungCap = value; OnPropertyChanged(); } }

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
                    SDTKhachHang = SelectedItemOutPut.KhachHang.SDT;
                    TenKhachHang = SelectedItemOutPut.KhachHang.Ten;
                    TenVatTu = SelectedItemOutPut.Vattu.Ten;
                    TenNhaCungCap = SelectedItemOutPut.NhaCungCap.Ten;
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
                    SelectedItemAcc.TaiKhoan = SelectedItemOutPut.TaiKhoan;
                    SelectedItemSupp.NhaCungCap = SelectedItemOutPut.NhaCungCap;
                    SelectedItemVattu.VatTu = SelectedItemOutPut.Vattu;
                } 
            }
        }
        //
        private Accounts _SelectedItemAcc;
       public Accounts SelectedItemAcc { 
            get => _SelectedItemAcc; 
            set { _SelectedItemAcc = value;
                OnPropertyChanged();


            } 
        }
        private Customers _SelectedItemCus;
        public Customers SelectedItemCus
        {
            get => _SelectedItemCus;
            set
            {
                _SelectedItemCus = value;
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
        private VatTus _SelectedItemVattu;
        public VatTus SelectedItemVattu
        {
            get => _SelectedItemVattu;
            set
            {
                _SelectedItemVattu = value;
                OnPropertyChanged();


            }
        }
        //chọn item search tài khoản
        private Accounts _SelectedItemSearchAcc;
        public Accounts SelectedItemSearchAcc
        {
            get => _SelectedItemSearchAcc;
            set
            {
                SetProperty(ref _SelectedItemSearchAcc, value); LoadOutput();


            }
        }
        //command cho các nút chức năng thêm xóa sửa
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ReLoadCommand { get; set; }
        public OutputViewModel()
        {
            StartDay = DateTime.Parse("1/1/2020");
            EndDay = DateTime.Now;
            NgayXuat = DateTime.Now;
            LoadOutput();
            LoadComBoBoxSupp();
            LoadComBoBoxAcc();
            LoadComBoBoxVattu();
            LoadComBoBoxCus();

            AddCommand = new RelayCommand<object>((p) => { return CanAddCommand(); }, (p) => { ExcutedAddCommand(); });
            EditCommand = new RelayCommand<object>((p) => { return CanEditCommand(); }, (p) => { ExcutedEditCommand(); });
            DeleteCommand = new RelayCommand<object>((p) => { return CanDelCommand(); }, (p) => { ExcutedDelCommand(); });
            ReLoadCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                LoadOutput();
                LoadComBoBoxSupp();
                LoadComBoBoxAcc();
                LoadComBoBoxVattu();
                LoadComBoBoxCus();
                SelectedItemAcc = null;
                SelectedItemCus = null;
                SelectedItemSupp= null;
                SelectedItemVattu= null;
                SDTKhachHang = "";
                TenKhachHang = "";
                Count = 0;
                GiaXuat =0;
                TrangThai = "";
                SDTNhanVienXuatKho = "";
                NgayXuat = DateTime.Now;
                StartDay = DateTime.Parse("1/1/2020");
                EndDay = DateTime.Now;
            });
        }
        public void LoadComBoBoxCus()
        {
            CustomerViewModel cusVM = new CustomerViewModel();
            ListCus = cusVM.LoadComboboxCus();
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
        public void LoadComBoBoxAcc() 
        {
            AcocuntsViewModel accVM=new AcocuntsViewModel();
            ListAcc = accVM.LoadComboBoxAcc();
        }
        public void LoadOutput()
        {
            if (StartDay != null || EndDay != null)
            {
                if (SelectedItemSearchAcc != null)
                {
                    LoadFillterAcc();
                    return;
                }
                LoadFillterDay();
                return;
            }

            LoadAllOutput();
        }
        private void LoadAllOutput()
        {
            //Biến i sẽ là STT tăng dần
            int i = 1;
            ListOutputs = new ObservableCollection<Outputs>();
            var listOutput = (
                from outp in DataProvider.Ins.DB.BangXuats
                join acc in DataProvider.Ins.DB.TaiKhoans on outp.IdTaiKhoan equals acc.Id
                where acc.TrangThai == 1
                join outpinf in DataProvider.Ins.DB.ThongTinBangXuats on outp.Id equals outpinf.IdBangXuat
                where outpinf.TrangThai == 0
                join vattu in DataProvider.Ins.DB.VatTus on outpinf.IdVatTu equals vattu.Id
                where vattu.TrangThai == 1
                join donvi in DataProvider.Ins.DB.DonViDoes on vattu.IdDonViDo equals donvi.Id
                where donvi.TrangThai == 1
                join suppl in DataProvider.Ins.DB.NhaCungCaps on vattu.IdNhaCungCap equals suppl.Id
                where suppl.TrangThai == 1
                join cus in DataProvider.Ins.DB.KhachHangs on outpinf.IdKhachHang equals cus.Id
                select new { outp, acc, outpinf, vattu, suppl, cus }).ToList();


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
                outps.KhachHang = item.cus;
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


        private void LoadFillterAcc()
        {
            //Biến i sẽ là STT tăng dần
            int i = 1;
            ListOutputs = new ObservableCollection<Outputs>();
            var listOutput = (
                from outp in DataProvider.Ins.DB.BangXuats
                where outp.TaiKhoan.TenTaiKhoan == SelectedItemSearchAcc.TaiKhoan.TenTaiKhoan
                join acc in DataProvider.Ins.DB.TaiKhoans on outp.IdTaiKhoan equals acc.Id
                where acc.TrangThai == 1
                join outpinf in DataProvider.Ins.DB.ThongTinBangXuats on outp.Id equals outpinf.IdBangXuat
                where outpinf.TrangThai == 0
                join vattu in DataProvider.Ins.DB.VatTus on outpinf.IdVatTu equals vattu.Id
                where vattu.TrangThai == 1
                join donvi in DataProvider.Ins.DB.DonViDoes on vattu.IdDonViDo equals donvi.Id
                where donvi.TrangThai == 1
                join suppl in DataProvider.Ins.DB.NhaCungCaps on vattu.IdNhaCungCap equals suppl.Id
                where suppl.TrangThai == 1
                join cus in DataProvider.Ins.DB.KhachHangs on outpinf.IdKhachHang equals cus.Id
                select new { outp, acc, outpinf, vattu, suppl, cus }).ToList();


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
                outps.KhachHang = item.cus;
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
        private void LoadFillterDay()
        {
            if (StartDay == null)
            {

                //Biến i sẽ là STT tăng dần
                int i = 1;
                ListOutputs = new ObservableCollection<Outputs>();
                var listOutput = (
                    from outp in DataProvider.Ins.DB.BangXuats
                    where outp.NgayXuat <= (DateTime)EndDay
                    join acc in DataProvider.Ins.DB.TaiKhoans on outp.IdTaiKhoan equals acc.Id
                    where acc.TrangThai == 1
                    join outpinf in DataProvider.Ins.DB.ThongTinBangXuats on outp.Id equals outpinf.IdBangXuat
                    where outpinf.TrangThai == 0
                    join vattu in DataProvider.Ins.DB.VatTus on outpinf.IdVatTu equals vattu.Id
                    where vattu.TrangThai == 1
                    join donvi in DataProvider.Ins.DB.DonViDoes on vattu.IdDonViDo equals donvi.Id
                    where donvi.TrangThai == 1
                    join suppl in DataProvider.Ins.DB.NhaCungCaps on vattu.IdNhaCungCap equals suppl.Id
                    where suppl.TrangThai == 1
                    join cus in DataProvider.Ins.DB.KhachHangs on outpinf.IdKhachHang equals cus.Id
                    select new { outp, acc, outpinf, vattu, suppl, cus }).ToList();


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
                    outps.KhachHang = item.cus;
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
            if (EndDay == null)
            {

                //Biến i sẽ là STT tăng dần
                int i = 1;
                ListOutputs = new ObservableCollection<Outputs>();
                var listOutput = (
                    from outp in DataProvider.Ins.DB.BangXuats
                    where outp.NgayXuat >= (DateTime)StartDay
                    join acc in DataProvider.Ins.DB.TaiKhoans on outp.IdTaiKhoan equals acc.Id
                    where acc.TrangThai == 1
                    join outpinf in DataProvider.Ins.DB.ThongTinBangXuats on outp.Id equals outpinf.IdBangXuat
                    where outpinf.TrangThai == 0
                    join vattu in DataProvider.Ins.DB.VatTus on outpinf.IdVatTu equals vattu.Id
                    where vattu.TrangThai == 1
                    join donvi in DataProvider.Ins.DB.DonViDoes on vattu.IdDonViDo equals donvi.Id
                    where donvi.TrangThai == 1
                    join suppl in DataProvider.Ins.DB.NhaCungCaps on vattu.IdNhaCungCap equals suppl.Id
                    where suppl.TrangThai == 1
                    join cus in DataProvider.Ins.DB.KhachHangs on outpinf.IdKhachHang equals cus.Id
                    select new { outp, acc, outpinf, vattu, suppl, cus }).ToList();


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
                    outps.KhachHang = item.cus;
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
            if (StartDay != null && EndDay != null)
            {
                //Biến i sẽ là STT tăng dần
                int i = 1;
                ListOutputs = new ObservableCollection<Outputs>();
                var listOutput = (
                    from outp in DataProvider.Ins.DB.BangXuats
                    where outp.NgayXuat >= (DateTime)StartDay && outp.NgayXuat <= (DateTime)EndDay
                    join acc in DataProvider.Ins.DB.TaiKhoans on outp.IdTaiKhoan equals acc.Id
                    where acc.TrangThai == 1
                    join outpinf in DataProvider.Ins.DB.ThongTinBangXuats on outp.Id equals outpinf.IdBangXuat
                    where outpinf.TrangThai == 0
                    join vattu in DataProvider.Ins.DB.VatTus on outpinf.IdVatTu equals vattu.Id
                    where vattu.TrangThai == 1
                    join donvi in DataProvider.Ins.DB.DonViDoes on vattu.IdDonViDo equals donvi.Id
                    where donvi.TrangThai == 1
                    join suppl in DataProvider.Ins.DB.NhaCungCaps on vattu.IdNhaCungCap equals suppl.Id
                    where suppl.TrangThai == 1
                    join cus in DataProvider.Ins.DB.KhachHangs on outpinf.IdKhachHang equals cus.Id
                    select new { outp, acc, outpinf, vattu, suppl, cus }).ToList();


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
                    outps.KhachHang = item.cus;
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
        private bool CanAddCommand()
        {
            if (!int.TryParse(SDTKhachHang, out int n))
            {
                return false;
            }


            if (string.IsNullOrWhiteSpace(Count.ToString())
               || string.IsNullOrWhiteSpace(HoVaTenNhanVienXuatKho)
               || string.IsNullOrWhiteSpace(GiaXuat.ToString())
               || string.IsNullOrWhiteSpace(SDTKhachHang)
               || string.IsNullOrWhiteSpace(NgayXuat.ToString())
               || SelectedItemOutPut != null
               || SelectedItemAcc == null
               || SelectedItemSupp== null
               || SelectedItemVattu== null)
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

            if (!int.TryParse(SDTKhachHang, out int n))
            {
                return false;
            }
            return true;
            if (string.IsNullOrWhiteSpace(Count.ToString())
               || string.IsNullOrWhiteSpace(HoVaTenNhanVienXuatKho)
               || string.IsNullOrWhiteSpace(GiaXuat.ToString())
               || string.IsNullOrWhiteSpace(SDTKhachHang)
               || string.IsNullOrWhiteSpace(NgayXuat.ToString())
               || SelectedItemOutPut == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool CanDelCommand()
        {
            if (SelectedItemOutPut == null)
            {
                return false;
            }
            return true;
        }
        private void ExcutedAddCommand()
        {
            try
            {
               
                //bảng xuất
                BangXuat bx = new BangXuat();
                if (NgayXuat > DateTime.Now)
                {
                    bx.NgayXuat = DateTime.Now;
                }
                else
                {
                    bx.NgayXuat = (DateTime)NgayXuat;
                }
                bx.IdTaiKhoan = SelectedItemAcc.TaiKhoan.Id;
                DataProvider.Ins.DB.BangXuats.Add(bx);
                DataProvider.Ins.DB.SaveChanges();


                //thông tin bảng xuất
                ThongTinBangXuat ttbx = new ThongTinBangXuat();
                ttbx.IdVatTu = SelectedItemVattu.VatTu.Id;
                ttbx.IdBangXuat = bx.Id;
                ttbx.Count = Count;
                KhachHang list = DataProvider.Ins.DB.KhachHangs.Where(p => p.SDT == SDTKhachHang.Trim()).SingleOrDefault();
                if (list != null)// đã tồn tại khách hàng
                {
                    ttbx.IdKhachHang = list.Id;

                }
                else
                {
                    KhachHang kh=new KhachHang();
                    kh.SDT= SDTKhachHang.Trim();
                    kh.ThongTinThem = "Chưa có thông tin";
                    kh.Ten = "";
                    kh.Email = "";
                    kh.Address = "";
                    
                    DataProvider.Ins.DB.KhachHangs.Add(kh);
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Khách hàng mới!","Thông báo",MessageBoxButton.OK, MessageBoxImage.Information);

                }
                KhachHang newlist = DataProvider.Ins.DB.KhachHangs.Where(p => p.SDT == SDTKhachHang.Trim()).SingleOrDefault();
                ttbx.IdKhachHang = newlist.Id;
                ttbx.GiaXuat = (double)GiaXuat;
                ttbx.Count = (int)Count;
                ttbx.TrangThai = 0;
                DataProvider.Ins.DB.ThongTinBangXuats.Add(ttbx);
                DataProvider.Ins.DB.SaveChanges();
                LoadOutput();

                MessageBox.Show("Thêm thẻ xuất: " +
                    SelectedItemVattu.VatTu.Ten.Trim().ToUpper() +
                    ", giá: " + NgayXuat.ToString() +
                    ", số lượng: " + Count +
                    ", do nhân viên: " +
                    SelectedItemAcc.TaiKhoan.TenTaiKhoan +
                    " xuất thành công!", "Thông báo", MessageBoxButton.OK);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiến trình thêm bị lỗi", "Thông báo", MessageBoxButton.OK);
            }

        }

        private void ExcutedEditCommand()
        {
            try
            {
                var listOutPut = 
                (
                 from outp in DataProvider.Ins.DB.BangXuats where outp.Id == SelectedItemOutPut.BangXuat.Id
                 join acc in DataProvider.Ins.DB.TaiKhoans on outp.IdTaiKhoan equals acc.Id
                 where acc.TrangThai == 1
                 join outpinf in DataProvider.Ins.DB.ThongTinBangXuats on outp.Id equals outpinf.IdBangXuat
                 where outpinf.TrangThai == 0
                 where outpinf.Id == SelectedItemOutPut.ThongTinBangXuat.Id
                 where outpinf.IdBangXuat == outp.Id
                 join vattu in DataProvider.Ins.DB.VatTus on outpinf.IdVatTu equals vattu.Id
                 where vattu.TrangThai == 1
                 join suppl in DataProvider.Ins.DB.NhaCungCaps on vattu.IdNhaCungCap equals suppl.Id
                 where suppl.TrangThai == 1
                 join cus in DataProvider.Ins.DB.KhachHangs on outpinf.IdKhachHang equals cus.Id
                 join donvi in DataProvider.Ins.DB.DonViDoes on vattu.IdDonViDo equals donvi.Id
                 where donvi.TrangThai == 1
                 select new { outp, acc, outpinf, vattu, suppl,cus }).SingleOrDefault();



                listOutPut.outpinf.GiaXuat = (double)GiaXuat;
                listOutPut.outpinf.Count = Count;
                if (NgayXuat > DateTime.Now)
                {
                    listOutPut.outp.NgayXuat = DateTime.Now;
                }
                else
                {
                    listOutPut.outp.NgayXuat = (DateTime)NgayXuat;
                }
                listOutPut.outp.IdTaiKhoan = SelectedItemAcc.TaiKhoan.Id;
                listOutPut.vattu.IdNhaCungCap = SelectedItemSupp.NhaCungCap.Id;
                listOutPut.outpinf.IdVatTu = SelectedItemVattu.VatTu.Id;
                KhachHang list = DataProvider.Ins.DB.KhachHangs.Where(p => p.SDT == SDTKhachHang.Trim()).SingleOrDefault();
                if (list !=null)// đã tồn tại khách hàng
                {
                   listOutPut.outpinf.IdKhachHang= list.Id;

                }
                else
                {
                    KhachHang kh = new KhachHang();
                    kh.SDT = SDTKhachHang.Trim();
                    kh.ThongTinThem = "Chưa có thông tin";
                    kh.Ten = "";
                    kh.Email = "";
                    kh.Address = "";
                    DataProvider.Ins.DB.KhachHangs.Add(kh);
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Khách hàng mới!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                KhachHang newlist = DataProvider.Ins.DB.KhachHangs.Where(p => p.SDT == SDTKhachHang.Trim()).SingleOrDefault();
                listOutPut.outpinf.IdKhachHang = newlist.Id;
                DataProvider.Ins.DB.SaveChanges();
                LoadOutput();
                MessageBox.Show("Sửa thông tin xuất vật tư: " + TenVatTu.Trim().ToUpper() +
                    ", số lượng: " + Count.ToString().Trim() +
                    ", với giá xuất: " + GiaXuat.ToString().Trim() +
                    " thành công!", "Thông báo", MessageBoxButton.OK);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiến trình lỗi", "Thông báo", MessageBoxButton.OK);
            }
        }

        private void ExcutedDelCommand()
        {
            MessageBoxResult result = MessageBox.Show("Xóa thông tin xuất vật tư: " +
                TenVatTu.Trim().ToUpper() + " đã xuất vào ngày: " +
                (DateTime)SelectedItemOutPut.BangXuat.NgayXuat +
                " ?", "Thông báo", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var listOutput = (
                 from outp in DataProvider.Ins.DB.BangXuats
                 where outp.Id == SelectedItemOutPut.BangXuat.Id
                 join acc in DataProvider.Ins.DB.TaiKhoans on outp.IdTaiKhoan equals acc.Id
                 where acc.TrangThai == 1
                 join outpinf in DataProvider.Ins.DB.ThongTinBangXuats on outp.Id equals outpinf.IdBangXuat
                 where outpinf.TrangThai == 0
                 where outpinf.Id == SelectedItemOutPut.ThongTinBangXuat.Id
                 where outpinf.IdBangXuat == outp.Id
                 join vattu in DataProvider.Ins.DB.VatTus on outpinf.IdVatTu equals vattu.Id
                 where vattu.TrangThai == 1
                 join suppl in DataProvider.Ins.DB.NhaCungCaps on vattu.IdNhaCungCap equals suppl.Id
                 where suppl.TrangThai == 1
                 join cus in DataProvider.Ins.DB.KhachHangs on outpinf.IdKhachHang equals cus.Id
                 join donvi in DataProvider.Ins.DB.DonViDoes on vattu.IdDonViDo equals donvi.Id
                 where donvi.TrangThai == 1
                 select new { outp, acc, outpinf, vattu, suppl, cus }).SingleOrDefault();


                    listOutput.outpinf.TrangThai = 1;//chỉ hiện trạng thái bằng 0 nên 1 sẽ  ẩn

                    DataProvider.Ins.DB.SaveChanges();
                    LoadOutput();
                    MessageBox.Show("Xóa thông tin xuất của vật tư: " + TenVatTu.Trim().ToUpper() +
                        ", số lượng: " + Count.ToString().Trim() +
                        ", với giá xuất: " + GiaXuat.ToString().Trim() +
                        " thành công!", "Thông báo", MessageBoxButton.OK);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Tiến trình lỗi", "Thông báo", MessageBoxButton.OK);
                }
            }
        }
    }
}
