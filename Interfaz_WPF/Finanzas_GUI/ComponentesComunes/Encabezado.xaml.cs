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
using AccesoServicio;
using AccesoServicio.FinanzasService;

namespace Login_WPF.ComponentesComunes
{
    /// <summary>
    /// Interaction logic for Encabezado.xaml
    /// </summary>
    public partial class Encabezado : UserControl
    {
        public Encabezado()
        {
            InitializeComponent();
            fecha1.SelectedDate = DateTime.Now.Date;
        }


        #region Propiedades

        public string Fecha1Label
        {
            set
            {
                lblFecha1.Content = value;
            }
        }

        public string Fecha2Label
        {
            set
            {
                lblFecha2.Content = value;
            }
        }

        public string SocioLabel
        {
            set
            {
                lblSocio.Content = value;
            }
        }

        public SocNegocio SocioNegocio
        {
            get
            {
                return (SocNegocio)cmbSocio.SelectedItem;
            }
            set
            {
                cmbSocio.SelectedItem = value;
            }
        }

        #endregion

        #region Metodos

        public void CargarSocios(string pTipo)
        {
            cmbSocio.ItemsSource = ServicioFinanzas.Instancia.ObtenerSociosNegocioCV(pTipo);
        }

        #endregion
    }
    
}
