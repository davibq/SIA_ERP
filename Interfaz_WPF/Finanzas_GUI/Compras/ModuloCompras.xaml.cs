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
    }
}
