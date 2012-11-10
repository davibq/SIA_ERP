ALTER TABLE [dbo].[Documento]
    ADD CONSTRAINT [FK_Documento_TipoDocumento] FOREIGN KEY ([IdTipoDocumento]) REFERENCES [dbo].[TipoDocumento] ([IdTipoDocumento]) ON DELETE NO ACTION ON UPDATE NO ACTION;

