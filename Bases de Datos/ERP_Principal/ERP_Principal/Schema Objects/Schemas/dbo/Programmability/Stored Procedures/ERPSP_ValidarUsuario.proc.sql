-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 23/09/2012
-- Descripcion: Valida la sesión de un usuario a un módulo
-----------------------------------------------------------
CREATE PROCEDURE [dbo].[ERPSP_ValidarUsuario]
	@Login VARCHAR(256),
	@Pass VARCHAR(256),
	@Modulo VARCHAR(50),
	@Entidad VARCHAR(35)
AS 
	SELECT Tipo.Nombre TipoUsuario, Perm.Nombre Permiso, Perm.Descripcion  FROM dbo.ERP_Entidades Ent
	INNER JOIN dbo.ERP_Modulos Mod ON Mod.IdEntidad = Ent.IdEntidad
	INNER JOIN dbo.ERP_UsuarioXModulo UsuXMod ON UsuXMod.IdModulo = Mod.IdModulo
	INNER JOIN dbo.ERP_Usuarios Usu ON Usu.IdUsuario = UsuXMod.IdUsuario
	INNER JOIN dbo.ERP_TipoUsuario Tipo ON Tipo.IdTipoUsuario = UsuXMod.IdTipoUsuario
	LEFT JOIN dbo.ERP_PermisosXTipoUsuario PermXTipo ON PermXTipo.IdTipoUsuario = Tipo.IdTipoUsuario
	LEFT JOIN dbo.ERP_Permisos Perm ON Perm.IdPermisos = PermXTipo.IdPermisos
	WHERE Mod.Nombre = @Modulo AND Ent.Nombre = @Entidad AND Usu.Login = CONVERT(VARBINARY, @Login)
	AND Usu.Password = CONVERT(VARBINARY, @Pass) AND Mod.Enabled = 1 AND Ent.Enabled = 1
	AND UsuXMod.Enabled = 1 AND Usu.Enabled = 1 
RETURN 0