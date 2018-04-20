--CR4-12\SQLEXPRESS



create database Olympics
use Olympics


create table TypeOfOlympiads
(
	IDTypeOfOlympiad int not null primary key identity,
	TypeOfOlympiad nvarchar(20) not null
)
insert into TypeOfOlympiads
values 
	('������'),
	('������')


create table Country
(
	IDCountry int not null primary key identity,
	Country nvarchar(20) not null
) 

insert into Country
values
	('�������'),
	('������'),
	('������')

create table Sports
(
	IDSport int not null primary key identity,
	�NameSport nvarchar(50) not null
)
insert into Sports
values 
	('�������'),
	('�������� �������'),
	('�������'),
	('�������������� ����������'),
	('������ �����'),
	('˸���� ��������')

create table Medals
(
	IDMedal int not null primary key identity,
	Medal nvarchar(20) not null,
)

insert into Medals
values
	('������'),
	('�������'),
	('������'),
	('�� �������� �����')

create table Pictures
(
	IDPicture int not null primary key identity,
	Photo  nvarchar(50) not null
)

insert into Pictures
values
	('user1.jpg'),
	('user2.jpg'),
	('user3.jpg')


create table Athletes
(
	IDAthlete int not null primary key identity,
	FirstName  nvarchar(50) not null,
	LastName  nvarchar(50) not null,
	IDCountry int not null foreign key references Country(IDCountry) ON DELETE CASCADE,
	IDSport int not null foreign key references Sports(IDSport) ON DELETE CASCADE,
	[Data] date not null,
	IDPicture int not null foreign key references Pictures(IDPicture) ON DELETE CASCADE
)

insert into Athletes
values
	('������1','����1', 1, 1, '1990-10-10', 1),
	('������2','����12', 2, 1, '2000-10-10', 2),
	('������3','����13', 3, 1, '2000-10-10', 3),

	('������2','����2', 1, 2, '1990-10-10', 1),
	('������22','����22', 2, 2, '2000-10-10', 2),
	('������23','����23', 3, 2, '2000-10-10', 3),

	('������3','����3', 1, 3, '1990-10-10', 2),
	('������23','����23', 2, 3, '2000-10-10', 1),
	('������33','����33', 3, 3, '2000-10-10', 3),

	('������4','����4', 1, 4, '1990-10-10', 1),
	('������24','����24', 2, 4, '2000-10-10', 3),
	('������34','����34', 3, 4, '2000-10-10', 2),

	('������5','����5', 1, 5, '1990-10-10', 3),
	('������25','����25', 2, 5, '2000-10-10', 2),
	('������35','����35', 3, 5, '2000-10-10', 1),

	('������6','����6', 1, 6, '1990-10-10', 2),
	('������26','����26', 2, 6, '2000-10-10', 1),
	('������36','����36', 3, 6, '2000-10-10', 3)


create table Results
(
	IDResult int not null primary key identity,
	IDCountry int not null foreign key references Country(IDCountry),
	[Year] int not null,
	City nvarchar(50) not null,
	IDAthlete int not null foreign key references Athletes(IDAthlete),
	IDMedal int foreign key references Medals(IDMedal),
	IDTypeOfOlympiad int not null foreign key references TypeOfOlympiads(IDTypeOfOlympiad)
)
insert into Results
values
	(1, 2018, '����', 1, 1, 1),
	(1, 2018, '����', 2, 3, 1),
	(1, 2018, '����', 3, 4, 1),
	(1, 2018, '������ ���', 4, 2, 1),
	(1, 2018, '������ ���', 5, 4, 1),
	(1, 2018, '������ ���', 6, 4, 1),
	(1, 2018, '������', 8, 4, 1),
	(1, 2018, '������', 9, 4, 1),
	(1, 2018, '������', 7, 4, 1),

	(2, 2016, '�������', 1, 2, 1),
	(2, 2016, '�������', 2, 4, 1),
	(2, 2016, '�������', 3, 4, 1),
	(2, 2016, '��������', 4, 3, 1),
	(2, 2016, '��������', 5, 1, 1),
	(2, 2016, '��������', 6, 4, 1),
	(2, 2016, '������', 8, 4, 1),
	(2, 2016, '������', 9, 4, 1),
	(2, 2016, '������', 7, 4, 1),

	(3, 2014, '������', 1, 3, 1),
	(3, 2014, '������', 2, 4, 1),
	(3, 2014, '������', 3, 4, 1),
	(3, 2014, '������', 4, 2, 1),
	(3, 2014, '������', 5, 1, 1),
	(3, 2014, '������', 6, 1, 1),
	(3, 2014, '������', 8, 4, 1),
	(3, 2014, '������', 9, 4, 1),
	(3, 2014, '������', 7, 4, 1),

	(1, 2017, '�����', 10, 1, 2),
	(1, 2017, '�����', 11, 3, 2),
	(1, 2017, '�����', 12, 4, 2),
	(1, 2017, '������', 13, 2, 2),
	(1, 2017, '������', 14, 4, 2),
	(1, 2017, '������', 15, 4, 2),
	(1, 2017, '�������', 16, 4, 2),
	(1, 2017, '�������', 17, 4, 2),
	(1, 2017, '�������', 18, 4, 2),

	(2, 2015, '������', 10, 2, 2),
	(2, 2015, '������', 11, 4, 2),
	(2, 2015, '������', 12, 4, 2),
	(2, 2015, '��������', 13, 3, 2),
	(2, 2015, '��������', 14, 1, 2),
	(2, 2015, '��������', 15, 4, 2),
	(2, 2015, '�������', 16, 4, 2),
	(2, 2015, '�������', 17, 4, 2),
	(2, 2015, '�������', 18, 4, 2),
	
	(2, 2013, '������', 10, 4, 2),
	(2, 2013, '������', 11, 4, 2),
	(2, 2013, '������', 12, 4, 2),
	(2, 2013, '��������', 13, 4, 2),
	(2, 2013, '��������', 14, 4, 2),
	(2, 2013, '��������', 15, 1, 2),
	(2, 2013, '��������', 16, 4, 2),
	(2, 2013, '��������', 17, 2, 2),
	(2, 2013, '��������', 18, 3, 2)

















