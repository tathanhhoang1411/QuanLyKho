using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<TonKho> _ListTonKho;
        public ObservableCollection<TonKho> ListTonKho { get => _ListTonKho; set { _ListTonKho = value; OnPropertyChanged(); } }
        public bool Isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand UnitWindowCommand { get; set; }
        public ICommand AccountsWindowCommand { get; set; }
        public ICommand InputWindowCommand { get; set; }
        public ICommand OutputWindowCommand { get; set; }
        public ICommand VattuWindowCommand { get; set; }
        public ICommand SupplierWindowCommand { get; set; }
        public ICommand CustomerWindowCommand { get; set; }
        // mọi thứ xử lý sẽ nằm trong này 
        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Isloaded = true;
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;

                if (loginVM.IsLogin)//Đăng nhâp thành công thì
                {
                    p.Show();
                    LoadTonKho();
                }
                else
                {
                    p.Close();
                }
            }
              );
            
            UnitWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                UnitWindow wd = new UnitWindow();
                wd.ShowDialog();
            });
            AccountsWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AccountsWindow wd = new AccountsWindow();
                wd.ShowDialog();
            });
            InputWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                InputWindow wd = new InputWindow();
                wd.ShowDialog();
            });
            OutputWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OutputWindow wd = new OutputWindow();
                wd.ShowDialog();
            });
            VattuWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                VattuWindow wd = new VattuWindow();
                wd.ShowDialog();
            });
            SupplierWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SupplierWindow wd = new SupplierWindow();
                wd.ShowDialog();
            });
            CustomerWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CustomerWindow wd = new CustomerWindow();
                wd.ShowDialog();
            });
        }
        public bool kiemTraCapTaiKhoan(TaiKhoan tk)
        {
            if(tk.IdRoleTaiKhoan==1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        void LoadTonKho()
        {
            ListTonKho = new ObservableCollection<TonKho>();
            List<VatTu>  ListVatTu = DataProvider.Ins.DB.VatTus.ToList();
            //Biến i sẽ là STT tăng dần
            int i = 1;
            foreach (VatTu item in ListVatTu)
            {
                List<ThongTinBangNhap> ListInputInfo = DataProvider.Ins.DB.ThongTinBangNhaps.Where(p => p.IdVatTu == item.Id).ToList();
                List<ThongTinBangXuat> ListOutputInfo = DataProvider.Ins.DB.ThongTinBangXuats.Where(p => p.IdVatTu == item.Id).ToList();
                List<DonViDo> DonViDo=DataProvider.Ins.DB.DonViDoes.Where(p=>p.Id==item.IdDonViDo).ToList();
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

                TonKho tonkho = new TonKho();
                //Đổ số thứ tự vật tư 
                tonkho.STT = i;
                //Đổ số lượng vật tư  
                tonkho.Count = sumInput - sumOutput;
                //Đổ vật tư 
                tonkho.Vattu = item;
                tonkho.DonViDo = DonViDo[0].Ten.ToString();
                ListTonKho.Add(tonkho);

                i++;
            }
   
        }
    }
}
