-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 23/09/2012
-- Descripcion: Obtiene los datos de una entidad 
-----------------------------------------------------------
CREATE PROCEDURE [dbo].[ERPSP_ObtenerDatosEntidad]
	@Entidad VARCHAR(35)
AS 
	SELECT Ent.Nombre Entidad, Ent.CedulaJuridica, Ent.Logo, Pai.Nombre Pais, Con.Nombre Contacto, Con.Descripcion ValorContacto
	FROM dbo.ERP_Entidades Ent
	INNER JOIN dbo.ERP_ContactoXEntidad ConXEnt ON ConXEnt.IdEntidad = Ent.IdEntidad
	INNER JOIN dbo.ERP_Contacto Con ON Con.IdContacto = ConXEnt.IdContacto
	INNER JOIN dbo.ERP_EntidadXPais EntXPai ON EntXPai.IdEntidad = Ent.IdEntidad
	INNER JOIN dbo.ERP_Paises Pai ON Pai.IdPais = EntXPai.IdPais
	WHERE Ent.Nombre = @Entidad AND Ent.Enabled = 1 AND EntXPai.Enabled = 1
	AND ConXEnt.Enabled = 1 AND Con.Enabled = 1
RETURN 0