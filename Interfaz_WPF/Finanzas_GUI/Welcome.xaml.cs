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
using System.ComponentModel;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using SIA.Libreria;

namespace Login_WPF
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : Window
    {
        public int NoCierre = 0;

        //private ObservableCollection<Asiento> _Coleccion;

        public Welcome()
        {
            InitializeComponent();
            //_Coleccion = new ObservableCollection<Asiento>();
            //dataGridAgregaAsiento.ItemsSource = _Coleccion;
        }


        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (NoCierre == 0)
            {
                MessageBoxResult res = MessageBox.Show(this, "Si cierra la ventana, todos los Datos no guardados pueden ser perdidos.", "Confirmación", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (res == MessageBoxResult.OK)
                {
                    Application.Current.Shutdown();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string message = "Esta seguro que desea cerrar sesión?";
            string caption = "Confirmación";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
            {
                Login login = new Login();
                login.Show();
                Close();
            }
        }

        public void Reset()
        {
            textBoxCodigo.Text = "";
            textBoxNomCuenta.Text = "";
            textBoxNomExtranjero.Text = "";
            comboBoxMoneda.Text = "";
        }

        private void buttonAgregar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Se Ha Agregado La Cuenta Correctamente");
            Reset();
        }

        private void buttonGuardarAsiento_Click(object sender, RoutedEventArgs e)
        {
            string message = "Desea guardar el asiento creado?";
            string caption = "Confirmación";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
            {

            }
        }

        private void buttonAnularAsiento_Click(object sender, RoutedEventArgs e)
        {
            string message = "Esta seguro que desea anular el asiento seleccionado?";
            string caption = "Confirmación";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
            {

            }
        }

        private void buttonEliminar_Click(object sender, RoutedEventArgs e)
        {
            string message = "Esta seguro que desea eliminar la cuenta seleccionada?";
            string caption = "Confirmación";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
            {
                /*if()
                {
                    MessageBoxResult result = MessageBox.Show("Se ha Eliminado la Cuenta con Éxito");
                }
                else
                {
                    MessageBoxResult error = MessageBox.Show(this, "No se pudo eliminar la Cuenta Correctamente", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }*/
            }
        }

        private void buttonModificar_Click(object sender, RoutedEventArgs e)
        {
            Update modificar = new Update();
            modificar.Show();
            //Close();
        }

        private void checkBoxAbierto_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxCerrado.IsHitTestVisible = false;
            checkBoxAbiertoExVentas.IsHitTestVisible = false;
        }

        private void checkBoxAbierto_UnChecked(object sender, RoutedEventArgs e)
        {
            checkBoxCerrado.IsHitTestVisible = true;
            checkBoxAbiertoExVentas.IsHitTestVisible = true;
        }

        private void checkBoxCerrado_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxAbierto.IsHitTestVisible = false;
            checkBoxAbiertoExVentas.IsHitTestVisible = false;
        }

        private void checkBoxCerrado_UnChecked(object sender, RoutedEventArgs e)
        {
            checkBoxAbierto.IsHitTestVisible = true;
            checkBoxAbiertoExVentas.IsHitTestVisible = true;
        }

        private void checkBoxAbiertoExVentas_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxAbierto.IsHitTestVisible = false;
            checkBoxCerrado.IsHitTestVisible = false;
        }

        private void checkBoxAbiertoExVentas_UnChecked(object sender, RoutedEventArgs e)
        {
            checkBoxAbierto.IsHitTestVisible = true;
            checkBoxCerrado.IsHitTestVisible = true;
        }

        public void ResetMoneda()
        {
            textBoxNombreMoneda.Text = "";
            textBoxAcronimoMoneda.Text = "";
        }

        private void buttonCrearMoneda_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Se Ha Agregado La Moneda Correctamente");
            ResetMoneda();
        }

        private void buttonRealizarCambioPeriodo_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Se Ha Realizado el Cambio Correctamente");
        }

        private void buttonTipoDeCambio_Click(object sender, RoutedEventArgs e)
        {
            TipoCambio tc = new TipoCambio();
            tc.Show();
        }

        private void buttonGenerarCierre_Click(object sender, RoutedEventArgs e)
        {
            textBoxUtilidades.Text = "";
            textBoxPerdidasYGanancias.Text = "";
        }
    }
}
