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

        public string ObtenerMonedas(string pBaseDatos)
        {
            return _CSC.ObtenerMonedas(pBaseDatos);
        }

        public bool AutenticarUsuario(Usuario pUsuario, string pNombreEmpresa)
        {
            return _CSC.AutenticarUsuario(pUsuario, pNombreEmpresa);
        }

        public bool InsertarNuevoUsuario(Usuario pUsuario)
        {
            return _CSC.InsertarNuevoUsuario(pUsuario);
        }

        public bool InsertarNuevaEmpresa(Empresa pEmpresa, byte[] pLogo)
        {
            return _CSC.InsertarNuevaEmpresa(pEmpresa,pLogo);
        }

        public bool CrearCuenta(Cuenta pCuenta, string pNombreEmpresa)
        {
            return _CSC.CrearCuenta(pCuenta, pNombreEmpresa);
        }

        private ContabilidadServiceClient _CSC;

        private static ServicioFinanzas _Instancia;
        private static object _Lock=new object();
    }
}
