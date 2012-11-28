using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessVentasCompras;
using SIA.VentaCompra.Libreria;
using SIA.Libreria;
using System.Net.Mail;
using SIA.ExceptionLog;

namespace Logica
{
    public class LogicaCompraVenta
    {
        #region Constructor

        private LogicaCompraVenta()
        {
            _DataAccess=new DACompraVenta("ERP_Ventas");
        }

        #endregion

        #region Propiedades

        public static LogicaCompraVenta Instancia
        {
            get
            {
                if (_Instancia == null)
                {
                    lock (_Lock)
                    {
                        if (_Instancia == null)
                        {
                            _Instancia = new LogicaCompraVenta();
                        }
                    }

                }
                return _Instancia;
            }
        }

        #endregion

        #region Metodos

        public List<SocNegocio> ObtenerSociosNegocio(string pTipoSocio)
        {
            return _DataAccess.ObtenerSociosNegocio(pTipoSocio);
        }

        public List<ProductoCV> ObtenerProductos()
        {
            return _DataAccess.DemeProductos();
        }

        private bool EnviarCorreo()
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.To.Add("gatjens20@gmail.com");
            msg.From = new MailAddress("tec.sia.2012@gmail.com", "TEC SIA", System.Text.Encoding.UTF8);
            msg.Subject = "Nueva Orden de Compra";
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = "Se ha revisado una nueva orden de compra";
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = false;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("tec.sia.2012@gmail.com", "siatec2012");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            try
            {
                client.Send(msg);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                ExceptionLogger.LogExcepcion(ex, "Error enviando correo");
                return false;
            }
            return true;
        }
        public bool CrearSocioDeNegocio(string Nombre, string Codigo, string TipoSocio, int IdMoneda, string CuentaAsociada)
        {
            return _DataAccess.CrearSocioDeNegocio(Nombre, Codigo, TipoSocio, IdMoneda, CuentaAsociada);
        }

        public string ObtenerCuentaDeMayorXCodigo(string CodigoSN)
        {
            return _DataAccess.ObtenerCuentaDeMayorXCodigo(CodigoSN);
        }

        public int ObtenerIDMonedaCuentaDeMayorXCodigo(string CodigoSN)
        {
            return _DataAccess.ObtenerIDMonedaCuentaDeMayorXCodigo(CodigoSN);
        }

        public bool GuardarDocumento(Documento pDocumento)
        {
            var guardadoDocumento=_DataAccess.GuardarDocumento(pDocumento);
            var extras=false;
            var monedaSistema = LogicaNegocio.Instancia.ObtenerMonedasSistema("Sistema");
            if (pDocumento.TipoDocumento.CompareTo("Orden de Compra")==0){
                //Aumenta solicitados
                extras = _DataAccess.ModificarCantidadArticulos(pDocumento.LineasVenta, true, "Solicitado");
            }
            else if (pDocumento.TipoDocumento.CompareTo("Entrada de Mercancias") == 0)
            {
                //Registrar asientos -> Inventario contra Inventario dotacion
                foreach (var item in pDocumento.LineasVenta)
                {
                    string xml = "<Cuentas>";
                    xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                            item.Producto.Precio*item.Cantidad, monedaSistema.Acronimo, item.Producto.CuentaExistencias, 0);
                    xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                        item.Producto.Precio * item.Cantidad, monedaSistema.Acronimo, item.Producto.CuentaTransitoria, 1);
                    xml += "</Cuentas>";
                    /*LogicaNegocio.Instancia.AgregarAsiento(pDocumento.Fecha1.ToShortDateString(), 
                        item.Producto.Precio, item.Producto.Precio, xml, "EM");*/
                }
                
                //Calcular nuevo costo promedio
                extras = _DataAccess.ModificarCostoArticulo(pDocumento.LineasVenta);

                // Disminuye solicitados y aumenta stock
                extras = extras && _DataAccess.ModificarCantidadArticulos(pDocumento.LineasVenta, true, "Stock");
                if (pDocumento.CreadoDesdeAnterior)
                {
                    extras = extras && _DataAccess.ModificarCantidadArticulos(pDocumento.LineasVenta, false, "Solicitado");
                }
            }
            else if (pDocumento.TipoDocumento.CompareTo("Factura de Proveedores") == 0)
            {
                foreach (var item in pDocumento.LineasVenta)
                {
                    string xml = "<Cuentas>";
                    double total = (item.Producto.Precio*item.Cantidad)*(item.Impuestos/100)+(item.Producto.Precio*item.Cantidad);
                    //CXP
                    xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                            total, monedaSistema.Acronimo, pDocumento.SocioNegocio.CuentaAsociada, 0);
                    //Inventario o Inventario en dotacion
                    xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                        total, monedaSistema.Acronimo, 
                        pDocumento.CreadoDesdeAnterior?item.Producto.CuentaTransitoria:item.Producto.CuentaExistencias, 1);
                    xml += "</Cuentas>";
                    /*LogicaNegocio.Instancia.AgregarAsiento(pDocumento.Fecha1.ToShortDateString(), 
                        item.Producto.Precio, item.Producto.Precio, xml, "FP");*/
                }
                extras = true;
            }
            else if (pDocumento.TipoDocumento.CompareTo("Orden de Venta") == 0)
            {
                extras = _DataAccess.ModificarCantidadArticulos(pDocumento.LineasVenta, true, "Comprometido");
            }
            else if (pDocumento.TipoDocumento.CompareTo("Entrega de Mercancias") == 0)
            {
                extras = extras && _DataAccess.ModificarCantidadArticulos(pDocumento.LineasVenta, false, "Stock");
                if (pDocumento.CreadoDesdeAnterior)
                {
                    extras = extras && _DataAccess.ModificarCantidadArticulos(pDocumento.LineasVenta, false, "Comprometido");
                }
                foreach (var item in pDocumento.LineasVenta)
                {
                    string xml = "<Cuentas>";
                    double total = (item.Producto.Precio * item.Cantidad) * (item.Impuestos / 100) + (item.Producto.Precio * item.Cantidad);
                    //Costo Ventas
                    xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                            item.Bodega.Costo, monedaSistema.Acronimo, item.Producto.CuentaCostos, 1);
                    //Inventario
                    xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                        item.Bodega.Costo, monedaSistema.Acronimo,
                        item.Producto.CuentaExistencias, 0);
                    xml += "</Cuentas>";
                    /*LogicaNegocio.Instancia.AgregarAsiento(pDocumento.Fecha1.ToShortDateString(), 
                        item.Producto.Precio, item.Producto.Precio, xml, "EE");*/
                }
            }
            else if (pDocumento.TipoDocumento.CompareTo("Factura de clientes") == 0)
            {
                //TODO: remover CXP alambrada
                var cuentaIVXPagar = "2-1-01-01";

                //Inventario contra costo ventas
                if (!pDocumento.CreadoDesdeAnterior)
                {
                    _DataAccess.ModificarCantidadArticulos(pDocumento.LineasVenta, false, "Stock");
                    foreach (var item in pDocumento.LineasVenta)
                    {
                        string xml = "<Cuentas>";
                        double total = (item.Producto.Precio * item.Cantidad) * (item.Impuestos / 100) + (item.Producto.Precio * item.Cantidad);
                        //Costo Ventas
                        xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                                item.Bodega.Costo, monedaSistema.Acronimo, item.Producto.CuentaCostos, 1);
                        //Inventario
                        xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                            item.Bodega.Costo, monedaSistema.Acronimo,
                            item.Producto.CuentaExistencias, 0);

                        //CxC
                        xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                            total, monedaSistema.Acronimo,
                            pDocumento.SocioNegocio.CuentaAsociada, 1);
                        //IV x pagar
                        xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                            (item.Producto.Precio * item.Cantidad) * (item.Impuestos / 100), monedaSistema.Acronimo,
                            cuentaIVXPagar, 0);
                        //Ventas
                        xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                            (item.Producto.Precio * item.Cantidad), monedaSistema.Acronimo,
                            item.Producto.CuentaVentas, 0);
                        xml += "</Cuentas>";
                        /*LogicaNegocio.Instancia.AgregarAsiento(pDocumento.Fecha1.ToShortDateString(), 
                            item.Producto.Precio, item.Producto.Precio, xml, "FC");*/
                    }
                }
                else
                {
                    foreach (var item in pDocumento.LineasVenta)
                    {
                        string xml = "<Cuentas>";
                        double total = (item.Producto.Precio * item.Cantidad) * (item.Impuestos / 100) + (item.Producto.Precio * item.Cantidad);
                        //CxC
                        xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                            total, monedaSistema.Acronimo,
                            pDocumento.SocioNegocio.CuentaAsociada, 1);
                        //IV x pagar
                        xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                            (item.Producto.Precio * item.Cantidad) * (item.Impuestos / 100), monedaSistema.Acronimo,
                            cuentaIVXPagar, 0);
                        //Ventas
                        xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                            (item.Producto.Precio * item.Cantidad), monedaSistema.Acronimo,
                            item.Producto.CuentaVentas, 0);
                        xml += "</Cuentas>";
                        /*LogicaNegocio.Instancia.AgregarAsiento(pDocumento.Fecha1.ToShortDateString(), 
                            item.Producto.Precio, item.Producto.Precio, xml, "FC");*/
                    }
                }
                extras = true;
            }
            return extras && guardadoDocumento;
        }

        public List<Documento> ObtenerDocumentosCompras()
        {
            return _DataAccess.ObtenerDocumentosCompra();
        }
        public List<Documento> ObtenerDocumentosVentas()
        {
            return _DataAccess.ObtenerDocumentosVenta();
        }

        public Documento ObtenerDocumento(int pIdDocumento)
        {
            return _DataAccess.ObtenerDocumento(pIdDocumento);
        }

        #region Facturas

        public List<Documento> ObtenerFacturasXEstadoXSocioNegocio(string pCodSN, string pEstadoFactura)
        {
            return _DataAccess.ObtenerFacturasXEstadoXSocioNegocio(pCodSN, pEstadoFactura);
        }

        public bool setearFacturas(int idDoc, string pEstado)
        {
            return _DataAccess.setearFacturas(idDoc, pEstado);
        }

        public bool insertarTransferencia(Transferencia pTransferencia)
        {
            return _DataAccess.insertarTransferencia(pTransferencia);
        }

        #endregion

        #endregion

        #region Atributos

        private DACompraVenta _DataAccess;
        private static LogicaCompraVenta _Instancia;
        private static object _Lock=new object();

        #endregion
    }
}
