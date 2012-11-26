CREATE PROCEDURE [dbo].[CrearBodega]
	@pCodigo	VARCHAR(20),
	@pNombre	VARCHAR(40)
AS
BEGIN
	
	SET NOCOUNT ON;

     --Variables para manejo de SP transaccional y errores
	DECLARE @InicieTransaccion BIT
	DECLARE @ErrorNumber INT, @ErrorSeverity INT, @ErrorState INT
	DECLARE @Message VARCHAR(200)
	
	--Variables de SP
	DECLARE @IdBodega INT
	
	SET @InicieTransaccion = 0
	IF @@TRANCOUNT=0 BEGIN
		SET @InicieTransaccion = 1
		SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		BEGIN TRANSACTION		
	END
	
	BEGIN TRY

		IF NOT EXISTS(SELECT IdBodega FROM dbo.Bodega WHERE Codigo=@pCodigo) BEGIN
		
			INSERT INTO dbo.Bodega (Codigo, Nombre) 
			VALUES (@pCodigo, @pNombre)
	
		END ELSE BEGIN
			SELECT @IdBodega = IdBodega FROM dbo.Bodega WHERE Codigo = @pCodigo

			UPDATE dbo.Bodega SET Nombre = @pNombre
			WHERE IdBodega = @IdBodega
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