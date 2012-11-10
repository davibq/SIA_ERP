CREATE TABLE [dbo].[FotografiaXArticulo] (
    [IdFotografia] INT             IDENTITY (1, 1) NOT NULL,
    [Fotografia]   VARBINARY (MAX) NOT NULL,
    [IdArticulo]   INT             NOT NULL
);

