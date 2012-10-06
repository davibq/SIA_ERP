using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using SIA.ExceptionLog;
using System.Configuration;

namespace SIA.DataAccess
{
    public abstract class DataAccess
    {
        #region Constructor

        protected DataAccess(string pConnectionString)
        {
            _Conexion = new SqlConnection(pConnectionString);
        }

        #endregion

        #region Metodos

        private SqlCommand CrearComando(string pProcedureName, IEnumerable<SqlParameter> pParametros)
        {
            var comando = new SqlCommand(pProcedureName, _Conexion)
            {
                CommandType=CommandType.StoredProcedure
            };
            foreach (var parametro in pParametros)
            {
                comando.Parameters.Add(parametro);
            }
            return comando;
        }

        public DataSet EjecutarConsulta(string pProcedureName, IEnumerable<SqlParameter> pParametros)
        {
            var dataSet = new DataSet();
            try
            {
                var adapter = new SqlDataAdapter(CrearComando(pProcedureName, pParametros));
                _Conexion.Open();
                adapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExcepcion(ex, string.Empty);
                dataSet = null;
            }
            finally
            {
                if (_Conexion != null && _Conexion.State == ConnectionState.Open)
                {
                    _Conexion.Close();
                }
            }
            return dataSet;
        }

        public bool EjecutarNoConsulta(string pProcedureName, IEnumerable<SqlParameter> pParametros)
        {
            try
            {
                var comando=CrearComando(pProcedureName, pParametros);
                _Conexion.Open();
                int resultado=comando.ExecuteNonQuery();
                _Conexion.Close();
                return resultado==-1;
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExcepcion(ex, string.Empty);
                return false;
            }
            finally
            {
                if (_Conexion != null && _Conexion.State == ConnectionState.Open)
                {
                    _Conexion.Close();
                }
            }
        }
        
        #endregion

        #region Atributos

        private SqlConnection _Conexion;

        #endregion
    }
}
