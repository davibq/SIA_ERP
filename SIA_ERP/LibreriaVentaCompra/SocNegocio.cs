using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SIA.VentaCompra.Libreria
{
    [DataContract]
    public class SocNegocio
    {
        [DataMember]
        public int IdSocio { get; set; }

        [DataMember]
        public string Codigo { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string TipoSocio { get; set; }

        [DataMember]
        public int IdMoneda { get; set; }

        [DataMember]
        public string CuentaAsociada { get; set; }

        [DataMember]
        public double LimiteCredito { get; set; }
    }
}
