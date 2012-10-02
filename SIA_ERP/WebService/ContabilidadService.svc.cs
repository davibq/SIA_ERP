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

        public bool AutenticarUsuario(Usuario pUsuario, string pNombreEmpresa)
        {
            //Llamar SP para verificar userName y password
            return LogicaNegocio.Instancia.AutenticarUsuario(pUsuario, pNombreEmpresa);
            //return pUsuario.NombreUsuario.CompareTo("davibq") == 0;
        }

        public bool InsertarNuevoUsuario(Usuario pUsuario)
        {
            //Llamar SP para insertar nuevo usuario
            Console.WriteLine("Bienvenido" + pUsuario.NombreUsuario);  //prueba
            return true;
        }

        public bool CrearCuenta(Cuenta pCuenta)
        {
            //Llamar SP para crear una nueva cuenta
            return true;
        }
    }
}
