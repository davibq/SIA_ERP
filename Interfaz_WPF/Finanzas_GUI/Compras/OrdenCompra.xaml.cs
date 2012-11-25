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

namespace Login_WPF.Compras
{
    /// <summary>
    /// Interaction logic for OrdenCompra.xaml
    /// </summary>
    public partial class OrdenCompra : Window
    {
        public OrdenCompra()
        {
            InitializeComponent();
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
    }
}
