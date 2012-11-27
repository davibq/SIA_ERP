using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using SIA.Libreria;

namespace SIA.VentaCompra.Libreria
{
    [DataContract]
    public class Transferencia
    {
        [DataMember]
        public int idTransferencia { get; set; }
        [DataMember]
        public string TipoTransferencia { get; set; }
        [DataMember]
        public SocNegocio Socio { get; set; }
        [DataMember]
        public string NumTranseferencia { get; set; }
        [DataMember]
        public double Monto { get; set; }
        [DataMember]
        public Banco banco { get; set; }
        [DataMember]
        public DateTime Fecha { get; set; }

    }
}