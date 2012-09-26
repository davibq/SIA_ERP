CREATE TABLE [dbo].[FIN_Moneda] (
    [IdMoneda]  INT          IDENTITY (1, 1) NOT NULL,
    [Nombre]    VARCHAR (20) NOT NULL,
    [Acronimo]  VARCHAR (3)  NOT NULL,
    [EsLocal]   BIT          NOT NULL,
    [EsSistema] BIT          NOT NULL
);

