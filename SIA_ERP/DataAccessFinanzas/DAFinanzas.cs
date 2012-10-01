using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIA.DataAccess;
using System.Configuration;

namespace DataAccessFinanzas
{
    public class DAFinanzas : DataAccess
    {
        public DAFinanzas(string pBaseDatos)
            : base(string.Format(ConfigurationManager.ConnectionStrings["Clientes"].ConnectionString, pBaseDatos))
        {
            
        }
    }
}
