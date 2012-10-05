using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SIA.DataAccess;
using SIA.Libreria;
using System.Configuration;

namespace DataAccessPrincipal
{
    public class DAPrincipal:DataAccess
    {
        // Inicializa el Connection String en el padre.
        // El connectionString lo toma de Web.config
        public DAPrincipal():base(ConfigurationManager.ConnectionStrings["Principal"].ConnectionString)
        {
            
        }

        public string ObtenerEmpresas()
        {
            var ds = EjecutarConsulta("dbo.ObtenerEntidades", new List<SqlParameter>());
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                string retorno = "";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    retorno += row[0] +";"; 
                }
                return retorno;
            }
            return string.Empty;
        }

        public string AutenticarUsuario(Usuario pUsuario, string pEmpresa)
        {
            var ds=EjecutarConsulta("dbo.LoginUsuario", new List<SqlParameter>()
                                                          {
                                                              new SqlParameter("pNombreUsuario", pUsuario.NombreUsuario),
                                                              new SqlParameter("pPassword", pUsuario.PasswordBinario),
                                                              new SqlParameter("pNombreEmpresa", pEmpresa)
                                                          });
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows!=null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    return row[0].ToString();
                }
            }
            return string.Empty;
        }


        //HAY QUE HACERLE CAMBIO AL SP PARA PROBARLO Y QUE FUNCIONE
        public bool InsertarNuevoUsuario(Usuario pUsuario)
        {
            return EjecutarNoConsulta("dbo.ERPSP_ActualizarUsuario", new List<SqlParameter>()
                                                          {
                                                              new SqlParameter("Login", pUsuario.NombreUsuario),
                                                              new SqlParameter("Pass", pUsuario.PasswordBinario),
                                                              new SqlParameter("Enabled", true)
                                                          });
        }

        //ME RETORNA -1??????????????????????
        public bool InsertarNuevaEmpresa(Empresa pEmpresa,byte[] pLogo)
        {
            const string quote = "\"";
            string contactos = "<Contactos><Contacto enabled=" + quote + "1" + quote + " nombre=" + quote + "Telefono" + quote + " valor=" + quote + pEmpresa.Telefono + quote + " /><Contacto enabled=" + quote + "1" + quote + " nombre=" + quote + "Fax" + quote + " valor=" + quote + pEmpresa.Fax + quote + " /></Contactos>";
            return EjecutarNoConsulta("dbo.ERPSP_ActualizarEntidad", new List<SqlParameter>()
                                                          {
                                                              new SqlParameter("Nombre", pEmpresa.Nombre),
                                                              new SqlParameter("Contactos", contactos),
                                                              new SqlParameter("Logo", pLogo),
                                                              new SqlParameter("CedulaJuridica", pEmpresa.CedulaJuridica),
                                                              new SqlParameter("Enabled", true)
                                                          });
        }
    }
}
