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

        private ObservableCollection<Documento> _Documentos;

        public ModuloSociosDeNegocios()
        {
            InitializeComponent();

            _Documentos = new ObservableCollection<Documento>();

            comboBoxTipoSN.Items.Add("Cliente");
            comboBoxTipoSN.Items.Add("Proveedor");
            comboBoxTipoSN.SelectedIndex = 0;

            var monedas = ServicioFinanzas.Instancia.ObtenerMonedas();
            comboBoxMonedaSN.ItemsSource = monedas;
            comboBoxMonedaSN.SelectedIndex = 0;

            var cuentas_mayor = ServicioFinanzas.Instancia.ObtenerCuentasDeMayorSN();
            foreach (var item in cuentas_mayor)
            {
                comboBoxCuentaMayorSN.Items.Add(item.Codigo + " " + item.Nombre);
            }
            comboBoxCuentaMayorSN.SelectedIndex = 0;

            comboBoxConsultaTipoSN.Items.Add("Cliente");
            comboBoxConsultaTipoSN.Items.Add("Proveedor");
            comboBoxConsultaTipoSN.SelectedIndex = 0;

            comboBoxConsultaDocTipoSN.Items.Add("Cliente");
            comboBoxConsultaDocTipoSN.Items.Add("Proveedor");
            comboBoxConsultaDocTipoSN.SelectedIndex = 0;
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
            textBoxEmailSN.Text = "";
            comboBoxTipoSN.SelectedIndex = 0;
            comboBoxMonedaSN.SelectedIndex = 0;
            comboBoxCuentaMayorSN.SelectedIndex = 0;
            textBoxLimiteCredito.Text = "";
        }

        private void MostrarLimiteDeCreditoClientes()
        {
            string TipoSocio = comboBoxTipoSN.SelectedItem.ToString();
            if (TipoSocio == "Cliente")
            {
                textBlockLimiteCredito.Visibility = Visibility.Visible;
                textBoxLimiteCredito.Visibility = Visibility.Visible;
            }
            else
            {
                textBlockLimiteCredito.Visibility = Visibility.Hidden;
                textBoxLimiteCredito.Visibility = Visibility.Hidden;
            }
        }

        private void comboBoxTipoSN_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MostrarLimiteDeCreditoClientes();
        }

        private void buttonCrearSN_Click(object sender, RoutedEventArgs e)
        {
            errormessageCodigo.Text = "";
            errormessageNombre.Text = "";
            errormessageEmail.Text = "";
            errormessageLimiteCredito.Text = "";

            if (textBoxCodigoSN.Text.Length == 0)
            {
                errormessageCodigo.Text = "Debe ingresar un Código";
                textBoxCodigoSN.Focus();
            }
            else if (textBoxNombreSN.Text.Length == 0)
            {
                errormessageNombre.Text = "Debe ingresar un Nombre";
                textBoxNombreSN.Focus();
            }
            else if (!Regex.IsMatch(textBoxCodigoSN.Text, "^[_A-Za-z0-9-]{4,15}$"))
            {
                errormessageCodigo.Text = "Debe ingresar un Código Válido";
                textBoxCodigoSN.Focus();
            }
            else if (!Regex.IsMatch(textBoxNombreSN.Text, "^[A-Z,Á,É,Í,Ó,Ú,Ñ,a-z,ñ,á,é,í,ó,ú,0-9-, ,_,.]{1,40}$"))
            {
                errormessageNombre.Text = "El nombre excede el tamaño permitido";
                textBoxNombreSN.Focus();
            }
            else if ((textBoxEmailSN.Text.Length != 0) && (!Regex.IsMatch(textBoxEmailSN.Text, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*")))
            {
                errormessageEmail.Text = "Debe ingresar un E-Mail Váido";
                textBoxEmailSN.Focus();
            }
            else if ((textBoxLimiteCredito.Text.Length != 0) && !Regex.IsMatch(textBoxLimiteCredito.Text, "^[0-9]*\\.?[0-9]{0,2}$"))
            {
                errormessageLimiteCredito.Text = "Debe ingresar un Monto Válido";
                textBoxLimiteCredito.Select(0, textBoxLimiteCredito.Text.Length);
                textBoxLimiteCredito.Focus();
            }
            else
            {
                errormessageCodigo.Text = "";
                errormessageNombre.Text = "";
                errormessageEmail.Text = "";
                errormessageLimiteCredito.Text = "";
                string Nombre = textBoxNombreSN.Text;
                string Codigo = textBoxCodigoSN.Text;
                string Email = textBoxEmailSN.Text;
                string TipoSocio = comboBoxTipoSN.SelectedItem.ToString();
                string Moneda = comboBoxMonedaSN.SelectedItem.ToString();
                string CuentaAsociada = comboBoxCuentaMayorSN.SelectedItem.ToString();
                CuentaAsociada = CuentaAsociada.Substring(0, 9);
                int IdMoneda = ServicioFinanzas.Instancia.ObtenerIdMoneda(Moneda);
                double LimiteCredito = 0;
                if (textBoxLimiteCredito.Text.Length != 0)
                {
                    LimiteCredito = double.Parse(textBoxLimiteCredito.Text);
                }

                if (ServicioFinanzas.Instancia.CrearSocioDeNegocio(Nombre, Codigo, Email, TipoSocio, IdMoneda, CuentaAsociada, LimiteCredito))
                {
                    MessageBoxResult result = MessageBox.Show("Se Ha Agregado el Socio De Negocio Correctamente");
                    Reset();
                    LlenarComboBoxConsultaSocioNegocio();
                    LlenarComboBoxConsultaDocSN();
                }
                else
                    MessageBox.Show("Error al intentar crear el Socio De Negocio");
            }
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
            string cuenta_socio = ServicioFinanzas.Instancia.ObtenerCuentaDeMayorXCodigo(CodigoSN);
            int IdMoneda = ServicioFinanzas.Instancia.ObtenerIDMonedaCuentaDeMayorXCodigo(CodigoSN);
            string nombre_cuenta_socio = ServicioFinanzas.Instancia.ObtenerNombreCuentaDeMayorSN(cuenta_socio);
            string Codigo_Cuenta_SN = cuenta_socio + " " + nombre_cuenta_socio;
            var saldo = ServicioFinanzas.Instancia.consultarCreditoSaldo(CodigoSN);
            
            //string saldo = ServicioFinanzas.Instancia.ObtenerSaldoCuenta(cuenta_socio, IdMoneda);                       
            textBoxConsultaCuentaDeMayorSN.Text = Codigo_Cuenta_SN;
            textBoxConsultaSaldoSN.Text = saldo.Saldo.ToString();
        }

        private void comboBoxConsultaDocTipoSN_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LlenarComboBoxConsultaDocSN();
        }

        private void LlenarComboBoxConsultaDocSN()
        {
            comboBoxConsultaDocSN.Items.Clear();
            string TipoSocio = comboBoxConsultaDocTipoSN.SelectedItem.ToString();
            var obtener_socios = ServicioFinanzas.Instancia.ObtenerSociosNegocioCV(TipoSocio);
            foreach (var item in obtener_socios)
            {
                comboBoxConsultaDocSN.Items.Add(item.Codigo + " - " + item.Nombre);
            }
            comboBoxConsultaDocSN.SelectedIndex = 0;
        }

        private void buttonConsultarDocumentos_Click(object sender, RoutedEventArgs e)
        {
            _Documentos = new ObservableCollection<Documento>();
            string SN = comboBoxConsultaDocSN.SelectedItem.ToString();
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
            var documentos = ServicioFinanzas.Instancia.ObtenerFacturasXEstadoXSocioNegocio(CodigoSN, "Pendiente");
            foreach (var doc in documentos)
            {
                var documento = new Documento();
                documento.IdDocumento = doc.IdDocumento;
                documento.Consecutivo = doc.Consecutivo;
                documento.Fecha1 = doc.Fecha1;
                documento.Subtotal = doc.Subtotal;
                documento.Total = doc.Total;
                _Documentos.Add(documento);
            }

            dataGridDocumentos.ItemsSource = _Documentos;
            dataGridDocumentos.Items.Refresh();
        }

    }
}
