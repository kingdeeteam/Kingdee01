create table Atable(
id int primary key,
password varchar(255),
textX varchar(255),
textY varchar(255),
textZ varchar(255)
);

create table Btable(
id int primary key,
password varchar(255),
textM varchar(255),
textN varchar(255),
textZ varchar(255)
);

insert into Atable values(1101,'abc','X01','Y01','Z01');
insert into Atable values(1102,'abc','X02','Y02','Z02');
insert into Atable values(1103,'abc','X03','Y03','Z03');
insert into Atable values(1104,'abc','X04','Y04','Z04');
insert into Atable values(1105,'abc','X05','Y05','Z05');
insert into Atable values(1106,'abc','X05','Y05','Z07');

insert into Btable values(1101,'abc','M01','N01','Z01');
insert into Btable values(1102,'abc','M02','N02','Z02');
insert into Btable values(1103,'abc','M03','N03','Z03');
insert into Btable values(1104,'abc','M04','N04','Z04');
insert into Btable values(1105,'abc','M05','N05','Z05');
insert into Btable values(1106,'abc','M05','N05','Z05');
insert into Btable values(1108,'abc','M05','N05','Z09');