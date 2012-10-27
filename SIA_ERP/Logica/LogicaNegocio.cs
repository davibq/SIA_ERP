﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessPrincipal;
using SIA.Libreria;
using DataAccessFinanzas;
using SIA.Contabilidad.Libreria;

namespace Logica
{
    public class LogicaNegocio
    {
        #region Propiedades

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

        #endregion

        #region Metodos

        public string ObtenerEmpresas()
        {
            var daPrincipal = new DAPrincipal();
            return daPrincipal.ObtenerEmpresas();
        }

        public IEnumerable<Moneda> ObtenerMonedas()
        {
            return _DataAccess.ObtenerMonedas();
        }

        // falta crear la BD antes de hacer esto
        // no esta guardando el PasswordBinario en la clase Usuario
        public bool AutenticarUsuario(Usuario pUsuario, string pNombreEmpresa)
        {
            var daPrincipal = new DAPrincipal();
            pUsuario.ConvertirPassword();
            _DataAccess = new DAFinanzas("LCO_Finanzas");
            var nombreBD = daPrincipal.AutenticarUsuario(pUsuario, pNombreEmpresa);
            if (nombreBD.Length > 2)
            {
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
            _DataAccess = new DAFinanzas(pEmpresa.Nombre);
            bool retorno = _DataAccess.InsertarMonedasConfiguracion(pEmpresa);
            if (retorno)
                retorno = daPrincipal.InsertarNuevaEmpresa(pEmpresa, pLogo);
            return retorno;
        }

        public bool InsertarNuevaMoneda(Moneda pMoneda)
        {
            return _DataAccess.InsertarMonedas(pMoneda);
        }

        public bool CrearCuenta(Cuenta pCuenta)
        {
            return _DataAccess.CrearCuenta(pCuenta);
        }

        public bool GuardarPeriodoContable(Mes[] pArregloMeses)
        {
            return _DataAccess.GuardarPeriodoContable(pArregloMeses);
        }

        public IEnumerable<Cuenta> DemeCuentasHijas()
        {
            return _DataAccess.ObtenerCuentas();
            /*var todasCuentas = _DataAccess.ObtenerCuentas();
            var cuentasHijas = todasCuentas;
            foreach (var cuenta in todasCuentas)
            {
                var cuentasAQuitar = new List<Cuenta>();
                foreach (var cta in todasCuentas)
                {
                    if (cta.Codigo == cuenta.CodigoCuentaPadre)
                    {
                        cuentasAQuitar.Add(cta);
                    }
                }
                cuentasHijas = cuentasHijas.Except(cuentasAQuitar);
            }
            return cuentasHijas;*/
        }

        public IEnumerable<Moneda> DemeMonedasCuenta(string pCuenta)
        {
            return _DataAccess.DemeMonedas(pCuenta);
        }

        public bool AgregarAsiento(string Fecha, double MontoDebe, double MontoHaber, string pXML)
        {
            return _DataAccess.AgregarAsiento(Fecha, MontoDebe, MontoHaber, pXML);
        }

        public Moneda ObtenerMonedasSistema(string pAtributo)
        {
            return _DataAccess.ObtenerMonedasSistema(pAtributo);
        }

#endregion

        #region Atributos

        private static LogicaNegocio _Instancia;
        private static object _Lock = new object();

        private DAFinanzas _DataAccess;

        #endregion
    }
}
