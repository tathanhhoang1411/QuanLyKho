using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        public bool IsLogin { get; set; }
        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }//onpropertyChanged để nhận sự thay đổi từ UserName
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } } 


        public ICommand CloseCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        // mọi thứ xử lý sẽ nằm trong này
        public LoginViewModel()
        {

            IsLogin = false;
            Password = "";
            UserName = "";
            LoginCommand = new RelayCommand<Window>((p) => { return isField(); }, (p) => { Login(p); });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
        }
        bool isField()
        {
            if (Password != "" &&  UserName!="" )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
         void Login(Window p)
        {

            //TaThanhHoang admin
            //NguyenVanAn staff
            if (UserName.Trim() == "" && Password.Trim() == "")
            {
                IsLogin = false;
                MessageBox.Show("Hãy nhập tài khoản và mật khẩu!");
            }
            else// có điền thông tin
            {
                 string passEncode = MD5Hash(Base64Encode(Password));
                var accCount = DataProvider.Ins.DB.TaiKhoans.Where(x => x.TenTaiKhoan == UserName && x.MatKhau == passEncode).Count();
                if (accCount > 0)
                {
                    IsLogin = true;
                    p.Close();
                }
                else
                {
                    IsLogin = false;
                    MessageBox.Show("Tài khoản hoặc mật khẩu chưa chính xác!");
                }
            }

        }
        public List<TaiKhoan> AccInfo()
        {
            string passEncode = MD5Hash(Base64Encode(Password));
            List<TaiKhoan> accInfo = DataProvider.Ins.DB.TaiKhoans.Where(x => x.TenTaiKhoan == UserName && x.MatKhau == passEncode).ToList();
            return accInfo;
        }
        public  bool IsAdmin()
        {

            if (AccInfo()[0].IdRoleTaiKhoan == 1)
            {

                return true;
               
            }
            else
            {
                return false;
            }
        }

         

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }



        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }


    }
}
