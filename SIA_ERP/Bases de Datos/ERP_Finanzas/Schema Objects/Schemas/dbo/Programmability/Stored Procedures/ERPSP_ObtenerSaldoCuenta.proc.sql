CREATE PROCEDURE [dbo].[ERPSP_ObtenerSaldoCuenta]
	@CodigoCuentaSN varchar(15),
	@IdMoneda int
AS
BEGIN
	SELECT Cuen.Codigo CodigoCuenta, Cuen.Nombre NombreCuenta, SalXCuXMo.Saldo SaldoCuenta, Mon.Acronimo AcronimoMoneda, Mon.EsLocal
	FROM dbo.FIN_Cuenta Cuen
	INNER JOIN dbo.FIN_IdentificadorCuenta IdeCuen ON IdeCuen.IdIdentificadorCuenta = Cuen.IdIdentificadorCuenta
	INNER JOIN dbo.FIN_SaldoXCuentaXMoneda SalXCuXMo ON SalXCuXMo.IdCuenta = Cuen.IdCuenta
	INNER JOIN dbo.FIN_Moneda Mon ON Mon.IdMoneda = SalXCuXMo.IdMoneda
	WHERE Cuen.Codigo= @CodigoCuentaSN AND Mon.IdMoneda=@IdMoneda
	AND Cuen.IdCuenta NOT IN
	(
		SELECT IdCuentaPadre FROM dbo.FIN_Cuenta
	)
	ORDER BY Cuen.Codigo
END
RETURN 0
