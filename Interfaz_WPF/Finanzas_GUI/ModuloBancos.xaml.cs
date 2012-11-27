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
    /// Interaction logic for ModuloBancos.xaml
    /// </summary>
    public partial class ModuloBancos : Window
    {
        public int NoCierre = 0;

        public ModuloBancos()
        {
            InitializeComponent();

            var proveedores = ServicioFinanzas.Instancia.ObtenerSociosNegocioCV("Proveedor");
            comboBoxProveedores.ItemsSource = proveedores;

            var bancos = ServicioFinanzas.Instancia.obtenerBancos();
            comboBoxBancos.ItemsSource = bancos;
        }

        private void comboBoxProveedores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var facturas = ServicioFinanzas.Instancia.ObtenerFacturasXEstadoXSocioNegocio(((SocNegocio)comboBoxProveedores.SelectedItem).Codigo, "Pendiente");
            listBoxFacturas.ItemsSource = facturas;
            listBoxFacturas.Items.Refresh();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxFacturas.SelectedItems.Count == 0 || comboBoxBancos.SelectedIndex == -1)
            {
                MessageBox.Show("Debe completar todos los datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var facturasList = listBoxFacturas.SelectedItems;
            }
        }
    }
}
