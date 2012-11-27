CREATE TABLE [dbo].[Documento] (
    [IdDocumento]        INT          IDENTITY (1, 1) NOT NULL,
    [Consecutivo]        VARCHAR (30) NOT NULL,
    [IdEncabezado]       INT          NOT NULL,
    [IdDetalleProductos] INT          NULL,
    [IdTipoDocumento]    INT          NOT NULL,
    [IdSocioNegocio]     INT          NOT NULL,
    [IdDetalleServicios] INT          NULL,
	[IdEstado] [int] NULL
);
