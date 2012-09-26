using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica
{
    //Clase para Nodo Padre, sólo posee el nombre de la empresa y las ocho cuentas principales
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

        //Constructor, se crea la raíz con las ocho cuentas principales en el árbol
        public NodoRaiz(string pNombreEmpresa)
        {
            this.nombreEmpresa = pNombreEmpresa;
            this.activos = new NodoCuenta("1", "Activos", false);
            this.pasivos = new NodoCuenta("2", "Pasivos", false);
            this.patrimonio = new NodoCuenta("3", "Patrimonio", false);
            this.ingresos = new NodoCuenta("4", "Ingresos", false);
            this.costos = new NodoCuenta("5", "Costos", false);
            this.gastos = new NodoCuenta("6", "Gastos", false);
            this.otrosIngresos = new NodoCuenta("7", "Otros Ingresos", false);
            this.otrosGastos = new NodoCuenta("8", "Otros Gastos", false);
        }
    }

}
