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
            
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            data = new LinqToSqlDataClassesDataContext();
            var empleado = from u in data.GetTable<Empleado>()
                           select new { u.idEmpleado, u.identidad, u.nombre, u.apellido, u.direccion, u.fechaNac, u.estadoCivil, u.sexo, u.telefono };
            dgEmpleado.ItemsSource = empleado.ToList();
        }
    }
}
