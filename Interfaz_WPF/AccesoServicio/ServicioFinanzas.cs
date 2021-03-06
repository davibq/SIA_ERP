﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AccesoServicio.FinanzasService;

namespace AccesoServicio
{
    public class ServicioFinanzas
    {
        private ServicioFinanzas()
        {
            _CSC = new ContabilidadServiceClient();
        }

        ~ServicioFinanzas()
        {

            _CSC.Close();
        }

        public static ServicioFinanzas Instancia
        {
            get
            {
                if (_Instancia == null)
                {
                    lock (_Lock)
                    {
                        if (_Instancia == null)
                        {
                            _Instancia = new ServicioFinanzas();
                        }
                    }
                }
                return _Instancia;
            }
        }

        public String Saludar()
        {
            return _CSC.Saludar();
        }

        public string ObtenerEmpresas()
        {
            return _CSC.ObtenerEmpresas();
        }

        public IEnumerable<Moneda> ObtenerMonedas()
        {
            return _CSC.ObtenerMonedas();
        }

        public bool AutenticarUsuario(Usuario pUsuario, string pNombreEmpresa)
        {
            return _CSC.AutenticarUsuario(pUsuario, pNombreEmpresa);
        }

        public bool InsertarNuevoUsuario(Usuario pUsuario)
        {
            return _CSC.InsertarNuevoUsuario(pUsuario);
        }

        public bool InsertarNuevaEmpresa(Empresa pEmpresa, byte[] pLogo)
        {
            return _CSC.InsertarNuevaEmpresa(pEmpresa, pLogo);
        }

        public bool InsertarNuevaMoneda(Moneda pMoneda)
        {
            return _CSC.InsertarNuevaMoneda(pMoneda);
        }

        public bool CrearCuenta(Cuenta pCuenta, string pXml)
        {
            return _CSC.CrearCuenta(pCuenta, pXml);
        }

        public bool GuardarPeriodoContable(string pFechaIn, string pFechaFin, int pAno, Mes[] pArregloMeses)
        {
            return _CSC.GuardarPeriodoContable(pFechaIn, pFechaFin, pAno, pArregloMeses);
        }

        public IEnumerable<Cuenta> DemeCuentasHijas()
        {
            return _CSC.DemeCuentasHijas();
        }

        public IEnumerable<Moneda> DemeMonedasCuenta(string pCuenta)
        {
            return _CSC.DemeMonedasCuenta(pCuenta);
        }

        public double DemeCambio(Moneda pOrigen, double pValor, Moneda pDestino)
        {
            return _CSC.DemeCambio(pOrigen, pValor, pDestino);
        }

        public bool InsertarAsiento(string Fecha, double MontoDebe, double MontoHaber, string pXML, string pTipoAsiento)
        {
            return _CSC.AgregarAsiento(Fecha, MontoDebe, MontoHaber, pXML, pTipoAsiento);
        }

        public Moneda ObtenerMonedasSistema(string pTipo)
        {
            return _CSC.ObtenerMonedasSistema(pTipo);
        }

        public double ConvertirAMonedaSistema(MonedasValidas pMoneda, double pValor)
        {
            return _CSC.ConvertirAMonedaSistema(pMoneda, pValor);
        }

        public double ConvertirAMonedaLocal(Moneda pMoneda, double pValor)
        {
            return _CSC.ConvertirAMonedaLocal(pMoneda, pValor);
        }

        public IEnumerable<Cuenta> ObtenerCuentasHijasSegunPadre(string pNombrePadre)
        {
            return _CSC.ObtenerCuentasHijasSegunPadre(pNombrePadre);
        }

        public IEnumerable<Cuenta> ObtenerCuentasCierreCompras()
        {
            return _CSC.ObtenerCuentasCierreCompras();
        }

        public IEnumerable<Cuenta> ObtenerCuentasCierreIngresos()
        {
            return _CSC.ObtenerCuentasCierreIngresos();
        }

        public IEnumerable<Cuenta> ObtenerCuentasCierreGastos()
        {
            return _CSC.ObtenerCuentasCierreGastos();
        }

        public Cuenta ObtenerCuenta(string pNombreCuenta)
        {
            return _CSC.ObtenerCuenta(pNombreCuenta);
        }

        public IEnumerable<Cuenta> ObtenerCuentasTreeView()
        {
            return _CSC.ObtenerCuentasTreeView();
        }

        public IEnumerable<Cuenta> ObtenerCuentasDeMayorSN()
        {
            return _CSC.ObtenerCuentasDeMayorSN();
        }

        public int ObtenerIdMoneda(string moneda)
        {
            return _CSC.ObtenerIdMoneda(moneda);
        }

        #region ModuloInventario

        public IEnumerable<UnidadMedida> ObtenerUnidadesdeMedida()
        {
            return _CSC.obtenerUnidadesMedida();
        }

        public IEnumerable<Bodega> obtenerBodegas()
        {
            return _CSC.obtenerBodegas();
        }

        public bool crearArticulo(Articulo pArticulo)
        {
            return _CSC.crearArticulo(pArticulo);
        }

        public bool crearBodega(Bodega pBodega)
        {
            return _CSC.crearBodega(pBodega);
        }

        public IEnumerable<Cuenta> obtenerCuentasInventario()
        {
            return _CSC.obtenerCuentasInventario();
        }

        public IEnumerable<Cuenta> obtenerCuentasVentas()
        {
            return _CSC.obtenerCuentasVentas();
        }

        public IEnumerable<Cuenta> obtenerCuentasCostos()
        {
            return _CSC.obtenerCuentasCostos();
        }

        #endregion


        public IEnumerable<SocNegocio> ObtenerSociosNegocioCV(string pTipoSocio)
        {
            return _CSC.ObtenerSociosCV(pTipoSocio);
        }

        public IEnumerable<ProductoCV> ObtenerProductosCV()
        {
            return _CSC.ObtenerProductosCV();
        }

        public bool CrearSocioDeNegocio(string Nombre, string Codigo, string Email, string TipoSocio, int IdMoneda, string CuentaAsociada, double LimiteCredito)
        {
            return _CSC.CrearSocioDeNegocio(Nombre, Codigo, Email, TipoSocio, IdMoneda, CuentaAsociada, LimiteCredito);
        }

        public string ObtenerCuentaDeMayorXCodigo(string CodigoSN)
        {
            return _CSC.ObtenerCuentaDeMayorXCodigo(CodigoSN);
        }

        public int ObtenerIDMonedaCuentaDeMayorXCodigo(string CodigoSN)
        {
            return _CSC.ObtenerIDMonedaCuentaDeMayorXCodigo(CodigoSN);
        }

        public string ObtenerNombreCuentaDeMayorSN(string CodigoSN)
        {
            return _CSC.ObtenerNombreCuentaDeMayorSN(CodigoSN);
        }

        public string ObtenerSaldoCuenta(string CodigoCuentaSN, int IdMoneda)
        {
            return _CSC.ObtenerSaldoCuenta(CodigoCuentaSN, IdMoneda);
        }

        public bool GuardarDocumento(Documento pDocumento)
        {
            return _CSC.GuardarDocumento(pDocumento);
        }

        public IEnumerable<Documento> ObtenerDocumentosCompras()
        {
            return _CSC.ObtenerDocumentosCompras();
        }

        public IEnumerable<Documento> ObtenerDocumentosVentas()
        {
            return _CSC.ObtenerDocumentosVentas();
        }

        public Documento ObtenerDocumento(int pIdDocumento)
        {
            return _CSC.ObtenerDocumento(pIdDocumento);
        }

        public IEnumerable<Documento> ObtenerFacturasXEstadoXSocioNegocio(string pCodSN, string pEstadoFactura)
        {
            return _CSC.ObtenerFacturasXEstadoXSocioNegocio(pCodSN, pEstadoFactura);
        }

        public IEnumerable<Banco> obtenerBancos()
        {
            return _CSC.obtenerBancos();
        }

        public bool setearFacturas(int idDoc, string pEstado)
        {
            return _CSC.setearFacturas(idDoc, pEstado);
        }

        public bool insertarTransferencia(Transferencia pTransferencia)
        {
            return _CSC.insertarTransferencia(pTransferencia);
        }

        public bool insertarBanco(Banco pBanco)
        {
            return _CSC.insertarBanco(pBanco);
        }

        public IEnumerable<Cuenta> ObtenerCuentasHojas(string pNombrePadre)
        {
            return _CSC.ObtenerCuentasHojas(pNombrePadre);
        }

        public ConsultaSaldo consultarCreditoSaldo(string pCodigoCliente)
        {
            return _CSC.consultarCreditoSaldo(pCodigoCliente);
        }

        public IEnumerable<Articulo> obtenerArticuloXBodeba(string pCodArticulo, string pCodBodega)
        {
            return _CSC.obtenerArticuloXBodeba(pCodArticulo, pCodBodega);
        }

        public IEnumerable<Articulo> obtenerTodosArticulos()
        {
            return _CSC.obtenerTodosArticulos();
        }

        public IEnumerable<Cuenta> obtenerAsientos()
        {
            return _CSC.obtenerAsientos();
        }

        private ContabilidadServiceClient _CSC;

        private static ServicioFinanzas _Instancia;
        private static object _Lock = new object();
    }
}