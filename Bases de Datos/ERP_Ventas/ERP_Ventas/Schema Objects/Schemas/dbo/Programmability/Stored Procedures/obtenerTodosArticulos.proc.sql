CREATE PROCEDURE [dbo].[obtenerTodosArticulos]
	AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM dbo.Articulo
END
RETURN 0