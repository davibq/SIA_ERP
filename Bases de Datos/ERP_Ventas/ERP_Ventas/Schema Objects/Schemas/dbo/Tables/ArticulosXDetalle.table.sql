CREATE TABLE [dbo].[ArticulosXDetalle] (
    [IdArticulo] INT            NOT NULL,
    [IdDetalle]  INT            NOT NULL,
	[IdBodega]	 INT			NULL,
    [Cantidad]   INT            NOT NULL,
    [Impuesto]   DECIMAL (8, 2) NOT NULL,
	[Precio]	 DECIMAL(12, 2) NULL
);

