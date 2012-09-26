-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 23/09/2012
-- Descripcion: Obtiene los datos de un modulo asociado a una entidad 
-----------------------------------------------------------
CREATE PROCEDURE [dbo].[ERPSP_ObtenerDatosModulo]
	@Modulo VARCHAR(50),
	@Entidad VARCHAR(35)
AS 
	SELECT Ent.Nombre Entidad, Mod.Nombre Modulo, Mod.NombreBD Base FROM dbo.ERP_Entidades Ent
	INNER JOIN dbo.ERP_Modulos Mod ON Mod.IdEntidad = Ent.IdEntidad
	WHERE Ent.Nombre = @Entidad AND Mod.Nombre = @Modulo AND Mod.Enabled = 1
	AND Ent.Enabled = 1
RETURN 0