CREATE TABLE [dbo].[FIN_Cuenta] (
    [IdCuenta]      INT          IDENTITY (1, 1) NOT NULL,
    [Identificador] INT          NOT NULL,
    [Nombre]        VARCHAR (15) NOT NULL,
    [Codigo]        VARCHAR (20) NOT NULL,
    [IdCuentaPadre] INT          NOT NULL,
    [Nivel]         INT          NOT NULL,
    [Enabled]       BIT          NOT NULL
);

