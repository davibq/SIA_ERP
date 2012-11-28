CREATE PROCEDURE dbo.ObtenerHistorialArticulosCliente 
	@CodigoCliente VARCHAR(15)
AS
	SELECT 
	Art.Codigo ,Art.Nombre,
	ISNULL(MAX(EncDoc.Fecha),0) FechaUltimaVenta,
	ISNULL(SUM(ArtXDet.Cantidad),0) CantidadVendida,
	ISNULL(SUM(ArtXBod.Stock),0) Stock,
	ISNULL(SUM(ArtXBod.Comprometido),0) Comprometido,
	ISNULL(( SUM(ArtXBod.Stock) + SUM(ArtXBod.Solicitado) - SUM(ArtXBod.Comprometido)), 0) Disponible,
	ISNULL(SUM(CASE WHEN DATEDIFF(MONTH, EncDoc.Fecha, GETDATE())<13 THEN ArtXDet.Cantidad ELSE 0 END),0) VendidoMeses ,
	ISNULL(AVG(ConsultaPromedio.Cantidad),0) Promedio
    FROM dbo.SocioNegocio SocNeg
	INNER JOIN dbo.Documento Doc ON Doc.IdSocioNegocio = SocNeg.IdSocioNegocio
	INNER JOIN dbo.EncabezadoDocumento EncDoc ON EncDoc.IdEncabezado = Doc.IdEncabezado
	INNER JOIN dbo.TipoDocumento TipoDoc ON TipoDoc.IdTipoDocumento = Doc.IdTipoDocumento
	INNER JOIN dbo.EstadoDocumento EstDoc ON EstDoc.IdEstado = Doc.IdEstado
	INNER JOIN dbo.DetalleDocumento DetDoc ON DetDoc.IdDetalle = IdDetalleProductos
	INNER JOIN dbo.ArticulosXDetalle ArtXDet ON ArtXDet.IdDetalle = DetDoc.IdDetalle
	INNER JOIN dbo.Articulo Art ON Art.IdArticulo = ArtXDet.IdArticulo
	INNER JOIN dbo.ArticuloXBodega ArtXBod ON ArtXBod.IdArticulo = Art.IdArticulo
	INNER JOIN(
		SELECT ArtXDet.Cantidad , Art.IdArticulo FROM  dbo.SocioNegocio SocNeg
		INNER JOIN dbo.Documento Doc ON Doc.IdSocioNegocio = SocNeg.IdSocioNegocio
		INNER JOIN dbo.EncabezadoDocumento EncDoc ON EncDoc.IdEncabezado = Doc.IdEncabezado
		INNER JOIN dbo.TipoDocumento TipoDoc ON TipoDoc.IdTipoDocumento = Doc.IdTipoDocumento
		INNER JOIN dbo.EstadoDocumento EstDoc ON EstDoc.IdEstado = Doc.IdEstado
		INNER JOIN dbo.DetalleDocumento DetDoc ON DetDoc.IdDetalle = Doc.IdDetalleProductos
		INNER JOIN dbo.ArticulosXDetalle ArtXDet ON ArtXDet.IdDetalle = DetDoc.IdDetalle
		INNER JOIN dbo.Articulo Art ON Art.IdArticulo = ArtXDet.IdArticulo
		WHERE SocNeg.Codigo = @CodigoCliente
	)AS ConsultaPromedio ON ConsultaPromedio.IdArticulo = Art.IdArticulo 
	WHERE TipoDoc.Nombre = 'Factura de Clientes' AND EstDoc.Detalle = 'Cancelado' AND
	SocNeg.Codigo = @CodigoCliente
	GROUP BY Art.IdArticulo, Art.Codigo, Art.Nombre
RETURN 0