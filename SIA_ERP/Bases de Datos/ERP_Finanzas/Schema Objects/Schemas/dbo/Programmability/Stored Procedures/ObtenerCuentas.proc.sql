CREATE PROCEDURE [dbo].[ObtenerCuentas]
AS
BEGIN
	SELECT Codigo, Nombre, Nivel FROM FIN_Cuenta ORDER BY Codigo
END
RETURN 0