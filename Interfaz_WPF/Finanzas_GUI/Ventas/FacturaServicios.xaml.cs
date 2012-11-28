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
    }
}
