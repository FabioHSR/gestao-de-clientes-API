CREATE DATABASE FATestDB
USE [FATestDB]
CREATE TABLE tTiposCliente(
    ID        INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Descricao NVARCHAR(100)     NOT NULL,
)

CREATE TABLE tSituacoesCliente(
    ID        INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Descricao VARCHAR(10)       NOT NULL
)

CREATE TABLE tClientes(
	ID		          INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Nome              NVARCHAR(50)      NOT NULL,
    CPF               VARCHAR(11)       NOT NULL UNIQUE,
    TipoClienteID     INT               DEFAULT 1,
    Sexo              CHAR(1)           NULL,
	SituacaoClienteID INT               DEFAULT 1,

	FOREIGN KEY (TipoClienteID)     REFERENCES tTiposCliente(ID),
	FOREIGN KEY (SituacaoClienteID) REFERENCES tSituacoesCliente(ID)
)

INSERT INTO tTiposCliente VALUES ('PADRÃO')
INSERT INTO tTiposCliente VALUES ('VIP')

INSERT INTO tSituacoesCliente VALUES ('ATIVO')
INSERT INTO tSituacoesCliente VALUES ('INATIVO')

GO
CREATE PROCEDURE spInsertClientes
    @Nome              NVARCHAR(50),      
    @CPF               VARCHAR(11),       
    @TipoClienteID     INT     = 1,               
    @Sexo              CHAR(1) = NULL,           
	@SituacaoClienteID INT     = 1               
AS
BEGIN 
 INSERT INTO tClientes (Nome,CPF,TipoClienteID,Sexo,SituacaoClienteID) 
 OUTPUT 
	Inserted.ID,
	Inserted.Nome,
	Inserted.CPF,   
	Inserted.TipoClienteID,               
	Inserted.Sexo,                        
	Inserted.SituacaoClienteID 
 VALUES (@Nome,@CPF,@TipoClienteID,@Sexo,@SituacaoClienteID)
END

GO
CREATE PROCEDURE spUpdateClientes
	@ID                INT,
    @Nome              NVARCHAR(50) = NULL,      
    @CPF               VARCHAR(11)  = NULL,       
    @TipoClienteID     INT          = NULL,                 
    @Sexo              CHAR(1)      = NULL,           
	@SituacaoClienteID INT          = NULL             
AS
BEGIN 
 UPDATE tClientes
 SET 
 Nome              = CASE WHEN @Nome              IS NOT NULL THEN @Nome              ELSE   Nome              END,
 CPF               = CASE WHEN @CPF               IS NOT NULL THEN @CPF               ELSE   CPF               END,
 TipoClienteID     = CASE WHEN @TipoClienteID     IS NOT NULL THEN @TipoClienteID     ELSE   TipoClienteID     END,
 Sexo              = CASE WHEN @Sexo              IS NOT NULL THEN @Sexo              ELSE   Sexo              END,
 SituacaoClienteID = CASE WHEN @SituacaoClienteID IS NOT NULL THEN @SituacaoClienteID ELSE   SituacaoClienteID END
 OUTPUT 
	Inserted.ID,
	Inserted.Nome,
	Inserted.CPF,   
	Inserted.TipoClienteID,               
	Inserted.Sexo,                        
	Inserted.SituacaoClienteID 
 FROM tClientes
 WHERE ID = @ID
END

GO
CREATE PROCEDURE spGetClientes       
AS
BEGIN 
 SELECT * FROM tClientes
END

GO
CREATE PROCEDURE spGetClientesByID  
@ID INT
AS
BEGIN 
 SELECT * FROM tClientes WHERE ID =  @ID
END

GO
CREATE PROCEDURE spDeleteClientesByID  
@ID INT
AS
BEGIN 
 DELETE FROM tClientes WHERE ID =  @ID
END

GO
CREATE PROCEDURE spUniqueClientesCPF  
@CPF VARCHAR(11) 
AS
BEGIN 
 SELECT COUNT(ID) COUNT FROM tClientes WHERE CPF =  @CPF
END
--PROCEDURES tTiposCliente

GO
CREATE PROCEDURE spGetTiposClienteByID
@ID INT
AS
BEGIN 
 SELECT * FROM tTiposCliente WHERE ID =  @ID
END

--PROCEDURES tSituacoesCliente

GO
CREATE PROCEDURE spGetSituacoesClienteByID
@ID INT
AS
BEGIN 
 SELECT * FROM tSituacoesCliente WHERE ID =  @ID
END
