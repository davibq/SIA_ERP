ALTER TABLE [dbo].[FIN_Mes]
    ADD CONSTRAINT [FK_FIN_Mes_FIN_PeriodoContable] FOREIGN KEY ([IdPeriodoContable]) REFERENCES [dbo].[FIN_PeriodoContable] ([IdPeriodoContable]) ON DELETE NO ACTION ON UPDATE NO ACTION;

