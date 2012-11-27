using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIA.VentaCompra.Libreria
{
    public class SocNegocio
    {
        public int IdSocio { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string TipoSocio { get; set; }
        public int IdMoneda { get; set; }
        public string CuentaAsociada { get; set; }
    }
}
