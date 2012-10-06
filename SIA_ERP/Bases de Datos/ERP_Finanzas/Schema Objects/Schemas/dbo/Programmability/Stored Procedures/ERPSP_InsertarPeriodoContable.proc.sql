-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 06/10/2012
-- Descripcion: Inserta el periodo Contable, los meses y sus estados
-- @Meses: 
--'<Meses>
-- <Mes Nombre='' FechaInicio="2012-01-01" FechaFin="2012-01-30" Estado="Abierto" /> 
-- </Meses>'
-----------------------------------------------------------
CREATE PROCEDURE [dbo].[ERPSP_InsertarPeriodoContable]
	@FechaInicio	DATE,
	@FechaFinal		DATE,
	@Anyo			INT,
	@Meses			VARCHAR(1300)='' --Hasta 12 meses
AS 
BEGIN
	
	SET NOCOUNT ON

	--Variables para manejo de SP transaccional y errores
	DECLARE @InicieTransaccion BIT
	DECLARE @ErrorNumber INT, @ErrorSeverity INT, @ErrorState INT
	DECLARE @Message VARCHAR(200)
	
	--Variables de SP
	DECLARE @XmlMeses XML
	DECLARE @IdPeriodoContable INT
	
	SET @InicieTransaccion = 0
	IF @@TRANCOUNT=0 BEGIN
		SET @InicieTransaccion = 1
		SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		BEGIN TRANSACTION		
	END
	
	BEGIN TRY
		
		SET @XmlMeses = @Meses
		
		IF NOT EXISTS (SELECT IdPeriodoContable FROM dbo.FIN_PeriodoContable WHERE Anyo = @Anyo AND FechaInicio = @FechaInicio AND FechaFinal = @FechaFinal) 
		BEGIN 
			INSERT INTO dbo.FIN_PeriodoContable (Anyo, FechaInicio, FechaFinal)
			VALUES (@Anyo, @FechaInicio, @FechaFinal)
			
			SET @IdPeriodoContable = SCOPE_IDENTITY()
		END
		
		-- Si hay contenido se procesa, sino se obvia el proceso
		IF (LEN(@Meses) > 0 ) BEGIN
			INSERT INTO dbo.FIN_Mes (IdPeriodoContable, IdEstadoMes, FechaInicio, FechaFinal)
				SELECT @IdPeriodoContable, IdEstadoMes, MesesPeriodo.XmlFechaInicio, MesesPeriodo.XmlFechaFinal FROM (
					SELECT DISTINCT Meses.Mes.value('@FechaInicio', 'DATE') XmlFechaInicio,
									Meses.Mes.value('@FechaFin', 'DATE') XmlFechaFinal,
									Meses.Mes.value('@Estado', 'VARCHAR(30)') XmlEstadoMes
					FROM @XmlMeses.nodes('/Meses/Mes') AS Meses(Mes)
				)AS MesesPeriodo
				INNER JOIN dbo.FIN_EstadoMes EstMes ON EstMes.Descripcion = MesesPeriodo.XmlEstadoMes
		END
		
		IF @InicieTransaccion=1 BEGIN
			COMMIT
		END
	END TRY
	BEGIN CATCH
		SET @ErrorNumber = ERROR_NUMBER()
		SET @ErrorSeverity = ERROR_SEVERITY()
		SET @ErrorState = ERROR_STATE()
		SET @Message = ERROR_MESSAGE()
		
		IF @InicieTransaccion=1 BEGIN
			ROLLBACK
		END
		RAISERROR(@Message, @ErrorSeverity, @ErrorState)
	END CATCH	
END
RETURN 0