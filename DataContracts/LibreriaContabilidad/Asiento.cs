using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

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
        public List<Cuenta> CuentasAsociadas { get; set; }         

        [DataMember]
        public double DebeMonedaSistema { get; set; }

        [DataMember]
        public double HaberMonedaSistema { get; set; }
    }
}
