using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using SIA.Libreria;

namespace SIA.VentaCompra.Libreria
{
    [DataContract]
    public class OrdenCompras
    {
        [DataMember]
        public string codigo { get; set; }

        [DataMember]
        public string fechaContabilizacion { get; set; }

        [DataMember]
        public string fechaEntrega { get; set; }

        [DataMember]
        public SocNegocio proveedor { get; set; }

        [DataMember]
        public Productos producto { get; set; }
    }
}