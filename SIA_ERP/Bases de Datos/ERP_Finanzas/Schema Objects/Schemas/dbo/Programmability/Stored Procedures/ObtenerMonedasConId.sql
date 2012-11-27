CREATE PROCEDURE [dbo].[ObtenerMonedasConId]
	@Moneda varchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT FIN_Moneda.IdMoneda, FIN_Moneda.Nombre, FIN_Moneda.Acronimo FROM FIN_Moneda
	WHERE FIN_Moneda.Nombre = @Moneda
END
