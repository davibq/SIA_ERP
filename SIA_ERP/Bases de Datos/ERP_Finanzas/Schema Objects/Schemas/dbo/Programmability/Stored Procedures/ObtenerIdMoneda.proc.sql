CREATE PROCEDURE [dbo].[ObtenerIdMoneda]
	@Moneda varchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT FIN_Moneda.IdMoneda FROM FIN_Moneda
	WHERE FIN_Moneda.Nombre = @Moneda
END