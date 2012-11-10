ALTER TABLE [dbo].[FotografiaXArticulo]
    ADD CONSTRAINT [FK_FotografiaXArticulo_Articulo] FOREIGN KEY ([IdArticulo]) REFERENCES [dbo].[Articulo] ([IdArticulo]) ON DELETE NO ACTION ON UPDATE NO ACTION;

