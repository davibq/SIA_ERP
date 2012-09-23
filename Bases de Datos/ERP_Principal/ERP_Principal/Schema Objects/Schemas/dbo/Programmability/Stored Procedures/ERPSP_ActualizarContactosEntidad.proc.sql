-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 22/29/2012
-- Descripcion: Inserta o actualiza contactos asociados a una entidad
-- @Contactos: Si es vacio se ignora, sino inserta o actualiza
-- '<Contactos>
--		<Contacto enabled="1" nombre="Telefono" valor="2222-5564" />
--		<Contacto enabled="0" nombre="Fax" valor="2222-5565" />
--  </Contactos>'
-----------------------------------------------------------
CREATE PROCEDURE [dbo].[ERPSP_ActualizarContactosEntidad]
	@Contactos VARCHAR(1000), --Hasta 11 países
	@Nombre VARCHAR(35),
	@IdEntidad INT
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
			INNER JOIN dbo.ERP_ContactoXEntidad ConXEnt ON ConXEnt.IdContacto = Con.IdContacto
			WHERE ConXEnt.IdEntidad = @IdEntidad)
		END
		
		IF LEN(@Contactos)>0 BEGIN
		
			-- Habilitar o deshabilita los contactos asociados a una entidad
			UPDATE dbo.ERP_ContactoXEntidad SET Enabled = XmlEnabled FROM dbo.ERP_ContactoXEntidad ConEnt
			INNER JOIN (
				SELECT IdContacto, XmlEnabled FROM (
					SELECT Contactos.Contacto.value('@nombre','varchar(35)')  XmlContacto,
					       Contactos.Contacto.value('@enabled','BIT') XmlEnabled
					FROM @XmlContactos.nodes('/Contactos/Contacto') AS Contactos(Contacto)
				) AS XmlContactos
				INNER JOIN dbo.ERP_Contacto Contactos ON Contactos.Nombre = XmlContactos.XmlContacto
			) Contactos ON Contactos.IdContacto = ConEnt.IdContacto AND ConEnt.IdEntidad = @IdEntidad

			-- Asocia nuevos contactos
			INSERT INTO dbo.ERP_ContactoXEntidad 
			SELECT @IdEntidad, IdContacto, Enabled FROM 
			(
				SELECT Contactos.Contacto.value('@nombre','VARCHAR(20)') Contacto
				FROM @XmlContactos.nodes('/Contactos/Contacto') AS Contactos(Contacto)
			) AS XmlContactos
			INNER JOIN dbo.ERP_Contacto Contactos ON Contactos.Nombre = XmlContactos.Contacto
			WHERE Contactos.IdContacto NOT IN (SELECT IdContacto FROM dbo.ERP_ContactoXEntidad WHERE IdEntidad = @IdEntidad)
		
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