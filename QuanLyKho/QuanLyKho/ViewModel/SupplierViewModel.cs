using System;
using QuanLyKho.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    public class SupplierViewModel : BaseViewModel
    {
        private ObservableCollection<Supplieres> _ListSuppls;
        public ObservableCollection<Supplieres> ListSuppls { get => _ListSuppls; set { _ListSuppls = value; OnPropertyChanged(); } }
        //các trường sẽ binding qua view
        private string _TenNhaCungCap;
        public string TenNhaCungCap { get => _TenNhaCungCap; set { _TenNhaCungCap = value; OnPropertyChanged(); } }

        private string _SDT;
        public string  SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private string _ThongTinThem;
        public string  ThongTinThem { get => _ThongTinThem; set { _ThongTinThem = value; OnPropertyChanged(); } }

        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }

        private DateTime _NgayHopTac;
        public DateTime NgayHopTac { get => _NgayHopTac; set { _NgayHopTac = value; OnPropertyChanged(); } }

        //command cho các nút chức năng thêm xóa sửa
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UnDeleteCommand { get; set; }

        private Supplieres _SelectedItem;
        public Supplieres SelectedItem { 
            get=> _SelectedItem; 
            set { _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem!=null)
                {
                    TenNhaCungCap = SelectedItem.NhaCungCap.Ten;
                    SDT = SelectedItem.NhaCungCap.SDT;
                    Email = SelectedItem.NhaCungCap.Email;
                    ThongTinThem = SelectedItem.NhaCungCap.ThongTinThem;
                    DiaChi = SelectedItem.NhaCungCap.Address;
                    NgayHopTac = (DateTime)SelectedItem.NhaCungCap.NgayHopTac;
                }
            } }


        public SupplierViewModel()
        {
            NgayHopTac = DateTime.Now;
            LoadSuppl();
            AddCommand = new RelayCommand<object>((p) => { return CanAddCommand(); }, (p) => { ExcutedAddCommand(); });
            EditCommand = new RelayCommand<object>((p) => { return CanEditCommand(); }, (p) => { ExcutedEditCommand(); });
            DeleteCommand = new RelayCommand<object>((p) => { return CanDelCommand(); }, (p) => { ExcutedDelCommand(); });
            UnDeleteCommand = new RelayCommand<object>((p) => { return CanDelCommand(); }, (p) => { ExcutedUnDelCommand(); });
        }
        public ObservableCollection<Supplieres> LoadComboboxSuppl()
        {
            ListSuppls = new ObservableCollection<Supplieres>();
            List<NhaCungCap> listNhaCC = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.TrangThai == 1).ToList();//Combobox unit chỉ hiện những donvido có trang thai =1
            //Biến i sẽ là STT tăng dần
            int i = 1;
            foreach (NhaCungCap item in listNhaCC)

            {

                Supplieres nhacc = new Supplieres();
                //Đổ số thứ tự Khách hàng
                nhacc.STT = i;
                //Đổ tai khoản
                nhacc.NhaCungCap = item;
                ListSuppls.Add(nhacc);
                i++;
            }
            return ListSuppls;
        }
        public void LoadSuppl()
        {
            ListSuppls = new ObservableCollection<Supplieres>();
            List<NhaCungCap> listNhaCungCap = DataProvider.Ins.DB.NhaCungCaps.ToList();
            //Biến i sẽ là STT tăng dần
            int i = 1;
            foreach (NhaCungCap item in listNhaCungCap)

            {

                Supplieres supp = new Supplieres();
                //Đổ số thứ tự Khách hàng
                supp.STT = i;
                if (item.TrangThai == 1)
                {
                    supp.TrangThai = "Kích hoạt";
                }
                else
                {
                    supp.TrangThai = "Tắt";
                }
                //Đổ tai khoản
                supp.NhaCungCap= item;
                ListSuppls.Add(supp);
                i++;
            }
        }


        protected bool CanAddCommand()
        {
            if (string.IsNullOrWhiteSpace(TenNhaCungCap) 
                || string.IsNullOrWhiteSpace(Email) 
                || string.IsNullOrWhiteSpace(SDT) 
                || string.IsNullOrWhiteSpace(DiaChi)
                || string.IsNullOrWhiteSpace(NgayHopTac.ToString()))
            {
                return false;
            }
            List<NhaCungCap> list = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.Ten == TenNhaCungCap || x.Email==Email || x.SDT==SDT).ToList();
            if (list.Count() == 0)//Nếu List đơn vị đo chưa từng tồn tại nhacungcap.ten có giá trị nhacungcap 
            {
                foreach (Supplieres  item in ListSuppls)
                {
                    if (item.NhaCungCap.Ten != TenNhaCungCap)
                    {
                        return true;
                    }
                }


            }
                return false;
        }

        private void ExcutedAddCommand()
        {
            try
            {
                NhaCungCap nhacc = new NhaCungCap();
                nhacc.Ten = TenNhaCungCap.Trim();
                nhacc.Email = Email.Trim();
                nhacc.SDT= SDT.Trim();
                nhacc.ThongTinThem = ThongTinThem.Trim();
                nhacc.TrangThai = 1;
                nhacc.Address = DiaChi.Trim();
                if (NgayHopTac > DateTime.Now)
                {
                    nhacc.NgayHopTac =DateTime.Now;
                }
                else
                {
                    nhacc.NgayHopTac = NgayHopTac;
                }
                
                DataProvider.Ins.DB.NhaCungCaps.Add(nhacc);
                DataProvider.Ins.DB.SaveChanges();

                //phương án 1: bỏ unit vào vị trí cuối của ListDonViDoes vì ListDonViDoes có OnPropertyChanged()
                //kết quả: không load lại ListDonViDoes
                //int countItem=ListDonViDoes.Count();
                //ListDonViDoes[countItem - 1].STT= 100;
                //ListDonViDoes[countItem - 1].DonViDo = unit;

                //phương án 2: gọi lại hàm LoadUnit() chắc chắc sẽ load lại ListDonViDoes vì trong hàm này có ListDonViDoes
                //Kết quả: thành công nhưng vì mất thời gian gọi nên sẽ lâu hơn với dữ liệu lớn 
                LoadSuppl();


                MessageBox.Show("Thêm đối tác nhà cung cấp" + TenNhaCungCap.Trim() + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm nhà cung cấp ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        protected bool CanEditCommand()
        {

            if (SelectedItem == null 
                || string.IsNullOrWhiteSpace(TenNhaCungCap)
                || string.IsNullOrWhiteSpace(Email)
                || string.IsNullOrWhiteSpace(ThongTinThem)
                || string.IsNullOrWhiteSpace(SDT)
                || string.IsNullOrWhiteSpace(DiaChi)
                || string.IsNullOrWhiteSpace(NgayHopTac.ToString()))
            {
                return false;
            }
            else
            {

                return true;
            }
        }
        private void ExcutedEditCommand()
        {
            try
            {
                NhaCungCap nhacc = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.Id == SelectedItem.NhaCungCap.Id).SingleOrDefault();
                nhacc.Ten = TenNhaCungCap.Trim();
                nhacc.Email = Email.Trim();
                nhacc.SDT= SDT.Trim();
                nhacc.ThongTinThem = ThongTinThem.Trim();
                if(NgayHopTac> DateTime.Now)
                {
                    nhacc.NgayHopTac= DateTime.Now;
                }
                else
                {
                    nhacc.NgayHopTac = (DateTime)NgayHopTac;
                }
              
                DataProvider.Ins.DB.SaveChanges();
                LoadSuppl();
                MessageBox.Show("Sửa thông tin nhà cung cấp" + TenNhaCungCap.Trim().ToUpper() + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiến trình lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected bool CanDelCommand()
        {
            if (SelectedItem == null)//Nếu không chọn item nào
            {
                return false;
            }
            else
            {

                return true;
            }

        }
        private void ExcutedUnDelCommand()
        {
            try
            {
                var nhacc = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.Id == SelectedItem.NhaCungCap.Id).SingleOrDefault();
                nhacc.TrangThai = 1;// dùng
                DataProvider.Ins.DB.SaveChanges();
                LoadSuppl();
                MessageBox.Show("Kích hoạt nhà cung cấp" + TenNhaCungCap.Trim().ToUpper() + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiến trình lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void ExcutedDelCommand()
        {
            try
            {
                var nhacc = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.Id == SelectedItem.NhaCungCap.Id).SingleOrDefault();
                nhacc.TrangThai = 0;// 
                DataProvider.Ins.DB.SaveChanges();
                LoadSuppl();
                MessageBox.Show("Tắt nhà cung cấp" + TenNhaCungCap.Trim().ToUpper() + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiến trình lỗi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
