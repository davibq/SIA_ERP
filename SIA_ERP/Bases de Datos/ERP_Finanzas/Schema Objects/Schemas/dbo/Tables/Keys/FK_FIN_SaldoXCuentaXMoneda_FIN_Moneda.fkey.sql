ALTER TABLE [dbo].[FIN_SaldoXCuentaXMoneda]
    ADD CONSTRAINT [FK_FIN_SaldoXCuentaXMoneda_FIN_Moneda] FOREIGN KEY ([IdMoneda]) REFERENCES [dbo].[FIN_Moneda] ([IdMoneda]) ON DELETE NO ACTION ON UPDATE NO ACTION;

