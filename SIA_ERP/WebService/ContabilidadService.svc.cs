using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SIA.Libreria;

namespace SIA.Contabilidad.WebService
{
    public class ContabilidadService : IContabilidadService
    {
        public string Saludar()
        {
            return "Hola";
        }

        public bool AutenticarUsuario(Usuario pUsuario)
        {
            //Llamar SP para verificar userName y password
            Console.WriteLine("Bienvenido" + pUsuario.NombreUsuario);  //prueba
            return true;
            //return pUsuario.NombreUsuario.CompareTo("davibq") == 0;
        }

        public bool InsertarNuevoUsuario(Usuario pUsuario)
        {
            //Llamar SP para insertar nuevo usuario
            Console.WriteLine("Bienvenido" + pUsuario.NombreUsuario);  //prueba
            return true;
        }
    }
}
