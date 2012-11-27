CREATE PROCEDURE [dbo].[ObtenerNombreCuentaDeMayorSN]
	@CodigoSN varchar(15)
AS
BEGIN
	SELECT Codigo, Nombre, Nivel FROM FIN_Cuenta
	WHERE FIN_Cuenta.Codigo = @CodigoSN 
END
