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
using AccesoServicio;
using AccesoServicio.FinanzasService;

namespace Login_WPF
{
    /// <summary>
    /// Interaction logic for ModuloBancos.xaml
    /// </summary>
    public partial class ModuloBancos : Window
    {
        public int NoCierre = 0;

        public ModuloBancos()
        {
            InitializeComponent();

            var proveedores = ServicioFinanzas.Instancia.ObtenerSociosNegocioCV("Proveedor");
            comboBoxProveedores.ItemsSource = proveedores;

            var bancos = ServicioFinanzas.Instancia.obtenerBancos();
            comboBoxBancos.ItemsSource = bancos;

            var clientes = ServicioFinanzas.Instancia.ObtenerSociosNegocioCV("Cliente");
            comboBoxClientes.ItemsSource = clientes;

            comboBoxCuenta.ItemsSource = bancos;
        }

        private void comboBoxProveedores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var facturas = ServicioFinanzas.Instancia.ObtenerFacturasXEstadoXSocioNegocio(((SocNegocio)comboBoxProveedores.SelectedItem).Codigo, "Pendiente");
            listBoxFacturas.ItemsSource = facturas;
            listBoxFacturas.Items.Refresh();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void button2_Click(object sender, RoutedEventArgs e)
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxFacturas.SelectedItems.Count == 0 || comboBoxBancos.SelectedIndex == -1 || textBoxNumTransferencia.Text.Length==0)
            {
                MessageBox.Show("Debe completar todos los datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                List<Documento> facturasList = new List<Documento>();
                for (int i = 0; i < listBoxFacturas.SelectedItems.Count; i++)
                {
                    Documento fact = (Documento)listBoxFacturas.SelectedItems[i];
                    facturasList.Add(fact);
                }
                
                //var facturasList = (List<Documento>)listBoxFacturas.SelectedItems;
                var bancoDep = (Banco)comboBoxBancos.SelectedItem;
                var proveedor = (SocNegocio)comboBoxProveedores.SelectedItem;

                double factor = Math.Pow(10, 2);
                string message = "Desea resgitrar el pago?";
                string caption = "Confirmación";
                bool completo = true;
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Question;
                if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
                {
                    var monedaSistema = ServicioFinanzas.Instancia.ObtenerMonedasSistema("Sistema");
                    var monedaLocal = ServicioFinanzas.Instancia.ObtenerMonedasSistema("Local");
                    string xml = "<Cuentas>";
                    double montoDebe = 0, montoHaber = 0;
                    foreach (var factura in facturasList)
                    {
                        /*      <Cuentas>
                            --		<Cuenta monto="10000.00" moneda="CRC" cuenta="" debe="0"/>
                            --		<Cuenta monto="5490.98" moneda="CRC" cuenta="" debe="1"/>
                            --  </Cuentas>*/
                        xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                            factura.Total, monedaSistema.Acronimo, factura.SocioNegocio.Codigo, "1");
                        montoDebe += factura.Total;

                        xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                            factura.Total, monedaSistema.Acronimo, bancoDep.CuentaMayor, "0");
                        montoHaber += factura.Total;

                        xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                            factura.Total, monedaLocal.Acronimo, factura.SocioNegocio.Codigo, "1");
                        
                        xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                            factura.Total, monedaLocal.Acronimo, bancoDep.CuentaMayor, "0");

                        xml += "</Cuentas>";

                        if (ServicioFinanzas.Instancia.InsertarAsiento(DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString(), montoDebe, montoHaber, xml, "PR"))
                        {
                            if (!(ServicioFinanzas.Instancia.setearFacturas(factura.IdDocumento, "Cancelado")))
                                completo = false;

                            Transferencia transferencia = new Transferencia()
                            {
                                TipoTransferencia = "Efectuado",
                                Socio = proveedor,
                                banco = bancoDep,
                                Monto = factura.Total,
                                Fecha = DateTime.Now,
                                NumTranseferencia = textBoxNumTransferencia.Text
                            };

                            if (!(ServicioFinanzas.Instancia.insertarTransferencia(transferencia)))
                                completo = false;
                        }
                        else
                            completo = false;

                        xml = "<Cuentas>";
                        montoDebe = 0;
                        montoHaber = 0;
                    }
                    if (completo)
                        MessageBox.Show("Pagos registrados!", "SIA", MessageBoxButton.OK, MessageBoxImage.Information);
                    textBoxNumTransferencia.Text = "";
                    comboBoxBancos.SelectedIndex = -1;
                    var facturas = ServicioFinanzas.Instancia.ObtenerFacturasXEstadoXSocioNegocio(((SocNegocio)comboBoxProveedores.SelectedItem).Codigo, "Pendiente");
                    listBoxFacturas.ItemsSource = facturas;
                    listBoxFacturas.Items.Refresh();
                }
            }
        }

        private void comboBoxClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var facturas = ServicioFinanzas.Instancia.ObtenerFacturasXEstadoXSocioNegocio(((SocNegocio)comboBoxClientes.SelectedItem).Codigo, "Pendiente");
            listBoxFactClientes.ItemsSource = facturas;
            listBoxFactClientes.Items.Refresh();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxFactClientes.SelectedItems.Count == 0 || comboBoxCuenta.SelectedIndex == -1 || textBoxNumTransaccion.Text.Length == 0)
            {
                MessageBox.Show("Debe completar todos los datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                List<Documento> facturasList = new List<Documento>();
                for (int i = 0; i < listBoxFactClientes.SelectedItems.Count; i++)
                {
                    Documento fact = (Documento)listBoxFactClientes.SelectedItems[i];
                    facturasList.Add(fact);
                }

                var bancoDep = (Banco)comboBoxCuenta.SelectedItem;
                var cliente = (SocNegocio)comboBoxClientes.SelectedItem;

                double factor = Math.Pow(10, 2);
                string message = "Desea resgitrar el pago?";
                string caption = "Confirmación";
                bool completo = true;
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Question;
                if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
                {
                    var monedaSistema = ServicioFinanzas.Instancia.ObtenerMonedasSistema("Sistema");
                    var monedaLocal = ServicioFinanzas.Instancia.ObtenerMonedasSistema("Local");
                    string xml = "<Cuentas>";
                    double montoDebe = 0, montoHaber = 0;
                    foreach (var factura in facturasList)
                    {
                        /*      <Cuentas>
                            --		<Cuenta monto="10000.00" moneda="CRC" cuenta="" debe="0"/>
                            --		<Cuenta monto="5490.98" moneda="CRC" cuenta="" debe="1"/>
                            --  </Cuentas>*/
                        xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                            factura.Total, monedaSistema.Acronimo, factura.SocioNegocio.Codigo, "0");
                        montoDebe += factura.Total;

                        xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                            factura.Total, monedaSistema.Acronimo, bancoDep.CuentaMayor, "1");
                        montoHaber += factura.Total;

                        xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                            factura.Total, monedaLocal.Acronimo, factura.SocioNegocio.Codigo, "0");

                        xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                            factura.Total, monedaLocal.Acronimo, bancoDep.CuentaMayor, "1");

                        xml += "</Cuentas>";

                        if (ServicioFinanzas.Instancia.InsertarAsiento(DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString(), montoDebe, montoHaber, xml, "PR"))
                        {
                            if (!(ServicioFinanzas.Instancia.setearFacturas(factura.IdDocumento, "Cancelado")))
                                completo = false;

                            Transferencia transferencia = new Transferencia()
                            {
                                TipoTransferencia = "Recibido",
                                Socio = cliente,
                                banco = bancoDep,
                                Monto = factura.Total,
                                Fecha = DateTime.Now,
                                NumTranseferencia = textBoxNumTransaccion.Text
                            };

                            if (!(ServicioFinanzas.Instancia.insertarTransferencia(transferencia)))
                                completo = false;
                        }
                        else
                            completo = false;

                        xml = "<Cuentas>";
                        montoDebe = 0;
                        montoHaber = 0;
                    }
                    if (completo)
                        MessageBox.Show("Pagos registrados!", "SIA", MessageBoxButton.OK, MessageBoxImage.Information);
                    textBoxNumTransaccion.Text = "";
                    comboBoxBancos.SelectedIndex = -1;
                    var facturas = ServicioFinanzas.Instancia.ObtenerFacturasXEstadoXSocioNegocio(((SocNegocio)comboBoxClientes.SelectedItem).Codigo, "Pendiente");
                    listBoxFactClientes.ItemsSource = facturas;
                    listBoxFactClientes.Items.Refresh();
                }
            }
        }
    }
}
