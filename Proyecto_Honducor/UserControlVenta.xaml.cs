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
    /// Lógica de interacción para UserControlVenta.xaml
    /// </summary>
    public partial class UserControlVenta : UserControl
    {
        
        private LinqToSqlDataClassesDataContext data;
        public UserControlVenta()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Honducor.Properties.Settings.HonducorConnectionString"].ConnectionString;

            data = new LinqToSqlDataClassesDataContext(connectionString);

            data = new LinqToSqlDataClassesDataContext();
            var venta = from u in data.GetTable<Venta>()
                           select new { u.idEmpleado, u.identidadCliente, u.idPaquete, u.nombreCompletoCliente, u.fechaVenta, u.isv};
            dtDetalleVenta.ItemsSource = venta.ToList();
            txtEmpleado.Text = ClaseGlobal.Idempleado.ToString();
        }

        private void TxtGenerar_Click(object sender, RoutedEventArgs e)
        {
            Venta ven = new Venta();
            ven.idEmpleado = txtEmpleado.Text;
            ven.identidadCliente = txtIdenCliente.Text;
            ven.idPaquete = Convert.ToInt32(txtidPaquete.Text);
            ven.nombreCompletoCliente = txtCliente.Text;
            ven.isv = Convert.ToDecimal(txtIvs.Text);

            data.Venta.InsertOnSubmit(ven);
            data.SubmitChanges();
            dtDetalleVenta.ItemsSource = data.Empleado;
        }
    }
}
