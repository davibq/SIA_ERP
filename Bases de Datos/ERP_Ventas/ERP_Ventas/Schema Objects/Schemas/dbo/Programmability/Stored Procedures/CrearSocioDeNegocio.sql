CREATE PROCEDURE [dbo].[CrearSocioDeNegocio]
	@Codigo varchar(15),
	@NombreSN varchar(40),
	@NombreTipo varchar(10),
	@IdMoneda int,
	@CuentaDeMayor varchar(15)
	
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRANSACTION
	
	declare @IdTipoSocio int;
	set @IdTipoSocio = (SELECT TipoSocioNegocio.IdTipoSocio FROM TipoSocioNegocio WHERE TipoSocioNegocio.Nombre = @NombreTipo);
	
	INSERT INTO SocioNegocio(Codigo, Nombre, IdTipoSocio, IDMoneda, _Cuenta)
	VALUES (@Codigo, @NombreSN, @IdTipoSocio, @IdMoneda, @CuentaDeMayor)

	COMMIT TRANSACTION

END
