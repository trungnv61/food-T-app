create database FoodOnline

create table Users (
    UserId int primary key identity(1, 1) not null,
	Name varchar(50)  null,
	UserName varchar(50) null unique,
	Mobile varchar(50) null,
	Email varchar(50) null unique,
	Address varchar(max) null,
	PostCode varchar(50) null,
	Password varchar(50) null,
	ImageUrl varchar(max) null,
	CreatedDate datetime null
)

create table Contact (
    ContactId int primary key identity(1, 1) not null,
	Name varchar(50) null,
	Email varchar(50) null,
	Subject varchar(50) null,
	Message varchar(max) null,
	CreatedDate datetime null
)

create table Categories (
   CategoryId int primary key identity(1, 1) not null,
   Name varchar(50) null,
   ImageUrl varchar(max) null,
   IsActive bit null,
   CreatedDate datetime null
)


create table Products (
    ProductId int primary key identity(1, 1) not null,
	Name varchar(50)  null,
	Description varchar(max) null,
	Price decimal(18, 2) null,
	Quantity int null,
	ImageUrl varchar(max) null,
	CategoryId int null references Categories(CategoryId),
    IsActive bit null,
    CreatedDate datetime null
)

create table Carts (
    CartId int primary key identity(1, 1) not null,
	ProductId int null references Products(ProductId),
	Quantity int null,
	UserId int null references Users(UserId)
)


create table Payment (
   PaymentId int primary key identity(1, 1) not null,
   Name varchar(50) null,
   CardNo varchar(50) null,
   ExpiryDate varchar(50) null,
   CvvNo int null,
   Address varchar(max) null,
   PaymentMode varchar(50) null
)


create table Orders (
    OrderDetailsId int primary key identity(1, 1) not null,
	OrderNo varchar(450) null unique,
	ProductId int null references Products(ProductId),
    Quantity int null,
	UserId int null references Users(UserId),
	Status varchar(50) null,
	PaymentId int null references Payment(PaymentId),
	OrderDate datetime null
)                   