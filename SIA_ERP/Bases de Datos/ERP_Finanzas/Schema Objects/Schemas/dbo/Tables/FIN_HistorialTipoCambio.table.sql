CREATE TABLE [dbo].[FIN_HistorialTipoCambio] (
    [IdMonedaOrigen]  INT             NOT NULL,
    [IdMonedaDestino] INT             NOT NULL,
    [Fecha]           DATETIME        NOT NULL,
    [TipoCambio]      DECIMAL (14, 2) NOT NULL
);

