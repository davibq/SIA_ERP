ALTER TABLE [dbo].[ERP_ContactoXUsuario]
    ADD CONSTRAINT [FK_ERP_ContactoXUsuario_ERP_Contacto] FOREIGN KEY ([IdContacto]) REFERENCES [dbo].[ERP_Contacto] ([IdContacto]) ON DELETE NO ACTION ON UPDATE NO ACTION;

