ALTER TABLE [dbo].[ERP_PermisosXTipoUsuario]
    ADD CONSTRAINT [FK_ERP_PermisosXTipoUsuario_ERP_TipoUsuario] FOREIGN KEY ([IdTipoUsuario]) REFERENCES [dbo].[ERP_TipoUsuario] ([IdTipoUsuario]) ON DELETE NO ACTION ON UPDATE NO ACTION;

