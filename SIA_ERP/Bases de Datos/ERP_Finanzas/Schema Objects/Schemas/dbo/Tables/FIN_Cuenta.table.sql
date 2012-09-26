CREATE TABLE [dbo].[FIN_Cuenta] (
    [IdCuenta]              INT          IDENTITY (1, 1) NOT NULL,
    [IdIdentificadorCuenta] INT          NOT NULL,
    [IdCuentaPadre]         INT          NOT NULL,
    [Nombre]                VARCHAR (45) NOT NULL,
    [Codigo]                VARCHAR (15) NOT NULL,
    [Nivel]                 INT          NOT NULL,
    [Enabled]               BIT          NOT NULL
);

