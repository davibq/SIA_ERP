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
                                                              new SqlParameter("pImagen", pArticulo.imagen),
                                                              new SqlParameter("pCodBodega", pArticulo.bodega.Codigo),
                                                              new SqlParameter("pCodExistencias", pArticulo.Existencias.Codigo),
                                                              new SqlParameter("pCodVentas", pArticulo.Ventas.Codigo),
                                                              new SqlParameter("pCodCostos", pArticulo.Costos.Codigo)
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

        public List<Articulo> obtenerArticulos()
        {
            var articulos = new List<Articulo>();
            var ds = EjecutarConsulta("dbo.ObtenerArticulos", new List<SqlParameter>() { });
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    articulos.Add(new Articulo()
                    {
                        Nombre = row["Nombre"].ToString(),
                        Codigo = row["Codigo"].ToString(),
                        Descripcion = row["Descripcion"].ToString(),
                        unidadMedida = new UnidadMedida() { Nombre = row["UnidadMedidad"].ToString() },
                        UrlImagen = row["UrlFotografia"].ToString(),
                        Precio = row["Costo"].ToString()
                    });
                }
            }
            return articulos;
        }

        public List<Banco> obtenerBancos()
        {
            var bancos = new List<Banco>();
            var ds = EjecutarConsulta("dbo.obtenerBancos", new List<SqlParameter>() { });
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    bancos.Add(new Banco()
                    {
                        idBanco = int.Parse(row["idBanco"].ToString()),
                        Nombre = row["Nombre"].ToString(),
                        AcronimoMoneda = row["Moneda"].ToString(),
                        NoCuenta = row["NoCuenta"].ToString(),
                        CuentaMayor = row["CuentaMayor"].ToString()
                    });
                }
            }
            return bancos;
        }

    }
}
