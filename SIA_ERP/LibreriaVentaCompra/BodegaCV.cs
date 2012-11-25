using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIA.VentaCompra.Libreria
{
    public class BodegaCV
    {
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public int Comprometido { get; set; }
        public int Solicitado { get; set; }
        public double Costo { get; set; }
    }
}
