using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

namespace SIA.Libreria
{
    [DataContract]
    public class Empresa
    {
        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string CedulaJuridica { get; set; }

        [DataMember]
        public bool Enabled{ get; set; }

        [DataMember]
        public string Fax { get; set; }

        [DataMember]
        public string Telefono { get; set; }

        [DataMember]
        public string MonedaLocal { get; set; }

        [DataMember]
        public string MonedaSistema { get; set; }

        public string Logo
        {
            set
            {
                FileStream stream = new FileStream(_FilePath, FileMode.OpenOrCreate, FileAccess.Read);
                BinaryReader reader = new BinaryReader(stream);
                _Logo = reader.ReadBytes((int)stream.Length);
                reader.Close();
                stream.Close();
            }
        }

        public byte[] LogoBinario
        {
            get
            {
                return _Logo;
            }
        }

        [DataMember]
        private byte[] _Logo;

        [DataMember]
        public string _FilePath;
    }
}
