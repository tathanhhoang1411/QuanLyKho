﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QuanLyKhoEntities : DbContext
    {
        public QuanLyKhoEntities()
            : base("name=QuanLyKhoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BangNhap> BangNhaps { get; set; }
        public virtual DbSet<BangXuat> BangXuats { get; set; }
        public virtual DbSet<DonViDo> DonViDoes { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<RoleTaiKhoan> RoleTaiKhoans { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<ThongTinBangNhap> ThongTinBangNhaps { get; set; }
        public virtual DbSet<ThongTinBangXuat> ThongTinBangXuats { get; set; }
        public virtual DbSet<VatTu> VatTus { get; set; }
    }
}
