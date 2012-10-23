CREATE PROCEDURE [dbo].[ObtenerMonedasSistema]
	@Tipo	VARCHAR(20)
AS
BEGIN
	IF @Tipo='Local'
	BEGIN
		SELECT Nombre, Acronimo, idBCCR FROM dbo.FIN_Moneda WHERE EsLocal=1
	END
	ELSE
	BEGIN
		SELECT Nombre, Acronimo, idBCCR FROM dbo.FIN_Moneda WHERE EsSistema=1
	END
END
RETURN 0