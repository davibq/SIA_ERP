﻿ALTER TABLE [dbo].[FIN_Mes]
    ADD CONSTRAINT [FK_FIN_Mes_FIN_EstadoMes] FOREIGN KEY ([IdEstadoMes]) REFERENCES [dbo].[FIN_EstadoMes] ([IdEstadoMes]) ON DELETE NO ACTION ON UPDATE NO ACTION;

