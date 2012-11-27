using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using AccesoServicio;
using AccesoServicio.FinanzasService;

namespace Login_WPF
{
    /// <summary>
    /// Interaction logic for ModuloSociosNegocios.xaml
    /// </summary>
    public partial class ModuloSociosDeNegocios : Window
    {
        public int NoCierre = 0;

        public ModuloSociosDeNegocios()
        {
            InitializeComponent();

            comboBoxTipoSN.Items.Add("Cliente");
            comboBoxTipoSN.Items.Add("Proveedor");
            comboBoxTipoSN.SelectedIndex = 0;

            var monedas = ServicioFinanzas.Instancia.ObtenerMonedas();
            comboBoxMonedaSN.ItemsSource = monedas;
            comboBoxMonedaSN.SelectedIndex = 0;

            //var cuentas_mayor = ServicioFinanzas.Instancia.ObtenerCuentasDeMayorSN();
            /*foreach (var item in cuentas_mayor)
            {
                comboBoxCuentaMayorSN.Items.Add(item.Codigo + " " + item.Nombre);
            }
            comboBoxCuentaMayorSN.SelectedIndex = 0;*/

            comboBoxConsultaTipoSN.Items.Add("Cliente");
            comboBoxConsultaTipoSN.Items.Add("Proveedor");
            comboBoxConsultaTipoSN.SelectedIndex = 0;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (NoCierre == 0)
            {
                MessageBoxResult res = MessageBox.Show(this, "Si cierra la ventana, todos los Datos no guardados pueden ser perdidos.", "Confirmación", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (res == MessageBoxResult.OK)
                {
                    Application.Current.Shutdown();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            string message = "Esta seguro que desea cerrar sesión?";
            string caption = "Confirmación";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
            {
                Login login = new Login();
                login.Show();
                NoCierre = 1;
                Close();
            }
        }
        
        public void Reset()
        {
            textBoxNombreSN.Text = "";
            textBoxCodigoSN.Text = "";
            comboBoxTipoSN.SelectedIndex = 0;
            comboBoxMonedaSN.SelectedIndex = 0;
            comboBoxCuentaMayorSN.SelectedIndex = 0;
        }

        private void buttonCrearSN_Click(object sender, RoutedEventArgs e)
        {
            string Nombre = textBoxNombreSN.Text;
            string Codigo = textBoxCodigoSN.Text;
            string TipoSocio = comboBoxTipoSN.SelectedItem.ToString();
            string Moneda = comboBoxMonedaSN.SelectedItem.ToString();
            string CuentaAsociada = comboBoxCuentaMayorSN.SelectedItem.ToString();
            CuentaAsociada = CuentaAsociada.Substring(0, 9);
            //int IdMoneda = ServicioFinanzas.Instancia.ObtenerIdMoneda(Moneda);
          
            /*if (ServicioFinanzas.Instancia.CrearSocioDeNegocio(Nombre, Codigo, TipoSocio, IdMoneda, CuentaAsociada))
            {
                MessageBoxResult result = MessageBox.Show("Se Ha Agregado el Socio De Negocio Correctamente");
                Reset();
                LlenarComboBoxConsultaSocioNegocio();
            }
            else
                MessageBox.Show("Error al intentar crear el Socio De Negocio");*/
        }

        private void comboBoxConsultaTipoSN_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LlenarComboBoxConsultaSocioNegocio();
        }

        private void LlenarComboBoxConsultaSocioNegocio()
        {
            comboBoxConsultaSocioNegocio.Items.Clear();
            string TipoSocio = comboBoxConsultaTipoSN.SelectedItem.ToString();
            var obtener_socios = ServicioFinanzas.Instancia.ObtenerSociosNegocioCV(TipoSocio);
            foreach (var item in obtener_socios)
            {
                comboBoxConsultaSocioNegocio.Items.Add(item.Codigo + " - " + item.Nombre);
            }
            comboBoxConsultaSocioNegocio.SelectedIndex = 0; 
        }

        private void buttonConsultarSaldo_Click(object sender, RoutedEventArgs e)
        {
            string SN = comboBoxConsultaSocioNegocio.SelectedItem.ToString();
            char[] SN_Letras = SN.ToCharArray();
            string CodigoSN = "";
            for (int i = 0; i < SN.Length; i++)
            {
                if (SN_Letras[i] != ' ')
                {
                    CodigoSN = CodigoSN + SN_Letras[i];
                }
                else
                {
                    break;
                }
            }
            //string cuenta_socio = ServicioFinanzas.Instancia.ObtenerCuentaDeMayorXCodigo(CodigoSN);
            //int IdMoneda = ServicioFinanzas.Instancia.ObtenerIDMonedaCuentaDeMayorXCodigo(CodigoSN);
            //string nombre_cuenta_socio = ServicioFinanzas.Instancia.ObtenerNombreCuentaDeMayorSN(cuenta_socio);
            //string Codigo_Cuenta_SN = cuenta_socio + " " + nombre_cuenta_socio; 
            //string saldo = ServicioFinanzas.Instancia.ObtenerSaldoCuenta(cuenta_socio, IdMoneda);                       
            //textBoxConsultaCuentaDeMayorSN.Text = Codigo_Cuenta_SN;
            //textBoxConsultaSaldoSN.Text = saldo;
        }

    }
}
