CREATE PROCEDURE [dbo].[ObtenerCuentaDeMayorXCodigo]
	@CodigoSN VARCHAR(15)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT IdSocioNegocio, SN.Nombre, SN.IDMoneda, _Cuenta CC FROM SocioNegocio SN
	WHERE SN.Codigo = @CodigoSN
END