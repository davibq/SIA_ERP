CREATE PROCEDURE ObtenerSociosCV
	@pTipoSocio VARCHAR(30)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT IdSocioNegocio, Codigo, SN.Nombre, _Cuenta CC, TSN.Nombre TipoSocio FROM SocioNegocio SN
	INNER JOIN TipoSocioNegocio TSN ON TSN.IdTipoSocio=SN.IdTipoSocio
	WHERE TSN.Nombre=@pTipoSocio
END