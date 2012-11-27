CREATE PROCEDURE [dbo].[insertarTransferencia]
	@TipoTransferencia	VARCHAR(30),
	@CodSN				VARCHAR(15),
	@NumTransferencia	VARCHAR(25),
	@Monto				DECIMAL(12,2),
	@idBanco			INT,	
	@Fecha				DATETIME
AS
BEGIN
	SET NOCOUNT ON;

    --Variables para manejo de SP transaccional y errores
	DECLARE @InicieTransaccion BIT
	DECLARE @ErrorNumber INT, @ErrorSeverity INT, @ErrorState INT
	DECLARE @Message VARCHAR(200)
	
	--Variables de SP
	DECLARE @IdSN INT
	DECLARE @IdTipoTransferencia INT
	DECLARE @CuentaBanco VARCHAR(25)
		
	SET @InicieTransaccion = 0
	IF @@TRANCOUNT=0 BEGIN
		SET @InicieTransaccion = 1
		SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		BEGIN TRANSACTION		
	END
	
	BEGIN TRY

		SELECT @IdSN = IdSocioNegocio FROM dbo.SocioNegocio WHERE Codigo = @CodSN
		SELECT @IdTipoTransferencia = IdTipoTransferencia FROM dbo.TipoTransferencia WHERE Nombre = @TipoTransferencia
		SELECT @CuentaBanco = NoCuenta FROM dbo.Bancos WHERE idBanco = @idBanco
		
		INSERT INTO dbo.Transferencia (IdSocioNegocio, IdTipoTransferencia, NumeroTransferencia, CodigoCuenta, Monto, idBanco, Fecha) 
		VALUES (@IdSN, @IdTipoTransferencia, @NumTransferencia, @CuentaBanco, @Monto, @idBanco, @Fecha)
		
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