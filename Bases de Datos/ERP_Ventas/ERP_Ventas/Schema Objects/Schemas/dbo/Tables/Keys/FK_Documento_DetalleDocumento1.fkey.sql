ALTER TABLE [dbo].[Documento]
    ADD CONSTRAINT [FK_Documento_DetalleDocumento1] FOREIGN KEY ([IdDetalleProductos]) REFERENCES [dbo].[DetalleDocumento] ([IdDetalle]) ON DELETE NO ACTION ON UPDATE NO ACTION;

