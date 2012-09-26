CREATE TABLE [dbo].[FIN_Asiento] (
    [IdAsiento]            INT             IDENTITY (1, 1) NOT NULL,
    [IdPeriodoContable]    INT             NOT NULL,
    [Debe]                 DECIMAL (14, 2) NOT NULL,
    [Haber]                DECIMAL (14, 2) NOT NULL,
    [FechaContabilizacion] DATE            NOT NULL,
    [FechaDocumento]       DATE            NOT NULL,
    [IdTipoAsiento]        INT             NOT NULL,
    [Referencia1]          VARCHAR (20)    NULL,
    [Referencia2]          VARCHAR (20)    NULL,
    [Enabled]              BIT             NOT NULL
);

