using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIA.VentaCompra.Libreria
{
    public class Documento
    {
        public string Consecutivo { get; set; }
        public DateTime Fecha1  { get; set; }
        public DateTime Fecha2 { get; set; }
        public SocNegocio SocioNegocio { get; set; }
        public List<LineaVenta> LineasVenta { get; set; }
    }
}
