using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessPrincipal;
using SIA.Libreria;
using DataAccessFinanzas;

namespace Logica
{
    public class LogicaNegocio
    {

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

        public string ObtenerEmpresas() 
        {
            var daPrincipal = new DAPrincipal();
            return daPrincipal.ObtenerEmpresas();
        }

        public string ObtenerMonedas()
        {
            return _DataAccess.ObtenerMonedas();
        }

        public bool AutenticarUsuario(Usuario pUsuario, string pNombreEmpresa)
        {
            var daPrincipal = new DAPrincipal();
            var nombreBD=daPrincipal.AutenticarUsuario(pUsuario, pNombreEmpresa);
            if (nombreBD.Length>2){
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
            bool retorno = daPrincipal.InsertarNuevaEmpresa(pEmpresa,pLogo);
            bool retorno1 = daFinanzas.InsertarMonedas(pEmpresa);
            if (retorno&&retorno1)
                return true;
            return false;
        }

        public bool CrearCuenta(Cuenta pCuenta)
        {
            pCuenta.CodigoCuentaPadre = pCuenta.Codigo.Remove(pCuenta.Codigo.LastIndexOf("-"));
            pCuenta.Enabled = 1;
            //dividir el cod en tokens
            char[] delimiterChars = { '-' };
            string[] tokens = pCuenta.Codigo.Split(delimiterChars);
            pCuenta.Nivel = tokens.Length;

            string codId  = pCuenta.Codigo.Remove(pCuenta.Codigo.IndexOf("-"));
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

            if (_DataAccess.CrearCuenta(pCuenta))
            {
                //_Arbol.crearArbol(pNombreEmpresa);
                _Arbol.insertarNuevaCuenta(pCuenta);
                return true;
            }
            return false;
        }

        private static LogicaNegocio _Instancia;
        private static object _Lock=new object();

        private DAFinanzas _DataAccess;
        Arbol _Arbol = new Arbol();
    }
}
