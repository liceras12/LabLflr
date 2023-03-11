create database LabLflr;

use [master]
go
create login [usrlab] with password=N'12345678',
	default_database=[LabLflr],
	check_expiration=off,
	check_policy=on
go
use [LabLflr]
go
create user [usrLab] for login [usrLab]
go
alter role [db_owner] add member [usrLab]

create table Serie(
	id int identity(1,1) primary key,
	titulo varchar(250),
	sinopsis varchar(5000),
	director varchar(100),
	duracion int,
	fechaEstreno date,
	usuarioRegistro varchar(12) default suser_sname(),
	registroActivo bit
);

create table Usuario(
	id int identity(1,1) primary key,
	usuario varchar(12),
	clave varchar(250),
	rol varchar(20),
	registroActivo bit
);

use LabLflr;

go

CREATE PROCEDURE paSerieListar @parametro VARCHAR(50)
AS
  SELECT id, titulo, sinopsis, director, duracion, fechaEstreno, usuarioRegistro
  FROM Serie
  WHERE registroActivo=1


use  LabLflr

insert into Usuario values('Luis', 'liceras', 'programador', 1);
