
CREATE PROCEDURE ModificarCostoPromedio
	@pLineasVenta As [dbo].[LineaVenta] Readonly
AS
BEGIN
	SET NOCOUNT ON;
	
	INSERT INTO dbo.CostoXArticuloXBodega (IdArticulo, IdBodega, FechaActualizacion, Costo)
	SELECT 
		LV.IdArticulo, LV.IdBodega, GETDATE(), (CXAXB.Costo*AXB.Stock+LV.Precio*LV.Cantidad)/(AXB.Stock+LV.Cantidad)
	FROM @pLineasVenta LV
	INNER JOIN 
	(
		SELECT MAX(FechaActualizacion) Fecha, IdBodega, IdArticulo from dbo.CostoXArticuloXBodega
		GROUP BY IdBodega, IdArticulo
	) AS FechaUltima ON 
		FechaUltima.IdArticulo=LV.IdArticulo AND FechaUltima.IdBodega=LV.IdBodega
	INNER JOIN  dbo.CostoXArticuloXBodega CXAXB ON 
		FechaUltima.Fecha=CXAXB.FechaActualizacion AND FechaUltima.IdArticulo=CXAXB.IdArticulo
		AND FechaUltima.IdBodega=CXAXB.IdBodega
	INNER JOIN dbo.ArticuloXBodega AXB ON 
		AXB.IdArticulo=LV.IdArticulo AND AXB.IdBodega=LV.IdBodega
END