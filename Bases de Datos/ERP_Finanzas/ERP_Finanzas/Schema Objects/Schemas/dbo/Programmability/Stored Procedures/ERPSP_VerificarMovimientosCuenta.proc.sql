-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 28/09/2012
-- Descripcion: Verifica movimientos en una cuenta
-----------------------------------------------------------
CREATE PROCEDURE [dbo].[ERPSP_VerificarMovimientosCuenta]
	@Codigo VARCHAR(15)
AS
	SELECT COUNT(*) AS NumeroMovimientos FROM dbo.FIN_Cuenta Cuen
	INNER JOIN dbo.FIN_CuentasXAsiento CuxAsi ON CuxAsi.IdCuenta = Cuen.IdCuenta
	WHERE Cuen.Codigo = @Codigo
RETURN 0