CREATE TABLE [dbo].[FIN_Mes] (
    [IdMes]             INT  IDENTITY (1, 1) NOT NULL,
    [IdEstadoMes]       INT  NOT NULL,
    [FechaInicio]       DATE NOT NULL,
    [FechaFinal]        DATE NOT NULL,
    [IdPeriodoContable] INT  NOT NULL
);

