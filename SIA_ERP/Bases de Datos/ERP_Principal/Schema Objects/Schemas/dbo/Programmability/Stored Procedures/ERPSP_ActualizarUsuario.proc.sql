-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 23/09/2012
-- Descripcion: Inserta o actualiza un usuario y lo asocia a un modulo
-- @Modulos: Si es vacio se ignora, sino inserta o actualiza
-- '<Modulos>
--		<Modulo enabled="1" nombre="Finanzas" tipo="Administrador" />
--		<Modulo enabled="0" nombre="Recursos" tipo="Supervisor" />
--  </Modulos>'
-- @Contactos: Si es vacio se ignora, sino inserta o actualiza
-- '<Contactos>
--		<Contacto enabled="1" nombre="Telefono" valor="2222-5564" />
--		<Contacto enabled="0" nombre="Fax" valor="2222-5565" />
--  </Contactos>'
-----------------------------------------------------------
CREATE PROCEDURE [dbo].[ERPSP_ActualizarUsuario]
	@Login VARCHAR(256),
	@Pass VARCHAR(256),
	@Contactos VARCHAR(1000)='',
	@Enabled BIT,
	@Modulos VARCHAR(1000)='',
	@Entidad VARCHAR(35),
	@NuevoLogin VARCHAR(256)='',
	@NuevoPass VARCHAR(256)=''
AS 
BEGIN
	
	SET NOCOUNT ON

	--Variables para manejo de SP transaccional y errores
	DECLARE @InicieTransaccion BIT
	DECLARE @ErrorNumber INT, @ErrorSeverity INT, @ErrorState INT
	DECLARE @Message VARCHAR(200)
	
	--Variables de SP
	DECLARE @IdUsuario INT
	DECLARE @IdEntidad INT
	
	--Se extraen ids necesarios
	SELECT @IdEntidad = Ent.IdEntidad FROM dbo.ERP_Entidades Ent WHERE Ent.Nombre = @Entidad 
	SELECT @IdUsuario = IdUsuario FROM dbo.ERP_Usuarios WHERE Login = CONVERT(VARBINARY, @Login) AND Password = CONVERT(VARBINARY, @Pass)
	
	SET @InicieTransaccion = 0
	IF @@TRANCOUNT=0 BEGIN
		SET @InicieTransaccion = 1
		SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		BEGIN TRANSACTION		
	END
	
	BEGIN TRY
		
		IF LEN(@Login) > 0 AND LEN(@Pass) > 0 BEGIN
			IF EXISTS(SELECT IdUsuario FROM dbo.ERP_Usuarios WHERE IdUsuario = @IdUsuario)
			BEGIN
				--Habilita o deshabilita a un usuario
				UPDATE dbo.ERP_Usuarios SET Enabled = @Enabled WHERE IdUsuario = @IdUsuario
				
				--Actualiza el login y el pass en caso de existir contenido
				IF LEN(@NuevoLogin) > 0 AND LEN(@NuevoPass) > 0 BEGIN
					UPDATE dbo.ERP_Usuarios SET Login = CONVERT(VARBINARY, @NuevoLogin), Password = CONVERT(VARBINARY, @NuevoPass)
					WHERE IdUsuario = @IdUsuario
				END
				
			END ELSE BEGIN
				--Inserta al usuario
				INSERT INTO dbo.ERP_Usuarios(Login, Password, Enabled)
				VALUES(CONVERT(VARBINARY,@Login), HASHBYTES('MD5', @Pass), @Enabled)
				
				SET @IdUsuario = SCOPE_IDENTITY()
			END
		END
		
		EXEC dbo.[ERPSP_ActualizarUsuarioXModulo] @Modulos, @IdUsuario, @IdEntidad
		EXEC dbo.[ERPSP_ActualizarContactosUsuario] @Contactos, @IdUsuario

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