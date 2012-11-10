ALTER TABLE [dbo].[ComentariosXArticulo]
    ADD CONSTRAINT [FK_ComentariosXArticulo_Articulo] FOREIGN KEY ([IdArticulo]) REFERENCES [dbo].[Articulo] ([IdArticulo]) ON DELETE NO ACTION ON UPDATE NO ACTION;

