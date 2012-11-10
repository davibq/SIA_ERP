ALTER TABLE [dbo].[Documento]
    ADD CONSTRAINT [FK_Documento_EncabezadoDocumento] FOREIGN KEY ([IdEncabezado]) REFERENCES [dbo].[EncabezadoDocumento] ([IdEncabezado]) ON DELETE NO ACTION ON UPDATE NO ACTION;

