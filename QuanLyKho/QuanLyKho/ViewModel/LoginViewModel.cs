using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.TextFormatting;

namespace QuanLyKho.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand MainWindowCommand { get; set; }
        public ICommand CloseLoginWindow { get; set; }

        // mọi thứ xử lý sẽ nằm trong này
        public LoginViewModel()
        {
            MainWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
             MainWindow mainWindow = new MainWindow();
             mainWindow.ShowDialog();
            });

            CloseLoginWindow = new RelayCommand<object>((p) => { return true; }, (p) => {
                  MessageBoxResult result= MessageBox.Show("Thoát chương trình","Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                { 
                }
            });
        }
    }
}