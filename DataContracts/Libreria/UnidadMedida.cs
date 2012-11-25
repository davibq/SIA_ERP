using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SIA.Libreria
{

    [DataContract]
    class UnidadMedida
    {
        [DataMember]
        public string Nombre { get; set; }
    }
}
