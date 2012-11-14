CREATE PROCEDURE [dbo].[ERPSP_ObtenerCuentasHijas]
	@NombreCuenta VARCHAR(45)
AS
	DECLARE @CodigoPadre VARCHAR(15)
	DECLARE @TipoMoneda INT
	SELECT @CodigoPadre = Codigo FROM FIN_Cuenta WHERE Nombre = @NombreCuenta

	SELECT Cuen.Codigo CodigoCuenta, Cuen.Nombre NombreCuenta, SalXCuXMo.Saldo SaldoCuenta, Mon.Acronimo AcronimoMoneda, Mon.EsLocal
	FROM dbo.FIN_Cuenta Cuen 
	INNER JOIN dbo.FIN_IdentificadorCuenta IdeCuen ON IdeCuen.IdIdentificadorCuenta = Cuen.IdIdentificadorCuenta
	INNER JOIN dbo.FIN_SaldoXCuentaXMoneda SalXCuXMo ON SalXCuXMo.IdCuenta = Cuen.IdCuenta
	INNER JOIN dbo.FIN_Moneda Mon ON Mon.IdMoneda = SalXCuXMo.IdMoneda
	WHERE Cuen.Enabled = 1 AND Mon.EsSistema = 1 AND Cuen.Codigo LIKE @CodigoPadre+'%'
	AND Cuen.IdCuenta NOT IN
	(
		SELECT IdCuentaPadre FROM dbo.FIN_Cuenta
	)
	ORDER BY Cuen.Codigo
RETURN 0