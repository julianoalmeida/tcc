-- run this script before start application

use master
GO

drop database unip_tcc
GO

create database unip_tcc
GO

USE [unip_tcc]
GO


CREATE TABLE PAIS(
 Id int not null primary key identity (1,1),
 Descricao varchar(100) not null
);

CREATE TABLE ESTADO(
	Id char(2) NOT NULL primary key,
	Descricao varchar(120) NULL,
	IdPais int not null foreign key References PAIS(Id)
);

CREATE TABLE CIDADE(
	Id int NOT NULL primary key identity (1,1),
	SiglaEstado char(2) NOT NULL foreign key References ESTADO(Id),
	Descricao varchar(100) NULL
);

CREATE TABLE ENDERECO(
	Id int IDENTITY(1,1) NOT NULL primary key ,
	IdCidade int NOT NULL foreign key References CIDADE (Id),
	Cep varchar(10) NOT NULL,
	Logradouro varchar(100) NOT NULL,
	Bairro varchar(100) NOT NULL,
	NumeroCasa varchar(10) NOT NULL,
	Complemento varchar(100) NULL
);

CREATE TABLE Pessoa(
	Id int IDENTITY(1,1) NOT NULL primary key,
	IdEndereco int NULL foreign key  references Endereco(Id),
	Nome varchar(255) NOT NULL,
	Cpf char(11) NULL,
	Sexo int NULL,
	DataNascimento [date] NULL,
	Email varchar(100) NULL,
	Telefone varchar(12) NULL,
	Celular varchar(12) NULL,	
	EstadoCivil int NOT NULL	
);

CREATE TABLE ADMINISTRADOR(
	Id int IDENTITY (1,1) NOT NULL primary key,
	IdPessoa int NOT NULL foreign key References Pessoa(Id)	
);

CREATE TABLE DISCENTE(
	Id int IDENTITY(1,1) NOT NULL primary key,
	IdPessoa int NOT NULL foreign key References Pessoa(Id),
	NumeroMatricula varchar(20) NULL,
	IdEscolaridade int NOT NULL
);

CREATE TABLE DOCENTE(
	Id int IDENTITY(1,1) NOT NULL primary key,
	IdPessoa int NOT NULL foreign key References Pessoa(Id),
	IdEscolaridade int NOT NULL	
);

CREATE TABLE DISCIPLINA(
	Id int IDENTITY(1,1) NOT NULL primary key,
	Descricao varchar(50) NOT NULL
);

CREATE TABLE DOCENTE_DISCIPLINA(
	IdDocente int NOT NULL Foreign Key References Docente(Id),
	IdDisciplina int NOT NULL Foreign Key References Disciplina(Id),
	Primary key (IdDocente , IdDisciplina)
);

CREATE TABLE MATERIAL_APOIO(
	Id int IDENTITY(1,1) NOT NULL primary key,
	Titulo varchar(100) NOT NULL,
	Descricao varchar(255) NOT NULL,
	IdDocente int NOT NULL foreign key References Docente(Id)
);

CREATE TABLE MATERIAL_APOIO_ARQUIVO(
	Id int IDENTITY(1,1) NOT NULL primary key,
	IdMaterialApoio int NOT NULL foreign key references Material_Apoio(Id),
	NomeArquivo varchar(225) NOT NULL,
	Arquivo varbinary(max) NOT NULL
);

CREATE TABLE TURMA(
	Id int IDENTITY(1,1) NOT NULL primary key,
	Descricao varchar(100) NOT NULL,
	Capacidade int NOT NULL,
	DataInicio date NOT NULL,
	DataFim date NOT NULL,
	IsAtivo [bit] NOT NULL,
	Turno int NULL
);

CREATE TABLE TURMA_DOCENTE(
	IdTurma int NOT NULL foreign key references TURMA(Id),
	IdDocente int NOT NULL foreign key references DOCENTE(Id),
	Primary Key (IdTurma , IdDocente)
);

CREATE TABLE TURMA_DISCENTE_NOTA(
	IdTurma int NOT NULL foreign key References TURMA(Id),
	IdDiscente int NOT NULL foreign key References DISCENTE(Id),
	TipoNota int NOT NULL,
	Nota decimal(18, 2) NULL,
	Primary Key (IdTurma , IdDiscente)
);

CREATE TABLE TURMA_DISCENTE_MEDIA(
	IdTurma int NOT NULL foreign key References TURMA(Id),
	IdDiscente int NOT NULL foreign key References DISCENTE(Id),
	Media decimal(18, 2) NULL,
	Primary Key (IdTurma , IdDiscente)
);

CREATE TABLE TURMA_DISCENTE(
	IdTurma int NOT NULL foreign key references TURMA(Id),
	IdDiscente int NOT NULL foreign key references DISCENTE(Id),
	Primary Key (IdTurma , IdDiscente)
);

CREATE TABLE MATERIAL_APOIO_TURMA(
	IdTurma int NOT NULL foreign key References TURMA(Id),
	IdMAterialApoio int NOT NULL foreign Key References MATERIAL_APOIO(Id),
	DataLiberacao date NOT NULL,
	primary key (IdTurma , IdMaterialApoio)
);

CREATE TABLE USUARIO(	
	Id int primary key identity(1,1),
	Login varchar(8) NOT NULL,
	Senha varchar(8) NOT NULL,
	IdPerfil int NOT NULL	
);

CREATE TABLE FREQUENCIA_DATA(
	Id int IDENTITY(1,1) NOT NULL primary key,
	Data date NOT NULL,
	MotivoFalta varchar(255) NULL	
);

CREATE TABLE FREQUENCIA(
	IdTurma int NOT NULL foreign key references Turma(Id),
	IdDiscente int NOT NULL foreign key references Discente(Id),
	IdFrequenciaData int NOT NULL foreign key references FREQUENCIA_DATA(Id),
	IsAusente bit NOT NULL,
	primary key (IdTurma , IdDiscente , IdFrequenciaData)
);