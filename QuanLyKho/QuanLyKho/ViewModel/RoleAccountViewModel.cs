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
        public ObservableCollection<RoleAccounts> LoadComboboxRole()
        {

            ListRoleAccounts = new ObservableCollection<RoleAccounts>();
            List<RoleTaiKhoan> listRole = DataProvider.Ins.DB.RoleTaiKhoans.Where(x => x.TrangThai == 1).ToList();//Combobox unit chỉ hiện những donvido có trang thai =1
            //Biến i sẽ là STT tăng dần
            int i = 1;
            foreach (RoleTaiKhoan item in listRole)

            {

                RoleAccounts role = new RoleAccounts();
                //Đổ số thứ tự Khách hàng
                role.STT = i;

                role.RoleTK = item;
                ListRoleAccounts.Add(role);
                i++;
            }
            return ListRoleAccounts;
        }
    }
}
