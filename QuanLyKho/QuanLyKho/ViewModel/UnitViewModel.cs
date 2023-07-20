using System;
using QuanLyKho.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.ViewModel
{
    public class UnitViewModel : BaseViewModel
    {
        private List<DonViDo> _ListDonViDoes;
        public List<DonViDo> ListDonViDoes { get => _ListDonViDoes; set { _ListDonViDoes = value; OnPropertyChanged(); } }
        public UnitViewModel()
        {
            ListDonViDoes = DataProvider.Ins.DB.DonViDoes.ToList();
        }
    }
}
