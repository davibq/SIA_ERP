using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using SIA.TipoCambio;

namespace SIA.Libreria
{
    [DataContract]
    public class Moneda
    {
        [DataMember]
        public int IdMoneda { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Acronimo { get; set; }

        [DataMember]
        public string Tipo { get; set; }

        [DataMember]
        public MonedasValidas TipoMoneda { get; set; }


    }
}
