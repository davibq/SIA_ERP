ALTER TABLE [dbo].[CostoXArticuloXBodega]
    ADD CONSTRAINT [FK_CostoXArticuloXBodega_Articulo] FOREIGN KEY ([IdArticulo]) REFERENCES [dbo].[Articulo] ([IdArticulo]) ON DELETE NO ACTION ON UPDATE NO ACTION;

