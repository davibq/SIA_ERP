using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SIA.Libreria
{
    [DataContract]
    public class Asiento
    {
        [DataMember]
        public int NumeroAsiento { get; set; }

        [DataMember]
        public DateTime FechaContable { get; set; }

        [DataMember]
        public string Cuenta_CodigoSN { get; set; }

        [DataMember]
        public string Cuenta_NombreSN { get; set; }

        [DataMember]
        public double DebeMonedaLocal { get; set; }

        [DataMember]
        public double HaberMonedaLocal { get; set; }

        [DataMember]
        public double DebeMonedaSistema { get; set; }

        [DataMember]
        public double HaberMonedaSistema { get; set; }
    }
}
