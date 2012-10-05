CREATE PROCEDURE [dbo].[ObtenerMonedas]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Nombre FROM FIN_Moneda
END
