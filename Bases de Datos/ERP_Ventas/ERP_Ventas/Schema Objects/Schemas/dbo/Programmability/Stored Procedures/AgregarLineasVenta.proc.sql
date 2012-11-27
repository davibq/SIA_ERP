
CREATE PROCEDURE [dbo].[AgregarLineasVenta]
	@LineasVenta As [dbo].[LineaVenta] Readonly
AS
BEGIN
	
	SET NOCOUNT ON;
	INSERT INTO ArticulosXDetalle(IdArticulo, IdDetalle, Cantidad, Impuesto, IdBodega, Precio)
		SELECT IdArticulo, IdDetalle, Cantidad, Impuesto, IdBodega, Precio From @LineasVenta
END
