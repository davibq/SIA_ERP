﻿using System;
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

        #endregion

    }
}
