ALTER TABLE [dbo].[ERP_UsuarioXModulo]
    ADD CONSTRAINT [FK_ERP_UsuarioXModulo_ERP_Usuarios] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[ERP_Usuarios] ([IdUsuario]) ON DELETE NO ACTION ON UPDATE NO ACTION;

