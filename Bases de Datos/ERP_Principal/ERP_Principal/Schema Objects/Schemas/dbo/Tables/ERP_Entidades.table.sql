CREATE TABLE [dbo].[ERP_Entidades] (
    [IdEntidad]      INT             IDENTITY (1, 1) NOT NULL,
    [Nombre]         VARCHAR (35)    NOT NULL,
    [CedulaJuridica] VARCHAR (15)    NOT NULL,
    [Logo]           VARBINARY (MAX) NULL,
    [Enabled]        BIT             NOT NULL
);

