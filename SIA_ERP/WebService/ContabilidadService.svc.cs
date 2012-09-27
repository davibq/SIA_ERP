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
            return pUsuario.NombreUsuario.CompareTo("davibq") == 0;
        }
    }
}
