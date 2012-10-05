﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessPrincipal;
using SIA.Libreria;
using DataAccessFinanzas;

namespace Logica
{
    public class LogicaNegocio
    {

        public static LogicaNegocio Instancia
        {
            get
            {
                if (_Instancia == null)
                {
                    lock (_Lock)
                    {
                        if (_Instancia == null)
                        {
                            _Instancia = new LogicaNegocio();
                        }
                    }
                    
                }
                return _Instancia;
            }
        }

        public string ObtenerEmpresas() 
        {
            var daPrincipal = new DAPrincipal();
            return daPrincipal.ObtenerEmpresas();
        }

        public string ObtenerMonedas(string pBaseDatos)
        {
            var daFinanzas = new DAFinanzas(pBaseDatos);
            return daFinanzas.ObtenerMonedas();
        }

        public bool AutenticarUsuario(Usuario pUsuario, string pNombreEmpresa)
        {
            var daPrincipal = new DAPrincipal();
            var nombreBD=daPrincipal.AutenticarUsuario(pUsuario, pNombreEmpresa);
            if (nombreBD.Length>2){
                _DataAccess = new DAFinanzas(nombreBD);
                return true;
            }
            return false;
        }

        public bool InsertarNuevoUsuario(Usuario pUsuario)
        {
            var daPrincipal = new DAPrincipal();
            return daPrincipal.InsertarNuevoUsuario(pUsuario);
        }

        public bool InsertarNuevaEmpresa(Empresa pEmpresa, byte[] pLogo)
        {
            var daPrincipal = new DAPrincipal();
            var daFinanzas = new DAFinanzas(pEmpresa.Nombre);
            //Si las dos inserciones son válidas retorna true
            return ((daPrincipal.InsertarNuevaEmpresa(pEmpresa,pLogo))&&(daFinanzas.InsertarMonedas(pEmpresa)));
        }

        private static LogicaNegocio _Instancia;
        private static object _Lock=new object();

        private DAFinanzas _DataAccess;
    }
}
