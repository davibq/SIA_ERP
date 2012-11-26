CREATE TYPE [dbo].[LineaVenta] AS TABLE(
	[IdArticulo] [int] NULL,
	[IdDetalle] [int] NULL,
	[IdBodega] [int] NULL,
	[Cantidad] [int] NULL,
	[Impuesto] [decimal](8, 2) NULL
)