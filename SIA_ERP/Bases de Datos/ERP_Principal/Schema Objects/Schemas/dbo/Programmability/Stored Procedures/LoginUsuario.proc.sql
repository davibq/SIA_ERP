CREATE PROCEDURE [dbo].[LoginUsuario]
	@pNombreUsuario VARCHAR(50),
	@pPassword VARBINARY(256),
	@pNombreEmpresa VARCHAR(50)
AS
BEGIN
	DECLARE @Valido INT
		
	SELECT @Valido=ISNULL(IdUsuario, -1) FROM dbo.ERP_Usuarios
	WHERE [Login]=@pNombreUsuario AND [Password]=@pPassword
	
	IF (@Valido<>-1)
	BEGIN
		SELECT NombreBD FROM dbo.ERP_Modulos Mo
		INNER JOIN dbo.ERP_Entidades En ON En.IdEntidad=Mo.IdEntidad
		WHERE En.Nombre=@pNombreEmpresa
	END
	ELSE
	BEGIN
		SELECT @Valido
	END
END
