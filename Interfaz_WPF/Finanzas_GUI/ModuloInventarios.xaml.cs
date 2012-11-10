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
    /// Interaction logic for ModuloInventarios.xaml
    /// </summary>
    public partial class ModuloInventarios : Window
    {
        public ModuloInventarios()
        {
            InitializeComponent();
            
            //http://elconta.com/2012/01/23/unidad-de-medida-facturas/
            comboBoxUnidMedida.Items.Add("Metro");
            comboBoxUnidMedida.Items.Add("Kilogramo");
            comboBoxUnidMedida.Items.Add("Ampere");
            comboBoxUnidMedida.Items.Add("Kelvin");
            comboBoxUnidMedida.Items.Add("Mol");            
            comboBoxUnidMedida.Items.Add("Candela");
        }

        private void buttonCrearArticulo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxCodigo.Text) || string.IsNullOrWhiteSpace(textBoxComentarios.Text) || string.IsNullOrWhiteSpace(textBoxDescripcion.Text) || string.IsNullOrWhiteSpace(textBoxImagen.Text) || string.IsNullOrWhiteSpace(textBoxUnidMedida.Text) || comboBoxUnidMedida.SelectedIndex == -1)
            {
                MessageBox.Show("Debe completar todos los datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if(textBoxCodigo.Text.Length != 20)
                {
                    MessageBox.Show("El código debe tener 20 caracteres.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                /*
                 * Mandar a la base la info del articulo
                 */

                if (true)
                {
                    MessageBox.Show("Artículo creado exitosamente", "Nuevo artículo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Imposible crear el artículo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }                
                }
            }
        }
    }
}
