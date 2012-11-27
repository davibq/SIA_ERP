using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SIA.VentaCompra.Libreria
{
    [DataContract]
    public class LineaVenta
    {
        [DataMember]
        public ProductoCV Producto { get; set; }
        [DataMember]
        public int Cantidad { get; set; }
        [DataMember]
        public double Impuestos { get; set; }
        [DataMember]
        public BodegaCV Bodega { get; set; }
        [DataMember]
        public double Total { get; set; }
    }
}
