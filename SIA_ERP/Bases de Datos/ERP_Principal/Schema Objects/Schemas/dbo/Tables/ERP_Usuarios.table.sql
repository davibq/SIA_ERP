CREATE TABLE [dbo].[ERP_Usuarios] (
    [IdUsuario] INT             IDENTITY (1, 1) NOT NULL,
    [Login]     VARCHAR (50) NOT NULL,
    [Password]  VARBINARY (256) NOT NULL,
    [Enabled]   BIT             NOT NULL
);

