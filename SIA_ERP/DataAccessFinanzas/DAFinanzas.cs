using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIA.DataAccess;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SIA.Libreria;


namespace DataAccessFinanzas
{
    public class DAFinanzas : DataAccess
    {

        #region Constructor

        /// <summary>
        /// Instancia el connection string con el nombre de la BD que el usuario escogio.
        /// </summary>
        /// <param name="pBaseDatos">Nombre de la base de datos</param>
        public DAFinanzas(string pBaseDatos)
            : base(string.Format(ConfigurationManager.ConnectionStrings["Clientes"].ConnectionString, pBaseDatos))
        {

        }

        #endregion

        #region Metodos

        public string ObtenerMonedas()
        {
            var ds = EjecutarConsulta("dbo.ObtenerMonedas", new List<SqlParameter>());
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                string retorno = "";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    retorno += row[0] + ";";
                }
                return retorno;
            }
            return string.Empty;
        }

        public bool InsertarMonedas(Empresa pEmpresa)
        {
            const string quote = "\"";
            string monedas = "<Monedas><Moneda nombre="+quote+pEmpresa.NombreMonedaLocal+quote+" acronimo="+quote+pEmpresa.AcronimoMonedaLocal+quote+" esLocal="+quote+"1"+quote+" esSistema="+quote+"0"+quote+"/><Moneda nombre="+quote+pEmpresa.NombreMonedaSistema+quote+" acronimo="+quote+pEmpresa.AcronimoMonedaSistema+quote+" esLocal="+quote+"0"+quote+" esSistema="+quote+"1"+quote+"/></Monedas>";
            return EjecutarNoConsulta("dbo.ERPSP_InsertarMonedas", new List<SqlParameter>()
                                                          {
                                                              new SqlParameter("Monedas", monedas)
                                                          });
        }


        // No me esta sirviendo(no se que recibe seguro o esta dando algo raro) REVISAR SP
        public bool CrearCuenta(Cuenta pCuenta)
        {
            const string quote = "\"";
                              //<Nombres><Nombre nombre=      "                        "       idioma=      "      es     "       /><Nombre nombre=      "                                        "       idioma=      "      en-US     "       /></Nombres>
            string nombres = "<Nombres><Nombre nombre=" + quote + pCuenta.Nombre + quote + " idioma=" + quote + "es"+ quote + " /><Nombre nombre=" + quote + pCuenta.NombreIdiomaExtranjero + quote + " idioma=" + quote + "en-US"+ quote + " /></Nombres>";
            return EjecutarNoConsulta("dbo.ERPSP_ActualizarCuenta", new List<SqlParameter>()
                                                          {
                                                              new SqlParameter("Nombre", pCuenta.Nombre),
                                                              new SqlParameter("Codigo", pCuenta.Codigo),
                                                              new SqlParameter("Nivel", pCuenta.Nivel),
                                                              new SqlParameter("Enabled", pCuenta.Enabled),
                                                              new SqlParameter("CuentaPadre", pCuenta.CuentaPadre),
                                                              new SqlParameter("Identificador", pCuenta.Identificador),
                                                              new SqlParameter("Nombres", nombres)
                                                          });
        }

        #endregion

    }
}
