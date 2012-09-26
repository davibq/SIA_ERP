-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 22/09/2012
-- Descripcion: Inserta o actualiza una entidad
-- @Contactos: Si es vacio se ignora, sino inserta o actualiza
-- '<Contactos>
--		<Contacto enabled="1" nombre="Telefono" valor="2222-5564" />
--		<Contacto enabled="0" nombre="Fax" valor="2222-5565" />
--  </Contactos>'
-- @Paises: Si es vacio se ignora, sino inserta o actualiza
-- '<Paises>
--     <Pais enabled="1">Costa Rica</Pais>
--     <Pais enabled="0">Guatemala</Pais>
-- </Paises>'
-- @Modulos: Si es vacio se ignora, sino inserta o actualiza
-- '<Modulos>
--		<Modulo enabled="1" nombre="Finanzas" base="ERP_Finanzas" />
--		<Modulo enabled="0" nombre="Recursos" base="ERP_Recursos" />
--  </Modulos>'
-----------------------------------------------------------
CREATE PROCEDURE [dbo].[ERPSP_ActualizarEntidad]
	@Nombre VARCHAR(35),
	@Contactos VARCHAR(1000)='',
	@Logo VARCHAR(MAX) = '',
	@CedulaJuridica VARCHAR(15),
	@Paises VARCHAR(500), --Hasta 11 países
	@Enabled BIT,
	@Modulos VARCHAR(1000)='' -- Hasta 14 modulos
AS 
BEGIN
	
	SET NOCOUNT ON

	--Variables para manejo de SP transaccional y errores
	DECLARE @InicieTransaccion BIT
	DECLARE @ErrorNumber INT, @ErrorSeverity INT, @ErrorState INT
	DECLARE @Message VARCHAR(200)
	
	--Variables de SP
	DECLARE @IdEntidad INT
		
	SET @InicieTransaccion = 0
	IF @@TRANCOUNT=0 BEGIN
		SET @InicieTransaccion = 1
		SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		BEGIN TRANSACTION		
	END
	
	BEGIN TRY

		IF NOT EXISTS(SELECT IdEntidad FROM dbo.ERP_Entidades WHERE Nombre=@Nombre) BEGIN
	
			INSERT INTO dbo.ERP_Entidades (Nombre, CedulaJuridica, Enabled, Logo) 
			VALUES (@Nombre, @CedulaJuridica, @Enabled, CONVERT(VARBINARY,@Logo)) 
	
			SET @IdEntidad = SCOPE_IDENTITY()

		END ELSE BEGIN
			SELECT @IdEntidad = IdEntidad FROM dbo.ERP_Entidades WHERE Nombre = @Nombre

			UPDATE dbo.ERP_Entidades SET Enabled = @Enabled, CedulaJuridica = @CedulaJuridica, Logo = CONVERT(VARBINARY,@Logo)
			WHERE IdEntidad = @IdEntidad

		END	

		EXEC dbo.[ERPSP_ActualizarContactosEntidad] @Contactos, @IdEntidad
		
		EXEC dbo.[ERPSP_ActualizarPaises] @Paises, @IdEntidad

		EXEC dbo.[ERPSP_ActualizarModulosEntidad] @Modulos, @IdEntidad
		
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