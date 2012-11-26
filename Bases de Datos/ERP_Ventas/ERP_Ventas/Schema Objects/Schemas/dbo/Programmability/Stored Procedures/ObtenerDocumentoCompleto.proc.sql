
CREATE PROCEDURE [dbo].[ObtenerDocumentoCompleto]
	@pIdDocumento INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		Doc.IdDocumento, Doc.Consecutivo, ED.Fecha, ED.Fecha2, TD.Nombre TipoDocumento,
		DD.Subtotal, DD.Total, A.IdArticulo, A.Nombre NombreArticulo, 
		B.IdBodega, B.Nombre Bodega, AXD.Cantidad, AXD.Impuesto,
		SN.IdSocioNegocio, SN.Nombre SocioNegocio
	FROM Documento Doc
	INNER JOIN SocioNegocio SN ON SN.IdSocioNegocio=Doc.IdSocioNegocio
	INNER JOIN EncabezadoDocumento ED ON ED.IdEncabezado=Doc.IdEncabezado
	LEFT JOIN TipoDocumento TD ON TD.IdTipoDocumento=Doc.IdTipoDocumento
	LEFT JOIN DetalleDocumento DD ON Doc.IdDetalleProductos=DD.IdDetalle
	LEFT JOIN ArticulosXDetalle AXD ON AXD.IdDetalle=DD.IdDetalle
	LEFT JOIN Articulo A ON A.IdArticulo=AXD.IdArticulo
	LEFT JOIN Bodega B ON B.IdBodega=AXD.IdBodega
	WHERE Doc.IdDocumento=@pIdDocumento
END
