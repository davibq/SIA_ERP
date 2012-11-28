CREATE PROCEDURE [dbo].[obtenerOrdenCompra]
	@codProveedor	VARCHAR(15),
	@tipoDocumento	VARCHAR(25)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT	Doc.Consecutivo Consecutivo, EncDoc.Fecha FechaConta, EncDoc.Fecha2 FechaEntrega, 
			SN.Codigo CodigoSN, SN.Nombre NombreSN, AXD.Cantidad CantidadProducto, AXD.Precio PrecioProducto, 
			AXD.IdArticulo IdArticulo, AXD.IdBodega IdBodega, Art.Nombre NombreArticulo, Bod.Nombre NombreBodega
	FROM Documento Doc 
	INNER JOIN EncabezadoDocumento EncDoc ON Doc.IdEncabezado=EncDoc.IdEncabezado
	INNER JOIN SocioNegocio SN ON Doc.IdSocioNegocio=SN.IdSocioNegocio
	INNER JOIN DetalleDocumento DD ON Doc.IdDetalleProductos=DD.IdDetalle
	INNER JOIN ArticulosXDetalle AXD ON DD.IdDetalle=AXD.IdDetalle
	INNER JOIN Articulo Art ON AXD.IdArticulo=Art.IdArticulo
	INNER JOIN Bodega Bod ON AXD.IdBodega=Bod.IdBodega
	INNER JOIN TipoDocumento TD ON TD.IdTipoDocumento=Doc.IdTipoDocumento
	INNER JOIN EstadoDocumento ED ON ED.IdEstado=Doc.IdEstado
	WHERE SN.Codigo=@codProveedor AND TD.Nombre=@tipoDocumento AND Ed.Detalle='Pendiente'
	ORDER BY Doc.Consecutivo
END
RETURN 0