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