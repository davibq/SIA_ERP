using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccesoServicio.FinanzasService
{
    public partial class Articulo
    {
        public override string ToString()
        {
            return Codigo + " - " + Nombre;
        }
    }
}
