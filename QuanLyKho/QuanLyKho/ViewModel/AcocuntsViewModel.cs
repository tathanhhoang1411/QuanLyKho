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
   public class AcocuntsViewModel:BaseViewModel
    {
        private ObservableCollection<Accounts> _ListAccounts;
        public ObservableCollection<Accounts> ListAccounts { get => _ListAccounts; set { _ListAccounts = value; OnPropertyChanged(); } }
        private ObservableCollection<RoleAccounts> _ListRoleAccounts;
        public ObservableCollection<RoleAccounts> ListRoleAccounts { get => _ListRoleAccounts; set { _ListRoleAccounts = value; OnPropertyChanged(); } }
        public AcocuntsViewModel()
        {
            LoadTaiKhoan();
            LoadRole();
        }
        public void LoadTaiKhoan()
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
                acc.RoleTaiKhoan = ListRole[0].Ten.ToString();
                ListAccounts.Add(acc);
                i++;
            }
        }
        public void LoadRole()
        {
            ListRoleAccounts = new ObservableCollection<RoleAccounts>();
            List<RoleTaiKhoan> listRoleTaiKhoan = DataProvider.Ins.DB.RoleTaiKhoans.ToList();
            int i = 1;
            foreach (RoleTaiKhoan item in listRoleTaiKhoan)

            {
                RoleAccounts rolacc = new RoleAccounts();
                //Đổ số thứ tự Role
                rolacc.STT = i;
                //Đổ role
                rolacc.RoleTK = item;
                ListRoleAccounts.Add(rolacc);
                i++;
            }
        }
    }
}
