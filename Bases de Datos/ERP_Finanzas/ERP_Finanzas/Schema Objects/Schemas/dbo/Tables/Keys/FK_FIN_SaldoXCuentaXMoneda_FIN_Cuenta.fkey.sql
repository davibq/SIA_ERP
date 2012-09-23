ALTER TABLE [dbo].[FIN_SaldoXCuentaXMoneda]
    ADD CONSTRAINT [FK_FIN_SaldoXCuentaXMoneda_FIN_Cuenta] FOREIGN KEY ([IdCuenta]) REFERENCES [dbo].[FIN_Cuenta] ([IdCuenta]) ON DELETE NO ACTION ON UPDATE NO ACTION;

