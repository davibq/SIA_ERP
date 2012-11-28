CREATE PROCEDURE [dbo].[ObtenerSaldo_LimiteCredito]
	@CodigoCliente VARCHAR(15)
AS
	SELECT SocNeg.LimiteCredito, ISNULL(ConsultaSaldo.Saldo, 0) Saldo FROM dbo.SocioNegocio SocNeg
	LEFT JOIN (
		SELECT SUM(DetDoc.Total) Saldo, Doc.IdSocioNegocio FROM dbo.Documento Doc
		INNER JOIN TipoDocumento TipoDoc ON TipoDoc.IdTipoDocumento = Doc.IdTipoDocumento 
		INNER JOIN DetalleDocumento DetDoc ON DetDoc.IdDetalle = Doc.IdDetalleProductos
		WHERE TipoDoc.Nombre = 'Factura de Clientes'
		GROUP BY Doc.IdSocioNegocio
	) AS ConsultaSaldo ON  ConsultaSaldo.IdSocioNegocio = SocNeg.IdSocioNegocio
	WHERE SocNeg.Codigo = @CodigoCliente
RETURN 0