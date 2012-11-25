using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using SIA.Libreria;

namespace SIA.Libreria
{
    [DataContract]
    public class Articulo
    {
        [DataMember]
        public string Codigo { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public UnidadMedida unidadMedida { get; set; }

        [DataMember]
        public string Comentarios { get; set; }

        [DataMember]
        public byte[] imagen { get; set; }
    }
}
