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

namespace Login_WPF
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public int NoCierre = 0;

        public Admin()
        {
            InitializeComponent();
        }


        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (NoCierre == 0)
            {
                MessageBoxResult result = MessageBox.Show("Desea cerrar la aplicación?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
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
                NoCierre = 1;
                Close();
            }
        }

        private void buttonAgregar_Click(object sender, RoutedEventArgs e)
        {
            string message = "Desea agregar esta nueva cuenta?";
            string caption = "Confirmación";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
            {

            }
        }

        private void buttonModificar_Click(object sender, RoutedEventArgs e)
        {
            string message = "Esta seguro que desea modificar la cuenta seleccionada?";
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

            }
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
    }
}
