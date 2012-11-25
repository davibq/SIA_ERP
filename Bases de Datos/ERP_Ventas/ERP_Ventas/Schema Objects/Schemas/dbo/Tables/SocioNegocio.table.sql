﻿CREATE TABLE [dbo].[SocioNegocio] (
    [IdSocioNegocio] INT          IDENTITY (1, 1) NOT NULL,
    [Codigo]         VARCHAR (15) NOT NULL,
    [Nombre]         VARCHAR (40) NOT NULL,
    [IdTipoSocio]    INT          NOT NULL,
	[CodigoAsiento]	 VARCHAR(20) NOT NULL,
	[IDMoneda]		 INT		  NOT NULL,
	[_Cuenta]		 VARCHAR (15) NOT NULL
);

