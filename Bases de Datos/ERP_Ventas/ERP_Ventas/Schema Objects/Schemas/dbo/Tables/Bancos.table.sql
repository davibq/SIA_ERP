CREATE TABLE [dbo].[Bancos]
(
	[idBanco]		int			IDENTITY(1,1) NOT NULL,
	[Nombre]		varchar(50) NOT NULL,
	[Moneda]		varchar(3)	NOT NULL,
	[NoCuenta]		varchar(25) NOT NULL,
	[CuentaMayor]	varchar(25) NOT NULL
);
