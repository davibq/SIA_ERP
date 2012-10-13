using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIA.DataAccess;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SIA.Libreria;
using SIA.TipoCambio;

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

        public IEnumerable<Moneda> ObtenerMonedas()
        {
            var monedas = new List<Moneda>();
            var ds = EjecutarConsulta("dbo.ObtenerMonedas", new List<SqlParameter>());
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var moneda = new Moneda();
                    moneda.Nombre = row["Nombre"].ToString();// .CodigoCuentaPadre = row["IdCuentaPadre"].ToString();
                    moneda.Acronimo = row["Acronimo"].ToString();
                    moneda.TipoMoneda = (MonedasValidas)(int.Parse(row["idBCCR"].ToString()));
                    monedas.Add(moneda);
                }
            }
            return monedas;
        }

        public bool InsertarMonedasConfiguracion(Empresa pEmpresa)
        {
            const string quote = "\"";
            string monedas = "<Monedas><Moneda nombre="+quote+pEmpresa.NombreMonedaLocal+quote+" acronimo="+quote+pEmpresa.AcronimoMonedaLocal+quote+" esLocal="+quote+"1"+quote+" esSistema="+quote+"0"+quote+"/><Moneda nombre="+quote+pEmpresa.NombreMonedaSistema+quote+" acronimo="+quote+pEmpresa.AcronimoMonedaSistema+quote+" esLocal="+quote+"0"+quote+" esSistema="+quote+"1"+quote+"/></Monedas>";
            return EjecutarNoConsulta("dbo.ERPSP_InsertarMonedas", new List<SqlParameter>()
                                                          {
                                                              new SqlParameter("Monedas", monedas)
                                                          });
        }

        public bool InsertarMonedas(Moneda pMoneda)
        {
            const string quote = "\"";
            string monedas = "<Monedas><Moneda nombre=" + quote + pMoneda.Nombre + quote + " acronimo=" + quote + pMoneda.Acronimo + quote + " esLocal=" + quote + "0" + quote + " esSistema=" + quote + "0" + quote + " idBCCR=" + quote + (int)pMoneda.TipoMoneda + quote + "/></Monedas>";
            return EjecutarNoConsulta("dbo.ERPSP_InsertarMonedas", new List<SqlParameter>()
                                                          {
                                                              new SqlParameter("Monedas", monedas)
                                                          });
        }

        //Falta insertar las monedas asociadas de alguna manera
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
                                                              new SqlParameter("CuentaPadre", pCuenta.CodigoCuentaPadre),
                                                              new SqlParameter("Identificador", pCuenta.Identificador),
                                                              new SqlParameter("Nombres", nombres)
                                                          });
        }

        public bool GuardarPeriodoContable(Mes[] pMeses)
        {
            string xml = "<Meses>";
            foreach (var mes in pMeses)
            {
                xml += string.Format("<Mes Nombre=\"{0}\" FechaInicio=\"{1}\" FechaFin=\"{2}\" Estado=\"{3}\"/>",
                    mes.NombreMes, mes.FechaInicio, mes.FechaFin, mes.EstadoMes);
            }
            xml += "</Meses>";
            return EjecutarNoConsulta("dbo.InsertarPeriodoContable", new List<SqlParameter>()
            {
                new SqlParameter("ListaMeses", xml)
            });
        }

        public IEnumerable<Cuenta> ObtenerCuentas()
        {
            var cuentas = new List<Cuenta>();
            var ds = EjecutarConsulta("dbo.ERPSP_ObtenerCatalogoCuentas", new List<SqlParameter>()
            {
                //new SqlParameter("Entidad", ":D:D:D:D")
            });
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var cuenta = new Cuenta();
                    cuenta.CodigoCuentaPadre = row["IdCuentaPadre"].ToString();
                    cuenta.Nombre = row["NombreCuenta"].ToString();
                    cuenta.Codigo = row["CodigoCuenta"].ToString();
                    cuenta.Nivel = int.Parse(row["NivelCuenta"].ToString());
                    cuenta.Identificador = row["NombreIdentificador"].ToString();
                    /*cuenta.Saldo = double.Parse(row["SaldoCuenta"].ToString());
                    cuenta.Moneda = new Moneda()
                    {
                        Nombre = row["NombreMoneda"].ToString(),
                        Acronimo = row["AcronimoMoneda"].ToString(),
                        Tipo = row["MonedaAplica"].ToString()
                    };*/
                    cuentas.Add(cuenta);
                }
            }
            return cuentas;
        }

        public IEnumerable<Moneda> DemeMonedas(string pNombreCuenta)
        {
            var ds = EjecutarConsulta("dbo.ObtenterMonedasCuenta", new List<SqlParameter>()
            {
                new SqlParameter("pNombreCuenta", pNombreCuenta)
            });
            var monedas = new List<Moneda>();
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var moneda = new Moneda();
                    moneda.Nombre = row[0].ToString();
                    moneda.TipoMoneda = (MonedasValidas)(int.Parse(row[1].ToString()));
                    monedas.Add(moneda);
                }
            }
            return monedas;
        }

        public bool AgregarAsiento(string Fecha, double MontoDebe, double MontoHaber, string pXML)
        {
            return EjecutarNoConsulta("dbo.ERPSP_ActualizarAsiento", new List<SqlParameter>()
                                                          {
                                                              new SqlParameter("IdAsiento", -1),
                                                              new SqlParameter("Fecha", Fecha),
                                                              new SqlParameter("MontoDebe", MontoDebe),
                                                              new SqlParameter("MontoHaber", MontoHaber),
                                                              new SqlParameter("Referencia1", "-"),
                                                              new SqlParameter("Referencia2", "-"),
                                                              new SqlParameter("Enabled", true),
                                                              new SqlParameter("Cuenta", pXML),
                                                              new SqlParameter("TipoAsiento", "AS")
                                                          });
        }

        #endregion

    }
}
