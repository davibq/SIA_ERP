CREATE PROCEDURE [dbo].[ObtenerMonedas]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Nombre, Acronimo, idBCCR FROM FIN_Moneda
END
