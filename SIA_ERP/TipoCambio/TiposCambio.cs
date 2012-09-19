using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIA.ExceptionLog;

namespace SIA.TipoCambio
{
    public class TiposCambio
    {
        #region Constructor

        private TiposCambio()
        {
            try
            {
                UsarCompra = true;
                _ValoresValidos = true;
                var wsCliente = new BCCRWebService.wsIndicadoresEconomicosSoapClient();
                var fechaHoy=DateTime.Now.ToShortDateString();
                wsCliente.Open();
                var dataSet = wsCliente.ObtenerIndicadoresEconomicos(INDICADOR_ECONOMICO_COMPRA,
                    fechaHoy, fechaHoy, NOMBRE, "N");
                if (dataSet != null && dataSet.Tables[0] != null && dataSet.Tables[0].Rows!=null)
                {
                    double compra;
                    if (double.TryParse(dataSet.Tables[0].Rows[0][2].ToString(), out compra))
                    {
                        _Compra = compra;
                    }
                    else
                    {
                        throw new Exception("Formato de número no válido");
                    }
                }
                dataSet = wsCliente.ObtenerIndicadoresEconomicos(INDICADOR_ECONOMICO_VENTA,
                    fechaHoy, fechaHoy, NOMBRE, "N");
                if (dataSet != null && dataSet.Tables[0] != null && dataSet.Tables[0].Rows != null)
                {
                    double venta;
                    if (double.TryParse(dataSet.Tables[0].Rows[0][2].ToString(), out venta))
                    {
                        _Venta = venta;
                    }
                    else
                    {
                        throw new Exception("Formato de número no válido");
                    }
                }
                wsCliente.Close();
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExcepcion(ex, "Error obteniendo datos del WS del BCCR");
                _ValoresValidos = false;
            }
        }

        #endregion

        #region Propiedades

        public static TiposCambio Instancia
        {
            get
            {
                if (_Instancia == null)
                {
                    lock (_Lock)
                    {
                        if (_Instancia == null)
                        {
                            _Instancia = new TiposCambio();
                        }
                    }
                }
                return _Instancia;
            }
        }

        public bool SonValoresValidos
        {
            get
            {
                return _ValoresValidos;
            }
        }

        public bool UsarCompra {get; set;}

        #endregion

        #region Metodos

        public double ObtenerDolares(double pColones){
            return pColones / (UsarCompra ? _Compra : _Venta);
        }

        public double ObtenerColones(double pDolares)
        {
            return pDolares * (UsarCompra ? _Compra : _Venta);
        }

        #endregion

        #region Constantes

        private const string INDICADOR_ECONOMICO_COMPRA = "317";
        private const string INDICADOR_ECONOMICO_VENTA = "318";

        private const string NOMBRE = "ProyectoSIA";

        #endregion

        #region Atributos

        private static TiposCambio _Instancia;
        private static object _Lock=new object();

        private double _Venta;
        private double _Compra;
        private bool _ValoresValidos;

        #endregion
    }
}
