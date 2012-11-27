CREATE TABLE [dbo].[SocioNegocio] (
    [IdSocioNegocio] INT          IDENTITY (1, 1) NOT NULL,
    [Codigo]         VARCHAR (15) NOT NULL,
    [Nombre]         VARCHAR (40) NOT NULL,
    [IdTipoSocio]    INT          NOT NULL,
	[IDMoneda]		 INT		  NOT NULL,
	[_Cuenta]		 VARCHAR (15) NOT NULL,
	[LimiteCredito]  DECIMAL(10,2) NULL
);

