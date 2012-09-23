ALTER TABLE [dbo].[ERP_Modulos]
    ADD CONSTRAINT [FK_ERP_Modulos_ERP_Entidades] FOREIGN KEY ([IdEntidad]) REFERENCES [dbo].[ERP_Entidades] ([IdEntidad]) ON DELETE NO ACTION ON UPDATE NO ACTION;

