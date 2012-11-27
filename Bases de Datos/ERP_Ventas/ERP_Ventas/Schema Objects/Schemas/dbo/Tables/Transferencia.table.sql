CREATE TABLE [dbo].[Transferencia] (
    [IdTransferencia]     INT             IDENTITY (1, 1) NOT NULL,
    [IdSocioNegocio]      INT             NOT NULL,
    [IdTipoTransferencia] INT             NOT NULL,
    [NumeroTransferencia] VARCHAR (25)    NOT NULL,
    [CodigoCuenta]        VARCHAR (25)    NOT NULL,
    [Monto]               DECIMAL (12, 2) NOT NULL,
	[idBanco]			  int			  NOT NULL,
	[Fecha]				  date			  NOT NULL
);