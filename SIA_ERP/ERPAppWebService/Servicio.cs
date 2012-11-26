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
      /*
            // TODO: Obtener los articulos de la base de datos
            // Ejemplo de como se esperan en el app
            List<Articulo> a = new List<Articulo>();
            a.Add(new Articulo { _Nombre = "Crema", _Descripcion = "Lo deja brillando", _Imagen = "http://t3.gstatic.com/images?q=tbn:ANd9GcQNcLPzMZQGZwXVcoc1ugTIsczdtcJUYiht8Q9VvKrig_8t9ug33AdeKT5b", _Precio = 150f});
            a.Add(new Articulo { _Nombre = "Yogurt", _Descripcion = "Con 0 calorias", _Imagen = "http://2.bp.blogspot.com/_VamB3sTrYkU/SUZ9fDqlgaI/AAAAAAAAAZs/MswoscHpLBI/s400/Milkaut_productos_WEB.jpg", _Precio = 450f });
            a.Add(new Articulo { _Nombre = "Energizante", _Descripcion = "Te hace recuperar toda la energia en un solo trago", _Imagen = "http://www.gastronomiaycia.com/wp-content/uploads/2008/06/zumosol_activo_plus.jpg", _Precio = 750f });
            return a.ToArray();
       * */
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


        public string consultarCantidadPrecio(RequestConsulta pRequestConsulta)
        {
            //TODO: Consultar
            return "consultarCantidadPrecio";
        }

        public string consultarClienteArticulo(RequestConsulta pRequestConsulta)
        {
            //TODO: Consultar
            return "consultarClienteArticulo";
        }

        public string consultarCreditoSaldo(RequestConsulta pRequestConsulta)
        {
            //TODO: Consultar
            return "consultarCreditoSaldo";
        }
    
        #endregion
    }
}