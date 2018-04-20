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
	('зимняя'),
	('летняя')


create table Country
(
	IDCountry int not null primary key identity,
	Country nvarchar(20) not null
) 

insert into Country
values
	('Украина'),
	('Канада'),
	('Россия')

create table Sports
(
	IDSport int not null primary key identity,
	ТNameSport nvarchar(50) not null
)
insert into Sports
values 
	('Биатлон'),
	('Фигурное катание'),
	('Бобслей'),
	('Художественная гимнастика'),
	('Конный спорт'),
	('Лёгкая атлетика')

create table Medals
(
	IDMedal int not null primary key identity,
	Medal nvarchar(20) not null,
)

insert into Medals
values
	('золото'),
	('серебро'),
	('бронза'),
	('не призовое место')

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
	('Иванов1','Иван1', 1, 1, '1990-10-10', 1),
	('Иванов2','Иван12', 2, 1, '2000-10-10', 2),
	('Иванов3','Иван13', 3, 1, '2000-10-10', 3),

	('Иванов2','Иван2', 1, 2, '1990-10-10', 1),
	('Иванов22','Иван22', 2, 2, '2000-10-10', 2),
	('Иванов23','Иван23', 3, 2, '2000-10-10', 3),

	('Иванов3','Иван3', 1, 3, '1990-10-10', 2),
	('Иванов23','Иван23', 2, 3, '2000-10-10', 1),
	('Иванов33','Иван33', 3, 3, '2000-10-10', 3),

	('Иванов4','Иван4', 1, 4, '1990-10-10', 1),
	('Иванов24','Иван24', 2, 4, '2000-10-10', 3),
	('Иванов34','Иван34', 3, 4, '2000-10-10', 2),

	('Иванов5','Иван5', 1, 5, '1990-10-10', 3),
	('Иванов25','Иван25', 2, 5, '2000-10-10', 2),
	('Иванов35','Иван35', 3, 5, '2000-10-10', 1),

	('Иванов6','Иван6', 1, 6, '1990-10-10', 2),
	('Иванов26','Иван26', 2, 6, '2000-10-10', 1),
	('Иванов36','Иван36', 3, 6, '2000-10-10', 3)


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
	(1, 2018, 'Киев', 1, 1, 1),
	(1, 2018, 'Киев', 2, 3, 1),
	(1, 2018, 'Киев', 3, 4, 1),
	(1, 2018, 'Кривой Рог', 4, 2, 1),
	(1, 2018, 'Кривой Рог', 5, 4, 1),
	(1, 2018, 'Кривой Рог', 6, 4, 1),
	(1, 2018, 'Одесса', 8, 4, 1),
	(1, 2018, 'Одесса', 9, 4, 1),
	(1, 2018, 'Одесса', 7, 4, 1),

	(2, 2016, 'Торонто', 1, 2, 1),
	(2, 2016, 'Торонто', 2, 4, 1),
	(2, 2016, 'Торонто', 3, 4, 1),
	(2, 2016, 'Ванкувер', 4, 3, 1),
	(2, 2016, 'Ванкувер', 5, 1, 1),
	(2, 2016, 'Ванкувер', 6, 4, 1),
	(2, 2016, 'Оттава', 8, 4, 1),
	(2, 2016, 'Оттава', 9, 4, 1),
	(2, 2016, 'Оттава', 7, 4, 1),

	(3, 2014, 'Москва', 1, 3, 1),
	(3, 2014, 'Москва', 2, 4, 1),
	(3, 2014, 'Москва', 3, 4, 1),
	(3, 2014, 'Ростов', 4, 2, 1),
	(3, 2014, 'Ростов', 5, 1, 1),
	(3, 2014, 'Ростов', 6, 1, 1),
	(3, 2014, 'Тюмень', 8, 4, 1),
	(3, 2014, 'Тюмень', 9, 4, 1),
	(3, 2014, 'Тюмень', 7, 4, 1),

	(1, 2017, 'Львов', 10, 1, 2),
	(1, 2017, 'Львов', 11, 3, 2),
	(1, 2017, 'Львов', 12, 4, 2),
	(1, 2017, 'Донецк', 13, 2, 2),
	(1, 2017, 'Донецк', 14, 4, 2),
	(1, 2017, 'Донецк', 15, 4, 2),
	(1, 2017, 'Харьков', 16, 4, 2),
	(1, 2017, 'Харьков', 17, 4, 2),
	(1, 2017, 'Харьков', 18, 4, 2),

	(2, 2015, 'Квебек', 10, 2, 2),
	(2, 2015, 'Квебек', 11, 4, 2),
	(2, 2015, 'Квебек', 12, 4, 2),
	(2, 2015, 'Ванкувер', 13, 3, 2),
	(2, 2015, 'Ванкувер', 14, 1, 2),
	(2, 2015, 'Ванкувер', 15, 4, 2),
	(2, 2015, 'Калгари', 16, 4, 2),
	(2, 2015, 'Калгари', 17, 4, 2),
	(2, 2015, 'Калгари', 18, 4, 2),
	
	(2, 2013, 'Гатино', 10, 4, 2),
	(2, 2013, 'Гатино', 11, 4, 2),
	(2, 2013, 'Гатино', 12, 4, 2),
	(2, 2013, 'Кингстон', 13, 4, 2),
	(2, 2013, 'Кингстон', 14, 4, 2),
	(2, 2013, 'Кингстон', 15, 1, 2),
	(2, 2013, 'Виннипег', 16, 4, 2),
	(2, 2013, 'Виннипег', 17, 2, 2),
	(2, 2013, 'Виннипег', 18, 3, 2)

















