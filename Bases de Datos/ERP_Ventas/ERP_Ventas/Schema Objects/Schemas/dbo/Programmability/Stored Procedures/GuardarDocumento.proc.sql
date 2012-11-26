
CREATE PROCEDURE dbo.GuardarDocumento 
	@pTipoDocumento VARCHAR(25),
	@pFecha1 DATE,
	@pFecha2 DATE,
	@pConsecutivo VARCHAR(15),
	@pSubtotal DECIMAL(12, 2),
	@pTotal DECIMAL (12, 2),
	@pEsServicio INT,
	@pDescripcionServicio VARCHAR(50),
	@pCodigoCuentaServicio VARCHAR(15),
	@pIdSocioNegocio INT
AS
BEGIN
	DECLARE @IdEncabezado INT
	DECLARE @IdTipoDocumento INT
	DECLARE @IdDetalleProductos INT
	DECLARE @IdDetalleServicios INT
	SET NOCOUNT ON;
	
	SELECT @IdTipoDocumento=IdTipoDocumento FROM dbo.TipoDocumento
		WHERE Nombre=@pTipoDocumento
	
	INSERT INTO dbo.EncabezadoDocumento (Fecha, Fecha2, CodigoDocumento) VALUES
		(@pFecha1, @pFecha2, @pConsecutivo)
	SET @IdEncabezado = SCOPE_IDENTITY()
	
	IF @pEsServicio=0
	BEGIN
		INSERT INTO dbo.DetalleDocumento(Subtotal, Total) VALUES
			(@pSubtotal, @pTotal)
		SET @IdDetalleProductos=SCOPE_IDENTITY()
		SET @IdDetalleServicios=NULL
	END
	ELSE
	BEGIN
		INSERT INTO dbo.DetalleServicios(Descripcion, CodigoCuenta) VALUES
			(@pDescripcionServicio, @pCodigoCuentaServicio)
		SET @IdDetalleServicios=SCOPE_IDENTITY()
		SET @IdDetalleProductos=NULL
	END
	
	INSERT INTO Documento 
		(Consecutivo, IdEncabezado, IdDetalleProductos, IdDetalleServicios, IdTipoDocumento, IdSocioNegocio)
	VALUES
		(@pConsecutivo, @IdEncabezado, @IdDetalleProductos, @IdDetalleServicios, @IdTipoDocumento, @pIdSocioNegocio)

	return 0;
END

