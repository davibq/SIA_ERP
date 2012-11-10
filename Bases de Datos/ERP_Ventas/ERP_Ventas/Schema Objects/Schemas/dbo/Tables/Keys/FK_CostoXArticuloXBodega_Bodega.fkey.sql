ALTER TABLE [dbo].[CostoXArticuloXBodega]
    ADD CONSTRAINT [FK_CostoXArticuloXBodega_Bodega] FOREIGN KEY ([IdBodega]) REFERENCES [dbo].[Bodega] ([IdBodega]) ON DELETE NO ACTION ON UPDATE NO ACTION;

