using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SIA.Libreria
{
    [DataContract]
    public class ConsultaSaldo
    {
        [DataMember]
        public double Saldo { get; set; }

        [DataMember]
        public double LimiteCredito { get; set; }
    }
}
