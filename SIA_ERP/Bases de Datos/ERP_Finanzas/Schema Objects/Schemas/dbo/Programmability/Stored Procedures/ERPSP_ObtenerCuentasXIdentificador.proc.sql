-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 24/09/2012
-- Descripcion: Obtiene las cuentas segun el identificador recibido
-----------------------------------------------------------
CREATE PROCEDURE [dbo].[ERPSP_ObtenerCuentasXIdentificador]
	@NombreIdentificador VARCHAR(50)
AS
	SELECT Cuen.Nombre NombreCuenta, SalXCuXMo.Saldo SaldoCuenta, Mon.Acronimo AcronimoMoneda
	FROM dbo.FIN_Cuenta Cuen 
	INNER JOIN dbo.FIN_IdentificadorCuenta IdeCuen ON IdeCuen.IdIdentificadorCuenta = Cuen.IdIdentificadorCuenta
	INNER JOIN dbo.FIN_SaldoXCuentaXMoneda SalXCuXMo ON SalXCuXMo.IdCuenta = Cuen.IdCuenta
	INNER JOIN dbo.FIN_Moneda Mon ON Mon.IdMoneda = SalXCuXMo.IdMoneda
	WHERE Cuen.Enabled = 1 AND Mon.EsLocal = 1 AND IdeCuen.Nombre = @NombreIdentificador AND Cuen.IdCuenta NOT IN
	(
		SELECT IdCuentaPadre FROM dbo.FIN_Cuenta
	)
	ORDER BY Cuen.Codigo
RETURN 0