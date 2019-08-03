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
        }

        private void Btnsalir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {

            Usuario usu = new Usuario();
            usu.nombreUsuario =txtnombre.Text;
            usu.contrasenia = txtContrasenia.Text;
            usu.nivel = txtNivel.Text;
            usu.idEmpleado = Convert.ToInt32(txtidempleado.Text);

            datacontext.Usuario.InsertOnSubmit(usu);
            datacontext.SubmitChanges();

                


        }
    }
}
