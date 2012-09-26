ALTER TABLE [dbo].[ERP_EntidadXPais]
    ADD CONSTRAINT [FK_ERP_EntidadXPais_ERP_Entidades] FOREIGN KEY ([IdEntidad]) REFERENCES [dbo].[ERP_Entidades] ([IdEntidad]) ON DELETE NO ACTION ON UPDATE NO ACTION;

