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
    /// Lógica de interacción para UserControlUsuario.xaml
    /// </summary>
    public partial class UserControlUsuario : UserControl
    {
        LinqToSqlDataClassesDataContext datacontext;
        public UserControlUsuario()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Honducor.Properties.Settings.HonducorConnectionString"].ConnectionString;

            datacontext = new LinqToSqlDataClassesDataContext(connectionString);

            datacontext = new LinqToSqlDataClassesDataContext();
            var usuario = from u in datacontext.GetTable<Usuario>()
                           select new { u.idUsuario, u.nombreUsuario, u.nivel, u.contrasenia, u.idEmpleado};
            dgusu.ItemsSource = usuario.ToList();

            if(ClaseGlobal.Estado==1)
            {
                txtnombre.Text = ClaseGlobal.Idusu;
                txtnombre.IsEnabled = false;
            }
            txtidempleado.Text = ClaseGlobal.Idempleadocreado.ToString();
        }

        private void Btnsalir_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
            ClaseGlobal.Estado = 0;
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Usuario usu = new Usuario();
                usu.nombreUsuario = txtnombre.Text;
                usu.contrasenia = txtContrasenia.Text;
                usu.nivel = cbNivel.Text;
                usu.idEmpleado = Convert.ToInt32(txtidempleado.Text);

                datacontext.Usuario.InsertOnSubmit(usu);
                datacontext.SubmitChanges();

                dgusu.ItemsSource = datacontext.Usuario;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            cbNivel.ItemsSource = null;
            txtContrasenia.Text = " ";
            txtidempleado.Text = " ";
            txtnombre.Text = " ";
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
