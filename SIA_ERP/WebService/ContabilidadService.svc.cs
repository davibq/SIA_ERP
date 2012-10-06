using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SIA.Libreria;
using Logica;

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

        public string ObtenerMonedas(string pBaseDatos)
        {
            return LogicaNegocio.Instancia.ObtenerMonedas(pBaseDatos);
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
            return LogicaNegocio.Instancia.InsertarNuevaEmpresa(pEmpresa,pLogo);
        }

        public bool CrearCuenta(Cuenta pCuenta, string pNombreEmpresa)
        {
            return LogicaNegocio.Instancia.CrearCuenta(pCuenta,pNombreEmpresa);
        }
    }
}
