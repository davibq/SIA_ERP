using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessPrincipal;
using SIA.Libreria;
using DataAccessFinanzas;
using SIA.Contabilidad.Libreria;

namespace Logica
{
    public class LogicaNegocio
    {
        #region Propiedades

        public static LogicaNegocio Instancia
        {
            get
            {
                if (_Instancia == null)
                {
                    lock (_Lock)
                    {
                        if (_Instancia == null)
                        {
                            _Instancia = new LogicaNegocio();
                        }
                    }

                }
                return _Instancia;
            }
        }

        #endregion

        #region Metodos

        public string ObtenerEmpresas()
        {
            var daPrincipal = new DAPrincipal();
            return daPrincipal.ObtenerEmpresas();
        }

        public string ObtenerMonedas()//string pBaseDatos)
        {
            //var daFinanzas = new DAFinanzas(pBaseDatos);
            return _DataAccess.ObtenerMonedas();
        }

        public bool AutenticarUsuario(Usuario pUsuario, string pNombreEmpresa)
        {
            var daPrincipal = new DAPrincipal();
            var nombreBD = daPrincipal.AutenticarUsuario(pUsuario, pNombreEmpresa);
            if (nombreBD.Length > 2)
            {
                _DataAccess = new DAFinanzas(nombreBD);
                _Arbol.crearArbol(pNombreEmpresa);
                return true;
            }
            return false;
        }

        public bool InsertarNuevoUsuario(Usuario pUsuario)
        {
            var daPrincipal = new DAPrincipal();
            return daPrincipal.InsertarNuevoUsuario(pUsuario);
        }

        public bool InsertarNuevaEmpresa(Empresa pEmpresa, byte[] pLogo)
        {
            var daPrincipal = new DAPrincipal();
            var daFinanzas = new DAFinanzas(pEmpresa.Nombre);
            bool retorno = daPrincipal.InsertarNuevaEmpresa(pEmpresa, pLogo);
            bool retorno1 = daFinanzas.InsertarMonedas(pEmpresa);
            if (retorno && retorno1)
                return true;
            return false;
        }

        public bool CrearCuenta(Cuenta pCuenta)
        {
            /*
            pCuenta.CodigoCuentaPadre = pCuenta.Codigo.Remove(pCuenta.Codigo.LastIndexOf("-"));
            pCuenta.Enabled = true;
            char[] delimiterChars = { '-' };
            string[] tokens = pCuenta.Codigo.Split(delimiterChars);
            pCuenta.Nivel = tokens.Length;

            string codId = pCuenta.Codigo.Remove(pCuenta.Codigo.IndexOf("-"));
            switch (codId)
            {
                case "1":
                    pCuenta.Identificador = "Activo";
                    break;
                case "2":
                    pCuenta.Identificador = "Pasivo";
                    break;
                case "3":
                    pCuenta.Identificador = "Patrimonio";
                    break;
                case "4":
                    pCuenta.Identificador = "Ingresos";
                    break;
                case "5":
                    pCuenta.Identificador = "Costos";
                    break;
                case "6":
                    pCuenta.Identificador = "Gastos";
                    break;
                case "7":
                    pCuenta.Identificador = "Otros Ingresos";
                    break;
                default:
                    pCuenta.Identificador = "Otros Gastos";
                    break;
            }
            */
            return _DataAccess.CrearCuenta(pCuenta);
        }

        public bool GuardarPeriodoContable(Mes[] pArregloMeses)
        {
            return _DataAccess.GuardarPeriodoContable(pArregloMeses);
        }

        public IEnumerable<Cuenta> DemeCuentasHijas()
        {
            _DataAccess = new DAFinanzas("ERP_Finanzas");
            var todasCuentas = _DataAccess.ObtenerCuentas();
            var cuentasHijas = todasCuentas;
            foreach (var cuenta in todasCuentas)
            {
                var cuentasAQuitar = new List<Cuenta>();
                foreach (var cta in todasCuentas)
                {
                    if (cta.Codigo == cuenta.CodigoCuentaPadre)
                    {
                        cuentasAQuitar.Add(cta);
                    }
                }
                cuentasHijas = cuentasHijas.Except(cuentasAQuitar);
            }
            return cuentasHijas;
        }

        public IEnumerable<Moneda> DemeMonedasCuenta(string pCuenta)
        {
            _DataAccess = new DAFinanzas("ERP_Finanzas");
            return _DataAccess.DemeMonedas(pCuenta);
        }

        public bool AgregarAsiento(string Fecha, double MontoDebe, double MontoHaber, string pXML)
        {
            return _DataAccess.AgregarAsiento(Fecha, MontoDebe, MontoHaber, pXML);
        }

#endregion

        #region Atributos

        private static LogicaNegocio _Instancia;
        private static object _Lock = new object();

        private DAFinanzas _DataAccess;
        Arbol _Arbol = new Arbol();

        #endregion
    }
}
