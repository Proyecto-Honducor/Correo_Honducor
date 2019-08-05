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

            data = new LinqToSqlDataClassesDataContext(connectionString);////

            data = new LinqToSqlDataClassesDataContext();
            var venta = from u in data.GetTable<Venta>()
                           select new { u.idEmpleado, u.identidadCliente, u.idPaquete, u.nombreCompletoCliente, u.fechaVenta, u.isv};
            dtDetalleVenta.ItemsSource = venta.ToList();
            txtEmpleado.Text = ClaseGlobal.Idlog.ToString();


        }

        private void TxtGenerar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtcantidad.Text != "" && txtCliente.Text != "" && txtEmpleado.Text != "" && txtfechaventa.Text != "" && txtIdenCliente1.Text != "" && txtidPaquete.Text != "" && txtIvs.Text != "" && txtPrecio.Text != "" && txtTotal.Text != "")
                {
                    Venta ven = new Venta();
                    ven.idEmpleado = Convert.ToInt32(txtEmpleado.Text);
                    ven.identidadCliente = txtIdenCliente1.Text;
                    ven.idPaquete = Convert.ToInt32(txtidPaquete.Text);
                    ven.nombreCompletoCliente = txtCliente.Text;
                    ven.fechaVenta = Convert.ToDateTime(txtfechaventa.Text);
                    ven.isv = Convert.ToDecimal(txtIvs.Text);

                    data.Venta.InsertOnSubmit(ven);
                    data.SubmitChanges();


                    var venta = data.Venta.FirstOrDefault(usu => usu.nombreCompletoCliente.Equals(txtCliente.Text));
                    if (venta.nombreCompletoCliente != null)
                    {
                        ClaseGlobal.Idventa = venta.idVenta;
                        DetalleVenta dt = new DetalleVenta();
                        dt.idPaquete = Convert.ToInt32(txtidPaquete.Text);
                        dt.idVenta = ClaseGlobal.Idventa;
                        dt.precioUnidad = Convert.ToDecimal(txtPrecio.Text);
                        dt.cantidad = Convert.ToInt32(txtcantidad.Text);
                        dt.total = Convert.ToDecimal(txtTotal.Text);

                        data.DetalleVenta.InsertOnSubmit(dt);
                        data.SubmitChanges();
                    }
                    dtDetalleVenta.ItemsSource = data.Venta;
                    MessageBox.Show("Venta registrada con exito");
                }  
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
