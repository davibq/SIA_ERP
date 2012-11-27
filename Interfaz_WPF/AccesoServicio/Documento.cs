using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccesoServicio.FinanzasService
{
    public partial class Documento
    {
        public override string ToString()
        {
            return Fecha1.ToShortDateString() + " - " + Consecutivo + "  Total: " + Total.ToString();
        }
    }
}
