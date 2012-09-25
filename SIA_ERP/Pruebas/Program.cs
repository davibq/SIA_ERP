using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIA.TipoCambio;
using Logica;

namespace Pruebas
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Arbol arbol = new Arbol();
            arbol.crearArbol("SIA ERP");
            arbol.insertarNuevaCuenta("1", "1-10", "Bancos");
            Console.WriteLine("Nuevo nodo enlazado: "+arbol.arbolCuentas.activos.listaCuentasHijas.nombre);
             */


            
            if (TiposCambio.Instancia.SonValoresValidos)
            {
                Console.WriteLine("$25 es igual a: C" + TiposCambio.Instancia.ObtenerColones(25));
                Console.WriteLine("C12500 es igual a: $" + TiposCambio.Instancia.ObtenerDolares(12500));
                Console.ReadLine();
            }
        }
    }
}
