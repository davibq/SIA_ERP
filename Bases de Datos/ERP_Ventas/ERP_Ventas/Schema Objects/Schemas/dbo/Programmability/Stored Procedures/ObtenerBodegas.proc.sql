CREATE PROCEDURE [dbo].[ObtenerBodegas]
AS
BEGIN
	SET NOCOUNT ON;

    SELECT Codigo, Nombre FROM dbo.Bodega
END
RETURN 0