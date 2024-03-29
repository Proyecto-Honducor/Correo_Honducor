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
using System.Windows.Threading;

namespace Proyecto_Honducor
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            Login log = new Login();
            log.ShowDialog();
            lblusu.Content = ClaseGlobal.Nomlog;
            lblcargo.Content = ClaseGlobal.Cargolog;
            startclock();
            
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            
            
        }

        private void startclock()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickevent;
            timer.Start();
        }

        private void tickevent(object sender, EventArgs e)
        {
            var hn = new System.Globalization.CultureInfo("es-HN");
           //hourlbl.Text= DateTime.Parse("29/7/2019", hn).ToLongDateString();
            datelbl.Text = DateTime.Now.ToLongTimeString();
           // hourlbl.Text = DateTime.Now.ToLongDateString();
            hourlbl.Text = DateTime.Now.ToString("D",hn);
           
        }

        private void ButtonAbrirMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCerrarMenu.Visibility = Visibility.Visible;
            ButtonAbrirMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCerrarMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCerrarMenu.Visibility = Visibility.Collapsed;
            ButtonAbrirMenu.Visibility = Visibility.Visible;
        }
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemEmpleados":
                    usc = new UserControlEmpleado();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemCliente":
                    usc = new UserControlClientes();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemPaquete":
                    usc = new UserControlPaquete();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemVentas":
                    usc = new UserControlVenta();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemReporte":
                    usc = new UserControlReporte();
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }
        //Login login = new Login();
        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Realmente desea salir?", "Consulta", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                App.Current.Shutdown();
            }
            else
            {
                //No hace nada
            }
        }

        private void BtnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            Login log = new Login();
            log.ShowDialog();
            lblusu.Content = ClaseGlobal.Nomlog;
            lblcargo.Content = ClaseGlobal.Cargolog;

        }

        private void BtnAyuda_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCuenta_Click(object sender, RoutedEventArgs e)
        {


        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key.Equals(Key.F5))
            {
                Login log = new Login();
                log.ShowDialog();
                lblusu.Content = ClaseGlobal.Nomlog;
                lblcargo.Content = ClaseGlobal.Cargolog;
            }
        }
    }
}
