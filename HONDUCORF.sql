USE tempdb
GO
IF EXISTS(SELECT * FROM sys.databases WHERE NAME='Honducor')
BEGIN
	DROP DATABASE Honducor
END
GO

CREATE DATABASE Honducor
GO

USE Honducor
GO

CREATE SCHEMA Persona
GO

CREATE SCHEMA Paquete
GO

CREATE TABLE Persona.Empleado
(
	idEmpleado INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Persona_Empleado_id PRIMARY KEY CLUSTERED,
	identidad VARCHAR(15)NOT NULL,
	nombre NVARCHAR(25) NOT NULL,
	apellido NVARCHAR(25) NOT NULL,
	direccion TEXT NOT NULL, 
	telefono VARCHAR(12) NOT NULL,
	fechaNac DATE NOT NULL,
	sexo VARCHAR NOT NULL,
	idCargo INT NOT NULL ,--FOREING
	estadoCivil VARCHAR(11) NOT NULL, 
)
GO

CREATE TABLE Persona.Cargo
(
	idCargo INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_Persona_Cargo_id PRIMARY KEY CLUSTERED,
	cargo VARCHAR(15) NOT NULL,
)
GO

CREATE TABLE Persona.Usuario
(	
	idUsuario INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Persona_Usuario_id PRIMARY KEY CLUSTERED,
	nombreUsuario NVARCHAR(20) NOT NULL,
	contrasenia NVARCHAR(20) NOT NULL,
	nivel VARCHAR(15) NOT NULL,
	idEmpleado INT NOT NULL --FOREING
)
GO

CREATE TABLE Paquete.Paquete
(
	idPaquete INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Paquete_Paquete_id PRIMARY KEY CLUSTERED,
	descripcion NVARCHAR(50) NOT NULL,
	noSeguimiento VARCHAR(25) NOT NULL,
	peso DECIMAL NOT NULL,
	direccion TEXT NOT NULL,
	fechaRecibido DATETIME NOT NULL DEFAULT GETDATE(),
	fechaEntregado DATE,
	idCliente INT NULL,--FOREING KEY
	idCategoria INT NOT NULL,--FOREING KEY
)
GO


CREATE TABLE Paquete.Categoria
(
	idCategoria INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Paquete_Categoria_id PRIMARY KEY CLUSTERED,
	nombreCategoria NVARCHAR(20) NOT NULL,
	descripcion VARCHAR(100)
)
GO



CREATE TABLE Persona.Cliente
(
	idCliente INT IDENTITY (1,1) NOT NULL CONSTRAINT PK_Persona_Cliente_id PRIMARY KEY CLUSTERED,
	nombre NVARCHAR(20) NOT NULL,
	identidad VARCHAR(15) NOT NULL,
	telefono VARCHAR(9),
)
GO

CREATE TABLE Paquete.Venta
(
	idVenta INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Paquete_Venta_id PRIMARY KEY CLUSTERED,
	idEmpleado INT NOT NULL,
	identidadCliente VARCHAR(15) NOT NULL,
	idPaquete INT NULL,
	nombreCompletoCliente NVARCHAR(50) NOT NULL,
	fechaVenta DATETIME DEFAULT GETDATE() NOT NULL,
	isv INT NOT NULL,
)
GO

CREATE TABLE Paquete.DetalleVenta
(
	idDetalleVenta INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Paquete_DetalleVenta_id PRIMARY KEY CLUSTERED,
	idPaquete INT NOT NULL,
	idVenta INT NOT NULL,
	precioUnidad SMALLMONEY NOT NULL,
	cantidad INT NOT NULL,
	total SMALLMONEY NOT NULL,
)
GO

--Llaves Foraneas
ALTER TABLE Persona.Empleado
	ADD CONSTRAINT FK_Persona_Empleado$Tiene$Persona_Cargo
		FOREIGN KEY (idCargo) REFERENCES Persona.Cargo(idCargo)
		ON DELETE CASCADE
GO

ALTER TABLE Persona.Usuario
	ADD CONSTRAINT FK_Persona_Usuario$Pertenece$Persona_Empleado
		FOREIGN KEY (idEmpleado) REFERENCES Persona.Empleado(idEmpleado)
		ON DELETE CASCADE
GO

--ALTER TABLE Paquete.Paquete
--	ADD CONSTRAINT FK_Paquete_Paquete$Pertenece$Persona_Cliente
--		FOREIGN KEY (idCliente) REFERENCES Persona.Cliente(idCliente)
--		ON DELETE CASCADE
--GO

ALTER TABLE Paquete.Paquete
	ADD CONSTRAINT FK_Paquete_Paquete$TieneUna$Paquete_Categoria
		FOREIGN KEY (idCategoria) REFERENCES Paquete.Categoria(idCategoria)
		ON DELETE CASCADE
GO

ALTER TABLE Paquete.Venta
	ADD CONSTRAINT FK_Paquete_Venta$TieneUn$Persona_Empleado
		FOREIGN KEY (idEmpleado) REFERENCES Persona.Empleado(idEmpleado)
		ON DELETE CASCADE
GO

ALTER TABLE Paquete.DetalleVenta
	ADD CONSTRAINT FK_Paquete_DetalleVenta$TieneUn$Paquete_Paquete
		FOREIGN KEY (idPaquete) REFERENCES Paquete.Paquete(idPaquete)
		ON DELETE CASCADE
GO

ALTER TABLE Paquete.DetalleVenta
	ADD CONSTRAINT FK_Paquete_DetalleVenta$TieneUna$Paquete_Venta
		FOREIGN KEY (idVenta) REFERENCES Paquete.Venta(idVenta)
		ON UPDATE NO ACTION
GO

/*
INSERT INTO Persona.Cargo(cargo)
VALUES('Administrador'),
('Empleado')
go

INSERT INTO Persona.Empleado(identidad,nombre,apellido,direccion,telefono,fechaNac,sexo,idCargo,estadoCivil)
VALUES('0318199900710','Alee','Mayorga','Mas alla','97813880','24-06-1997','M',1,'Soltero')
GO

INSERT INTO Persona.Usuario(nombreUsuario,contrasenia,nivel,idEmpleado)
VALUES('Alee','Alee','Administrador',1)
GO
INSERT INTO Persona.Empleado(identidad,nombre,apellido,direccion,telefono,fechaNac,sexo,idCargo,estadoCivil)
VALUES('10071999','Danny','Cantarero','Por ahi','96198924','05-06-1999','M',1,'Solito :(')
GO

INSERT INTO Persona.Usuario(nombreUsuario,contrasenia,nivel,idEmpleado)
VALUES('Danny','danny13','Administrador',1)
GO
INSERT INTO Persona.Empleado(identidad,nombre,apellido,direccion,telefono,fechaNac,sexo,idCargo,estadoCivil)
VALUES('16191998','Mario','Argueta','Ave Siempre viva','99560206','11-09-1999','M',1,'Solterito')
GO

INSERT INTO Persona.Usuario(nombreUsuario,contrasenia,nivel,idEmpleado)
VALUES('Mario','Mario11','Administrador',2)
GO
=============================================================================================================
========================================INSERATAR PAQUETES===================================================
INSERT INTO Paquete.Categoria(nombreCategoria, descripcion)
VALUES('Pesado','Paquetes Grandes'),
	  ('Peque','Paquete Peque')
go

INSERT INTO Persona.Cliente(nombre, identidad,telefono)
VALUES('Carmelo','0656562','965622'),
	  ('Carmela','555666', '95953')
go

SELECT * FROM Paquete.Paquete
INSERT INTO Paquete.Paquete(descripcion,noSeguimiento,peso,direccion,idCliente,idCategoria)
VALUES('Carmelo','15646','15','Los Angeles',1,1),
	  ('Carmela','2511', '25','New York',2,2)
go

SELECT * FROM Persona.Usuario
GO

SELECT * FROM Persona.Cliente
GO
*/