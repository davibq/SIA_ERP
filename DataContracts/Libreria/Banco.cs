using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SIA.Libreria
{
    [DataContract]
    public class Banco
    {
        [DataMember]
        public int idBanco { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string AcronimoMoneda { get; set; }

        [DataMember]
        public string NoCuenta { get; set; }

        [DataMember]
        public string CuentaMayor { get; set; }
    }
}
