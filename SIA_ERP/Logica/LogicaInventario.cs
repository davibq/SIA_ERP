﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessInventario;
using SIA.Libreria;

namespace Logica
{
    public class LogicaInventario
    {
        #region Constructor

        private LogicaInventario()
        {
            _DataAccess = new DAInventario("ERP_Ventas");
        }

        #endregion

        #region Propiedades

        public static LogicaInventario Instancia
        {
            get
            {
                if (_Instancia == null)
                {
                    lock (_Lock)
                    {
                        if (_Instancia == null)
                        {
                            _Instancia = new LogicaInventario();
                        }
                    }

                }
                return _Instancia;
            }
        }

        #endregion

        #region Metodos

        public IEnumerable<UnidadMedida> obtenerUnidadesMedida()
        {
            return _DataAccess.obtenerUnidadesMedida();
        }

        public List<Bodega> obtenerBodegas()
        {
            return _DataAccess.obtenerBodegas();
        }

        public bool crearArticulo(Articulo pArticulo)
        {
            return _DataAccess.crearArticulo(pArticulo);
        }

        public bool crearBodega(Bodega pBodega)
        {
            return _DataAccess.crearBodega(pBodega);
        }

        public List<Banco> obtenerBancos()
        {
            return _DataAccess.obtenerBancos();
        }

        #endregion

        #region Métodos App

        public List<Articulo> obtenerArticulos()
        {
            return _DataAccess.obtenerArticulos();
        }

        public List<string> obtenerHistorialArticulosCliente(string pCodigoCliente)
        {
            return _DataAccess.obtenerHistorialArticulosCliente(pCodigoCliente);
        }

        public List<string> consultarCantidadPrecio(string pCliente, string pArticulo)
        {
            return _DataAccess.consultarCantidadPrecio(pCliente, pArticulo);
        }

        public bool insertarBanco(Banco pBanco)
        {
            return _DataAccess.insertarBanco(pBanco);
        }

        public List<Articulo> obtenerArticuloXBodeba(string pCodArticulo, string pCodBodega)
        {
            return _DataAccess.obtenerArticuloXBodeba(pCodArticulo, pCodBodega);
        }

        public List<Articulo> obtenerTodosArticulos()
        {
            return _DataAccess.obtenerTodosArticulos();
        }

        #endregion

        #region Metodos ERPApp

        public ConsultaSaldo consultarCreditoSaldo(string pCodigoCliente)
        {
            return _DataAccess.consultarCreditoSaldo(pCodigoCliente);
        }

        #endregion

        #region Atributos

        private DAInventario _DataAccess;
        private static LogicaInventario _Instancia;
        private static object _Lock = new object();

        #endregion
    }
}
