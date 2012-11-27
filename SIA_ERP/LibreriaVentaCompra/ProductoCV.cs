using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SIA.VentaCompra.Libreria
{
    [DataContract]
    public class ProductoCV
    {
        [DataMember]
        public int IdProducto { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Codigo { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public List<BodegaCV> Bodegas {get; set;}
        [DataMember]
        public double Precio { get; set; }
        [DataMember]
        public string CuentaCostos { get; set; }
        [DataMember]
        public string CuentaExistencias { get; set; }
        [DataMember]
        public string CuentaVentas { get; set; }
        [DataMember]
        public string CuentaTransitoria { get; set; }
    }
}

