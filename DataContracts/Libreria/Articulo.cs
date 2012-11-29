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

        [DataMember]
        public Cuenta Existencias { get; set; }

        [DataMember]
        public Cuenta Ventas { get; set; }

        [DataMember]
        public Cuenta Costos { get; set; }

        [DataMember]
        public Bodega bodega { get; set; }

        #region Atributos para el App
        [DataMember]
        public string Precio { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string UrlImagen { get; set; }
        #endregion
    }
}
