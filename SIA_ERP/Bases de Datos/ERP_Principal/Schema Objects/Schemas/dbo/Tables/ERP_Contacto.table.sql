CREATE TABLE [dbo].[ERP_Contacto] (
    [IdContacto]  INT          IDENTITY (1, 1) NOT NULL,
    [Nombre]      VARCHAR (20) NOT NULL,
    [Descripcion] VARCHAR (40) NOT NULL,
    [Enabled]     BIT          NOT NULL
);

