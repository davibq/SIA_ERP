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

        #endregion

        #region Atributos

        private DACompraVenta _DataAccess;
        private static LogicaCompraVenta _Instancia;
        private static object _Lock=new object();

        #endregion
    }
}
