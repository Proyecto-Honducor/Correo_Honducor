using System;
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
    /// Lógica de interacción para UserControlReporte.xaml
    /// </summary>
    public partial class UserControlReporte : UserControl
    {
        private LinqToSqlDataClassesDataContext data;
        public UserControlReporte()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Honducor.Properties.Settings.HonducorConnectionString"].ConnectionString;

            data = new LinqToSqlDataClassesDataContext(connectionString);////

            data = new LinqToSqlDataClassesDataContext();
            var venta = from u in data.GetTable<Venta>()
                        select new { u.idEmpleado, u.identidadCliente, u.idPaquete, u.nombreCompletoCliente, u.fechaVenta, u.isv };
            dgreport.ItemsSource = venta.ToList();

        }

        private void Btnbuscar_Click(object sender, RoutedEventArgs e)
        {
            if(txtventasrep.Text!="")
            {

                data = new LinqToSqlDataClassesDataContext();
                var venta = from u in data.GetTable<Venta>()
                            where u.fechaVenta.Equals(Convert.ToDateTime(txtventasrep.Text))
                            select new { u.idEmpleado, u.identidadCliente, u.idPaquete, u.nombreCompletoCliente, u.fechaVenta, u.isv };
                dgreport.ItemsSource = venta.ToList();
            }
        }

        private void Btnsalir_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Grid).Children.Remove(this);
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtventasrep.Text = "";
            data = new LinqToSqlDataClassesDataContext();
            var venta = from u in data.GetTable<Venta>()
                        select new { u.idEmpleado, u.identidadCliente, u.idPaquete, u.nombreCompletoCliente, u.fechaVenta, u.isv };
            dgreport.ItemsSource = venta.ToList();
        }
    }
}
