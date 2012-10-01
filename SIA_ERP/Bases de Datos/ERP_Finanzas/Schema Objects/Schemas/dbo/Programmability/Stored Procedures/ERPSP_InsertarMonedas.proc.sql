-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 28/09/2012
-- Descripcion: Inserta monedas en caso de existir
-- @Monedas: Si es vacio se ignora, sino inserta si no existe la cuenta
-- '<Monedas>
--		<Moneda nombre="Colon" acronimo="CRC" esLocal="1" esSistema="0"/>
--		<Moneda nombre="Dólar" acronimo="USD" esLocal="0" esSistema="1"/>
--  </Monedas>'
-----------------------------------------------------------
CREATE PROCEDURE [dbo].[ERPSP_InsertarMonedas]
	@Monedas	VARCHAR(1000)=''
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
			INSERT INTO dbo.FIN_Moneda (Nombre, Acronimo, EsLocal, EsSistema)
				SELECT XmlNombre, XmlAcronimo, XmlLocal, XmlSistema FROM(
					SELECT Monedas.Moneda.value('@nombre', 'VARCHAR(20)') XmlNombre,
						   Monedas.Moneda.value('@acronimo', 'VARCHAR(3)') XmlAcronimo,
						   Monedas.Moneda.value('@esLocal', 'BIT') XmlLocal,
						   Monedas.Moneda.value('@esSistema', 'BIT') XmlSistema
					FROM @XmlMonedas.nodes('/Monedas/Moneda') AS Monedas(Moneda)
				) AS Monedas
				WHERE Monedas.XmlAcronimo NOT IN 
				(
					SELECT Acronimo FROM dbo.FIN_Moneda WHERE Acronimo = XmlAcronimo
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