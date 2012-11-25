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
using AccesoServicio;
using AccesoServicio.FinanzasService;

namespace Login_WPF
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : Window
    {
        public int NoCierre = 0;

        private ObservableCollection<Asiento> _Coleccion;
        private ObservableCollection<Asiento> _cuentasCierreIngresos;
        //private ObservableCollection<Cuenta> _Cuenta;

        // Xq no hacer un Mes[12]?
        public Mes Enero = new Mes { FechaInicio = null };
        public Mes Febrero = new Mes { FechaInicio = null };
        public Mes Marzo = new Mes { FechaInicio = null };
        public Mes Abril = new Mes { FechaInicio = null };
        public Mes Mayo = new Mes { FechaInicio = null };
        public Mes Junio = new Mes { FechaInicio = null };
        public Mes Julio = new Mes { FechaInicio = null };
        public Mes Agosto = new Mes { FechaInicio = null };
        public Mes Septiembre = new Mes { FechaInicio = null };
        public Mes Octubre = new Mes { FechaInicio = null };
        public Mes Noviembre = new Mes { FechaInicio = null };
        public Mes Diciembre = new Mes { FechaInicio = null };
        private string anio;
        private string FechaInicio;
        private string FechaFin;
        private bool periodoCreado = false;

        public Welcome()
        {
            InitializeComponent();

            //Inicializar DataGrid
            _Coleccion = new ObservableCollection<Asiento>();
            _cuentasCierreIngresos = new ObservableCollection<Asiento>();
            dataGridAgregaAsiento.ItemsSource = _Coleccion;

            //DataGridComboBoxColumn comboboxColumn = dataGridAgregaAsiento.Columns[2] as DataGridComboBoxColumn;
            //comboboxColumn.ItemsSource = _Cuenta;
            var cuentas = ServicioFinanzas.Instancia.DemeCuentasHijas();
            _CmbCuentas.ItemsSource = cuentas;

            var monedas = ServicioFinanzas.Instancia.ObtenerMonedas();
            comboBoxMoneda.ItemsSource = monedas;
            /*Moneda monedaTodas = new Moneda ()
                {
                    Nombre="TODAS"
                };
            comboBoxMoneda.Items.Add(monedaTodas);*/
            comboBoxMoneda.SelectedIndex = 0;

            foreach (MonedasValidas moneda in Enum.GetValues(typeof(MonedasValidas)))
            {
                CMBNuevaMoneda.Items.Add(moneda);
            }
            CMBNuevaMoneda.SelectedIndex = 0;


            /*Carga del treeview*/
            var cuentasTreeView = ServicioFinanzas.Instancia.ObtenerCuentasTreeView();
            TreeViewItem itemTreeView = Parent;
            TreeViewItem itemNivelCuatro = null;
            TreeViewItem itemNivelTres = null;
            TreeViewItem itemNivelDos = null;

            foreach (var item in cuentasTreeView) 
            {
                TreeViewItem newChild = new TreeViewItem();
                newChild.Header = item.Codigo + " " + item.Nombre;

                if (item.Nivel == 1)
                {
                    itemTreeView.Items.Add(newChild);
                    itemNivelDos = newChild;
                }
                else 
                {
                    if (item.Nivel == 2)
                    {
                        itemNivelDos.Items.Add(newChild);
                        itemNivelTres = newChild;
                    }
                    else
                    {
                        if (item.Nivel == 3)
                        {
                            itemNivelTres.Items.Add(newChild);
                            itemNivelCuatro = newChild;
                        }
                        else
                        {
                            itemNivelCuatro.Items.Add(newChild);
                        }
                    }
                }
                /*TreeViewItem newChild = new TreeViewItem();
                newChild.Header = item.Nombre;
                Parent.Items.Add(newChild);*/
            }
            Parent.IsExpanded = true;

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
                NoCierre = 1;
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
            Cuenta cuenta = new Cuenta()
                {
                    Codigo = textBoxCodigo.Text,
                    Nombre = textBoxNomCuenta.Text,
                    NombreIdiomaExtranjero = textBoxNomExtranjero.Text,
                    _Moneda=(Moneda)comboBoxMoneda.SelectedItem
                };
            if (ServicioFinanzas.Instancia.CrearCuenta(cuenta))
            {
                MessageBoxResult result = MessageBox.Show("Se Ha Agregado La Cuenta Correctamente");
                Reset();
            }
            else
                MessageBox.Show("Error al intentar crear la cuenta");
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

        public void ResetMoneda()
        {
            textBoxAcronimoMoneda.Text = "";
        }

        private void buttonCrearMoneda_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxAcronimoMoneda.Text == "")
                MessageBox.Show("Llene los dos datos necesarios por favor");
            else
            {
                Moneda nuevaMoneda = new Moneda()
                {
                    Nombre=CMBNuevaMoneda.SelectedItem.ToString(),
                    Acronimo=textBoxAcronimoMoneda.Text,                   
                    TipoMoneda= (MonedasValidas)CMBNuevaMoneda.SelectedItem
                };

                //Moneda nuevaMoneda = (Moneda)CMBNuevaMoneda.SelectedItem;

                if (ServicioFinanzas.Instancia.InsertarNuevaMoneda(nuevaMoneda))
                {
                    MessageBoxResult result = MessageBox.Show("Se Ha Agregado La Moneda Correctamente");
                    ResetMoneda();
                }
                else
                    MessageBox.Show("Error al insertar la moneda");
            }
        }

        private void buttonRealizarCambioPeriodo_Click(object sender, RoutedEventArgs e)
        {
            if (datePickerMesInicio.SelectedDate == null || comboBoxFinPeriodo.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un mes de inicio.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (comboBoxMeses.SelectedItem == null || comboBoxInicio.SelectedItem == null || comboBoxFin.SelectedItem == null || comboBoxEstado.SelectedItem == null)
                {
                    MessageBox.Show("Datos incompletos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string mes = comboBoxMeses.SelectedItem.ToString();
                    switch (mes)
                    {
                        case "Enero":
                            Enero = new Mes()
                            {
                                NombreMes = "Enero",
                                FechaInicio = comboBoxInicio.SelectedItem.ToString(),
                                FechaFin = comboBoxFin.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstado.SelectedItem.ToString()
                            };
                            break;

                        case "Febrero":
                            Febrero = new Mes()
                            {
                                NombreMes = "Febrero",
                                FechaInicio = comboBoxInicio.SelectedItem.ToString(),
                                FechaFin = comboBoxFin.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstado.SelectedItem.ToString()
                            };
                            break;

                        case "Marzo":
                            Marzo = new Mes()
                            {
                                NombreMes = "Marzo",
                                FechaInicio = comboBoxInicio.SelectedItem.ToString(),
                                FechaFin = comboBoxFin.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstado.SelectedItem.ToString()
                            };
                            break;

                        case "Abril":
                            Abril = new Mes()
                            {
                                NombreMes = "Abril",
                                FechaInicio = comboBoxInicio.SelectedItem.ToString(),
                                FechaFin = comboBoxFin.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstado.SelectedItem.ToString()
                            };
                            break;

                        case "Mayo":
                            Mayo = new Mes()
                            {
                                NombreMes = "Mayo",
                                FechaInicio = comboBoxInicio.SelectedItem.ToString(),
                                FechaFin = comboBoxFin.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstado.SelectedItem.ToString()
                            };
                            break;

                        case "Junio":
                            Junio = new Mes()
                            {
                                NombreMes = "Junio",
                                FechaInicio = comboBoxInicio.SelectedItem.ToString(),
                                FechaFin = comboBoxFin.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstado.SelectedItem.ToString()
                            };
                            break;

                        case "Julio":
                            Julio = new Mes()
                            {
                                NombreMes = "Julio",
                                FechaInicio = comboBoxInicio.SelectedItem.ToString(),
                                FechaFin = comboBoxFin.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstado.SelectedItem.ToString()
                            };
                            break;

                        case "Agosto":
                            Agosto = new Mes()
                            {
                                NombreMes = "Agosto",
                                FechaInicio = comboBoxInicio.SelectedItem.ToString(),
                                FechaFin = comboBoxFin.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstado.SelectedItem.ToString()
                            };
                            break;

                        case "Septiembre":
                            Septiembre = new Mes()
                            {
                                NombreMes = "Septiembre",
                                FechaInicio = comboBoxInicio.SelectedItem.ToString(),
                                FechaFin = comboBoxFin.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstado.SelectedItem.ToString()
                            };
                            break;

                        case "Octubre":
                            Octubre = new Mes()
                            {
                                NombreMes = "Octubre",
                                FechaInicio = comboBoxInicio.SelectedItem.ToString(),
                                FechaFin = comboBoxFin.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstado.SelectedItem.ToString()
                            };
                            break;

                        case "Noviembre":
                            Noviembre = new Mes()
                            {
                                NombreMes = "Noviembre",
                                FechaInicio = comboBoxInicio.SelectedItem.ToString(),
                                FechaFin = comboBoxFin.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstado.SelectedItem.ToString()
                            };
                            break;

                        case "Diciembre":
                            Diciembre = new Mes()
                            {
                                NombreMes = "Diciembre",
                                FechaInicio = comboBoxInicio.SelectedItem.ToString(),
                                FechaFin = comboBoxFin.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstado.SelectedItem.ToString()
                            };
                            break;
                    }

                    comboBoxFin.SelectedIndex = -1;
                    comboBoxInicio.SelectedIndex = -1;
                    comboBoxEstado.SelectedIndex = -1;
                    comboBoxMeses.SelectedIndex = -1;
                }
            }
        }

        private void buttonTipoDeCambio_Click(object sender, RoutedEventArgs e)
        {
            TipoCambio tc = new TipoCambio();
            tc.Show();
        }

        private void buttonGenerarCierre_Click(object sender, RoutedEventArgs e)
        {
            _cuentasCierreIngresos = new ObservableCollection<Asiento>();
            double montoGlobalPG = 0;
            var monedaSistema = ServicioFinanzas.Instancia.ObtenerMonedasSistema("Sistema");
            Asiento asientoBlanco = new Asiento();
            double montoPG = 0;
            double montoCV = 0;
            double factor = Math.Pow(10, 2);
            var cuentaPG = ServicioFinanzas.Instancia.ObtenerCuenta("Pérdidas y ganancias");
            var asientoPG = new Asiento();
            Cuenta cuentaNuevaPG = new Cuenta();
            var cuentaCV = ServicioFinanzas.Instancia.ObtenerCuenta("Costo de ventas");
            var asientoCV = new Asiento();
            Cuenta cuentaNuevaCV = new Cuenta();

            //Agregar asientos de cierre de compras
            var cuentasCompras = ServicioFinanzas.Instancia.ObtenerCuentasCierreCompras();
            foreach (var cuenta in cuentasCompras)
            {
                var asiento = new Asiento();
                Cuenta cuentaNueva = new Cuenta();
                cuentaNueva.Nombre = cuenta.Nombre;
                cuentaNueva.Codigo = cuenta.Codigo;
                asiento.Cuenta = cuentaNueva;
                asiento.FechaContable = DateTime.Today;
                if (cuenta.Saldo_Haber != 0)
                {
                    asiento.HaberMonedaSistema = cuenta.Saldo_Haber;
                    asiento.HaberMonedaOtra = ServicioFinanzas.Instancia.ConvertirAMonedaLocal(monedaSistema, cuenta.Saldo_Haber);
                    asiento.HaberMonedaOtra = Math.Truncate(asiento.HaberMonedaOtra * factor) / factor;
                    montoCV+=cuenta.Saldo_Haber;
                }
                else
                {
                    asiento.DebeMonedaSistema = cuenta.Saldo;
                    asiento.DebeMonedaOtra = ServicioFinanzas.Instancia.ConvertirAMonedaLocal(monedaSistema, cuenta.Saldo);
                    asiento.DebeMonedaOtra = Math.Truncate(asiento.DebeMonedaOtra * factor) / factor;
                    montoCV -= cuenta.Saldo_Haber;
                }
                asiento.MonedaAcronimo = monedaSistema.Acronimo;
                _cuentasCierreIngresos.Add(asiento);
            }

            cuentaNuevaCV.Codigo = cuentaCV.Codigo;
            cuentaNuevaCV.Nombre = cuentaCV.Nombre;
            asientoCV.Cuenta = cuentaNuevaCV;
            asientoCV.FechaContable = DateTime.Today;
            if (montoCV != 0)
            {
                asientoCV.HaberMonedaSistema = montoCV;
                asientoCV.HaberMonedaOtra = ServicioFinanzas.Instancia.ConvertirAMonedaLocal(monedaSistema, montoPG);
                asientoCV.HaberMonedaOtra = Math.Truncate(asientoCV.HaberMonedaOtra * factor) / factor;
                asientoCV.MonedaAcronimo = monedaSistema.Acronimo;
                _cuentasCierreIngresos.Add(asientoCV);
            }
            _cuentasCierreIngresos.Add(asientoBlanco);

            //Agregar asientos de cierre de Ingresos
            asientoPG = new Asiento();
            cuentaNuevaPG = new Cuenta();
            var cuentasIngresos = ServicioFinanzas.Instancia.ObtenerCuentasCierreIngresos();
            foreach(var cuenta in cuentasIngresos){
                var asiento = new Asiento();
                Cuenta cuentaNueva = new Cuenta();
                cuentaNueva.Nombre = cuenta.Nombre;
                cuentaNueva.Codigo = cuenta.Codigo;
                asiento.Cuenta = cuentaNueva;
                asiento.FechaContable = DateTime.Today;
                if (cuenta.Saldo_Haber != 0)
                {
                    asiento.HaberMonedaSistema = cuenta.Saldo_Haber;
                    asiento.HaberMonedaOtra = ServicioFinanzas.Instancia.ConvertirAMonedaLocal(monedaSistema,cuenta.Saldo_Haber);
                    asiento.HaberMonedaOtra = Math.Truncate(asiento.HaberMonedaOtra * factor) / factor;
                    montoPG -= cuenta.Saldo_Haber;
                }
                else
                {
                    asiento.DebeMonedaSistema = cuenta.Saldo;
                    asiento.DebeMonedaOtra = ServicioFinanzas.Instancia.ConvertirAMonedaLocal(monedaSistema, cuenta.Saldo);
                    asiento.DebeMonedaOtra = Math.Truncate(asiento.DebeMonedaOtra * factor) / factor;
                    montoPG += cuenta.Saldo;
                }
                asiento.MonedaAcronimo=monedaSistema.Acronimo;
                _cuentasCierreIngresos.Add(asiento);
            }

            if (textBoxPerdidasYGanancias.Text == "") cuentaNuevaPG.Nombre = cuentaPG.Nombre;
            else cuentaNuevaPG.Nombre = textBoxPerdidasYGanancias.Text;
            cuentaNuevaPG.Codigo = cuentaPG.Codigo;
            asientoPG.Cuenta = cuentaNuevaPG;
            asientoPG.FechaContable = DateTime.Today;

            if (montoPG != 0)
            {
                asientoPG.HaberMonedaSistema = montoPG;
                asientoPG.HaberMonedaOtra = ServicioFinanzas.Instancia.ConvertirAMonedaLocal(monedaSistema, montoPG);
                asientoPG.HaberMonedaOtra = Math.Truncate(asientoPG.HaberMonedaOtra * factor) / factor;

                asientoPG.MonedaAcronimo = monedaSistema.Acronimo;
                _cuentasCierreIngresos.Add(asientoPG);
                montoGlobalPG += montoPG;
            }
            _cuentasCierreIngresos.Add(asientoBlanco);

            //Agregar asientos de cierre de gastos
            asientoPG = new Asiento();
            cuentaNuevaPG = new Cuenta();

            montoPG = 0;
            var cuentasGastos = ServicioFinanzas.Instancia.ObtenerCuentasCierreGastos();
            foreach (var cuenta in cuentasGastos)
            {
                var asiento = new Asiento();
                Cuenta cuentaNueva = new Cuenta();
                cuentaNueva.Nombre = cuenta.Nombre;
                cuentaNueva.Codigo = cuenta.Codigo;
                asiento.Cuenta = cuentaNueva;
                asiento.FechaContable = DateTime.Today;
                
                asiento.HaberMonedaSistema = cuenta.Saldo;
                asiento.HaberMonedaOtra = ServicioFinanzas.Instancia.ConvertirAMonedaLocal(monedaSistema, cuenta.Saldo);
                asiento.HaberMonedaOtra = Math.Truncate(asiento.HaberMonedaOtra * factor) / factor;
                montoPG += cuenta.Saldo;
                
                asiento.MonedaAcronimo = monedaSistema.Acronimo;
                _cuentasCierreIngresos.Add(asiento);
            }

            if (textBoxPerdidasYGanancias.Text == "") cuentaNuevaPG.Nombre = cuentaPG.Nombre;
            else cuentaNuevaPG.Nombre = textBoxPerdidasYGanancias.Text;
            cuentaNuevaPG.Codigo = cuentaPG.Codigo;
            asientoPG.Cuenta = cuentaNuevaPG;
            asientoPG.FechaContable = DateTime.Today;

            if (montoPG != 0)
            {
                asientoPG.DebeMonedaSistema = montoPG;
                asientoPG.DebeMonedaOtra = ServicioFinanzas.Instancia.ConvertirAMonedaLocal(monedaSistema, montoPG);
                asientoPG.DebeMonedaOtra = Math.Truncate(asientoPG.DebeMonedaOtra * factor) / factor;
                asientoPG.MonedaAcronimo = monedaSistema.Acronimo;
                _cuentasCierreIngresos.Add(asientoPG);
                montoGlobalPG -= montoPG;
            }
            _cuentasCierreIngresos.Add(asientoBlanco);


            //Asiento de PyG contra Utilidades retenidas
            asientoPG = new Asiento();
            cuentaNuevaPG = new Cuenta();
            var cuentaUR = ServicioFinanzas.Instancia.ObtenerCuenta("Utilidades retenidas");
            var asientoUR = new Asiento();
            Cuenta cuentaNuevaUR = new Cuenta();

            if (montoGlobalPG != 0)
            {
                if (textBoxPerdidasYGanancias.Text == "") cuentaNuevaPG.Nombre = cuentaPG.Nombre;
                else cuentaNuevaPG.Nombre = textBoxPerdidasYGanancias.Text;
                cuentaNuevaPG.Codigo = cuentaPG.Codigo;
                asientoPG.Cuenta = cuentaNuevaPG;
                asientoPG.FechaContable = DateTime.Today;

                asientoPG.DebeMonedaSistema = montoGlobalPG;
                asientoPG.DebeMonedaOtra = ServicioFinanzas.Instancia.ConvertirAMonedaLocal(monedaSistema, montoGlobalPG);
                asientoPG.DebeMonedaOtra = Math.Truncate(asientoPG.DebeMonedaOtra * factor) / factor;
                asientoPG.MonedaAcronimo = monedaSistema.Acronimo;

                _cuentasCierreIngresos.Add(asientoPG);

                if (textBoxUtilidades.Text == "") cuentaNuevaUR.Nombre = cuentaUR.Nombre;
                else cuentaNuevaUR.Nombre = textBoxPerdidasYGanancias.Text;
                cuentaNuevaUR.Codigo = cuentaUR.Codigo;
                asientoUR.Cuenta = cuentaNuevaUR;
                asientoUR.FechaContable = DateTime.Today;

                asientoUR.HaberMonedaSistema = asientoPG.DebeMonedaSistema;
                asientoUR.HaberMonedaOtra = asientoPG.DebeMonedaOtra;
                asientoUR.MonedaAcronimo = monedaSistema.Acronimo;

                _cuentasCierreIngresos.Add(asientoUR);
                _cuentasCierreIngresos.Add(asientoBlanco);
            }

            //Asiento de Utilidades retenidas contra dividendos
            asientoUR = new Asiento();
            var cuentaDiv = ServicioFinanzas.Instancia.ObtenerCuenta("Dividendos");
            if (cuentaDiv.Saldo != 0)
            {
                var asientoDiv = new Asiento();
                Cuenta cuentaNuevaDiv = new Cuenta();

                cuentaNuevaDiv.Nombre = cuentaDiv.Nombre;
                cuentaNuevaUR.Codigo = cuentaUR.Codigo;
                asientoDiv.Cuenta = cuentaNuevaDiv;
                asientoDiv.FechaContable = DateTime.Today;

                asientoDiv.HaberMonedaSistema = cuentaDiv.Saldo;
                asientoDiv.HaberMonedaOtra = ServicioFinanzas.Instancia.ConvertirAMonedaLocal(monedaSistema, cuentaDiv.Saldo);
                asientoDiv.HaberMonedaOtra = Math.Truncate(asientoDiv.HaberMonedaOtra * factor) / factor;
                asientoDiv.MonedaAcronimo = monedaSistema.Acronimo;

                if (textBoxUtilidades.Text == "") cuentaNuevaUR.Nombre = cuentaUR.Nombre;
                else cuentaNuevaUR.Nombre = textBoxPerdidasYGanancias.Text;
                cuentaNuevaUR.Codigo = cuentaUR.Codigo;
                asientoUR.Cuenta = cuentaNuevaUR;
                asientoUR.FechaContable = DateTime.Today;
                asientoUR.DebeMonedaOtra = asientoDiv.HaberMonedaOtra;
                asientoUR.DebeMonedaSistema = asientoDiv.HaberMonedaSistema;

                _cuentasCierreIngresos.Add(asientoUR);
                _cuentasCierreIngresos.Add(asientoDiv);
                _cuentasCierreIngresos.Add(asientoBlanco);
            }

            dataGridCierre.ItemsSource = _cuentasCierreIngresos;
            dataGridCierre.Items.Refresh();
        }

        private void datePickerMesInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            string mes = datePickerMesInicio.DisplayDate.Month.ToString();
            anio = datePickerMesInicio.DisplayDate.Year.ToString();
            int n = 0, j = 1;

            comboBoxFinPeriodo.Items.Clear();
            comboBoxMeses.Items.Clear();
            comboBoxMesesAct.Items.Clear();
            switch (mes)
            {
                case "1":
                    string[] EneroFirst = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                    foreach (string mesAhora in EneroFirst)
                    {
                        comboBoxMeses.Items.Add(mesAhora);
                        comboBoxMesesAct.Items.Add(mesAhora);
                    }
                    n = 31;
                    break;

                case "2":
                    string[] FebreroFirst = { "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre", "Enero" };
                    foreach (string mesAhora in FebreroFirst)
                    {
                        comboBoxMeses.Items.Add(mesAhora);
                        comboBoxMesesAct.Items.Add(mesAhora);
                    }
                    n = 31;
                    break;

                case "3":
                    string[] MarzoFirst = { "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre", "Enero", "Febrero" };
                    foreach (string mesAhora in MarzoFirst)
                    {
                        comboBoxMeses.Items.Add(mesAhora);
                        comboBoxMesesAct.Items.Add(mesAhora);
                    }
                    n = 28;
                    break;

                case "4":
                    string[] AbrilFirst = { "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre", "Enero", "Febrero", "Marzo" };
                    foreach (string mesAhora in AbrilFirst)
                    {
                        comboBoxMeses.Items.Add(mesAhora);
                        comboBoxMesesAct.Items.Add(mesAhora);
                    }
                    n = 31;
                    break;

                case "5":
                    string[] MayoFirst = { "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre", "Enero", "Febrero", "Marzo", "Abril" };
                    foreach (string mesAhora in MayoFirst)
                    {
                        comboBoxMeses.Items.Add(mesAhora);
                        comboBoxMesesAct.Items.Add(mesAhora);
                    }
                    n = 30;
                    break;

                case "6":
                    string[] JunioFirst = { "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre", "Enero", "Febrero", "Marzo", "Abril", "Mayo" };
                    foreach (string mesAhora in JunioFirst)
                    {
                        comboBoxMeses.Items.Add(mesAhora);
                        comboBoxMesesAct.Items.Add(mesAhora);
                    }
                    n = 31;
                    break;

                case "7":
                    string[] JulioFirst = { "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio" };
                    foreach (string mesAhora in JulioFirst)
                    {
                        comboBoxMeses.Items.Add(mesAhora);
                        comboBoxMesesAct.Items.Add(mesAhora);
                    }
                    n = 30;
                    break;

                case "8":
                    string[] AgostoFirst = { "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio" };
                    foreach (string mesAhora in AgostoFirst)
                    {
                        comboBoxMeses.Items.Add(mesAhora);
                        comboBoxMesesAct.Items.Add(mesAhora);
                    }
                    n = 31;
                    break;

                case "9":
                    string[] SeptiembreFirst = { "Septiembre", "Octubre", "Noviembre", "Diciembre", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto" };
                    foreach (string mesAhora in SeptiembreFirst)
                    {
                        comboBoxMeses.Items.Add(mesAhora);
                        comboBoxMesesAct.Items.Add(mesAhora);
                    }
                    n = 31;
                    break;

                case "10":
                    string[] OctubreFirst = { "Octubre", "Noviembre", "Diciembre", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre" };
                    foreach (string mesAhora in OctubreFirst)
                    {
                        comboBoxMeses.Items.Add(mesAhora);
                        comboBoxMesesAct.Items.Add(mesAhora);
                    }
                    n = 30;
                    break;

                case "11":
                    string[] NoviembreFirst = { "Noviembre", "Diciembre", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre" };
                    foreach (string mesAhora in NoviembreFirst)
                    {
                        comboBoxMeses.Items.Add(mesAhora);
                        comboBoxMesesAct.Items.Add(mesAhora);
                    }
                    n = 31;
                    break;

                case "12":
                    string[] DiciembreFirst = { "Diciembre", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre" };
                    foreach (string mesAhora in DiciembreFirst)
                    {
                        comboBoxMeses.Items.Add(mesAhora);
                        comboBoxMesesAct.Items.Add(mesAhora);
                    }
                    n = 30;
                    break;
            }

            while (n > 0)
            {
                comboBoxFinPeriodo.Items.Add(j);
                n--; j++;
            }

            comboBoxInicio.Items.Clear();
            comboBoxFin.Items.Clear();
            comboBoxInicioAct.Items.Clear();
            comboBoxFinAct.Items.Clear();
            comboBoxEstado.Items.Clear();
            comboBoxEstadoAct.Items.Clear();
            comboBoxEstado.Items.Add("Abierto");
            comboBoxEstado.Items.Add("Cerrado");
            comboBoxEstado.Items.Add("Abierto excepto ventas");
            comboBoxEstadoAct.Items.Add("Abierto");
            comboBoxEstadoAct.Items.Add("Cerrado");
            comboBoxEstadoAct.Items.Add("Abierto excepto ventas");
        }

        public void comboBoxMeses_SelectionChanged(string mes)
        {
            if (mes == "Enero" || mes == "Marzo" || mes == "Mayo" || mes == "Julio" || mes == "Agosto" || mes == "Octubre" || mes == "Diciembre")
            {
                for (int n = 1; n < 32; n++)
                {
                    comboBoxInicio.Items.Add(n.ToString());
                    comboBoxFin.Items.Add(n.ToString());
                    comboBoxInicioAct.Items.Add(n.ToString());
                    comboBoxFinAct.Items.Add(n.ToString());
                }
            }
            else
            {
                if (mes == "Abril" || mes == "Junio" || mes == "Septiembre" || mes == "Noviembre")
                {
                    for (int n = 1; n < 31; n++)
                    {
                        comboBoxInicio.Items.Add(n.ToString());
                        comboBoxFin.Items.Add(n.ToString());
                        comboBoxInicioAct.Items.Add(n.ToString());
                        comboBoxFinAct.Items.Add(n.ToString());
                    }
                }
                else
                {
                    for (int n = 1; n < 29; n++)
                    {
                        comboBoxInicio.Items.Add(n.ToString());
                        comboBoxFin.Items.Add(n.ToString());
                        comboBoxInicioAct.Items.Add(n.ToString());
                        comboBoxFinAct.Items.Add(n.ToString());
                    }
                }
            }
        }

        private void comboBoxMeses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBoxInicio.Items.Clear();
            comboBoxFin.Items.Clear();
            if (comboBoxMeses.SelectedItem != null)
            {
                comboBoxMeses_SelectionChanged(comboBoxMeses.SelectedItem.ToString());
            }
        }
        //ir a la base
        private void buttonTerminar_Click(object sender, RoutedEventArgs e)
        {
            if (datePickerMesInicio.SelectedDate == null || comboBoxFinPeriodo.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un mes de inicio.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Mes[] ListaMeses = new Mes[12];
                string mes;
                if (Enero.FechaInicio == null)
                {
                    Enero.NombreMes = "Enero";
                    Enero.EstadoMes = "Abierto";
                    Enero.FechaInicio = "1/1/" + anio;
                    Enero.FechaFin = "1/31/" + anio;
                }
                if (Febrero.FechaInicio == null)
                {
                    Febrero.NombreMes = "Febrero";
                    Febrero.EstadoMes = "Abierto";
                    Febrero.FechaInicio = "2/1/" + anio;
                    Febrero.FechaFin = "2/28/" + anio;
                }
                if (Marzo.FechaInicio == null)
                {
                    Marzo.NombreMes = "Marzo";
                    Marzo.EstadoMes = "Abierto";
                    Marzo.FechaInicio = "3/1/" + anio;
                    Marzo.FechaFin = "3/31/" + anio;
                }
                if (Abril.FechaInicio == null)
                {
                    Abril.NombreMes = "Abril";
                    Abril.EstadoMes = "Abierto";
                    Abril.FechaInicio = "4/1/" + anio;
                    Abril.FechaFin = "4/30/" + anio;
                }
                if (Mayo.FechaInicio == null)
                {
                    Mayo.NombreMes = "Mayo";
                    Mayo.EstadoMes = "Abierto";
                    Mayo.FechaInicio = "5/1/" + anio;
                    Mayo.FechaFin = "5/31/" + anio;
                }
                if (Junio.FechaInicio == null)
                {
                    Junio.NombreMes = "Junio";
                    Junio.EstadoMes = "Abierto";
                    Junio.FechaInicio = "6/1/" + anio;
                    Junio.FechaFin = "6/30/" + anio;
                }
                if (Julio.FechaInicio == null)
                {
                    Julio.NombreMes = "Julio";
                    Julio.EstadoMes = "Abierto";
                    Julio.FechaInicio = "7/1/" + anio;
                    Julio.FechaFin = "7/31/" + anio;
                }
                if (Agosto.FechaInicio == null)
                {
                    Agosto.NombreMes = "Agosto";
                    Agosto.EstadoMes = "Abierto";
                    Agosto.FechaInicio = "8/1/" + anio;
                    Agosto.FechaFin = "8/31/" + anio;
                }
                if (Septiembre.FechaInicio == null)
                {
                    Septiembre.NombreMes = "Septiembre";
                    Septiembre.EstadoMes = "Abierto";
                    Septiembre.FechaInicio = "9/1/" + anio;
                    Septiembre.FechaFin = "9/30/" + anio;
                }
                if (Octubre.FechaInicio == null)
                {
                    Octubre.NombreMes = "Octubre";
                    Octubre.EstadoMes = "Abierto";
                    Octubre.FechaInicio = "10/1/" + anio;
                    Octubre.FechaFin = "10/31/" + anio;
                }
                if (Noviembre.FechaInicio == null)
                {
                    Noviembre.NombreMes = "Noviembre";
                    Noviembre.EstadoMes = "Abierto";
                    Noviembre.FechaInicio = "11/1/" + anio;
                    Noviembre.FechaFin = "11/30/" + anio;
                }
                if (Diciembre.FechaInicio == null)
                {
                    Diciembre.NombreMes = "Diciembre";
                    Diciembre.EstadoMes = "Abierto";
                    Diciembre.FechaInicio = "12/1/" + anio;
                    Diciembre.FechaFin = "12/31/" + anio;
                }

                anio = datePickerMesInicio.DisplayDate.Year.ToString();

                mes = datePickerMesInicio.DisplayDate.Month.ToString();

                switch (mes)
                {
                    case "1":
                        FechaFin = "12";
                        break;
                    case "2":
                        FechaFin = "1";
                        break;
                    case "3":
                        FechaFin = "2";
                        break;
                    case "4":
                        FechaFin = "3";
                        break;
                    case "5":
                        FechaFin = "4";
                        break;
                    case "6":
                        FechaFin = "5";
                        break;
                    case "7":
                        FechaFin = "6";
                        break;
                    case "8":
                        FechaFin = "7";
                        break;
                    case "9":
                        FechaFin = "8";
                        break;
                    case "10":
                        FechaFin = "9";
                        break;
                    case "11":
                        FechaFin = "10";
                        break;
                    case "12":
                        FechaFin = "11";
                        break;
                }

                FechaInicio = datePickerMesInicio.SelectedDate.Value.Month + "/" + datePickerMesInicio.SelectedDate.Value.Day + "/" + datePickerMesInicio.SelectedDate.Value.Year;
                FechaFin = FechaFin + "/" + comboBoxFinPeriodo.SelectedItem + "/" + anio;

                ListaMeses[0] = Enero;
                ListaMeses[1] = Febrero;
                ListaMeses[2] = Marzo;
                ListaMeses[3] = Abril;
                ListaMeses[4] = Mayo;
                ListaMeses[5] = Junio;
                ListaMeses[6] = Julio;
                ListaMeses[7] = Agosto;
                ListaMeses[8] = Septiembre;
                ListaMeses[9] = Octubre;
                ListaMeses[10] = Noviembre;
                ListaMeses[11] = Diciembre;

                //meter consulta
                if (true)
                {
                    if (ServicioFinanzas.Instancia.GuardarPeriodoContable(FechaInicio,FechaFin,int.Parse(anio),ListaMeses))
                    {
                        periodoCreado = true;
                        MessageBox.Show("Creacion del periodo realizada.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Imposible actualizar el periodo contable", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void comboBoxMesesAct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBoxInicio.Items.Clear();
            comboBoxFin.Items.Clear();
            if (comboBoxMesesAct.SelectedItem != null)
            {
                comboBoxMeses_SelectionChanged(comboBoxMesesAct.SelectedItem.ToString());
            }
        }
        //ir a la base
        private void buttonCambioPeriodoAct_Click(object sender, RoutedEventArgs e)
        {
            if (!periodoCreado)
            {
                MessageBox.Show("Primero debe definir el periodo contable.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (comboBoxMesesAct.SelectedItem == null || comboBoxInicioAct.SelectedItem == null || comboBoxFinAct.SelectedItem == null || comboBoxEstadoAct.SelectedItem == null)
                {
                    MessageBox.Show("Datos incompletos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string mes = comboBoxMesesAct.SelectedItem.ToString();
                    switch (mes)
                    {
                        case "Enero":
                            Enero = new Mes()
                            {
                                NombreMes = "Enero",
                                FechaInicio = comboBoxInicioAct.SelectedItem.ToString(),
                                FechaFin = comboBoxFinAct.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstadoAct.SelectedItem.ToString()
                            };
                            break;

                        case "Febrero":
                            Febrero = new Mes()
                            {
                                NombreMes = "Febrero",
                                FechaInicio = comboBoxInicioAct.SelectedItem.ToString(),
                                FechaFin = comboBoxFinAct.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstadoAct.SelectedItem.ToString()
                            };
                            break;

                        case "Marzo":
                            Marzo = new Mes()
                            {
                                NombreMes = "Marzo",
                                FechaInicio = comboBoxInicioAct.SelectedItem.ToString(),
                                FechaFin = comboBoxFinAct.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstadoAct.SelectedItem.ToString()
                            };
                            break;

                        case "Abril":
                            Abril = new Mes()
                            {
                                NombreMes = "Abril",
                                FechaInicio = comboBoxInicioAct.SelectedItem.ToString(),
                                FechaFin = comboBoxFinAct.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstadoAct.SelectedItem.ToString()
                            };
                            break;

                        case "Mayo":
                            Mayo = new Mes()
                            {
                                NombreMes = "Mayo",
                                FechaInicio = comboBoxInicioAct.SelectedItem.ToString(),
                                FechaFin = comboBoxFinAct.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstadoAct.SelectedItem.ToString()
                            };
                            break;

                        case "Junio":
                            Junio = new Mes()
                            {
                                NombreMes = "Junio",
                                FechaInicio = comboBoxInicioAct.SelectedItem.ToString(),
                                FechaFin = comboBoxFinAct.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstadoAct.SelectedItem.ToString()
                            };
                            break;

                        case "Julio":
                            Julio = new Mes()
                            {
                                NombreMes = "Julio",
                                FechaInicio = comboBoxInicioAct.SelectedItem.ToString(),
                                FechaFin = comboBoxFinAct.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstadoAct.SelectedItem.ToString()
                            };
                            break;

                        case "Agosto":
                            Agosto = new Mes()
                            {
                                NombreMes = "Agosto",
                                FechaInicio = comboBoxInicioAct.SelectedItem.ToString(),
                                FechaFin = comboBoxFinAct.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstadoAct.SelectedItem.ToString()
                            };
                            break;

                        case "Septiembre":
                            Septiembre = new Mes()
                            {
                                NombreMes = "Septiembre",
                                FechaInicio = comboBoxInicioAct.SelectedItem.ToString(),
                                FechaFin = comboBoxFinAct.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstadoAct.SelectedItem.ToString()
                            };
                            break;

                        case "Octubre":
                            Octubre = new Mes()
                            {
                                NombreMes = "Octubre",
                                FechaInicio = comboBoxInicioAct.SelectedItem.ToString(),
                                FechaFin = comboBoxFinAct.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstadoAct.SelectedItem.ToString()
                            };
                            break;

                        case "Noviembre":
                            Noviembre = new Mes()
                            {
                                NombreMes = "Noviembre",
                                FechaInicio = comboBoxInicioAct.SelectedItem.ToString(),
                                FechaFin = comboBoxFinAct.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstadoAct.SelectedItem.ToString()
                            };
                            break;

                        case "Diciembre":
                            Diciembre = new Mes()
                            {
                                NombreMes = "Diciembre",
                                FechaInicio = comboBoxInicioAct.SelectedItem.ToString(),
                                FechaFin = comboBoxFinAct.SelectedItem.ToString(),
                                EstadoMes = comboBoxEstadoAct.SelectedItem.ToString()
                            };
                            break;
                    }

                    comboBoxFinAct.SelectedIndex = -1;
                    comboBoxInicioAct.SelectedIndex = -1;
                    comboBoxEstadoAct.SelectedIndex = -1;
                    comboBoxMesesAct.SelectedIndex = -1;
                }
            }
        }
        //ir a la base
        private void buttonTerminarAct_Click(object sender, RoutedEventArgs e)
        {
            if (!periodoCreado)
            {
                MessageBox.Show("Primero debe definir un periodo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Mes[] ListaMeses = new Mes[12];
                string mes;
                if (Enero.FechaInicio == null)
                {
                    Enero.FechaInicio = "1/1/" + anio;
                    Enero.FechaFin = "1/31" + anio;
                }
                if (Febrero.FechaInicio == null)
                {
                    Febrero.FechaInicio = "2/1/" + anio;
                    Febrero.FechaFin = "2/28" + anio;
                }
                if (Marzo.FechaInicio == null)
                {
                    Marzo.FechaInicio = "3/1/" + anio;
                    Marzo.FechaFin = "3/31" + anio;
                }
                if (Abril.FechaInicio == null)
                {
                    Abril.FechaInicio = "4/1/" + anio;
                    Abril.FechaFin = "4/30" + anio;
                }
                if (Mayo.FechaInicio == null)
                {
                    Mayo.FechaInicio = "5/1/" + anio;
                    Mayo.FechaFin = "5/31" + anio;
                }
                if (Junio.FechaInicio == null)
                {
                    Junio.FechaInicio = "6/1/" + anio;
                    Junio.FechaFin = "6/30" + anio;
                }
                if (Julio.FechaInicio == null)
                {
                    Julio.FechaInicio = "7/1/" + anio;
                    Julio.FechaFin = "7/31" + anio;
                }
                if (Agosto.FechaInicio == null)
                {
                    Agosto.FechaInicio = "8/1/" + anio;
                    Agosto.FechaFin = "8/31" + anio;
                }
                if (Septiembre.FechaInicio == null)
                {
                    Septiembre.FechaInicio = "9/1/" + anio;
                    Septiembre.FechaFin = "9/30" + anio;
                }
                if (Octubre.FechaInicio == null)
                {
                    Octubre.FechaInicio = "10/1/" + anio;
                    Octubre.FechaFin = "10/31" + anio;
                }
                if (Noviembre.FechaInicio == null)
                {
                    Noviembre.FechaInicio = "11/1/" + anio;
                    Noviembre.FechaFin = "11/30" + anio;
                }
                if (Diciembre.FechaInicio == null)
                {
                    Diciembre.FechaInicio = "12/1/" + anio;
                    Diciembre.FechaFin = "12/31" + anio;
                }

                anio = datePickerMesInicio.DisplayDate.Year.ToString();

                mes = datePickerMesInicio.DisplayDate.Month.ToString();

                int mesInt;
                if (int.TryParse(mes, out mesInt)){
                    if (mesInt == 2)
                    {
                        FechaFin = "12";
                    }
                    else
                    {
                        FechaFin = (mesInt - 1).ToString();
                    }
                }
                /* ¿Es igual q lo de arriba?
                switch (mes)
                {
                    case "1":
                        FechaFin = "12";
                        break;
                    case "2":
                        FechaFin = "1";
                        break;
                    case "3":
                        FechaFin = "2";
                        break;
                    case "4":
                        FechaFin = "3";
                        break;
                    case "5":
                        FechaFin = "4";
                        break;
                    case "6":
                        FechaFin = "5";
                        break;
                    case "7":
                        FechaFin = "6";
                        break;
                    case "8":
                        FechaFin = "7";
                        break;
                    case "9":
                        FechaFin = "8";
                        break;
                    case "10":
                        FechaFin = "9";
                        break;
                    case "11":
                        FechaFin = "10";
                        break;
                    case "12":
                        FechaFin = "11";
                        break;
                }
                */
                FechaInicio = datePickerMesInicio.SelectedDate.Value.Month + "/" + datePickerMesInicio.SelectedDate.Value.Day + "/" + datePickerMesInicio.SelectedDate.Value.Year;
                FechaFin = FechaFin + "/" + comboBoxFinPeriodo.SelectedItem + "/" + anio;

                ListaMeses[0] = Enero;
                ListaMeses[1] = Febrero;
                ListaMeses[2] = Marzo;
                ListaMeses[3] = Abril;
                ListaMeses[4] = Mayo;
                ListaMeses[5] = Junio;
                ListaMeses[6] = Julio;
                ListaMeses[7] = Agosto;
                ListaMeses[8] = Septiembre;
                ListaMeses[9] = Octubre;
                ListaMeses[10] = Noviembre;
                ListaMeses[11] = Diciembre;

                //meter la consulta
                if (true)
                {
                    MessageBox.Show("Actualizacion de los estados realizada.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Imposible actualizar el periodo contable", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void cmdAgregarCuenta_Click(object sender, RoutedEventArgs e)
        {
            var asiento = new Asiento();
            asiento.Cuenta = (Cuenta)_CmbCuentas.SelectedItem;
            double factor = Math.Pow(10, 2);
            asiento.FechaContable = fechaAsiento.SelectedDate.Value;
            if (_txtHaber.Text.Length > 0)
            {
                asiento.HaberMonedaOtra = double.Parse(_txtHaber.Text);
                asiento.HaberMonedaSistema =
                    ServicioFinanzas.Instancia.ConvertirAMonedaSistema(((Moneda) _CmbMonedas.SelectedItem).TipoMoneda,
                                                                       double.Parse(_txtHaber.Text));
                asiento.HaberMonedaSistema = Math.Truncate(asiento.HaberMonedaSistema * factor) / factor;
            }
            else
            {
                asiento.DebeMonedaOtra = double.Parse(_txtDebe.Text);
                asiento.DebeMonedaSistema =
                    ServicioFinanzas.Instancia.ConvertirAMonedaSistema(((Moneda)_CmbMonedas.SelectedItem).TipoMoneda,
                                                                       double.Parse(_txtDebe.Text));
                
                asiento.DebeMonedaSistema = Math.Truncate(asiento.DebeMonedaSistema * factor) / factor;
            }
            asiento.MonedaAcronimo = ((Moneda)_CmbMonedas.SelectedItem).Acronimo;
            _Coleccion.Add(asiento);
            dataGridAgregaAsiento.Items.Refresh();
        }

        private void buttonGuardarAsiento_Click(object sender, RoutedEventArgs e)
        {
            double factor = Math.Pow(10, 2);
            double montoMonedaLocal = 0;
            string message = "Desea guardar el asiento creado?";
            string caption = "Confirmación";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
            {
                var monedaSistema = ServicioFinanzas.Instancia.ObtenerMonedasSistema("Sistema");
                var monedaLocal = ServicioFinanzas.Instancia.ObtenerMonedasSistema("Local");
                string xml = "<Cuentas>";
                double montoDebe = 0, montoHaber = 0;
                foreach (var item in _Coleccion)
                {
                    /*      <Cuentas>
                      --		<Cuenta monto="10000.00" moneda="CRC" cuenta="" debe="0"/>
                      --		<Cuenta monto="5490.98" moneda="CRC" cuenta="" debe="1"/>
                      --  </Cuentas>*/
                    xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                        (item.DebeMonedaSistema > 1 ? item.DebeMonedaSistema.ToString() : item.HaberMonedaSistema.ToString()),
                        monedaSistema.Acronimo, item.Cuenta.Codigo, (item.DebeMonedaSistema != 0) ? "1" : "0");
                    montoDebe += (item.DebeMonedaSistema != null ? item.DebeMonedaSistema : 0);
                    montoHaber += (item.HaberMonedaSistema != null ? item.HaberMonedaSistema : 0);

                    if (item.DebeMonedaSistema > 1)
                    {
                        montoMonedaLocal = ServicioFinanzas.Instancia.ConvertirAMonedaLocal(monedaSistema, item.DebeMonedaSistema);
                        montoMonedaLocal = Math.Truncate(montoMonedaLocal * factor) / factor;
                    }
                    else 
                    {
                        montoMonedaLocal = ServicioFinanzas.Instancia.ConvertirAMonedaLocal(monedaSistema, item.HaberMonedaSistema);
                        montoMonedaLocal = Math.Truncate(montoMonedaLocal * factor) / factor;
                    }
                    xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                        montoMonedaLocal.ToString(),monedaLocal.Acronimo, item.Cuenta.Codigo, (item.DebeMonedaSistema != 0) ? "1" : "0");
                }
                xml += "</Cuentas>";
                

                if (ServicioFinanzas.Instancia.InsertarAsiento(fechaAsiento.SelectedDate.Value.Month.ToString()+"/"+fechaAsiento.SelectedDate.Value.Day.ToString()+"/"+fechaAsiento.SelectedDate.Value.Year.ToString(),//fechaAsiento.SelectedDate.Value.ToShortDateString(),
                    montoDebe, montoHaber, xml, "AS"))
                {
                    MessageBox.Show("Asiento agregado!", "SIA", MessageBoxButton.OK, MessageBoxImage.Information);
                    _Coleccion = new ObservableCollection<Asiento>();
                    dataGridAgregaAsiento.Items.Refresh();
                    _txtDebe.Text = string.Empty;
                    _txtHaber.Text = string.Empty;
                }
            }
        }

        private void _CmbCuentas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var monedas = ServicioFinanzas.Instancia.DemeMonedasCuenta(((Cuenta)_CmbCuentas.SelectedItem).Nombre);
            _CmbMonedas.ItemsSource = monedas;
            _CmbMonedas.SelectedIndex = 1;
        }

        private void buttonGuardarAsientosCierre_Click_1(object sender, RoutedEventArgs e)
        {
            double factor = Math.Pow(10, 2);
            string message = "Desea guardar los asiento creados?";
            string caption = "Confirmación";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
            {
                var monedaSistema = ServicioFinanzas.Instancia.ObtenerMonedasSistema("Sistema");
                var monedaLocal = ServicioFinanzas.Instancia.ObtenerMonedasSistema("Local");
                string xml = "<Cuentas>";
                double montoDebe = 0, montoHaber = 0;
                foreach (var item in _cuentasCierreIngresos)
                {
                    if (item.Cuenta != null)
                    {
                        /*      <Cuentas>
                          --		<Cuenta monto="10000.00" moneda="CRC" cuenta="" debe="0"/>
                          --		<Cuenta monto="5490.98" moneda="CRC" cuenta="" debe="1"/>
                          --  </Cuentas>*/
                        xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                            (item.DebeMonedaSistema > 1 ? item.DebeMonedaSistema.ToString() : item.HaberMonedaSistema.ToString()),
                            monedaSistema.Acronimo, item.Cuenta.Codigo, (item.DebeMonedaSistema != 0) ? "1" : "0");
                        montoDebe += (item.DebeMonedaSistema != null ? item.DebeMonedaSistema : 0);
                        montoHaber += (item.HaberMonedaSistema != null ? item.HaberMonedaSistema : 0);

                        xml += string.Format("<Cuenta monto=\"{0}\" moneda=\"{1}\" cuenta=\"{2}\" debe=\"{3}\" />",
                            (item.DebeMonedaSistema > 1 ? item.DebeMonedaOtra.ToString() : item.HaberMonedaOtra.ToString()),
                            monedaLocal.Acronimo, item.Cuenta.Codigo, (item.DebeMonedaSistema != 0) ? "1" : "0");
                    }
                    else 
                    {
                        xml += "</Cuentas>";
                        ServicioFinanzas.Instancia.InsertarAsiento(DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString(), montoDebe, montoHaber, xml, "Automatico");
                        xml = "<Cuentas>";
                        montoDebe = 0;
                        montoHaber = 0;
                    }
                }
                MessageBox.Show("Asientos agregados!", "SIA", MessageBoxButton.OK, MessageBoxImage.Information);
                _cuentasCierreIngresos = new ObservableCollection<Asiento>();
                dataGridCierre.Items.Refresh();
            }
        }

    }
}