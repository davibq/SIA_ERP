CREATE PROCEDURE [dbo].[ObtenerDocumentoCompleto]
	@pIdDocumento INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		Doc.IdDocumento, Doc.Consecutivo, ED.Fecha, ED.Fecha2, TD.Nombre TipoDocumento,
		DD.Subtotal, DD.Total, A.IdArticulo, A.Nombre NombreArticulo, 
		B.IdBodega, B.Nombre Bodega, AXD.Cantidad, AXD.Impuesto, AXD.Precio,
		SN.IdSocioNegocio, SN.Nombre SocioNegocio, CXAXB.Costo, AXB.codCuentasCostos, AXB.codCuentasExistencia,
		AXB.codCuentasVentas, AXB.codCuentaTransitoria
	FROM Documento Doc
	INNER JOIN SocioNegocio SN ON SN.IdSocioNegocio=Doc.IdSocioNegocio
	INNER JOIN EncabezadoDocumento ED ON ED.IdEncabezado=Doc.IdEncabezado
	LEFT JOIN TipoDocumento TD ON TD.IdTipoDocumento=Doc.IdTipoDocumento
	LEFT JOIN DetalleDocumento DD ON Doc.IdDetalleProductos=DD.IdDetalle
	LEFT JOIN ArticulosXDetalle AXD ON AXD.IdDetalle=DD.IdDetalle
	LEFT JOIN Articulo A ON A.IdArticulo=AXD.IdArticulo
	LEFT JOIN Bodega B ON B.IdBodega=AXD.IdBodega
	LEFT JOIN 
	(
		SELECT MAX(FechaActualizacion) Fecha, IdBodega, IdArticulo from dbo.CostoXArticuloXBodega
		GROUP BY IdBodega, IdArticulo
	) AS FechaUltima ON 
		FechaUltima.IdArticulo=A.IdArticulo AND FechaUltima.IdBodega=B.IdBodega
	LEFT JOIN  dbo.CostoXArticuloXBodega CXAXB ON 
		FechaUltima.Fecha=CXAXB.FechaActualizacion AND FechaUltima.IdArticulo=CXAXB.IdArticulo
		AND FechaUltima.IdBodega=CXAXB.IdBodega
	LEFT JOIN ArticuloXBodega AXB ON AXB.IdArticulo=A.IdArticulo AND AXB.IdBodega=B.IdBodega
	WHERE Doc.IdDocumento=@pIdDocumento
END