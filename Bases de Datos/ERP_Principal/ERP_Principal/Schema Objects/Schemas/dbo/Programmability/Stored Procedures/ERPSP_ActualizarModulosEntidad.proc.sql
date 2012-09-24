-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 23/09/2012
-- Descripcion: Inserta o actualiza modulos asociados a una entidad
-- @Modulos: Si es vacio se ignora, sino inserta o actualiza
-- '<Modulos>
--		<Modulo enabled="1" nombre="Finanzas" base="ERP_Finanzas" />
--		<Modulo enabled="0" nombre="Recursos" base="ERP_Recursos" />
--  </Modulos>'
-----------------------------------------------------------
CREATE PROCEDURE [dbo].[ERPSP_ActualizarModulosEntidad]
	@Modulos VARCHAR(1000), -- Hasta 14 modulos
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
		
		--Asocia nuevos modulos a una entidad
		IF LEN(@Modulos) > 0 BEGIN
		
			INSERT INTO dbo.ERP_Modulos(Nombre, NombreBD, IdEntidad, Enabled)
				SELECT Modulos.XmlModulo, Modulos.XmlBase, @IdEntidad, Modulos.XmlEnabled FROM 
				(
					SELECT Modulos.Modulo.value('@nombre', 'VARCHAR(50)') XmlModulo,
						   Modulos.Modulo.value('@enabled', 'BIT') XmlEnabled,
						   Modulos.Modulo.value('@base', 'VARCHAR(35)') XmlBase
					FROM @XmlModulos.nodes('/Modulos/Modulo') AS Modulos(Modulo)
				) AS Modulos 
				WHERE Modulos.XmlModulo NOT IN 
				(
					SELECT Mod.Nombre FROM dbo.ERP_Modulos Mod
					INNER JOIN dbo.ERP_Entidades Ent ON Ent.IdEntidad = Mod.IdEntidad
					WHERE Mod.IdEntidad = @IdEntidad
				)
		END
			
		--Actualiza modulos asociados a una entidad
		IF LEN(@Modulos) > 0 BEGIN
			
			UPDATE dbo.ERP_Modulos SET Enabled = XmlModulos.XmlEnabled FROM dbo.ERP_Modulos Mod
			INNER JOIN (
				SELECT Modulos.Modulo.value('@nombre', 'VARCHAR(50)') XmlModulo,
					   Modulos.Modulo.value('@enabled', 'BIT') XmlEnabled
				FROM @XmlModulos.nodes('/Modulos/Modulo') AS Modulos(Modulo)		
			) XmlModulos ON XmlModulos.XmlModulo = Mod.Nombre AND Mod.IdEntidad = @IdEntidad
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