-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 24/09/2012
-- Descripcion: Obtiene el catálogo de cuentas activas, con sus respectivos datos asociados
-----------------------------------------------------------
CREATE PROCEDURE [dbo].[ERPSP_ObtenerCatalogoCuentas]
AS 
	SELECT Cuen.IdCuentaPadre, Cuen.Nombre NombreCuenta, Cuen.Codigo CodigoCuenta, Cuen.Nivel NivelCuenta,
	IdeCuen.Nombre NombreIdentificador, IdeCuen.Numero NumeroIdentificador, NomIdi.Nombre NombreIdioma, Idi.Nombre Idioma,
	SalXCuXMo.Saldo SaldoCuenta, Mon.Nombre NombreMoneda, Mon.Acronimo AcronimoMoneda,
	CASE
         WHEN Mon.EsLocal = 1 THEN 'Local'
         WHEN Mon.EsSistema = 1 THEN 'Sistema'
         ELSE '' END MonedaAplica
	FROM dbo.FIN_Cuenta Cuen 
	INNER JOIN dbo.FIN_IdentificadorCuenta IdeCuen ON IdeCuen.IdIdentificadorCuenta = Cuen.IdIdentificadorCuenta
	INNER JOIN dbo.FIN_NombreIdioma NomIdi ON NomIdi.IdCuenta = Cuen.IdCuenta
	INNER JOIN dbo.FIN_Idioma Idi ON Idi.IdIdioma = NomIdi.IdIdioma
	INNER JOIN dbo.FIN_SaldoXCuentaXMoneda SalXCuXMo ON SalXCuXMo.IdCuenta = Cuen.IdCuenta
	INNER JOIN dbo.FIN_Moneda Mon ON Mon.IdMoneda = SalXCuXMo.IdMoneda
	WHERE Cuen.Enabled = 1
	ORDER BY Cuen.Codigo
RETURN 0