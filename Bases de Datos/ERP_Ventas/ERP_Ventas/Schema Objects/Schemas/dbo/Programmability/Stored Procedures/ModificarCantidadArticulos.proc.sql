CREATE PROCEDURE dbo.ModificarCantidadArticulos
	@pLineasVenta As [dbo].[LineaVenta] Readonly,
	@pCampo AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;
	IF @pCampo='Solicitado'
	BEGIN
		UPDATE ArticuloXBodega SET Solicitado+=lv.Cantidad
		FROM @pLineasVenta lv
		WHERE ArticuloXBodega.IdArticulo=lv.IdArticulo AND ArticuloXBodega.IdBodega=lv.IdBodega
	END
	ELSE IF @pCampo='Comprometido'
	BEGIN
		UPDATE ArticuloXBodega SET Comprometido+=lv.Cantidad
		FROM @pLineasVenta lv
		WHERE ArticuloXBodega.IdArticulo=lv.IdArticulo AND ArticuloXBodega.IdBodega=lv.IdBodega
	END
	ELSE IF @pCampo='Stock'
	BEGIN
		UPDATE ArticuloXBodega SET Stock+=lv.Cantidad
		FROM @pLineasVenta lv
		WHERE ArticuloXBodega.IdArticulo=lv.IdArticulo AND ArticuloXBodega.IdBodega=lv.IdBodega
	END
END