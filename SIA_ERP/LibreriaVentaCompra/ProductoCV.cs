using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIA.VentaCompra.Libreria
{
    public class ProductoCV
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public List<BodegaCV> Bodegas {get; set;}
        public double Precio { get; set; }
    }
}

