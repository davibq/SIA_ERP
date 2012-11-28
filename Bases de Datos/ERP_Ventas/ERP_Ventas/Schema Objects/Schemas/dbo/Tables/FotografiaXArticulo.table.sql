CREATE TABLE [dbo].[FotografiaXArticulo] (
    [IdFotografia] INT             IDENTITY (1, 1) NOT NULL,
	[UrlFotografia] VARCHAR(500)   NOT NULL, 
    [IdArticulo]   INT             NOT NULL
);

