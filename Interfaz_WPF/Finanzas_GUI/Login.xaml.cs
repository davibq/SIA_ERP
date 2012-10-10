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
        }
        
        /*
         * Los IF estan comentados solo para hacer mas facil cuando se esta probando
         */
        private void button1_Click(object sender, RoutedEventArgs e)
        {            

           /* if (SociedadcomboBox1.SelectedIndex == -1 || textBoxUserName.Text == string.Empty || passwordBox1.Password == string.Empty)
            {
                MessageBox.Show("Debe completar todos los datos solicitados.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {*/
                string empresa = SociedadcomboBox1.SelectedIndex.ToString();
                Usuario usuario = new Usuario()
                {
                    NombreUsuario = textBoxUserName.Text,
                    Password = passwordBox1.Password
                };
                
            //    if (ServicioFinanzas.Instancia.AutenticarUsuario(usuario, empresa))
            //    {
                    Welcome welcome = new Welcome();
                    welcome.Show();
                    NoCierre = 1;
                    Close();
            /*    }
                else 
                {
                    MessageBox.Show("Datos erroneos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }*/
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
