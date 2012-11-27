ALTER TABLE [dbo].[Transferencia]  ADD  CONSTRAINT [FK_Transferencia_Bancos] FOREIGN KEY([idBanco])
REFERENCES [dbo].[Bancos] ([idBanco]) ON DELETE NO ACTION ON UPDATE NO ACTION;