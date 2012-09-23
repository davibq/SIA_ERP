CREATE TABLE [dbo].[FIN_SaldoXCuentaXMoneda] (
    [IdCuenta] INT             IDENTITY (1, 1) NOT NULL,
    [IdMoneda] INT             NOT NULL,
    [Saldo]    DECIMAL (14, 2) NOT NULL
);

