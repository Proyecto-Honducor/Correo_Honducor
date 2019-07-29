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
            startclock();
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
                    //usc = new UserControlEmpleados();
                    //GridMain.Children.Add(usc);
                    break;
                case "ItemFacturacion":
                    //usc = new UserControlFacturacion();
                    //GridMain.Children.Add(usc);
                    break;
                case "ItemHistorial":
                   // usc = new UserControlHistorial();
                    //GridMain.Children.Add(usc);
                    break;
                case "ItemCargo":
                    //usc = new UserControlCarg();
                    //GridMain.Children.Add(usc);
                    break;
                case "ItemCategoria":
                    //usc = new UserControlCategoria();
                    //GridMain.Children.Add(usc);
                    break;
                case "ItemClientes":
                    //usc = new UserControlClientes();
                    //GridMain.Children.Add(usc);
                    break;
                case "ItemProducto":
                    //usc = new UserControlProducto();
                   // GridMain.Children.Add(usc);
                    break;
                case "ItemUsuarios":
                    //usc = new UserControlUsuarios();
                    //ridMain.Children.Add(usc);
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
            if (MessageBox.Show("Realmente desea cerrar sesion?", "Consulta", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
              //  login.Show();
                this.Close();
            }
            else
            {
                //No hace nada
            }

        }

        private void BtnAyuda_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCuenta_Click(object sender, RoutedEventArgs e)
        {

            UserControl usc = null;
          //  usc = new Ayuda();
            //GridMain.Children.Add(usc);

        }
    }
}
