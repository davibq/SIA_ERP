using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SIA.Libreria
{
    [DataContract]
    public class Cuenta
    {
        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Codigo { get; set; }

        [DataMember]
        public string NombreIdiomaExtranjero { get; set; }

        [DataMember]
        public string Moneda { get; set; }
    }
}
