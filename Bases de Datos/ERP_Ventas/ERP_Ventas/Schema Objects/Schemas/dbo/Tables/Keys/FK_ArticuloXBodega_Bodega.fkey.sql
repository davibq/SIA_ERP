ALTER TABLE [dbo].[ArticuloXBodega]
    ADD CONSTRAINT [FK_ArticuloXBodega_Bodega] FOREIGN KEY ([IdBodega]) REFERENCES [dbo].[Bodega] ([IdBodega]) ON DELETE NO ACTION ON UPDATE NO ACTION;

