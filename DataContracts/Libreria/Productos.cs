using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace SIA.Libreria
{
    [DataContract]
    public class Productos
    {
        [DataMember]
        public int cantidad { get; set; }

        [DataMember]
        public double precioUnit { get; set; }

        [DataMember]
        public int idArticulo { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public int idBodega { get; set; }

        [DataMember]
        public string NombreBodega { get; set; }

        private System.Data.DataTable objtabla;
        private System.Data.DataTable objtablaProducto;
        /// <summary>
        /// Crea una tabla para la ventana de ordenes de compra
        /// Muestra: producto, cantidad actual y cantidad minima
        /// </summary>
        /// <returns></returns>
        private bool CrearTabla()
        {
            try
            {
                objtabla = new System.Data.DataTable();
                objtabla.Columns.Add("Producto");
                objtabla.Columns.Add("Cantidad Actual");
                objtabla.Columns.Add("Cantidad Minima");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Crea una tabla para mostrar en la ventana de las facturas
        /// Muestra: producto
        /// </summary>
        /// <returns></returns>
        private bool CrearTablaProducto()
        {
            try
            {
                objtablaProducto = new System.Data.DataTable();
                objtablaProducto.Columns.Add("Producto");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Agrega nuevas filas a la tabla de la ventana de ordenes de compra
        /// </summary>
        /// <returns></returns>
        public bool AgregarFila()
        {
            if (objtabla == null)
            {
                if (CrearTabla() == false)
                    return false;
            }
            try
            {
                System.Data.DataRow objFila = objtabla.NewRow();
                objFila["Producto"] = producto;
                objFila["Cantidad Actual"] = cantActual;
                objFila["Cantidad Minima"] = cantMinima;
                objtabla.Rows.Add(objFila);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Agrega nuevas filas a la tabla de la ventana de facturas
        /// </summary>
        /// <returns></returns>
        public bool AgregarFilaProducto()
        {
            if (objtablaProducto == null)
            {
                if (CrearTablaProducto() == false)
                    return false;
            }
            try
            {
                System.Data.DataRow objFila = objtablaProducto.NewRow();
                objFila["Producto"] = producto;
                objtablaProducto.Rows.Add(objFila);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}