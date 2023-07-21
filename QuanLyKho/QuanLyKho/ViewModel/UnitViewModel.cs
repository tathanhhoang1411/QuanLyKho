using System;
using QuanLyKho.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace QuanLyKho.ViewModel
{
    public class UnitViewModel : BaseViewModel
    {
        private ObservableCollection<DonViDo> _ListDonViDoes;
        public ObservableCollection<DonViDo> ListDonViDoes { get => _ListDonViDoes; set { _ListDonViDoes = value; OnPropertyChanged(); } }
        public UnitViewModel()
        {
            ListDonViDoes = new ObservableCollection<DonViDo>(DataProvider.Ins.DB.DonViDoes);
        }
    }
}
