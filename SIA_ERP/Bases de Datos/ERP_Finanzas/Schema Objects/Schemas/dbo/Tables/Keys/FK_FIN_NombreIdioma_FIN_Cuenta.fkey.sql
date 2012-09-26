ALTER TABLE [dbo].[FIN_NombreIdioma]
    ADD CONSTRAINT [FK_FIN_NombreIdioma_FIN_Cuenta] FOREIGN KEY ([IdCuenta]) REFERENCES [dbo].[FIN_Cuenta] ([IdCuenta]) ON DELETE NO ACTION ON UPDATE NO ACTION;

