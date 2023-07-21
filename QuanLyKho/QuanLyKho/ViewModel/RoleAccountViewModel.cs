using System;
using QuanLyKho.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace QuanLyKho.ViewModel
{
    public class RoleAccountViewModel : BaseViewModel
    {
        private ObservableCollection<RoleAccounts> _ListRoleAccounts;
        public ObservableCollection<RoleAccounts> ListRoleAccounts { get => _ListRoleAccounts; set { _ListRoleAccounts = value; OnPropertyChanged(); } }
        public RoleAccountViewModel()
        {

        }
        public ObservableCollection<RoleAccounts> LoadRole()
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
            return ListRoleAccounts;
        }
    }
}
