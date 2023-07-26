-- DROP SCHEMA dbo;

CREATE SCHEMA dbo;
-- Fabbisogno_personale.dbo.PERS_FABBISOGNO_BCK_2022 definition

-- Drop table

-- DROP TABLE Fabbisogno_personale.dbo.PERS_FABBISOGNO_BCK_2022;

CREATE TABLE Fabbisogno_personale.dbo.PERS_FABBISOGNO_BCK_2022 (
	ID int NOT NULL,
	DIREZIONE varchar(255) COLLATE Latin1_General_CI_AS NOT NULL,
	ANNO int NOT NULL,
	CATEGORIA varchar(10) COLLATE Latin1_General_CI_AS NOT NULL,
	PROFILO_ID int NOT NULL,
	MANSIONE_ID int NOT NULL,
	PROFILO_FORMATIVO nvarchar(100) COLLATE Latin1_General_CI_AS NOT NULL,
	UNITA int NOT NULL,
	STATO_ID int NOT NULL,
	DATA_INS date NOT NULL,
	UTENTE_INS varchar(50) COLLATE Latin1_General_CI_AS NOT NULL,
	DATA_CONF date NULL,
	UTENTE_CONF varchar(50) COLLATE Latin1_General_CI_AS NULL,
	DATA_ANN date NULL,
	UTENTE_ANN varchar(50) COLLATE Latin1_General_CI_AS NULL,
	MOTIVO_RICHIESTA_ID int NOT NULL,
	DATA_ANALIZZATO datetime NULL,
	UTENTE_ANALIZZATO varchar(100) COLLATE Latin1_General_CI_AS NULL,
	Annotazioni nvarchar(255) COLLATE Latin1_General_CI_AS NULL,
	CONSTRAINT PERS_FABBISOGNO_BCK_2022_PK PRIMARY KEY (ID)
);


-- Fabbisogno_personale.dbo.PERS_MANSIONE definition

-- Drop table

-- DROP TABLE Fabbisogno_personale.dbo.PERS_MANSIONE;

CREATE TABLE Fabbisogno_personale.dbo.PERS_MANSIONE (
	ID int IDENTITY(1,1) NOT NULL,
	NOME varchar(50) COLLATE Latin1_General_CI_AS NOT NULL,
	CONSTRAINT PERS_MANSIONE_PK PRIMARY KEY (ID)
);


-- Fabbisogno_personale.dbo.PERS_MOTIVO_RICHIESTA definition

-- Drop table

-- DROP TABLE Fabbisogno_personale.dbo.PERS_MOTIVO_RICHIESTA;

CREATE TABLE Fabbisogno_personale.dbo.PERS_MOTIVO_RICHIESTA (
	ID int IDENTITY(1,1) NOT NULL,
	NOME varchar(255) COLLATE Latin1_General_CI_AS NOT NULL,
	CONSTRAINT PERS_MOTIVO_RICHIESTA_PK PRIMARY KEY (ID)
);


-- Fabbisogno_personale.dbo.PERS_PROFILO definition

-- Drop table

-- DROP TABLE Fabbisogno_personale.dbo.PERS_PROFILO;

CREATE TABLE Fabbisogno_personale.dbo.PERS_PROFILO (
	ID int IDENTITY(1,1) NOT NULL,
	NOME varchar(255) COLLATE Latin1_General_CI_AS NOT NULL,
	CATEGORIA varchar(10) COLLATE Latin1_General_CI_AS NOT NULL,
	CONSTRAINT PERS_PROFILO_PK PRIMARY KEY (ID)
);


-- Fabbisogno_personale.dbo.PERS_UTENTE definition

-- Drop table

-- DROP TABLE Fabbisogno_personale.dbo.PERS_UTENTE;

CREATE TABLE Fabbisogno_personale.dbo.PERS_UTENTE (
	ID int IDENTITY(1,1) NOT NULL,
	MATRICOLA varchar(25) COLLATE Latin1_General_CI_AS NOT NULL,
	DIREZIONE varchar(100) COLLATE Latin1_General_CI_AS NOT NULL,
	NOME varchar(50) COLLATE Latin1_General_CI_AS NOT NULL,
	COGNOME varchar(50) COLLATE Latin1_General_CI_AS NOT NULL,
	RUOLO int DEFAULT 2 NOT NULL,
	ATTIVO int DEFAULT '1' NOT NULL,
	PASSWORD binary NULL,
	CONSTRAINT PERS_UTENTE_PK PRIMARY KEY (ID),
	CONSTRAINT PERS_UTENTE_UN UNIQUE (MATRICOLA)
);
CREATE UNIQUE NONCLUSTERED INDEX PERS_UTENTE_UN ON Fabbisogno_personale.dbo.PERS_UTENTE (MATRICOLA);


-- Fabbisogno_personale.dbo.sysdiagrams definition

-- Drop table

-- DROP TABLE Fabbisogno_personale.dbo.sysdiagrams;

CREATE TABLE Fabbisogno_personale.dbo.sysdiagrams (
	name sysname COLLATE Latin1_General_CI_AS NOT NULL,
	principal_id int NOT NULL,
	diagram_id int IDENTITY(1,1) NOT NULL,
	version int NULL,
	definition varbinary(MAX) NULL,
	CONSTRAINT PK__sysdiagr__C2B05B615D83E51C PRIMARY KEY (diagram_id),
	CONSTRAINT UK_principal_name UNIQUE (principal_id,name)
);
CREATE UNIQUE NONCLUSTERED INDEX UK_principal_name ON Fabbisogno_personale.dbo.sysdiagrams (principal_id, name);


-- Fabbisogno_personale.dbo.PERS_FABBISOGNO definition

-- Drop table

-- DROP TABLE Fabbisogno_personale.dbo.PERS_FABBISOGNO;

CREATE TABLE Fabbisogno_personale.dbo.PERS_FABBISOGNO (
	ID int IDENTITY(1,1) NOT NULL,
	DIREZIONE varchar(255) COLLATE Latin1_General_CI_AS NOT NULL,
	ANNO int NOT NULL,
	CATEGORIA varchar(10) COLLATE Latin1_General_CI_AS NOT NULL,
	PROFILO_ID int NOT NULL,
	MANSIONE_ID int NOT NULL,
	PROFILO_FORMATIVO nvarchar(100) COLLATE Latin1_General_CI_AS NOT NULL,
	UNITA int NOT NULL,
	STATO_ID int NOT NULL,
	DATA_INS date NOT NULL,
	UTENTE_INS varchar(50) COLLATE Latin1_General_CI_AS NOT NULL,
	DATA_CONF date NULL,
	UTENTE_CONF varchar(50) COLLATE Latin1_General_CI_AS NULL,
	DATA_ANN date NULL,
	UTENTE_ANN varchar(50) COLLATE Latin1_General_CI_AS NULL,
	MOTIVO_RICHIESTA_ID int NOT NULL,
	DATA_ANALIZZATO datetime NULL,
	UTENTE_ANALIZZATO varchar(100) COLLATE Latin1_General_CI_AS NULL,
	Annotazioni nvarchar(255) COLLATE Latin1_General_CI_AS NULL,
	CONSTRAINT PERS_FABBISOGNO_PK PRIMARY KEY (ID),
	CONSTRAINT FK_FABB_MANSIONE FOREIGN KEY (MANSIONE_ID) REFERENCES Fabbisogno_personale.dbo.PERS_MANSIONE(ID) ON UPDATE CASCADE,
	CONSTRAINT FK_FABB_MOTIVO_RICHIESTA FOREIGN KEY (MOTIVO_RICHIESTA_ID) REFERENCES Fabbisogno_personale.dbo.PERS_MOTIVO_RICHIESTA(ID) ON UPDATE CASCADE,
	CONSTRAINT FK_FABB_PROFILO FOREIGN KEY (PROFILO_ID) REFERENCES Fabbisogno_personale.dbo.PERS_PROFILO(ID) ON UPDATE CASCADE
);
