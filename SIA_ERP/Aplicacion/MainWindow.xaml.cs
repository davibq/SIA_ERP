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
using SIA.ExceptionLog;
using SIA.TipoCambio;

namespace Aplicacion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                int j = 2;
                int i = 3 / (j-2);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExcepcion(ex, "Division entre 0");
            }
            if (TiposCambio.Instancia.SonValoresValidos)
            {

            }
        }
    }
}
