using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessVentasCompras;
using SIA.VentaCompra.Libreria;

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

        public bool GuardarDocumento(Documento pDocumento)
        {
            return _DataAccess.GuardarDocumento(pDocumento);
        }

        public List<Documento> ObtenerDocumentosCompras()
        {
            return _DataAccess.ObtenerDocumentosCompra();
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

        #endregion

        #endregion

        #region Atributos

        private DACompraVenta _DataAccess;
        private static LogicaCompraVenta _Instancia;
        private static object _Lock=new object();

        #endregion
    }
}
