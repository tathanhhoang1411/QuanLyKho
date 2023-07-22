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
                if (SelectedItem !=null) 
                {
                    HoVaTen = SelectedItem.TaiKhoan.HoVaTen;
                    TaiKhoan = SelectedItem.TaiKhoan.TenTaiKhoan;
                    SDT = SelectedItem.TaiKhoan.SDT;
                    Email=SelectedItem.TaiKhoan.Email;
                    TenRoleTaiKhoan = SelectedItem.RoleTaiKhoan.Ten;
                    NgayTao =(DateTime)SelectedItem.TaiKhoan.NgayTao;

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
                    RoleAccountViewModel rlVM = new RoleAccountViewModel();
                    TenRoleTaiKhoan2 = SelectedItemRole.RoleTK.Ten;

                }
            }
        }
        public AcocuntsViewModel()
        {
          
            LoadAccount();//đổ danh sách tài khoản vào 
            LoadRole();//đổ danh sách role



        }
        public void LoadRole()
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
