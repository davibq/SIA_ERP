ALTER TABLE [dbo].[ArticulosXDetalle]  WITH CHECK ADD  CONSTRAINT [FK_ArticulosXDetalle_Bodega] FOREIGN KEY([IdArticulo], [IdBodega])
REFERENCES [dbo].[ArticuloXBodega] ([IdArticulo], [IdBodega])