//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyKho.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ThongTinBangNhap
    {
        public int Id { get; set; }
        public int IdVatTu { get; set; }
        public int IdBangNhap { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<double> GiaNhap { get; set; }
        public Nullable<int> TrangThai { get; set; }
    
        public virtual BangNhap BangNhap { get; set; }
        public virtual VatTu VatTu { get; set; }
    }
}
