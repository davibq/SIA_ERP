using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SIA.Libreria
{
    [DataContract]
    public class Empresa
    {
        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string CedulaJuridica { get; set; }

        [DataMember]
        public bool Enabled{ get; set; }

        [DataMember]
        public string Fax { get; set; }

        [DataMember]
        public string NombreMonedaLocal { get; set; }

        [DataMember]
        public string NombreMonedaSistema { get; set; }

        [DataMember]
        public string AcronimoMonedaLocal { get; set; }

        [DataMember]
        public string AcronimoMonedaSistema { get; set; }

        [DataMember]
        public string Telefono { get; set; }
    }
}
