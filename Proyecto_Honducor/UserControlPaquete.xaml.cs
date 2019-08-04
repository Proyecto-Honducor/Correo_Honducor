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

            data = new LinqToSqlDataClassesDataContext();
            var paquete = from pa in data.GetTable<Paquete>()
                          select new { pa.idPaquete, pa.descripcion, pa.noSeguimiento, pa.peso, pa.direccion, pa.fechaRecibido, pa.fechaEntregado, pa.idCliente, pa.idCategoria };
            dgPaquete.ItemsSource = paquete.ToList();
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (Txtnoseguimiento.Text != "")
            {
                data = new LinqToSqlDataClassesDataContext();
                var paquete = from pa in data.GetTable<Paquete>()
                              where pa.noSeguimiento.Equals(Txtnoseguimiento.Text)
                              select new { pa.idPaquete, pa.descripcion, pa.noSeguimiento, pa.peso, pa.direccion, pa.fechaRecibido, pa.fechaEntregado, pa.idCliente, pa.idCategoria };
                dgPaquete.ItemsSource = paquete.ToList();
                //var paquete = data.Paquete.First(paq => paq.noSeguimiento.Equals(Txtnoseguimiento.Text));
                //dgPaquete.ItemsSource = data.Paquete;
            }
            else
                MessageBox.Show("ingrese un numero de seguimiento");Txtnoseguimiento.Focus();
            
        }
        private void BtnGuardarT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Paquete paq = new Paquete();
                paq.descripcion = txtDescripcion.Text;
                paq.idCliente = Convert.ToInt32(TxtCliente.Text);
                paq.noSeguimiento = Txtnoseguimiento.Text;
                paq.peso = Convert.ToInt32(txtPeso.Text);
                paq.fechaRecibido = txtfechaderecibo.DisplayDate;
                paq.direccion = Txtdireccion.Text;
                paq.fechaEntregado = txtFechaEntregado.DisplayDate;
                paq.idCategoria = Convert.ToInt32(txtCategoria.Text);

                data.Paquete.InsertOnSubmit(paq);
                data.SubmitChanges();
                dgPaquete.ItemsSource = data.Paquete;
                MessageBox.Show("REGISTRO GUARDADO CORRECTAMENTE");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            
            
        }
    }
}
