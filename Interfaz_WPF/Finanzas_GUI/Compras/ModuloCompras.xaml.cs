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

namespace Login_WPF
{
    /// <summary>
    /// Interaction logic for ModuloCompras.xaml
    /// </summary>
    public partial class ModuloCompras : Window
    {
        public ModuloCompras()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                gridDocumentos.ItemsSource = ServicioFinanzas.Instancia.ObtenerDocumentosCompras();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void btnAgregarOrdenCompra_Click(object sender, RoutedEventArgs e)
        {
            var ordenCompra = new Compras.OrdenCompra();
            ordenCompra.ShowDialog();
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            gridDocumentos.ItemsSource = ServicioFinanzas.Instancia.ObtenerDocumentosCompras();
            gridDocumentos.Items.Refresh();
        }

        private void gridDocumentos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridDocumentos.SelectedItem == null){
                btnNuevaEM.IsEnabled = false;
                btnNuevaF.IsEnabled = false;
                return;   
            }
            var documento = (Documento)gridDocumentos.SelectedItem;
            if (documento.TipoDocumento.CompareTo("Orden de Compra") == 0)
            {
                btnNuevaEM.IsEnabled = true;
                btnNuevaF.IsEnabled = true;
            }
            else if (documento.TipoDocumento.CompareTo("Entrada de Mercancias") == 0)
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

        private void btnNuevaEM_Click(object sender, RoutedEventArgs e)
        {
            var documento = (Documento)gridDocumentos.SelectedItem;
            var doc = ServicioFinanzas.Instancia.ObtenerDocumento(documento.IdDocumento);
            var entradaM = new Compras.EntradaMercancias();
            entradaM.detalle1.Productos = doc.LineasVenta.ToList();
            entradaM.InicializarControles();
            foreach (var item in entradaM.encabezado1.cmbSocio.Items)
            {
                if (((SocNegocio)item).Nombre.CompareTo(doc.SocioNegocio.Nombre)==0){
                    entradaM.encabezado1.cmbSocio.SelectedItem=item;
                }
            }
            entradaM.ShowDialog();
        }

        private void btnAgregarEntradaMercancias_Click(object sender, RoutedEventArgs e)
        {
            var entradaM= new Compras.EntradaMercancias();
            entradaM.ShowDialog();
        }
    }
}
