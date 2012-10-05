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

        public bool InsertarNuevoUsuario(Usuario pUsuario)
        {
            return EjecutarNoConsulta("dbo.ERPSP_ActualizarUsuario", new List<SqlParameter>()
                                                          {
                                                              new SqlParameter("Login", pUsuario.NombreUsuario),
                                                              new SqlParameter("Pass", pUsuario.PasswordBinario),
                                                              new SqlParameter("Enabled", true)
                                                          });
        }
    }
}
