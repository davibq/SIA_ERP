CREATE PROCEDURE dbo.AgregarSolicitados
	@pLineasVenta As [dbo].[LineaVenta] Readonly
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE ArticuloXBodega SET Solicitado+=lv.Cantidad
		FROM @pLineasVenta lv
		WHERE ArticuloXBodega.IdArticulo=lv.IdArticulo AND ArticuloXBodega.IdBodega=lv.IdBodega
END