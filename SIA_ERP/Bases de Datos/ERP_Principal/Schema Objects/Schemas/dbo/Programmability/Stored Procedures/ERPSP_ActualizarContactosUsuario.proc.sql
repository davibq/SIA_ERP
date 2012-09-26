-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 22/29/2012
-- Descripcion: Inserta o actualiza contactos asociados a un usuario
-- @Contactos: Si es vacio se ignora, sino inserta o actualiza
-- '<Contactos>
--		<Contacto enabled="1" nombre="Telefono" valor="2222-5564" />
--		<Contacto enabled="0" nombre="Fax" valor="2222-5565" />
--  </Contactos>'
-----------------------------------------------------------
CREATE PROCEDURE [dbo].[ERPSP_ActualizarContactosUsuario]
	@Contactos VARCHAR(1000), --Hasta 11 contactos
	@IdUsuario INT
AS 
BEGIN
	
	SET NOCOUNT ON

	--Variables para manejo de SP transaccional y errores
	DECLARE @InicieTransaccion BIT
	DECLARE @ErrorNumber INT, @ErrorSeverity INT, @ErrorState INT
	DECLARE @Message VARCHAR(200)
	
	--Variables de SP
	DECLARE @XmlContactos XML
	
	SET @InicieTransaccion = 0
	IF @@TRANCOUNT=0 BEGIN
		SET @InicieTransaccion = 1
		SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		BEGIN TRANSACTION		
	END
	
	BEGIN TRY
		
		SET @XmlContactos = @Contactos
		
		-- Inserta los contactos
		IF(LEN(@Contactos) > 0) BEGIN
			INSERT INTO dbo.ERP_Contacto(Nombre, Descripcion, Enabled)
			SELECT Nombre, Valor, Enabled FROM (
				SELECT Contactos.Contacto.value('@nombre','varchar(35)')  Nombre,
					   Contactos.Contacto.value('@valor', 'varchar(40)') Valor,
					   Contactos.Contacto.value('@enabled', 'BIT') Enabled
				FROM @XmlContactos.nodes('/Contactos/Contacto') AS Contactos(Contacto)
			) As Contactos 
			WHERE Contactos.Nombre NOT IN 
			(SELECT Con.Nombre FROM dbo.ERP_Contacto Con
			INNER JOIN dbo.ERP_ContactoXUsuario ConXUsu ON ConXUsu.IdContacto = Con.IdContacto
			WHERE ConXUsu.IdUsuario = @IdUsuario)
		END
		
		IF LEN(@Contactos)>0 BEGIN
		
			-- Habilitar o deshabilita los contactos asociados a un usuario
			UPDATE dbo.ERP_ContactoXUsuario SET Enabled = Contactos.XmlEnabled FROM dbo.ERP_ContactoXUsuario ConUsu
			INNER JOIN (
				SELECT IdContacto, XmlEnabled FROM (
					SELECT Contactos.Contacto.value('@nombre','varchar(35)')  XmlContacto,
					       Contactos.Contacto.value('@enabled','BIT') XmlEnabled
					FROM @XmlContactos.nodes('/Contactos/Contacto') AS Contactos(Contacto)
				) AS XmlContactos
				INNER JOIN dbo.ERP_Contacto Contactos ON Contactos.Nombre = XmlContactos.XmlContacto
			) Contactos ON Contactos.IdContacto = ConUsu.IdContacto AND ConUsu.IdUsuario = @IdUsuario

			-- Asocia nuevos contactos
			INSERT INTO dbo.ERP_ContactoXUsuario
			SELECT Contactos.IdContacto, @IdUsuario, XmlContactos.XmlEnabled FROM 
			(
				SELECT Contactos.Contacto.value('@nombre','VARCHAR(20)') Contacto,
				       Contactos.Contacto.value('@enabled','BIT') XmlEnabled
				FROM @XmlContactos.nodes('/Contactos/Contacto') AS Contactos(Contacto)
			) AS XmlContactos
			INNER JOIN dbo.ERP_Contacto Contactos ON Contactos.Nombre = XmlContactos.Contacto
			WHERE Contactos.IdContacto NOT IN (SELECT IdContacto FROM dbo.ERP_ContactoXUsuario WHERE IdUsuario = @IdUsuario)
		
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