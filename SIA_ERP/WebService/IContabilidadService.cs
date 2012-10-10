using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SIA.Libreria;
using SIA.Contabilidad.Libreria;

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
        string ObtenerMonedas();//string pBaseDatos);

        [OperationContract]
        bool AutenticarUsuario(Usuario pUsuario, string pNombreEmpresa);

        [OperationContract]
        bool InsertarNuevoUsuario(Usuario pUsuario);

        [OperationContract]
        bool InsertarNuevaEmpresa(Empresa pEmpresa, byte[] pLogo);

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
        void InsertarAsiento(Asiento pAs);
    }

}
