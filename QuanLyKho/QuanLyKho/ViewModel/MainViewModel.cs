using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    public class MainViewModel : BaseViewModel
    {

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
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                Isloaded = true;
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;

                if (loginVM.IsLogin)
                {
                    p.Show();
                }
                else
                {
                    p.Close();
                }
            }
              );

            UnitWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                UnitWindow wd = new UnitWindow();
                wd.ShowDialog();
            });
            AccountsWindowCommand=new  RelayCommand<object>((p) => { return true; }, (p) => {
                AccountsWindow wd = new AccountsWindow();
                wd.ShowDialog();
            });
            InputWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>{
                InputWindow wd = new InputWindow();
                wd.ShowDialog();
            });
            OutputWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>{
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
    }
}
