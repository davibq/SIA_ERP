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
using System.Windows.Shapes;
using AccesoServicio;
using AccesoServicio.FinanzasService;

namespace Login_WPF
{
    /// <summary>
    /// Interaction logic for ModuloVentas.xaml
    /// </summary>
    public partial class ModuloVentas : Window
    {
        public ModuloVentas()
        {
            InitializeComponent();
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAgregarFactura_Click(object sender, RoutedEventArgs e)
        {
            var factura = new Ventas.FacturaClientes();
            factura.InicializarControles();
            factura.DesdeDocumentoPasado = false;
            factura.ShowDialog();
        }

        private void btnAgregarEntradaMercancias_Click(object sender, RoutedEventArgs e)
        {
            var entradaM = new Ventas.EntregaMercancias();
            entradaM.DesdeDocumentoPasado = false;
            entradaM.InicializarControles();
            entradaM.ShowDialog();
        }

        private void btnAgregarOrdenCompra_Click(object sender, RoutedEventArgs e)
        {
            var ordenCompra = new Ventas.OrdenVenta();
            ordenCompra.InicializarControles();
            ordenCompra.ShowDialog();
        }

        private void btnNuevaF_Click(object sender, RoutedEventArgs e)
        {
            var documento = (Documento)gridDocumentos.SelectedItem;
            var doc = ServicioFinanzas.Instancia.ObtenerDocumento(documento.IdDocumento);
            var factura = new Ventas.FacturaClientes();
            factura.DesdeDocumentoPasado = true;
            factura.detalle1.Productos = doc.LineasVenta.ToList();
            factura.InicializarControles();
            foreach (var item in factura.encabezado1.cmbSocio.Items)
            {
                if (((SocNegocio)item).Nombre.CompareTo(doc.SocioNegocio.Nombre) == 0)
                {
                    factura.encabezado1.cmbSocio.SelectedItem = item;
                }
            }
            factura.detalle1.ActualizarTotal();
            factura.ShowDialog();
        }

        private void btnNuevaEM_Click(object sender, RoutedEventArgs e)
        {
            var documento = (Documento)gridDocumentos.SelectedItem;
            var doc = ServicioFinanzas.Instancia.ObtenerDocumento(documento.IdDocumento);
            var entradaM = new Ventas.EntregaMercancias();
            entradaM.DesdeDocumentoPasado = true;
            entradaM.detalle1.Productos = doc.LineasVenta.ToList();
            entradaM.InicializarControles();
            foreach (var item in entradaM.encabezado1.cmbSocio.Items)
            {
                if (((SocNegocio)item).Nombre.CompareTo(doc.SocioNegocio.Nombre) == 0)
                {
                    entradaM.encabezado1.cmbSocio.SelectedItem = item;
                }
            }
            entradaM.detalle1.ActualizarTotal();
            entradaM.ShowDialog();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var documento = (Documento)gridDocumentos.SelectedItem;
            var doc = ServicioFinanzas.Instancia.ObtenerDocumento(documento.IdDocumento);
            var factura = new Ventas.FacturaClientes();
            factura.DesdeDocumentoPasado = true;
            factura.encabezado1.label1.Content = doc.TipoDocumento;
            factura.encabezado1.txtConsecutivo.Text = doc.Consecutivo;
            factura.detalle1.Productos = doc.LineasVenta.ToList();
            factura.InicializarControles();
            foreach (var item in factura.encabezado1.cmbSocio.Items)
            {
                if (((SocNegocio)item).Nombre.CompareTo(doc.SocioNegocio.Nombre) == 0)
                {
                    factura.encabezado1.cmbSocio.SelectedItem = item;
                }
            }
            factura.detalle1.btnAgregar.IsEnabled = false;
            factura.detalle1.ActualizarTotal();
            factura.ShowDialog();
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            gridDocumentos.ItemsSource = ServicioFinanzas.Instancia.ObtenerDocumentosVentas();
            gridDocumentos.Items.Refresh();
        }

        private void gridDocumentos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridDocumentos.SelectedItem == null)
            {
                btnNuevaEM.IsEnabled = false;
                btnNuevaF.IsEnabled = false;
                return;
            }
            var documento = (Documento)gridDocumentos.SelectedItem;
            if (documento.TipoDocumento.CompareTo("Orden de Venta") == 0)
            {
                btnNuevaEM.IsEnabled = true;
                btnNuevaF.IsEnabled = true;
            }
            else if (documento.TipoDocumento.CompareTo("Entrega de Mercancias") == 0)
            {
                btnNuevaEM.IsEnabled = false;
                btnNuevaF.IsEnabled = true;
            }
            else
            {
                btnNuevaEM.IsEnabled = false;
                btnNuevaF.IsEnabled = false;
            }
        }

        private void cmdNuevaFacturaServicios_Click(object sender, RoutedEventArgs e)
        {
            var fservicios = new Ventas.FacturaServicios();
            fservicios.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gridDocumentos.ItemsSource = ServicioFinanzas.Instancia.ObtenerDocumentosVentas();
            gridDocumentos.Items.Refresh();
        }
    }
}
