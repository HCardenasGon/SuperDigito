CREATE DATABASE SuperDigito
USE SuperDigito

--Scaffold-DbContext "Server=.; Database= SuperDigito; TrustServerCertificate=True; User ID=sa; Password=pass@word1;" Microsoft.EntityFrameworkCore.SqlServer -f

CREATE TABLE Usuario
(
	IdUsuario INT IDENTITY(1,1) CONSTRAINT PK_Usuario PRIMARY KEY,
	Username VARCHAR(50) NOT NULL UNIQUE,
	Password VARBINARY(20) NOT NULL
)
GO

ALTER PROCEDURE UsuarioByUsername
@Username VARCHAR(50),
@Password VARBINARY(20)
AS
SELECT	Usuario.IdUsuario,
		Usuario.UserName,
		Usuario.Password
FROM Usuario
WHERE Usuario.Username = @Username AND Usuario.Password = @Password
GO

CREATE PROCEDURE UsuarioAdd
@UserName VARCHAR(50),
@Password VARBINARY(20)
AS
INSERT INTO Usuario
(
	UserName,
	Password
)
VALUES
(
	@UserName,
	@Password
)
GO

CREATE TABLE Historial
(
	IdHistorial INT IDENTITY(1,1) CONSTRAINT PK_Historial PRIMARY KEY,
	Numero INT NOT NULL ,
	Resultado INT NOT NULL,
	FechaHora DATETIME,
	IdUsuario INT NOT NULL CONSTRAINT FK_Historial
	FOREIGN KEY (IdUsuario)
	REFERENCES Usuario(IdUsuario)
)
GO

CREATE PROCEDURE HistorialAdd
@Numero INT,
@Resultado INT,
@IdUsuario INT
AS
INSERT INTO Historial
(
	Numero,
	Resultado,
	FechaHora,
	IdUsuario
)
VALUES
(
	@Numero,
	@Resultado,
	GETDATE(),
	@IdUsuario
)
GO

CREATE PROCEDURE HistorialDelete
@IdUsuario INT
AS
DELETE FROM Historial
WHERE IdUsuario = @IdUsuario
GO

ALTER PROCEDURE HistorialUpdate
@Numero INT,
@IdUsuario INT
AS
UPDATE Historial
SET FechaHora = GETDATE()
WHERE Numero = @Numero AND IdUsuario = @IdUsuario
GO

ALTER PROCEDURE HistorialGetAllById
@IdUsuario INT
AS
SELECT	Historial.IdHistorial,
		Historial.Numero,
		Historial.Resultado,
		Historial.FechaHora,
		Historial.IdUsuario
FROM Historial
WHERE IdUsuario = @IdUsuario
ORDER BY FechaHora DESC
Go

CREATE PROCEDURE HistorialFindNumero
@Numero INT,
@IdUsuario INT
AS
SELECT	Historial.IdHistorial,
		Historial.Numero,
		Historial.Resultado,
		Historial.FechaHora,
		Historial.IdUsuario
FROM Historial
WHERE IdUsuario = @IdUsuario AND Numero = @Numero
Go