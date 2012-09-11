using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;

namespace SIA.ExceptionLog
{
    public class ExceptionLogger
    {
        static ExceptionLogger()
        {
            XmlConfigurator.Configure();
        }

        public static void LogExcepcion(Exception pException, string pMensaje)
        {
            log.Error(pMensaje, pException);
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(ExceptionLogger));
    }
}
