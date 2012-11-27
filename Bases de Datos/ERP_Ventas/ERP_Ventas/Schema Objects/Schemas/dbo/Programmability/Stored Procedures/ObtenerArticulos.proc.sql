CREATE PROCEDURE [dbo].[ObtenerArticulos]
AS
	SELECT DISTINCT Art.Codigo, Art.Nombre, Art.Descripcion, UniMed.Nombre UnidadMedida, FotXArt.UrlFotografia, CosXArtXBod.Costo, CosXArtXBod.FechaActualizacion FROM dbo.Articulo Art
	INNER JOIN dbo.ArticulosXDetalle ArtXDet ON ArtXDet.IdArticulo = Art.IdArticulo
	INNER JOIN dbo.UnidadMedida UniMed ON UniMed.IdUnidadMedida = Art.IdUnidadMedida
	INNER JOIN dbo.FotografiaXArticulo FotXArt ON FotXArt.IdArticulo = Art.IdArticulo
	INNER JOIN dbo.ArticuloXBodega ArtXBod ON ArtXBod.IdArticulo = Art.IdArticulo
	INNER JOIN dbo.CostoXArticuloXBodega CosXArtXBod ON CosXArtXBod.IdBodega = ArtXBod.IdBodega
	ORDER BY CosXArtXBod.FechaActualizacion

RETURN 0