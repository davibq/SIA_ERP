CREATE PROCEDURE [dbo].[SetearFactura]
	@IdDocumento	INT,
	@Estado			VARCHAR(50)
AS
BEGIN

	--Variables de SP
	DECLARE @IdEstado INT
	
	SELECT @IdEstado = IdEstado FROM dbo.EstadoDocumento WHERE Detalle = @Estado

	UPDATE dbo.Documento SET IdEstado = @IdEstado
	WHERE IdDocumento = @IdDocumento
END
RETURN 0