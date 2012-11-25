CREATE TABLE [dbo].[EncabezadoDocumento] (
    [IdEncabezado]    INT          IDENTITY (1, 1) NOT NULL,
    [Fecha]           DATETIME     NOT NULL,
	[Fecha2]           DATETIME     NULL,
    [CodigoDocumento] VARCHAR (20) NULL
);

