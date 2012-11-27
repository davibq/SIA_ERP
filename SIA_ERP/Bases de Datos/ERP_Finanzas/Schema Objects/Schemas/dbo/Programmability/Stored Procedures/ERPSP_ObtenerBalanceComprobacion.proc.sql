-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 06/10/2012
-- Descripcion: Obtiene el balance de comprobacion
-----------------------------------------------------------
CREATE PROCEDURE [dbo].[ERPSP_ObtenerBalanceComprobacion]
AS
	SELECT Cuen.Nombre NombreCuenta, SalXCuXMo.Saldo SaldoCuenta, Mon.Acronimo AcronimoMoneda, Cuen.Codigo CodigoCuenta,
	CASE
		WHEN IdeCuen.Numero IN (1,6,8,5) THEN 1
		WHEN IdeCuen.Numero IN (2,3,4,7) THEN 0
	END AlDebe
	FROM dbo.FIN_Cuenta Cuen 
	INNER JOIN dbo.FIN_IdentificadorCuenta IdeCuen ON IdeCuen.IdIdentificadorCuenta = Cuen.IdIdentificadorCuenta
	INNER JOIN dbo.FIN_SaldoXCuentaXMoneda SalXCuXMo ON SalXCuXMo.IdCuenta = Cuen.IdCuenta
	INNER JOIN dbo.FIN_Moneda Mon ON Mon.IdMoneda = SalXCuXMo.IdMoneda
	WHERE Cuen.Enabled = 1 AND Mon.EsSistema = 1 AND Cuen.IdCuenta NOT IN
	(
		SELECT IdCuentaPadre FROM dbo.FIN_Cuenta
	)
	ORDER BY Cuen.Codigo, IdeCuen.Numero
RETURN 0