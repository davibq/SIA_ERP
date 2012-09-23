ALTER TABLE [dbo].[FIN_MontosXMonedaXAsientos]
    ADD CONSTRAINT [FK_FIN_MontosXMonedaXAsientos_FIN_Moneda] FOREIGN KEY ([IdMoneda]) REFERENCES [dbo].[FIN_Moneda] ([IdMoneda]) ON DELETE NO ACTION ON UPDATE NO ACTION;

