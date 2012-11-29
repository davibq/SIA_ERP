using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SIA.Libreria;
using SIA.Contabilidad.Libreria;
using SIA.TipoCambio;
using SIA.VentaCompra.Libreria;

namespace SIA.Contabilidad.WebService
{
    [ServiceContract]
    public interface IContabilidadService
    {
        [OperationContract]
        string Saludar();

        [OperationContract]
        string ObtenerEmpresas();

        [OperationContract]
        IEnumerable<Moneda> ObtenerMonedas();

        [OperationContract]
        bool AutenticarUsuario(Usuario pUsuario, string pNombreEmpresa);

        [OperationContract]
        bool InsertarNuevoUsuario(Usuario pUsuario);

        [OperationContract]
        bool InsertarNuevaEmpresa(Empresa pEmpresa, byte[] pLogo);

        [OperationContract]
        bool InsertarNuevaMoneda(Moneda pMoneda);

        [OperationContract]
        bool CrearCuenta(Cuenta pCuenta, string pXml);

        [OperationContract]
        bool GuardarPeriodoContable(string pFechaIn, string pFechaFin, int pAno, Mes[] pArregloMeses);

        [OperationContract]
        IEnumerable<Cuenta> DemeCuentasHijas();

        [OperationContract]
        IEnumerable<Moneda> DemeMonedasCuenta(string pCuenta);

        [OperationContract]
        double DemeCambio(Moneda pOrigen, double pValor, Moneda pDestino);

        [OperationContract]
        bool AgregarAsiento(string Fecha, double MontoDebe, double MontoHaber, string pXML, string pTipoAsiento);

        [OperationContract]
        Moneda ObtenerMonedasSistema(string pAtributo);

        [OperationContract]
        void InsertarAsiento(Asiento pAs);

        [OperationContract]
        double ConvertirAMonedaSistema(MonedasValidas pMoneda, double pValor);

        [OperationContract]
        double ConvertirAMonedaLocal(Moneda pMoneda, double pValor);

        [OperationContract]
        IEnumerable<Cuenta> ObtenerCuentasHijasSegunPadre(string pNombrePadre);

        [OperationContract]
        IEnumerable<Cuenta> ObtenerCuentasCierreCompras();

        [OperationContract]
        IEnumerable<Cuenta> ObtenerCuentasCierreIngresos();

        [OperationContract]
        IEnumerable<Cuenta> ObtenerCuentasCierreGastos();

        [OperationContract]
        Cuenta ObtenerCuenta(string pNombreCuenta);

        [OperationContract]
        IEnumerable<Cuenta> ObtenerCuentasTreeView();
        
        [OperationContract]
        IEnumerable<ProductoCV> ObtenerProductosCV();

        [OperationContract]
        IEnumerable<Cuenta> ObtenerCuentasDeMayorSN();

        [OperationContract]
        int ObtenerIdMoneda(string moneda);
        [OperationContract]
        IEnumerable<Cuenta> obtenerCuentasInventario();

        [OperationContract]
        IEnumerable<Cuenta> obtenerCuentasVentas();

        [OperationContract]
        IEnumerable<Cuenta> obtenerCuentasCostos();
        
        #region Modulo inventarios

        [OperationContract]
        IEnumerable<UnidadMedida> obtenerUnidadesMedida();

        [OperationContract]
        IEnumerable<Bodega> obtenerBodegas();

        [OperationContract]
        bool crearArticulo(Articulo pArticulo);

        [OperationContract]
        bool crearBodega(Bodega pBodega);
        
        #endregion

        [OperationContract]
        IEnumerable<SocNegocio> ObtenerSociosCV(string pTipoSocio);

        [OperationContract]
        bool GuardarDocumento(Documento pDocumento);
    
        [OperationContract]
        IEnumerable<Documento> ObtenerDocumentosCompras();

        [OperationContract]
        Documento ObtenerDocumento(int pIdDocumento);

        [OperationContract]
        IEnumerable<Documento> ObtenerFacturasXEstadoXSocioNegocio(string pCodSN, string pEstadoFactura);

        [OperationContract]
        IEnumerable<Banco> obtenerBancos();

        [OperationContract]
        bool setearFacturas(int idDoc, string pEstado);

        [OperationContract]
        bool insertarTransferencia(Transferencia pTransferencia);

        [OperationContract]
        IEnumerable<Documento> ObtenerDocumentosVentas();

        [OperationContract]
        bool CrearSocioDeNegocio(string Nombre, string Codigo, string Email, string TipoSocio, int IdMoneda, string CuentaAsociada, double LimiteCredito);

        [OperationContract]
        string ObtenerCuentaDeMayorXCodigo(string CodigoSN);

        [OperationContract]
        int ObtenerIDMonedaCuentaDeMayorXCodigo(string CodigoSN);

        [OperationContract]
        string ObtenerNombreCuentaDeMayorSN(string CodigoSN);

        [OperationContract]
        string ObtenerSaldoCuenta(string CodigoCuentaSN, int IdMoneda);

        [OperationContract]
        bool insertarBanco(Banco pBanco);

        [OperationContract]
        IEnumerable<Cuenta> ObtenerCuentasHojas(string pNombre);

        [OperationContract]
        ConsultaSaldo consultarCreditoSaldo(string pCodigoCliente);

        [OperationContract]
        IEnumerable<Articulo> obtenerArticuloXBodeba(string pCodArticulo, string pCodBodega);

        [OperationContract]
        IEnumerable<Articulo> obtenerTodosArticulos();

        [OperationContract]
        IEnumerable<Cuenta> obtenerAsientos();
    }

}
