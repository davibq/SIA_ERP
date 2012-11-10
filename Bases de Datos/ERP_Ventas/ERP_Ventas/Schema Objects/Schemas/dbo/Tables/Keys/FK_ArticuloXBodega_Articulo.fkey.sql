ALTER TABLE [dbo].[ArticuloXBodega]
    ADD CONSTRAINT [FK_ArticuloXBodega_Articulo] FOREIGN KEY ([IdArticulo]) REFERENCES [dbo].[Articulo] ([IdArticulo]) ON DELETE NO ACTION ON UPDATE NO ACTION;

