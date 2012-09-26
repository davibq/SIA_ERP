using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SIA.DataAccess;

namespace DataAccessPrincipal
{
    public class DAPrincipal:DataAccess
    {
        // Inicializa el Connection String en el padre.
        // El connectionString lo toma de Web.config
        public DAPrincipal():base("Principal")
        {
            
        }

        //Crear md5 a partir de string
        /*
         * _PasswordBinary es un byte[]
        Stream memStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(STRING_A_CONVERTIR), false);
        if ((memStream != null) && (memStream.Length > 0)) // if the stream is valid
        {
            _PasswordBinary = MD5.Create().ComputeHash(memStream);
        }
         */
        // Suponiendo que esto retorna el nombre del usuario y el apellido
        public String AutenticarUsuario(string pNombreUsuario, byte[] pPassword)
        {
            var ds=EjecutarConsulta("dbo.AutenticarUsuario", new List<SqlParameter>()
                                                          {
                                                              new SqlParameter("paramName1", "VALOR"),
                                                              new SqlParameter("paramName1", "VALOR"),
                                                          });
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows!=null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    return row[0].ToString() + " " + row[1].ToString();
                }
            }
            return string.Empty;
        }
    }
}
