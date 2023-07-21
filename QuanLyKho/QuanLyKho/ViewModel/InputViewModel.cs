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
        //Các trường này sẽ binding qua view 
        private string _HoVaTen;
        public string HoVaTen { get => _HoVaTen; set { _HoVaTen = value; OnPropertyChanged(); } }

        private string _SDT;
        public string SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }

        private string _TenVatTu;
        public string TenVatTu { get => _TenVatTu; set { _TenVatTu = value; OnPropertyChanged(); } }

        private int _Count;
        public int Count { get => _Count; set { _Count = value; OnPropertyChanged(); } }

        private double _GiaNhap;
        public double GiaNhap { get => _GiaNhap; set { _GiaNhap = value; OnPropertyChanged(); } }

        private string _TenNhaCungCap;
        public string TenNhaCungCap { get => _TenNhaCungCap; set { _TenNhaCungCap = value; OnPropertyChanged(); } }

        private string _TrangThai;
        public string TrangThai { get => _TrangThai; set { _TrangThai = value; OnPropertyChanged(); } }

        private DateTime _NgayNhap;
        public DateTime NgayNhap { get => _NgayNhap; set { _NgayNhap = value; OnPropertyChanged(); } }

        private Inputs _SelectedItem;
        public Inputs SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value; OnPropertyChanged();
                if (SelectedItem != null)
                {
                    HoVaTen = SelectedItem.TaiKhoan.HoVaTen;
                    SDT = SelectedItem.TaiKhoan.SDT;
                    TenVatTu = SelectedItem.Vattu.Ten;
                    Count = (int)SelectedItem.ThongTinBangNhap.Count;
                    TenNhaCungCap = SelectedItem.NhaCungCap.Ten;
                    GiaNhap = (double)SelectedItem.ThongTinBangNhap.GiaNhap;

                    switch ((int)SelectedItem.ThongTinBangNhap.TrangThai)
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
                    NgayNhap = (DateTime)SelectedItem.BangNhap.NgayNhap;
                }
            }
        }
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
