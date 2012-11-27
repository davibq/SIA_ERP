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

namespace Login_WPF.Ventas
{
    /// <summary>
    /// Interaction logic for OrdenVenta.xaml
    /// </summary>
    public partial class OrdenVenta : Window
    {
        public OrdenVenta()
        {
            InitializeComponent();
        }
        private void InicializarControles()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            encabezado1.Fecha1Label = "Fecha de Contabilización";
            encabezado1.Fecha2Label = "Fecha de Entrega";
            encabezado1.SocioLabel = "Proveedor:";
            encabezado1.CargarSocios("Proveedor");
            detalle1.InicializarColumnas(true);
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InicializarControles();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
