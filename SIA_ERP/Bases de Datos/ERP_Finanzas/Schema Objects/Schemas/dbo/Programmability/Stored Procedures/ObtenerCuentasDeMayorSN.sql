CREATE PROCEDURE [dbo].[ObtenerCuentasDeMayorSN]
AS
BEGIN
	SELECT Codigo, Nombre, Nivel FROM FIN_Cuenta
	WHERE FIN_Cuenta.Nivel = '4' AND ('Cuentas por' like FIN_Cuenta.Nombre OR DIFFERENCE('Cuentas por',FIN_Cuenta.Nombre)>2)
END
RETURN 0
