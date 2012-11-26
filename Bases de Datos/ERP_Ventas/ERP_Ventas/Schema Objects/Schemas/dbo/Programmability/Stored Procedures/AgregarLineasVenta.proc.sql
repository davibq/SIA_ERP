
CREATE PROCEDURE AgregarLineasVenta
	@LineasVenta As [dbo].[LineaVenta] READONLY
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO ArticulosXDetalle(IdArticulo, IdDetalle, Cantidad, Impuesto, IdBodega)
		SELECT IdArticulo, IdDetalle, Cantidad, Impuesto, IdBodega From @LineasVenta
END
GO
