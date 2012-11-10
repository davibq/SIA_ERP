CREATE TABLE [dbo].[ComentariosXArticulo] (
    [IdComentario] INT           IDENTITY (1, 1) NOT NULL,
    [IdArticulo]   INT           NOT NULL,
    [Comentario]   VARCHAR (150) NOT NULL
);

