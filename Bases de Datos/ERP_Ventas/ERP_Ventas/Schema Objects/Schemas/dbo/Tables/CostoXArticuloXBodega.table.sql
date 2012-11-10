CREATE TABLE [dbo].[CostoXArticuloXBodega] (
    [IdArticulo]         INT             NOT NULL,
    [IdBodega]           INT             NOT NULL,
    [Costo]              DECIMAL (12, 2) NOT NULL,
    [FechaActualizacion] DATETIME        NOT NULL
);

