ALTER TABLE [dbo].[FIN_Asiento]
    ADD CONSTRAINT [FK_FIN_Asiento_FIN_TipoAsiento] FOREIGN KEY ([IdTipoAsiento]) REFERENCES [dbo].[FIN_TipoAsiento] ([IdTipoAsiento]) ON DELETE NO ACTION ON UPDATE NO ACTION;

