﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SIA.Libreria;
using Logica;
using SIA.Contabilidad.Libreria;
using SIA.TipoCambio;
using SIA.VentaCompra.Libreria;

namespace SIA.Contabilidad.WebService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ContabilidadService : IContabilidadService
    {
        public string Saludar()
        {
            return "Hola";
        }

        public string ObtenerEmpresas()
        {
            return LogicaNegocio.Instancia.ObtenerEmpresas();
        }

        public IEnumerable<Moneda> ObtenerMonedas()
        {
            return LogicaNegocio.Instancia.ObtenerMonedas();
        }

        public bool AutenticarUsuario(Usuario pUsuario, string pNombreEmpresa)
        {
            return LogicaNegocio.Instancia.AutenticarUsuario(pUsuario, pNombreEmpresa);
        }

        public bool InsertarNuevoUsuario(Usuario pUsuario)
        {
            return LogicaNegocio.Instancia.InsertarNuevoUsuario(pUsuario);
        }

        public bool InsertarNuevaEmpresa(Empresa pEmpresa, byte[] pLogo)
        {
            return LogicaNegocio.Instancia.InsertarNuevaEmpresa(pEmpresa, pLogo);
        }

        public bool InsertarNuevaMoneda(Moneda pMoneda)
        {
            return LogicaNegocio.Instancia.InsertarNuevaMoneda(pMoneda);
        }

        public bool CrearCuenta(Cuenta pCuenta, string pXml)
        {
            return LogicaNegocio.Instancia.CrearCuenta(pCuenta, pXml);
        }

        public bool GuardarPeriodoContable(string pFechaIn, string pFechaFin, int pAno, Mes[] pArregloMeses)
        {
            return LogicaNegocio.Instancia.GuardarPeriodoContable(pFechaIn,pFechaFin,pAno,pArregloMeses);
        }

        public IEnumerable<Cuenta> DemeCuentasHijas()
        {
            return LogicaNegocio.Instancia.DemeCuentasHijas();
        }

        public IEnumerable<Moneda> DemeMonedasCuenta(string pCuenta)
        {
            var monedas = LogicaNegocio.Instancia.DemeMonedasCuenta(pCuenta);
            return monedas;
        }

        public void InsertarAsiento(Asiento pAs) { }

        public double DemeCambio(Moneda pOrigen, double pValor, Moneda pDestino)
        {
            return TiposCambio.Instancia.DemeCambio(pOrigen.TipoMoneda, pValor, pDestino.TipoMoneda);
        }

        public bool AgregarAsiento(string Fecha, double MontoDebe, double MontoHaber, string pXML, string pTipoAsiento)
        {
            return LogicaNegocio.Instancia.AgregarAsiento(Fecha, MontoDebe, MontoHaber, pXML, pTipoAsiento);
        }

        public Moneda ObtenerMonedasSistema(string pAtributo)
        {
            return LogicaNegocio.Instancia.ObtenerMonedasSistema(pAtributo);
        }

        public double ConvertirAMonedaSistema(MonedasValidas pMoneda, double pValor)
        {
            return LogicaNegocio.Instancia.ConvertirAMonedaSistema(pMoneda, pValor);
        }

        public double ConvertirAMonedaLocal(Moneda pMoneda, double pValor)
        {
            return LogicaNegocio.Instancia.ConvertirAMonedaLocal(pMoneda, pValor);
        }

        public IEnumerable<Cuenta> ObtenerCuentasHijasSegunPadre(string pNombrePadre)
        {
            return LogicaNegocio.Instancia.ObtenerCuentasHijasSegunPadre(pNombrePadre);
        }

        public IEnumerable<Cuenta> ObtenerCuentasCierreCompras()
        {
            return LogicaNegocio.Instancia.ObtenerCuentasCierreCompras();
        }

        public IEnumerable<Cuenta> ObtenerCuentasCierreIngresos()
        {
            return LogicaNegocio.Instancia.ObtenerCuentasCierreIngresos();
        }

        public IEnumerable<Cuenta> ObtenerCuentasCierreGastos()
        {
            return LogicaNegocio.Instancia.ObtenerCuentasCierreGastos();
        }

        public Cuenta ObtenerCuenta(string pNombreCuenta)
        {
            return LogicaNegocio.Instancia.ObtenerCuenta(pNombreCuenta);
        }

        public IEnumerable<Cuenta> ObtenerCuentasTreeView()
        {
            return LogicaNegocio.Instancia.ObtenerCuentasTreeView();
        }

        public IEnumerable<Cuenta> ObtenerCuentasDeMayorSN()
        {
            return LogicaNegocio.Instancia.ObtenerCuentasDeMayorSN();
        }

        public int ObtenerIdMoneda(string moneda)
        {
            return LogicaNegocio.Instancia.ObtenerIdMoneda(moneda);
        }


        #region Modulo inventarios

        public IEnumerable<UnidadMedida> obtenerUnidadesMedida()
        {
            return LogicaInventario.Instancia.obtenerUnidadesMedida();
        }

        public IEnumerable<Bodega> obtenerBodegas()
        {
            return LogicaInventario.Instancia.obtenerBodegas();
        }

        public bool crearArticulo(Articulo pArticulo)
        {
            return LogicaInventario.Instancia.crearArticulo(pArticulo);
        }

        public bool crearBodega(Bodega pBodega)
        {
            return LogicaInventario.Instancia.crearBodega(pBodega);
        }

        public IEnumerable<Cuenta> obtenerCuentasInventario() 
        {
            return LogicaNegocio.Instancia.ObtenerCuentasHojas("INVENTARIOS");
        }

        public IEnumerable<Cuenta> obtenerCuentasVentas()
        {
            return LogicaNegocio.Instancia.ObtenerCuentasHojas("VENTAS");
        }

        public IEnumerable<Cuenta> obtenerCuentasCostos()
        {
            return LogicaNegocio.Instancia.ObtenerCuentasHojas("COSTOS");
        }

        #endregion

        public IEnumerable<SocNegocio> ObtenerSociosCV(string pTipoSocio)
        {
            return LogicaCompraVenta.Instancia.ObtenerSociosNegocio(pTipoSocio);
        }

        public IEnumerable<ProductoCV> ObtenerProductosCV()
        {
            return LogicaCompraVenta.Instancia.ObtenerProductos();
        }

        public bool CrearSocioDeNegocio(string Nombre, string Codigo, string Email, string TipoSocio, int IdMoneda, string CuentaAsociada, double LimiteCredito)
        {
            return LogicaCompraVenta.Instancia.CrearSocioDeNegocio(Nombre, Codigo, Email, TipoSocio, IdMoneda, CuentaAsociada, LimiteCredito);
        }

        public string ObtenerCuentaDeMayorXCodigo(string CodigoSN)
        {
            return LogicaCompraVenta.Instancia.ObtenerCuentaDeMayorXCodigo(CodigoSN);
        }

        public int ObtenerIDMonedaCuentaDeMayorXCodigo(string CodigoSN)
        {
            return LogicaCompraVenta.Instancia.ObtenerIDMonedaCuentaDeMayorXCodigo(CodigoSN);
        }

        public string ObtenerNombreCuentaDeMayorSN(string CodigoSN)
        {
            return LogicaNegocio.Instancia.ObtenerNombreCuentaDeMayorSN(CodigoSN);
        }

        public string ObtenerSaldoCuenta(string CodigoCuentaSN, int IdMoneda)
        {
            return LogicaNegocio.Instancia.ObtenerSaldoCuenta(CodigoCuentaSN, IdMoneda);
        }

        public bool GuardarDocumento(Documento pDocumento)
        {
            return LogicaCompraVenta.Instancia.GuardarDocumento(pDocumento);
        }

        public IEnumerable<Documento> ObtenerDocumentosCompras()
        {
            return LogicaCompraVenta.Instancia.ObtenerDocumentosCompras();
        }

        public IEnumerable<Documento> ObtenerDocumentosVentas()
        {
            return LogicaCompraVenta.Instancia.ObtenerDocumentosVentas();
        }

        public Documento ObtenerDocumento(int pIdDocumento)
        {
            return LogicaCompraVenta.Instancia.ObtenerDocumento(pIdDocumento);
        }

        public IEnumerable<Documento> ObtenerFacturasXEstadoXSocioNegocio(string pCodSN, string pEstadoFactura)
        {
            return LogicaCompraVenta.Instancia.ObtenerFacturasXEstadoXSocioNegocio(pCodSN, pEstadoFactura);
        }

        public IEnumerable<Banco> obtenerBancos()
        {
            return LogicaInventario.Instancia.obtenerBancos();
        }

        public bool setearFacturas(int idDoc, string pEstado)
        {
            return LogicaCompraVenta.Instancia.setearFacturas(idDoc,pEstado);
        }

        public bool insertarTransferencia(Transferencia pTransferencia)
        {
            return LogicaCompraVenta.Instancia.insertarTransferencia(pTransferencia);
        }

        public bool insertarBanco(Banco pBanco)
        {
            return LogicaInventario.Instancia.insertarBanco(pBanco);
        }

        public IEnumerable<Cuenta> ObtenerCuentasHojas(string pNombre)
        {
            return LogicaNegocio.Instancia.ObtenerCuentasHojas(pNombre);
        }

        public ConsultaSaldo consultarCreditoSaldo(string pCodigoCliente)
        {
            return LogicaInventario.Instancia.consultarCreditoSaldo(pCodigoCliente);
        }

        public IEnumerable<Articulo> obtenerArticuloXBodeba(string pCodArticulo, string pCodBodega)
        {
            return LogicaInventario.Instancia.obtenerArticuloXBodeba(pCodArticulo, pCodBodega);
        }

        public IEnumerable<Articulo> obtenerTodosArticulos()
        {
            return LogicaInventario.Instancia.obtenerTodosArticulos();
        }

        public IEnumerable<Cuenta> obtenerAsientos()
        {
            return LogicaNegocio.Instancia.obtenerAsientos();
        }
         
    }
}
