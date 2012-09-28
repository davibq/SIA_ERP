-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 27/09/2012
-- Descripcion: Verifica el estado de un mes
-----------------------------------------------------------
CREATE PROCEDURE [dbo].[ERPSP_VerificarEstadoMes]
	@Fecha DATE
AS
	SELECT EstMes.Descripcion FROM dbo.FIN_Mes Mes
	INNER JOIN dbo.FIN_EstadoMes EstMes ON EstMes.IdEstadoMes = Mes.IdEstadoMes
	WHERE MONTH(Mes.FechaInicio) = MONTH(@Fecha) AND YEAR(Mes.FechaInicio) = YEAR(@Fecha) 
RETURN 0