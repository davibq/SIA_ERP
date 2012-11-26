CREATE PROCEDURE dbo.ObtenerDetalle
	@pConsecutivo VARCHAR(15)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT IdDetalle FROM Documento
	INNER JOIN DetalleDocumento ON Documento.IdDetalleProductos=DetalleDocumento.IdDetalle
	WHERE Documento.Consecutivo=@pConsecutivo
END
