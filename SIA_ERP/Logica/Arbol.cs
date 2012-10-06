using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIA.Libreria;

namespace Logica
{
    public class Arbol
    {
        //instancia de la clase que manejara el arbol
        public NodoRaiz arbolCuentas;

        //Costructor
        public Arbol()
        {
            arbolCuentas = null;
        }


        public void crearArbol(string pNombreEmpresa)    //crear el árbol para una compañía
        {
            this.arbolCuentas = new NodoRaiz(pNombreEmpresa);
        }

        public void cargarArbol()   //desde BD
        {

        }

        //funcion que retorna el nodo con la cuenta que se desea, se envía como parámetro el codCuenta
        public NodoCuenta buscarCuenta(string pCodCuenta)
        {
            NodoCuenta recorridoArbol = null;

            //dividrir el codPadre en tokens
            char[] delimiterChars = { '-' };
            string[] tokens = pCodCuenta.Split(delimiterChars);

            //case para determinar el primer token del cod
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

            //si existen más tokens
            if (tokens.Length > 1) 
            {
                //ciclo para recorrer el arbol de manera vertical
                for (int i = 1; i< tokens.Length; i++)
                {
                    bool encontrado = false;
                    recorridoArbol = recorridoArbol.listaCuentasHijas;
                    //ciclo para recorrer la rama a nivel horizontal
                    while (!(encontrado)&&recorridoArbol!=null)
                    {
                        string[] tokensCodigoCuenta = recorridoArbol.codigo.Split(delimiterChars);
                        if (tokens[i] == tokensCodigoCuenta[i])
                        {
                            //recorridoArbol = recorridoArbol.listaCuentasHijas;
                            encontrado = true;
                        }
                        else
                            recorridoArbol = recorridoArbol.sigCuenta;
                    }
                    if (!(encontrado))
                        Console.WriteLine("Error, no se encontro el código");
                }
            }

            return recorridoArbol;
        }

        //procedimiento para insertar una nueva cuenta en el arbolCuentas
        public void insertarNuevaCuenta(Cuenta pCuenta)
        {
            //busca la cuenta padre
            NodoCuenta cuentaPadre = buscarCuenta(pCuenta.CodigoCuentaPadre);
            //crea la instancia de la nueva cuenta
            NodoCuenta nuevaCuenta = new NodoCuenta(pCuenta.Codigo, pCuenta.Nombre, true);
            nuevaCuenta.moneda = pCuenta.Moneda;
            nuevaCuenta.cuentaPadre = cuentaPadre;
            //la cuenta padre para a estar desactivada para movimientos
            cuentaPadre.cuentaActiva = false;

            //si la cuentaPadre no posee hijos
            if (cuentaPadre.listaCuentasHijas == null)
                //se le asigna el primer hijo
                cuentaPadre.listaCuentasHijas = nuevaCuenta;
            else
            {
                //se inserta la cuenta nueva cuenta en la lista simple de cuentas hijas
                cuentaPadre.listaCuentasHijas.antCuenta = nuevaCuenta;
                nuevaCuenta.sigCuenta = cuentaPadre.listaCuentasHijas;
                cuentaPadre.listaCuentasHijas = nuevaCuenta;
            }
        }

        //procedimiento para eliminar una cuenta que no posea moviminetos relacionados, recibe como parámetro el cod de la cuenta
        public void eliminarCuenta(string pCodCuenta)
        {
            /*
             * validar desde BD que la cuenta no posea moviminetos asociados
             * si no poseía movimientos asociados se realiza:
             *
             */ 
            NodoCuenta cuentaAEliminar = buscarCuenta(pCodCuenta);
            //Si es una hoja del árbol de cuentas, se puede eliminar
            if (cuentaAEliminar.cuentaActiva == true)
            { 
                //si es el primer nodo cuenta de la lista de hijos
                if (cuentaAEliminar.antCuenta == null)
                {
                    cuentaAEliminar.cuentaPadre.listaCuentasHijas = cuentaAEliminar.sigCuenta;
                    if (cuentaAEliminar.sigCuenta!=null)
                        cuentaAEliminar.sigCuenta.antCuenta = null;
                }
                else
                {
                    //si es el último nodo de la lista de hijos
                    if (cuentaAEliminar.sigCuenta == null)
                        cuentaAEliminar.antCuenta.sigCuenta = null;
                    //sino entonces esta en medio de la lista de cuentas hijas
                    else
                    {
                        cuentaAEliminar.sigCuenta.antCuenta = cuentaAEliminar.antCuenta;
                        cuentaAEliminar.antCuenta.sigCuenta = cuentaAEliminar.sigCuenta;
                    }
                }
                //se eliminan referencia para que el recolector de basura libere memoria
                cuentaAEliminar.antCuenta = null;
                cuentaAEliminar.cuentaPadre = null;
                cuentaAEliminar.sigCuenta = null;
            }
            else
                Console.WriteLine("Imposible borrar cuenta" + cuentaAEliminar.nombre + ", ya que es un padre del árbol");
        }

        //procedimiento para cambiar el nombre de una cuenta
        public void modificarNombreCuenta(string pCodCuenta, string pNuevoNombre)
        {
            buscarCuenta(pCodCuenta).nombre = pNuevoNombre;
        }

        //procedimiento para cambiar el estado de una cuenta
        public void modificarEstadoCuenta(string pCodCuenta, bool pNuevoEstado)
        {
            buscarCuenta(pCodCuenta).cuentaActiva = pNuevoEstado;
        }

        //procedimiento para cambiar la moneda a otra o bien a TODAS las monedas de una cuenta
        public void modificarMonedaCuenta(string pCodCuenta, string pNuevaMoneda)
        {
            buscarCuenta(pCodCuenta).moneda = pNuevaMoneda;
        }

        //método para actualizar los saldos de las cuentas en moneda local y del sistema de una sola Cuenta
        //se ejecuta cada vez que se realiza un movimiento (asiento) 
        public void actualizarSaldoCuenta(string pCodCuenta, bool pTipoOperacion, double pMontoMonedaLocal, double pMontoMonedaSistema)
        { 
            NodoCuenta cuentaAActualizar = buscarCuenta(pCodCuenta);
            //pTipoOperacion==true es incrmentar saldo
            //pTipoOperacion==false es decrementar saldo
            if (pTipoOperacion)
            {
                cuentaAActualizar.saldoEnLocal += pMontoMonedaLocal;
                cuentaAActualizar.saldoEnSistema += pMontoMonedaSistema;
            }
            else
            {
                cuentaAActualizar.saldoEnLocal -= pMontoMonedaLocal;
                cuentaAActualizar.saldoEnSistema -= pMontoMonedaSistema;
            }
        }
    }
}
