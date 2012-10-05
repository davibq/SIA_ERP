using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SIA.Contabilidad.Libreria
{
    [DataContract]
    public class Cuenta
    {
        [DataMember]
        public int CuentaId { get; set; }

        [DataMember]
        public Cuenta CuentaPadre { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Codigo { get; set; }

        [DataMember]
        public int Nivel { get; set; }

        [DataMember]
        public double MontoDebe { get; set; }

        [DataMember]
        public double MontoHaber { get; set; }
    }
}
