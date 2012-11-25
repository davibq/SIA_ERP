CREATE TABLE [dbo].[Articulo] (
    [IdArticulo]     INT          IDENTITY (1, 1) NOT NULL,
    [Codigo]         VARCHAR (20) NOT NULL,
	[Nombre]         VARCHAR (40) NOT NULL,
    [Descripcion]    VARCHAR (75) NULL,
    [IdUnidadMedida] INT          NOT NULL
);

