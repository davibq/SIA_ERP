CREATE PROCEDURE [dbo].[obtenerBancos]
AS
BEGIN
	SET NOCOUNT ON;

    SELECT idBanco, Nombre, Moneda, NoCuenta, CuentaMayor FROM Bancos;
END
RETURN 0