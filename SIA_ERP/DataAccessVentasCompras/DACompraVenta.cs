using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SIA.DataAccess;
using System.Configuration;
using SIA.VentaCompra.Libreria;

namespace DataAccessVentasCompras
{
    public class DACompraVenta : DataAccess
    {
        #region Constructor

        public DACompraVenta(string pBaseDatos)
            : base(string.Format(ConfigurationManager.ConnectionStrings["CompraVenta"].ConnectionString, pBaseDatos))
        {

        }

        #endregion

        #region Metodos

        public List<SocNegocio> ObtenerSociosNegocio(string pTipoSocio)
        {
            var socios = new List<SocNegocio>();
            var ds = EjecutarConsulta("dbo.ObtenerSociosCV", new List<SqlParameter>(){
                    new SqlParameter("pTipoSocio", pTipoSocio)
            });
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    socios.Add(new SocNegocio()
                    {
                        IdSocio=int.Parse(row["IdSocioNegocio"].ToString()),
                        Codigo=row["Codigo"].ToString(),
                        Nombre=row["Nombre"].ToString(),
                        TipoSocio = row["TipoSocio"].ToString(),
                        CuentaAsociada = row["CC"].ToString(),
                    });
                }
            }
            return socios;
        }

        public List<ProductoCV> DemeProductos()
        {
            var productos = new List<ProductoCV>();
            var ds = EjecutarConsulta("dbo.ObtenerProductosCV", new List<SqlParameter>());
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var repetido = (from prods in productos
                                    where prods.IdProducto == int.Parse(row["IdArticulo"].ToString())
                                    select prods);
                    if (repetido.Any())
                    {
                        var producto = repetido.First();
                        producto.Bodegas.Add(new BodegaCV()
                        {
                            Nombre = row["Bodega"].ToString(),
                            Costo = double.Parse(row["Costo"].ToString())
                        });
                    }
                    else
                    {
                        productos.Add(new ProductoCV()
                        {
                            IdProducto = int.Parse(row["IdArticulo"].ToString()),
                            Codigo = row["Codigo"].ToString(),
                            Descripcion = row["Descripcion"].ToString(),
                            Bodegas=new List<BodegaCV>()
                        });
                    }
                }
            }
            return productos;
        }

        public bool CrearSocioDeNegocio(string Nombre, string Codigo, string TipoSocio, int IdMoneda, string CuentaAsociada)
        {
            return EjecutarNoConsulta("dbo.CrearSocioDeNegocio", new List<SqlParameter>()
                                                          {
                                                              new SqlParameter("Codigo", Codigo),
                                                              new SqlParameter("NombreSN", Nombre),
                                                              new SqlParameter("NombreTipo", TipoSocio),
                                                              new SqlParameter("IdMoneda", IdMoneda),
                                                              new SqlParameter("CuentaDeMayor", CuentaAsociada),
                                                          });
        }

        public string ObtenerCuentaDeMayorXCodigo(string CodigoSN)
        {
            string Cuenta_Socio = "";
            var ds = EjecutarConsulta("dbo.ObtenerCuentaDeMayorXCodigo", new List<SqlParameter>(){
                    new SqlParameter("CodigoSN", CodigoSN)
            });
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var socio = new SocNegocio();
                    socio.CuentaAsociada = row["CC"].ToString();
                    Cuenta_Socio = socio.CuentaAsociada;
                }
            }
            return Cuenta_Socio;
        }

        public int ObtenerIDMonedaCuentaDeMayorXCodigo(string CodigoSN)
        {
            int IdMoneda_Cuenta_Socio = 0;
            var ds = EjecutarConsulta("dbo.ObtenerCuentaDeMayorXCodigo", new List<SqlParameter>(){
                    new SqlParameter("CodigoSN", CodigoSN)
            });
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var socio = new SocNegocio();
                    socio.IdMoneda = int.Parse(row["IDMoneda"].ToString());
                    IdMoneda_Cuenta_Socio = socio.IdMoneda;
                }
            }
            return IdMoneda_Cuenta_Socio;
        }

        #endregion
    }
}
