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
using System.IO;
using AccesoServicio;
using AccesoServicio.FinanzasService;

namespace Login_WPF
{
    /// <summary>
    /// Interaction logic for ModuloInventarios.xaml
    /// </summary>
    public partial class ModuloInventarios : Window
    {
        public int NoCierre = 0;

        public ModuloInventarios()
        {
            InitializeComponent();

            var unidades = ServicioFinanzas.Instancia.ObtenerUnidadesdeMedida();
            comboBoxUnidMedida.ItemsSource = unidades;

            var bodegas = ServicioFinanzas.Instancia.obtenerBodegas();
            comboBoxBodega.ItemsSource = bodegas;
            comboBoxBodegas.ItemsSource = bodegas;
            dataGridBodegas.ItemsSource = bodegas;

            var articulos = ServicioFinanzas.Instancia.obtenerTodosArticulos();
            comboBoxArticulo.ItemsSource = articulos;

            var cuentasInventarios = ServicioFinanzas.Instancia.obtenerCuentasInventario();
            comboBoxCuentasExistencias.ItemsSource = cuentasInventarios;

            var cuentasVentas = ServicioFinanzas.Instancia.obtenerCuentasVentas();
            comboBoxCuentasVentas.ItemsSource = cuentasVentas;

            var cuentasCostos = ServicioFinanzas.Instancia.obtenerCuentasCostos();
            comboBoxCuentasCostos.ItemsSource = cuentasCostos;

            //http://elconta.com/2012/01/23/unidad-de-medida-facturas/
            /*comboBoxUnidMedida.Items.Add("Metro");
            comboBoxUnidMedida.Items.Add("Kilogramo");
            comboBoxUnidMedida.Items.Add("Ampere");
            comboBoxUnidMedida.Items.Add("Kelvin");
            comboBoxUnidMedida.Items.Add("Mol");            
            comboBoxUnidMedida.Items.Add("Candela");*/
        }

        private void buttonCrearArticulo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxCodigo.Text) || string.IsNullOrWhiteSpace(textBoxComentarios.Text) || string.IsNullOrWhiteSpace(textBoxDescripcion.Text) || string.IsNullOrWhiteSpace(textBoxImagen.Text) || comboBoxUnidMedida.SelectedIndex == -1 || comboBoxCuentasCostos.SelectedIndex == -1 || comboBoxCuentasVentas.SelectedIndex == -1 || comboBoxCuentasExistencias.SelectedIndex == -1 || comboBoxBodega.SelectedIndex == -1 ||  textBoxNombreArticulo.Text.Length==0) 
            {
                MessageBox.Show("Debe completar todos los datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if(textBoxCodigo.Text.Length > 20)
                {
                    MessageBox.Show("El código no debe tener más de 20 caracteres.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string urlImagen1 = textBoxImagen.Text;

                    Articulo articulo = new Articulo()
                    {
                        Nombre=textBoxNombreArticulo.Text,
                        Codigo = textBoxCodigo.Text,
                        Descripcion = textBoxDescripcion.Text,
                        unidadMedida = (UnidadMedida)comboBoxUnidMedida.SelectedItem,
                        Comentarios = textBoxComentarios.Text,
                        UrlImagen = urlImagen1,
                        Existencias = (Cuenta)comboBoxCuentasExistencias.SelectedItem,
                        Ventas=(Cuenta)comboBoxCuentasVentas.SelectedItem,
                        Costos=(Cuenta)comboBoxCuentasCostos.SelectedItem,
                        bodega=(Bodega)comboBoxBodega.SelectedItem
                    };

                    if (ServicioFinanzas.Instancia.crearArticulo(articulo))
                    {
                        MessageBox.Show("Artículo creado exitosamente", "Nuevo artículo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Imposible crear el artículo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    var articulos = ServicioFinanzas.Instancia.obtenerTodosArticulos();
                    comboBoxArticulo.ItemsSource = articulos;
                    comboBoxArticulo.Items.Refresh();
                }
            }
        }

        private void buttonBuscar_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Pictures (*.jpg; *.bmp; *.png)|*.jpg;*.bmp;*.png";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open picture
                string filename = dlg.FileName;
                textBoxImagen.Text = filename;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void buttonCrearBodega_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxCodigoBodega.Text) || string.IsNullOrWhiteSpace(textBoxNombreBodega.Text))
            {
                MessageBox.Show("Debe completar todos los datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Bodega bodega = new Bodega()
                {
                    Codigo = textBoxCodigoBodega.Text,
                    Nombre = textBoxNombreBodega.Text
                };

                if (ServicioFinanzas.Instancia.crearBodega(bodega))
                {
                    MessageBox.Show("Bodega creada exitosamente", "Nueva bodega", MessageBoxButton.OK, MessageBoxImage.Information);
                    textBoxCodigoBodega.Text = "";
                    textBoxNombreBodega.Text = "";

                    var bodegas = ServicioFinanzas.Instancia.obtenerBodegas();
                    dataGridBodegas.ItemsSource = bodegas;
                    dataGridBodegas.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Imposible crear la bodega", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
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

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxArticulo.SelectedIndex != -1 && comboBoxBodegas.SelectedIndex != -1)
            {
                var artXbod = ServicioFinanzas.Instancia.obtenerArticuloXBodeba(((Articulo)comboBoxArticulo.SelectedItem).Codigo, ((Bodega)comboBoxBodegas.SelectedItem).Codigo);
                dataGridArticulosXBodegas.ItemsSource = artXbod;
                dataGridArticulosXBodegas.Items.Refresh();
            }
        }
    }
}
