using System;
using QuanLyKho.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.ViewModel
{
    public class RoleAccount : BaseViewModel
    {
        private List<RoleTaiKhoan> _ListRoleAccounts;
        public List<RoleTaiKhoan> ListRoleAccounts { get => _ListRoleAccounts; set { _ListRoleAccounts = value; OnPropertyChanged(); } }
        public RoleAccount()
        {
            ListRoleAccounts = DataProvider.Ins.DB.RoleTaiKhoans.ToList();
        }
    }
}
