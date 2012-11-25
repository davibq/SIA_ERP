using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIA.VentaCompra.Libreria
{
    public class LineaVenta
    {
        public ProductoCV Producto { get; set; }
        public int Cantidad { get; set; }
        public double Impuestos { get; set; }
        public BodegaCV Bodega { get; set; }
        public double Total { get; set; }
    }
}
