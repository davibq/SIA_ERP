using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SIA.VentaCompra.Libreria
{
    [DataContract]
    public class BodegaCV
    {
        [DataMember]
        public int IdBodega { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public int Stock { get; set; }
        [DataMember]
        public int Comprometido { get; set; }
        [DataMember]
        public int Solicitado { get; set; }
        [DataMember]
        public double Costo { get; set; }
    }
}
