CREATE PROCEDURE dbo.ConsultarCantidadPrecio
	@CodigoCliente VARCHAR(15),
	@Articulo VARCHAR(20)
AS
	SELECT Art.Codigo, Art.Nombre, ArtXDet.Cantidad ,ArtXDet.Precio FROM dbo.SocioNegocio SocNeg
	INNER JOIN dbo.Documento Doc ON Doc.IdSocioNegocio = SocNeg.IdSocioNegocio
	INNER JOIN dbo.TipoDocumento TipoDoc ON TipoDoc.IdTipoDocumento = Doc.IdTipoDocumento
	INNER JOIN dbo.EstadoDocumento EstDoc ON EstDoc.IdEstado = Doc.IdEstado
	INNER JOIN dbo.DetalleDocumento DetDoc ON DetDoc.IdDetalle = IdDetalleProductos
	INNER JOIN dbo.ArticulosXDetalle ArtXDet ON ArtXDet.IdDetalle = DetDoc.IdDetalle
	INNER JOIN dbo.Articulo Art ON Art.IdArticulo = ArtXDet.IdArticulo
	WHERE TipoDoc.Nombre = 'Factura de Clientes' AND EstDoc.Detalle = 'Cancelado' AND
	SocNeg.Codigo = @CodigoCliente
RETURN 0