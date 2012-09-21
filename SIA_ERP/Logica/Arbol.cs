using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
    public class NodoRaiz
    {
        public string nombreEmpresa;
        public NodoCuenta activos;
        public NodoCuenta pasivos;
        public NodoCuenta patrimonio;
        public NodoCuenta ingresos;
        public NodoCuenta costos;
        public NodoCuenta gastos;
        public NodoCuenta otrosIngresos;
        public NodoCuenta otrosGastos;

        //Constructor
        public NodoRaiz(string pNombreEmpresa)
        {
            this.nombreEmpresa = pNombreEmpresa;
            this.activos = new NodoCuenta("1", "Activos");
            this.pasivos = new NodoCuenta("2", "Pasivos");
            this.patrimonio = new NodoCuenta("3", "Patrimonio");
            this.ingresos = new NodoCuenta("4", "Ingresos");
            this.costos = new NodoCuenta("5", "Costos");
            this.gastos = new NodoCuenta("6", "Gastos");
            this.otrosIngresos = new NodoCuenta("7", "Otros Ingresos");
            this.otrosGastos = new NodoCuenta("8", "Otros Gastos");
        }
    }

    public class NodoCuenta
    {
        public string codigo;
        public string nombre;
        public bool cuentaActiva;
        public string moneda;
        public double saldoEnLocal;
        public double saldoEnSistema;
        public NodoCuenta sigCuenta;
        public NodoCuenta listaCuentasHijas;

        //Constructor
        public NodoCuenta(string pCodigo, string pNombre)
        {
            this.codigo = pCodigo;
            this.nombre = pNombre;
            this.cuentaActiva = false;
            this.moneda = "USD";
            this.saldoEnLocal = 0;
            this.saldoEnSistema = 0;
            this.listaCuentasHijas = null;
            this.sigCuenta = null;
        }
    }

    class Arbol
    {
        public NodoRaiz arbolCuentas;

        //Costructor
        public Arbol()
        {

        }

        public void crearArbol(string pNombreEmpresa)    //crear el árbol para una compañía
        {
            this.arbolCuentas = new NodoRaiz(pNombreEmpresa);
        }

        public void cargarArbol()   //desde BD
        {

        }

        public NodoCuenta buscarCuentaPadre(string pCodPadre)
        {
            NodoCuenta recorridoArbol = null;

            //dividrir el codPadre en tokens
            char[] delimiterChars = { '-' };
            string[] tokens = pCodPadre.Split(delimiterChars);

            switch (tokens[0])
            {
                case "1":
                    recorridoArbol = arbolCuentas.activos;
                    break;
                case "2":
                    recorridoArbol = arbolCuentas.pasivos;
                    break;
                case "3":
                    recorridoArbol = arbolCuentas.patrimonio;
                    break;
                case "4":
                    recorridoArbol = arbolCuentas.ingresos;
                    break;
                case "5":
                    recorridoArbol = arbolCuentas.costos;
                    break;
                case "6":
                    recorridoArbol = arbolCuentas.gastos;
                    break;
                case "7":
                    recorridoArbol = arbolCuentas.otrosIngresos;
                    break;
                case "8":
                    recorridoArbol = arbolCuentas.otrosGastos;
                    break;
                default:
                    Console.WriteLine("Error, tokens mal divididos");
                    break;
            }

            if (tokens.Length > 1) 
            {
                for (int i = 1; (i-1)< tokens.Length; i++)
                {
                    string[] tokensCodigoCuenta = recorridoArbol.codigo.Split(delimiterChars);
                    if (tokens[i] == tokensCodigoCuenta[i])
                        recorridoArbol = recorridoArbol.listaCuentasHijas;
                    else
                        recorridoArbol = recorridoArbol.sigCuenta;
                }
            }

            return recorridoArbol;
        }

        public void insertarNuevaCuenta(string pCodPadre, string pCodigo, string pNombre)
        {
            NodoCuenta cuentaPadre = buscarCuentaPadre(pCodPadre);
            NodoCuenta nuevaCuenta = new NodoCuenta(pCodigo, pNombre);

            if (cuentaPadre.listaCuentasHijas == null)
                cuentaPadre.listaCuentasHijas = nuevaCuenta;
            else 
            {
                NodoCuenta recorridoArbol = cuentaPadre.listaCuentasHijas;

                while (recorridoArbol.sigCuenta == null)
                    recorridoArbol=recorridoArbol.sigCuenta;

                recorridoArbol.sigCuenta = nuevaCuenta;
            }
        }
    }
}
