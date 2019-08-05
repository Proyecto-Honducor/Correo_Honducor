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

            var clente = from u in datacontext.GetTable<Cliente>()
                         select new { u.identidad, u.nombre, u.apellido, u.telefono };
            if (clente == null)
            { MessageBox.Show("no existe"); }//
            dgcliente.ItemsSource = clente.ToList();
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
                if (txtNombre.Text != "" && txtApellido.Text != "" && txtIdentidad.Text != "" && txtNumeroTel.Text != "")
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
                else
                    MessageBox.Show("Faltan datos por ingresar");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text != "" && txtApellido.Text != "" && txtIdentidad.Text != "" && txtNumeroTel.Text != "")
            {

                var update = datacontext.Cliente.FirstOrDefault(up => up.identidad.Equals(txtIdentidad.Text));
                update.identidad = txtIdentidad.Text;
                update.nombre = txtNombre.Text;
                update.apellido = txtApellido.Text;
                update.telefono = txtNumeroTel.Text;
                try
                {
                    datacontext.SubmitChanges();
                    MessageBox.Show("Registro actualizado con exito");
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }


                dgcliente.ItemsSource = datacontext.Cliente;
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdentidad.Text != "")
            {
                var empleado = (from emp in datacontext.Cliente
                                where emp.identidad == txtIdentidad.Text
                                select emp).FirstOrDefault();
                //var empleado = data.Empleado.First(emp => emp.nombre.Equals(txtNombre.Text));
                if (empleado != null)
                {
                    var eliminar = from elim in datacontext.Cliente
                                   where elim.identidad.Equals(txtIdentidad.Text)
                                   select elim;
                    foreach (var detalles in eliminar)
                    {
                        datacontext.Cliente.DeleteOnSubmit(detalles);
                    }
                    try
                    {
                        datacontext.SubmitChanges();
                        MessageBox.Show("Registro eliminado con exito");
                        dgcliente.ItemsSource = datacontext.Cliente;
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            else
                MessageBox.Show("No existe registo con ese nombre");
        }

        private void Btnbuscar_Click(object sender, RoutedEventArgs e)
        {

            if (txtIdentidad.Text != "")
            {
                datacontext = new LinqToSqlDataClassesDataContext();
                var clente = from u in datacontext.GetTable<Cliente>()
                               where u.identidad.Equals(txtIdentidad.Text)
                               select new { u.identidad, u.nombre, u.apellido, u.telefono };
                if (clente == null)
                { MessageBox.Show("no existe"); }//
                dgcliente.ItemsSource = clente.ToList();
            }
            else
                MessageBox.Show("ingrese un numero de identidad"); txtIdentidad.Focus();
        }
    }
}
