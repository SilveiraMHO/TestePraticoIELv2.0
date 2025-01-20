
-- 1º) Cire o banco no SSMS rodando o comando 'create database' abaixo.
--===============================================================================================
-- CREATE DATABASE CadWebDB;
--===============================================================================================

-- 2º) Antes de rodar todo o script para a criação das tabelas, selecione o banco 'CadWebDB'.
--===============================================================================================
CREATE TABLE cad_app_user (
	[id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[usuario] NVARCHAR(20) NOT NULL,
	[senha_hash] NVARCHAR(100) NOT NULL,
    [salt] NVARCHAR(50) NULL,
	[data_criacao] DATETIME NOT NULL DEFAULT GETDATE()
);


CREATE TABLE dom_estado (
	[id] INT NOT NULL PRIMARY KEY,
	[nome] NVARCHAR(50) NOT NULL,
	[UF] NCHAR(2) NOT NULL
);


CREATE TABLE dom_instituicao_ensino (
	[id] INT NOT NULL PRIMARY KEY,
	[nome] NVARCHAR(100) NOT NULL,
);


CREATE TABLE cad_estudante (
	[cpf] NCHAR(11) NOT NULL PRIMARY KEY,
	[nome] NVARCHAR(100) NOT NULL,
	[endereco] NVARCHAR(200) NOT NULL,
	[id_estado] INT NOT NULL,
	[cidade] NVARCHAR(50) NOT NULL,
	[nome_curso] NVARCHAR(100) NOT NULL,
	[data_conclusao] DATETIME NOT NULL,
	[id_instituicao_ensino] INT NOT NULL
);
ALTER TABLE [dbo].[cad_estudante] ADD CONSTRAINT FK_cad_estudante_dom_estado
	FOREIGN KEY ([id_estado]) REFERENCES [dbo].[dom_estado]([id]);
ALTER TABLE [dbo].[cad_estudante] ADD CONSTRAINT FK_cad_estudante_dom_instituicao_ensino
	FOREIGN KEY ([id_instituicao_ensino]) REFERENCES [dbo].[dom_instituicao_ensino]([id]);
