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
    /// Lógica de interacción para UserControlEmpleado.xaml
    /// </summary>
    public partial class UserControlEmpleado : UserControl
    {
        private LinqToSqlDataClassesDataContext data;
        public UserControlEmpleado()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Honducor.Properties.Settings.HonducorConnectionString"].ConnectionString;

            data = new LinqToSqlDataClassesDataContext(connectionString);

        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            data = new LinqToSqlDataClassesDataContext();
            var empleado = from u in data.GetTable<Empleado>()
                           select new { u.idEmpleado, u.identidad, u.nombre, u.apellido, u.direccion, u.fechaNac, u.estadoCivil, u.sexo, u.telefono };
            dgEmpleado1.ItemsSource = empleado.ToList();
            
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Empleado emp = new Empleado();
            emp.identidad = txtIdentidad.Text;
            emp.nombre = txtNombre.Text;
            emp.apellido = txtApellido.Text;
            emp.direccion = txtDireccion.Text;
            emp.telefono = txtTelefono.Text;
            emp.fechaNac = Convert.ToDateTime(dateFecha.Text);
            emp.sexo = cbSexo.Text;
            emp.idCargo = Convert.ToInt32(txtCargo.Text);
            emp.estadoCivil = cbEstadoCivil.Text;

            data.Empleado.InsertOnSubmit(emp);
            data.SubmitChanges();
            dgEmpleado1.ItemsSource = data.Empleado;
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
