CREATE PROCEDURE [dbo].[CrearArticulo]
	@pCodigo		VARCHAR(20),
	@pDescripcion	VARCHAR(75),
	@pUnidadMedida	VARCHAR(40),
	@pComentarios	VARCHAR(150),
	@pImagen		VARBINARY(MAX)
AS
BEGIN
	
	SET NOCOUNT ON;

    --Variables para manejo de SP transaccional y errores
	DECLARE @InicieTransaccion BIT
	DECLARE @ErrorNumber INT, @ErrorSeverity INT, @ErrorState INT
	DECLARE @Message VARCHAR(200)
	
	--Variables de SP
	DECLARE @IdArticulo INT
	DECLARE @IdUnidadMedida INT
		
	SET @InicieTransaccion = 0
	IF @@TRANCOUNT=0 BEGIN
		SET @InicieTransaccion = 1
		SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		BEGIN TRANSACTION		
	END
	
	BEGIN TRY

		IF NOT EXISTS(SELECT IdArticulo FROM dbo.Articulo WHERE Codigo=@pCodigo) BEGIN
	
			IF NOT EXISTS (SELECT idUnidadMedida FROM dbo.UnidadMedida WHERE Nombre=@pUnidadMedida) BEGIN
				INSERT INTO dbo.UnidadMedida (Nombre)
				VALUES (@pUnidadMedida)
				
				SET @IdUnidadMedida = SCOPE_IDENTITY()
			END ELSE BEGIN
				SELECT @IdUnidadMedida = IdUnidadMedida FROM dbo.UnidadMedida WHERE Nombre = @pUnidadMedida
			END
			
			INSERT INTO dbo.Articulo (Codigo, Descripcion, IdUnidadMedida) 
			VALUES (@pCodigo, @pDescripcion, @IdUnidadMedida)
	
			SET @IdArticulo = SCOPE_IDENTITY()
			
			INSERT INTO dbo.FotografiaXArticulo (Fotografia, IdArticulo)
			VALUES (@pImagen, @IdArticulo)
			
			INSERT INTO dbo.ComentariosXArticulo (IdArticulo, Comentario)
			VALUES (@IdArticulo, @pComentarios)

		END ELSE BEGIN
			SELECT @IdArticulo = IdArticulo FROM dbo.Articulo WHERE Codigo = @pCodigo

			UPDATE dbo.Articulo SET Descripcion = @pDescripcion
			WHERE IdArticulo = @IdArticulo
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