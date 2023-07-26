using System;
using QuanLyKho.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Windows.Input;
using System.Windows.Forms;

namespace QuanLyKho.ViewModel
{
    public class CustomerViewModel : BaseViewModel
    {
        private ObservableCollection<Customers> _ListCustomers;
        public ObservableCollection<Customers> ListCustomers { get => _ListCustomers; set { _ListCustomers = value; OnPropertyChanged(); } }

        private string  _TenKhachHang;
        public string TenKhachHang { get => _TenKhachHang; set { _TenKhachHang = value; OnPropertyChanged(); } }

        private string _SDT;
        public string  SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }

        private string _ThongTinThem;
        public string ThongTinThem { get => _ThongTinThem; set { _ThongTinThem = value; OnPropertyChanged(); } }

        private Customers _SeletedItem;
        public Customers SelectedItem { get => _SeletedItem; set { _SeletedItem = value; OnPropertyChanged(); 
                if (SelectedItem !=null) 
                {
                    TenKhachHang = SelectedItem.KhachHang.Ten;
                    SDT = SelectedItem.KhachHang.SDT;
                    Email = SelectedItem.KhachHang.Email;
                    ThongTinThem = SelectedItem.KhachHang.ThongTinThem;
                    DiaChi = SelectedItem.KhachHang.Address;
                } 
            } 
        }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public CustomerViewModel()
        {
            LoadCustomer();
            AddCommand = new RelayCommand<object>((p) => { return CanAddCommand(); }, (p) => { ExcutedAddCommand(); });
            EditCommand = new RelayCommand<object>((p) => { return CanEditCommand(); }, (p) => { ExcutedEditCommand(); });
        }
        public ObservableCollection<Customers> LoadComboboxCus()
        {

            ListCustomers = new ObservableCollection<Customers>();
            List<KhachHang> list = DataProvider.Ins.DB.KhachHangs.ToList();
            //Biến i sẽ là STT tăng dần
            int i = 1;
            foreach (KhachHang item in list)

            {

                Customers cus = new Customers();
                //Đổ số thứ tự Khách hàng
                cus.STT = i;
                //Đổ tai khoản
                cus.KhachHang = item;
                ListCustomers.Add(cus);
                i++;
            }
            return ListCustomers;
        }
        private bool CanAddCommand()
        {
            if(string.IsNullOrWhiteSpace(TenKhachHang) 
                || string.IsNullOrWhiteSpace(SDT)
                ||string.IsNullOrWhiteSpace(Email)
                || string.IsNullOrWhiteSpace(DiaChi)
                || SelectedItem != null)
            {
                return false;
            }


            List<KhachHang> list = DataProvider.Ins.DB.KhachHangs.Where(x => x.SDT == SDT).ToList();
            if (list.Count() == 0)
            {
                foreach (Customers item in ListCustomers)
                {
                    if (item.KhachHang.SDT != SDT)
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

                KhachHang kh = new KhachHang();
                kh.Ten = TenKhachHang.Trim();
                kh.SDT = SDT.Trim() ;
                kh.Email = Email.Trim();
                kh.Address=DiaChi.Trim();
                if (ThongTinThem == "")
                {
                    kh.ThongTinThem = "";
                }
                else
                {
                    kh.ThongTinThem =ThongTinThem.Trim();
                }
                DataProvider.Ins.DB.KhachHangs.Add(kh);
                DataProvider.Ins.DB.SaveChanges();
                LoadCustomer();
                MessageBox.Show("Thêm khách mua hàng" + TenKhachHang.Trim().ToUpper() + " "+SDT.Trim()+" thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiến trình thêm bị lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private bool CanEditCommand()
        {
            if (SelectedItem==null 
                || string.IsNullOrWhiteSpace(TenKhachHang) 
                || string.IsNullOrWhiteSpace(SDT) 
                ||string.IsNullOrWhiteSpace(Email)
                || string.IsNullOrWhiteSpace(DiaChi))
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
                var khachhang = DataProvider.Ins.DB.KhachHangs.Where(x => x.Id == SelectedItem.KhachHang.Id).SingleOrDefault();
                khachhang.Ten = TenKhachHang.Trim();
                khachhang.SDT = SDT.Trim();
                khachhang.Email = Email.Trim();
                khachhang.ThongTinThem = ThongTinThem.Trim();
                DataProvider.Ins.DB.SaveChanges();
                LoadCustomer();
                MessageBox.Show("Sửa Thông tin " + TenKhachHang.Trim().ToUpper() + " "+SDT.Trim()+ " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiến trình lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public string Find(string sdt)
        {
            try
            {
                string ten;
                if (sdt == null)
                {
                    ten = "";
                }
                else
                {
                    var kh = DataProvider.Ins.DB.KhachHangs.Where(p => p.SDT == sdt).ToList();
                    ten = kh[0].Ten;
                    
                }
                return ten;
            }
            catch(Exception err)
            {

                MessageBox.Show("Lỗi");
                string ten = "";
                return ten;
            }
        }
        public void LoadCustomer()
        {
            ListCustomers = new ObservableCollection<Customers>();
            List<KhachHang> listKhachHang = DataProvider.Ins.DB.KhachHangs.ToList();
            //Biến i sẽ là STT tăng dần
            int i = 1;
            foreach (KhachHang item in listKhachHang)

            {

                Customers cus = new Customers();
                //Đổ số thứ tự Khách hàng
                cus.STT = i;
                //Đổ 
                cus.KhachHang = item;
                ListCustomers.Add(cus);
                i++;
            }
        }
    }
}
