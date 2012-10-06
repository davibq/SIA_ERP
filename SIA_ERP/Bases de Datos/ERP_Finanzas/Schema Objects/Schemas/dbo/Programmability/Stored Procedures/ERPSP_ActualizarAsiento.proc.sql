-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 27/09/2012
-- Descripcion: Inserta o actualiza un asiento contable
-- @Cuentas: Si es vacio se ignora, sino inserta si no existe la cuenta
-- '<Cuentas>
--		<Cuenta monto="10000.00" moneda="CRC" cuenta="" debe="0"/>
--		<Cuenta monto="5490.98" moneda="CRC" cuenta="" debe="1"/>
--  </Cuentas>'
-----------------------------------------------------------
CREATE PROCEDURE [dbo].[ERPSP_ActualizarAsiento]
	@IdAsiento		INT,
	@Fecha			DATE,
	@MontoDebe		DECIMAL(14,2),
	@MontoHaber		DECIMAL(14,2),
	@Referencia1	VARCHAR(20),
	@Referencia2	VARCHAR(20),
	@Enabled		BIT,
	@Cuenta			VARCHAR(1000)='',
	@TipoAsiento	VARCHAR(2)
AS 
BEGIN
	
	SET NOCOUNT ON

	--Variables para manejo de SP transaccional y errores
	DECLARE @InicieTransaccion BIT
	DECLARE @ErrorNumber INT, @ErrorSeverity INT, @ErrorState INT
	DECLARE @Message VARCHAR(200)
	
	--Variables de SP
	DECLARE @XmlCuentas		XML
	DECLARE @IdTipoAsiento	INT
	DECLARE @IdPeriodoContable INT
	--Se extraen ids necesarios
	SELECT @IdTipoAsiento = IdTipoAsiento FROM dbo.FIN_TipoAsiento WHERE Nombre = @TipoAsiento
	SELECT @IdPeriodoContable = IdPeriodoContable FROM dbo.FIN_PeriodoContable WHERE Anyo = YEAR(@Fecha) 
	
	SET @InicieTransaccion = 0
	IF @@TRANCOUNT=0 BEGIN
		SET @InicieTransaccion = 1
		SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		BEGIN TRANSACTION		
	END
	
	BEGIN TRY
	
		SET @XmlCuentas = @Cuenta
		
		--Habilita o deshabilita un asiento sino inserta
		IF EXISTS (SELECT IdAsiento FROM dbo.FIN_Asiento WHERE IdAsiento = @IdAsiento) BEGIN
			UPDATE dbo.FIN_Asiento SET Enabled = @Enabled WHERE IdAsiento = @IdAsiento
		END
		ELSE IF(LEN(@Cuenta) > 0) BEGIN 
		
			INSERT INTO dbo.FIN_Asiento (Debe, Haber, Enabled, FechaContabilizacion, FechaDocumento, IdPeriodoContable, IdTipoAsiento, Referencia1, Referencia2)
			VALUES (@MontoDebe, @MontoHaber, @Enabled, GETDATE(), @Fecha, @IdPeriodoContable, @IdTipoAsiento, @Referencia1, @Referencia2)
				
			SET @IdAsiento = SCOPE_IDENTITY()
		
			INSERT INTO dbo.FIN_CuentasXAsiento (IdAsiento, IdCuenta, AlDebe)
				SELECT @IdAsiento, Cuen.IdCuenta, XmlCuentas.XmlDebe FROM
				(
					SELECT DISTINCT Cuentas.Cuenta.value('@cuenta', 'VARCHAR(15)') XmlCuenta,
									Cuentas.Cuenta.value('@debe', 'BIT') XmlDebe
					FROM @XmlCuentas.nodes('/Cuentas/Cuenta') AS Cuentas(Cuenta)
				)AS XmlCuentas
				INNER JOIN dbo.FIN_Cuenta Cuen ON Cuen.Codigo = XmlCuentas.XmlCuenta
				
			INSERT INTO dbo.FIN_MontosXMonedaXAsientos(IdAsiento, IdCuenta, IdMoneda, Monto)
				SELECT @IdAsiento, Cuen.IdCuenta, Mon.IdMoneda, XmlCuentas.XmlMonto FROM
				(
					SELECT DISTINCT Cuentas.Cuenta.value('@monto','DECIMAL(14,2)') XmlMonto,
									Cuentas.Cuenta.value('@moneda', 'VARCHAR(3)') XmlMoneda,
									Cuentas.Cuenta.value('@cuenta', 'VARCHAR(15)') XmlCuenta
					FROM @XmlCuentas.nodes('/Cuentas/Cuenta') AS Cuentas(Cuenta)
				)AS XmlCuentas
				INNER JOIN dbo.FIN_Cuenta Cuen ON Cuen.Codigo = XmlCuentas.XmlCuenta
				INNER JOIN dbo.FIN_Moneda Mon ON Mon.Acronimo = XmlCuentas.XmlMoneda


			UPDATE dbo.FIN_SaldoXCuentaXMoneda SET Saldo =
			CASE
				WHEN XmlDebe = 1 AND Numero IN (1,6,8,5) THEN (Saldo + XmlMonto)
				WHEN XmlDebe = 0 AND Numero IN (1,6,8,5) THEN (Saldo - XmlMonto)				
				WHEN XmlDebe = 0 AND Numero IN (2,3,4,7) THEN (Saldo + XmlMonto)			
				WHEN XmlDebe = 1 AND Numero IN (2,3,4,7) THEN (Saldo - XmlMonto)
			END 
			FROM dbo.FIN_SaldoXCuentaXMoneda SalXCuenXMon 
			INNER JOIN (
				SELECT Cuen.IdCuenta, Mon.IdMoneda, XmlCuentas.XmlMonto, XmlDebe, IdeCuen.Numero FROM
					(
						SELECT DISTINCT Cuentas.Cuenta.value('@monto','DECIMAL(14,2)') XmlMonto,
										Cuentas.Cuenta.value('@moneda', 'VARCHAR(3)') XmlMoneda,
										Cuentas.Cuenta.value('@cuenta', 'VARCHAR(15)') XmlCuenta,
										Cuentas.Cuenta.value('@debe', 'BIT') XmlDebe
						FROM @XmlCuentas.nodes('/Cuentas/Cuenta') AS Cuentas(Cuenta)
					)AS XmlCuentas
					INNER JOIN dbo.FIN_Cuenta Cuen ON Cuen.Codigo = XmlCuentas.XmlCuenta
					INNER JOIN dbo.FIN_Moneda Mon ON Mon.Acronimo = XmlCuentas.XmlMoneda
					INNER JOIN dbo.FIN_IdentificadorCuenta IdeCuen ON IdeCuen.IdidentificadorCuenta = Cuen.IdIdentificadorCuenta
			) AS Cuentas ON Cuentas.IdCuenta = SalXCuenXMon.IdCuenta AND Cuentas.IdMoneda = SalXCuenXMon.IdMoneda

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