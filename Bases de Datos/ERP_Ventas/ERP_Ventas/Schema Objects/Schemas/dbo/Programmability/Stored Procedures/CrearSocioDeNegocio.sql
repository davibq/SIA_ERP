CREATE PROCEDURE [dbo].[CrearSocioDeNegocio]
	@Codigo varchar(15),
	@NombreSN varchar(40),
	@Email varchar(40),
	@NombreTipo varchar(10),
	@IdMoneda int,
	@CuentaDeMayor varchar(15),
	@LimiteCredito decimal(10,2)
	
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRANSACTION
	
	declare @IdTipoSocio int;
	set @IdTipoSocio = (SELECT TipoSocioNegocio.IdTipoSocio FROM TipoSocioNegocio WHERE TipoSocioNegocio.Nombre = @NombreTipo);
	
	INSERT INTO SocioNegocio(Codigo, Nombre, Email, IdTipoSocio, IDMoneda, _Cuenta, LimiteCredito)
	VALUES (@Codigo, @NombreSN, @Email, @IdTipoSocio, @IdMoneda, @CuentaDeMayor, @LimiteCredito)

	COMMIT TRANSACTION

END
