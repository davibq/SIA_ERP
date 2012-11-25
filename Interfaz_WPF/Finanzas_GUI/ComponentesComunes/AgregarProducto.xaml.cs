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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AccesoServicio.FinanzasService;

namespace Login_WPF.ComponentesComunes
{
    /// <summary>
    /// Interaction logic for AgregarProducto.xaml
    /// </summary>
    public partial class AgregarProducto : Window
    {
        public AgregarProducto(IEnumerable<ProductoCV> pProductos, Detalle pPadre, bool pBodega)
        {
            InitializeComponent();
            lstProductos.ItemsSource = pProductos;
            _Padre = pPadre;
            _MostrarBodega = pBodega;
            if (!pBodega){
                cmbBodegas.Visibility = Visibility.Hidden;
                lblBodega.Visibility = Visibility.Hidden;
            }
        }

        private void cmdAceptar_Click(object sender, RoutedEventArgs e)
        {
            double precio, impuestos;
            int cantidad;
            if (lblProducto.Content == null)
            {
                MessageBox.Show("Escoja un producto");
                return;
            }
            if (!int.TryParse(txtCantidad.Text, out cantidad))
            {
                MessageBox.Show("Digite una cantidad válida");
                return;
            }
            if (!double.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("Digite un precio válido");
                return;
            }
            if (!double.TryParse(txtImpuestos.Text, out impuestos))
            {
                MessageBox.Show("Digite un impuesto válido");
                return;
            }
            if (_MostrarBodega && cmbBodegas.SelectedItem == null)
            {
                MessageBox.Show("Escoja una bodega");
                return;
            }
            BodegaCV bodega=_MostrarBodega?(BodegaCV)cmbBodegas.SelectedItem:null;
            ProductoCV producto=(ProductoCV)lblProducto.Content;
            producto.Precio=precio;
            _Padre.Productos.Add(new LineaVenta()
            {
                Cantidad=cantidad,
                Impuestos=impuestos,
                Producto=producto,
                Bodega=bodega,
                Total=(cantidad*producto.Precio)+((impuestos/100)*producto.Precio*cantidad)
            });
            DialogResult = true;
            Close();
        }

        private void lstProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblProducto.Content = lstProductos.SelectedItem;
            cmbBodegas.ItemsSource = ((ProductoCV)lstProductos.SelectedItem).Bodegas;
        }

        private void cmdCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private Detalle _Padre;
        private bool _MostrarBodega;

    }
}
