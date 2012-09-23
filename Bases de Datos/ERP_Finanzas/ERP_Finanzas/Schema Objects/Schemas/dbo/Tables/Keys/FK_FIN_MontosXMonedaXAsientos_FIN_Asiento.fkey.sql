ALTER TABLE [dbo].[FIN_MontosXMonedaXAsientos]
    ADD CONSTRAINT [FK_FIN_MontosXMonedaXAsientos_FIN_Asiento] FOREIGN KEY ([IdAsiento]) REFERENCES [dbo].[FIN_Asiento] ([IdAsiento]) ON DELETE NO ACTION ON UPDATE NO ACTION;

