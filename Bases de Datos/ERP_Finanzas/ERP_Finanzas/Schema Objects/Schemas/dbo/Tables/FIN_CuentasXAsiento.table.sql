CREATE TABLE [dbo].[FIN_CuentasXAsiento] (
    [IdAsiento] INT IDENTITY (1, 1) NOT NULL,
    [IdCuenta]  INT NOT NULL,
    [AlDebe]    BIT NOT NULL
);

