using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessPrincipal;
using SIA.Libreria;
using DataAccessFinanzas;
using SIA.Contabilidad.Libreria;
using SIA.TipoCambio;

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

        public string NombreBase { get; set; }

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
                NombreBase = nombreBD;
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

        public bool CrearCuenta(Cuenta pCuenta, string pXml)
        {
            return _DataAccess.CrearCuenta(pCuenta, pXml);
        }

        public bool GuardarPeriodoContable(string pFechaIn, string pFechaFin, int pAno, Mes[] pArregloMeses)
        {
            return _DataAccess.GuardarPeriodoContable(pFechaIn,pFechaFin,pAno,pArregloMeses);
        }

        public IEnumerable<Cuenta> DemeCuentasHijas()
        {
            return _DataAccess.ObtenerCuentas();
        }

        public IEnumerable<Moneda> DemeMonedasCuenta(string pCuenta)
        {
            return _DataAccess.DemeMonedas(pCuenta);
        }

        public bool AgregarAsiento(string Fecha, double MontoDebe, double MontoHaber, string pXML, string pTipoAsiento)
        {
            return _DataAccess.AgregarAsiento(Fecha, MontoDebe, MontoHaber, pXML, pTipoAsiento);
        }

        public Moneda ObtenerMonedasSistema(string pAtributo)
        {
            return _DataAccess.ObtenerMonedasSistema(pAtributo);
        }

        public double ConvertirAMonedaSistema(MonedasValidas pMoneda, double pValor)
        {
            var moneda = _DataAccess.ObtenerMonedasSistema("Sistema");
            return (pMoneda == moneda.TipoMoneda) ? pValor : TiposCambio.Instancia.DemeCambio(pMoneda, pValor, moneda.TipoMoneda);
        }

        public double ConvertirAMonedaLocal(Moneda pMoneda, double pValor)
        {
            var moneda = _DataAccess.ObtenerMonedasSistema("Local");
            return (pMoneda.TipoMoneda == moneda.TipoMoneda) ? pValor : TiposCambio.Instancia.DemeCambio(pMoneda.TipoMoneda, pValor, moneda.TipoMoneda);
        }

        public IEnumerable<Cuenta> ObtenerCuentasHijasSegunPadre(string pNombrePadre)
        {
            return _DataAccess.ObtenerCuentasHijasSegunPadre(pNombrePadre);
        }

        public IEnumerable<Cuenta> ObtenerCuentasHojas(string pNombrePadre)
        {
            return _DataAccess.ObtenerCuentasHojas(pNombrePadre);
        }

        public IEnumerable<Cuenta> ObtenerCuentasCierreCompras()
        {
            var cuentas = _DataAccess.ObtenerCuentasHijasSegunPadre("GASTOS DE COMPRAS");
            if (_DataAccess.ObtenerCuenta("Inventario final").Codigo!=null) cuentas.Add(_DataAccess.ObtenerCuenta("Inventario final"));
            if (_DataAccess.ObtenerCuenta("Inventario inicial").Codigo != null) cuentas.Add(_DataAccess.ObtenerCuenta("Inventario inicial"));

            foreach (var cuenta in cuentas)
            {
                if (cuenta.Nombre == "Fletes sobre compras" || cuenta.Nombre == "Compras" || cuenta.Nombre == "Inventario inicial")
                {
                    cuenta.Saldo_Haber = cuenta.Saldo;
                    cuenta.Saldo = 0;
                }
            }

            return cuentas;
        }

        public IEnumerable<Cuenta> ObtenerCuentasCierreIngresos()
        {
            IEnumerable<Cuenta> cuentas = _DataAccess.ObtenerCuentasHijasSegunPadre("INGRESOS");
            cuentas = cuentas.Concat(_DataAccess.ObtenerCuentasHijasSegunPadre("OTROS INGRESOS"));

            foreach (var cuenta in cuentas)
            {
                if (cuenta.Nombre == "Descuentos sobre ventas" || cuenta.Nombre == "Devoluciones sobre ventas")
                {
                    cuenta.Saldo_Haber = cuenta.Saldo;
                    cuenta.Saldo = 0;
                }
            }

            return cuentas;
        }

        public IEnumerable<Cuenta> ObtenerCuentasCierreGastos()
        {
            IEnumerable<Cuenta> cuentas = _DataAccess.ObtenerCuentasHijasSegunPadre("GASTOS");
            cuentas = cuentas.Concat(_DataAccess.ObtenerCuentasHijasSegunPadre("OTROS GASTOS"));
            cuentas = cuentas.Concat(_DataAccess.ObtenerCuentasHijasSegunPadre("COSTOS"));

            return cuentas;
        }

        public Cuenta ObtenerCuenta(string pNombreCuenta) 
        {
            return _DataAccess.ObtenerCuenta(pNombreCuenta);
        }

        public IEnumerable<Cuenta> ObtenerCuentasTreeView()
        {
            return _DataAccess.ObtenerCuentasTreeView();
        }

        public IEnumerable<Cuenta> ObtenerCuentasDeMayorSN()
        {
            return _DataAccess.ObtenerCuentasDeMayorSN();
        }

        public int ObtenerIdMoneda(string moneda)
        {
            return _DataAccess.ObtenerIdMoneda(moneda);
        }

        public string ObtenerNombreCuentaDeMayorSN(string CodigoSN)
        {
            return _DataAccess.ObtenerNombreCuentaDeMayorSN(CodigoSN);
        }

        public string ObtenerSaldoCuenta(string CodigoCuentaSN, int IdMoneda)
        {
            return _DataAccess.ObtenerSaldoCuenta(CodigoCuentaSN, IdMoneda);
        }


        #endregion

        #region Metodos ERPApp

        public ConsultaSaldo consultarCreditoSaldo(string pCodigoCliente)
        {
            return _DataAccess.consultarCreditoSaldo(pCodigoCliente);
        }

        #endregion

        #region Atributos

        private static LogicaNegocio _Instancia;
        private static object _Lock = new object();

        private DAFinanzas _DataAccess;

        #endregion
    }
}
