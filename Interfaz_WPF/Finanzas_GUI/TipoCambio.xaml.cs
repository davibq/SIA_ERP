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

namespace Login_WPF
{
    /// <summary>
    /// Interaction logic for TipoCambio.xaml
    /// </summary>
    public partial class TipoCambio : Window
    {
        public int NoCierre = 0;

        public TipoCambio()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (NoCierre == 0)
            {
                MessageBoxResult res = MessageBox.Show(this, "Si cierra la ventana, El Tipo de Cambio no guardado puede ser perdido.", "Confirmación", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (res == MessageBoxResult.OK)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        public void ResetTipoDeCambio()
        {
            comboBoxMlocal.Text = "";
            comboBoxMSistema.Text = "";
            textBoxMontoTipoDeCambio1.Text = "";
            textBoxMontoTipoDeCambio2.Text = "";
        }

        private void buttonModificarTipoDeCambio_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(textBoxMontoTipoDeCambio1.Text, "^[1-9]{1}[0-9]*[.]{1}[0-9]*$"))
            {
                errormessage2.Text = "";
                errormessage1.Text = "Digite un número válido";
                textBoxMontoTipoDeCambio1.Select(0, textBoxMontoTipoDeCambio1.Text.Length);
                textBoxMontoTipoDeCambio1.Focus();
            }
            else if (!Regex.IsMatch(textBoxMontoTipoDeCambio2.Text, "^[1-9]{1}[0-9]*[.]{1}[0-9]*$"))
            {
                errormessage1.Text = "";
                errormessage2.Text = "Digite un número válido";
                textBoxMontoTipoDeCambio2.Select(0, textBoxMontoTipoDeCambio2.Text.Length);
                textBoxMontoTipoDeCambio2.Focus();
            }
            else if (comboBoxMlocal.Text.Length == 0)
            {
                errormessage2.Text = "";
                errormessage1.Text = "Ingrese la primera moneda";
                comboBoxMlocal.Focus();
            }
            else if (comboBoxMSistema.Text.Length == 0)
            {
                errormessage1.Text = "";
                errormessage2.Text = "Ingrese la segunda moneda";
                comboBoxMSistema.Focus();
            }
            else
            {
                errormessage1.Text = "";
                errormessage2.Text = "";
                MessageBoxResult result = MessageBox.Show("Se Ha Modificado El Tipo De Cambio Correctamente");
                ResetTipoDeCambio();
            }
        }
    }
}
