using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Security.Cryptography;

namespace SIA.Libreria
{
    [DataContract]
    public class Mes
    {
        [DataMember]
        public string NombreMes { get; set; }

        [DataMember]
        public string FechaInicio { get; set; }

        [DataMember]
        public string FechaFin { get; set; }

        [DataMember]
        public string EstadoMes { get; set; }
    }
}