using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Logica;
using SIA.Libreria;

namespace ERPAppWebService
{
    public class Servicio : IServicio
    {
        #region IServicio Members

        public Articulo[] obtenerArticulos()
        {
            return LogicaInventario.Instancia.obtenerArticulos().ToArray();
        }

        public int[] registrarPedido(List<RequestPedido> pPedido)
        {
            //TODO: Enviar estos pedidos a la base de datos

            List<int> idsPedidos = new List<int>();

            foreach (RequestPedido pedido in pPedido)
            {
                idsPedidos.Add(pedido._IdFactura);
            }

            return idsPedidos.ToArray();
        }


        public List<string> consultarCantidadPrecio(RequestConsulta pRequestConsulta)
        {
            return LogicaInventario.Instancia.consultarCantidadPrecio(pRequestConsulta._Cliente, pRequestConsulta._Articulo);
        }

        public List<string> consultarClienteArticulo(RequestConsulta pRequestConsulta)
        {
            return LogicaInventario.Instancia.obtenerHistorialArticulosCliente(pRequestConsulta._Cliente);
        }

        public ConsultaSaldo consultarCreditoSaldo(RequestConsulta pRequestConsulta)
        {
            return LogicaInventario.Instancia.consultarCreditoSaldo(pRequestConsulta._Cliente);
        }
    
        #endregion
    }
}