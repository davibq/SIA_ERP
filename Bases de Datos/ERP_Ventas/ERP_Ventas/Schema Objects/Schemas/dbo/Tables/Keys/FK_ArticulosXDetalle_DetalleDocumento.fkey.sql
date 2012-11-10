ALTER TABLE [dbo].[ArticulosXDetalle]
    ADD CONSTRAINT [FK_ArticulosXDetalle_DetalleDocumento] FOREIGN KEY ([IdDetalle]) REFERENCES [dbo].[DetalleDocumento] ([IdDetalle]) ON DELETE NO ACTION ON UPDATE NO ACTION;

