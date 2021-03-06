﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIA.TipoCambio;
using SIA.ExceptionLog;
using Logica;
using SIA.Libreria;

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
            arbol.insertarNuevaCuenta("1", "1-11", "Inventario");
            Console.WriteLine("Nuevo nodo enlazado: " + arbol.arbolCuentas.activos.listaCuentasHijas.nombre);
            Console.WriteLine("Nuevo nodo enlazado: " + arbol.arbolCuentas.activos.listaCuentasHijas.sigCuenta.nombre);

            arbol.insertarNuevaCuenta("1-10", "1-10-1", "BCR");
            arbol.insertarNuevaCuenta("1-10", "1-10-2", "BNCR");
            arbol.insertarNuevaCuenta("1-10-1", "1-10-1-1", "BCR colones");
            Console.WriteLine("Nuevo nodo enlazado: " + arbol.arbolCuentas.activos.listaCuentasHijas.nombre);
            Console.WriteLine("Nuevo nodo enlazado: " + arbol.arbolCuentas.activos.listaCuentasHijas.sigCuenta.nombre);
            Console.WriteLine("Nuevo nodo enlazado: " + arbol.arbolCuentas.activos.listaCuentasHijas.sigCuenta.listaCuentasHijas.nombre);
            Console.WriteLine("Nuevo nodo enlazado: " + arbol.arbolCuentas.activos.listaCuentasHijas.sigCuenta.listaCuentasHijas.sigCuenta.nombre);
            Console.WriteLine("Nuevo nodo enlazado: " + arbol.arbolCuentas.activos.listaCuentasHijas.sigCuenta.listaCuentasHijas.sigCuenta.listaCuentasHijas.nombre);

            arbol.insertarNuevaCuenta("2", "2-10", "Cuentas por pagar");
            Console.WriteLine("Nuevo nodo enlazado: " + arbol.arbolCuentas.pasivos.listaCuentasHijas.nombre);
            arbol.insertarNuevaCuenta("3", "3-10", "Capital");
            Console.WriteLine("Nuevo nodo enlazado: " + arbol.arbolCuentas.patrimonio.listaCuentasHijas.nombre);
            arbol.insertarNuevaCuenta("4", "4-10", "Ventas");
            Console.WriteLine("Nuevo nodo enlazado: " + arbol.arbolCuentas.ingresos.listaCuentasHijas.nombre);
            arbol.insertarNuevaCuenta("5", "5-10", "Compras");
            Console.WriteLine("Nuevo nodo enlazado: " + arbol.arbolCuentas.costos.listaCuentasHijas.nombre);
            arbol.eliminarCuenta("1-10-1-1");
            arbol.eliminarCuenta("2-10");
            arbol.eliminarCuenta("8");

            arbol.modificarEstadoCuenta("1-10-2", false);
            arbol.modificarMonedaCuenta("1-10-2", "CRC");
            arbol.modificarNombreCuenta("1-10-2", "Banco Popular");

            arbol.actualizarSaldoCuenta("1-10-2", true, 10, 200.55);
            arbol.actualizarSaldoCuenta("1-10-2", false, 1, 20.55);

            System.Console.Read();
            */

            /*            
             * EJEMPLO: PEDIR TIPO DE CAMBIO AL BCCR*/
            /*
            if (TiposCambio.Instancia.SonValoresValidos)
            {
                Console.WriteLine("$25 es igual a: C");
                Console.ReadLine();
            }
            */
            Usuario usuario = new Usuario()
            {
                NombreUsuario="davibq",
                Password="12345"
            };
            var d=LogicaNegocio.Instancia.AutenticarUsuario(usuario, "Empresa1");
            /*
             * EJEMPLO: MANEJO DE EXCEPCIONES
             * 
            try
            {
                int j = 10;
                int i = 24 / (j - 10);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExcepcion(ex, "Division entre 0");
            }
             */
        }
    }
}
