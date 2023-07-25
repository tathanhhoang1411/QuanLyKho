create database QuanLyKho
go

use QuanlyKho
go

create table DonViDo
(
	Id int identity(1,1) primary key,
	Ten nvarchar(max),
	TrangThai int default(1)
)
insert into DonViDo values (N'Tấm(0,3m * 0.3m)',1)
insert into DonViDo values (N'Bao( 50KG)',1)
insert into DonViDo values (N'Trụ sắt( 3m)',1)
insert into DonViDo values (N'Tấm( 2m * 5m',1)
 select * from donvido

create table NhaCungCap
(
	Id int identity(1,1) primary key,
	Ten nvarchar(max),
	Address nvarchar(max),
	SDT nvarchar(20),
	Email nvarchar(200),
	ThongTinThem nvarchar(max),
	NgayHopTac DateTime,
	TrangThai int default(1)
)

select * from  NhaCungCap
insert into NhaCungCap values(N'Công ty Cổ phân ACT',N'Tân Phú, Đồng Nai','0987545310','act@gmail.com',N'',N'1/1/2010',1)
insert into NhaCungCap values('ABC Company','123 Main Street, Anytown USA','555-1234','abc@company.com','Specializing in widgets','2021-01-01',1)
insert into NhaCungCap values('XYZ Corporation','456 High Street, Another Town USA','555-5678','xyz@corporation.com','Providing innovative solutions','2021-02-15',1)
insert into NhaCungCap values('Smith & Sons','789 Low Street, Somewhere USA','555-9012','smithandsons@email.com','Family owned and operated','2021-03-31',1)
insert into NhaCungCap values('Best Buy','1234 Tech Road, Anytown USA','555-4321','info@bestbuy.com','Electronics retailer','2021-04-15',1)
insert into NhaCungCap values('Johnson & Johnson','5678 Health Lane, Anytown USA','555-8765','contact@jnj.com','Pharmaceutical company','2021-05-01',1)
insert into NhaCungCap values('Acme Corporation','999 Industrial Drive, Anytown USA','555-1111','info@acme.com','Manufacturing and distribution','2021-06-15',1)
insert into NhaCungCap values('Big Bank','111 Finance Avenue, Anytown USA','555-2222','customerservice@bigbank.com','Full-service bank','2021-07-01',1)



create table KhachHang
(
	Id int identity(1,1) primary key,
	Ten nvarchar(max),
	Address nvarchar(max),
	SDT nvarchar(20),
	Email nvarchar(200),
	ThongTinThem nvarchar(max)
)
select * from KhachHang

insert into KhachHang values(N'Nguyễn Thị Hương',N'TP.HCM','0987654321','huong.nguyen@email.com','')
insert into KhachHang values(N'Trần Văn Nam',N'TP.HCM','0912345678','nam.tran@email.com','')
insert into KhachHang values(N'Lê Thị Mai',N'TP.HCM','0965432109','mai.le@email.com','')
insert into KhachHang values(N'Vũ Minh Tuấn',N'TP.HCM','0888888888','tuan.vu@email.com','')
insert into KhachHang values(N'Ngô Thị Hạnh',N'TP.HCM','0899999999','hanh.ngo@email.com','')
insert into KhachHang values(N'Phan Văn Đức',N'TP.HCM','0977777777','duc.phan@email.com','')
insert into KhachHang values(N'Dương Thị Thúy',N'TP.HCM','0909090909','thuy.duong@email.com','')
insert into KhachHang values(N'Nguyễn Hoàng Anh',N'TP.HCM','0911111111','anh.nguyen@email.com','')
insert into KhachHang values(N'Phạm Thị Thu',N'TP.HCM','0922222222','thu.pham@email.com','')
insert into KhachHang values(N'Lý Quang Minh',N'TP.HCM','0933333333','minh.ly@email.com','')
create table VatTu
(
	Id int  identity(1,1) primary key,
	Ten nvarchar(max),
	IdDonViDo int not null,
	IdNhaCungCap int not null,
	TrangThai int default(1),
	foreign key(IdDonViDo) references DonViDo(Id),
	foreign key(IdNhaCungCap) references NhaCungCap(Id),
)
select *from VatTu
insert into VatTu values(N'Gạch ốp tường 111',1,1,1)
insert into VatTu values(N'Xi Măng Siêu hút nước',1,7,1)
insert into VatTu values(N'Xi Măng Supper 1',1,3,1)
insert into VatTu values(N'Trụ sắt tròn  ',3,4,1)
insert into VatTu values(N'Trụ sắt V lỗ 3* 5cm ',3,4,1)
insert into VatTu values(N'Tôn Đông Á',4,6,1)
insert into VatTu values(N'Tôn Đại Thành',4,3,1)
create table RoleTaiKhoan
(
	Id int identity(1,1) primary key,
	Ten nvarchar(max)
)

select * from RoleTaiKhoan
insert into RoleTaiKhoan(Ten) values(N'Admin')
insert into RoleTaiKhoan(Ten) values(N'Nhân viên')
insert into RoleTaiKhoan(Ten) values(N'Quản lý')

create table TaiKhoan
(
	Id int identity(1,1) primary key,
	TenTaiKhoan nvarchar(max),
	HoVaTen nvarchar(100),
	MatKhau nvarchar(max),
    SDT nvarchar(20),
	IdRoleTaiKhoan int not null,
	Email nvarchar(200),
	NgayTao DateTime
	foreign key (IdRoleTaiKhoan) references RoleTaiKhoan(Id),
	TrangThai int default(1)
)
select * from TaiKhoan
insert into TaiKhoan values(N'TaThanhHoang', N'Tạ Thanh Hoàng', N'db69fc039dcbd2962cb4d28f5891aae1','0325793505', 1,'tathanhhoang.work@gmail.com','7/20/2010',1)
insert into TaiKhoan values(N'NguyenVanAn', N'Nguyễn Văn An', N'978aae9bb6bee8fb75de3e4830a1be46','0325793506', 2,'nguyenan@gmail.com','7/20/2023',1)
insert into TaiKhoan values(N'NguyenThiHanh', N'Nguyễn Thị Hạnh', N'978aae9bb6bee8fb75de3e4830a1be46','0325798778', 2,'nguyenhanh@gmail.com','7/20/2023',1)
insert into TaiKhoan values(N'TranThiKieu', N'Trần Thi Kiều', N'978aae9bb6bee8fb75de3e4830a1be46','0325790327', 2,'kieun@gmail.com','7/20/2023',1)
insert into TaiKhoan values(N'LeVan', N'Lê Văn', N'978aae9bb6bee8fb75de3e4830a1be46','0325895897', 2,'lvan@gmail.com', '7/20/2023',1)

create table BangNhap
(
	Id int identity(1,1) primary key,
	NgayNhap DateTime,
	IdTaiKhoan int not null
	foreign key (IdTaiKhoan) references TaiKhoan(Id),
)
select * from BangNhap
insert into BangNhap values ('7/7/2023',1)
insert into BangNhap values ('7/7/2023',2)
insert into BangNhap values ('7/7/2023',3)
insert into BangNhap values ('7/7/2023',2)
insert into BangNhap values ('7/7/2023',1)
create table ThongTinBangNhap
(
	Id int identity(1,1) primary key,
	IdVatTu int  not null,
	IdBangNhap int  not null,
	Count int,
	GiaNhap float default 0,
	TrangThai int,
	foreign key (IdVatTu) references VatTu(Id),
	foreign key (IdBangNhap) references BangNhap(Id),
)
select * from ThongTinBangNhap
insert into ThongTinBangNhap values(1,1,30,10000,0)
insert into ThongTinBangNhap values(2,2,100,200000,0)
insert into ThongTinBangNhap values(3,1,30,100000,0)
insert into ThongTinBangNhap values(1,2,50,15000,0)
insert into ThongTinBangNhap values(1,3,20,15000,0)
insert into ThongTinBangNhap values(3,5,70,100000,0)
insert into ThongTinBangNhap values(4,2,50,50000,0)
insert into ThongTinBangNhap values(5,2,100,50000,0)
insert into ThongTinBangNhap values(6,2,100,100000,0)
insert into ThongTinBangNhap values(7,2,100,100000,0)

create table BangXuat
(
	Id int identity(1,1) primary key,
	NgayXuat DateTime,
	IdTaiKhoan int not null

	foreign key (IdTaiKhoan) references TaiKhoan(Id)
)
select * from BangXuat
insert into BangXuat values ('7/4/2023',4)
insert into BangXuat values ('7/5/2020',2)
insert into BangXuat values ('7/7/2023',4)
insert into BangXuat values ('10/7/2022',2)
insert into BangXuat values ('10/10/2021',4)

create table ThongTinBangXuat
(
	Id int identity(1,1) primary key,
	IdVatTu int  not null,
	IdBangXuat int  not null,
	IdKhachHang int not null,
	Count int,	
	GiaXuat float default 0,
	TrangThai int ,
	foreign key (IdVatTu) references VatTu(Id),
	foreign key (IdBangXuat) references BangXuat(Id),
	foreign key (IdKhachHang) references KhachHang(Id)
)

insert into ThongTinBangXuat values(4,1,2,10,11000,0)
insert into ThongTinBangXuat values(2,2,1,4,250000,0)
insert into ThongTinBangXuat values(2,3,6,10,10000,0)
insert into ThongTinBangXuat values(1,1,3,5,5000,0)
insert into ThongTinBangXuat values(1,4,3,5,5000,0)
insert into ThongTinBangXuat values(1,5,3,5,5000,0)


select *from BangNhap
select *from BangXuat

