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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AccesoServicio.FinanzasService;
using AccesoServicio;

namespace Login_WPF.ComponentesComunes
{
    /// <summary>
    /// Interaction logic for Detalle.xaml
    /// </summary>
    public partial class Detalle : UserControl
    {
        public Detalle()
        {
            InitializeComponent();
            Productos = new List<LineaVenta>();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                Articulos = ServicioFinanzas.Instancia.ObtenerProductosCV();
            }
        }

        #region Properties

        public List<LineaVenta> Productos
        {
            get;
            set;
        }

        public IEnumerable<ProductoCV> Articulos
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public void InicializarColumnas(bool pMostrarBodega)
        {
            gridProductos.CanUserAddRows = false;
            gridProductos.CanUserDeleteRows = false;
            gridProductos.IsReadOnly = true;
            gridProductos.Columns.Add(new DataGridTextColumn()
            {
                Header = "Articulo",
                Binding=new Binding("Producto.Descripcion"),
                MinWidth=pMostrarBodega?170:230
            });
            if (pMostrarBodega)
            {
                gridProductos.Columns.Add(new DataGridTextColumn()
                {
                    Header = "Bodega Destino",
                    Binding = new Binding("Bodega.Nombre"),
                    MinWidth = pMostrarBodega ? 163 : 230
                });
            }
            gridProductos.Columns.Add(new DataGridTextColumn()
            {
                Header = "Cantidad",
                Binding = new Binding("Cantidad"),
                MinWidth = pMostrarBodega ? 71 : 230
            });
            gridProductos.Columns.Add(new DataGridTextColumn()
            {
                Header = "Precio",
                Binding = new Binding("Producto.Precio"),
                MinWidth = pMostrarBodega ? 58 : 230
            });
            gridProductos.Columns.Add(new DataGridTextColumn()
            {
                Header = "Impuestos",
                Binding = new Binding("Impuestos"),
                MinWidth = pMostrarBodega ? 71 : 230
            });
            gridProductos.Columns.Add(new DataGridTextColumn()
            {
                Header = "Total",
                Binding = new Binding("Total"),
                MinWidth = pMostrarBodega ? 85 : 230
            });
            gridProductos.ItemsSource = Productos;
        }


        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            var agregar = new AgregarProducto(Articulos, this, true);
            if (agregar.ShowDialog().Value)
            {
                gridProductos.Items.Refresh();
                ActualizarTotal();
            }
        }

        private void btnQuitar_Click(object sender, RoutedEventArgs e)
        {
            if (gridProductos.SelectedItem != null)
            {
                Productos.Remove((LineaVenta)gridProductos.SelectedItem);
                gridProductos.Items.Refresh();
                ActualizarTotal();
            }
        }

        public void ActualizarTotal()
        {
            double subtotal = 0.0, impuestos = 0.0;
            foreach (var lineaVenta in Productos)
            {
                subtotal += lineaVenta.Cantidad * lineaVenta.Producto.Precio;
                impuestos += (lineaVenta.Cantidad * lineaVenta.Producto.Precio) * (lineaVenta.Impuestos / 100);
            }
            lblImpuestos.Content = impuestos;
            lblSubtotal.Content = subtotal;
            lblTotal.Content = impuestos + subtotal;
        }

        #endregion
                        
        #region Constantes

        #endregion
    }
}
