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
            set
            {
                _PasswordStr = value;
                Stream memStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(value), false);
                if ((memStream != null) && (memStream.Length > 0))
                {
                    _Password = MD5.Create().ComputeHash(memStream);
                }
            }
            get
            {
                return _PasswordStr;
            }
        }

        public byte[] PasswordBinario
        {
            get
            {
                return _Password;
            }
        }

        [DataMember]
        private byte[] _Password;
        [DataMember]
        private string _PasswordStr;
    }
}
