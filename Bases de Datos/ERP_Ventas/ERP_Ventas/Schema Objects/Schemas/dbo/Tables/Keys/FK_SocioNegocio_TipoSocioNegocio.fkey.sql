ALTER TABLE [dbo].[SocioNegocio]
    ADD CONSTRAINT [FK_SocioNegocio_TipoSocioNegocio] FOREIGN KEY ([IdTipoSocio]) REFERENCES [dbo].[TipoSocioNegocio] ([IdTipoSocio]) ON DELETE NO ACTION ON UPDATE NO ACTION;

