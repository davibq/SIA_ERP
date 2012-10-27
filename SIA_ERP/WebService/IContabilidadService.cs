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
        bool CrearCuenta(Cuenta pCuenta);

        [OperationContract]
        bool GuardarPeriodoContable(Mes[] pArregloMeses);

        [OperationContract]
        IEnumerable<Cuenta> DemeCuentasHijas();

        [OperationContract]
        IEnumerable<Moneda> DemeMonedasCuenta(string pCuenta);

        [OperationContract]
        double DemeCambio(Moneda pOrigen, double pValor, Moneda pDestino);

        [OperationContract]
        bool AgregarAsiento(string Fecha, double MontoDebe, double MontoHaber, string pXML);

        [OperationContract]
        Moneda ObtenerMonedasSistema(string pAtributo);

        [OperationContract]
        void InsertarAsiento(Asiento pAs);

        [OperationContract]
<<<<<<< HEAD
        double ConvertirAMonedaSistema(MonedasValidas pMoneda, double pValor);
=======
        IEnumerable<Cuenta> ObtenerCuentasHijasSegunPadre(string pNombrePadre);
>>>>>>> d54a7d10ed6198dc13e410eec96523c2331afb77
    }

}
