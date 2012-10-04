using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AccesoServicio.FinanzasService;
using SIA.Libreria;

namespace AccesoServicio
{
    public class ServicioFinanzas
    {
        private ServicioFinanzas()
        {
            _CSC = new ContabilidadServiceClient();
        }

        ~ServicioFinanzas(){
            _CSC.Close();
        }

        public static ServicioFinanzas Instancia
        {
            get
            {
                if (_Instancia == null)
                {
                    lock (_Lock)
                    {
                        if (_Instancia == null)
                        {
                            _Instancia = new ServicioFinanzas();
                        }
                    }
                }
                return _Instancia;
            }
        }

        public String Saludar()
        {
            return _CSC.Saludar();
        }

        public string ObtenerEmpresas()
        {
            return _CSC.ObtenerEmpresas();
        }

        public bool AutenticarUsuario(Usuario pUsuario, string pNombreEmpresa)
        {
            return _CSC.AutenticarUsuario(pUsuario, pNombreEmpresa);
        }

        public bool InsertarNuevoUsuario(Usuario pUsuario, string pNombreEmpresa, string pPassword)
        {
            return _CSC.InsertarNuevoUsuario(pUsuario,pNombreEmpresa,pPassword);
        }

        public bool CrearCuenta(Cuenta pCuenta)
        {
            return _CSC.CrearCuenta(pCuenta);
        }

        private ContabilidadServiceClient _CSC;

        private static ServicioFinanzas _Instancia;
        private static object _Lock=new object();
    }
}
