using System;
using QuanLyKho.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace QuanLyKho.ViewModel
{
    public class InputViewModel : BaseViewModel
    {
        private ObservableCollection<Inputs> _ListInputs;
        public ObservableCollection<Inputs> ListInputs { get => _ListInputs; set { _ListInputs = value; OnPropertyChanged(); } }

        public InputViewModel()
        {
            LoadInput();
        }
        public void LoadInput()
        {
            ListInputs = new ObservableCollection<Inputs>();
            List<BangNhap> listInput = DataProvider.Ins.DB.BangNhaps.ToList();
            //Biến i sẽ là STT tăng dần
            int i = 1;
            foreach (BangNhap itemInp in listInput)

            {
                List<ThongTinBangNhap> ListThongTinBangNhap = DataProvider.Ins.DB.ThongTinBangNhaps.Where(p => p.IdBangNhap == itemInp.Id).ToList();
                List<TaiKhoan> ListTaiKhoan = DataProvider.Ins.DB.TaiKhoans.Where(p => p.Id == itemInp.IdTaiKhoan).ToList();
                List<NhaCungCap> ListNhaCungCap = DataProvider.Ins.DB.NhaCungCaps.Where(p => p.Id == itemInp.IdTaiKhoan).ToList();
                foreach (ThongTinBangNhap itemInpInf in ListThongTinBangNhap)
                {
                    List<VatTu> ListVatTu = DataProvider.Ins.DB.VatTus.Where(p => p.Id == itemInpInf.IdVatTu).ToList();
                }
                Inputs inp = new Inputs();
                //Đổ số thứ tự tài khoản
                inp.STT = i;
                //Đổ tai khoản
                //inp.BangNhap = item;
                //inp.Vattu= ;
               // ListAccounts.Add(acc);
                i++;
            }
        }
    }
}
