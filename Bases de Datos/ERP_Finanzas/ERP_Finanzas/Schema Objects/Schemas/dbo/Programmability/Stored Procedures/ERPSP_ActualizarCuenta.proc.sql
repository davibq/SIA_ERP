-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 27/09/2012
-- Descripcion: Inserta o actualiza una entidad
-- @Saldos: Si es vacio se ignora, sino inserta si no existe la cuenta
-- '<Saldos>
--		<Saldo monto="10000.00" moneda="CRC"/>
--		<Saldo monto="5490.98" moneda="YEN"/>
--  </Saldos>'
-- @Nombres: Si es vacio se ignora, sino inserta o actualiza
-- '<Nombres>
--		<Nombre nombre="" idioma="es"/>
--		<Nombre nombre="" idioma="en-US"/>
--  </Nombres>'
-----------------------------------------------------------
CREATE PROCEDURE [dbo].[ERPSP_ActualizarCuenta]
	@Nombre			VARCHAR(45),
	@Codigo			VARCHAR(15),
	@Nivel			INT,
	@Enabled		BIT,
	@CuentaPadre	VARCHAR(15),
	@Identificador	VARCHAR(30),
	@SaldosMonedas	VARCHAR(1000)='',
	@Nombres		VARCHAR(1000)=''
AS 
BEGIN
	
	SET NOCOUNT ON

	--Variables para manejo de SP transaccional y errores
	DECLARE @InicieTransaccion BIT
	DECLARE @ErrorNumber INT, @ErrorSeverity INT, @ErrorState INT
	DECLARE @Message VARCHAR(200)
	
	--Variables de SP
	DECLARE @XmlSaldos XML
	DECLARE @XmlNombre XML
	DECLARE @IdCuenta INT
	DECLARE @IdIdentificador INT
	DECLARE @IdCuentaPadre INT
	
	--Se extraen ids necesarios
	SELECT @IdCuenta = IdCuenta FROM dbo.FIN_Cuenta WHERE Codigo = @Codigo
	SELECT @IdIdentificador = IdIdentificadorCuenta FROM dbo.FIN_IdentificadorCuenta WHERE Nombre = @Identificador
	SELECT @IdCuentaPadre = IdCuenta FROM dbo.FIN_Cuenta WHERE Codigo = @CuentaPadre
	
	SET @InicieTransaccion = 0
	IF @@TRANCOUNT=0 BEGIN
		SET @InicieTransaccion = 1
		SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		BEGIN TRANSACTION		
	END
	
	BEGIN TRY
	
		SET @XmlSaldos = @SaldosMonedas
		SET @XmlNombre = @Nombres
		
		IF NOT EXISTS (SELECT IdCuenta FROM dbo.FIN_Cuenta WHERE IdCuenta = @IdCuentaPadre) BEGIN
			SET @IdCuentaPadre = -1
		END
		
		--Si no existe la cuenta la inserta
		IF NOT EXISTS (SELECT IdCuenta FROM dbo.FIN_Cuenta WHERE IdCuenta = @IdCuenta) BEGIN
			INSERT INTO dbo.FIN_Cuenta (Nombre, Codigo, Enabled, Nivel, IdCuentaPadre) 
			VALUES (@Nombre, @Codigo, @Enabled, @Nivel, @IdCuentaPadre)
			
			SET @IdCuenta = SCOPE_IDENTITY()
			
			--Inserta saldos de cuenta en caso de no existir
			IF (LEN(@SaldosMonedas) > 0) BEGIN
				INSERT INTO dbo.FIN_SaldoXCuentaXMoneda (IdCuenta, IdMoneda, Saldo)
					SELECT @IdCuenta, Mon.IdMoneda, XmlSaldos.Saldo FROM
					(
						SELECT DISTINCT 
						       Saldos.Saldo.value('@monto', 'DECIMAL(14,2)') Saldo,
							   Saldos.Saldo.value('@moneda', 'VARCHAR(3)') XmlAcronimo
						FROM @XmlSaldos.nodes('/Saldos/Saldo') AS Saldos(Saldo)
					) AS XmlSaldos
					INNER JOIN dbo.FIN_Moneda Mon ON Mon.Acronimo = XmlSaldos.XmlAcronimo
					WHERE Mon.IdMoneda NOT IN 
					(
						SELECT IdMoneda FROM dbo.FIN_SaldoXCuentaXMoneda WHERE IdCuenta = @IdCuenta
						AND IdMoneda = Mon.IdMoneda 
					)
			END	
		END
		
		--Actualiza los nombres en idiomas de la cuenta en caso de haber.
		IF (LEN(@Nombres) > 0) BEGIN
		
			--Primero se insertan los idiomas en caso de existir nuevos
			INSERT INTO dbo.FIN_Idioma
				SELECT XmlIdiomas FROM
				(
					SELECT DISTINCT Nombres.Nombre.value('@idioma', 'VARCHAR(5)') XmlIdiomas
					FROM @XmlNombre.nodes('/Nombres/Nombre') AS Nombres(Nombre)
				) AS XmlNombres
				WHERE XmlNombres.XmlIdiomas NOT IN 
				(
					SELECT Nombre FROM dbo.FIN_Idioma WHERE Nombre = XmlNombres.XmlIdiomas
				)
				
			--Se actualizan los nombres en los idiomas
			UPDATE dbo.FIN_NombreIdioma SET Nombre = Nombres.XmlNombre FROM dbo.FIN_NombreIdioma NomIdi
			INNER JOIN(
				SELECT IdIdioma, XmlNombres.XmlNombre FROM
				(
					SELECT DISTINCT Nombres.Nombre.value('@idioma', 'VARCHAR(5)') XmlIdiomas,
									Nombres.Nombre.value('@nombre', 'VARCHAR(15)') XmlNombre
					FROM @XmlNombre.nodes('/Nombres/Nombre') AS Nombres(Nombre)
				) AS XmlNombres
				INNER JOIN dbo.FIN_Idioma Idi ON Idi.Nombre = XmlNombres.XmlIdiomas
			) Nombres ON Nombres.IdIdioma = NomIdi.IdIdioma AND NomIdi.IdCuenta = @IdCuenta
				
			-- Se insertan los nuevos nombres en los idiomas
			INSERT INTO dbo.FIN_NombreIdioma 
				SELECT Idi.IdIdioma, @IdCuenta, XmlNombres.XmlNombre FROM
				(
					SELECT DISTINCT Nombres.Nombre.value('@idioma', 'VARCHAR(5)') XmlIdiomas,
									Nombres.Nombre.value('@nombre', 'VARCHAR(15)') XmlNombre
					FROM @XmlNombre.nodes('/Nombres/Nombre') AS Nombres(Nombre)
				) AS XmlNombres
				INNER JOIN dbo.FIN_Idioma Idi ON Idi.Nombre = XmlNombres.XmlIdiomas
				WHERE XmlNombres.XmlNombre NOT IN
				( 
					SELECT NomIdi.Nombre FROM dbo.FIN_NombreIdioma NomIdi 
					WHERE NomIdi.Nombre = XmlNombres.XmlNombre AND IdCuenta = @IdCuenta
					AND XmlNombres.XmlIdiomas = Idi.Nombre
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