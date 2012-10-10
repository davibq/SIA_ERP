using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using SIA.Libreria;

namespace SIA.Contabilidad.Libreria
{
    [DataContract]
    public class Asiento
    {
        [DataMember]
        public int NumeroAsiento { get; set; }

        [DataMember]
        public DateTime FechaContable { get; set; }

        [DataMember]
        public Cuenta Cuenta { get; set; }

        [DataMember]
        public double DebeMonedaSistema { get; set; }

        [DataMember]
        public double HaberMonedaSistema { get; set; }

        [DataMember]
        public double DebeMonedaOtra { get; set; }

        [DataMember]
        public double HaberMonedaOtra { get; set; }

        [DataMember]
        public string MonedaAcronimo { get; set; }
    }
}
