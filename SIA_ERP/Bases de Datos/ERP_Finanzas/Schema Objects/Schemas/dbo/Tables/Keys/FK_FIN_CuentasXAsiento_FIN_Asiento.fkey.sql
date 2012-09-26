ALTER TABLE [dbo].[FIN_CuentasXAsiento]
    ADD CONSTRAINT [FK_FIN_CuentasXAsiento_FIN_Asiento] FOREIGN KEY ([IdAsiento]) REFERENCES [dbo].[FIN_Asiento] ([IdAsiento]) ON DELETE NO ACTION ON UPDATE NO ACTION;

