﻿using System;
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

            data = new LinqToSqlDataClassesDataContext();
            var empleado = from u in data.GetTable<Empleado>()
                           select new { u.idEmpleado, u.identidad, u.nombre, u.apellido, u.direccion, u.fechaNac, u.estadoCivil, u.sexo, u.telefono };
            dgEmpleado1.ItemsSource = empleado.ToList();
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdentidad.Text != "")
            {
                data = new LinqToSqlDataClassesDataContext();
                var empleado = from u in data.GetTable<Empleado>()
                               where u.identidad.Equals(txtIdentidad.Text)
                               select new { u.idEmpleado, u.identidad, u.nombre, u.apellido, u.direccion, u.fechaNac, u.estadoCivil, u.sexo, u.telefono };
                if(empleado==null)
                { MessageBox.Show("no existe"); }//
                dgEmpleado1.ItemsSource = empleado.ToList();
            }
            else
                MessageBox.Show("ingrese un numero de identidad"); txtIdentidad.Focus();

            
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtNombre.Text != "" && txtApellido.Text != "" && txtCargo.Text != "" && txtDireccion.Text != "" && txtIdentidad.Text != "" && txtTelefono.Text != "" && dateFecha.Text != "" && cbSexo.Text != "" && cbEstadoCivil.Text != "")
                {
                    var usuariologead = data.Empleado.FirstOrDefault(usu => usu.identidad.Equals(txtIdentidad.Text));
                    if (usuariologead == null)
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

                        var usuariologeado = data.Empleado.FirstOrDefault(usu => usu.identidad.Equals(txtIdentidad.Text));
                        ClaseGlobal.Idempleadocreado = usuariologeado.idEmpleado;
                    }
                    else
                        MessageBox.Show("Ya existe un usuario con esa identidad");txtIdentidad.Focus();
                }
                else
                    MessageBox.Show("falta un dato por completar", MessageBoxImage.Warning.ToString());
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void Btnagragarusu_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdentidad.Text != "")
            {
                var usuariologeado = data.Empleado.FirstOrDefault(usu => usu.identidad.Equals(txtIdentidad.Text));
                if (usuariologeado.identidad != null)
                {
                    ClaseGlobal.Idempleadocreado = usuariologeado.idEmpleado;
                    var usun = data.Usuario.FirstOrDefault(u => u.idEmpleado.Equals(usuariologeado.idEmpleado));
                    if (usun == null)
                    {

                        Window window = new Window
                        {
                            Title = "My User Control, Dialog",
                            Height = 500,
                            Width = 1250,
                            ResizeMode = ResizeMode.NoResize,
                            WindowStartupLocation = WindowStartupLocation.CenterScreen,
                            WindowStyle = WindowStyle.None,
                            Content = new UserControlUsuario()
                        };

                        window.ShowDialog();
                    }
                    else
                    {
                        if (MessageBox.Show("El Empleado ya tiene un usuario.... ¿Desea actualizar el Usuario?", "Atención", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            var userr = data.Usuario.FirstOrDefault(usu => usu.idEmpleado.Equals(usuariologeado.idEmpleado));
                            if(userr != null)
                            {
                                ClaseGlobal.Idusu = userr.nombreUsuario;
                                ClaseGlobal.Estado = 1;
                            }
                           
                            Window window = new Window
                            {
                                Title = "My User Control, Dialog",
                                Height = 500,
                                Width = 1250,
                                ResizeMode = ResizeMode.NoResize,
                                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                                WindowStyle = WindowStyle.None,
                               Content = new UserControlUsuario()
                            };

                            window.ShowDialog();
                            
                        }
                    }
                }
            }
            else
                MessageBox.Show("debe ingresar una identidad de un usuario valido");
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdentidad.Text != "")
            {
                var empleado = (from emp in data.Empleado
                               where emp.identidad == txtIdentidad.Text
                               select emp).FirstOrDefault();
                //var empleado = data.Empleado.First(emp => emp.nombre.Equals(txtNombre.Text));
                if (empleado != null)
                {
                    var eliminar = from elim in data.Empleado
                                   where elim.identidad.Equals(txtIdentidad.Text)
                                   select elim;
                    foreach (var detalles in eliminar)
                    {
                        data.Empleado.DeleteOnSubmit(detalles);
                    }
                    try
                    {
                        data.SubmitChanges();
                        MessageBox.Show("Registro eliminado con exito");
                        dgEmpleado1.ItemsSource = data.Empleado;
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                    MessageBox.Show("Para eliminar escriba un numero de identidad"); txtIdentidad.Focus();
            }
            else
                MessageBox.Show("No existe registo con ese nombre");
               
            
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtIdentidad.Text = " ";
            txtNombre.Text = " ";
            txtApellido.Text = " ";
            txtDireccion.Text = " ";
            txtCargo.Text = " ";
            cbSexo.ItemsSource = default;
            cbEstadoCivil.ItemsSource = default;
            txtTelefono.Text = " ";
            dateFecha.Text = " ";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Grid).Children.Remove(this);
        }

        private void TxtCargo_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void TxtCargo_GotFocus(object sender, RoutedEventArgs e)
        {
            data = new LinqToSqlDataClassesDataContext();
            var cargo = from u in data.GetTable<Cargo>()
                        select new { u.idCargo, u.cargo1 };
            dgEmpleado1.ItemsSource = cargo.ToList();
        }

        private void TxtCargo_LostFocus(object sender, RoutedEventArgs e)
        {
            data = new LinqToSqlDataClassesDataContext();
            var empleado = from u in data.GetTable<Empleado>()
                           select new { u.idEmpleado, u.identidad, u.nombre, u.apellido, u.direccion, u.fechaNac, u.estadoCivil, u.sexo, u.telefono };
            dgEmpleado1.ItemsSource = empleado.ToList();
        }
    }
}
