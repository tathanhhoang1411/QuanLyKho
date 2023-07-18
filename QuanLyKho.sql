create database QuanLyKho
go

use QuanlyKho
go

create table DonViDo
(
	Id int identity(1,1) primary key,
	Ten nvarchar(max)
)
go

create table NhaCungCap
(
	Id int identity(1,1) primary key,
	Ten nvarchar(max),
	Address nvarchar(max),
	SDT nvarchar(20),
	Email nvarchar(200),
	ThongTinThem nvarchar(max),
	NgayHopTac DateTime
)
go

create table KhachHang
(
	Id int identity(1,1) primary key,
	Ten nvarchar(max),
	Address nvarchar(max),
	SDT nvarchar(20),
	Email nvarchar(200),
	ThongTinThem nvarchar(max)
)
go

create table VatTu
(
	Id nvarchar(128) primary key,
	Ten nvarchar(max),
	IdDonViDo int not null,
	IdNhaCungCap int not null,
	QRCode nvarchar(max),

	foreign key(IdDonViDo) references DonViDo(Id),
	foreign key(IdNhaCungCap) references NhaCungCap(Id),
)
go

create table RoleTaiKhoan
(
	Id int identity(1,1) primary key,
	Ten nvarchar(max)
)
go

insert into RoleTaiKhoan(Ten) values(N'Admin')
go
insert into RoleTaiKhoan(Ten) values(N'Nhân viên')
go

create table TaiKhoan
(
	Id int identity(1,1) primary key,
	TenTaiKhoan nvarchar(max),
	HoVaTen nvarchar(100),
	MatKhau nvarchar(max),
	IdRoleTaiKhoan int not null

	foreign key (IdRoleTaiKhoan) references RoleTaiKhoan(Id)
)
go
insert into TaiKhoan values(N'TaThanhHoang', N'Tạ Thanh Hoàng', N'db69fc039dcbd2962cb4d28f5891aae1', 1)
go
insert into TaiKhoan values(N'NguyenVanAn', N'Nguyễn Văn An', N'978aae9bb6bee8fb75de3e4830a1be46', 2)
go

create table BangNhap
(
	Id nvarchar(128) primary key,
	NgayNhap DateTime
)
go

create table ThongTinBangNhap
(
	Id nvarchar(128) primary key,
	IdVatTu nvarchar(128) not null,
	IdBangNhap nvarchar(128) not null,
	Count int,
	GiaNhap float default 0,
	TrangThai nvarchar(max),
	IDTaiKhoan int not null

	foreign key (IdVatTu) references VatTu(Id),
	foreign key (IdBangNhap) references BangNhap(Id),
	foreign key (IdTaiKhoan) references TaiKhoan(Id)
)
go

create table BangXuat
(
	Id nvarchar(128) primary key,
	NgayXuat DateTime
)
go

create table ThongTinBangXuat
(
	Id nvarchar(128) primary key,
	IdVatTu nvarchar(128) not null,
	IdThongTinBangXuat nvarchar(128) not null,
	IdKhachHang int not null,
	Count int,	
	Status nvarchar(max)

	foreign key (IdVatTu) references VatTu(Id),
	foreign key (IdThongTinBangXuat) references ThongTinBangXuat(Id),
	foreign key (IdKhachHang) references KhachHang(Id)
)
go