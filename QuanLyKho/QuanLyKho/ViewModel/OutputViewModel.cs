using System;
using QuanLyKho.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace QuanLyKho.ViewModel
{
    public class OutputViewModel : BaseViewModel
    {
        private ObservableCollection<Outputs> _ListOutputs;
        public ObservableCollection<Outputs> ListOutputs { get => _ListOutputs; set { _ListOutputs = value; OnPropertyChanged(); } }

        public OutputViewModel()
        {
            LoadOutput();
        }
        public void LoadOutput()
        {
            //Biến i sẽ là STT tăng dần
            int i = 1;
            ListOutputs = new ObservableCollection<Outputs>();
            var listOutput = (
                from outp in DataProvider.Ins.DB.BangXuats
                join acc in DataProvider.Ins.DB.TaiKhoans on outp.IdTaiKhoan equals acc.Id
                join outpinf in DataProvider.Ins.DB.ThongTinBangXuats on outp.Id equals outpinf.IdBangXuat
                join vattu in DataProvider.Ins.DB.VatTus on outpinf.IdVatTu equals vattu.Id
                join suppl in DataProvider.Ins.DB.NhaCungCaps on vattu.IdNhaCungCap equals suppl.Id
                join cus in DataProvider.Ins.DB.KhachHangs on outpinf.IdKhachHang equals cus.Id
                select new { outp, acc, outpinf, vattu, suppl,cus }).ToList();
            foreach (var item in listOutput)
            {
                Outputs outps = new Outputs();
                outps.STT = i;
                //Đổ bangnhap
                outps.BangXuat = item.outp;
                outps.TaiKhoan = item.acc;
                outps.ThongTinBangXuat = item.outpinf;
                outps.Vattu = item.vattu;
                outps.NhaCungCap = item.suppl;
                outps.KhachHang= item.cus;
                string trangThai = "";
                switch ((int)item.outpinf.TrangThai)
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

                outps.TrangThai = trangThai.ToString();
                ListOutputs.Add(outps);
                i++;
            }


        }
    }
}
