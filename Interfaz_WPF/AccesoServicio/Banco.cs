using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccesoServicio.FinanzasService
{
    public partial class Banco
    {
        public override string ToString()
        {
            return NoCuenta + " - " + Nombre + " " + AcronimoMoneda;
        }
    }
}
