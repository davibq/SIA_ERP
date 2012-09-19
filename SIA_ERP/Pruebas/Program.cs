using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIA.TipoCambio;

namespace Pruebas
{
    class Program
    {
        static void Main(string[] args)
        {
            if (TiposCambio.Instancia.SonValoresValidos)
            {
                Console.WriteLine("$25 es igual a: C" + TiposCambio.Instancia.ObtenerColones(25));
                Console.WriteLine("C12500 es igual a: $" + TiposCambio.Instancia.ObtenerDolares(12500));
                Console.ReadLine();
            }
        }
    }
}
