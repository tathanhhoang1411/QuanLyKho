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
            //Biến i sẽ là STT tăng dần
            int i = 1;
            ListInputs = new ObservableCollection<Inputs>();
            var  listInput = (
                from inp in DataProvider.Ins.DB.BangNhaps
                join acc in DataProvider.Ins.DB.TaiKhoans on inp.IdTaiKhoan equals acc.Id
                join inpinf in DataProvider.Ins.DB.ThongTinBangNhaps on inp.Id equals inpinf.IdBangNhap
                join vattu in DataProvider.Ins.DB.VatTus on inpinf.IdVatTu equals vattu.Id
                join suppl in DataProvider.Ins.DB.NhaCungCaps on vattu.IdNhaCungCap equals suppl.Id
                select new  { inp,acc,inpinf,vattu,suppl}).ToList();
            foreach(var  item in listInput )
            {
                Inputs inps = new Inputs();
                inps.STT = i;
                //Đổ bangnhap
                inps.BangNhap = item.inp;
                inps.TaiKhoan = item.acc;
                inps.ThongTinBangNhap= item.inpinf;
                inps.Vattu = item.vattu;
                inps.NhaCungCap = item.suppl;
                string trangThai="";
                switch ((int)item.inpinf.TrangThai)
                {
                    case 0:
                        trangThai = "Hoàn thành";
                        break;
                    case 1:
                        trangThai = "Đang thực hiện";
                        break;
                    default:
                        break;
                }

                inps.TrangThai = trangThai.ToString();
                ListInputs.Add(inps);
                i++;
            }


        }
    }
}
