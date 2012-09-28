-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 27/09/2012
-- Descripcion: Obtiene la informacíon de asientos activos de un periodo contable
-----------------------------------------------------------
CREATE PROCEDURE [dbo].[ERPSP_ObtenerAsientosPeriodoContable]
	@Anyo	INT
AS 
	SELECT Asi.Debe, Asi.Haber, Asi.FechaContabilizacion, Asi.FechaDocumento, TipoAsi.Nombre NombreTipo, TipoAsi.Descripcion DescripcionTipo,
	Asi.Referencia1, Asi.Referencia2, Cuen.Codigo, Cuen.Nombre, CuXAs.AlDebe, MonXMonXAsi.Monto, Mon.Acronimo,
	CASE
		WHEN Mon.EsLocal = 1 THEN 'Local'
		WHEN Mon.EsSistema = 1 THEN 'Sistema'
		ELSE ''
	END MonedaAplica ,Hist.TipoCambio, Hist.Fecha FechaTipoCambio
	FROM dbo.FIN_Asiento Asi
	INNER JOIN dbo.FIN_PeriodoContable PerCon ON PerCon.IdPeriodoContable = Asi.IdPeriodoContable
	INNER JOIN dbo.FIN_TipoAsiento TipoAsi ON TipoAsi.IdTipoAsiento = Asi.IdTipoAsiento
	INNER JOIN dbo.FIN_CuentasXAsiento CuXAs ON CuXAs.IdAsiento = Asi.IdAsiento
	INNER JOIN dbo.FIN_Cuenta Cuen ON Cuen.IdCuenta = CuXAs.IdCuenta
	INNER JOIN dbo.FIN_MontosXMonedaXAsientos MonXMonXAsi ON MonXMonXAsi.IdAsiento = Asi.IdAsiento AND Cuen.IdCuenta = MonXMonXAsi.IdCuenta
	INNER JOIN dbo.FIN_Moneda Mon ON Mon.IdMoneda = MonXMonXAsi.IdMoneda
	INNER JOIN dbo.FIN_HistorialTipoCambio Hist ON Hist.IdMonedaOrigen = Mon.IdMoneda AND Hist.IdMonedaDestino = Mon.IdMoneda
	WHERE Asi.Enabled = 1 AND Cuen.Enabled = 1 AND PerCon.Anyo = Anyo
	ORDER BY Asi.FechaContabilizacion
RETURN 0
