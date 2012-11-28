using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SIA.DataAccess;
using System.Configuration;
using SIA.VentaCompra.Libreria;
using SIA.Libreria;

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
                            CuentaExistencias = row["codCuentasExistencia"].ToString(),
                            CuentaVentas = row["codCuentasVentas"].ToString(),
                            CuentaTransitoria = row["codCuentaTransitoria"].ToString()
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
        public List<Documento> ObtenerDocumentosVenta()
        {
            var documentos = new List<Documento>();
            var ds = EjecutarConsulta("dbo.ObtenerDocumentosVentas", new List<SqlParameter>());
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
                            Nombre = row["Nombre"].ToString()
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
                                        Nombre=row["Bodega"]==null?string.Empty:row["Bodega"].ToString(),
                                        Costo=row["Costo"]==null?-1:double.Parse(row["Costo"].ToString())
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
                                Nombre=row["Bodega"]==null?string.Empty:row["Bodega"].ToString(),
                                Costo=row["Costo"]==null?-1:double.Parse(row["Costo"].ToString())
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

        public List<OrdenCompras> obtenerOrdenCompra(string pCodProveedor, string pTipoDocumento)
        {
            var ordenesDeCompra = new List<OrdenCompras>();
            var ds = EjecutarConsulta("dbo.obtenerOrdenCompra", new List<SqlParameter>(){
                    new SqlParameter("codProveedor", pCodProveedor),
                    new SqlParameter("tipoDocumento", pTipoDocumento)
                });


            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ordenesDeCompra.Add(new OrdenCompras()
                    {
                        codigo = row["Consecutivo"].ToString(),
                        fechaContabilizacion = row["FechaConta"].ToString(),
                        fechaEntrega = row["FechaEntrega"].ToString(),
                        proveedor = new SocNegocio()
                        {
                            Codigo = row["CodigoSN"].ToString(),
                            Nombre = row["NombreSN"].ToString()
                        },
                        producto = new Productos()
                        {
                            cantidad = int.Parse(row["CantidadProducto"].ToString()),
                            precioUnit = double.Parse(row["PrecioProducto"].ToString()),
                            idArticulo = int.Parse(row["IdArticulo"].ToString()),
                            idBodega = int.Parse(row["IdBodega"].ToString()),
                            Nombre= row["NombreArticulo"].ToString(),
                            NombreBodega = row["NombreBodega"].ToString()
                        }
                    });
                }
            }
            return ordenesDeCompra;
        }

        public bool GuardarDocumentoServicios(Documento pDocumento)
        {
            return EjecutarNoConsulta("dbo.GuardarDocumento", new List<SqlParameter>()
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
        }

        #region Facturas

        public List<Documento> ObtenerFacturasXEstadoXSocioNegocio(string pCodSN, string pEstadoFactura)
        {
            var documentos = new List<Documento>();
            var ds = EjecutarConsulta("dbo.ObtenerFacturasXEstadoXSN", new List<SqlParameter>() 
                                        {
                                            new SqlParameter("pCodSN", pCodSN),
                                            new SqlParameter("pEstadoFactura", pEstadoFactura)
                                        });
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    SocNegocio socNeg = new SocNegocio();
                    socNeg.Codigo = row["_Cuenta"].ToString();
                    documentos.Add(new Documento()
                    {
                        IdDocumento = int.Parse(row["IdDocumento"].ToString()),
                        Consecutivo = row["Consecutivo"].ToString(),
                        Fecha1 = DateTime.Parse(row["Fecha"].ToString()),
                        Subtotal = double.Parse(row["Subtotal"].ToString()),
                        Total = double.Parse(row["Total"].ToString()),
                        SocioNegocio = socNeg
                    });
                }
            }
            return documentos;
        }

        public bool setearFacturas(int idDoc, string pEstado) {
            return EjecutarNoConsulta("dbo.SetearFactura", new List<SqlParameter>()
                                                          {
                                                              new SqlParameter("IdDocumento", idDoc),
                                                              new SqlParameter("Estado", pEstado)
                                                          });
        }

        public bool insertarTransferencia(Transferencia pTransferencia)
        {
            return EjecutarNoConsulta("dbo.insertarTransferencia", new List<SqlParameter>()
                                                          {
                                                              new SqlParameter("TipoTransferencia", pTransferencia.TipoTransferencia),
                                                              new SqlParameter("CodSN", pTransferencia.Socio.Codigo),
                                                              new SqlParameter("NumTransferencia", pTransferencia.NumTranseferencia),
                                                              new SqlParameter("Monto", pTransferencia.Monto),
                                                              new SqlParameter("idBanco", pTransferencia.banco.idBanco),
                                                              new SqlParameter("Fecha", pTransferencia.Fecha)
                                                          });
        }

        #endregion

        #endregion
    }
}
