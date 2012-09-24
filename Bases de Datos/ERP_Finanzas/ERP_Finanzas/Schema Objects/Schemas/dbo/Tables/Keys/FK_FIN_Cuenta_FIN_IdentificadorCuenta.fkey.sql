ALTER TABLE [dbo].[FIN_Cuenta]
    ADD CONSTRAINT [FK_FIN_Cuenta_FIN_IdentificadorCuenta] FOREIGN KEY ([IdIdentificadorCuenta]) REFERENCES [dbo].[FIN_IdentificadorCuenta] ([IdidentificadorCuenta]) ON DELETE NO ACTION ON UPDATE NO ACTION;

