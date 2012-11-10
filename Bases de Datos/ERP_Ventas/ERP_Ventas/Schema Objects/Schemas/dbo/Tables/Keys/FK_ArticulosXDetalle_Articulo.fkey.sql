ALTER TABLE [dbo].[ArticulosXDetalle]
    ADD CONSTRAINT [FK_ArticulosXDetalle_Articulo] FOREIGN KEY ([IdArticulo]) REFERENCES [dbo].[Articulo] ([IdArticulo]) ON DELETE NO ACTION ON UPDATE NO ACTION;

