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
using MessageBox = System.Windows.Forms.MessageBox;

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

        private string _MatKhau;
        public string MatKhau { get => _MatKhau; set { _MatKhau = value; OnPropertyChanged(); } }

        private DateTime _NgayTao;
        public DateTime NgayTao { get => _NgayTao; set { _NgayTao = value; OnPropertyChanged(); } }

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
                    MatKhau = SelectedItem.TaiKhoan.MatKhauUnEncry;
                    Email = SelectedItem.TaiKhoan.Email;
                    TenRoleTaiKhoan = SelectedItem.RoleTaiKhoan.Ten;
                    ImagePath = SelectedItem.TaiKhoan.AnhDaiDien;
                    NgayTao = (DateTime)SelectedItem.TaiKhoan.NgayTao;
                    SelectedItemRole.RoleTK = SelectedItem.RoleTaiKhoan;
                   
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

            }
        }
        public ICommand AddCommand { get;set ; }
        public ICommand EditCommand { get;set ; }
        public ICommand DeleteCommand { get;set ; }
        public ICommand UnDeleteCommand { get;set ; }
        public ICommand OpenFolderCommand { get;set ; }
        public ICommand SaveFileCommand { get;set ; }
        public ICommand ReLoadCommand { get;set ; }




        public AcocuntsViewModel()
        {
            NgayTao=DateTime.Now;
            LoadAccount();//đổ danh sách tài khoản vào 
            LoadComboBoxRole();//đổ danh sách role
            AddCommand = new RelayCommand<object>((p) => { return CanAddCommand(); }, (p) => { SaveImage(); ExcutedAddCommand();   });
            EditCommand = new RelayCommand<object>((p) => { return CanEditCommand(); }, (p) => { ExcutedEditCommand(); });
            DeleteCommand = new RelayCommand<object>((p) => { return CanDelCommand(); }, (p) => { ExcutedUnDelCommand(); });
            UnDeleteCommand = new RelayCommand<object>((p) => { return CanDelCommand(); }, (p) => { ExcutedDelCommand(); });
            ReLoadCommand = new RelayCommand<object>((p) => { return true; }, (p) => { LoadComboBoxRole(); Reload(); });
            OpenFolderCommand = new RelayCommand(OpenImage);
        }
        private void Reload()
        {
            
            HoVaTen = "";
            TaiKhoan = "";
            SDT = "";
            Email = "nhanvien@gmail.com";
            ImagePath = "";
            SelectedItemRole = null;
            MatKhau = "";
            SelectedItem = null;
            SelectedItemRole = null;
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

            ListAccounts = new ObservableCollection<Accounts>();
            List<TaiKhoan> listtk = DataProvider.Ins.DB.TaiKhoans.Where(x => x.TrangThai == 1).ToList();//Combobox unit chỉ hiện những donvido có trang thai =1
            //Biến i sẽ là STT tăng dần
            int i = 1;
            foreach (TaiKhoan item in listtk)

            {

                Accounts acc = new Accounts();
                //Đổ số thứ tự Khách hàng
                acc.STT = i;

                acc.TaiKhoan = item;
                ListAccounts.Add(acc);
                i++;
            }
            return ListAccounts;
        }
        public void LoadComboBoxRole()
        {
            RoleAccountViewModel role = new RoleAccountViewModel();
            ListRoleAccounts = role.LoadComboboxRole();
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
                switch (item.TrangThai)
                {
                    case 0: acc.TrangThai = "Khóa";
                        break;
                    case 1:acc.TrangThai = "Không khóa";
                        break;
                }
                    acc.TaiKhoan = item;
                acc.RoleTaiKhoan = ListRole[0];
                ListAccounts.Add(acc);
                i++;
            }
        }
        private bool CanAddCommand()
        {
            if(string.IsNullOrWhiteSpace(HoVaTen) 
                || string.IsNullOrWhiteSpace(TaiKhoan)
                || string.IsNullOrWhiteSpace(MatKhau)
                || string.IsNullOrWhiteSpace(SDT)
                || string.IsNullOrWhiteSpace(Email)
                || string.IsNullOrWhiteSpace(NgayTao.ToString())
                || SelectedItemRole == null


            )
            {
                return false;
            }
            if (SDT.Length < 9)
            {
                return false;
            }
            if (!int.TryParse(SDT, out int n))
            {
                return false;
            }

            List<TaiKhoan> list = DataProvider.Ins.DB.TaiKhoans.Where(x => x.SDT == SDT ).ToList();
            if (list.Count() == 0)
            {
                foreach (Accounts item in ListAccounts)
                {
                    if (item.TaiKhoan.SDT != SDT && item.TaiKhoan.TenTaiKhoan != TaiKhoan && item.TaiKhoan.AnhDaiDien!=ImagePath)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        private bool ktTaiKhoan()
        {
            TaiKhoan tk = DataProvider.Ins.DB.TaiKhoans.Where(p => p.SDT == SDT.Trim() && p.TenTaiKhoan==TaiKhoan.Trim()
            && p.Email==Email.Trim()).SingleOrDefault();
            if (tk == null)
            {
                return true;
            }
            return false;
        }
        private void ExcutedAddCommand()
        {
            try
            {
                if (ktTaiKhoan() == true)
                {
                    LoginViewModel lgVM = new LoginViewModel();
                    string matKhauMoi = lgVM.MD5Hash(lgVM.Base64Encode(MatKhau.Trim()));

                    TaiKhoan tk = new TaiKhoan();
                    tk.HoVaTen = HoVaTen.Trim();
                    tk.TenTaiKhoan = TaiKhoan.Trim();
                    tk.MatKhau = matKhauMoi;
                    tk.MatKhauUnEncry = MatKhau.Trim();
                    tk.SDT = SDT.Trim();
                    tk.Email = Email.Trim();
                    tk.IdRoleTaiKhoan = SelectedItemRole.RoleTK.Id;
                    tk.TrangThai = 1;
                    tk.AnhDaiDien = ImagePath.Trim();
                    if (NgayTao > DateTime.Now)
                    {
                        tk.NgayTao = DateTime.Now;
                    }
                    else
                    {
                        tk.NgayTao = (DateTime)NgayTao;
                    }
                    DataProvider.Ins.DB.TaiKhoans.Add(tk);
                    DataProvider.Ins.DB.SaveChanges();
                    LoadAccount();
                    MessageBox.Show("Thêm nhân viên: Tài khoản " + TaiKhoan.Trim().ToUpper() + " " + SDT.Trim() + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thông tin bạn đã nhập: Tài khoản " + TaiKhoan.Trim().ToUpper() + " " + SDT.Trim() + " đã tồn tại ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiến trình thêm bị lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private bool CanEditCommand()
        {
             if (string.IsNullOrWhiteSpace(HoVaTen)
                || string.IsNullOrWhiteSpace(TaiKhoan)
                || string.IsNullOrWhiteSpace(MatKhau)
                || string.IsNullOrWhiteSpace(SDT)
                || string.IsNullOrWhiteSpace(Email)
                || string.IsNullOrWhiteSpace(NgayTao.ToString())
                || string.IsNullOrWhiteSpace(ImagePath)
                || SelectedItemRole == null)
            {
                return false;
            }
            if (SDT.Length < 9)
            {
                return false;
            }
            if (!int.TryParse(SDT, out int n))
            {
                return false;
            }

            return true;
        }
        private void ExcutedEditCommand()
        {
            try
            {
                LoginViewModel lgVM = new LoginViewModel();
                string matKhauMoi = lgVM.MD5Hash(lgVM.Base64Encode(MatKhau.Trim()));

                var tk = DataProvider.Ins.DB.TaiKhoans.Where(x => x.Id == SelectedItem.TaiKhoan.Id).SingleOrDefault();
                tk.HoVaTen = HoVaTen.Trim();
                tk.TenTaiKhoan = TaiKhoan.Trim();
                tk.MatKhau = matKhauMoi;
                tk.MatKhauUnEncry = MatKhau.Trim();
                tk.SDT = SDT.Trim();
                tk.Email = Email.Trim();
                tk.IdRoleTaiKhoan = SelectedItemRole.RoleTK.Id;
                tk.TrangThai = 1;
                tk.AnhDaiDien = ImagePath.Trim();
                if (NgayTao > DateTime.Now)
                {
                    tk.NgayTao = DateTime.Now;
                }
                else
                {
                    tk.NgayTao = (DateTime)NgayTao;
                }
                DataProvider.Ins.DB.SaveChanges();
                LoadAccount();
                MessageBox.Show("Sửa thông tin nhân viên: Tài khoản " + TaiKhoan.Trim().ToUpper() + " " + SDT.Trim() + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Chọn item trước khi sửa ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private bool CanDelCommand()
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
        private void ExcutedUnDelCommand()
        {
            try
            {
                LoginViewModel lgVM = new LoginViewModel();
                string matKhauMoi = lgVM.MD5Hash(lgVM.Base64Encode(MatKhau.Trim()));

                var tk = DataProvider.Ins.DB.TaiKhoans.Where(x => x.Id == SelectedItem.TaiKhoan.Id).SingleOrDefault();
                tk.TrangThai = 1;
                DataProvider.Ins.DB.SaveChanges();
                LoadAccount();
                MessageBox.Show("Mở khóa nhân viên: Tài khoản " + TaiKhoan.Trim().ToUpper() + " " + SDT.Trim() + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiến trình bị lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void ExcutedDelCommand()
        {
            try
            {
                LoginViewModel lgVM = new LoginViewModel();
                string matKhauMoi = lgVM.MD5Hash(lgVM.Base64Encode(MatKhau.Trim()));

                var tk = DataProvider.Ins.DB.TaiKhoans.Where(x => x.Id == SelectedItem.TaiKhoan.Id).SingleOrDefault();
                tk.TrangThai = 0;
                DataProvider.Ins.DB.SaveChanges();
                LoadAccount();
                MessageBox.Show("Khóa nhân viên: Tài khoản " + TaiKhoan.Trim().ToUpper() + " " + SDT.Trim() + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiến trình bị lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

    }
}
