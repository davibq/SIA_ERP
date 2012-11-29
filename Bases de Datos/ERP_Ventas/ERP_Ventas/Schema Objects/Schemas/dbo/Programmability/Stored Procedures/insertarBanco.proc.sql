CREATE PROCEDURE [dbo].[insertarBanco]
	@Nombre			VARCHAR(50),
	@Moneda			VARCHAR(3),
	@NoCuenta		VARCHAR(25),
	@CuentaMayor	VARCHAR(25)
AS
BEGIN
	SET NOCOUNT ON;

    --Variables para manejo de SP transaccional y errores
	DECLARE @InicieTransaccion BIT
	DECLARE @ErrorNumber INT, @ErrorSeverity INT, @ErrorState INT
	DECLARE @Message VARCHAR(200)
		
	SET @InicieTransaccion = 0
	IF @@TRANCOUNT=0 BEGIN
		SET @InicieTransaccion = 1
		SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		BEGIN TRANSACTION		
	END
	
	BEGIN TRY
		INSERT INTO dbo.Bancos (Nombre, Moneda, NoCuenta, CuentaMayor)
		VALUES (@Nombre,@Moneda,@NoCuenta,@CuentaMayor)
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