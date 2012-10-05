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
        public string Telefono { get; set; }
    }
}
