ALTER TABLE [dbo].[Transferencia]
    ADD CONSTRAINT [FK_Transferencia_TipoTransferencia] FOREIGN KEY ([IdTipoTransferencia]) REFERENCES [dbo].[TipoTransferencia] ([IdTipoTransferencia]) ON DELETE NO ACTION ON UPDATE NO ACTION;

