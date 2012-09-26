ALTER TABLE [dbo].[FIN_NombreIdioma]
    ADD CONSTRAINT [FK_FIN_NombreIdioma_FIN_Idioma] FOREIGN KEY ([IdIdioma]) REFERENCES [dbo].[FIN_Idioma] ([IdIdioma]) ON DELETE NO ACTION ON UPDATE NO ACTION;

