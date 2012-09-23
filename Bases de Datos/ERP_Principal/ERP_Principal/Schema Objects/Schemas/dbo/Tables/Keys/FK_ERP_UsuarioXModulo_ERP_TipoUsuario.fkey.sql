ALTER TABLE [dbo].[ERP_UsuarioXModulo]
    ADD CONSTRAINT [FK_ERP_UsuarioXModulo_ERP_TipoUsuario] FOREIGN KEY ([IdTipoUsuario]) REFERENCES [dbo].[ERP_TipoUsuario] ([IdTipoUsuario]) ON DELETE NO ACTION ON UPDATE NO ACTION;

