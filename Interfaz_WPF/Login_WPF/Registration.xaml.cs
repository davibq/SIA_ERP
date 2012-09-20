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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Login_WPF
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public int NoCierre = 0;

        public Registration()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            NoCierre = 1;
            Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        public void Reset()
        {
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxUserName.Text = "";
            passwordBox1.Password = "";
            passwordBoxConfirm.Password = "";
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (NoCierre == 0)
            {
                Application.Current.Shutdown();
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxFirstName.Text.Length == 0)
            {
                errormessage.Text = "Ingrese su Nombre";
                textBoxFirstName.Focus();
            }
            else if (textBoxLastName.Text.Length == 0)
            {
                errormessage.Text = "Ingrese su Apellido";
                textBoxLastName.Focus();
            }
            else if (textBoxUserName.Text.Length == 0)
            {
                errormessage.Text = "Ingrese un Nombre de Usuario";
                textBoxUserName.Focus();
            }
            else
            {
                if (passwordBox1.Password.Length == 0)
                {
                    errormessage.Text = "Ingrese su password";
                    passwordBox1.Focus();
                }
                else if (passwordBoxConfirm.Password.Length == 0)
                {
                    errormessage.Text = "Confirme su password";
                    passwordBoxConfirm.Focus();
                }
                else if (passwordBox1.Password != passwordBoxConfirm.Password)
                {
                    errormessage.Text = "Su password de confirmación es diferente";
                    passwordBoxConfirm.Focus();
                }
                else
                {
                    errormessage.Text = "";
                    MessageBoxResult result = MessageBox.Show("Se Ha Registrado Correctamente"); 
                    Reset();
                    Login login = new Login();
                    login.Show();
                    NoCierre = 1;
                    Close();
                }
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (NoCierre == 0)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
