ALTER TABLE [dbo].[FIN_MontosXMonedaXAsientos]
    ADD CONSTRAINT [FK_FIN_MontosXMonedaXAsientos_FIN_Cuenta] FOREIGN KEY ([IdCuenta]) REFERENCES [dbo].[FIN_Cuenta] ([IdCuenta]) ON DELETE NO ACTION ON UPDATE NO ACTION;

