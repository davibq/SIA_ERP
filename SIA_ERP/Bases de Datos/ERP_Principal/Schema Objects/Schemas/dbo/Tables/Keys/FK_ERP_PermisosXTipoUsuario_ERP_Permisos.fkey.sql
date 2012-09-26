ALTER TABLE [dbo].[ERP_PermisosXTipoUsuario]
    ADD CONSTRAINT [FK_ERP_PermisosXTipoUsuario_ERP_Permisos] FOREIGN KEY ([IdPermisos]) REFERENCES [dbo].[ERP_Permisos] ([IdPermisos]) ON DELETE NO ACTION ON UPDATE NO ACTION;

