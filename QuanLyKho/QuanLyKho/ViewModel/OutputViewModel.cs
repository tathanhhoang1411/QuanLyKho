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
        //Các trường này sẽ binding qua view 
        private string _SDT;
        public string  SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }

        private string _TenKhachHang;
        public string TenKhachHang { get => _TenKhachHang; set { _TenKhachHang = value; OnPropertyChanged(); } }

        private string _TenVatTu;
        public string TenVatTu { get => _TenVatTu; set { _TenVatTu = value; OnPropertyChanged(); } }

        private int _Count;
        public int Count { get => _Count; set { _Count = value; OnPropertyChanged(); } }

        private double _GiaXuat;
        public double GiaXuat { get => _GiaXuat; set { _GiaXuat = value; OnPropertyChanged(); } }

        private string _TrangThai;
        public string TrangThai { get => _TrangThai; set { _TrangThai = value; OnPropertyChanged(); } }

        private DateTime _NgayXuat;
        public DateTime NgayXuat { get => _NgayXuat; set { _NgayXuat = value; OnPropertyChanged(); } }

        private Outputs _SelectedItem;
        public Outputs SelectedItem { get=> _SelectedItem; set { _SelectedItem = value;OnPropertyChanged();
                if (SelectedItem != null)
                {
                    SDT = SelectedItem.KhachHang.SDT;
                    TenKhachHang = SelectedItem.KhachHang.Ten;
                    TenVatTu = SelectedItem.Vattu.Ten;
                    Count = (int)SelectedItem.ThongTinBangXuat.Count;
                    GiaXuat =(double)SelectedItem.ThongTinBangXuat.GiaXuat;
                    switch ((int)SelectedItem.ThongTinBangXuat.TrangThai)
                    {
                        case 0:
                            TrangThai = "Hoàn thành";
                            break;
                        case 1:
                            TrangThai = "Đang trong quá trình";
                            break;
                        default:
                            break;
                    }
                    NgayXuat =(DateTime) SelectedItem.BangXuat.NgayXuat;
                } 
            }
        }

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
