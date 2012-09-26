ALTER TABLE [dbo].[ERP_UsuarioXModulo]
    ADD CONSTRAINT [FK_ERP_UsuarioXModulo_ERP_Modulos] FOREIGN KEY ([IdModulo]) REFERENCES [dbo].[ERP_Modulos] ([IdModulo]) ON DELETE NO ACTION ON UPDATE NO ACTION;

