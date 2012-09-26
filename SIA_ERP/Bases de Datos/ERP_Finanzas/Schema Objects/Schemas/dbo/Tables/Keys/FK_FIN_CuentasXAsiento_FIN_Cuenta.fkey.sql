ALTER TABLE [dbo].[FIN_CuentasXAsiento]
    ADD CONSTRAINT [FK_FIN_CuentasXAsiento_FIN_Cuenta] FOREIGN KEY ([IdCuenta]) REFERENCES [dbo].[FIN_Cuenta] ([IdCuenta]) ON DELETE NO ACTION ON UPDATE NO ACTION;

