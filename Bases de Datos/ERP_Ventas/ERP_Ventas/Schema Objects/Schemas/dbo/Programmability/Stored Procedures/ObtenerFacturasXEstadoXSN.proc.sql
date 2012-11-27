CREATE PROCEDURE [dbo].[ObtenerFacturasXEstadoXSN]
	@pCodSN			VARCHAR(20),
	@pEstadoFactura	VARCHAR(75)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Doc.IdDocumento IdDocumento, Doc.Consecutivo Consecutivo, Enc.Fecha Fecha, DetDoc.Subtotal Subtotal, DetDoc.Total Total
	FROM dbo.Documento Doc 
	INNER JOIN dbo.EncabezadoDocumento Enc ON Doc.IdEncabezado=Enc.IdEncabezado
	INNER JOIN dbo.DetalleDocumento DetDoc ON Doc.IdDetalleProductos=DetDoc.IdDetalle
	INNER JOIN dbo.TipoDocumento TDoc ON Doc.IdTipoDocumento=TDoc.IdTipoDocumento
	INNER JOIN dbo.SocioNegocio SN ON Doc.IdSocioNegocio=SN.IdSocioNegocio
	INNER JOIN dbo.EstadoDocumento EDoc ON Doc.IdEstado=EDoc.IdEstado
	WHERE SN.Codigo=@pCodSN AND EDoc.Detalle=@pEstadoFactura
	ORDER BY Doc.Consecutivo
END
RETURN 0