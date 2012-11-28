using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AccesoServicio;
using AccesoServicio.FinanzasService;

namespace Login_WPF.Ventas
{
    /// <summary>
    /// Interaction logic for FacturaServicios.xaml
    /// </summary>
    public partial class FacturaServicios : Window
    {
        public FacturaServicios()
        {
            InitializeComponent();
        }

        private void cmdAtras_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void InicializarControles()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            encabezado1.Fecha1Label = "Fecha de Contabilización";
            encabezado1.Fecha2Label = "Fecha de Factura";
            encabezado1.SocioLabel = "Cliente:";
            encabezado1.CargarSocios("Cliente");
        }

        private void cmdGuardar_Click(object sender, RoutedEventArgs e)
        {
            double precio, iv;
            if (!double.TryParse(txtTotal.Text, out precio))
            {
                MessageBox.Show("Digite un precio válido");
                return;
            }
            if (!double.TryParse(textBox1.Text, out iv))
            {
                MessageBox.Show("Digite un IV válido");
                return;
            }
            var documento = new AccesoServicio.FinanzasService.Documento();
            documento.Consecutivo = encabezado1.txtConsecutivo.Text;
            documento.CreadoDesdeAnterior = false;
            documento.TipoDocumento = "Factura de Clientes";
            documento.Fecha1 = encabezado1.fecha1.SelectedDate.Value;
            documento.Fecha2 = encabezado1.fecha2.SelectedDate.Value;
            documento.Subtotal = precio;
            documento.Total = precio+(precio*(iv/100));
            documento.EsServicio = true;
            documento.DescripcionServicio = txtDetalle.Text;
            documento.CodigoCuentaServicio = txtCuentaAsociada.Text;
            documento.LineasVenta = null;
            documento.SocioNegocio = (SocNegocio)encabezado1.cmbSocio.SelectedItem;
            if (ServicioFinanzas.Instancia.GuardarDocumento(documento))
            {
                MessageBox.Show("Factura de clientes guardada");
                encabezado1.txtConsecutivo.Text = string.Empty;
                txtDetalle.Text = string.Empty; 
                txtCuentaAsociada.Text=string.Empty;
                encabezado1.cmbSocio.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Se produjo un error");
            }
        }

        private void txtTotal_LostFocus(object sender, RoutedEventArgs e)
        {
            double precio, iv;
            if (!double.TryParse(txtTotal.Text, out precio))
            {
                return;
            }
            if (!double.TryParse(textBox1.Text, out iv))
            {
                return;
            }
            lblTotal.Content = precio + (precio * (iv / 100));
        }

        private void textBox1_LostFocus(object sender, RoutedEventArgs e)
        {
            double precio, iv;
            if (!double.TryParse(txtTotal.Text, out precio))
            {
                return;
            }
            if (!double.TryParse(textBox1.Text, out iv))
            {
                return;
            }
            lblTotal.Content = precio + (precio * (iv / 100));
        }
    }
}
