CREATE TABLE [dbo].[ArticuloXBodega](
	[IdArticulo] [int] NOT NULL,
	[IdBodega] [int] NOT NULL,
	[Stock] [int] NOT NULL,
	[Comprometido] [int] NOT NULL,
	[Solicitado] [int] NOT NULL,
	[codCuentasExistencia] [varchar](25) NOT NULL,
	[codCuentasVentas] [varchar](25) NOT NULL,
	[codCuentasCostos] [varchar](25) NOT NULL,
	[codCuentaTransitoria] [varchar](25) NULL
);

