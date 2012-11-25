using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SIA.DataAccess;
using SIA.Libreria;
using System.Configuration;
using System.Collections.ObjectModel;

namespace DataAccessInventario
{
    public class DAInventario : DataAccess
    {
        // Inicializa el Connection String en el padre.
        // El connectionString lo toma de Web.config
        #region Constructor

        public DAInventario(string pBaseDatos)
            : base(string.Format(ConfigurationManager.ConnectionStrings["CompraVenta"].ConnectionString, pBaseDatos))
        {

        }

        #endregion

        public IEnumerable<UnidadMedida> obtenerUnidadesMedida()
        {
            var unidadesMedida = new List<UnidadMedida>();
            var ds = EjecutarConsulta("dbo.ObtenerUnidadesdeMedida", new List<SqlParameter>(){});
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    unidadesMedida.Add(new UnidadMedida()
                    {
                        Nombre = row["Nombre"].ToString(),
                    });
                }
            }
            return unidadesMedida;
        }

        public List<Bodega> obtenerBodegas()
        {
            var bodegas = new List<Bodega>();
            var ds = EjecutarConsulta("dbo.ObtenerBodegas", new List<SqlParameter>() { });
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    bodegas.Add(new Bodega()
                    {
                        Codigo = row["Codigo"].ToString(),
                        Nombre = row["Nombre"].ToString(),
                    });
                }
            }
            return bodegas;
        }

        public bool crearArticulo(Articulo pArticulo) 
        {
            return EjecutarNoConsulta("dbo.CrearArticulo", new List<SqlParameter>()
                                                          {
                                                              new SqlParameter("pCodigo", pArticulo.Codigo),
                                                              new SqlParameter("pDescripcion", pArticulo.Descripcion),
                                                              new SqlParameter("pUnidadMedida", pArticulo.unidadMedida.Nombre),
                                                              new SqlParameter("pComentarios", pArticulo.Comentarios),
                                                              new SqlParameter("pImagen", pArticulo.imagen)
                                                          });
        }

        public bool crearBodega(Bodega pBodega)
        {
            return EjecutarNoConsulta("dbo.CrearBodega", new List<SqlParameter>()
                                                          {
                                                              new SqlParameter("pCodigo", pBodega.Codigo),
                                                              new SqlParameter("pNombre", pBodega.Nombre)
                                                          });
        }

    }
}
