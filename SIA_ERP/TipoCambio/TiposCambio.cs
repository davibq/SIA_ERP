using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIA.ExceptionLog;
using System.Collections;

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
                _TiposCambio = new Hashtable();
                 wsCliente = new BCCRWebService.wsIndicadoresEconomicosSoapClient();
                var fechaHoy=DateTime.Now.ToShortDateString();
                wsCliente.Open();

                foreach (var moneda in Enum.GetValues(typeof(MonedasValidas)).Cast<MonedasValidas>())
                {
                    CargarTipoCambio(moneda, fechaHoy);
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

        private void CargarTipoCambio(MonedasValidas pMoneda, string pFechaHoy)
        {
            var dataSet = wsCliente.ObtenerIndicadoresEconomicos(((int)pMoneda).ToString(),
                    pFechaHoy, pFechaHoy, NOMBRE, "N");
            if (dataSet != null && dataSet.Tables[0] != null && dataSet.Tables[0].Rows != null)
            {
                double cambio;
                try
                {
                    if (double.TryParse(dataSet.Tables[0].Rows[0][2].ToString(), out cambio))
                    {
                        _TiposCambio.Add(pMoneda, cambio);
                    }
                    return;
                }
                catch (Exception ex)
                {
                    ExceptionLogger.LogExcepcion(ex, "Error obteniendo datos del WS del BCCR");
                }
            }
        }

        public double DemeCambio(MonedasValidas pOrigen, double pValor, MonedasValidas pDestino)
        {
            return ((double)(_TiposCambio[pOrigen]) * pValor) / (double)_TiposCambio[pDestino];
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

        private Hashtable _TiposCambio;
        private bool _ValoresValidos;

        private BCCRWebService.wsIndicadoresEconomicosSoapClient wsCliente;

        #endregion
    }
}
