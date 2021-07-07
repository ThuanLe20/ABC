create database WEBABC1


create table Man(
	idman int identity(1,1) primary key,
	name nvarchar(100) not null,
	username nvarchar(100)  unique,
	password nvarchar(100) not null,
	email varchar(100) unique,
	phone varchar(15),
	address nvarchar(100) ,
	birth datetime,	
	role int

)
create table Cart(
	idcart int identity(1,1) not null primary key,
	totalprice float not null,
	idman int not null,
	foreign key (idman) references Man(idman)
)

create table Category(
	idcate int identity(1,1) not null primary key,
	name nvarchar(100) not null
)
create table Product(
	idpro int identity(1,1) not null primary key,
	namepro nvarchar(100) not null,
	price float not null,
	image nvarchar(500) not null,
	description nvarchar(2000) not null,
	idcate int 

	foreign key (idcate) references Category(idcate)
)
create table InfoCart(
	idifc int identity(1,1) not null primary key,
	amount int not null,
	idcart int not null,
	idpro int not null,

	foreign key (idcart) references Cart(idcart),
	foreign key (idpro) references Product(idpro)
)
create table Bill(
	idbill int identity(1,1) not null primary key,
	address nvarchar(100) default null,
	note nvarchar(100) default null,
	customername nvarchar(100) default null,
	datein datetime default null,
	dateout datetime default null,
	phoneout varchar(100) default null,
	status varchar(100) default null,
	idcus int default null,
	

	foreign key (idcus) references Man(idman),
	
)
create table InfoBill(
	idib int identity(1,1) not null primary key,
	price float null,
	idpro int not null,
	idbill int not null,
	count int null
	foreign key (idpro) references Product(idpro),
	foreign key (idbill) references Bill(idbill)
)
create table Contact(
	idcon int identity(1,1) not null primary key,
	emailcon varchar(100) not null,
	datein datetime default null,
	dateout datetime default null,
	textin nvarchar(100) not null,
	textout nvarchar(100) not null,
	status varchar(100) not null,
	idman int not null,

	foreign key (idman) references Man(idman)
)

insert Category(name) values(N'All')
insert Category(name) values(N'Breads')
insert Category(name) values(N'Pasties')
insert Category(name) values(N'Sliced caked')
insert Category(name) values(N'Whole caked')
insert Category(name) values(N'Packaged')
insert Category(name) values(N'Seasonal')
insert Category(name) values(N'Others')

------All---------
insert Product(namepro,price,image,description,idcate)
values (N'Bánh hamburger',10000,'A5.png', 
N'Bánh hamburger mềm, thơm ngon thích hợp dùng trong mọi dịp và mọi lúc bạn muốn.',1)
insert Product(namepro,price,image,description,idcate)
values (N'Bánh mì',5000,'A1.png', 
N'Ổ bánh mì nướng có da giòn, ruột mềm; bên trong là phần nhân.Là một loại thức ăn nhanh và bình dân dành cho buổi sáng, hoặc bất kỳ thời điểm nào trong ngày. ',1)
insert Product(namepro,price,image,description,idcate)
values (N'Bánh mì sandwich',10000,'A2.png', 
N'Bạn vẫn đang tìm kiếm một ổ bánh mì sandwich hoàn hảo với lớp vỏ mềm và độ dai vừa phải, phần nhân mềm, dai và dậy mùi thơm? Bây giờ nó ở đây.',1)
insert Product(namepro,price,image,description,idcate)
values (N'Bánh xốp matcha',40000,'banh2.png', 
N'Bánh xốp matcha tạo vị bùi đắng cùng với độ giòn nhẹ và vị ngọt hậu tạo sức hút mạnh liệt',1)
insert Product(namepro,price,image,description,idcate)
values (N'Bánh xốp dâu',40000,'banh3.png', 
N'Bánh xốp dâu với vị ngọt thanh với độ giòn nhẹ dễ gây nghiện',1)
insert Product(namepro,price,image,description,idcate)
values (N'Bánh dâu 2 lớp',50000,'A6.png', 
N'Cùng với lớp kem nền tạo vị ngon hỗn hợp không chối từ ',1)
insert Product(namepro,price,image,description,idcate)
values (N'Bánh lát kèm mứt dâu ',30000,'A7.png', 
N'Bánh có độ bông cùng với độ ngọt lịm của mứt tạo độ ngon miệng',1)
insert Product(namepro,price,image,description,idcate)
values (N'Bánh lát socola',35000,'A8.png', 
N'Bánh có một miếng vụn mềm và mềm hoàn hảo, hương vị socola vô cùng thơm ngon và độ ẩm tan chảy trong miệng của bạn. ',1)
insert Product(namepro,price,image,description,idcate)
values (N'Bánh lớn dâu',70000,'A9.png', 
N'Bánh cùng lớp dâu và lớp nền sữa cùng với những bánh cookies tạo sự kinh tế ',1)
insert Product(namepro,price,image,description,idcate)
values (N'Bánh lớn socola',75000,'A10.png', 
N'Bánh cùng với lớp nền socolo mềm lịm với những lớp kem tạo sự bắt mắt ',1)
insert Product(namepro,price,image,description,idcate)
values (N'Bánh mì chà bông',15000,'A11.png', 
N'Bánh cùng với lớp chà bông heo tuyệt vời ',1)
insert Product(namepro,price,image,description,idcate)
values (N'Bánh bao ',12000,'A12.png', 
N'Bánh bao trắng như ngọc trinh ',1)
insert Product(namepro,price,image,description,idcate)
values (N'Bánh trung thu nhân thập cẩm',75000,'banh5.jpg', 
N'Bánh trung thu với hoa văn đẹp cùng với nhân thập cẩm tạo vị ngon ',1)
insert Product(namepro,price,image,description,idcate)
values (N'Bánh trung thu nhân đậu xanh',75000,'A13.png', 
N'Bánh trung thu cùng với vị bùi của đậu tạo độ nghiện cao ',1)
insert Product(namepro,price,image,description,idcate)
values (N'Yogurt vanilla',15000,'A14.png', 
N'Yogurt có vị vanilla ngọt lịm , mát lạnh ',1)
insert Product(namepro,price,image,description,idcate)
values (N'Yogurt dâu',20000,'A15.png', 
N'Yogurt có vị dâu thanh mát',1)

------Breads---------
insert Product(namepro,price,image,description,idcate)
values (N'Bánh mì',5000,'A1.png', 
N'Ổ bánh mì nướng có da giòn, ruột mềm; bên trong là phần nhân.Là một loại thức ăn nhanh và bình dân dành cho buổi sáng, hoặc bất kỳ thời điểm nào trong ngày. ',2)

insert Product(namepro,price,image,description,idcate)
values (N'Bánh mì sandwich',10000,'A2.png', 
N'Bạn vẫn đang tìm kiếm một ổ bánh mì sandwich hoàn hảo với lớp vỏ mềm và độ dai vừa phải, phần nhân mềm, dai và dậy mùi thơm? Bây giờ nó ở đây.',2)


-----Pasties----------
insert Product(namepro,price,image,description,idcate)
values (N'Bánh xốp matcha',40000,'banh2.png', 
N'Bánh xốp matcha tạo vị bùi đắng cùng với độ giòn nhẹ và vị ngọt hậu tạo sức hút mạnh liệt',3)

insert Product(namepro,price,image,description,idcate)
values (N'Bánh xốp dâu',40000,'banh3.png', 
N'Bánh xốp dâu với vị ngọt thanh với độ giòn nhẹ dễ gây nghiện',3)

insert Product(namepro,price,image,description,idcate)
values (N'Bánh dâu 2 lớp',50000,'A6.png', 
N'Cùng với lớp kem nền tạo vị ngon hỗn hợp không chối từ ',3)

-------Sliced caked--------
insert Product(namepro,price,image,description,idcate)
values (N'Bánh lát kèm mứt dâu ',30000,'A7.png', 
N'Bánh có độ bông cùng với độ ngọt lịm của mứt tạo độ ngon miệng',4)

insert Product(namepro,price,image,description,idcate)
values (N'Bánh lát socola',35000,'A8.png', 
N'Bánh có một miếng vụn mềm và mềm hoàn hảo, hương vị socola vô cùng thơm ngon và độ ẩm tan chảy trong miệng của bạn. ',4)

-------Whole caked--------

insert Product(namepro,price,image,description,idcate)
values (N'Bánh lớn dâu',70000,'A9.png', 
N'Bánh cùng lớp dâu và lớp nền sữa cùng với những bánh cookies tạo sự kinh tế ',5)

insert Product(namepro,price,image,description,idcate)
values (N'Bánh lớn socola',75000,'A10.png', 
N'Bánh cùng với lớp nền socolo mềm lịm với những lớp kem tạo sự bắt mắt ',5)

-------Packaged---------

insert Product(namepro,price,image,description,idcate)
values (N'Bánh mì chà bông',15000,'A11.png', 
N'Bánh cùng với lớp chà bông heo tuyệt vời ',6)

insert Product(namepro,price,image,description,idcate)
values (N'Bánh bao ',12000,'A12.png', 
N'Bánh bao trắng như ngọc trinh ',6)

---------------------

insert Product(namepro,price,image,description,idcate)
values (N'Bánh trung thu nhân thập cẩm',75000,'banh5.jpg', 
N'Bánh trung thu với hoa văn đẹp cùng với nhân thập cẩm tạo vị ngon ',7)

insert Product(namepro,price,image,description,idcate)
values (N'Bánh trung thu nhân đậu xanh',75000,'A13.png', 
N'Bánh trung thu cùng với vị bùi của đậu tạo độ nghiện cao ',7)

------------------------

insert Product(namepro,price,image,description,idcate)
values (N'Yogurt vanilla',15000,'A14.png', 
N'Yogurt có vị vanilla ngọt lịm , mát lạnh ',8)
insert Product(namepro,price,image,description,idcate)
values (N'Yogurt dâu',20000,'A15.png', 
N'Yogurt có vị dâu thanh mát',8)

DBCC CHECKIDENT ('dbo.Product', RESEED, 0);
delete from Product
