using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SIA.Libreria;
using Logica;
using SIA.Contabilidad.Libreria;
using SIA.TipoCambio;

namespace SIA.Contabilidad.WebService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ContabilidadService : IContabilidadService
    {
        public string Saludar()
        {
            return "Hola";
        }

        public string ObtenerEmpresas()
        {
            return LogicaNegocio.Instancia.ObtenerEmpresas();
        }

        public IEnumerable<Moneda> ObtenerMonedas()
        {
            return LogicaNegocio.Instancia.ObtenerMonedas();
        }

        public bool AutenticarUsuario(Usuario pUsuario, string pNombreEmpresa)
        {
            return LogicaNegocio.Instancia.AutenticarUsuario(pUsuario, pNombreEmpresa);
        }

        public bool InsertarNuevoUsuario(Usuario pUsuario)
        {
            return LogicaNegocio.Instancia.InsertarNuevoUsuario(pUsuario);
        }

        public bool InsertarNuevaEmpresa(Empresa pEmpresa, byte[] pLogo)
        {
            return LogicaNegocio.Instancia.InsertarNuevaEmpresa(pEmpresa, pLogo);
        }

        public bool InsertarNuevaMoneda(Moneda pMoneda)
        {
            return LogicaNegocio.Instancia.InsertarNuevaMoneda(pMoneda);
        }

        public bool CrearCuenta(Cuenta pCuenta)
        {
            return LogicaNegocio.Instancia.CrearCuenta(pCuenta);
        }

        public bool GuardarPeriodoContable(string pFechaIn, string pFechaFin, int pAno, Mes[] pArregloMeses)
        {
            return LogicaNegocio.Instancia.GuardarPeriodoContable(pFechaIn,pFechaFin,pAno,pArregloMeses);
        }

        public IEnumerable<Cuenta> DemeCuentasHijas()
        {
            return LogicaNegocio.Instancia.DemeCuentasHijas();
        }

        public IEnumerable<Moneda> DemeMonedasCuenta(string pCuenta)
        {
            var monedas = LogicaNegocio.Instancia.DemeMonedasCuenta(pCuenta);
            return monedas;
        }


        public double DemeCambio(Moneda pOrigen, double pValor, Moneda pDestino)
        {
            return TiposCambio.Instancia.DemeCambio(pOrigen.TipoMoneda, pValor, pDestino.TipoMoneda);
        }

        public bool AgregarAsiento(string Fecha, double MontoDebe, double MontoHaber, string pXML)
        {
            return LogicaNegocio.Instancia.AgregarAsiento(Fecha, MontoDebe, MontoHaber, pXML);
        }

        public Moneda ObtenerMonedasSistema(string pAtributo)
        {
            return LogicaNegocio.Instancia.ObtenerMonedasSistema(pAtributo);
        }

        public void InsertarAsiento(Asiento pAs)
        {

        }

        public double ConvertirAMonedaSistema(MonedasValidas pMoneda, double pValor)
        {
            return LogicaNegocio.Instancia.ConvertirAMonedaSistema(pMoneda, pValor);
        }

        public IEnumerable<Cuenta> ObtenerCuentasHijasSegunPadre(string pNombrePadre)
        {
            return LogicaNegocio.Instancia.ObtenerCuentasHijasSegunPadre(pNombrePadre);
        }
    }
}
