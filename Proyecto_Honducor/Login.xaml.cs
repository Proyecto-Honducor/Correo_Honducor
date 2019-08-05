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
using System.Windows.Shapes;
using System.Configuration;

namespace Proyecto_Honducor
{
    //// <summary>
    /// Lógica de interacción para Login.xaml
    //// </summary>
    public partial class Login : Window
    {
        ClaseGlobal cglobal = new ClaseGlobal();
        LinqToSqlDataClassesDataContext dataContext;
        public Login()
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Honducor.Properties.Settings.HonducorConnectionString"].ConnectionString;

            dataContext = new LinqToSqlDataClassesDataContext(connectionString);
            
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Btningresar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var usuariologeado = dataContext.Usuario.FirstOrDefault(usu => usu.nombreUsuario.Equals(txtUsuario.Text));
                if (usuariologeado == null)
                {
                    MessageBox.Show("ingrese un usuario valido");
                    txtUsuario.Text = "";
                    txtUsuario.Focus();
                }
                else
                {
                    if (usuariologeado.contrasenia == txtContrasena.Password)
                    {
                        MessageBox.Show("Logueado con exito");
                        ClaseGlobal.Idlog=usuariologeado.idUsuario;
                        ClaseGlobal.Nomlog = txtUsuario.Text;
                        ClaseGlobal.Cargolog = usuariologeado.nivel;
                        this.Close();
                    }
                    else
                        MessageBox.Show("Usuario o Contraseña incorrect@s");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            
            
        }

        private void TxtContrasena_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                var usuariologeado = dataContext.Usuario.FirstOrDefault(usu => usu.nombreUsuario.Equals(txtUsuario.Text));
                if (usuariologeado == null)
                {
                    MessageBox.Show("ingrese un usuario valido");
                    txtUsuario.Text = "";
                    txtUsuario.Focus();
                }
                else
                {
                    if (usuariologeado.contrasenia == txtContrasena.Password)
                    {
                        MessageBox.Show("Logueado con exito");
                        ClaseGlobal.Idlog = usuariologeado.idUsuario;
                        ClaseGlobal.Nomlog = txtUsuario.Text;
                        ClaseGlobal.Cargolog = usuariologeado.nivel;
                        this.Close();
                    }
                    else
                        MessageBox.Show("Usuario o Contraseña incorrect@s");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
