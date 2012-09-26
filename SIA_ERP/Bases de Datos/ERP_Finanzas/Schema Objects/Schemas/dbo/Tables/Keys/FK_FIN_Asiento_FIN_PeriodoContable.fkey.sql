ALTER TABLE [dbo].[FIN_Asiento]
    ADD CONSTRAINT [FK_FIN_Asiento_FIN_PeriodoContable] FOREIGN KEY ([IdPeriodoContable]) REFERENCES [dbo].[FIN_PeriodoContable] ([IdPeriodoContable]) ON DELETE NO ACTION ON UPDATE NO ACTION;

