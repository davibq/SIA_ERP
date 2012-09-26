ALTER TABLE [dbo].[ERP_ContactoXEntidad]
    ADD CONSTRAINT [FK_ERP_ContactoXEntidad_ERP_Contacto] FOREIGN KEY ([IdContacto]) REFERENCES [dbo].[ERP_Contacto] ([IdContacto]) ON DELETE NO ACTION ON UPDATE NO ACTION;

