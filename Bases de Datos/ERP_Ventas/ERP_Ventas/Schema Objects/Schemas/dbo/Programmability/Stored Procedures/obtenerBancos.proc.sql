<<<<<<< HEAD
﻿CREATE PROCEDURE [dbo].[obtenerBancos]
AS
BEGIN
	SET NOCOUNT ON;

    SELECT idBanco, Nombre, Moneda, NoCuenta, CuentaMayor FROM Bancos;
END
=======
﻿CREATE PROCEDURE [dbo].[obtenerBancos]
AS
BEGIN
	SET NOCOUNT ON;

    SELECT idBanco, Nombre, Moneda, NoCuenta, CuentaMayor FROM Bancos;
END
>>>>>>> 674ab780c3ed7d7b8d2a0823c347c88227c2bea0
RETURN 0