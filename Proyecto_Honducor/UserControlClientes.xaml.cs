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
    /// Lógica de interacción para UserControlClientes.xaml
    /// </summary>
    public partial class UserControlClientes : UserControl
    {
        LinqToSqlDataClassesDataContext datacontext;
        public UserControlClientes()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Honducor.Properties.Settings.HonducorConnectionString"].ConnectionString;

            datacontext = new LinqToSqlDataClassesDataContext(connectionString);
        }

        private void Btnsalir_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Grid).Children.Remove(this);
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtIdentidad.Text = " ";
            txtNombre.Text = " ";
            txtApellido.Text = " ";
            txtNumeroTel.Text = " ";
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente usu = new Cliente();
                usu.identidad = txtIdentidad.Text;
                usu.nombre = txtNombre.Text;
                usu.apellido = txtApellido.Text;
                usu.telefono = txtNumeroTel.Text;

                datacontext.Cliente.InsertOnSubmit(usu);
                datacontext.SubmitChanges();

                dgcliente.ItemsSource = datacontext.Cliente;
                MessageBox.Show("Registro Guardado Correctamente");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
