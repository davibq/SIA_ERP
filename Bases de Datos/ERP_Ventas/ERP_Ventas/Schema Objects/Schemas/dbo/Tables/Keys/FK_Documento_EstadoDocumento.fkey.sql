ALTER TABLE [dbo].[Documento]  ADD  CONSTRAINT [FK_Documento_EstadoDocumento] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[EstadoDocumento] ([IdEstado]) ON DELETE NO ACTION ON UPDATE NO ACTION;