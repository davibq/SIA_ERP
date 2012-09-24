-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 22/29/2012
-- Descripcion: Inserta o actualiza paises asociados a una entidad
-- @Paises: Si es vacio se ignora, sino inserta o actualiza
-- '<Paises>
--     <Pais enabled="1">Costa Rica</Pais>
--     <Pais enabled="0">Guatemala</Pais>
-- </Paises>'
-----------------------------------------------------------
CREATE PROCEDURE [dbo].[ERPSP_ActualizarPaises]
	@Paises VARCHAR(500), --Hasta 11 países
	@IdEntidad INT
AS 
BEGIN
	
	SET NOCOUNT ON

	--Variables para manejo de SP transaccional y errores
	DECLARE @InicieTransaccion BIT
	DECLARE @ErrorNumber INT, @ErrorSeverity INT, @ErrorState INT
	DECLARE @Message VARCHAR(200)
	
	--Variables de SP
	DECLARE @XmlPaises XML
	
	SET @InicieTransaccion = 0
	IF @@TRANCOUNT=0 BEGIN
		SET @InicieTransaccion = 1
		SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		BEGIN TRANSACTION		
	END
	
	BEGIN TRY
		
		SET @XmlPaises = @Paises
		
		-- Inserta los paises en caso de no existir
		IF(LEN(@Paises) > 0) BEGIN
			INSERT INTO dbo.ERP_Paises(Nombre)
			SELECT Nombre FROM (
				SELECT Paises.Pais.value('.','VARCHAR(150)')  Nombre
				FROM @XmlPaises.nodes('/Paises/Pais') AS Paises(Pais)
			) As Paises 
			WHERE Paises.Nombre NOT IN (SELECT Nombre FROM dbo.ERP_Paises)
		END
		
		IF LEN(@Paises)>0 BEGIN
		
			-- Habilitar o deshabilita los paises asociados a una entidad
			UPDATE dbo.ERP_EntidadXPais SET Enabled = Paises.Enabled FROM dbo.ERP_EntidadXPais EntPais
			INNER JOIN (
				SELECT IdPais, Enabled FROM (
					SELECT Paises.Pais.value('.','VARCHAR(150)') XmlPais, Paises.Pais.value('@enabled','BIT') Enabled
					FROM @XmlPaises.nodes('/Paises/Pais') AS Paises(Pais)
				) AS XmlPaises
				INNER JOIN dbo.ERP_Paises Paises ON Paises.Nombre = XmlPaises.XmlPais
			) Paises ON Paises.IdPais = EntPais.IdPais AND EntPais.IdEntidad = @IdEntidad

			-- Asocia nuevos paises
			INSERT INTO dbo.ERP_EntidadXPais 
			SELECT @IdEntidad, IdPais, Enabled FROM 
			(
				SELECT Paises.Pais.value('.','VARCHAR(150)') Pais, Paises.Pais.value('@enabled','BIT') Enabled
				FROM @XmlPaises.nodes('/Paises/Pais') AS Paises(Pais)
			) AS XmlPaises
			INNER JOIN dbo.ERP_Paises Paises ON Paises.Nombre = XmlPaises.Pais
			WHERE Paises.IdPais NOT IN (SELECT IdPais FROM dbo.ERP_EntidadXPais WHERE IdEntidad = @IdEntidad)
		
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