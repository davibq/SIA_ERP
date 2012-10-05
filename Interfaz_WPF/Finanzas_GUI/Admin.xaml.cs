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
using System.IO;
using AccesoServicio;
using AccesoServicio.FinanzasService;
using SIA.Libreria;

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
            string empresas = ServicioFinanzas.Instancia.ObtenerEmpresas();
            string[] split = empresas.Split(new Char[] { ';' });
            foreach (string s in split)
            {
                if (s.Trim() != "")
                    listBox1.Items.Add(s);
            }
            listBox1.SelectedIndex = 0;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (NoCierre == 0)
            {
                MessageBoxResult res = MessageBox.Show(this, "Si cierra la ventana, todos los Datos no guardados pueden ser perdidos.", "Confirmación", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (res == MessageBoxResult.OK)
                {
                    Login login = new Login();
                    login.Show();
                    NoCierre = 1;
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
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
            UpdateEmpress ModificarEmpresa = new UpdateEmpress();
            ModificarEmpresa.Show();
            //Close();
        }

        public void Reset()
        {
            textBoxNombre.Text = "";
            textBoxTelefono.Text = "";
            textBoxFax.Text = "";
            textBoxCedJuridica.Text = "";
            textBoxRuta.Text = "";
            textBoxNombreMonedaL.Text = "";
            textBoxNombreMonedaS.Text = "";
            textBoxAcronimoMonedaL.Text = "";
            textBoxAcronimoMonedaS.Text = "";
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxNombre.Text.Length == 0)
            {
                errormessage.Text = "Digite el nombre de la empresa";
                textBoxNombre.Focus();
            }
            
            else if (!Regex.IsMatch(textBoxTelefono.Text, "^[2-8]{1}[1-9]{2}[0-9]{5}$"))
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

            else if (textBoxCedJuridica.Text.Length == 0)
            {
                errormessage.Text = "Digite la cédula jurídica";
                textBoxCedJuridica.Focus();
            }
            
            else if (textBoxRuta.Text.Length == 0)
            {
                errormessage.Text = "Ingrese una imagen de logo";
                textBoxRuta.Focus();
            }

            else if (textBoxNombreMonedaL.Text.Length == 0)
            {
                errormessage.Text = "Ingrese un nombre para la moneda local";
                textBoxRuta.Focus();
            }

            else if (textBoxAcronimoMonedaL.Text.Length == 0)
            {
                errormessage.Text = "Ingrese un acrónimo para la moneda local";
                textBoxRuta.Focus();
            }

            else if (textBoxNombreMonedaS.Text.Length == 0)
            {
                errormessage.Text = "Ingrese un nombre para la moneda del sistema";
                textBoxRuta.Focus();
            }

            else if (textBoxAcronimoMonedaS.Text.Length == 0)
            {
                errormessage.Text = "Ingrese un acrónimo para la moneda del sistema";
                textBoxRuta.Focus();
            }

            else
            {
                Empresa empresa = new Empresa()
                {
                    Nombre = textBoxNombre.Text,
                    CedulaJuridica = textBoxCedJuridica.Text,
                    Fax = textBoxFax.Text,
                    Telefono= textBoxTelefono.Text,
                    Enabled=true
                };

                FileStream stream = new FileStream(textBoxRuta.Text, FileMode.OpenOrCreate, FileAccess.Read);
                BinaryReader reader = new BinaryReader(stream);
                byte[] Logo = reader.ReadBytes((int)stream.Length);
                reader.Close();
                stream.Close();

                if (ServicioFinanzas.Instancia.InsertarNuevaEmpresa(empresa,Logo))
                {
                    errormessage.Text = "";
                    MessageBoxResult result = MessageBox.Show("Se Ha Creado La Empresa Correctamente");
                    Reset();
                }
                else
                    errormessage.Text = "Error al insertar la empresa";
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
    }
}
