ALTER TABLE [dbo].[ERP_ContactoXUsuario]
    ADD CONSTRAINT [FK_ERP_ContactoXUsuario_ERP_Usuarios] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[ERP_Usuarios] ([IdUsuario]) ON DELETE NO ACTION ON UPDATE NO ACTION;

