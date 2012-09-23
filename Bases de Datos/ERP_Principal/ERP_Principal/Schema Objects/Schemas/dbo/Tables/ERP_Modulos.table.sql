CREATE TABLE [dbo].[ERP_Modulos] (
    [IdModulo]  INT          IDENTITY (1, 1) NOT NULL,
    [Nombre]    VARCHAR (50) NOT NULL,
    [NombreBD]  VARCHAR (35) NOT NULL,
    [IdEntidad] INT          NOT NULL,
    [Enabled]   BIT          NOT NULL
);

