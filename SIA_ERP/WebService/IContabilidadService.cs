using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SIA.Libreria;

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
        string ObtenerMonedas(string pBaseDatos);

        [OperationContract]
        bool AutenticarUsuario(Usuario pUsuario, string pNombreEmpresa);

        [OperationContract]
        bool InsertarNuevoUsuario(Usuario pUsuario);

        [OperationContract]
        bool InsertarNuevaEmpresa(Empresa pEmpresa, byte[] pLogo);

        [OperationContract]
        bool CrearCuenta(Cuenta pCuenta);
    }

}
