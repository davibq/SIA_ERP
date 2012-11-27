using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SIA.VentaCompra.Libreria
{
    [DataContract]
    public class Documento
    {
        [DataMember]
        public int IdDocumento { get; set; }
        [DataMember]
        public string Consecutivo { get; set; }
        [DataMember]
        public DateTime Fecha1  { get; set; }
        [DataMember]
        public DateTime Fecha2 { get; set; }
        [DataMember]
        public string TipoDocumento { get; set; }
        [DataMember]
        public SocNegocio SocioNegocio { get; set; }
        [DataMember]
        public List<LineaVenta> LineasVenta { get; set; }
        [DataMember]
        public double Subtotal { get; set; }
        [DataMember]
        public double Total { get; set; }
        [DataMember]
        public bool EsServicio { get; set; }
        [DataMember]
        public string DescripcionServicio {get; set;}
        [DataMember]
        public string CodigoCuentaServicio { get; set; }
        [DataMember]
        public bool CreadoDesdeAnterior { get; set; }
    }
}
