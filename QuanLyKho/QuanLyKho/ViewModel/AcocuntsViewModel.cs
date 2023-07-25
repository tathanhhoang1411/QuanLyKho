using GalaSoft.MvvmLight.CommandWpf;
using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace QuanLyKho.ViewModel
{
    public class AcocuntsViewModel : BaseViewModel
    {
        private ObservableCollection<Accounts> _ListAccounts;
        public ObservableCollection<Accounts> ListAccounts { get => _ListAccounts; set { _ListAccounts = value; OnPropertyChanged(); } }
        private ObservableCollection<RoleAccounts> _ListRoleAccounts;
        public ObservableCollection<RoleAccounts> ListRoleAccounts { get => _ListRoleAccounts; set { _ListRoleAccounts = value; OnPropertyChanged(); } }

        private string _HoVaTen;
        public string HoVaTen { get => _HoVaTen; set { _HoVaTen = value; OnPropertyChanged(); } }

        private string _TaiKhoan;
        public string TaiKhoan { get => _TaiKhoan; set { _TaiKhoan = value; OnPropertyChanged(); } }

        private string _SDT;
        public string SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private string _TenRoleTaiKhoan;
        public string TenRoleTaiKhoan { get => _TenRoleTaiKhoan; set { _TenRoleTaiKhoan = value; OnPropertyChanged(); } }

        private DateTime _NgayTao;
        public DateTime NgayTao { get => _NgayTao; set { _NgayTao = value; OnPropertyChanged(); } }

        private Accounts _SelectedItem;
        public Accounts SelectedItem {
            get => _SelectedItem;
            set { _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    HoVaTen = SelectedItem.TaiKhoan.HoVaTen;
                    TaiKhoan = SelectedItem.TaiKhoan.TenTaiKhoan;
                    SDT = SelectedItem.TaiKhoan.SDT;
                    Email = SelectedItem.TaiKhoan.Email;
                    TenRoleTaiKhoan = SelectedItem.RoleTaiKhoan.Ten;
                    NgayTao = (DateTime)SelectedItem.TaiKhoan.NgayTao;

                }
            }
        }

        private string _TenRoleTaiKhoan2;
        public string TenRoleTaiKhoan2 { get => _TenRoleTaiKhoan2; set { _TenRoleTaiKhoan2 = value; OnPropertyChanged(); } }
        private RoleAccounts _SelectedItemRole;
        public RoleAccounts SelectedItemRole
        {
            get => _SelectedItemRole;
            set
            {
                _SelectedItemRole = value;
                OnPropertyChanged();
                if (SelectedItemRole != null)
                {
                    TenRoleTaiKhoan2 = SelectedItemRole.RoleTK.Ten;

                }
            }
        }
        public ICommand AddCommand { get;set ; }
        public ICommand EditCommand { get;set ; }
        public ICommand DeleteCommand { get;set ; }
        public ICommand UnDeleteCommand { get;set ; }
        public ICommand OpenFolderCommand { get;set ; }
        public ICommand SaveFileCommand { get;set ; }


        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }

        public AcocuntsViewModel()
        {

            LoadAccount();//đổ danh sách tài khoản vào 
            LoadComboBoxRole();//đổ danh sách role
            AddCommand = new RelayCommand<object>((p) => { return true; }, (p) => {  });
            EditCommand = new RelayCommand<object>((p) => { return true; }, (p) => { });
            DeleteCommand = new RelayCommand<object>((p) => { return true; }, (p) => {  });
            UnDeleteCommand = new RelayCommand<object>((p) => { return true; }, (p) => { });
            OpenFolderCommand = new RelayCommand(OpenImage);
            SaveFileCommand = new RelayCommand(SaveImage);
        }

        private void OpenImage()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png";
            dlg.InitialDirectory = @"D:\WORK\";
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                ImagePath = dlg.FileName;
                
            }
        }
        private void SaveImage()
        {
            // Mở hộp thoại chọn nơi lưu và đặt tên tệp
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png";
            dialog.FileName = ImagePath; // Tên tệp mặc định

            if (dialog.ShowDialog() == true)
            {
                dialog.InitialDirectory = @"D:\WORK\WPF\WPF\PhanMemQuanLyKho\QuanLyKho\QuanLyKho\QuanLyKho\Images\Accounts";
                // Tạo một BitmapSource từ tệp ảnh được chọn
                BitmapImage bitmap = new BitmapImage(new Uri(ImagePath));
                // Tạo một BitmapEncoder để mã hóa ảnh
                BitmapEncoder encoder;
                switch (Path.GetExtension(dialog.FileName).ToLower())
                {
                    case ".jpg":
                    case ".jpeg":
                        encoder = new JpegBitmapEncoder();
                        break;
                    case ".png":
                    default:
                        encoder = new PngBitmapEncoder();
                        break;
                }
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                // Lưu ảnh vào đường dẫn được chỉ định bởi người dùng
               

                
            }
        }
            public ObservableCollection<Accounts> LoadComboBoxAcc()
        {
            var a = this.ListAccounts;
            return a;
        }
        public void LoadComboBoxRole()
        {
            RoleAccountViewModel role = new RoleAccountViewModel();
            ListRoleAccounts = role.LoadRole();
        }
        public void LoadAccount()
        {
            ListAccounts = new ObservableCollection<Accounts>();
            List<TaiKhoan> listTaiKhoan = DataProvider.Ins.DB.TaiKhoans.ToList();
            //Biến i sẽ là STT tăng dần
            int i = 1;
            foreach (TaiKhoan item in listTaiKhoan)
              
            {
                List<RoleTaiKhoan> ListRole = DataProvider.Ins.DB.RoleTaiKhoans.Where(p => p.Id == item.IdRoleTaiKhoan).ToList();

                Accounts acc = new Accounts();
                //Đổ số thứ tự tài khoản
                acc.STT = i;
                //Đổ tai khoản
                acc.TaiKhoan = item;
                acc.RoleTaiKhoan = ListRole[0];
                ListAccounts.Add(acc);
                i++;
            }
        }

    }
}
