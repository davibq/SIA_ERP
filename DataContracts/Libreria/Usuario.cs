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
    public class Usuario
    {
        [DataMember]
        public string NombreUsuario { get; set; }
        
        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Apellido1 { get; set; }

        [DataMember]
        public string Apellido2 { get; set; }

        [DataMember]
        public string Password
        {
            set; get;
        }

        public byte[] PasswordBinario
        {
            get
            {
                return _Password;
            }
        }

        public void ConvertirPassword()
        {
            Stream memStream = new MemoryStream(Encoding.UTF8.GetBytes(Password), false);
            if ((memStream != null) && (memStream.Length > 0))
            {
                _Password = MD5.Create().ComputeHash(memStream);
            }

        }

        private byte[] _Password;
    }
}
