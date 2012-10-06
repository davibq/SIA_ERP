-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 24/09/2012
-- Descripcion: Obtiene las cuentas hijas segun la cuenta padre de un nivel 2
-----------------------------------------------------------
CREATE PROCEDURE [dbo].[ERPSP_ObtenerCuentasBalanceGeneral]
	@NombreCuenta VARCHAR(45)
AS
	SELECT Cuen.Nombre NombreCuenta, SalXCuXMo.Saldo SaldoCuenta, Mon.Acronimo AcronimoMoneda
	FROM dbo.FIN_Cuenta Cuen 
	INNER JOIN dbo.FIN_IdentificadorCuenta IdeCuen ON IdeCuen.IdIdentificadorCuenta = Cuen.IdIdentificadorCuenta
	INNER JOIN dbo.FIN_SaldoXCuentaXMoneda SalXCuXMo ON SalXCuXMo.IdCuenta = Cuen.IdCuenta
	INNER JOIN dbo.FIN_Moneda Mon ON Mon.IdMoneda = SalXCuXMo.IdMoneda
	WHERE Cuen.Enabled = 1 AND Mon.EsLocal = 1 AND Cuen.IdCuentaPadre IN
	(
		SELECT IdCuenta FROM dbo.FIN_Cuenta WHERE Nombre = @NombreCuenta
	)
	 AND Cuen.IdCuenta NOT IN
	(
		SELECT IdCuentaPadre FROM dbo.FIN_Cuenta
	)
	ORDER BY Cuen.Codigo
RETURN 0