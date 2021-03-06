﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Login_WPF.Ventas
{
    /// <summary>
    /// Interaction logic for OrdenVenta.xaml
    /// </summary>
    public partial class OrdenVenta : Window
    {
        public OrdenVenta()
        {
            InitializeComponent();
        }
        public void InicializarControles()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            encabezado1.Fecha1Label = "Fecha de Contabilización";
            encabezado1.Fecha2Label = "Fecha de Entrega";
            encabezado1.SocioLabel = "Cliente:";
            encabezado1.CargarSocios("Cliente");
            detalle1.InicializarColumnas(true);
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InicializarControles();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            if (encabezado1.txtConsecutivo.Text.Length < 1)
            {
                MessageBox.Show("Digite un consecutivo");
                return;
            }
            if (encabezado1.cmbSocio.SelectedItem == null)
            {
                MessageBox.Show("Escoja un proveedor");
                return;
            }
            if (encabezado1.fecha2.SelectedDate == null)
            {
                MessageBox.Show("Escoja una fecha de entrega");
                return;
            }
            if (detalle1.Productos.Count() < 1)
            {
                MessageBox.Show("No hay articulos o servicios");
                return;
            }
            var documento = new AccesoServicio.FinanzasService.Documento();
            documento.Consecutivo = encabezado1.txtConsecutivo.Text;
            documento.TipoDocumento = "Orden de Venta";
            documento.Fecha1 = encabezado1.fecha1.SelectedDate.Value;
            documento.Fecha2 = encabezado1.fecha2.SelectedDate.Value;
            documento.Subtotal = double.Parse(detalle1.lblSubtotal.Content.ToString());
            documento.Total = double.Parse(detalle1.lblTotal.Content.ToString());
            documento.EsServicio = false;
            documento.DescripcionServicio = "-";
            documento.CodigoCuentaServicio = "-";
            documento.LineasVenta = detalle1.Productos.ToArray();
            documento.SocioNegocio = (SocNegocio)encabezado1.cmbSocio.SelectedItem;
            if (ServicioFinanzas.Instancia.GuardarDocumento(documento))
            {
                MessageBox.Show("Orden de venta guardada");
                encabezado1.txtConsecutivo.Text = string.Empty;
                encabezado1.cmbSocio.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Se produjo un error");
            }
        }
    }
}
