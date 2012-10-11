CREATE TABLE [dbo].[FIN_Moneda](
	[IdMoneda] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](20) NOT NULL,
	[Acronimo] [varchar](3) NOT NULL,
	[EsLocal] [bit] NOT NULL,
	[EsSistema] [bit] NOT NULL,
	[idBCCR] [int] NOT NULL
);