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
                                                              new SqlParameter("pNombre", pArticulo.Nombre),
                                                              new SqlParameter("pCodigo", pArticulo.Codigo),
                                                              new SqlParameter("pDescripcion", pArticulo.Descripcion),
                                                              new SqlParameter("pUnidadMedida", pArticulo.unidadMedida.Nombre),
                                                              new SqlParameter("pComentarios", pArticulo.Comentarios),
                                                              new SqlParameter("pImagen", pArticulo.UrlImagen),
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



        public List<string> obtenerHistorialArticulosCliente(string pCliente)
        {
            List<string> historialArticulos = new List<string>();
            // Se arma un string para ser desplegado de una vez en el cel
            var ds = EjecutarConsulta("dbo.ObtenerHistorialArticulosCliente", new List<SqlParameter>() {
                new SqlParameter("CodigoCliente", pCliente)
            });
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    historialArticulos.Add(
                        String.Format(
                        "Cliente: {0}\n"+
                        "Codigo: {1}"+
                        "Articulo: {2}\n"+
                        "Ultima Venta: {3}\n"+
                        "Cantidad Vendida: {4}"+
                        "Cantidad vendida en ultimos 12 meses: {5}\n"+
                        "Promedio de venta por factura: {6}\n"+
                        "Stock: {7}\n"+
                        "Comprometido: {8}\n"+
                        "Disponible: {9}\n",
                        pCliente,
                        row["Codigo"].ToString(),
                        row["Nombre"].ToString(),
                        row["FechaUltimaVenta"].ToString(),
                        row["CantidadVendida"].ToString(),
                        row["VendidoMeses"].ToString(),
                        row["Promedio"].ToString(),
                        row["Stock"].ToString(),
                        row["Comprometido"].ToString(),
                        row["Disponible"].ToString()
                    ));

                }
            }
            return historialArticulos;
        }

        public List<string> consultarCantidadPrecio(string pCliente, string pArticulo)
        {
            List<string> historialArticulos = new List<string>();
            // Se arma un string para ser desplegado de una vez en el cel
            var ds = EjecutarConsulta("dbo.ConsultarCantidadPrecio", new List<SqlParameter>() { 
                new SqlParameter("CodigoCliente", pCliente),
                new SqlParameter("Articulo", pArticulo),
            });

            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    historialArticulos.Add(
                        String.Format(
                        "Codigo: {0}\n" +
                        "Articulo: {1}" +
                        "Cantidad: {2}\n" +
                        "Precio: {3}\n",
                        row["Codigo"].ToString(),
                        row["Nombre"].ToString(),
                        row["Cantidad"].ToString(),
                        row["Precio"].ToString()
                    ));

                }
            }
            return historialArticulos;
        }

        public bool insertarBanco(Banco pBanco)
        {
            return EjecutarNoConsulta("dbo.insertarBanco", new List<SqlParameter>()
                                    {
                                        new SqlParameter("Nombre", pBanco.Nombre),
                                        new SqlParameter("Moneda", pBanco.AcronimoMoneda),
                                        new SqlParameter("NoCuenta", pBanco.NoCuenta),
                                        new SqlParameter("CuentaMayor", pBanco.CuentaMayor)
                                    });
        }

        public List<Articulo> obtenerArticuloXBodeba(string pCodArticulo, string pCodBodega)
        {
            var articulos = new List<Articulo>();
            var ds = EjecutarConsulta("dbo.obtenerArticuloXBodeba", new List<SqlParameter>() 
                {
                    new SqlParameter("pCodArticulo", pCodArticulo),
                    new SqlParameter("pCodBodega", pCodBodega)
                });
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    articulos.Add(new Articulo()
                    {
                        Stock = int.Parse(row["Stock"].ToString()),
                        Comprometido = int.Parse(row["Comprometido"].ToString()),
                        Solicitado = int.Parse(row["Solicitado"].ToString())
                    });
                }
            }
            return articulos;
        }

        public List<Articulo> obtenerTodosArticulos()
        {
            var articulos = new List<Articulo>();
            var ds = EjecutarConsulta("dbo.obtenerTodosArticulos", new List<SqlParameter>() 
                {});
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    articulos.Add(new Articulo()
                    {
                        Codigo = row["Codigo"].ToString(),
                        Nombre = row["Nombre"].ToString()
                    });
                }
            }
            return articulos;
        }

        #region Métodos App

        public ConsultaSaldo consultarCreditoSaldo(string pCodigoCliente)
        {
            var consultaSaldo = new ConsultaSaldo();
            var ds = EjecutarConsulta("dbo.ObtenerSaldo_LimiteCredito", new List<SqlParameter>()
            {
                new SqlParameter("@CodigoCliente", pCodigoCliente)
            });
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    consultaSaldo.LimiteCredito = Convert.ToDouble(row["LimiteCredito"].ToString());
                    consultaSaldo.Saldo = Convert.ToDouble(row["Saldo"].ToString());
                }
            }
            return consultaSaldo;
        }
        #endregion


    }
}
