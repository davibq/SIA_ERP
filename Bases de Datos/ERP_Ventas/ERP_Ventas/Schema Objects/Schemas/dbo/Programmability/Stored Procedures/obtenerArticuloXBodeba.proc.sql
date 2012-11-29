CREATE PROCEDURE [dbo].[obtenerArticuloXBodeba]
	@pCodArticulo	VARCHAR(20),
	@pCodBodega		VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT AxB.Stock, AxB.Comprometido, AxB.Solicitado FROM dbo.ArticuloXBodega AxB
	INNER JOIN dbo.Articulo Art ON Art.IdArticulo=AxB.IdArticulo
	INNER JOIN dbo.Bodega Bod ON AxB.IdBodega=Bod.IdBodega
	WHERE Art.Codigo=@pCodArticulo AND Bod.Codigo=@pCodBodega
END
RETURN 0