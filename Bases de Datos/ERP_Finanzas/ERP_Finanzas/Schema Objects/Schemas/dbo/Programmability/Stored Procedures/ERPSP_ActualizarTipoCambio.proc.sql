-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 27/09/2012
-- Descripcion: Inserta un tipo de cambio
-- @Monedas: Si es vacio se ignora, sino inserta si no existe la cuenta
-- '<Monedas>
--		<Moneda monto="10000.00" monedaOrigen="CRC" monedaDestino="YEN"/>
--		<Moneda monto="5490.98" monedaOrigen="CRC" monedaDestino="YEN"/>
--  </Monedas>'
-----------------------------------------------------------
CREATE PROCEDURE [dbo].[ERPSP_ActualizarTipoCambio]
	@Monedas	VARCHAR(1000)='',
	@Fecha		DATE
AS 
BEGIN
	
	SET NOCOUNT ON

	--Variables para manejo de SP transaccional y errores
	DECLARE @InicieTransaccion BIT
	DECLARE @ErrorNumber INT, @ErrorSeverity INT, @ErrorState INT
	DECLARE @Message VARCHAR(200)
	
	--Variables de SP
	DECLARE @XmlMonedas XML
	
	SET @InicieTransaccion = 0
	IF @@TRANCOUNT=0 BEGIN
		SET @InicieTransaccion = 1
		SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		BEGIN TRANSACTION		
	END
	
	BEGIN TRY
	
		SET @XmlMonedas = @Monedas
		
		IF (LEN(@Monedas)>0) BEGIN
			
			INSERT INTO dbo.FIN_HistorialTipoCambio (IdMonedaDestino, IdMonedaOrigen, Fecha, TipoCambio)
				SELECT DISTINCT MonedasOrigen.IdMonedaOrigen, IdMonedaDestino, @Fecha, MonedasOrigen.XmlTipoCambio FROM
				(
					SELECT Mon.IdMoneda IdMonedaOrigen, XmlTipoCambio FROM(
						SELECT Monedas.Moneda.value('@monto', 'DECIMAL(14,2)') XmlTipoCambio,
							   Monedas.Moneda.value('@monedaOrigen', 'VARCHAR(3)') XmlMonedaOrigen
						FROM @XmlMonedas.nodes('/Monedas/Moneda') AS Monedas(Moneda)
					) AS XmlMonedasOrigen
					INNER JOIN dbo.FIN_Moneda Mon ON Mon.Acronimo = XmlMonedasOrigen.XmlMonedaOrigen
				) AS MonedasOrigen
				INNER JOIN(
					SELECT Mon.IdMoneda IdMonedaDestino, XmlTipoCambio  FROM(
						SELECT Monedas.Moneda.value('@monto', 'DECIMAL(14,2)') XmlTipoCambio,
							   Monedas.Moneda.value('@monedaDestino', 'VARCHAR(3)') XmlMonedaDestino
						FROM @XmlMonedas.nodes('/Monedas/Moneda') AS Monedas(Moneda)
					) AS XmlMonedasDestino
					INNER JOIN dbo.FIN_Moneda Mon ON Mon.Acronimo = XmlMonedasDestino.XmlMonedaDestino 
				) AS MonedasDestino ON MonedasDestino.XmlTipoCambio = MonedasOrigen.XmlTipoCambio 
				WHERE MonedasOrigen.IdMonedaOrigen NOT IN 
				(
					SELECT IdMonedaOrigen FROM dbo.FIN_HistorialTipoCambio 
					WHERE IdMonedaOrigen = MonedasOrigen.IdMonedaOrigen AND IdMonedaDestino = MonedasDestino.IdMonedaDestino
					AND Fecha = @Fecha
				)
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