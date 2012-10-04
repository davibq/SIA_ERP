CREATE PROCEDURE [dbo].[ObtenerEntidades]
	
AS
BEGIN
	SELECT Nombre, CedulaJuridica FROM ERP_Entidades
END