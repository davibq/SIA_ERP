using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using SIA.Libreria;

namespace ERPAppWebService
{
    [ServiceContract]
    public interface IServicio
    {
        [OperationContract]
        [WebGet(UriTemplate = "obtenerArticulos", ResponseFormat = WebMessageFormat.Json)]
        Articulo[] obtenerArticulos();

        [OperationContract]
        [WebInvoke(UriTemplate = "registrarPedido", ResponseFormat = WebMessageFormat.Json)]
        int[] registrarPedido(List<RequestPedido> pPedido);

        [OperationContract]
        [WebInvoke(UriTemplate = "consultarCantidadPrecio", ResponseFormat = WebMessageFormat.Json)]
        string consultarCantidadPrecio(RequestConsulta pRequestConsulta);

        [OperationContract]
        [WebInvoke(UriTemplate = "consultarClienteArticulo", ResponseFormat = WebMessageFormat.Json)]
        string consultarClienteArticulo(RequestConsulta pRequestConsulta);

        [OperationContract]
        [WebInvoke(UriTemplate = "consultarCreditoSaldo", ResponseFormat = WebMessageFormat.Json)]
        ConsultaSaldo consultarCreditoSaldo(RequestConsulta pRequestConsulta);
    }

    #region Clases consulta
    //TODO: Pasar estas clases a archivos separados, solo era para prueba
    [DataContract]
    public class RequestPedido
    {
        [DataMember]
        public List<Articulo> _Articulos { get; set; }
        [DataMember]
        public string _Cliente { get; set; }
        [DataMember]
        public int _IdFactura { get; set; }
    }

    [DataContract]
    public class RequestConsulta
    {
        [DataMember]
        public string _Cliente { get; set; }
        [DataMember]
        public string _Articulo { get; set; }
    }
    #endregion
}