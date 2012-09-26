ALTER TABLE [dbo].[FIN_HistorialTipoCambio]
    ADD CONSTRAINT [FK_FIN_HistorialTipoCambio_FIN_Moneda] FOREIGN KEY ([IdMonedaOrigen]) REFERENCES [dbo].[FIN_Moneda] ([IdMoneda]) ON DELETE NO ACTION ON UPDATE NO ACTION;

