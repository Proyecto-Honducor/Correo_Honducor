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
	fechaNac DATETIME NOT NULL,
	sexo CHAR NOT NULL,
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
	fechaEntregado DATETIME,
	idCliente INT NOT NULL,--FOREING KEY
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
	idCliente INT NOT NULL,
	fechaVenta DATETIME NOT NULL,
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

ALTER TABLE Paquete.Paquete
	ADD CONSTRAINT FK_Paquete_Paquete$Pertenece$Persona_Cliente
		FOREIGN KEY (idCliente) REFERENCES Persona.Cliente(idCliente)
		ON DELETE CASCADE
GO

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

ALTER TABLE Paquete.Venta
	ADD CONSTRAINT FK_Paquete_Venta$TieneUn$Persona_Cliente
		FOREIGN KEY (idCliente) REFERENCES Persona.Cliente(idCliente)
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


