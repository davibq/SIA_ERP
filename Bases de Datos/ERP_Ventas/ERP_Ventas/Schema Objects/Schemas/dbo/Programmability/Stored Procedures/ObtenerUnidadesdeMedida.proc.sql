CREATE PROCEDURE [dbo].[ObtenerUnidadesdeMedida]
	AS
BEGIN
	SELECT Nombre FROM dbo.UnidadMedida
END
RETURN 0