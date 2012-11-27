CREATE PROCEDURE dbo.ObtenerDocumentosCompras
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		Documento.IdDocumento, Documento.Consecutivo, 
		TipoDocumento.Nombre TipoDocumento, DD.Total, SN.Nombre
	FROM Documento
	INNER JOIN TipoDocumento ON TipoDocumento.IdTipoDocumento=Documento.IdTipoDocumento
	INNER JOIN DetalleDocumento DD ON DD.IdDetalle=Documento.IdDetalleProductos
	INNER JOIN SocioNegocio SN ON SN.IdSocioNegocio=Documento.IdSocioNegocio
	WHERE TipoDocumento.IdTipoDocumento<4
END