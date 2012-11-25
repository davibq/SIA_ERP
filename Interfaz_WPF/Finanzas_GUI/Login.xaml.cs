using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using AccesoServicio;
using AccesoServicio.FinanzasService;

namespace Login_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class Login : Window
    {
        public int NoCierre = 0;        

        public Login()
        {
            InitializeComponent();
            string empresas = ServicioFinanzas.Instancia.ObtenerEmpresas();
            string[] split = empresas.Split(new Char[] {';'});
            foreach (string s in split)
            {
                if (s.Trim() != "")
                    SociedadcomboBox1.Items.Add(s);    
            }
            SociedadcomboBox1.SelectedIndex = 0;

            comboBoxModulos.Items.Add("Finanzas");
            comboBoxModulos.Items.Add("Inventarios");
            comboBoxModulos.Items.Add("Socios de negocio");
            comboBoxModulos.Items.Add("Bancos");
            comboBoxModulos.Items.Add("Compras");
            comboBoxModulos.Items.Add("Ventas");
            comboBoxModulos.Items.Add("Administrativo");
            comboBoxModulos.SelectedIndex = 0;
        }
        
        /*
         * Los IF estan comentados solo para hacer mas facil cuando se esta probando
         */
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (SociedadcomboBox1.SelectedIndex == -1 || textBoxUserName.Text == string.Empty || passwordBox1.Password == string.Empty || comboBoxModulos.SelectedIndex == -1)
            {
                MessageBox.Show("Debe completar todos los datos solicitados.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                string empresa = SociedadcomboBox1.SelectedItem.ToString();
                var usuario = new Usuario()
                {
                    NombreUsuario = textBoxUserName.Text,
                    Password = passwordBox1.Password
                };

                if (ServicioFinanzas.Instancia.AutenticarUsuario(usuario, empresa))
                {
                    int modulo = comboBoxModulos.SelectedIndex;

                    switch (modulo)
                    {
                        case 0:
                            Welcome welcome = new Welcome();
                            welcome.Show();
                            NoCierre = 1;
                            Close();
                            break;

                        case 1:
                            ModuloInventarios inventarios = new ModuloInventarios();
                            NoCierre = 1;
                            inventarios.Show();
                            Close();
                            break;

                        case 2:
                            ModuloSociosNegocios socios = new ModuloSociosNegocios();
                            NoCierre = 1;
                            Close();
                            socios.Show();
                            break;

                        case 3:
                            ModuloBancos bancos = new ModuloBancos();
                            NoCierre = 1;
                            Close();
                            break;

                        case 4:
                            ModuloCompras compras = new ModuloCompras();
                            NoCierre = 1;
                            compras.Show();
                            Close();
                            break;

                        case 5:
                            ModuloVentas ventas = new ModuloVentas();
                            NoCierre = 1;
                            ventas.Show();
                            Close();
                            break;

                        case 6:
                            ModuloAdministrativo administrativo = new ModuloAdministrativo();
                            Close();
                            administrativo.Show();
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Datos erroneos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            NoCierre = 1;
            Close();
        }

        private void buttonRegistrarEmpresas_Click(object sender, RoutedEventArgs e)
        {
            Admin empresa = new Admin();
            empresa.Show();
            NoCierre = 1;
            Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (NoCierre == 0)
            {
                MessageBoxResult result = MessageBox.Show("Desea cerrar la aplicación?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}