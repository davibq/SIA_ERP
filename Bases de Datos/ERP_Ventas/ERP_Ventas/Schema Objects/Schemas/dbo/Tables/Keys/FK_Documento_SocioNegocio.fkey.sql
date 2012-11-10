ALTER TABLE [dbo].[Documento]
    ADD CONSTRAINT [FK_Documento_SocioNegocio] FOREIGN KEY ([IdSocioNegocio]) REFERENCES [dbo].[SocioNegocio] ([IdSocioNegocio]) ON DELETE NO ACTION ON UPDATE NO ACTION;

