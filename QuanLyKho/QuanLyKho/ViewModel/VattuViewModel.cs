using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.ViewModel
{
  
        public class VattuViewModel : BaseViewModel
        {
            private ObservableCollection<VatTus> _ListVattus;
            public ObservableCollection<VatTus> ListVattus { get => _ListVattus; set { _ListVattus = value; OnPropertyChanged(); } }
            public VattuViewModel()
            {
                LoadVattu();
            }
            public void LoadVattu()
            {
            //Biến i sẽ là STT tăng dần
            int i = 1;
            ListVattus = new ObservableCollection<VatTus>();
                var  listVattu = (
                from vt in DataProvider.Ins.DB.VatTus
                join suppli in DataProvider.Ins.DB.NhaCungCaps on vt.IdNhaCungCap equals suppli.Id
                join unit in DataProvider.Ins.DB.DonViDoes on vt.IdDonViDo equals unit.Id
                select new {vt, suppli ,unit}).ToList();

            foreach (var item in listVattu)

                {

                    VatTus vattu = new VatTus();
                //Đổ số thứ tự Khách hàng
                vattu.STT = i;
                //Đổ tai khoản
                vattu.VatTu = item.vt;
                vattu.TenDonViDo = item.unit.Ten;
                vattu.TenNhaCungCap=item.suppli.Ten;
                    ListVattus.Add(vattu);
                    i++;
                }
            }
        }
    }

