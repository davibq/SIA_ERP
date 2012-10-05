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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Login_WPF
{
    /// <summary>
    /// Interaction logic for UpdateEmpress.xaml
    /// </summary>
    public partial class UpdateEmpress : Window
    {
        public int NoCierre = 0;

        public UpdateEmpress()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (NoCierre == 0)
            {
                MessageBoxResult res = MessageBox.Show(this, "Si cierra la ventana, todos los Datos no guardados pueden ser perdidos.", "Confirmación", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (res == MessageBoxResult.OK)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
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
                textBoxRuta.Text = filename;
            }
        }

        private void buttonRealizarCambios_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(textBoxTelefono.Text, "^[2-8]{1}[1-9]{2}[0-9]{5}$"))
            {
                errormessage.Text = "Digite un número telefónico válido";
                textBoxTelefono.Select(0, textBoxTelefono.Text.Length);
                textBoxTelefono.Focus();
            }

            else if (!Regex.IsMatch(textBoxFax.Text, "^[2-7]{1}[1-9]{2}[0-9]{5}$"))
            {
                errormessage.Text = "Digite un número de fax válido";
                textBoxFax.Select(0, textBoxFax.Text.Length);
                textBoxFax.Focus();
            }

            else
            {
                errormessage.Text = "";
                string message = "Esta seguro que desea modificar la Cuenta?";
                string caption = "Confirmación";
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;
                if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
                {
                    NoCierre = 1;
                    Close();
                }
            }
        }
    }
}
