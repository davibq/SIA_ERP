CREATE TABLE [dbo].[SocioNegocio] (
    [IdSocioNegocio] INT          IDENTITY (1, 1) NOT NULL,
    [Codigo]         VARCHAR (15) NOT NULL,
    [Nombre]         VARCHAR (40) NOT NULL,
	[Email]			 VARCHAR (40) NULL,
    [IdTipoSocio]    INT          NOT NULL,
	[IDMoneda]		 INT		  NOT NULL,
	[_Cuenta]		 VARCHAR (15) NOT NULL,
	[LimiteCredito]  DECIMAL(10,2) NULL

	 CONSTRAINT [UQ_Codigo_SocioNegocio] UNIQUE NONCLUSTERED 
(
	[Codigo] ASC
) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
);

