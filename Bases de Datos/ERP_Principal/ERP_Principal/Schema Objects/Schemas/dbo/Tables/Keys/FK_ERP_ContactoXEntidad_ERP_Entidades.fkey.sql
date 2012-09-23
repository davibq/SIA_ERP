ALTER TABLE [dbo].[ERP_ContactoXEntidad]
    ADD CONSTRAINT [FK_ERP_ContactoXEntidad_ERP_Entidades] FOREIGN KEY ([IdEntidad]) REFERENCES [dbo].[ERP_Entidades] ([IdEntidad]) ON DELETE NO ACTION ON UPDATE NO ACTION;

