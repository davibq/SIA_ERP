CREATE PROCEDURE [dbo].[ERPSP_ObtenerCuenta]
	@NombreCuenta VARCHAR(45)
AS
BEGIN
	SELECT Cuen.Codigo CodigoCuenta, Cuen.Nombre NombreCuenta, SalXCuXMo.Saldo SaldoCuenta, Mon.Acronimo AcronimoMoneda, Mon.EsLocal
	FROM dbo.FIN_Cuenta Cuen
	INNER JOIN dbo.FIN_IdentificadorCuenta IdeCuen ON IdeCuen.IdIdentificadorCuenta = Cuen.IdIdentificadorCuenta
	INNER JOIN dbo.FIN_SaldoXCuentaXMoneda SalXCuXMo ON SalXCuXMo.IdCuenta = Cuen.IdCuenta
	INNER JOIN dbo.FIN_Moneda Mon ON Mon.IdMoneda = SalXCuXMo.IdMoneda
	WHERE Cuen.Nombre=@NombreCuenta AND Mon.EsSistema=1
	AND Cuen.IdCuenta NOT IN
	(
		SELECT IdCuentaPadre FROM dbo.FIN_Cuenta
	)
	ORDER BY Cuen.Codigo
END
RETURN 0