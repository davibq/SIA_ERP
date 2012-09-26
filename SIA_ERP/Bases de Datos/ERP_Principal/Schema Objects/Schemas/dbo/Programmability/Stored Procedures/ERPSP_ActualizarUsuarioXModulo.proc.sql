-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 23/09/2012
-- Descripcion: Inserta o actualiza un usuario y lo asocia a un modulo
-- @Modulos: Si es vacio se ignora, sino inserta o actualiza
-- '<Modulos>
--		<Modulo enabled="1" nombre="Finanzas" tipo="Administrador" />
--		<Modulo enabled="0" nombre="Recursos" tipo="Supervisor" />
--  </Modulos>'
-----------------------------------------------------------
CREATE PROCEDURE [dbo].[ERPSP_ActualizarUsuarioXModulo]
	@Modulos VARCHAR(1000),
	@IdUsuario INT,
	@IdEntidad INT
AS 
BEGIN
	
	SET NOCOUNT ON

	--Variables para manejo de SP transaccional y errores
	DECLARE @InicieTransaccion BIT
	DECLARE @ErrorNumber INT, @ErrorSeverity INT, @ErrorState INT
	DECLARE @Message VARCHAR(200)
	
	--Variables de SP
	DECLARE @XmlModulos XML
	
	
	SET @InicieTransaccion = 0
	IF @@TRANCOUNT=0 BEGIN
		SET @InicieTransaccion = 1
		SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		BEGIN TRANSACTION		
	END
	
	BEGIN TRY
		SET @XmlModulos = @Modulos
		
		IF LEN(@Modulos)>0 BEGIN
		
			-- Habilitar o deshabilita los modulos asociados a un usuario y tipo usuario
			UPDATE dbo.ERP_UsuarioXModulo SET Enabled = Modulos.XmlEnabled FROM dbo.ERP_UsuarioXModulo UsuXMod
			INNER JOIN (
				SELECT IdModulo, XmlEnabled, IdTipoUsuario FROM (
					SELECT Modulos.Modulo.value('@nombre','varchar(35)')  XmlModulo,
					       Modulos.Modulo.value('@enabled','BIT') XmlEnabled,
					       Modulos.Modulo.value('@tipo','varchar(40)') XmlTipo
					FROM @XmlModulos.nodes('/Modulos/Modulo') AS Modulos(Modulo)
				) AS XmlModulos
				INNER JOIN dbo.ERP_Modulos Modulos ON Modulos.Nombre = XmlModulos.XmlModulo AND Modulos.IdEntidad = @IdEntidad
				INNER JOIN dbo.ERP_TipoUsuario Tipo ON Tipo.Nombre = XmlModulos.XmlTipo
			) Modulos ON Modulos.IdModulo = UsuXMod.IdModulo AND UsuXMod.IdUsuario = @IdUsuario 
			AND UsuXMod.IdTipoUsuario = Modulos.IdTipoUsuario

			--Asocia nuevos modulos a un usuario
			INSERT INTO dbo.ERP_UsuarioXModulo
			SELECT Modulos.IdModulo, @IdUsuario, XmlModulos.XmlEnabled, Tipo.IdTipoUsuario FROM
			(
				SELECT Modulos.Modulo.value('@nombre','varchar(35)') XmlModulo,
					   Modulos.Modulo.value('@enabled','BIT') XmlEnabled,
					   Modulos.Modulo.value('@tipo','varchar(40)') XmlTipo
				FROM @XmlModulos.nodes('/Modulos/Modulo') AS Modulos(Modulo)
			)AS XmlModulos
			INNER JOIN dbo.ERP_Modulos Modulos ON Modulos.Nombre = XmlModulos.XmlModulo AND Modulos.IdEntidad = @IdEntidad
			INNER JOIN dbo.ERP_TipoUsuario Tipo ON Tipo.Nombre = XmlModulos.XmlTipo
			WHERE Modulos.IdModulo NOT IN 
			( 
			SELECT IdModulo FROM dbo.ERP_UsuarioXModulo WHERE IdUsuario = @IdUsuario 
			AND Tipo.IdTipoUsuario = IdTipoUsuario
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