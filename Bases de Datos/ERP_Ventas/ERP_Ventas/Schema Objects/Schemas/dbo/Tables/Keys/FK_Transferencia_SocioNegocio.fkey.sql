ALTER TABLE [dbo].[Transferencia]
    ADD CONSTRAINT [FK_Transferencia_SocioNegocio] FOREIGN KEY ([IdSocioNegocio]) REFERENCES [dbo].[SocioNegocio] ([IdSocioNegocio]) ON DELETE NO ACTION ON UPDATE NO ACTION;

