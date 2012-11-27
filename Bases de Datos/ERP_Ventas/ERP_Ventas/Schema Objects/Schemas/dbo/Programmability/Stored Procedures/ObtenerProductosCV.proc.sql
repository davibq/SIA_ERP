CREATE PROCEDURE [dbo].[ObtenerProductosCV]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		Articulo.IdArticulo, Articulo.Descripcion, Articulo.Codigo, Bodega.IdBodega, Bodega.Nombre Bodega, CA.Costo,
		AXB.codCuentasCostos, AXB.codCuentasExistencia, AXB.codCuentasVentas, AXB.codCuentaTransitoria
	FROM dbo.Articulo
	INNER JOIN (
		SELECT MAX(FechaActualizacion) FechaAct, IdArticulo, IdBodega from dbo.CostoXArticuloXBodega
		GROUP BY IdArticulo, IdBodega
		) as CAXB ON Articulo.IdArticulo=CAXB.IdArticulo
	INNER JOIN dbo.Bodega ON Bodega.IdBodega=CAXB.IdBodega
	INNER JOIN dbo.CostoXArticuloXBodega CA ON 
		CAXB.FechaAct=CA.FechaActualizacion AND CA.IdArticulo=Articulo.IdArticulo 
		AND CA.IdBodega=Bodega.IdBodega
	INNER JOIN dbo.ArticuloXBodega AXB ON AXB.IdArticulo=Articulo.IdArticulo AND AXB.IdBodega=Bodega.IdBodega
END
