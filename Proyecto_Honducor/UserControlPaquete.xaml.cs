﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

namespace Proyecto_Honducor
{
    /// <summary>
    /// Lógica de interacción para UserControlPaquete.xaml
    /// </summary>
    public partial class UserControlPaquete : UserControl
    {
        private LinqToSqlDataClassesDataContext data;
        public UserControlPaquete()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Honducor.Properties.Settings.HonducorConnectionString"].ConnectionString;

            data = new LinqToSqlDataClassesDataContext(connectionString);
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            data = new LinqToSqlDataClassesDataContext();
            var paquete = from pa in data.GetTable<Paquete>()
                          select new { pa.idPaquete, pa.descripcion, pa.noSeguimiento, pa.peso, pa.direccion, pa.fechaRecibido, pa.fechaEntregado, pa.idCliente, pa.nombreCategoria };
            dgPaquete.ItemsSource = paquete.ToList();
        }
        private void BtnGuardarT_Click(object sender, RoutedEventArgs e)
        {
            Paquete paq = new Paquete();
            paq.descripcion = txtDescripcion.Text;
            paq.idCliente = Convert.ToInt32(TxtCliente.Text);
            paq.noSeguimiento = Txtnoseguimiento.Text;
            paq.peso = Convert.ToInt32(txtPeso.Text);
            paq.direccion = Txtdireccion.Text;
            paq.fechaEntregado = Convert.ToDateTime(txtFechaEntregado.Text);
            paq.nombreCategoria = txtCategoria.Text;
            
        }
    }
}
