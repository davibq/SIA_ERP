ALTER TABLE [dbo].[Documento]
    ADD CONSTRAINT [FK_Documento_DetalleServicios] FOREIGN KEY ([IdDetalleServicios]) REFERENCES [dbo].[DetalleServicios] ([IdDetalleServicios]) ON DELETE NO ACTION ON UPDATE NO ACTION;

