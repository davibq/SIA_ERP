CREATE PROCEDURE [dbo].[obtenerAsientos]
	AS
BEGIN
	
	SELECT Asi.IdAsiento IdAsiento, Asi.FechaContabilizacion Fecha, Cuen.Nombre NombreCuenta, CxA.AlDebe AlDebe, MxMxA.Monto Monto 
	FROM dbo.FIN_ASIENTO Asi 
	INNER JOIN dbo.FIN_CuentasXAsiento CxA ON Asi.IdAsiento=CxA.IdAsiento
	INNER JOIN dbo.FIN_Cuenta Cuen ON CxA.IdCuenta=Cuen.IdCuenta
	INNER JOIN dbo.FIN_MontosXMonedaXAsientos MxMxA ON MxMxA.IdAsiento=Asi.IdAsiento AND MxMxA.IdCuenta=Cuen.IdCuenta
	INNER JOIN dbo.FIN_Moneda Mon ON Mon.IdMoneda=MxMxA.IdMoneda
	WHERE Mon.EsSistema=1
	ORDER BY Asi.IdASiento
END
RETURN 0