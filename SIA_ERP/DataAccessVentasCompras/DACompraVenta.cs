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
                            IdBodega = int.Parse(row["IdBodega"].ToString()),
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
                            Bodegas = new List<BodegaCV>()
                            {
                                new BodegaCV()
                                {
                                    IdBodega = int.Parse(row["IdBodega"].ToString()),
                                    Nombre = row["Bodega"].ToString(),
                                    Costo = double.Parse(row["Costo"].ToString())
                                }
                            },
                            CuentaCostos = row["codCuentasCostos"].ToString(),
                            CuentaExistencias = row["codCuentasExistencias"].ToString(),
                            CuentaVentas = row["codCuentasVentas"].ToString(),
                            CuentaTransitoria = row["codCuentaTransitoria"].ToString()
                        });
                    }
                }
            }
            return productos;
        }

        public bool GuardarDocumento(Documento pDocumento)
        {
            var nuevoDocumento = EjecutarNoConsulta("dbo.GuardarDocumento", new List<SqlParameter>()
            {
                new SqlParameter("pTipoDocumento", pDocumento.TipoDocumento),
                new SqlParameter("pFecha1", pDocumento.Fecha1),
                new SqlParameter("pFecha2", pDocumento.Fecha2),
                new SqlParameter("pConsecutivo", pDocumento.Consecutivo),
                new SqlParameter("pSubtotal", pDocumento.Subtotal),
                new SqlParameter("pTotal", pDocumento.Total),
                new SqlParameter("pEsServicio", pDocumento.EsServicio?1:0),
                new SqlParameter("pDescripcionServicio", pDocumento.DescripcionServicio),
                new SqlParameter("pCodigoCuentaServicio", pDocumento.CodigoCuentaServicio),
                new SqlParameter("pIdSocioNegocio", pDocumento.SocioNegocio.IdSocio)
            });
            bool lineasVenta = true;
            if (!pDocumento.EsServicio)
            {
                var idDetalle = ObtenerIdDetalleDocumento(pDocumento.Consecutivo);
                DataTable dataTable = new DataTable("LineaVenta"); 

                dataTable.Columns.Add("IdArticulo", typeof(int)); 
                dataTable.Columns.Add("IdDetalle", typeof(int));
                dataTable.Columns.Add("IdBodega", typeof(int)); 
                dataTable.Columns.Add("Cantidad", typeof(int)); 
                dataTable.Columns.Add("Impuesto", typeof(double));
                dataTable.Columns.Add("Precio", typeof(double));
                foreach (var lv in pDocumento.LineasVenta)
                {
                    dataTable.Rows.Add(lv.Producto.IdProducto, idDetalle, 
                        lv.Bodega.IdBodega, lv.Cantidad, lv.Impuestos, lv.Producto.Precio);
                }
                var param=new SqlParameter();
                param.ParameterName = "LineasVenta"; 
                param.SqlDbType = SqlDbType.Structured; 
                param.Value = dataTable; 
                lineasVenta = EjecutarNoConsulta("dbo.AgregarLineasVenta", new List<SqlParameter>()
                {
                    param
                });
            }
            return nuevoDocumento && lineasVenta;
        }

        public int ObtenerIdDetalleDocumento(string pConsecutivo)
        {
            var ds = EjecutarConsulta("dbo.ObtenerDetalle", new List<SqlParameter>()
                {
                    new SqlParameter("pConsecutivo", pConsecutivo)
                });
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    return int.Parse(row["IdDetalle"].ToString());
                }
            }
            return -1;
        }

        public bool ModificarCantidadArticulos(List<LineaVenta> pLineasVenta, bool pPositivo, string pCampo)
        {
            DataTable dataTable = new DataTable("LineaVenta"); 
            dataTable.Columns.Add("IdArticulo", typeof(int)); 
            dataTable.Columns.Add("IdDetalle", typeof(int));
            dataTable.Columns.Add("IdBodega", typeof(int)); 
            dataTable.Columns.Add("Cantidad", typeof(int)); 
            dataTable.Columns.Add("Impuesto", typeof(double));
            dataTable.Columns.Add("Precio", typeof(double));

            foreach (var lv in pLineasVenta)
            {
                dataTable.Rows.Add(lv.Producto.IdProducto, 0, 
                    lv.Bodega.IdBodega, lv.Cantidad*(pPositivo?1:-1), lv.Impuestos, 0);
            }
            var param=new SqlParameter();
            param.ParameterName = "pLineasVenta"; 
            param.SqlDbType = SqlDbType.Structured; 
            param.Value = dataTable;
            return EjecutarNoConsulta("dbo.ModificarCantidadArticulos", new List<SqlParameter>(){
               param,
               new SqlParameter("pCampo", pCampo)
            });
        }

        public List<Documento> ObtenerDocumentosCompra()
        {
            var documentos = new List<Documento>();
            var ds = EjecutarConsulta("dbo.ObtenerDocumentosCompras", new List<SqlParameter>());
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    documentos.Add(new Documento()
                    {
                        IdDocumento = int.Parse(row["IdDocumento"].ToString()),
                        Consecutivo = row["Consecutivo"].ToString(),
                        TipoDocumento = row["TipoDocumento"].ToString(),
                        Total = double.Parse(row["Total"].ToString()),
                        SocioNegocio = new SocNegocio()
                        {
                            Nombre=row["Nombre"].ToString()
                        }
                    });
                }
            }
            return documentos;
        }

        public Documento ObtenerDocumento(int pIdDocumento)
        {
            Documento documento=null;
            var ds = EjecutarConsulta("dbo.ObtenerDocumentoCompleto", new List<SqlParameter>()
            {
                new SqlParameter("pIdDocumento", pIdDocumento)
            });
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (documento == null)
                    {
                        documento = new Documento()
                        {
                            IdDocumento = pIdDocumento,
                            Consecutivo = row["Consecutivo"].ToString(),
                            Fecha1 = DateTime.Parse(row["Fecha"].ToString()),
                            Fecha2 = DateTime.Parse(row["Fecha2"].ToString()),
                            TipoDocumento = row["TipoDocumento"].ToString(),
                            Subtotal = double.Parse(row["Subtotal"].ToString()),
                            Total = double.Parse(row["Total"].ToString()),
                            LineasVenta = new List<LineaVenta>(){
                                new LineaVenta(){
                                    Bodega=new BodegaCV(){
                                        IdBodega=row["IdBodega"]==null?-1:int.Parse(row["IdBodega"].ToString()),
                                        Nombre=row["Bodega"]==null?string.Empty:row["Bodega"].ToString()
                                    },
                                    Cantidad=int.Parse(row["Cantidad"].ToString()),
                                    Impuestos=double.Parse(row["Impuesto"].ToString()),
                                    Producto=new ProductoCV(){
                                        IdProducto=int.Parse(row["IdArticulo"].ToString()),
                                        Nombre=row["NombreArticulo"].ToString(),
                                        Descripcion = row["NombreArticulo"].ToString(),
                                        Precio = row["Precio"]==null?-1:double.Parse(row["Precio"].ToString())
                                    }
                                }
                            },
                            SocioNegocio = new SocNegocio()
                            {
                                IdSocio=int.Parse(row["IdSocioNegocio"].ToString()),
                                Nombre=row["SocioNegocio"].ToString()
                            }
                        };
                    } else {
                        documento.LineasVenta.Add(new LineaVenta(){
                            Bodega=new BodegaCV(){
                                IdBodega=row["IdBodega"]==null?-1:int.Parse(row["IdBodega"].ToString()),
                                Nombre=row["Bodega"]==null?string.Empty:row["Bodega"].ToString()
                            },
                            Cantidad=int.Parse(row["Cantidad"].ToString()),
                            Impuestos=double.Parse(row["Impuesto"].ToString()),
                            Producto=new ProductoCV(){
                                IdProducto=int.Parse(row["IdArticulo"].ToString()),
                                Nombre=row["NombreArticulo"].ToString(),
                                Descripcion = row["NombreArticulo"].ToString(),
                                Precio = row["Precio"]==null?-1:double.Parse(row["Precio"].ToString())
                            }
                        });
                    }
                }
                foreach (var lv in documento.LineasVenta)
                {
                    lv.Total = (lv.Cantidad * lv.Producto.Precio) + ((lv.Impuestos / 100) * lv.Producto.Precio * lv.Cantidad);
                }
            }
            return documento;
        }

        public bool ModificarCostoArticulo(List<LineaVenta> pLineasVenta)
        {
            DataTable dataTable = new DataTable("LineaVenta");
            dataTable.Columns.Add("IdArticulo", typeof(int));
            dataTable.Columns.Add("IdDetalle", typeof(int));
            dataTable.Columns.Add("IdBodega", typeof(int));
            dataTable.Columns.Add("Cantidad", typeof(int));
            dataTable.Columns.Add("Impuesto", typeof(double));
            dataTable.Columns.Add("Precio", typeof(double));
            foreach (var lv in pLineasVenta)
            {
                dataTable.Rows.Add(lv.Producto.IdProducto, 0,
                    lv.Bodega.IdBodega, lv.Cantidad, lv.Impuestos, lv.Producto.Precio);
            }
            var param = new SqlParameter();
            param.ParameterName = "pLineasVenta";
            param.SqlDbType = SqlDbType.Structured;
            param.Value = dataTable;
            return EjecutarNoConsulta("dbo.ModificarCostoPromedio", new List<SqlParameter>(){
               param
            });
        }

        #endregion
    }
}
