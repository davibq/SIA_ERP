using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica
{

    //Nodo que representa las cuentas del árbol, con todos sus atributos
    public class NodoCuenta
    {
        public string codigo;
        public string nombre;
        public bool cuentaActiva;
        public string moneda;
        public double saldoEnLocal;
        public double saldoEnSistema;
        public NodoCuenta sigCuenta;
        public NodoCuenta antCuenta;
        public NodoCuenta listaCuentasHijas;
        public NodoCuenta cuentaPadre;

        /*
         * Constructor, crea la cuenta con el código, el nombre y estado de la cuenta como parámetros, 
         * lo demás se inicializa por default
         */
        public NodoCuenta(string pCodigo, string pNombre, bool pEstadoCuenta)
        {
            this.codigo = pCodigo;
            this.nombre = pNombre;
            this.cuentaActiva = pEstadoCuenta;
            this.moneda = "USD";
            this.saldoEnLocal = 0;
            this.saldoEnSistema = 0;
            this.listaCuentasHijas = null;
            this.sigCuenta = null;
            this.antCuenta = null;
            this.cuentaPadre = null;
        }
    }
}
