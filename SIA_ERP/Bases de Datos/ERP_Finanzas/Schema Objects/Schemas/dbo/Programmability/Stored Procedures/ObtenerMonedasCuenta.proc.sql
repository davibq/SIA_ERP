CREATE PROCEDURE [dbo].[ObtenerMonedasCuenta]
	@pNombreCuenta VARCHAR(50)
AS
	SELECT Mo.Nombre, Mo.idBCCR, Mo.Acronimo FROM FIN_Moneda Mo
	INNER JOIN FIN_SaldoXCuentaXMoneda SXC ON SXC.IdMoneda=Mo.IdMoneda
	INNER JOIN FIN_Cuenta Cu ON SXC.IdMoneda=sxc.IdMoneda
	WHERE Cu.Nombre=@pNombreCuenta AND Cu.IdCuenta=SXC.IdCuenta
RETURN 0